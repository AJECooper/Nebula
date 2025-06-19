// <copyright file="DistanceMetrics.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.NICE.MathOps
{
    /// <summary>
    /// Provides static methods for calculating various distance metrics between two vectors.
    /// </summary>
    public static class DistanceMetrics
    {
        /// <summary>
        /// Uses the Euclidean distance metric to calculate the distance between two vectors.
        /// </summary>
        /// <param name="a">The first input vector as a read-only span of double-precision values.</param>
        /// <param name="b">The second input vector as a read-only span of double-precision values.</param>
        /// <returns>The distance between vector a and vector b.</returns>
        public static double Euclidean(ReadOnlySpan<double> a, ReadOnlySpan<double> b)
        {
            if (a.Length != b.Length)
            {
                throw new ArgumentException("Vectors must be of the same length.");
            }

            double sum = 0.0;

            for (int i = 0; i < a.Length; i++)
            {
                double diff = a[i] - b[i];
                sum += diff * diff;
            }

            return Math.Sqrt(sum);
        }

        /// <summary>
        /// Uses the Manhattan distance metric to calculate the distance between two vectors.
        /// </summary>
        /// <param name="a">The first input vector as a read-only span of double-precision values.</param>
        /// <param name="b">The second input vector as a read-only span of double-precision values.</param>
        /// <returns>The distance between vector a and vector b.</returns>
        public static double Manhattan(ReadOnlySpan<double> a, ReadOnlySpan<double> b)
        {
            if (a.Length != b.Length)
            {
                throw new ArgumentException("Vectors must be of the same length.");
            }

            double sum = 0.0;

            for (int i = 0; i < a.Length; i++)
            {
                sum += Math.Abs(a[i] - b[i]);
            }

            return sum;
        }

        /// <summary>
        /// Uses the Chebyshev distance metric to calculate the distance between two vectors.
        /// </summary>
        /// <param name="a">The first input vector as a read-only span of double-precision values.</param>
        /// <param name="b">The second input vector as a read-only span of double-precision values.</param>
        /// <returns>The distance between vector a and vector b.</returns>
        public static double Chebyshev(ReadOnlySpan<double> a, ReadOnlySpan<double> b)
        {
            if (a.Length != b.Length)
            {
                throw new ArgumentException("Vectors must be of the same length.");
            }

            double max = 0.0;

            for (int i = 0; i < a.Length; i++)
            {
                double diff = Math.Abs(a[i] - b[i]);

                if (diff > max)
                {
                    max = diff;
                }
            }

            return max;
        }
    }
}
