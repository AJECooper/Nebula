// <copyright file="CsvReader.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.Data.IO
{
    using Nebula.Data.Structures;

   /// <summary>
    /// Reads from a csv files and converts them into a DataTable.
    /// </summary>
    public static class CsvReader
    {
        /// <summary>
        /// Reads from a csv file and converts it into a <see cref"DataTable"/>.
        /// </summary>
        /// <param name="filePath">The path to the csv file.</param>
        /// <returns>A <see cref="DataTable"/> containing the data from the csv file.</returns>
        /// <exception cref="FileNotFoundException">Throws if file does not exist.</exception>
        /// <exception cref="InvalidOperationException">Throws if file is empty.</exception>
        public static DataTable FromCsv(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file '{filePath}' does not exist.");
            }

            var content = File.ReadAllLines(filePath);

            if (content.Length == 0)
            {
                throw new InvalidOperationException("The file is empty.");
            }

            if (content.Length < 2)
            {
                throw new InvalidOperationException("The file must contain headers and at least one row of data.");
            }

            var columns = content[0].Split(',')
                .Select(x => x.Trim())
                .ToArray();

            var tableData = new List<Dictionary<string, object>>();

            for (int i = 1; i < columns.Length; i++)
            {
                var rowData = content[i]
                    .Split(',')
                    .Select(x => x.Trim())
                    .ToArray();

                if (rowData.Length != columns.Length)
                {
                    throw new FormatException($"Row {i + 1} has {rowData.Length} values but expected {columns.Length}.");
                }

                var row = new Dictionary<string, object>();

                for (int j = 0; j < columns.Length; j++)
                {
                    row[columns[j]] = rowData[j] == string.Empty ? null : rowData[j];
                }

                tableData.Add(row);
            }

            return new DataTable(tableData);
        }
    }
}
