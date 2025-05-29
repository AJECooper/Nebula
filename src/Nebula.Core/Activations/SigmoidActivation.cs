// <copyright file="SigmoidActivation.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.Core.Activations
{
    /// <summary>
    /// Represents the logistic sigmoid activation function.
    /// Sigmoid activation function squashes the input value and returns a value between 0 and 1.
    /// </summary>
    public class SigmoidActivation : IActivation
    {
        /// <summary>
        /// Applies the sigmoid activation function to the input.
        /// f(x) = 1 / (1 + exp(-x)).
        /// </summary>
        /// <param name="input">The input value.</param>
        /// <returns>The activated output between 0 and 1.</returns>
        public double Activate(double input)
        {
            return 1.0 / (1.0 + Math.Exp(-input));
        }

        /// <summary>
        /// Computes the derivative: f′(x) = f(x) * (1 – f(x)).
        /// </summary>
        /// <param name="input">The input value.</param>
        /// <returns>The slope of the sigmoid at input.</returns>
        public double Derivative(double input)
        {
            var activatedValue = Activate(input);
            return activatedValue * (1.0 - activatedValue);
        }
    }
}
