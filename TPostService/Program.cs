using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using T_PostService.GrpcServices;
using T_PostService.Heplers;
using T_PostService.Infrastructure;
using T_PostService.MapperProfiles;
using T_PostService.Services;

var builder = WebApplication.CreateBuilder(args);
// For running in Railway
var portVar = Environment.GetEnvironmentVariable("PORT");
if (portVar is { Length: > 0 } && int.TryParse(portVar, out int port))
{
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(port, listenOptions =>
        {
            listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
        });
    });
}

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .Build();


builder.Services.AddLogging();
// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
}));


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
// if (app.Environment.IsDevelopment())
// {
//     app.MapGrpcReflectionService();
// }


app.UseRouting();
// app.MapGrpcService<GreeterService>();
// app.MapGrpcService<PostGrpcService>();
app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
app.UseCors();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<GreeterService>().EnableGrpcWeb();
    endpoints.MapGrpcService<PostGrpcService>().EnableGrpcWeb();

});
app.Run();