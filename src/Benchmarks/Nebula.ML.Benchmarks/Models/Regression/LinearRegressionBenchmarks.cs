// <copyright file="LinearRegressionBenchmarks.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.ML.Benchmarks.Models.Regression
{
    using BenchmarkDotNet.Attributes;
    using Nebula.ML.Models.Regression;

    [MemoryDiagnoser]
    public class LinearRegressionBenchmarks
    {
        [Params(100, 1000, 10000, 50000, 100000)]
        public int SampleCount;

        [Params(2, 5, 20, 50)]
        public int FeatureCount;

        private double[][] _features;
        private double[] _labels;

        [GlobalSetup]
        public void Setup()
        {
            GenerateFakeData(SampleCount, FeatureCount);
        }

        [Benchmark(Description = "Trains the Linear Regression model over 100 epochs")]
        public void Benchmark_Fit()
        {
            var model = new LinearRegression(null, 100);
            model.Fit(_features, _labels);
        }

        private void GenerateFakeData(int sampleCount, int featureCount)
        {
            var random = new Random();
            _features = new double[sampleCount][];
            _labels = new double[sampleCount];

            for (int i = 0; i < sampleCount; i++)
            {
                _features[i] = new double[featureCount];
                for (int j = 0; j < featureCount; j++)
                {
                    _features[i][j] = random.NextDouble();
                }

                _labels[i] = 0.5 * _features[i][0] + 0.3 * (featureCount > 1 ? _features[i][1] : 0) + random.NextDouble() * 0.1;
            }
        }
    }
}
