// <copyright file="OneHotEncoder.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.ML.Preprocessing
{
    /// <summary>
    /// Converts categorical data into a one-hot encoded format.
    /// </summary>
    public static class OneHotEncoder
    {
        /// <summary>
        /// Converts an array of categorical data into a one-hot encoded format.
        /// </summary>
        /// <param name="categoricalData">The column containing the categorical data.</param>
        /// <returns>A multi dimensional array where each categorical value is cast to a one-hot encoded vector.</returns>
        /// <exception cref="ArgumentException"> Thrown when new entry not in distinct list appears.</exception>
        public static double[][] Encode(string[] categoricalData)
        {
            var dataLength = categoricalData.Length;
            var uniqueCategories = categoricalData
                .Select(c => c.Trim())
                .Distinct()
                .ToArray();

            var categoriesLength = uniqueCategories.Length;

            if (categoriesLength == 0)
            {
                return Array.Empty<double[]>();
            }

            var categoryValueToIndex = new Dictionary<string, int>(dataLength);
            
            for (int i = 0; i < uniqueCategories.Length; i++)
            {
                categoryValueToIndex[uniqueCategories[i]] = i;
            }

            var oneHotEncoded = new double[dataLength][];

            for (int i = 0; i < dataLength; i++)
            {
                oneHotEncoded[i] = new double[categoriesLength];
            }

            for (int i = 0; i < dataLength; i++)
            {
                string cat = categoricalData[i].Trim();

                var colIndex = categoryValueToIndex[cat];

                oneHotEncoded[i][colIndex] = 1.0;
            }

            return oneHotEncoded;
        }

        /// <summary>
        /// Takes an existing numeric feature matrix (double[][]) and appends one-hot columns AI GENERATED.
        /// derived from <paramref name="categoricalData"/>. Assumes <c>categoricalData.Length == features.Length</c>.
        /// </summary>
        /// <param name="features">
        ///   A jagged array of size [nSamples][nNumericFeatures].
        ///   Each row is one sample’s numeric features.
        /// </param>
        /// <param name="categoricalData">
        ///   A string array of length nSamples, giving a category for each sample.
        /// </param>
        /// <returns>
        ///   A new jagged array of size [nSamples][nNumericFeatures + nUniqueCategories],
        ///   where each row contains the original numeric features followed by the one-hot encoding
        ///   of that sample’s category.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///   Thrown if <paramref name="categoricalData"/> and <paramref name="features"/> have different lengths.
        /// </exception>
        public static double[][] AppendEncodedColumn(double[][] features, string[] categoricalData)
        {
            int nSamples = features.Length;
            if (categoricalData.Length != nSamples)
            {
                throw new ArgumentException("Number of samples in features must match number of entries in categoricalData.");
            }

            double[][] oneHot = Encode(categoricalData);

            int nCategories = oneHot[0].Length;
            int nNumeric = features[0].Length;

            var result = new double[nSamples][];
            for (int i = 0; i < nSamples; i++)
            {
                result[i] = new double[nNumeric + nCategories];

                Array.Copy(features[i], 0, result[i], 0, nNumeric);

                Array.Copy(oneHot[i], 0, result[i], nNumeric, nCategories);
            }

            return result;
        }
    }
}
