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
                throw new IndexOutOfRangeException($"Row {index} is out of range for the data table. The range is 0 - {_rows.Count - 1}");
            }

            return _rows[index];
        }

        /// <summary>
        /// Gets all values of a specified column by column name.
        /// </summary>
        /// <param name="columnName">The name of the column.</param>
        /// <returns>A list of values paired to a column name.</returns>
        /// <exception cref="ArgumentException">Thrown when entering a name that doesn't exist in the data table.</exception>
        public IEnumerable<object> GetColumn(string columnName)
        {
            if (!_columns.Contains(columnName))
            {
                throw new ArgumentException($"Column '{columnName}' does not exist in the data table.", nameof(columnName));
            }

            return _rows.Select(x => x[columnName]).ToList();
        }

        /// <summary>
        /// Tries to get the values of a specified column by column name.
        /// </summary>
        /// <param name="columnName">The name of the column.</param>
        /// <param name="data">All the rows of data for the specified column.</param>
        /// <returns><c>true</c> if the column exists, <c>false</c> if not.</returns>
        public bool TryGetColumn(string columnName, out IList<object> data)
        {
            if (_columns.Contains(columnName))
            {
                data = _rows.Select(x => x[columnName]).ToList();
                return true;
            }

            data = null;
            return false;
        }

        /// <summary>
        /// Creates a new data table with only the specified columns.
        /// </summary>
        /// <param name="columnNames">The required columns for the new data table.</param>
        /// <returns>A new data table with only the required data.</returns>
        /// <exception cref="ArgumentException">Throws when column names are null, empty or don't exist in the existing data table.</exception>
        public DataTable Extract(params string[] columnNames)
        {
            if (columnNames == null || columnNames.Length == 0)
            {
                throw new ArgumentException("Column names cannot be null or empty.", nameof(columnNames));
            }

            foreach (var columnName in columnNames)
            {
                if (!_columns.Contains(columnName))
                {
                    throw new ArgumentException($"Column '{columnName}' does not exist in the data table.", nameof(columnNames));
                }
            }

            var newDataTable = _rows
                .Select(row => columnNames.ToDictionary(columnName => columnName, columnName => row[columnName]))
                .ToList();

            return new DataTable(newDataTable);
        }

        /// <summary>
        /// Creates a new data table with only the specified columns by their indexes.
        /// </summary>
        /// <param name="columnIndexes">The index of the columns to extract.</param>
        /// <returns>A new data table with only the required data.</returns>
        /// <exception cref="ArgumentException">Throws when column indexes are null or empty.</exception>
        /// <exception cref="IndexOutOfRangeException">Throws when column indexes are out of range.</exception>
        public DataTable Extract(params int[] columnIndexes)
        {
            if (columnIndexes == null || columnIndexes.Length == 0)
            {
                throw new ArgumentException("You must provide the column indexes to extract.", nameof(columnIndexes));
            }

            if (columnIndexes.Any(x => x < 0 || x >= _columns.Count))
            {
                throw new IndexOutOfRangeException("Column indexes are out of range.");
            }

            var columnNames = columnIndexes
                .Select(x => _columns[x])
                .ToArray();

            return Extract(columnNames);
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
                    throw new ArgumentException("All rows must have the same column structure.", nameof(table));
                }
            }    
        }
    }
}
