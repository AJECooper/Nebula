using Nebula.Data.Extensions;
using Nebula.Data.IO;
using Nebula.ML.Models.Regression;
using Nebula.ML.Preprocessing;

namespace Nebula.Sandbox.Demos.Regression
{
    public static class LinearRegressionDemo
    {
        public static void Run()
        {
            Console.WriteLine("This is a demo of how to use the linear regression model for regression tasks.");
            Console.WriteLine("\nIn this demo we will fit a linear regression model to estimate sales revenue based on advertisment budget");

            var dataTable = CsvReader.FromCsv("../../../Data/AdSpendVSalesRev.csv");

            Console.WriteLine(dataTable.Info(20));

            Console.WriteLine("\nNext we'll create our training data and labels by using the .ToVectorMatrix and .ToLabelVector and we'll view the inputs and targets");
            double[][] inputs = dataTable.ToVectorMatrix("AdSpend");
            double[] targets = dataTable.ToLabelVector<double>("SalesRevenue");

            for (int i = 0; i < inputs.Length; i++)
            {
                string featureString = "[" + string.Join(", ", inputs[i]) + "]";
                string labelString = "[" + targets[i] + "]";

                Console.WriteLine($"{featureString} -> {labelString}");
            }

            Console.WriteLine("\nNext we want to split our data into training and test data.");

            var (trainInputs, trainTargets, testInputs, testTargets) = DataSplit.Split(inputs, targets, 0.8, true, 124);

            Console.WriteLine($"\nTraining on {trainInputs.Length} samples, testing on {testInputs.Length} samples.");

            var model = new LinearRegression(null, 500, 0.00001);
            model.Fit(trainInputs, trainTargets);

            Console.WriteLine("\nTraining complete. Now we can test the model on the test data.");
           
            double totalLoss = 0;
            for (int i = 0; i < testInputs.Length; i++)
            {
                double prediction = model.Predict(testInputs[i]);
                totalLoss += (int)Math.Abs(prediction - testTargets[i]);

                var predFeatureString = "[" + string.Join(", ", testInputs[i]) + "]";
                Console.WriteLine($"For data sample {predFeatureString} I predicted {prediction}, the desired target is {testTargets[i]}");
            }

            double mae = totalLoss / testInputs.Length;
            Console.WriteLine($"\nMean absolute error on test set: {mae:F3}");

            double adSpend = 120.0;
            var estimatedRevenue = model.Predict(new double[] { adSpend });

            Console.WriteLine($"Predicted revenue for AdSpend={adSpend} -> {estimatedRevenue:F2}");

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
