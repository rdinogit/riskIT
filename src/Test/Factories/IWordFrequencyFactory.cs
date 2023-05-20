using Test.Interfaces;

namespace Test.Factories
{
    public interface IWordFrequencyFactory
    {
        IWordFrequency CreateWordFrequency(string word, int frequency);
    }
}
