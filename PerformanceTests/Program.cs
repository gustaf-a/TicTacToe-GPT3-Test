
using BenchmarkDotNet.Running;
using PerformanceTests.MoveMaker;

var summary = BenchmarkRunner.Run<MoveMakerSmallBoardBenchMarkRunner>();

Console.ReadKey();