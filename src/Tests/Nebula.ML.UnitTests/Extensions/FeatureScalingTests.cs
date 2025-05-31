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
        public void ComputeMinMax_ShouldThrowArgumentNullException_WhenDataIsNull()
        {
            // Arrange
            double[][] data = null;

            // Act
            Action act = () => FeatureScaling.ComputeMinMax(data);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ComputeMinMax_ShouldThrowArgumentException_WhenRowsHaveDifferentFeatureCounts()
        {
            // Arrange
            double[][] data = [[1.0, 2.0], [3.0]];

            // Act
            Action act = () => FeatureScaling.ComputeMinMax(data);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("All rows in the data must have the same number of features.");
        }

        [Fact]
        public void ComputeMinMax_ShouldReturnConstantValue_GivenAllFeatureValuesEqual()
        {
            // Arrange
            double[][] data = [[5.0, 5.0], [5.0, 5.0]];
            double[] expectedMins = new double[] { 5.0, 5.0 };
            double[] expectedMaxs = new double[] { 5.0, 5.0 };

            // Act
            var (mins, maxs) = FeatureScaling.ComputeMinMax(data);

            // Assert
            mins.Should().BeEquivalentTo(expectedMins);
            maxs.Should().BeEquivalentTo(expectedMaxs);
        }

        [Fact]
        public void ComputeMinMax_ShouldReturnMinsAndMaxs_GivenMultipleFeatures()
        {
            // Arrange
            double[][] data = new[]
            {
                new double[] { 1.0, 4.0, 7.0 },
                new double[] { 2.0, 5.0, 8.0 },
                new double[] { 3.0, 6.0, 9.0 },
            };

            double[] expectedMins = new double[] { 1.0, 4.0, 7.0 };
            double[] expectedMaxs = new double[] { 3.0, 6.0, 9.0 };

            // Act
            var (mins, maxs) = FeatureScaling.ComputeMinMax(data);

            // Assert
            mins.Should().BeEquivalentTo(expectedMins);
            maxs.Should().BeEquivalentTo(expectedMaxs);
        }

        [Fact]
        public void ApplyMinMax_ShouldThrowArgumentNullException_WhenDataIsNull()
        {
            // Arrange
            double[][] data = null;
            double[] mins = [0.0, 0.0];
            double[] maxs = [1.0, 1.0];

            // Act
            Action act = () => FeatureScaling.ApplyMinMax(data, mins, maxs);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ApplyMinMax_ShouldThrowArgumentNullException_WhenMinsOrMaxsIsNull()
        {
            // Arrange
            double[][] data = [[1.0, 2.0], [3.0, 4.0]];
            double[] mins = null;
            double[] maxs = [1.0, 1.0];

            // Act
            Action act = () => FeatureScaling.ApplyMinMax(data, mins, maxs);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ApplyMinMax_ShouldThrowArgumentException_WhenMinsAndMaxsLengthMismatch()
        {
            // Arrange
            double[][] data = [[1.0, 2.0], [3.0, 4.0]];
            double[] mins = [0.0];
            double[] maxs = [1.0, 1.0];

            // Act
            Action act = () => FeatureScaling.ApplyMinMax(data, mins, maxs);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Mins and max must have the same length as the number of features in the data.");
        }

        [Fact]
        public void ApplyMinMax_ShouldReturnNormalizedData_GivenValidInput()
        {
            // Arrange
            double[][] data = [[1.0, 2.0], [3.0, 4.0], [5.0, 6.0]];
            double[] mins = [1.0, 2.0];
            double[] maxs = [5.0, 6.0];
            double[][] expectedNormalizedVectors = [[0.0, 0.0], [0.5, 0.5], [1.0, 1.0]];

            // Act
            var result = FeatureScaling.ApplyMinMax(data, mins, maxs);

            // Assert
            result.Should().BeEquivalentTo(expectedNormalizedVectors);
        }
    }
}
