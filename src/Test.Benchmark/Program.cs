
using BenchmarkDotNet.Running;
using Test.Benchmark;

var summary = BenchmarkRunner.Run<WordFrequencyAnalyzerRegexProviderVsWordFrequencyAnalyzerStringProvider>();
