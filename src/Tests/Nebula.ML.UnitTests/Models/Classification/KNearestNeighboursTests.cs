// <copyright file="KNearestNeighboursTests.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.ML.UnitTests.Models.Classification
{
    using FluentAssertions;
    using Nebula.ML.Models.Classification;

    public class KNearestNeighboursTests
    {
        private readonly KNearestNeighbours _model;

        public KNearestNeighboursTests()
        {
            _model = new KNearestNeighbours(1, KNearestNeighbours.DistanceMetric.Euclidean);
        }

        [Fact]
        public void Predict_ShouldReturnCorrectLabel_AfterTraining()
        {
            // Arrange
            double[][] features = {
                new[] { 0.0, 0.0 },
                new[] { 1.0, 0.0 },
                new[] { 0.0, 1.0 },
                new[] { 1.0, 1.0 }
            };
            string[] labels = { "A", "B", "B", "B" };
            _model.Fit(features, labels);

            // Act
            var prediction = _model.Predict(new double[] { 0.9, 0.9 });

            // Assert
            prediction.Should().Be("B");
        }

        [Theory]
        [InlineData(new double[] { 0.0, 0.0 }, "A")]
        [InlineData(new double[] { 1.0, 0.0 }, "B")]
        [InlineData(new double[] { 0.0, 1.0 }, "B")]
        [InlineData(new double[] { 1.0, 1.0 }, "B")]
        public void Predict_ShouldOutputCorrectClass_ForKnownInputs(double[] feature, string expectedLabel)
        {
            // Arrange
            double[][] features = {
                new[] { 0.0, 0.0 },
                new[] { 1.0, 0.0 },
                new[] { 0.0, 1.0 },
                new[] { 1.0, 1.0 }
            };
            string[] labels = { "A", "B", "B", "B" };
            _model.Fit(features, labels);

            // Act
            var prediction = _model.Predict(feature);

            // Assert
            prediction.Should().Be(expectedLabel);
        }
    }
}
