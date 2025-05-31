using Nebula.Core.Activations;
using Nebula.Data.Extensions;
using Nebula.Data.IO;
using Nebula.ML.Models.Classification;
using Nebula.ML.Preprocessing;

namespace Nebula.Sandbox.Demos.Classification
{
    public static class LogisticRegressionDemo
    {
        public static void Run()
        {
            Console.WriteLine("This is a demo of how to use the logistic regression model for binary classification.");
            Console.WriteLine("In this demo we will use logistic regression to predict what employees are at risk of leaving a company");

            // ────────────────────────────────────────────────────────────────────────
            // 1) Load the CSV into a DataTable and extract raw features/labels
            // ────────────────────────────────────────────────────────────────────────
            var dataTable = CsvReader.FromCsv("../../../Data/employee_data.csv");

            Console.WriteLine(dataTable.Info(5));

            Console.WriteLine("\nWe can see in this data that we have features that aren't numeric and have no importance on whether the person will leave or not; let's extract these.");

            var dataTableFiltered = dataTable.Extract(2, 3, 4, 5, 6, 7);
            Console.WriteLine(dataTableFiltered.Info(100));

            double[][] features = dataTableFiltered.ToVectorMatrix("YearsOfEmployment", "CurrentSalary", "StandardSalaryForRole", "PerformanceRating", "LastPromotionYearsAgo");
            int[] labels = dataTableFiltered.ToLabelVector<int>("AtRisk");

            // ────────────────────────────────────────────────────────────────────────
            // 2) Split into train/test data
            // ────────────────────────────────────────────────────────────────────────
            var (trainFeatures, trainLabels, testFeatures, testLabels) =
                DataSplit.Split(features, labels, splitRatio: 0.8, shuffle: true, seed: 124);

            // ────────────────────────────────────────────────────────────────────────
            // 3) Compute min/max on TRAINING SET
            // ────────────────────────────────────────────────────────────────────────
            Console.WriteLine("\nComputing per-feature min/max on the training set:");
            var (mins, maxs) = FeatureScaling.ComputeMinMax(trainFeatures);

            Console.WriteLine("    mins: [" + string.Join(", ", mins) + "]");
            Console.WriteLine("    maxs: [" + string.Join(", ", maxs) + "]");

            // ────────────────────────────────────────────────────────────────────────
            // 4) Apply those same mins/maxs to train and test
            // ────────────────────────────────────────────────────────────────────────
            double[][] trainNorm = FeatureScaling.ApplyMinMax(trainFeatures, mins, maxs);
            double[][] testNorm = FeatureScaling.ApplyMinMax(testFeatures, mins, maxs);

            // ────────────────────────────────────────────────────────────────────────
            // 5) Train the model on the normalized TRAINING data
            // ────────────────────────────────────────────────────────────────────────
            var model = new LogisticRegression(new SigmoidActivation(), 2000, 0.01);
            model.Fit(trainNorm, trainLabels);

            // ────────────────────────────────────────────────────────────────────────
            // 6) Test the model
            // ────────────────────────────────────────────────────────────────────────
            double totalLoss = 0.0;
            for (int i = 0; i < testNorm.Length; i++)
            {
                var (prediction, _) = model.Predict(testNorm[i]);

                int labelForFeature = testLabels[i];

                double eps = 1e-15;
                prediction = Math.Max(eps, Math.Min(1 - eps, prediction));

                // Cross‐entropy: −[y·ln(p) + (1−y)·ln(1−p)]
                totalLoss += -(labelForFeature * Math.Log(prediction) + (1 - labelForFeature) * Math.Log(1 - prediction));

                var sampleString = "[" + string.Join(", ", testNorm[i]) + "]";
                Console.WriteLine($"  Sample {sampleString} -> predicted {prediction}, actual {testLabels[i]}");
            }

            double avgLoss = totalLoss / testNorm.Length;
            Console.WriteLine($"\nAverage cross‐entropy loss on test set: {avgLoss:F4}");

            // ────────────────────────────────────────────────────────────────────────
            // 6) Predict on brand-new samples (always ApplyMinMax with the SAME mins/maxs!)
            // ────────────────────────────────────────────────────────────────────────
            double[] newVal = { 4, 102800.35, 110916.09, 3, 2 };
            double[] newValNorm = FeatureScaling.ApplyMinMax(new[] { newVal }, mins, maxs)[0];

            var (newPrediction, newClass) = model.Predict(newValNorm);
            Console.WriteLine($"\nFor the new sample {string.Join(", ", newVal)} we predict a probability of {newPrediction} and class {newClass} (0 = not at risk, 1 = at risk).");
        }
    }
}
