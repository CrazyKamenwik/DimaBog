using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkyDrive.DAL.Interfaces;
using SkyDrive.DAL.Repositories;

namespace SkyDrive.DAL.IoC
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DockerConnection"));
            });

            services.AddRepositories();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IInstructorRepository, InstructorRepository>();

            return services;
        }
    }
}
