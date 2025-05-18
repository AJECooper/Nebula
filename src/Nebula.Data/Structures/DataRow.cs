// <copyright file="DataRow.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.Data.Structures
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a single row of tabular data which maps columns to values.
    /// </summary>
    public class DataRow
    {
        private readonly IReadOnlyDictionary<string, object> _record;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataRow"/> class.
        /// </summary>
        /// <param name="record">Key value pair with column and value.</param>
        /// <exception cref="ArgumentNullException">Thrown when the record is null keys.</exception>
        public DataRow(Dictionary<string, object> record)
        {
            _record = record ?? throw new ArgumentNullException(nameof(record));
        }

        /// <summary>
        /// Gets the value for specified column name.
        /// </summary>
        /// <param name="columnName">The column title.</param>
        /// <returns>The value for the specified column.</returns>
        public object this[string columnName] => _record[columnName];

        /// <summary>
        /// Gets the value for specified column index.
        /// </summary>
        /// <param name="columnIndex">The column index.</param>
        /// <returns>The value for the specified column.</returns>
        public object this[int columnIndex] => _record[_record.Keys.ElementAt(columnIndex)];
    }
}
