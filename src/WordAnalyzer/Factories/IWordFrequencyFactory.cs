using WordAnalyzer.Interfaces;

namespace WordAnalyzer.Factories
{
    public interface IWordFrequencyFactory
    {
        IWordFrequency CreateWordFrequency(string word, int frequency);
    }
}
