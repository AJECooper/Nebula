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
        /// Computes the minimum and maximum values from the given dataset, per feature.
        /// </summary>
        /// <param name="data">A two‐dimensional array where each sub‐array is a sample and each element is a feature value.</param>
        /// <returns>A tuple containing the min and max arrays.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="data"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="data"/> has zero rows or if rows have varying lengths.</exception>
        public static (double[] mins, double[] max) ComputeMinMax(double[][] data)
        {
            if (data == null || data.Length == 0)
            {
                throw new ArgumentNullException(nameof(data));
            }

            int featureCount = data[0].Length;

            if (data.Any(x => x.Length != featureCount))
            {
                throw new ArgumentException("All rows in the data must have the same number of features.");
            }

            var mins = Enumerable.Range(0, featureCount)
                .Select(x => data.Min(row => row[x]))
                .ToArray();

            var maxs = Enumerable.Range(0, featureCount)
                .Select(x => data.Max(row => row[x]))
                .ToArray();

            return (mins, maxs);
        }

        /// <summary>
        /// Applies min–max scaling to a batch of samples using the provided per‐feature mins and maxs.
        /// </summary>
        /// <param name="data">A two‐dimensional array where each sub‐array is a sample and each element is a feature value.</param>
        /// <param name="mins">The minimum value for each feature (column), as computed by <see cref="ComputeMinMax"/> on the training set.</param>
        /// <param name="max">The maximum value for each feature (column), as computed by <see cref="ComputeMinMax"/> on the training set.</param>
        /// <returns>A new two‐dimensional array containing the scaled feature values in the range [0, 1].</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="data"/> or <paramref name="mins"/> or <paramref name="max"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="mins"/> does not equal <paramref name="mins"/> or <paramref name="mins"/> length does not equal <paramref name="data"/> length.</exception>
        public static double[][] ApplyMinMax(double[][] data, double[] mins, double[] max)
        {
            if (data == null || data.Length == 0)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (mins == null || max == null)
            {
                throw new ArgumentNullException("Mins and max must not be null");
            }

            if (mins.Length != max.Length || mins.Length != data[0].Length)
            {
                throw new ArgumentException("Mins and max must have the same length as the number of features in the data.");
            }

            int samples = data.Length;

            if (samples == 0)
            {
                return Array.Empty<double[]>();
            }

            int nFeature = mins.Length;

            var output = new double[samples][];

            for (int i = 0; i < samples; i++)
            {
                output[i] = new double[nFeature];
                for (int j = 0; j < nFeature; j++)
                {
                    double range = max[j] - mins[j];
                    if (range == 0.0)
                    {
                        output[i][j] = 0.0;
                    }
                    else
                    {
                        output[i][j] = (data[i][j] - mins[j]) / range;
                    }
                }
            }

            return output;
        }

        /// <summary>
        /// Applies min–max scaling to a single sample (one feature‐vector) using the provided per‐feature mins and maxs.
        /// </summary>
        /// <param name="sample">A one‐dimensional array representing a single sample’s feature values.</param>
        /// <param name="mins">The minimum value for each feature (column), as computed by <see cref="ComputeMinMax"/> on the training set.</param>
        /// <param name="maxs">The maximum value for each feature (column), as computed by <see cref="ComputeMinMax"/> on the training set.</param>
        /// <returns>A new one‐dimensional array containing the scaled feature values in the range [0, 1].</returns>
        /// <exception cref="ArgumentNullException"> Thrown if <paramref name="sample"/>, <paramref name="mins"/>, or <paramref name="maxs"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="sample"/> length does not match <paramref name="mins"/>/<paramref name="maxs"/> length.</exception>
        public static double[] ApplyMinMax(double[] sample, double[] mins, double[] maxs)
        {
            if (sample == null)
            {
                throw new ArgumentNullException(nameof(sample));
            }

            if (mins == null)
            {
                throw new ArgumentNullException(nameof(mins));
            }

            if (maxs == null)
            {
                throw new ArgumentNullException(nameof(maxs));
            }

            if (sample.Length != mins.Length || sample.Length != maxs.Length)
            {
                throw new ArgumentException("Sample length must match mins/maxs length.");
            }

            int nFeatures = sample.Length;
            var normalized = new double[nFeatures];

            for (int j = 0; j < nFeatures; j++)
            {
                double range = maxs[j] - mins[j];
                if (range == 0.0)
                {
                    normalized[j] = 0.0;
                }
                else
                {
                    normalized[j] = (sample[j] - mins[j]) / range;
                }
            }

            return normalized;
        }

    }
}
