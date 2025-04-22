using Nebula.Data.IO;

namespace Nebula.Sandbox.Experimentation.DataFrames
{
    public static class DataFramesExperiment
    {
        public static void Run()
        {
            var df = CsvReader.FromCsv(@"Experimentation/DataFrames/Data/sample_data.csv");

            Console.WriteLine(df.ToString());

            var columns = df.Columns;

            var names = df["Name"];
            Console.WriteLine(names);
        }
    }
}
