using System.Reflection;
using System.Security.Claims;
using Firebase.Database;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SharedModels.Options;
using TPostService.Consumers;
using TPostService.GrpcServices;
using TPostService.Heplers;
using TPostService.Infrastructure;
using TPostService.MapperProfiles;
using TPostService.Services;


var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .Build();
builder.Services.Configure<ShareFirebaseOption>(configuration.GetSection("FirebaseOptions"));

var filePath = Path.Combine(builder.Environment.ContentRootPath, "firebase-credentials.json");
FirebaseApp.Create(new AppOptions
{
    Credential = GoogleCredential.FromFile(filePath)
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.Authority = configuration["Jwt:Firebase:ValidIssuer"];
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            RoleClaimType = ClaimTypes.Role,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Firebase:ValidIssuer"],
            ValidAudience = configuration["Jwt:Firebase:ValidAudience"],
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddLogging();
// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc(options =>
{
    // options.Interceptors.Add<GrpcInterceptor>();
});
builder.Services.AddGrpcReflection();

// Add RabbitMQ
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<UpdatePostApproveStatusConsumer>();
    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
    {
        cfg.Host(new Uri("rabbitmq://localhost"),h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ReceiveEndpoint("post-approve-status", ep =>
        {
            ep.PrefetchCount = 16;
            ep.UseMessageRetry(r => r.Interval(2, 100));
            ep.ConfigureConsumer<UpdatePostApproveStatusConsumer>(provider);
        });
    }));
});
builder.Services.AddMassTransitHostedService();
// Add Auto mapper
builder.Services.AddAutoMapper(typeof(PostMapperProfile));



// Add services to the container
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFirebaseService, FirebaseService>();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddDbContext<PostDbContext>(opt =>
    opt.UseNpgsql(configuration.GetConnectionString("Default")));




builder.Services.AddTransient<UserPropertyHelper>();


var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}

// Configure the HTTP request pipeline.
app.MapGrpcService<PostGrpcService>();

app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();