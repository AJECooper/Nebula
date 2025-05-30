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

        private double[] _coefficients;
        private double _intercept;
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
        /// <param name="inputs">An array for feature vectors.</param>
        /// <param name="targets">The desired output for each feature.</param>
        public void Fit(double[][] inputs, double[] targets)
        {
            if (inputs == null || targets == null)
            {
                throw new ArgumentNullException(nameof(inputs));
            }

            if (inputs.Length == 0 || inputs.Length != targets.Length)
            {
                throw new ArgumentException("Features and labels must not be empty and of the same length.");
            }

            if (_coefficients == null)
            {
                InitialiseWeightsAndBias(inputs[0].Length);
            }
            else if (_coefficients.Length != inputs[0].Length)
            {
                throw new InvalidOperationException("Cannot fit: feature‐vector length changed since initialization.");
            }

            for (int epoch = 0; epoch < _epochs; epoch++)
            {
                for (int sample = 0; sample < inputs.Length; sample++)
                {
                    var z = VectorOperations.DotProduct(inputs[sample], _coefficients) + _intercept;

                    var prediction = _activation.Activate(z);

                    var error = targets[sample] - prediction;

                    var delta = error * _activation.Derivative(z);

                    for (int i = 0; i < _coefficients.Length; i++)
                    {
                        _coefficients[i] += _learningRate * delta * inputs[sample][i];
                    }

                    _intercept += _learningRate * delta;
                }
            }
        }

        /// <summary>
        /// Predicts the target value for the given feature vector using the trained linear regression model.
        /// </summary>
        /// <param name="input">An array of input feature values.</param>
        /// <returns>The estimated continuous target value.</returns>
        public double Predict(double[] input)
        {
            return _activation.Activate(VectorOperations.DotProduct(input, _coefficients) + _intercept);
        }

        private void InitialiseWeightsAndBias(int featureSize)
        {
            _coefficients = new double[featureSize];
            _intercept = 0.0;
        }
    }
}
