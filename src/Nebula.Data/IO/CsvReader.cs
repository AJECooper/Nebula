using Nebula.Data.Frame;

namespace Nebula.Data.IO
{
    public class CsvReader
    {
        /// <summary>
        /// Reads a CSV file and returns a DataFrame.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>A DataFrame.</returns>
        public static DataFrame FromCsv(string filePath)
        {
            var df = new DataFrame();

            var lines = File.ReadAllLines(filePath);

            if (lines.Length == 0)
            {
                throw new Exception("The provided file is empty.");
            }

            // Read in the headers and remove any whitespace
            var headers = lines[0].Split(',')
                .Select(x => x.Trim())
                .ToArray();

            df.SetColumns(headers);

            // Read in the data
            for (var i = 1; i < lines.Length; i++)
            {
                var record = lines[i].Split(",")
                    .Select(i => i.Trim())
                    .ToArray();

                var row = new Dictionary<string, object>();

                // Map value to header
                for (var j = 0; j < headers.Length; j++)
                {
                    row[headers[j]] = record[j];
                }

                df.AddRow(row);
            }

            return df;
        }
    }
}
