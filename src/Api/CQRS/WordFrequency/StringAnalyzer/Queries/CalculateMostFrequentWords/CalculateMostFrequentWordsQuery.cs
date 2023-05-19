using MediatR;
using static Api.CQRS.WordFrequency.StringAnalyzer.Queries.CalculateMostFrequentWords.CalculateMostFrequentWordsResponse;

namespace Api.CQRS.WordFrequency.StringAnalyzer.Queries.CalculateMostFrequentWords
{
    public record CalculateMostFrequentWordsQuery(string Text, int NumberOfWords) : IRequest<CalculateMostFrequentWordsResponse>;
    public record CalculateMostFrequentWordsResponse(IReadOnlyCollection<WordFrequencyDto> WordFrequencies)
    {
        public record WordFrequencyDto(string Word, int Frequency);
    };
}
