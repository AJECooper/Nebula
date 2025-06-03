// <copyright file="OneHotEncoderTests.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.ML.UnitTests.Extensions
{
    using FluentAssertions;
    using Nebula.ML.Preprocessing;

    public class OneHotEncoderTests
    {
        [Fact]
        public void Encode_ShouldReturnEmptyArray_GivenEmptyInput()
        {
            // Arrange
            string[] categories = Array.Empty<string>();

            // Act
            var result = OneHotEncoder.Encode(categories);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void Encode_ShouldTransformCategoricalDataToHotHot_GivenValidData()
        {
            // Arrange
            string[] categories = { "cat", "dog", "fish", "cat" };
            double[][] expected = new double[][]
            {
                new double[] { 1, 0, 0 },
                new double[] { 0, 1, 0 },
                new double[] { 0, 0, 1 },
                new double[] { 1, 0, 0 },
            };

            // Act
            var result = OneHotEncoder.Encode(categories);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }
    }
}
