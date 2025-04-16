using FluentAssertions;
using Nebula.Data.Frame;

namespace Nebula.Data.UnitTests.Frame
{
    public class DataFrameTests
    {
        private readonly DataFrame _dataFrame;
        private IEnumerable<string> _columnHeaders;

        public DataFrameTests()
        {
            _dataFrame = new DataFrame();
            _columnHeaders = new List<string> { "Column1", "Column2", "Column3" };

            _dataFrame.SetColumns(_columnHeaders);
        }

        [Fact]
        public void SetColumns_ShouldInsertColumnNamesIntoDataFrame()
        {
            var expectedColumns = new List<string> { "Column1", "Column2", "Column3" };

            _dataFrame.GetColumns().Count().Should().Be(3);
            _dataFrame.GetColumns().Should().BeEquivalentTo(expectedColumns);
        }

        [Fact]
        public void AddRows_ShouldUpdateTheRowCountInDataFrame()
        {
            var newDataRow = new Dictionary<string, object>
            {
                { "Column1", 1 },
                { "Column2", 2 },
                { "Column3", 3 }
            };

            _dataFrame.AddRow(newDataRow);

            _dataFrame.GetRows().Count().Should().Be(1);
        }

        [Fact]
        public void Indexer_ShouldReturnCorrectValuesByColumnName()
        {
            var firstRow = new Dictionary<string, object>
            {
                { "Column1", 1 },
                { "Column2", 2 },
                { "Column3", 3 }
            };

            var secondRow = new Dictionary<string, object>
            {
                { "Column1", 4 },
                { "Column2", 5 },
                { "Column3", 6 }
            };

            _dataFrame.AddRow(firstRow);
            _dataFrame.AddRow(secondRow);

            var data = _dataFrame["Column1"];

            data.Count().Should().Be(2);
            data.Should().BeEquivalentTo(new List<int> { 1, 4 });
        }

        [Fact]
        public void Get_ShouldReturnASingleValueFromDataFrame()
        {
            var firstRow = new Dictionary<string, object>
            {
                { "Column1", 1 },
                { "Column2", 2 },
                { "Column3", 3 }
            };

            var secondRow = new Dictionary<string, object>
            {
                { "Column1", 4 },
                { "Column2", 5 },
                { "Column3", 6 }
            };

            var thirdRow = new Dictionary<string, object>
            {
                { "Column1", 7 },
                { "Column2", 8 },
                { "Column3", 9 }
            };

            _dataFrame.AddRow(firstRow);
            _dataFrame.AddRow(secondRow);
            _dataFrame.AddRow(thirdRow);

            var secondValue = _dataFrame.GetRows().ElementAt(1).Get<int>("Column1");

            secondValue.Should().Be(4);
        }

        [Fact]
        public void ToString_ShouldReturnAStringRepresentationOfTheDataFrame()
        {
            var firstRow = new Dictionary<string, object>
            {
                { "Column1", 1 },
                { "Column2", 2 },
                { "Column3", 3 }
            };

            var secondRow = new Dictionary<string, object>
            {
                { "Column1", 4 },
                { "Column2", 5 },
                { "Column3", 6 }
            };

            var thirdRow = new Dictionary<string, object>
            {
                { "Column1", 7 },
                { "Column2", 8 },
                { "Column3", 9 }
            };

            _dataFrame.AddRow(firstRow);
            _dataFrame.AddRow(secondRow);
            _dataFrame.AddRow(thirdRow);

            var dfString = _dataFrame.ToString();

            dfString.Should().Contain("Column1 | Column2 | Column3");
            dfString.Should().Contain("1 | 2 | 3");
            dfString.Should().Contain("4 | 5 | 6");
            dfString.Should().Contain("7 | 8 | 9");
        }
    }
}
