using Microsoft.Extensions.DependencyInjection;
using System.Infra;

namespace System.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddInfrastructure();

            services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssemblies(assembly));

            return services;
        }
    }
}
