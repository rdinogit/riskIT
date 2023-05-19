using MediatR;

namespace Api.CQRS.WordFrequency.StringAnalyzer.Queries.CalculateForWord
{
    public record CalculateForWordQuery(string Text, string Word) : IRequest<CalculateForWordResponse>;
    public record CalculateForWordResponse(int HighestFrequency);
}
