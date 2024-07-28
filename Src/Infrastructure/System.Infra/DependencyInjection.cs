using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Domain.Auth.Repositories;
using System.Infra.Models.Auth.Repositories;

namespace System.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        IConfiguration configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

        //services.AddDomain();

        services.AddScoped<IUserRepository, UserRepository>();



        return services;
    }
}
