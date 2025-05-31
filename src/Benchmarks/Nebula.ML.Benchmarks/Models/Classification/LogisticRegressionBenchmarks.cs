// <copyright file="LogisticRegressionBenchmarks.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.ML.Benchmarks.Models.Classification
{
    using BenchmarkDotNet.Attributes;
    using Nebula.ML.Models.Classification;

    [MemoryDiagnoser]
    public class LogisticRegressionBenchmarks
    {
        [Params(100, 1000, 10000, 50000, 100000)]
        public int SampleCount;

        [Params(2, 5, 20, 50)]
        public int FeatureCount;

        [Params(10, 20, 50, 100, 500)]
        public int Epochs;

        private double[][] _features;

        [GlobalSetup]
        public void Setup()
        {
            GenerateFakeData(SampleCount, FeatureCount);
        }

        [Benchmark(Description = $"Trains the model to predict for new data points over X epochs")]
        public void Benchmark_Fit()
        {
            var model = new LogisticRegression(null, Epochs);
            model.Fit(_features, new int[SampleCount]);
        }

        private void GenerateFakeData(int sampleCount, int featureCount)
        {
            var random = new Random();
            _features = new double[sampleCount][];

            for (int i = 0; i < sampleCount; i++)
            {
                _features[i] = new double[featureCount];
                for (int j = 0; j < featureCount; j++)
                {
                    _features[i][j] = random.NextDouble();
                }
            }
        }
    }
}
