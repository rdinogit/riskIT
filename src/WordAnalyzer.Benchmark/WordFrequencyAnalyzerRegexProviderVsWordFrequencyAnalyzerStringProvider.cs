using BenchmarkDotNet.Attributes;
using Bogus;
using WordAnalyzer.Factories;
using WordAnalyzer.Interfaces;
using WordAnalyzer.Providers;

namespace WordAnalyzer.Benchmark
{
    public class WordFrequencyAnalyzerRegexProviderVsWordFrequencyAnalyzerStringProvider
    {
        private readonly string _text;
        private readonly string _word;
        private readonly WordFrequencyAnalyzerRegexProvider _regexAnalyzer;
        private readonly WordFrequencyAnalyzerStringProvider _stringAnalyzer;

        public WordFrequencyAnalyzerRegexProviderVsWordFrequencyAnalyzerStringProvider()
        {
            _text = new Faker("en").Lorem.Sentence(1000);
            _word = "lorem";
            var factory = new WordFrequencyFactory();
            _regexAnalyzer = new WordFrequencyAnalyzerRegexProvider(factory);
            _stringAnalyzer = new WordFrequencyAnalyzerStringProvider(factory);
        }


        // CalculateHighestFrequency
        [Benchmark]
        public int Regex_CalculateHighestFrequency() => _regexAnalyzer.CalculateHighestFrequency(_text);
        
        [Benchmark]
        public int String_CalculateHighestFrequency() => _stringAnalyzer.CalculateHighestFrequency(_text);


        // CalculateFrequencyForWord
        [Benchmark]
        public int Regex_CalculateFrequencyForWord() => _regexAnalyzer.CalculateFrequencyForWord(_text, _word);

        [Benchmark]
        public int String_CalculateFrequencyForWord() => _stringAnalyzer.CalculateFrequencyForWord(_text, _word);


        // CalculateMostFrequentNWords
        [Benchmark]
        public IList<IWordFrequency> Regex_CalculateMostFrequentNWords() => _regexAnalyzer.CalculateMostFrequentNWords(_text, 100);

        [Benchmark]
        public IList<IWordFrequency> String_CalculateMostFrequentNWords() => _stringAnalyzer.CalculateMostFrequentNWords(_text, 100);
    }
}
