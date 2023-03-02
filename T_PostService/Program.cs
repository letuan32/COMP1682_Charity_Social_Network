using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using T_PostService.Heplers;
using T_PostService.Infrastructure;
using T_PostService.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();


IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddJsonFile($"YARP.{builder.Environment.EnvironmentName}.json", true, true)
    .Build();

// Add services to the container.s
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
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();