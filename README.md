# NEBULA - The NICE Engine Behind Universal Learning Algorithms

Nebula is a modular, open source .NET machine learning framework built for flexibility and experimentation.
The framework includes components for data extraction, creating neural networks and pre built classic machine learning algorithms.

## Features

Nebula contain multiple libraries that are independently versioned and can be used separately or together. The libraries are:

- **Nebula.Core**: The core library that contains the base classes and interfaces for the framework.
- **Nebula.NICE**: The Numerical Inference & Computation Engine, which provides the core functionality for numerical operations.
- **Nebula.Data**: A library for data extraction and manipulation.
- **Nebula.ML**: A library for classic machine learning algorithms.
- **Nebula.NN**: A library for neural networks and deep learning.
- **Nebula.Sandbox**: A playground for trying out ideas and debugging.

## Developer Tools

- **Tests/**: Unit tests for each library.
- **Benchmarks/**: Benchmarkdotnet performance tracking for libraries.
- **Sandbox/**: A console application for trying out ideas and debugging.

## Getting Started
1. Clone the repository. 
   ``` 
	git clone https://github.com/ajecooper/nebula.git
	cd nebula 
   ```
2. Restore dependencies
   ```
	dotnet restore
	dotnet build
   ```
3. Run tests
   ``` dotnet test ```
4. Run Benchmarks 
   ``` dotnet run -c Release --project Benchmarks/benchmarktorun```
