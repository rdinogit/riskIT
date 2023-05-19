using MediatR;
using WordAnalyzer.Providers;

namespace Api.CQRS.WordFrequency.StringAnalyzer.Queries.CalculateForWord
{
    public class CalculateForWordQueryHandler : IRequestHandler<CalculateForWordQuery, CalculateForWordResponse>
    {
        private readonly IWordFrequencyAnalyzerString _analyzer;

        public CalculateForWordQueryHandler(IWordFrequencyAnalyzerString analyzer)
        {
            _analyzer = analyzer;
        }

        public Task<CalculateForWordResponse> Handle(CalculateForWordQuery request, CancellationToken cancellationToken)
        {
            var frequency = _analyzer.CalculateFrequencyForWord(request.Text, request.Word);
            return Task.FromResult(new CalculateForWordResponse(frequency));
        }
    }
}
