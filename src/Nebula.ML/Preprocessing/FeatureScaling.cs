// <copyright file="FeatureScaling.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.ML.Preprocessing
{
    /// <summary>
    /// Utility class for feature scaling techniques such as normalization and standardization.
    /// </summary>
    public static class FeatureScaling
    {
        /// <summary>
        /// Scales each feature in the input dataset to the [0, 1] range using min–max normalization.
        /// </summary>
        /// <param name="data">
        /// A two‐dimensional array where each sub‐array represents a sample and each element a feature value.
        /// </param>
        /// <returns>
        /// A new two‐dimensional array containing the normalized feature values in the range [0, 1].
        /// </returns>
        public static double[][] MinMaxNormalization(double[][] data)
        {
            if (data == null || data.Length == 0)
            {
                throw new ArgumentNullException(nameof(data));
            }

            int featureCount = data[0].Length;

            if (data.Any(row => row.Length != featureCount))
            {
                throw new ArgumentException("All rows in the data must have the same number of features.");
            }

            var mins = Enumerable.Range(0, featureCount)
                .Select(x => data.Min(row => row[x]))
                .ToArray();

            var maxs = Enumerable.Range(0, featureCount)
                .Select(x => data.Max(row => row[x]))
                .ToArray();

            return data
                .Select(row => row
                    .Select((v, j) =>
                    { 
                        var range = maxs[j] - mins[j];
                        return range == 0 ? 0.0 : (v - mins[j]) / range;
                    })
            .ToArray()).ToArray();
        }
    }
}
