// <copyright file="FeatureScalingTests.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.ML.UnitTests.Extensions
{
    using FluentAssertions;
    using Nebula.ML.Preprocessing;

    public class FeatureScalingTests
    {
        [Fact]
        public void MinMaxNormalization_ShouldThrowArgumentNullException_WhenDataIsNull()
        {
            // Arrange
            double[][] data = null;

            // Act
            Action act = () => FeatureScaling.MinMaxNormalization(data);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void MinMaxNormalization_ShouldThrowArgumentException_WhenRowsHaveDifferentFeatureCounts()
        {
            // Arrange
            double[][] data = [[1.0, 2.0], [3.0]];

            // Act
            Action act = () => FeatureScaling.MinMaxNormalization(data);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("All rows in the data must have the same number of features.");
        }

        [Fact]
        public void MinmaxNormalization_ShouldReturnZero_GivenMinMaxEqualValues()
        {
            // Arrange
            double[][] data = [[5.0, 5.0], [5.0, 5.0]];
            double[][] expectedNormalizedVectors = [[0.0, 0.0], [0.0, 0.0]];

            // Act
            var result = FeatureScaling.MinMaxNormalization(data);

            // Assert
            result.Should().BeEquivalentTo(expectedNormalizedVectors);
        }

        [Fact]
        public void MinMaxNormalization_ShouldReturnNormalizedData_GivenValidInput()
        {
            // Arrange
            double[][] data = [[1.0, 2.0], [3.0, 4.0], [5.0, 6.0]];
            double[][] expectedNormalizedVectors = [[0.0, 0.0], [0.5, 0.5], [1.0, 1.0]];

            // Act
            var result = FeatureScaling.MinMaxNormalization(data);

            // Assert
            result.Should().BeEquivalentTo(expectedNormalizedVectors);
        }
    }
}
