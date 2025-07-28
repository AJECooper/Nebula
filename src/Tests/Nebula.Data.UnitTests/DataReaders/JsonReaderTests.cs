// <copyright file="JsonReaderTests.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.Data.UnitTests.DataReaders
{
    using FluentAssertions;
    using Nebula.Data.IO;

    public class JsonReaderTests
    {
        [Fact]
        public void FromJson_ShouldReturnDataTable_WhenJsonIsValid()
        {
            // Arrange
            var jsonContent = "[{\"Name\":\"Alice\",\"Age\":30,\"Country\":\"USA\"},{\"Name\":\"Bob\",\"Age\":25,\"Country\":\"Canada\"}]";
            var filePath = "valid.json";
            File.WriteAllText(filePath, jsonContent);

            // Act
            var result = JsonReader.FromJson(filePath);

            // Assert
            result.Should().NotBeNull();
            result.RowCount.Should().Be(2);
            result.GetRowByIndex(0)["Name"].Should().Be("Alice");
            result.GetRowByIndex(0)["Age"].Should().Be(30);
            result.GetRowByIndex(0)["Country"].Should().Be("USA");
            result.GetRowByIndex(1)["Name"].Should().Be("Bob");
            result.GetRowByIndex(1)["Age"].Should().Be(25);
            result.GetRowByIndex(1)["Country"].Should().Be("Canada");

            // Cleanup
            File.Delete(filePath);
        }

        [Fact]
        public void FromJson_ShouldThrowFileNotFoundException_WhenFileDoesNotExist()
        {
            // Arrange
            var filePath = "nonexistent.json";

            // Act
            Action act = () => JsonReader.FromJson(filePath);

            // Assert
            act.Should().Throw<FileNotFoundException>()
                .WithMessage($"The file '{filePath}' not found in project.");
        }

        [Fact]
        public void FromJson_ShouldThrowInvalidOperationException_WhenFileIsEmpty()
        {
            // Arrange
            var filePath = "empty.json";
            File.WriteAllText(filePath, string.Empty);

            // Act
            Action act = () => JsonReader.FromJson(filePath);

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("File is empty. Unable to parse data.");

            // Cleanup
            File.Delete(filePath);
        }

        [Fact]
        public void FromJson_ShouldThrowInvalidOperationException_WhenJsonIsNotArray()
        {
            // Arrange
            var filePath = "notArray.json";
            File.WriteAllText(filePath, "{\"Name\":\"Alice\",\"Age\":30}");

            // Act
            Action act = () => JsonReader.FromJson(filePath);

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Data parsing failed. Json file is not in an appropriate format.");

            // Cleanup
            File.Delete(filePath);
        }

        [Fact]
        public void FromJson_ShouldThrowInvalidOperationException_WhenJsonArrayIsEmpty()
        {
            // Arrange
            var filePath = "emptyArray.json";
            File.WriteAllText(filePath, "[]");

            // Act
            Action act = () => JsonReader.FromJson(filePath);

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Data extraction failed. Data is null when parsing the json file.");

            // Cleanup
            File.Delete(filePath);
        }
    }
}
