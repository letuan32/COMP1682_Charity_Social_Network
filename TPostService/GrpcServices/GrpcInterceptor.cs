using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using TPostService.Heplers;

namespace TPostService.GrpcServices;

public class GrpcInterceptor : Interceptor
{
    private readonly IConfiguration _configuration;

    public GrpcInterceptor(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
    {
        var token = context.RequestHeaders.GetValue("Authorization")?.Substring("Bearer ".Length);
        if(token == null) return await continuation(request, context);
        
        List<X509SecurityKey> issuerSigningKeys = GetIssuerSigningKeys();
        // Authenticate the user
        var validationParameters = new TokenValidationParameters
        {
            RoleClaimType = ClaimTypes.Role,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _configuration["Jwt:Firebase:ValidIssuer"],
            ValidAudience = _configuration["Jwt:Firebase:ValidAudience"],
            IssuerSigningKeyResolver = (arbitrarily, declaring, these, parameters) => issuerSigningKeys
        };
        var handler = new JwtSecurityTokenHandler();
        // var principal = handler.ValidateToken(token, validationParameters, out var validatedToken);
        
        return await continuation(request, context);
    }
    
    
    private static List<X509SecurityKey> GetIssuerSigningKeys()
    {
        HttpClient client = new HttpClient();
        var task = client.GetStringAsync("https://www.googleapis.com/robot/v1/metadata/x509/securetoken@system.gserviceaccount.com");
        task.Wait();
        string jsonResult = task.Result;

        //Extract X509SecurityKeys from JSON result
        List<X509SecurityKey> x509IssuerSigningKeys = JObject.Parse(jsonResult)
            .Children()
            .Cast<JProperty>()
            .Select(i => BuildSecurityKey(i.Value.ToString())).ToList();

        return x509IssuerSigningKeys;
    }

    private static X509SecurityKey BuildSecurityKey(string certificate)
    {
        //Removing "-----BEGIN CERTIFICATE-----" and "-----END CERTIFICATE-----" lines
        var lines = certificate.Split('\n');
        var selectedLines = lines.Skip(1).Take(lines.Length - 3);
        var key = string.Join(Environment.NewLine, selectedLines);

        return new X509SecurityKey(new X509Certificate2(Convert.FromBase64String(key)));
    }
}