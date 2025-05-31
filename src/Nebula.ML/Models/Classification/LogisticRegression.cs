// <copyright file="LogisticRegression.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.ML.Models.Classification
{
    using Nebula.Core.Activations;
    using Nebula.NICE.MathOps;

    /// <summary>
    /// A logistic regression model for binary classification tasks.
    /// </summary>
    public class LogisticRegression
    {
        private readonly IActivation _activation;
        private double[] _weights;
        private double _bias;
        private readonly int _epochs;
        private readonly double _learningRate;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogisticRegression"/> class with the sigmoid default activation function.
        /// </summary>
        /// <param name="activation">The activation function to use; default sigmoid.</param>
        /// <param name="epochs">The number of full iterations of the data.</param>
        /// <param name="learningRate">Defines how quick the model learns.</param>
        public LogisticRegression(IActivation? activation, int epochs, double learningRate = 0.01)
        {
            _activation = activation ?? new SigmoidActivation();
            _epochs = epochs;
            _learningRate = learningRate;
        }

        /// <summary>
        /// Trains the model to predict the labels from the features.
        /// </summary>
        /// <param name="features">An two dimensional array of indexes and feature vectors.</param>
        /// <param name="labels">The desired output for each feature.</param>
        /// <exception cref="ArgumentNullException">Thrown when features or labels are null.</exception>
        /// <exception cref="ArgumentException">Thrown when an array of features are empty or when there's a mismatch between features and weights.</exception>
        /// <exception cref="InvalidOperationException">Thrown if length of weights don't match length of feature vectors.</exception>
        public void Fit(double[][] features, int[] labels)
        {
            if (features == null)
            {
                throw new ArgumentNullException(nameof(features));
            }

            if (labels == null)
            {
                throw new ArgumentNullException(nameof(labels));
            }

            if (features.Length == 0 || features.Length != labels.Length)
            {
                throw new ArgumentException("Features and labels must not be empty and of the same length.");
            }

            if (_weights == null)
            {
                InitialiseWeightsAndBias(features[0].Length);
            }

            if (_weights.Length != features[0].Length)
            {
                throw new InvalidOperationException("Cannot fit: feature‐vector length changed since initialization.");
            }

            var rnd = new Random();
            int nSamples = features.Length;
            int[] indices = new int[nSamples];
            for (int epoch = 0; epoch < _epochs; epoch++)
            {
                for (int i = 0; i < nSamples; i++)
                {
                    indices[i] = i;
                }

                // Fisher–Yates shuffle
                for (int i = nSamples - 1; i > 0; i--)
                {
                    int j = rnd.Next(i + 1);
                    (indices[i], indices[j]) = (indices[j], indices[i]);
                }

                foreach (int idx in indices)
                {
                    var (probability, _) = Predict(features[idx]);

                    // Cross-entropy error signal = (p - y)
                    double error = probability - labels[idx];

                    for (int j = 0; j < _weights.Length; j++)
                    {
                        _weights[j] -= _learningRate * error * features[idx][j];
                    }

                    _bias -= _learningRate * error;
                }
            }
        }

        /// <summary>
        /// Predicts the output for a given feature vector using the trained model.
        /// </summary>
        /// <param name="features">The feature vector.</param>
        /// <returns>A tuple containing the actual predicted value and the class.</returns>
        public (double, int) Predict(double[] features)
        {
            var predictedValue = _activation.Activate(VectorOperations.DotProduct(features, _weights) + _bias);

            if (predictedValue >= 0.5)
            {
                return (predictedValue, 1);
            }
            else
            {
                return (predictedValue, 0);
            }
        }

        private void InitialiseWeightsAndBias(int featureSize)
        {
            _weights = new double[featureSize];
            _bias = 0.0;
        }
    }
}
