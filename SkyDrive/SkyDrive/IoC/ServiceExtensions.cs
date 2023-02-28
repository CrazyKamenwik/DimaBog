using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using SkyDrive.Authentication;
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

        public static IServiceCollection AddAuth0(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = "https://dev-ih5zsl10t5jsta03.eu.auth0.com/";
                options.Audience = "AufZero";
            });

            return services;
        }

        public static IServiceCollection AddSwaggerAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            Scopes = new Dictionary<string, string>
                            {
                                { "openid", "Open Id" }
                            },
                            AuthorizationUrl = new Uri(configuration["Authentication:Domain"] +
                                                       "authorize?audience=" +
                                                       configuration["Authentication:Audience"])
                        }
                    }
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            return services;
        }
    }
}