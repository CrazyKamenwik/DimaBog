using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkyDrive.BLL.Interfaces;
using SkyDrive.BLL.Services;
using SkyDrive.DAL.IoC;

namespace SkyDrive.BLL.IoC
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddContext(configuration);

            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IInstructorSerivce, InstructorService>();

            return services;
        }
    }
}
