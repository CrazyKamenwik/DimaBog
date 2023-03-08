using Braintree;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using SkyDrive.Payment.Services.Models;

namespace SkyDrive.Payment.Services.MapsterConfig
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            TypeAdapterConfig<TransactionInfo, CustomerRequest>
                .NewConfig()
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.LastName, src => src.LastName)
                .Map(dest => dest.CreditCard.ExpirationMonth, src => src.ExpirationMonth)
                .Map(dest => dest.CreditCard.ExpirationYear, src => src.ExpirationYear)
                .Map(dest => dest.CreditCard.CVV, src => src.CVV);
        }
    }
}
