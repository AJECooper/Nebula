// <copyright file="JsonReaderBenchmarks.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.Data.Benchmarks.DataReaders
{
    using System.Text.Json;
    using BenchmarkDotNet.Attributes;
    using Bogus;
    using Nebula.Data.IO;

    [MemoryDiagnoser]
    public class JsonReaderBenchmarks
    {
        [Params(100, 1000, 10000, 50000, 100000)]
        public int RecordCount;

        private string _filePath = "data.json";

        [GlobalSetup]
        public void Setup()
        {
            GenerateFakeData(_filePath, RecordCount);
        }

        [Benchmark(Description = "Read from json file and returns new data table.")]
        public void Benchmark_FromJson()
        {
            JsonReader.FromJson(_filePath);
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
            var data = new List<Dictionary<string, object>>(rowCount);

            for (int i = 0; i < rowCount; i++)
            {
                var row = new Dictionary<string, object>
                {
                    ["FirstName"] = faker.Name.FirstName(),
                    ["Surname"] = faker.Name.LastName(),
                    ["Email"] = faker.Internet.Email(),
                    ["Score"] = faker.Random.Int(0, 100)
                };
                data.Add(row);
            }

            var json = JsonSerializer.Serialize(data);
            File.WriteAllText(filePath, json);
        }
    }
}
