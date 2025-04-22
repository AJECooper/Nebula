using BenchmarkDotNet.Attributes;
using Bogus;
using Nebula.Data.IO;

namespace Nebula.Data.Benchmarks.DataReaders
{
    [MemoryDiagnoser]
    public class CsvReaderBenchmarks
    {
        private string _csvFilePath;

        [GlobalSetup(Target = nameof(FromCsv_SmallCsvFile_10Rows))]
        public void SetupSmallCsvFile()
        {
            _csvFilePath = "small.csv";
            GenerateCsvFile(_csvFilePath, 10);
        }

        [GlobalSetup(Target = nameof(FromCsv_MediumCsvFile_1000Rows))]
        public void SetupMediumCsvFile()
        {
            _csvFilePath = "medium.csv";
            GenerateCsvFile(_csvFilePath, 1000);
        }

        [GlobalSetup(Target = nameof(FromCsv_LargeCsvFile_100000Rows))]
        public void SetupLargeCsvFile()
        {
            _csvFilePath = "large.csv";
            GenerateCsvFile(_csvFilePath, 100000);
        }

        [Benchmark]
        public void FromCsv_SmallCsvFile_10Rows()
        {
            CsvReader.FromCsv(_csvFilePath);
        }

        [Benchmark]
        public void FromCsv_MediumCsvFile_1000Rows()
        {
            CsvReader.FromCsv(_csvFilePath);
        }

        [Benchmark]
        public void FromCsv_LargeCsvFile_100000Rows()
        {
            CsvReader.FromCsv(_csvFilePath);
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            if (File.Exists(_csvFilePath))
            {
                File.Delete(_csvFilePath);
            }
        }

        private void GenerateCsvFile(string filePath, int rowCount)
        {
            var faker = new Faker();
            var headers = "First Name,Surname,Email,Score";

            var rows = new List<string>();

            using (var writer = new StreamWriter(filePath))
            {
                // Write the headers
                writer.WriteLine(headers);

                // Write each row
                for (var i = 0; i < rowCount; i++)
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
