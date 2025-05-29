// <copyright file="LinearRegressionTests.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.ML.UnitTests.Models.Regression
{
    using FluentAssertions;
    using Nebula.ML.Models.Regression;

    public class LinearRegressionTests
    {
        private readonly LinearRegression _model;

        public LinearRegressionTests()
        {
            _model = new LinearRegression(null, 10, 0.01);
        }

        [Fact]
        public void Fit_ShouldThrowException_GivenFeaturesAreNull()
        {
            // Arrange
            double[][] features = null;
            double[] labels = { 1, 0, 1, 0 };

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
            double[] labels = null;

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
            double[] labels = { 1, 0, 1, 0 };

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
            double[] labels = new double[0];

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
            double[] labels = { 1, 2, 3 };

            // Act
            Action act = () => _model.Fit(features, labels);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Features and labels must not be empty and of the same length.");
        }

        [Fact]
        public void Fit_And_Predict_ShouldLearnSimpleLinearFunction()
        {
            // Arrange
            const double slope = 3.0;
            const double intercept = 2.0;
            const int sampleCount = 100;
            var rng = new Random(0);

            var features = Enumerable.Range(0, sampleCount)
                                     .Select(i => new[] { i / 10.0 })
                                     .ToArray();

            var labels = features
                .Select(f => (slope * f[0]) + intercept + ((rng.NextDouble() * 0.1) - 0.05))
                .ToArray();

            var model = new LinearRegression(null, epochs: 500, learningRate: 0.01);

            // Act
            model.Fit(features, labels);

            // Assert
            var testPoints = new[] { 0.0, 3.0, 6.0, 9.0 };
            foreach (var x in testPoints)
            {
                double actual = model.Predict(new[] { x });
                double expected = (slope * x) + intercept;

                actual.Should().BeApproximately(expected, 0.2);
            }
        }
    }
}
