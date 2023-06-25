using Base.Entities.Concrete;
using Base.Utilities.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using WebAPI.Extensions;
using WebAPI.Middleware;

namespace WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddMapperExtension();        // Automapper Implementation
        builder.Host.AddAutofacExtension();           // Autofac Implementation
        builder.Services.AddCustomSwaggerExtension(); // CustomSwagger Implementation

        var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))
                    };
                });
        builder.Services.AddAuthorization();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/V1/swagger.json", "Final Project");
            });
        }

        app.UseHttpsRedirection();

        app.UseMiddleware<ErrorHandlerMiddleware>();
        Action<RequestProfilerModel> requestResponseHandler = requestProfilerModel =>
        {
            Log.Information("-------------Request-Begin------------");
            Log.Information(requestProfilerModel.Request);
            Log.Information(Environment.NewLine);
            Log.Information(requestProfilerModel.Response);
            Log.Information("-------------Request-End------------");
        };
        app.UseMiddleware<RequestLoggingMiddleware>(requestResponseHandler);

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}