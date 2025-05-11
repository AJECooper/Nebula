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
            var table = new List<Dictionary<string, object>>
            {
                new() { { "Name", "Alice" }, { "Age", 30 } },
                new() { { "Name", "Bob" }, { "Age", 25 } },
            };

            Action act = () => new DataTable(table);

            act.Should().NotThrow();
        }

        [Fact]
        public void Constructor_ShouldThrowException_GivenNullData()
        {
            var table = new List<Dictionary<string, object>>
            {
            };

            Action act = () => new DataTable(table);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Constructor_ShouldThrowException_GivenInconsistentKeys()
        {
            var table = new List<Dictionary<string, object>>
            {
                new() { { "Name", "Alice" }, { "Age", 30 } },
                new() { { "Name", "Bob" }, { "Location", "London" } },
            };

            Action act = () => new DataTable(table);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Constructor_ShouldNotThrowException_GivenNullValues()
        {
            var table = new List<Dictionary<string, object>>
            {
                new() { { "Name", "Alice" }, { "Age", 30 } },
                new() { { "Name", "Bob" }, { "Age", null } },
            };

            Action act = () => new DataTable(table);

            act.Should().NotThrow();
        }
    }
}
