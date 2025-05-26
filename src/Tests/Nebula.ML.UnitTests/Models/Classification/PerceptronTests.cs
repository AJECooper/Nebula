// <copyright file="PerceptronTests.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.ML.UnitTests.Models.Classification
{
    using FluentAssertions;
    using Nebula.ML.Models.Classification;

    public class PerceptronTests
    {
        private readonly Perceptron _model;

        public PerceptronTests()
        {
            _model = new Perceptron(null, 10, 0.1);
        }

        [Fact]
        public void Fit_ShouldThrowException_GivenFeaturesAreNull()
        {
            // Arrange
            double[][] features = null;
            int[] labels = { 1, 0, 1, 0 };

            // Act
            Action act = () => _model.Fit(features, labels);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Fit_ShouldThrowException_GivenLabelsAreNull()
        {
            // Arrange
            double[][] features = new[] { new double[] { 1.0, 2.0 } };
            int[] labels = null;

            // Act
            Action act = () => _model.Fit(features, labels);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Fit_ShouldThrowException_GivenFeaturesAreEmpty()
        {
            // Arrange
            double[][] features = new double[0][];
            int[] labels = { 1, 0, 1, 0 };

            // Act
            Action act = () => _model.Fit(features, labels);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Features and labels must not be empty and of the same length.");
        }

        [Fact]
        public void Fit_ShouldThrowException_GivenLabelsAreEmpty()
        {
            // Arrange
            double[][] features = new[] { new double[] { 1.0, 2.0 } };
            int[] labels = new int[0];

            // Act
            Action act = () => _model.Fit(features, labels);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Features and labels must not be empty and of the same length.");
        }

        [Fact]
        public void Fit_ShouldThrowException_GivenFeaturesAndLabelsMismatchLength()
        {
            // Arrange
            double[][] features = new[] { new double[] { 1.0, 2.0 } };
            int[] labels = { 1, 2, 3 };

            // Act
            Action act = () => _model.Fit(features, labels);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Features and labels must not be empty and of the same length.");
        }

        [Theory]
        [InlineData(new double[] { 0.0, 0.0 }, 0)]
        [InlineData(new double[] { 1.0, 0.0 }, 1)]
        [InlineData(new double[] { 0.0, 1.0 }, 1)]
        [InlineData(new double[] { 1.0, 1.0 }, 1)]
        public void Predict_ShouldOutputCorrectClass_GivenSuccessfulTraining(double[] feature, int expectedLabek)
        {
            // Arrange
            double[][] features = {
                new[] { 0.0, 0.0 },
                new[] { 0.0, 1.0 },
                new[] { 1.0, 0.0 },
                new[] { 1.0, 1.0 },
            };

            int[] labels = { 0, 1, 1, 1 };

            _model.Fit(features, labels);

            // Act
            var prediction = _model.Predict(feature);

            // Assert
            prediction.Should().Be(expectedLabek);
        }
    }
}
