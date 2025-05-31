using Nebula.Data.Extensions;
using Nebula.Data.IO;
using Nebula.ML.Models.Classification;
using Nebula.ML.Preprocessing;

namespace Nebula.Sandbox.Demos.Classification
{
    public static class PerceptronDemo
    {
        public static void Run()
        {
            Console.WriteLine("This is a demo of how to use the perceptron model for simple binary classification");
            Console.WriteLine("In this demo we will use the perceptron to determine if a mystery piece of fruit is an apple or an orange based on the weight and smoothness of the fruit.");
            Console.WriteLine("For this we'll say that fruits of a higher weight and smoothness will be an apple, and lower weight and smoothness will be an orange.");

            // ────────────────────────────────────────────────────────────────────────
            // 1) Load the CSV into a DataTable and extract raw features/labels
            // ────────────────────────────────────────────────────────────────────────
            var dataTable = CsvReader.FromCsv("../../../Data/more_fruit.csv");

            Console.WriteLine("We can use the .Info() method to see the data in a tabular format:");
            Console.WriteLine(dataTable.Info());

            Console.WriteLine("\nNext we'll extract our feature matrix and label vector:");
            double[][] features = dataTable.ToVectorMatrix("Weight", "Smoothness");
            int[] labels = dataTable.ToLabelVector<int>("Label");

            for (int i = 0; i < features.Length; i++)
            {
                string featureString = "[" + string.Join(", ", features[i]) + "]";
                string labelString = "[" + labels[i] + "]";
                Console.WriteLine($"{featureString} -> {labelString}");
            }

            // ────────────────────────────────────────────────────────────────────────
            // 2) Split into train/test data
            // ────────────────────────────────────────────────────────────────────────
            Console.WriteLine("\nNow we split raw data into 80% train / 20% test:");
            var (trainFeatures, trainLabels, testFeatures, testLabels) =
                DataSplit.Split(features, labels, splitRatio: 0.8, shuffle: true, seed: 124);

            Console.WriteLine($"\nTraining on {trainFeatures.Length} samples, testing on {testFeatures.Length} samples.");

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
            Console.WriteLine("\nScaling the training set into [0,1]:");
            double[][] trainNorm = FeatureScaling.ApplyMinMax(trainFeatures, mins, maxs);
            for (int i = 0; i < trainNorm.Length; i++)
            {
                Console.WriteLine("  [" + string.Join(", ", trainNorm[i]) + "]");
            }

            Console.WriteLine("\nScaling the test set into [0,1]:");
            double[][] testNorm = FeatureScaling.ApplyMinMax(testFeatures, mins, maxs);
            for (int i = 0; i < testNorm.Length; i++)
            {
                Console.WriteLine("  [" + string.Join(", ", testNorm[i]) + "]");
            }

            // ────────────────────────────────────────────────────────────────────────
            // 5) Train the perceptron on the normalized TRAINING data
            // ────────────────────────────────────────────────────────────────────────
            Console.WriteLine("\nNow we create and train our perceptron on the normalized training data:");
            var perceptron = new Perceptron(null, epochs: 50, learningRate: 0.1);
            perceptron.Fit(trainNorm, trainLabels);

            Console.WriteLine("\nTraining complete. Testing on the normalized test set:");
            int correct = 0;
            for (int i = 0; i < testNorm.Length; i++)
            {
                int prediction = perceptron.Predict(testNorm[i]);
                if (prediction == testLabels[i])
                    correct++;

                string sampleStr = "[" + string.Join(", ", testNorm[i]) + "]";
                Console.WriteLine($"  Sample {sampleStr} -> predicted {prediction}, actual {testLabels[i]}");
            }

            Console.WriteLine($"\nModel Accuracy: {(double)correct / testNorm.Length:P2}");

            // ────────────────────────────────────────────────────────────────────────
            // 6) Predict on brand-new samples (always ApplyMinMax with the SAME mins/maxs!)
            // ────────────────────────────────────────────────────────────────────────
            Console.WriteLine("\nPredicting on a brand-new fruit (weight=150, smoothness=0.8):");
            double[] newFruit = new double[] { 150, 0.8 };
            double[] newFruitScaled = FeatureScaling.ApplyMinMax(newFruit, mins, maxs);
            int newPrediction = perceptron.Predict(newFruitScaled);
            string newFruitType = newPrediction == 1 ? "Apple" : "Orange";
            Console.WriteLine($"  Scaled input: [{string.Join(", ", newFruitScaled)}]");
            Console.WriteLine($"  -> Model predicts: {newFruitType}");

            Console.WriteLine("\nNow another new fruit (weight=90, smoothness=0.3):");
            double[] anotherNewFruit = new double[] { 90, 0.3 };
            double[] anotherNewFruitScaled = FeatureScaling.ApplyMinMax(anotherNewFruit, mins, maxs);
            int anotherPrediction = perceptron.Predict(anotherNewFruitScaled);
            string anotherFruitType = anotherPrediction == 1 ? "Apple" : "Orange";
            Console.WriteLine($"  Scaled input: [{string.Join(", ", anotherNewFruitScaled)}]");
            Console.WriteLine($"  -> Model predicts: {anotherFruitType}");
        }
    }
}
