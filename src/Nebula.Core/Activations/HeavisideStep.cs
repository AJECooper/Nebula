// <copyright file="HeavisideStepActivation.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.Core.Activations
{
    /// <summary>
    /// The heaviside step activation function. Returns 0 for inputs less than threshold, and 1 for inputs greater than or equal to threshold.
    /// Default threshold is 0.5.
    /// </summary>
    public class HeavisideStep : IActivation
    {
        private readonly double _threshold;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeavisideStep"/> class.
        /// </summary>
        /// <param name="threshold">The value that determines if the perceptron should "fire".</param>
        public HeavisideStep(double threshold = 0.5)
        {
            _threshold = threshold;
        }

        /// <inheritdoc />
        public double Activate(double input) => input >= _threshold ? 1 : 0;

        /// <inheritdoc />
        public double Derivative(double input) => 0.0;
    }
}
