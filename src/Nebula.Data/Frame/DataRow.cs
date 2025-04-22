namespace Nebula.Data.Frame
{
    public class DataRow
    {
        /// <summary>
        /// Values stored as key-value pairs (column name and value).
        /// </summary>
        public readonly Dictionary<string, object> _record;

        public DataRow(Dictionary<string, object> record)
        {
            _record = record;
        }

        /// <summary>
        /// Gets the value of the specified column.
        /// </summary>
        /// <param name="columnName">The name of the column.</param>
        /// <returns>A single value within the dataframe.</returns>
        public object this[string columnName] => _record[columnName];

        /// <summary>
        /// Gets the value of the specified column and casts it to the specified type.
        /// </summary>
        /// <typeparam name="T">Generic class type</typeparam>
        /// <param name="columnName">The name of the column.</param>
        /// <returns>A single value within the dataframe with type safety.</returns>
        public T Get<T>(string columnName) => (T)_record[columnName];

        public IList<object> ToList()
        {
            return _record.Values.ToList();
        }
    }
}
