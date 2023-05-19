using MediatR;

namespace Api.CQRS.WordFrequency.StringAnalyzer.Queries.CalculateHighest
{
    public record CalculateHighestQuery(string Text) : IRequest<CalculateHighestResponse>;
    public record CalculateHighestResponse(int HighestFrequency);
}
