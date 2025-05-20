// <copyright file="CsvReaderTests.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.Data.UnitTests.DataReaders
{
    using System;
    using System.IO;
    using FluentAssertions;
    using Nebula.Data.IO;
    using Xunit;

    public class CsvReaderTests
    {
        [Fact]
        public void FromCsv_ShouldReturnDataTable_WhenCsvIsValid()
        {
            // Arrange
            var csvContent = "Name,Age,Country\nAlice,30,USA\nBob,25,Canada";
            var filePath = "valid.csv";
            File.WriteAllText(filePath, csvContent);

            // Act
            var result = CsvReader.FromCsv(filePath);

            // Assert
            result.Should().NotBeNull();
            result.RowCount.Should().Be(2);
            result.GetRowByIndex(0)["Name"].Should().Be("Alice");
            result.GetRowByIndex(0)["Age"].Should().Be("30");
            result.GetRowByIndex(0)["Country"].Should().Be("USA");
            result.GetRowByIndex(1)["Name"].Should().Be("Bob");
            result.GetRowByIndex(1)["Age"].Should().Be("25");
            result.GetRowByIndex(1)["Country"].Should().Be("Canada");

            // Cleanup
            File.Delete(filePath);
        }

        [Fact]
        public void FromCsv_ShouldThrowFileNotFoundException_WhenFileDoesNotExist()
        {
            // Arrange
            var filePath = "nonexistent.csv";

            // Act
            Action act = () => CsvReader.FromCsv(filePath);

            // Assert
            act.Should().Throw<FileNotFoundException>()
                .WithMessage($"The file '{filePath}' does not exist.");
        }

        [Fact]
        public void FromCsv_ShouldThrowInvalidOperationException_WhenFileIsEmpty()
        {
            // Arrange
            var filePath = "empty.csv";
            File.WriteAllText(filePath, string.Empty);

            // Act
            Action act = () => CsvReader.FromCsv(filePath);

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("The file is empty.");

            // Cleanup
            File.Delete(filePath);
        }

        [Fact]
        public void FromCsv_ShouldThrowInvalidOperationException_WhenFileHasOnlyHeaders()
        {
            // Arrange
            var filePath = "headersOnly.csv";
            File.WriteAllText(filePath, "Name,Age,Country");

            // Act
            Action act = () => CsvReader.FromCsv(filePath);

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("The file must contain headers and at least one row of data.");

            // Cleanup
            File.Delete(filePath);
        }

        [Fact]
        public void FromCsv_ShouldThrowFormatException_WhenRowHasMismatchedColumns()
        {
            // Arrange
            var csvContent = "Name,Age,Country\nAlice,30\nBob,25,Canada";
            var filePath = "mismatched.csv";
            File.WriteAllText(filePath, csvContent);

            // Act
            Action act = () => CsvReader.FromCsv(filePath);

            // Assert
            act.Should().Throw<FormatException>()
                .WithMessage("Row 2 has 2 values but expected 3.");

            // Cleanup
            File.Delete(filePath);
        }
    }
}
