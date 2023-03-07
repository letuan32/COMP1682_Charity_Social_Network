using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
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


builder.Services.AddLogging();
// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();


// Add Auto mapper
builder.Services.AddAutoMapper(typeof(PostMapperProfile));



// Add services to the container.s
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<PostContext>(opt =>
    opt.UseNpgsql(configuration.GetConnectionString("Default")));




builder.Services.AddTransient<UserPropertyService>();


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<PostGrpcService>();

app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();