using System.Reflection;
using System.Security.Claims;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TDonation.Consumers;
using TDonation.GrpcServices;
using TDonation.MapperProfiles;
using TDonation.Services;
using TDonation.Services.Interfaces;
using TDonation.Utils;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .Build();

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
// Add Rabbit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<PaymentConsumer>();
    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
    {
        cfg.Host(new Uri("rabbitmq://localhost"),h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ReceiveEndpoint("zalo-callback", ep =>
        {
            ep.PrefetchCount = 16;
            ep.UseMessageRetry(r => r.Interval(2, 100));
            ep.ConfigureConsumer<PaymentConsumer>(provider);
        });
    }));
});
builder.Services.AddMassTransitHostedService();
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

// Add MediatR
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());



var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<PaymentGrpcService>();

app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();