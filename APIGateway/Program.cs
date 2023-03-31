using System.Reflection;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text.Json;
using APIGateway.AutoMapper;
using APIGateway.Configs;
using APIGateway.Extensions;
using APIGateway.Helpers;
using APIGateway.Services;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Grpc.AspNetCore.Server;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerGen;
using TDonation;
using TPostService;



var builder = WebApplication.CreateBuilder(args);

// For running in Railway
var portVar = Environment.GetEnvironmentVariable("PORT");
if (portVar is { Length: > 0 } && int.TryParse(portVar, out int port))
{
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(port);
    });
}

// CORS 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
        });
});
// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });;

var filePath = Path.Combine(builder.Environment.ContentRootPath, "firebase-credentials.json");
FirebaseApp.Create(new AppOptions
{
    Credential = GoogleCredential.FromFile(filePath)
});

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddJsonFile($"YARP.{builder.Environment.EnvironmentName}.json", true, true)
    .Build();
// var proxyBuilder = builder.Services.AddReverseProxy();
// proxyBuilder.LoadFromConfig(configuration.GetSection("ReverseProxy"));

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

// Add Rabbit
builder.Services.AddMassTransit(x =>
{
    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
    {
        config.Host(new Uri("rabbitmq://localhost"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
    }));
});
builder.Services.AddMassTransitHostedService();
// Add grpc
var urls = builder.Configuration.GetSection("UrlConfig").Get<UrlConfig>();
// builder.Services.AddSingleton<GrpcInterceptor>();
builder.Services
    .AddGrpcClient<PostGrpc.PostGrpcClient>(options =>
    {
        options.Address = new Uri(urls.PostGrpc);
        options.ChannelOptionsActions.Add(channelOptions =>
        {
            channelOptions.HttpHandler = new HttpClientHandler
            {
                SslProtocols = SslProtocols.None
                // ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
        });
    }).AddCallCredentials(async (context, metadata, serviceProvider) =>
    {
        var provider = serviceProvider.GetRequiredService<IUserService>();
        var token = await provider.AcquireToken();
        metadata.Add("Authorization", $"{token}");
    });

builder.Services.AddGrpcClient<Payment.PaymentClient>(options =>
{
    options.Address = new Uri(urls.PaymentGrpc);
    options.ChannelOptionsActions.Add(channelOptions =>
    {
        channelOptions.HttpHandler = new HttpClientHandler
        {
            SslProtocols = SslProtocols.None
            // ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
    });
}).AddCallCredentials(async (context, metadata, serviceProvider) =>
{
    var provider = serviceProvider.GetRequiredService<IUserService>();
    var token = await provider.AcquireToken();
    metadata.Add("Authorization", $"Bearer {token}");
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
    
    
});
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<UserPropertyHelper>();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// if (!app.Environment.IsDevelopment())
// {
//     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//     app.UseHsts();
// }


app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



app.Run();


