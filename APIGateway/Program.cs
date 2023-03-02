using System.Security.Claims;
using APIGateway.Configs;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using T_PostService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

FirebaseApp.Create(new AppOptions
{
    Credential = GoogleCredential.FromFile(@"D:\TuanLe\TCharity\TCharity\tcharity-identity-service-firebase-adminsdk-o2fik-a1d7743340.json")
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

// Add grpc
var urls = builder.Configuration.GetSection("UrlConfig").Get<UrlConfig>();
builder.Services.AddGrpcClient<PostGrpc.PostGrpcClient>(options =>
{
    options.Address = new Uri(urls.PostGrpc);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthorization(options =>
{
    // This is a default authorization policy which requires authentication
    options.AddPolicy("admin", policy =>
    {
        policy.RequireAuthenticatedUser().RequireClaim(ClaimTypes.Role,"admin");
    });
    options.AddPolicy("user", policy =>
    {
        policy.RequireAuthenticatedUser().RequireClaim(ClaimTypes.Role,"user");;
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



app.Run();


