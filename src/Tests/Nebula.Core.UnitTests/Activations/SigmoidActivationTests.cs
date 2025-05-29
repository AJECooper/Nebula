// <copyright file="SigmoidActivationTests.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.Core.UnitTests.Activations
{
    using FluentAssertions;
    using Nebula.Core.Activations;

    public class SigmoidActivationTests
    {
        [Theory]
        [InlineData(-2)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]    
        public void Activate_ShouldReturnValueBetweenZeroAndOne(double input)
        {
            // Arrange
            var activation = new SigmoidActivation();

            // Act
            double result = activation.Activate(input);

            // Assert
            result.Should().BeInRange(0.0, 1.0);
        }

        [Theory]
        [InlineData(-2.0, 0.1049)]
        [InlineData(-1.0, 0.1966)]
        [InlineData(0.0, 0.2500)]
        [InlineData(1.0, 0.1966)]
        [InlineData(2.0, 0.1049)]
        public void Derivative_ShouldReturnCorrectSlope(double input, double expectedSlope)
        {
            // Arrange
            var activation = new SigmoidActivation();

            // Act
            double result = activation.Derivative(input);

            // Assert
            result.Should().BeApproximately(expectedSlope, 0.0001);
        }
    }
}
