using FluentValidation;

namespace Api.CQRS.WordFrequency.RegexAnalyzer.Queries.CalculateForWord
{
    public class CalculateForWordQueryValidator : AbstractValidator<CalculateForWordQuery>
    {
        public CalculateForWordQueryValidator()
        {
            RuleFor(x => x.Text).NotEmpty();
            RuleFor(x => x.Word).NotEmpty();
        }
    }
}
