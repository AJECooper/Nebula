// <copyright file="IdentityActivation.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.Core.Activations
{
    /// <summary>
    /// An activation function that returns the input value unchanged.
    /// </summary>
    public class IdentityActivation : IActivation
    {
        /// <summary>
        /// The identity activation function simply returns the input value.
        /// </summary>
        /// <param name="input">The input value.</param>
        /// <returns>The input value unchanged f(x) = x.</returns>
        public double Activate(double input) => input;

        /// <summary>
        /// Its derivative is 1 everywhere: f′(x) = 1.
        /// </summary>
        /// <param name="input">The input value.</param>
        /// <returns>1.</returns>
        public double Derivative(double input) => 1.0;
    }
}
