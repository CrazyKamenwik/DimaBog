using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkyDrive.DAL.IoC;

namespace SkyDrive.BLL.IoC
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddContext(configuration);

            return services;
        }

    }
}
