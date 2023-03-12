using System.Reflection;
using MediatR;
using TDonation.MapperProfiles;
using TDonation.Services;
using TDonation.Services.Interfaces;
using TDonation.Utils;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .Build();
// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();

// Add ENVs IOption
builder.Services.Configure<ZaloPayOption>(configuration.GetSection("ZaloPay"));
// Add Auto mapper
builder.Services.AddAutoMapper(typeof(MapperProfile));

// Add Service
builder.Services.AddScoped<IZaloPayService, ZaloPayService>();

// Add Mediat
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());



var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<PaymentGrpcService>();

app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();