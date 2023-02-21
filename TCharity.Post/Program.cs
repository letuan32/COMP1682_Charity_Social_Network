

using Microsoft.EntityFrameworkCore;
using TCharity.Post.Heplers;
using TCharity.Post.Infrastructure;
using TCharity.Post.Queries;
using TCharity.Post.ViewModels;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddJsonFile($"YARP.{builder.Environment.EnvironmentName}.json", true, true)
    .Build();
// Add services to the container.


builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<PostContext>(opt =>
    opt.UseNpgsql(configuration.GetConnectionString("Default")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<UserPropertyService>();
builder.Services.AddTransient<UserPropertyService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();