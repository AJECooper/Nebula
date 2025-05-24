// <copyright file="DataSplit.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.ML.Preprocessing
{
    /// <summary>
    /// Utility class for splitting datasets into training and testing sets.
    /// </summary>
    public static class DataSplit
    {
        /// <summary>
        /// Splits features and labels into training and testing subsets.
        /// </summary>
        /// <typeparam name="T">The target type to which each label value will be converted.</typeparam>
        /// <param name="features">A 2D array of features.</param>
        /// <param name="labels"> 1D array of labels.</param>
        /// <param name="splitRatio">Fraction of data to use for training (between 0 and 1).</param>
        /// <param name="shuffle">Whether to randomize (shuffle) the order before splitting.</param>
        /// <param name="seed">Optional random seed for reproducible shuffling. Ignored if <paramref name="shuffle"/> is <c>false</c>.</param>
        /// <returns> tuple of (trainX, trainY, testX, testY).</returns>
        /// <exception cref="ArgumentNullException">Thrown if features or labels are null.</exception>
        /// <exception cref="ArgumentException">Thrown if ratios are out of range or lengths mismatch.</exception>
        public static (double[][] trainX, T[] trainY, double[][] testX, T[] testY) Split<T>(
            double[][] features,
            T[] labels,
            double splitRatio = 0.8,
            bool shuffle = false,
            int? seed = null)
        {
            if (features == null || labels == null)
            {
                throw new ArgumentNullException("Features and labels cannot be null.");
            }

            if (!features.Length.Equals(labels.Length))
            {
                throw new ArgumentException("Features and labels must have the same number of rows.");
            }

            if (splitRatio <= 0 || splitRatio >= 1)
            {
                throw new ArgumentException("Split ratio must be between 0 and 1.");
            }

            IEnumerable<int> indices = Enumerable.Range(0, features.Length);

            if (shuffle)
            {
                var rng = seed.HasValue ? new Random(seed.Value) : new Random();
                indices = indices.OrderBy(_ => rng.Next());
            }

            var idxArray = indices.ToArray();
            int trainCount = (int)(features.Length * splitRatio);

            var trainIdx = idxArray.Take(trainCount);
            var testIdx = idxArray.Skip(trainCount);

            double[][] trainX = trainIdx.Select(i => features[i]).ToArray();
            T[] trainY = trainIdx.Select(i => labels[i]).ToArray();
            double[][] testX = testIdx.Select(i => features[i]).ToArray();
            T[] testY = testIdx.Select(i => labels[i]).ToArray();

            return (trainX, trainY, testX, testY);
        }
    }
}
