using MediatR;

namespace Api.CQRS.WordFrequency.RegexAnalyzer.Queries.CalculateHighest
{
    public record CalculateHighestQuery(string Text) : IRequest<CalculateHighestResponse>;
    public record CalculateHighestResponse(int HighestFrequency);
}
