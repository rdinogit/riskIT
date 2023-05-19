using Api.Providers.Identity;
using Api.Providers.IdentityProvider;
using Api.Providers.TimeDate;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WordAnalyzer.Factories;
using WordAnalyzer.Providers;

namespace Api.Providers
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProviders(this IServiceCollection services)
        {
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<IWordFrequencyFactory, WordFrequencyFactory>();
            services.AddScoped<IWordFrequencyAnalyzerRegex, WordFrequencyAnalyzerRegexProvider>();
            services.AddScoped<IWordFrequencyAnalyzerString, WordFrequencyAnalyzerStringProvider>();
            return services;
        }

        public static IServiceCollection AddJwtTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var configSection = configuration.GetSection(JwtTokenProviderSettings.SectionName);
            services.Configure<JwtTokenProviderSettings>(configSection);
            services.AddSingleton<IJwtTokenProvider, JwtTokenProvider>();

            var jwtSettings = new JwtTokenProviderSettings();
            configSection.Bind(jwtSettings);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                    ClockSkew = TimeSpan.Zero
                });

            return services;
        }
    }
}
