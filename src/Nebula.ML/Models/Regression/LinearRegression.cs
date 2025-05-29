// <copyright file="LinearRegression.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.ML.Models.Regression
{
    using Nebula.Core.Activations;
    using Nebula.NICE.MathOps;

    public class LinearRegression
    {
        private readonly IActivation _activation;

        private double[] _weights;
        private double _bias;
        private int _epochs;
        private double _learningRate;

        public LinearRegression(IActivation? activation, int epochs, double learningRate = 0.01)
        {
            _activation = activation ?? new IdentityActivation();
            _epochs = epochs;
            _learningRate = learningRate;
        }

        /// <summary>
        /// Trains the model to predict the labels from the features.
        /// </summary>
        /// <param name="features">An array for feature vectors.</param>
        /// <param name="labels">The desired output for each feature.</param>
        public void Fit(double[][] features, double[] labels)
        {
            if (features == null || labels == null)
            {
                throw new ArgumentNullException(nameof(features));
            }

            if (features.Length == 0 || features.Length != labels.Length)
            {
                throw new ArgumentException("Features and labels must not be empty and of the same length.");
            }

            if (_weights == null)
            {
                InitialiseWeightsAndBias(features[0].Length);
            }
            else if (_weights.Length != features[0].Length)
            {
                throw new InvalidOperationException("Cannot fit: feature‐vector length changed since initialization.");
            }

            for (int epoch = 0; epoch < _epochs; epoch++)
            {
                for (int sample = 0; sample < features.Length; sample++)
                {
                    var z = VectorOperations.DotProduct(features[sample], _weights) + _bias;

                    var prediction = _activation.Activate(z);

                    var error = labels[sample] - prediction;

                    var delta = error * _activation.Derivative(z);

                    for (int i = 0; i < _weights.Length; i++)
                    {
                        _weights[i] += _learningRate * delta * features[sample][i];
                    }

                    _bias += _learningRate * delta;
                }
            }
        }

        /// <summary>
        /// Predicts the output for a given feature vector using the trained model.
        /// </summary>
        /// <param name="features">The feature vector.</param>
        /// <returns>The estimated output for the feature vector (0 or 1).</returns>
        public double Predict(double[] features)
        {
            return _activation.Activate(VectorOperations.DotProduct(features, _weights) + _bias);
        }

        private void InitialiseWeightsAndBias(int featureSize)
        {
            _weights = new double[featureSize];
            _bias = 0.0;
        }
    }
}
