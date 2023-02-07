using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkyDrive.BLL.Interfaces;
using SkyDrive.BLL.Services;

namespace SkyDrive.BLL.IoC
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IInstructorService, InstructorService>();

            return services;
        }
    }
}
