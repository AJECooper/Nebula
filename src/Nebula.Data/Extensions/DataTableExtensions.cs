// <copyright file="DataTableExtensions.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.Data.Extensions
{
    using Nebula.Data.Structures;

    /// <summary>
    /// Provides extension methods for <see cref="DataTable"/> to convert it into feature and label vectors.
    /// </summary>
    public static class DataTableExtensions
    {
        /// <summary>
        /// Projects the specified columns of a <see cref="DataTable"/> into a numeric feature matrix.
        /// </summary>
        /// <param name="dataTable">The source data table containing raw data.</param>
        /// <param name="featureColumns">A list of columns to convert to vector matrix.</param>
        /// <returns>A two dimensional array containing the index and feature matrix within the inner array.</returns>
        public static double[][] ToVectorMatrix(this DataTable dataTable, params string[] featureColumns)
        {
            return dataTable.GetFeatures(featureColumns)
                        .Select(row => row.Select(Convert.ToDouble).ToArray())
                        .ToArray();
        }

        /// <summary>
        /// Projects the specified column of a <see cref="DataTable"/> into a strongly typed label vector.
        /// </summary>
        /// <typeparam name="T">The target type to which each label value will be converted.</typeparam>
        /// <param name="dataTable">The source data table containing raw data.</param>
        /// <param name="labelColumn">The name of the column to extract as labels.</param>
        /// <returns>An array of <typeparamref name="T"/> containing one label per row in the table.</returns>
        public static T[] ToLabelVector<T>(this DataTable dataTable, string labelColumn)
        {
            return dataTable.GetTargets(labelColumn)
                        .Select(value => (T)Convert.ChangeType(value, typeof(T)))
                        .ToArray();
        }
    }
}
