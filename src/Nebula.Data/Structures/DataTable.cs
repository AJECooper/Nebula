// <copyright file="DataTable.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.Data.Structures
{
    /// <summary>
    /// Represents a tabular data structure.
    /// </summary>
    public class DataTable
    {
        private readonly IList<string> _columns;
        private readonly IList<DataRow> _rows;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataTable"/> class.
        /// </summary>
        /// <param name="table">A collection of rows, each represented as a dictionary mapping column names to values.</param>
        public DataTable(IEnumerable<Dictionary<string, object>> table)
        {
            ValidateTable(table);

            _columns = table.First().Keys.ToList();
            _rows = table.Select(x => new DataRow(x)).ToList();
        }

        /// <summary>
        /// Validates that the provided table is not null or empty and that all rows have the same keys.
        /// </summary>
        /// <param name="table">The data table.</param>
        /// <exception cref="ArgumentException">Thrown when the table is null, empty, or rows have inconsistent keys.</exception>
        private void ValidateTable(IEnumerable<Dictionary<string, object>> table)
        {
            if (table == null || !table.Any())
            {
                throw new ArgumentException("Table cannot be null or empty.", nameof(table));
            }

            var keys = table.First().Keys.ToList();

            foreach (var row in table)
            {
                if (!keys.SequenceEqual(row.Keys))
                {
                    throw new ArgumentException("All rows must have the same keys.", nameof(table));
                }
            }    
        }
    }
}
