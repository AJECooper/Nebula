// <copyright file="IActivationFunction.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.Core.Activations
{
    /// <summary>
    /// Defines the interface for activation functions.
    /// </summary>
    public interface IActivation
    {
        /// <summary>
        /// Determines the output of the activation function for a given input.
        /// </summary>
        /// <param name="input">The pre-calcuated input.</param>
        /// <returns>The activated output.</returns>
        double Activate(double input);

        /// <summary>
        /// Computes the derivative of the activation function for a given input.
        /// </summary>
        /// <param name="input">The pre-activation input.</param>
        /// <returns>The derivative at <paramref name="input"/>.</returns>
        double Derivative(double input);
    }
}
