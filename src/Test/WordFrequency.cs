using Test.Interfaces;
using Test.Models;

namespace Test
{
    public class WordFrequency : ValueObject, IWordFrequency
    {
        private WordFrequency() : this(string.Empty, 0)
        {
        }

        private WordFrequency(string word, int frequency)
        {
            Word = word;
            Frequency = frequency;
        }

        public string Word { get; private init; }
        public int Frequency { get; private init; }

        public static WordFrequency Define(string word, int frequency)
        {
            _ = string.IsNullOrWhiteSpace(word) ? throw new ArgumentException("A word cannot be empty.", nameof(word)) : word;
            _ = frequency < 0 ? throw new ArgumentException("Frequency cannot be lower than 0.", nameof(frequency)) : frequency;

            return new(word.ToLowerInvariant(), frequency);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Word;
            yield return Frequency;
        }
    }
}
