using Api.Contract;
using Api.CQRS.WordFrequency.StringAnalyzer.Queries.CalculateForWord;
using Api.CQRS.WordFrequency.StringAnalyzer.Queries.CalculateHighest;
using Api.CQRS.WordFrequency.StringAnalyzer.Queries.CalculateMostFrequentWords;
using Mapster;

namespace Api.Mapping
{
    public class CqrsStringMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CalculateForWordRequest, CalculateForWordQuery>();
            config.NewConfig<CalculateForWordsRequest, CalculateMostFrequentWordsQuery>();
            config.NewConfig<CalculateHighestRequest, CalculateHighestQuery>();
        }
    }
}
