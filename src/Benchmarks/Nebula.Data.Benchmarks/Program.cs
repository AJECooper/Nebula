// <copyright file="Program.cs" company="Nebula">
// Copyright © Nebula 2025
// </copyright>

namespace Nebula.Data.Benchmarks
{
    using BenchmarkDotNet.Running;

    public class Program
    {
        static void Main(string[] args) =>
            BenchmarkSwitcher
            .FromAssembly(typeof(Program).Assembly)
            .Run(args);
    }
}