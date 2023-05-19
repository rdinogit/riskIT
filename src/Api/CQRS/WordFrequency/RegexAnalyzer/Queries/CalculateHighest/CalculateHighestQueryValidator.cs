using FluentValidation;

namespace Api.CQRS.WordFrequency.RegexAnalyzer.Queries.CalculateHighest
{
    public class CalculateHighestQueryValidator : AbstractValidator<CalculateHighestQuery>
    {
        public CalculateHighestQueryValidator()
        {
            RuleFor(x => x.Text).NotEmpty();
        }
    }
}
