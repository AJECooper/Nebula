// <copyright file="JsonReader.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.Data.IO
{
    using System.Collections.Specialized;
    using System.Text.Json;
    using Nebula.Data.Structures;

    /// <summary>
    /// Reads from a json file and parses data to DataTable.
    /// </summary>
    public static class JsonReader
    {
        /// <summary>
        /// Reads from json file and parses to <see cref="DataTable"/>.
        /// </summary>
        /// <param name="filePath">Path to json file.</param>
        /// <returns>A <see cref="DataTable"/> containing the data found in the json file.</returns>
        /// <exception cref="FileNotFoundException">Throws if file does not exist.</exception>
        /// <exception cref="InvalidOperationException">Throws if file is empty.</exception>
        public static DataTable FromJson(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file '{filePath}' not found in project.");
            }

            var content = File.ReadAllText(filePath);

            if (content == null || string.IsNullOrWhiteSpace(content))
            {
                throw new InvalidOperationException("File is empty. Unable to parse data.");
            }

            IList<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                rows = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(content);
            }
            catch
            {
                throw new InvalidOperationException("Data parsing failed. Json file is not in an appropriate format.");
            }

            if (!rows.Any())
            {
                throw new InvalidOperationException("Data extraction failed. Data is null when parsing the json file.");
            }

            for (int i = 0; i < rows.Count; i++)
            {
                var keys = rows[i].Keys.ToList();

                for (int j = 0; j < keys.Count(); j++)
                {
                    var key = keys[j];

                    if (rows[i][key] is JsonElement el)
                    {
                        switch(el.ValueKind)
                        {
                            case JsonValueKind.String:
                                rows[i][key] = el.GetString();
                                break;
                            case JsonValueKind.Number:
                                rows[i][key] = el.GetDouble();
                                break;
                        }
                    }
                }
            }

            return new DataTable(rows);
        }
    }
}
