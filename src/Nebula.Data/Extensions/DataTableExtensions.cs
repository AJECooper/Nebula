// <copyright file="DataTableExtensions.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.Data.Extensions
{
    using Nebula.Data.Structures;

    public static class DataTableExtensions
    {
        public static double[][] ToVectorMatrix(this DataTable dataTable, params string[] featureColumns)
        {
            return dataTable.GetFeatures(featureColumns)
                        .Select(row => row.Select(Convert.ToDouble).ToArray())
                        .ToArray();
        }

        public static T[] ToLabelVector<T>(this DataTable dataTable, string labelColumn)
        {
            return dataTable.GetTargets(labelColumn)
                        .Select(value => (T)Convert.ChangeType(value, typeof(T)))
                        .ToArray();
        }
    }
}
