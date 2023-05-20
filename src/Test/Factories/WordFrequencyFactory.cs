using Test.Interfaces;

namespace Test.Factories
{
    public class WordFrequencyFactory : IWordFrequencyFactory
    {
        public IWordFrequency CreateWordFrequency(string word, int frequency)
        {
            return WordFrequency.Define(word, frequency);
        }
    }
}
