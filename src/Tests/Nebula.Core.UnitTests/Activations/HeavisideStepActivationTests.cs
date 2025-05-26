// <copyright file="HeavisideStepActivationTests.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.Core.UnitTests.Activations
{
    using FluentAssertions;
    using Nebula.Core.Activations;

    public class HeavisideStepActivationTests
    {
        [Theory]
        [InlineData(0.1, 0.5)]
        [InlineData(0.2, 0.5)]
        [InlineData(0.3, 0.5)]
        [InlineData(0.4, 0.5)]
        public void Activate_ShouldReturnZero_GivenInputLessThanThreshold(double input, double threshold)
        {
            // Arrange
            var activation = new HeavisideStepActivation(threshold);

            // Act
            double result = activation.Activate(input);

            // Assert
            result.Should().Be(0);
        }

        [Theory]
        [InlineData(0.6, 0.5)]
        [InlineData(0.7, 0.5)]
        [InlineData(0.8, 0.5)]
        [InlineData(0.9, 0.5)]
        public void Activate_ShouldReturnOne_GivenInputLessThanThreshold(double input, double threshold)
        {
            // Arrange
            var activation = new HeavisideStepActivation(threshold);

            // Act
            double result = activation.Activate(input);

            // Assert
            result.Should().Be(1);
        }
    }
}
