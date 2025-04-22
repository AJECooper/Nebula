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
        }

        [Fact]
        public void Constructor_ShouldCreateEmptyDataFrame()
        {
            var df = new DataFrame();

            df.Should().NotBeNull();
            df.Columns.Should().BeEmpty();
            df.GetRows().Should().BeEmpty();
        }

        [Fact]
        public void Constructor_ShouldCreateDataFrameWithOnlyColumnsSet()
        {
            var df = new DataFrame(new List<string> { "Column1", "Column2" });

            df.Should().NotBeNull();
            df.Columns.Should().BeEquivalentTo(new List<string> { "Column1", "Column2" });
            df.GetRows().Should().BeEmpty();
        }

        [Fact]
        public void Constructor_ShouldCreatePopulatedDataFrame()
        {
            var data = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "Column1", "Data1" },
                    { "Column2", 1},
                    { "Column3", 0.5},
                },
                new Dictionary<string, object>
                {
                    { "Column1", "Data2" },
                    { "Column2", null},
                    { "Column3", 1.5},
                }
            };

            var df = new DataFrame(data);

            df.Should().NotBeNull();
            df.Columns.Should().BeEquivalentTo(new List<string> { "Column1", "Column2", "Column3" });
            df.Rows.Count().Should().Be(2);
        }

        [Fact]
        public void AddColumn_GivenColumnDoesNotExistInDataFrame_ShouldAppendNewColumn()
        {
            var df = new DataFrame(new List<string> { "Column1", "Column2" });

            df.AddColumn("Column3");

            df.Columns.Count().Should().Be(3);
        }

        [Fact]
        public void AddColumn_GivenColumnExistsInDataFrame_ShouldNotAppendColumn()
        {
            var df = new DataFrame(new List<string> { "Column1", "Column2" });

            df.AddColumn("Column1");

            df.Columns.Count().Should().Be(2);
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

            var df = new DataFrame(new List<string> { "Column1", "Column2", "Column3" });
            df.AddRow(newDataRow);

            df.GetRows().Count().Should().Be(1);
        }

        [Fact]
        public void Indexer_ShouldReturnCorrectValuesByColumnName()
        {
            var data = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "Column1", 1 },
                    { "Column2", 2 },
                    { "Column3", 3 }
                },
                new Dictionary<string, object>
                {
                    { "Column1", 4 },
                    { "Column2", 5 },
                    { "Column3", 6 }
                },
                new Dictionary<string, object>
                {
                    { "Column1", 7 },
                    { "Column2", 8 },
                    { "Column3", 9 }
                }
            };

            var df = new DataFrame(data);

            var columnValues = df["Column1"];

            columnValues.Count().Should().Be(3);
            columnValues.Should().BeEquivalentTo(new List<int> { 1, 4, 7 });
        }

        [Fact]
        public void Get_ShouldReturnASingleValueFromDataFrame()
        {
            var data = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "Column1", 1 },
                    { "Column2", 2 },
                    { "Column3", 3 }
                },
                new Dictionary<string, object>
                {
                    { "Column1", 4 },
                    { "Column2", 5 },
                    { "Column3", 6 }
                },
                new Dictionary<string, object>
                {
                    { "Column1", 7 },
                    { "Column2", 8 },
                    { "Column3", 9 }
                }
            };

            var df = new DataFrame(data);

            var secondValue = df.GetRows().ElementAt(1).Get<int>("Column1");

            secondValue.Should().Be(4);
        }

        [Fact]
        public void Extract_ShouldReturnDataFrameWithOnlyRequiredColumnsByColumnNames()
        {
            var data = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "Column1", "Data1" },
                    { "Column2", 1},
                    { "Column3", 0.5},
                },
                new Dictionary<string, object>
                {
                    { "Column1", "Data2" },
                    { "Column2", null},
                    { "Column3", 1.5},
                }
            };

            var df = new DataFrame(data);

            var extractedDataFrame = df.Extract("Column1", "Column3");

            extractedDataFrame.Columns.Should().BeEquivalentTo(new List<string> { "Column1", "Column3" });
            extractedDataFrame.GetRows().First().ToList().Should().BeEquivalentTo(new List<object> { "Data1", 0.5 });
        }

        [Fact]
        public void Extract_ShouldReturnDataFrameWithOnlyRequiredColumnsByColumnIndexes()
        {
            var data = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "Column1", "Data1" },
                    { "Column2", 1},
                    { "Column3", 0.5},
                },
                new Dictionary<string, object>
                {
                    { "Column1", "Data2" },
                    { "Column2", null},
                    { "Column3", 1.5},
                }
            };

            var df = new DataFrame(data);

            var extractedDataFrame = df.Extract(0, 2);

            extractedDataFrame.Columns.Should().BeEquivalentTo(new List<string> { "Column1", "Column3" });
            extractedDataFrame.GetRows().First().ToList().Should().BeEquivalentTo(new List<object> { "Data1", 0.5 });
        }

        [Fact]
        public void ToString_ShouldReturnAStringRepresentationOfTheDataFrame()
        {
            var data = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "Column1", 1 },
                    { "Column2", 2 },
                    { "Column3", 3 }
                },
                new Dictionary<string, object>
                {
                    { "Column1", 4 },
                    { "Column2", 5 },
                    { "Column3", 6 }
                },
                new Dictionary<string, object>
                {
                    { "Column1", 7 },
                    { "Column2", 8 },
                    { "Column3", 9 }
                }
            };

            var df = new DataFrame(data);

            var dfString = df.ToString();

            dfString.Should().Contain("Column1 | Column2 | Column3");
            dfString.Should().Contain("1 | 2 | 3");
            dfString.Should().Contain("4 | 5 | 6");
            dfString.Should().Contain("7 | 8 | 9");
        }
    }
}
