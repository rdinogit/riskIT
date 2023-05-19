using FluentValidation;

namespace Api.CQRS.WordFrequency.RegexAnalyzer.Queries.CalculateMostFrequentWords
{
    public class CalculateMostFrequentWordsQueryValidator : AbstractValidator<CalculateMostFrequentWordsQuery>
    {
        public CalculateMostFrequentWordsQueryValidator()
        {
            RuleFor(x => x.Text).NotEmpty();
            RuleFor(x => x.NumberOfWords).GreaterThan(0);
        }
    }
}
