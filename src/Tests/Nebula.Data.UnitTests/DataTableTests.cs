// <copyright file="DataTableTests.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.Data.UnitTests
{
    using FluentAssertions;
    using Nebula.Data.Structures;

    public class DataTableTests
    {
        public DataTableTests()
        {
        }

        [Fact]
        public void Constructor_ShouldNotThrowException_GivenValidData()
        {
            // Arrange
            var tableData = new List<Dictionary<string, object>>
            {
                new() { { "Name", "Alice" }, { "Age", 30 } },
                new() { { "Name", "Bob" }, { "Age", 25 } },
            };

            // Act
            Action act = () => new DataTable(tableData);

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void Constructor_ShouldThrowException_GivenNullData()
        {
            // Arrange
            var tableData = new List<Dictionary<string, object>>
            {
            };

            // Act
            Action act = () => new DataTable(tableData);

            // Assert
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Constructor_ShouldThrowException_GivenInconsistentKeys()
        {
            // Arrange
            var tableData = new List<Dictionary<string, object>>
            {
                new() { { "Name", "Alice" }, { "Age", 30 } },
                new() { { "Name", "Bob" }, { "Location", "London" } },
            };

            // Act
            Action act = () => new DataTable(tableData);

            // Assert
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Constructor_ShouldNotThrowException_GivenNullValues()
        {
            // Arrange
            var tableData = new List<Dictionary<string, object>>
            {
                new() { { "Name", "Alice" }, { "Age", 30 } },
                new() { { "Name", "Bob" }, { "Age", null } },
            };

            // Act
            Action act = () => new DataTable(tableData);

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void ColumnCount_ShouldReturnNumberOfColumns_GivenValidDataTable()
        {
            // Arrange
            var tableData = new List<Dictionary<string, object>>
            {
                new() { { "Name", "Alice" }, { "Age", 30 } },
                new() { { "Name", "Bob" }, { "Age", 25 } },
            };

            // Act
            var dataTable = new DataTable(tableData);

            // Assert
            dataTable.ColumnCount.Should().Be(2);
        }

        [Fact]
        public void RowCount_ShouldReturnNumberOfColumns_GivenValidDataTable()
        {
            // Arrange
            var tableData = new List<Dictionary<string, object>>
            {
                new() { { "Name", "Alice" }, { "Age", 30 } },
                new() { { "Name", "Bob" }, { "Age", 25 } },
                new() { { "Name", "Tim" }, { "Age", 40 } },
            };

            // Act
            var dataTable = new DataTable(tableData);

            // Assert
            dataTable.RowCount.Should().Be(3);
        }

        [Fact]
        public void Columns_ShouldReturnColumnNames_GivenValidDataTable()
        {
            // Arrange
            var tableData = new List<Dictionary<string, object>>
            {
                new() { { "Name", "Alice" }, { "Age", 30 } },
                new() { { "Name", "Bob" }, { "Age", 25 } },
            };

            // Act
            var dataTable = new DataTable(tableData);

            // Assert
            dataTable.Columns.Should().BeEquivalentTo(new List<string> { "Name", "Age" });
        }

        [Fact]
        public void GetRowByIndex_ShouldReturnRow_GivenValidIndex()
        {
            // Arrange
            var tableData = new List<Dictionary<string, object>>
            {
                new() { { "Name", "Alice" }, { "Age", 30 } },
                new() { { "Name", "Bob" }, { "Age", 25 } },
                new() { { "Name", "Steve" }, { "Age", 30 } },
                new() { { "Name", "Matthew" }, { "Age", 42 } },
                new() { { "Name", "John" }, { "Age", 17 } },
            };

            // Act
            var dataTable = new DataTable(tableData);
            var row = dataTable.GetRowByIndex(2);

            // Assert
            row.Should().NotBeNull();
            row["Name"].Should().Be("Steve");
            row["Age"].Should().Be(30);
        }

        [Fact]
        public void GetRowByIndex_ShouldThrowException_GivenInvalidIndex()
        {
            // Arrange
            var tableData = new List<Dictionary<string, object>>
            {
                new() { { "Name", "Alice" }, { "Age", 30 } },
                new() { { "Name", "Bob" }, { "Age", 25 } },
            };

            // Act
            var dataTable = new DataTable(tableData);
            Action act = () => dataTable.GetRowByIndex(5);

            // Assert
            act.Should().Throw<IndexOutOfRangeException>();
        }

        [Fact]
        public void GetColumn_ShouldReturnColumnValues_GivenValidColumnName()
        {
            // Arrange
            var tableData = new List<Dictionary<string, object>>
            {
                new() { { "Name", "Alice" }, { "Age", 30 } },
                new() { { "Name", "Bob" }, { "Age", 25 } },
            };

            // Act
            var dataTable = new DataTable(tableData);
            var columnValues = dataTable.GetColumn("Name").ToList();

            // Assert
            columnValues.Should().NotBeNull();
            columnValues.Should().HaveCount(2);
            columnValues.Should().BeEquivalentTo(new List<object> { "Alice", "Bob" });
        }

        [Fact]
        public void GetColumn_ShouldThrowException_GivenInvalidColumnName()
        {
            // Arrange
            var tableData = new List<Dictionary<string, object>>
            {
                new() { { "Name", "Alice" }, { "Age", 30 } },
                new() { { "Name", "Bob" }, { "Age", 25 } },
            };

            // Act
            var dataTable = new DataTable(tableData);
            Action act = () => dataTable.GetColumn("Location");

            // Assert
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Extract_ShouldReturnNewDataTable_GivenValidColumnNames()
        {
            // Arrange
            var tableData = new List<Dictionary<string, object>>
            {
                new() { { "Name", "Alice" }, { "Age", 30 }, { "Location", "NY" } },
                new() { { "Name", "Bob" }, { "Age", 25 }, { "Location", "LA" } },
            };

            // Act
            var dataTable = new DataTable(tableData);
            var extractedTable = dataTable.Extract("Name", "Location");

            // Assert
            extractedTable.Should().NotBeNull();
            extractedTable.ColumnCount.Should().Be(2);
            extractedTable.RowCount.Should().Be(2);
            extractedTable.Columns.Should().BeEquivalentTo(new List<string> { "Name", "Location" });
        }

        [Fact]
        public void Extract_ShouldThrowException_GivenInvalidColumnNames()
        {
            // Arrange
            var tableData = new List<Dictionary<string, object>>
            {
                new() { { "Name", "Alice" }, { "Age", 30 }, { "Location", "NY" } },
                new() { { "Name", "Bob" }, { "Age", 25 }, { "Location", "LA" } },
            };

            // Act
            var dataTable = new DataTable(tableData);
            Action act = () => dataTable.Extract("Name", "InvalidColumn");

            // Assert
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Extract_ShouldReturnNewDataTable_GivenValidColumnIndexes()
        {
            // Arrange
            var tableData = new List<Dictionary<string, object>>
            {
                new() { { "Name", "Alice" }, { "Age", 30 }, { "Location", "NY" } },
                new() { { "Name", "Bob" }, { "Age", 25 }, { "Location", "LA" } },
            };

            // Act
            var dataTable = new DataTable(tableData);
            var extractedTable = dataTable.Extract(0, 2);

            // Assert
            extractedTable.Should().NotBeNull();
            extractedTable.ColumnCount.Should().Be(2);
            extractedTable.RowCount.Should().Be(2);
            extractedTable.Columns.Should().BeEquivalentTo(new List<string> { "Name", "Location" });
        }

        [Fact]
        public void Extract_ShouldThrowIndexOutOfRangeException_GivenInvalidColumnIndexes()
        {
            // Arrange
            var tableData = new List<Dictionary<string, object>>
            {
                new() { { "Name", "Alice" }, { "Age", 30 }, { "Location", "NY" } },
                new() { { "Name", "Bob" }, { "Age", 25 }, { "Location", "LA" } },
            };

            // Act
            var dataTable = new DataTable(tableData);
            Action act = () => dataTable.Extract(0, 5);

            // Assert
            act.Should().Throw<IndexOutOfRangeException>();
        }

        [Fact]
        public void TryGetColumn_ShouldReturnTrueAndColumnValues_GivenValidColumnName()
        {
            // Arrange
            var tableData = new List<Dictionary<string, object>>
            {
                new() { { "Name", "Alice" }, { "Age", 30 }, { "Location", "NY" } },
                new() { { "Name", "Bob" }, { "Age", 25 }, { "Location", "LA" } },
            };

            // Act
            var dataTable = new DataTable(tableData);
            var result = dataTable.TryGetColumn("Name", out var columnValues);

            // Assert
            result.Should().BeTrue();
            columnValues.Should().NotBeNull();
            columnValues.Should().HaveCount(2);
            columnValues.Should().BeEquivalentTo(new List<object> { "Alice", "Bob" });
        }

        [Fact]
        public void TryGetColumn_ShouldReturnFalseAndNull_GivenInvalidColumnName()
        {
            // Arrange
            var tableData = new List<Dictionary<string, object>>
            {
                new() { { "Name", "Alice" }, { "Age", 30 }, { "Location", "NY" } },
                new() { { "Name", "Bob" }, { "Age", 25 }, { "Location", "LA" } },
            };

            // Act
            var dataTable = new DataTable(tableData);
            var result = dataTable.TryGetColumn("Profession", out var columnValues);

            // Assert
            result.Should().BeFalse();
            columnValues.Should().BeNull();
        }
    }
}
