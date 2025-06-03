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

        public static double[][] AppendEncodedColumn(double[][] features, string[] categoricalData)
        {
            return features;
        }
    }
}
