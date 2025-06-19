// <copyright file="DistanceMetricsTests.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.NICE.UnitTests.DistanceOps
{
    using System;
    using FluentAssertions;
    using Nebula.NICE.MathOps;
    using Xunit;

    public class DistanceMetricsTests
    {
        [Fact]
        public void Euclidean_ShouldReturnCorrectResult_WhenVectorsAreEqualLength()
        {
            // Arrange
            double[] vector1 = { 1.0, 2.0, 3.0 };
            double[] vector2 = { 4.0, 5.0, 6.0 };

            // Act
            var result = DistanceMetrics.Euclidean(vector1, vector2);

            // Assert
            result.Should().BeApproximately(5.196, 0.001);
        }

        [Fact]
        public void Euclidean_ShouldThrowArgumentException_GivenVectorsAreDifferentLengths()
        {
            // Arrange
            double[] vector1 = { 1.0, 2.0, 3.0 };
            double[] vector2 = { 4.0, 5.0 };

            // Act
            Action act = () => DistanceMetrics.Euclidean(vector1, vector2);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Vectors must be of the same length.");
        }

        [Fact]
        public void Manhattan_ShouldReturnCorrectResult_WhenVectorsAreEqualLength()
        {
            // Arrange
            double[] vector1 = { 1.0, 2.0, 3.0 };
            double[] vector2 = { 4.0, 5.0, 6.0 };

            // Act
            var result = DistanceMetrics.Manhattan(vector1, vector2);

            // Assert
            result.Should().Be(9.0);
        }

        [Fact]
        public void Manhattan_ShouldThrowArgumentException_GivenVectorsAreDifferentLengths()
        {
            // Arrange
            double[] vector1 = { 1.0, 2.0, 3.0 };
            double[] vector2 = { 4.0, 5.0 };

            // Act
            Action act = () => DistanceMetrics.Manhattan(vector1, vector2);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Vectors must be of the same length.");
        }

        [Fact]
        public void Chebyshev_ShouldReturnCorrectResult_WhenVectorsAreEqualLength()
        {
            // Arrange
            double[] vector1 = { 1.0, 2.0, 3.0 };
            double[] vector2 = { 4.0, 5.0, 6.0 };

            // Act
            var result = DistanceMetrics.Chebyshev(vector1, vector2);

            // Assert
            result.Should().Be(3.0);
        }

        [Fact]
        public void Chebyshev_ShouldThrowArgumentException_GivenVectorsAreDifferentLengths()
        {
            // Arrange
            double[] vector1 = { 1.0, 2.0, 3.0 };
            double[] vector2 = { 4.0, 5.0 };

            // Act
            Action act = () => DistanceMetrics.Chebyshev(vector1, vector2);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Vectors must be of the same length.");
        }
    }
}
