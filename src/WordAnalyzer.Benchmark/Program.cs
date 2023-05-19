
using BenchmarkDotNet.Running;
using WordAnalyzer.Benchmark;

var summary = BenchmarkRunner.Run<WordFrequencyAnalyzerRegexProviderVsWordFrequencyAnalyzerStringProvider>();
