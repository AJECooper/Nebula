// <copyright file="CsvReaderBenchmarks.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.Data.Benchmarks.DataReaders
{
    using BenchmarkDotNet.Attributes;
    using Bogus;
    using Nebula.Data.IO;

    [MemoryDiagnoser]
    public class CsvReaderBenchmarks
    {
        [Params(100, 1000, 10000, 50000, 100000)]
        public int RecordCount;

        private string _filePath = "data.csv";

        [GlobalSetup]
        public void Setup()
        {
            GenerateFakeData(_filePath, RecordCount);
        }

        [Benchmark(Description = "Read from csv file and returns new data table.")]
        public void Benchmark_FromCsv()
        {
            CsvReader.FromCsv(_filePath);
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }

        private void GenerateFakeData(string filePath, int rowCount)
        {
            var faker = new Faker();
            var headers = "FirstName,Surname,Email,Score";

            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine(headers);
                for (int i = 0; i < rowCount; i++)
                {
                    var firstName = faker.Name.FirstName();
                    var surname = faker.Name.LastName();
                    var email = faker.Internet.Email();
                    var score = faker.Random.Int(0, 100);

                    writer.WriteLine($"{firstName},{surname},{email},{score}");
                }
            }
        }
    }
}
