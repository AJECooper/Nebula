// <copyright file="VectorOperationsTests.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.NICE.UnitTests.VectorOps
{
    using FluentAssertions;
    using Nebula.NICE.MathOps;

    public class VectorOperationsTests
    {
        public VectorOperationsTests()
        {
        }

        [Fact]
        public void DotProduct_ShouldReturnCorrectResult_WhenVectorsAreEqualLength()
        {
            // Arrange
            double[] vector1 = { 1.0, 2.0, 3.0 };
            double[] vector2 = { 4.0, 5.0, 6.0 };

            // Act
            var result = VectorOperations.DotProduct(vector1, vector2);

            // Assert
            result.Should().Be(32.0);
        }

        [Fact]
        public void DotProduct_ShouldThrowArgumentException_GivenVectorsAreDifferentLengths()
        {
            // Arrange
            double[] vector1 = { 1.0, 2.0, 3.0 };
            double[] vector2 = { 4.0, 5.0 };

            // Act
            Action act = () => VectorOperations.DotProduct(vector1, vector2);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Vectors must be of the same length.");
        }
    }
}
