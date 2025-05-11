// <copyright file="DatatableDataTests.cs" company="Nebula">
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
    }
}
