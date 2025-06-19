// <copyright file="VectorOperations.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.NICE.MathOps
{
    /// <summary>
    /// Provides basic vector operations.
    /// </summary>
    public static class VectorOperations
    {
        /// <summary>
        /// Computes the dot product (scalar product) of two vectors of equal length.
        /// </summary>
        /// <param name="vector1">The first input vector as a read-only span of double-precision values.</param>
        /// <param name="vector2">The second input vector as a read-only span of double-precision values.</param>
        /// <returns>The sum of the element-wise products (vector1[i] * vector2[i]).</returns>
        /// <exception cref="ArgumentException">Thrown when vector1 and vector2 are different lengths.</exception>
        public static double DotProduct(ReadOnlySpan<double> vector1, ReadOnlySpan<double> vector2)
        {
            if (vector1.Length != vector2.Length)
            {
                throw new ArgumentException("Vectors must be of the same length.");
            }

            double result = 0.0;

            for (int i = 0; i < vector1.Length; i++)
            {
                result += vector1[i] * vector2[i];
            }

            return result;
        }
    }
}
