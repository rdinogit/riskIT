using Api.Contract;
using Api.CQRS.WordFrequency.RegexAnalyzer.Queries.CalculateForWord;
using Api.CQRS.WordFrequency.RegexAnalyzer.Queries.CalculateHighest;
using Api.CQRS.WordFrequency.RegexAnalyzer.Queries.CalculateMostFrequentWords;
using Mapster;

namespace Api.Mapping
{
    public class CqrsRegexMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CalculateForWordRequest, CalculateForWordQuery>();
            config.NewConfig<CalculateForWordsRequest, CalculateMostFrequentWordsQuery>();
            config.NewConfig<CalculateHighestRequest, CalculateHighestQuery>();
        }
    }
}
