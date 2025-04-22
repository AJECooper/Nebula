using FluentAssertions;
using Nebula.Data.IO;
using System.Data.Common;

namespace Nebula.Data.UnitTests.DataReader.Csv
{
    public class CsvReaderTests : IDisposable
    {
        private readonly string _testFilePath = Path.GetTempFileName();

        public CsvReaderTests()
        {
        }

        [Fact]
        public void FromCsv_ShouldThrowException_WhenFileIsEmpty()
        {
            File.WriteAllText(_testFilePath, string.Empty);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => CsvReader.FromCsv(_testFilePath));
            Assert.Equal("The provided file is empty.", exception.Message);
        }

        [Fact]
        public void FromCsv_ShouldReturnDataFrame_WhenFileIsValid()
        {
            var fileContent = "Column1,Column2\ndp1,dp2\ndp3,dp4";
            File.WriteAllText(_testFilePath, fileContent);

            var df = CsvReader.FromCsv(_testFilePath);

            var columns = df.Columns;
            columns.Should().BeEquivalentTo(new List<string> { "Column1", "Column2" });
        }

        [Fact]
        public void FromCsv_ShouldReturnDataFrame_GivenValidFilePathWithSpacesInData()
        {
            var fileContent = "Column1 ,Column2 \n dp1, dp2 \n dp3, dp4";
            File.WriteAllText(_testFilePath, fileContent);

            var df = CsvReader.FromCsv(_testFilePath);

            var columns = df.Columns;
            var data = df.GetRows();

            columns.Should().BeEquivalentTo(new List<string> { "Column1", "Column2" });
            data.Should().HaveCount(2);
        }

        public void Dispose()
        {
            File.Delete(_testFilePath);
        }
    }
}
