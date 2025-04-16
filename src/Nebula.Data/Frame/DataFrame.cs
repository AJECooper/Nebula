namespace Nebula.Data.Frame
{
    public class DataFrame
    {
        private IList<DataRow> _rows;
        private IEnumerable<string> _columns;

        public DataFrame()
        {
            _rows = new List<DataRow>();
            _columns = new List<string>();
        }

        /// <summary>
        /// Get a list of columns in the DataFrame.
        /// </summary>
        /// <returns>A list of columns.</returns>
        public IEnumerable<string> GetColumns()
        {
            return _columns;
        }

        /// <summary>
        /// Adds new columns to the DataFrame.
        /// </summary>
        /// <param name="columns">A list of columns to add to the DataFrame.</param>
        public void SetColumns(IEnumerable<string> columns)
        {
            _columns = columns;
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
    }
}
