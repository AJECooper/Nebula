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
            Console.WriteLine("In this demo we will use the perceptron to determine if a mystery piece of fruit is an apple or and orange based on the weight and smoothness of the friut.");
            Console.WriteLine("For this we'll say that fruits of a higher weight and smoothness will be an apple and lower weights and smoothness is an orange.");

            var dataTable = CsvReader.FromCsv("../../../Data/more_fruit.csv");

            Console.WriteLine("We can use the .info method to see the data in a tabular format.");
            Console.WriteLine(dataTable.Info());

            Console.WriteLine("\nNext we'll create our training data and labels by using the .ToVectorMatric and .ToLabelVector");

            double[][] features = dataTable.ToVectorMatrix("Weight", "Smoothness");
            int[] labels = dataTable.ToLabelVector<int>("Label");

            for (int i = 0; i < features.Length; i++)
            {
                string featureString = "[" + string.Join(", ", features[i]) + "]";
                string labelString = "[" + labels[i] + "]";

                Console.WriteLine($"{featureString} -> {labelString}");
            }

            Console.WriteLine("\nNext we want to split our data into training and test data.");

            var (trainFeatures, trainLabels, testFeatures, testLabels) = DataSplit.Split(features, labels, 0.8, true, 124);

            Console.WriteLine($"\nTraining on {trainFeatures.Length} samples, testing on {testFeatures.Length} samples.");

            Console.WriteLine("\nNow we can create our perceptron model and train it on the training data.");

            var perceptron = new Perceptron(null, epochs: 50, learningRate: 0.1);
            perceptron.Fit(trainFeatures, trainLabels);

            Console.WriteLine("\nTraining complete. Now we can test the model on the test data.");

            int correctPredictions = 0;

            for (int i = 0; i < testFeatures.Length; i++)
            {
                int prediction = perceptron.Predict(testFeatures[i]);
                if (prediction == testLabels[i])
                {
                    correctPredictions++;
                }

                var predFeatureString = "[" + string.Join(", ", testFeatures[i]) + "]";
                Console.WriteLine($"For data sample {predFeatureString} I predicted {prediction}, the correct label is {testLabels[i]}");
            }

            Console.WriteLine($"\nModel Accuracy: {(double)correctPredictions / testFeatures.Length}");

            Console.WriteLine("\nNow, let's make a prediction on a new piece of fruit. Let's say we have a fruit with a weight of 150 grams and a smoothness of 0.8.");

            double[] newFruit = new double[] { 150, 0.8 };

            int newPrediction = perceptron.Predict(newFruit);

            string fruitType = newPrediction == 1 ? "Apple" : "Orange";

            Console.WriteLine($"\nThe model predicts that the new fruit is an {fruitType} based on its weight and smoothness.");

            Console.WriteLine("\nLet's try another fruit with a weight of 90 grams and a smoothness of 0.3 to ensure that our model knows to predict orange!.");

            double[] anotherNewFruit = new double[] { 90, 0.3 };
            int anotherNewPrediction = perceptron.Predict(anotherNewFruit);

            string anotherFruitType = anotherNewPrediction == 1 ? "Apple" : "Orange";

            Console.WriteLine($"\nThe model predicts that the new fruit is an {anotherFruitType} based on its weight and smoothness.");
        }
    }
}
