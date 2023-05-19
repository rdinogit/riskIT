using MediatR;
using WordAnalyzer.Interfaces;
using WordAnalyzer.Providers;
using static Api.CQRS.WordFrequency.RegexAnalyzer.Queries.CalculateMostFrequentWords.CalculateMostFrequentWordsResponse;

namespace Api.CQRS.WordFrequency.RegexAnalyzer.Queries.CalculateMostFrequentWords
{
    public class CalculateMostFrequentWordsQueryHandler : IRequestHandler<CalculateMostFrequentWordsQuery, CalculateMostFrequentWordsResponse>
    {
        private readonly IWordFrequencyAnalyzerRegex _analyzer;

        public CalculateMostFrequentWordsQueryHandler(IWordFrequencyAnalyzerRegex analyzer)
        {
            _analyzer = analyzer;
        }

        public Task<CalculateMostFrequentWordsResponse> Handle(CalculateMostFrequentWordsQuery request, CancellationToken cancellationToken)
        {
            var frequencies = _analyzer.CalculateMostFrequentNWords(request.Text, request.NumberOfWords);
            var responseFrequencies = frequencies
                .Select(Map)
                .ToArray();

            return Task.FromResult(new CalculateMostFrequentWordsResponse(responseFrequencies));
        }

        private static WordFrequencyDto Map(IWordFrequency wordFrequency) => new(wordFrequency.Word, wordFrequency.Frequency);
    }
}
