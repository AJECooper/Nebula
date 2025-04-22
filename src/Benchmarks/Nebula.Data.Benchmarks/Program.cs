using BenchmarkDotNet.Running;
using Nebula.Data.Benchmarks.DataReaders;

class Program
{
    static void Main(string[] args)
    {
        BenchmarkRunner.Run<CsvReaderBenchmarks>();
    }
}