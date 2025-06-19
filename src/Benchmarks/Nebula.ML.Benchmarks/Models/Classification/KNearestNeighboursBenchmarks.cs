// <copyright file="KNearestNeighboursBenchmarks.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.ML.Benchmarks.Models.Classification
{
    using BenchmarkDotNet.Attributes;
    using Nebula.ML.Models.Classification;

    [MemoryDiagnoser]
    public class KNearestNeighboursBenchmarks
    {
        [Params(100, 1000, 10000, 20000)]
        public int SampleCount;

        [Params(2, 5, 20, 50)]
        public int FeatureCount;

        [Params(3, 5, 7)]
        public int K;

        private double[][] _features;

        private double[] _testSample;

        [GlobalSetup]
        public void Setup()
        {
            GenerateFakeData(SampleCount, FeatureCount);

            _testSample = new double[FeatureCount];
            var random = new Random();
            for (int i = 0; i < FeatureCount; i++)
            {
                _testSample[i] = random.NextDouble();
            }
        }

        [Benchmark(Description = "Predicts nearest neighbours using Euclidean metrics")]
        public void Benchmark_Predict_Euclidean()
        {
            var model = new KNearestNeighbours(K, KNearestNeighbours.DistanceMetric.Euclidean);
            model.Fit(_features, new string[SampleCount]);
            model.Predict(_testSample);
        }

        [Benchmark(Description = "Predicts nearest neighbours using Manhattan metrics")]
        public void Benchmark_Predict_Manhattan()
        {
            var model = new KNearestNeighbours(K, KNearestNeighbours.DistanceMetric.Manhattan);
            model.Fit(_features, new string[SampleCount]);
            model.Predict(_testSample);
        }

        [Benchmark(Description = "Predicts nearest neighbours using Chebyshev metrics")]
        public void Benchmark_Predict_Chebyshev()
        {
            var model = new KNearestNeighbours(K, KNearestNeighbours.DistanceMetric.Chebyshev);
            model.Fit(_features, new string[SampleCount]);
            model.Predict(_testSample);
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
