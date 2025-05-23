// <copyright file="DataTableExtensionsTests.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.Data.UnitTests.Extensions
{
    using FluentAssertions;
    using Nebula.Data.Extensions;
    using Nebula.Data.Structures;

    public class DataTableExtensionsTests
    {
        private readonly DataTable _dataTable;

        public DataTableExtensionsTests()
        {
            _dataTable = new DataTable(new List<Dictionary<string, object>>
            {
                new() { { "feature1", 0.5 }, { "feature2", 1.2 }, { "label", 1 } },
                new() { { "feature1", 0.3 }, { "feature2", 1.6 }, { "label", 0 } },
            });
        }

        [Fact]
        public void ToVectorMatrix_ShouldConvertFeatureColumnsToMatrix()
        {
            // Act
            var result = _dataTable.ToVectorMatrix("feature1", "feature2");

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(new[]
            {
                new[] { 0.5, 1.2 },
                new[] { 0.3, 1.6 },
            });
        }

        [Fact]
        public void ToLabelVector_ShouldCovertColumnToTArray()
        {
            // Act
            var result = _dataTable.ToLabelVector<int>("label");

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<int[]>();
            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(new[] { 1, 0 });
        }
    }
}
