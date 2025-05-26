// <copyright file="Perceptron.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.ML.Models.Classification
{
    using Nebula.Core.Activations;
    using Nebula.NICE.MathOps;

    /// <summary>
    /// A simple binary-classifier.
    /// </summary>
    public class Perceptron
    {
        private readonly IActivationFunction _activation;

        private double[] _weights;
        private double _bias;
        private int _epochs;
        private double _learningRate;

        /// <summary>
        /// Initializes a new instance of the <see cref="Perceptron"/> class with a default activation function.
        /// </summary>
        /// <param name="activation">The activation function to use; default heaviside step function.</param>
        /// <param name="epochs">The number of full iterations of the data.</param>
        /// <param name="learningRate">Defines how quick the model learns.</param>
        public Perceptron(IActivationFunction? activation, int epochs, double learningRate = 0.01)
        {
            _activation = activation ?? new HeavisideStepActivation();
            _epochs = epochs;
            _learningRate = learningRate;
        }

        /// <summary>
        /// Trains the model to predict the labels from the features.
        /// </summary>
        /// <param name="features">An array for feature vectors.</param>
        /// <param name="labels">The desired output for each feature.</param>
        public void Fit(double[][] features, int[] labels)
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
                    var prediction = Predict(features[sample]);

                    var error = labels[sample] - prediction;

                    for (int i = 0; i < _weights.Length; i++)
                    {
                        _weights[i] += _learningRate * error * features[sample][i];
                    }

                    _bias += _learningRate * error;
                }
            }
        }

        /// <summary>
        /// Predicts the output for a given feature vector using the trained model.
        /// </summary>
        /// <param name="features">The feature vector.</param>
        /// <returns>The estimated output for the feature vector (0 or 1).</returns>
        public int Predict(double[] features)
        {
            return (int)_activation.Activate(VectorOperations.DotProduct(features, _weights) + _bias);
        }

        private void InitialiseWeightsAndBias(int featureSize)
        {
            _weights = new double[featureSize];
            _bias = 0.0;
        }
    }
}
