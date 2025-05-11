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
        private readonly IReadOnlyList<string> _columns;
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
        /// Gets the number of columns in the data table.
        /// </summary>
        public int ColumnCount => _columns.Count;

        /// <summary>
        /// Gets the number of rows in the data table.
        /// </summary>
        public int RowCount => _rows.Count;

        /// <summary>
        /// Gets the column names of the data table.
        /// </summary>
        public IReadOnlyList<string> Columns => _columns;

        /// <summary>
        /// Gets the row of the data table by index.
        /// </summary>
        /// <param name="index">The index of the desired row.</param>
        /// <returns>A data row representing a single row of data within the data table.</returns>
        /// <exception cref="IndexOutOfRangeException">User tried to access a row that is out of range.</exception>
        public DataRow GetRowByIndex(int index)
        {
            if (index < 0 || index >= _rows.Count)
            {
                throw new IndexOutOfRangeException();
            }

            return _rows[index];
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
