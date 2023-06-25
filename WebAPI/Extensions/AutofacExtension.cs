using Autofac.Extensions.DependencyInjection;
using Autofac;
using Business.DependencyResolver.Autofac;

namespace WebAPI.Extensions;

public static class AutofacExtension
{
    public static void AddAutofacExtension(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterModule(new DependencyResolverModule());
            });
    }
}