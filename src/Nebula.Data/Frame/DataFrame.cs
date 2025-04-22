using System;

namespace Nebula.Data.Frame
{
    public class DataFrame
    {
        private IList<DataRow> _rows;
        private IList<string> _columns;

        /// <summary>
        /// Gets a list of column names from the DataFrame.
        /// </summary>
        public IEnumerable<string> Columns => _columns;

        /// <summary>
        /// Gets a list of rows from the DataFrame.
        /// </summary>
        public IEnumerable<DataRow> Rows => _rows;

        /// <summary>
        /// Creates an empty DataFrame.
        /// </summary>
        public DataFrame()
        {
            _columns = new List<string>();
            _rows = new List<DataRow>();
        }

        /// <summary>
        /// Creates a DataFrame with the specified column headers.
        /// </summary>
        /// <param name="columnHeaders">Column headers.</param>
        public DataFrame(IList<string> columnHeaders)
        {
            _columns = columnHeaders;
            _rows = new List<DataRow>();
        }

        /// <summary>
        /// Creates a fully populated DataFrame.
        /// </summary>
        /// <param name="data">Column headers and rows.</param>
        public DataFrame(IEnumerable<Dictionary<string, object>> data)
        {
            _columns = data.FirstOrDefault()?.Keys.ToList();
            _rows = data.Select(x => new DataRow(x)).ToList();
        }

        /// <summary>
        /// Adds a new column to the DataFrame.
        /// </summary>
        /// <param name="columnTitle">The column header title.</param>
        public void AddColumn(string columnTitle)
        {
            if (!_columns.Contains(columnTitle))
            {
                _columns.Add(columnTitle);
            }
        }

        /// <summary>
        /// Gets the rows of the DataFrame.
        /// </summary>
        /// <returns>A list of rows.</returns>
        public IEnumerable<DataRow> GetRows()
        {
            return _rows;
        }

        /// <summary>
        /// Adds a new row to the DataFrame.
        /// </summary>
        /// <param name="row">Represents a row to be added to the DataFrame.</param>
        public void AddRow(Dictionary<string, object> row)
        {
            _rows.Add(new DataRow(row));
        }

        /// <summary>
        /// Gets the value of the specified column for all rows in the DataFrame.
        /// </summary>
        /// <param name="columnName">The name of the column.</param>
        /// <returns>A list of rows within a specified column.</returns>
        public IEnumerable<object> this[string columnName] =>
            _rows.Select(row => row[columnName]);

        /// <summary>
        /// Extracts required columns by name from the DataFrame and returns a new DataFrame.
        /// </summary>
        /// <param name="columns">Column to extract by title.</param>
        /// <returns>A fully populated dataframe with only the required columns.</returns>
        /// <exception cref="ArgumentException"></exception>
        public DataFrame Extract(params string[] columns)
        {
            foreach (var column in columns)
            {
                if (!_columns.Contains(column))
                {
                    throw new ArgumentException($"Column '{column}' does not exist in the DataFrame.");
                }
            }

            var newRows = _rows.Select(row =>
                new DataRow(row._record.Where(kv => columns.Contains(kv.Key))
                                       .ToDictionary(kv => kv.Key, kv => kv.Value))
            ).ToList();

            return new DataFrame(columns.ToList()) { _rows = newRows };
        }

        /// <summary>
        /// Extracts required columns by index from the DataFrame and returns a new DataFrame.
        /// </summary>
        /// <param name="columns">Column to extract by index.</param>
        /// <returns>A fully populated dataframe with only the required columns.</returns>
        /// <exception cref="ArgumentException"></exception>
        public DataFrame Extract(params int[] columnIndexes)
        {
            // Validate indices
            foreach (var columnIndex in columnIndexes)
            {
                if (columnIndex < 0 || columnIndex >= _columns.Count)
                {
                    throw new ArgumentException($"Column index '{columnIndex}' is out of range.");
                }
            }

            // Get the column names from the indices
            var selectedColumnNames = columnIndexes.Select(i => _columns[i]).ToList();

            // Create new rows with only selected columns
            var newRows = _rows.Select(row =>
                new DataRow(
                    row._record
                        .Where(kv => selectedColumnNames.Contains(kv.Key))
                        .ToDictionary(kv => kv.Key, kv => kv.Value))
            ).ToList();

            // Return new DataFrame
            return new DataFrame(selectedColumnNames) { _rows = newRows };
        }

        /// <summary>
        /// A string representation of the DataFrame.
        /// </summary>
        /// <returns>The dataframe in string format.</returns>
        public override string ToString()
        {
            var stringOutput = new List<string>();

            // Add column names
            stringOutput.Add(string.Join(" | ", _columns));

            // Add separator
            stringOutput.Add(string.Join("-|-", _columns.Select(name => new string('-', name.Length))));

            // Add rows
            int maxOutput = 10;
            for (var i = 0; i < maxOutput; i++)
            {
                if (i >= _rows.Count())
                {
                    break;
                }

                var row = GetRows();
                var rowData = new List<string>();

                foreach(var column in _columns)
                {
                    rowData.Add(row.ElementAt(i).Get<object>(column)?.ToString() ?? "null");
                }

                stringOutput.Add(string.Join(" | ", rowData));
            }

            if (_rows.Count() > maxOutput)
                stringOutput.Add($"... {_rows.Count() - maxOutput} more row(s)");

            return string.Join("\n", stringOutput);
        }

        private bool Contains(string columnName)
        {
            return _columns.Contains(columnName);
        }
    }
}
