using Microsoft.Extensions.DependencyInjection;
using WordAnalyzer.Factories;
using WordAnalyzer.Providers;

namespace WordAnalyzer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWordFrequencyAnalyzers(this IServiceCollection services)
        {
            services.AddScoped<IWordFrequencyFactory, WordFrequencyFactory>();
            services.AddScoped<IWordFrequencyAnalyzerRegex, WordFrequencyAnalyzerRegexProvider>();
            services.AddScoped<IWordFrequencyAnalyzerString, WordFrequencyAnalyzerStringProvider>();
            return services;
        }
    }
}
