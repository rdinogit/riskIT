using WordAnalyzer.Exceptions;
using WordAnalyzer.Extensions;
using WordAnalyzer.Factories;
using WordAnalyzer.Interfaces;

namespace WordAnalyzer.Providers
{
    public class WordFrequencyAnalyzerStringProvider : IWordFrequencyAnalyzerString
    {
        private readonly IWordFrequencyFactory _wordFrequencyFactory;

        public WordFrequencyAnalyzerStringProvider(IWordFrequencyFactory wordFrequencyFactory)
        {
            _wordFrequencyFactory = wordFrequencyFactory;
        }

        public int CalculateHighestFrequency(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 0;

            var words = ExtractWords(text);
            var highestFrequency = words
                .GroupBy(word => word)
                .Select(g => g.Count())
                .DefaultIfEmpty(0)
                .Max();

            return highestFrequency;
        }

        public int CalculateFrequencyForWord(string text, string word)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 0;

            ValidateSingleWord(word);

            var words = ExtractWords(text);
            var frequency = words
                .Where(x => x == word.ToLowerInvariant())
                .Count();

            return frequency;
        }

        public IList<IWordFrequency> CalculateMostFrequentNWords(string text, int n)
        {
            if (string.IsNullOrWhiteSpace(text) || n <= 0)
                return new List<IWordFrequency>();

            var words = ExtractWords(text);
            var mostFrequentWords = words
                .GroupBy(x => x)
                .Select(x => _wordFrequencyFactory.CreateWordFrequency(x.Key, x.Count()))
                .OrderByDescending(x => x.Frequency)
                .ThenByDescending(x => x.Word)
                .Take(n)
                .ToList();

            return mostFrequentWords;
        }

        private static string[] ExtractWords(string text, bool makeLowerCase = true)
        {
            var filteredText = text.ReplaceNonAlphabeticalCharacters(' ');
            var words = filteredText.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (!makeLowerCase)
                return words;

            return words
                .Select(x => x.ToLowerInvariant())
                .ToArray();
        }

        private static void ValidateSingleWord(string word)
        {
            bool invalid = false;
            invalid |= string.IsNullOrWhiteSpace(word);

            var words = ExtractWords(word);

            invalid |= words.Length != 1;

            if (invalid)
                throw new InvalidWordException(word);
        }
    }
}
