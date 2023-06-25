using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace WebAPI.Extensions;

public static class CustomSwaggerExtension
{
    public static void AddCustomSwaggerExtension(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("V1", new OpenApiInfo { Title = "Simpra Bootcamp Project", Description="7 Haftalık Bootcamp sürecinin sonunda istenen bitirme projesinin dokümantasyonudur", Contact=new OpenApiContact { Name="Bekir Berat Kamacı", Email="b.b.kamac@gmail.com"}, Version = "V1" });


            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Sim Management for IT Company",
                Description = "Enter JWT Bearer token **_only_**",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {securityScheme, new string[] { }}
            });
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);

        });
    }
}
