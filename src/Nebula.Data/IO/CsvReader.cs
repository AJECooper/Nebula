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
            if (!filePath.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("The provided file is not a CSV file.");
            }

            var fileContent = File.ReadAllLines(filePath);

            if (fileContent.Length == 0)
            {
                throw new Exception("The provided file is empty.");
            }

            var columnHeaders = fileContent[0].Split(',')
                .Select(x => x.Trim())
                .ToArray();

            var data = fileContent.Skip(1)
                .Select(row =>
                {
                    var columnValues = row.Split(',')
                        .Select(val => val.Trim())
                        .ToList();

                    var rowData = new Dictionary<string, object>();

                    for(var i = 0; i < columnHeaders.Length; i++)
                    {
                        rowData[columnHeaders[i]] = columnValues[i];
                    }

                    return rowData;
                });

            return new DataFrame(data);
        }
    }
}
