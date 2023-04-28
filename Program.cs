// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using MapperBenchmarks;

var summary = BenchmarkRunner.Run(typeof(Benchmarks));

