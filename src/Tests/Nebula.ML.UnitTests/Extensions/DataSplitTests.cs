// <copyright file="DataSplitTests.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.ML.UnitTests.Extensions
{
    using FluentAssertions;
    using Nebula.ML.Preprocessing;

    public class DataSplitTests
    {
        [Fact]
        public void Split_ShouldThrowArgumentNullException_WhenFeaturesIsNull()
        {
            // Arrange
            double[][] features = null;
            int[] labels = [1, 1, 0];

            // Act
            Action act = () => DataSplit.Split(features, labels);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Split_ShouldThrowArgumentNullException_WhenLabelsIsNull()
        {
            // Arrange
            double[][] features = [[1.0, 2.0], [3.0, 4.0], [5.0, 6.0]];
            int[] labels = null;

            // Act
            Action act = () => DataSplit.Split(features, labels);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Split_ShouldThrowArgumentException_WhenFeaturesAndLabelsLengthMismatch()
        {
            // Arrange
            double[][] features = [[1.0, 2.0],[3.0, 4.0]];
            int[] labels = [1, 0, 1];

            // Act
            Action act = () => DataSplit.Split(features, labels);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Features and labels must have the same number of rows.");
        }

        [Fact]
        public void Split_ShouldThrowArgumentException_WhenSplitRatioIsOutOfRange()
        {
            // Arrange
            double[][] features = [[1.0, 2.0],[3.0, 4.0],[5.0, 6.0]];
            int[] labels = [1, 0, 1];
            double splitRatio = 1.5;

            // Act
            Action act = () => DataSplit.Split(features, labels, splitRatio);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Split ratio must be between 0 and 1.");
        }

        [Fact]
        public void Split_ShouldReturnCorrectSplit_WhenValidInputsProvided()
        {
            // Arrange
            double[][] features = [[1.0, 2.0], [3.0, 4.0], [5.0, 6.0], [7.0, 8.0]];
            int[] labels = [1, 0, 1, 0];
            double splitRatio = 0.75;

            // Act
            var (trainX, trainY, testX, testY) = DataSplit.Split(features, labels, splitRatio);

            // Assert
            trainX.Should().HaveCount(3);
            trainY.Should().HaveCount(3);
            testX.Should().HaveCount(1);
            testY.Should().HaveCount(1);

            trainX[0].Should().BeEquivalentTo([1.0, 2.0]);
            trainY[0].Should().Be(1);
        }

        [Fact]
        public void Split_ShouldShuffleData_WhenShuffleIsTrue()
        {
            // Arrange
            double[][] features = [[1.0, 2.0], [3.0, 4.0], [5.0, 6.0], [7.0, 8.0]];
            int[] labels = [1, 0, 1, 0];
            double splitRatio = 0.5;

            // Act
            var (trainX1, trainY1, testX1, testY1) = DataSplit.Split(features, labels, splitRatio, true);
            var (trainX2, trainY2, testX2, testY2) = DataSplit.Split(features, labels, splitRatio, true);

            // Assert
            trainX1.Should().NotBeSameAs(trainX2);
            testX1.Should().NotBeSameAs(testX2);
        }
    }
}
