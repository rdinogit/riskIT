using Test.Exceptions;
using Test.Factories;
using Test.Interfaces;
using System.Text.RegularExpressions;

namespace Test.Providers
{
    public partial class WordFrequencyAnalyzerRegexProvider : IWordFrequencyAnalyzerRegex
    {
        private static readonly Regex s_rxWord = Word();
        private static readonly Regex s_rxNotAWord = NotAWord();

        private readonly IWordFrequencyFactory _wordFrequencyFactory;

        public WordFrequencyAnalyzerRegexProvider(IWordFrequencyFactory wordFrequencyFactory)
        {
            _wordFrequencyFactory = wordFrequencyFactory;
        }

        public int CalculateHighestFrequency(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 0;

            var matches = GetGroupedWordMatches(text);
            var highestFrequency = matches
                .Select(x => x.Count())
                .DefaultIfEmpty(0)
                .Max();

            return highestFrequency;
        }

        public int CalculateFrequencyForWord(string text, string word)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 0;

            ValidateSingleWord(word);

            var rx = new Regex($@"(?<=[^a-z]|^){word}(?=[^a-z]|$)", RegexOptions.IgnoreCase);
            var matches = rx.Matches(text);

            return matches.Count;
        }

        public IList<IWordFrequency> CalculateMostFrequentNWords(string text, int n)
        {
            if (string.IsNullOrWhiteSpace(text) || n <= 0)
                return new List<IWordFrequency>();

            var matches = GetGroupedWordMatches(text);
            var mostFrequentWords = matches
                .Select(x => _wordFrequencyFactory.CreateWordFrequency(x.Key, x.Count()))
                .OrderByDescending(x => x.Frequency)
                .ThenByDescending(x => x.Word)
                .Take(n)
                .ToList();

            return mostFrequentWords;
        }

        private static void ValidateSingleWord(string word)
        {
            bool invalid = false;
            invalid |= string.IsNullOrWhiteSpace(word);
            invalid |= s_rxNotAWord.IsMatch(word);

            if (invalid)
                throw new InvalidWordException(word);
        }

        private static IEnumerable<IGrouping<string, Match>> GetGroupedWordMatches(string text)
        {
            return s_rxWord
                .Matches(text)
                .GroupBy(m => m.Value.ToLowerInvariant());
        }

        [GeneratedRegex("[a-z]+", RegexOptions.IgnoreCase)]
        private static partial Regex Word();

        [GeneratedRegex("[^a-z]", RegexOptions.IgnoreCase)]
        private static partial Regex NotAWord();
    }
}
