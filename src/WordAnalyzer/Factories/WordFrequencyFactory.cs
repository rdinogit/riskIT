using WordAnalyzer.Interfaces;

namespace WordAnalyzer.Factories
{
    public class WordFrequencyFactory : IWordFrequencyFactory
    {
        public IWordFrequency CreateWordFrequency(string word, int frequency)
        {
            return WordFrequency.Define(word, frequency);
        }
    }
}
