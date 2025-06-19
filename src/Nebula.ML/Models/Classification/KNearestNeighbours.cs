// <copyright file="KNearestNeighbours.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

using Nebula.NICE.MathOps;

namespace Nebula.ML.Models.Classification
{
    /// <summary>
    /// K Nearest Neighbours (KNN) is a supervised learning algorithm used for classification and regression tasks.
    /// </summary>
    public class KNearestNeighbours
    {
        private double[][] _features;
        private string[] _labels;
        private int _k;
        private DistanceMetric _distanceMetric;

        public enum DistanceMetric
        {
            Euclidean,
            Manhattan,
            Chebyshev
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KNearestNeighbours"/> class.
        /// </summary>
        /// <param name="k">Number of closest neighbours.</param>
        /// <param name="metric">The calculation to find closest neighbours.</param>
        public KNearestNeighbours(int k = 3, DistanceMetric metric = DistanceMetric.Euclidean)
        {
            _k = k;
            _distanceMetric = metric;
        }

        public void Fit(double[][] features, string[] labels)
        {
            _features = features;
            _labels = labels;
        }

        public string Predict(double[] input)
        {
            var neighbours = _features
                .Select((f, i) => (Distance: GetDistance(f, input), Label: _labels[i]))
                .OrderBy(x => x.Distance)
                .Take(_k)
                .GroupBy(x => x.Label)
                .OrderByDescending(g => g.Count())
                .First().Key;

            return neighbours;
        }

        private double GetDistance(double[] a, double[] b)
        {
            return _distanceMetric switch
            {
                DistanceMetric.Euclidean => DistanceMetrics.Euclidean(a, b),
                DistanceMetric.Manhattan => DistanceMetrics.Manhattan(a, b),
                DistanceMetric.Chebyshev => DistanceMetrics.Chebyshev(a, b),
                _ => throw new NotImplementedException("Metric not implemented"),
            };
        }
    }
}
