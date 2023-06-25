using AutoMapper;
using Business.Mapping;

namespace WebAPI.Extensions;

public static class MapperExtension
{
    public static void AddMapperExtension(this IServiceCollection services)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperProfile());
        });
        services.AddSingleton(config.CreateMapper());
    }
}
