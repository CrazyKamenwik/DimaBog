using FluentValidation;
using SkyDrive.Validators;

namespace SkyDrive.IoC
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<EventValidator>();

            return services;
        }
    }
}
