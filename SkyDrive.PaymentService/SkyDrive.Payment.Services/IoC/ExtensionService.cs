using Microsoft.Extensions.DependencyInjection;
using SkyDrive.Payment.Services.Abstractions;
using BraintreeService = SkyDrive.Payment.Services.Services.BraintreeService;

namespace SkyDrive.Payment.Services.IoC
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddBraintree(this IServiceCollection services)
        {
            services.AddScoped<IBraintreeService, BraintreeService>();

            return services;
        }
    }
}
