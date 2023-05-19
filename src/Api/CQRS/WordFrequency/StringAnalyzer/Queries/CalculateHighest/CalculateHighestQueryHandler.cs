using MediatR;
using WordAnalyzer.Providers;

namespace Api.CQRS.WordFrequency.StringAnalyzer.Queries.CalculateHighest
{
    public class CalculateHighestQueryHandler : IRequestHandler<CalculateHighestQuery, CalculateHighestResponse>
    {
        private readonly IWordFrequencyAnalyzerRegex _analyzer;

        public CalculateHighestQueryHandler(IWordFrequencyAnalyzerRegex analyzer)
        {
            _analyzer = analyzer;
        }

        public Task<CalculateHighestResponse> Handle(CalculateHighestQuery request, CancellationToken cancellationToken)
        {
            var highestFrequency = _analyzer.CalculateHighestFrequency(request.Text);
            return Task.FromResult(new CalculateHighestResponse(highestFrequency));
        }
    }
}
