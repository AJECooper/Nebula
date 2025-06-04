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

        [Fact]
        public void AppendEncodedColumn_ShouldThrowException_GivenCategoricalDataIsNotEqualFeaturesLength()
        {
            // Arrange
            double[][] features = new double[][]
            {
                new double[] { 1.0, 2.0 },
                new double[] { 3.0, 4.0 }
            };
            string[] categories = { "Test1", "Test2", "Test3" };

            // Act
            Action act = () => OneHotEncoder.AppendEncodedColumn(features, categories);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("New entry not in distinct list appears.");
        }

        [Fact]
        public void AppendEncodedColumn_AppendsOneHotColumns_Correctly()
        {
            // Arrange
            double[][] features = new double[][]
            {
            new[] { 1.0, 2.0 },
            new[] { 3.0, 4.0 }
            };
            
            string[] categoricalData = { "Dog", "Cat" };

            // Act
            double[][] combined = OneHotEncoder.AppendEncodedColumn(features, categoricalData);

            // Assert
            combined.Length.Should().Be(2);
            combined[0].Length.Should().Be(4);
            combined[1].Length.Should().Be(4);

            combined[0][0].Should().Be(1.0);
            combined[0][1].Should().Be(2.0);
            combined[1][0].Should().Be(3.0);
            combined[1][1].Should().Be(4.0);

            combined[0][2].Should().Be(1.0);
            combined[0][3].Should().Be(0.0);
            
            combined[1][2].Should().Be(0.0);
            combined[1][3].Should().Be(1.0);
        }
    }
}
