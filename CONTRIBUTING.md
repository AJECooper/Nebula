# Contributing to Nebula

Welcome to Nebula, The NICE Engine Behind Universal Learning Algorithms! We appreciate your interest in contributing to our project. This document outlines the guidelines for contributing to Nebula, including how to report issues, submit code changes, and adhere to our coding standards.

Nebula is a **.NET monorepo** contain multiple libraries that are independently versioned and can be used separately or together.

All contributions are greatly appreciated and highly valued, from code, bug reports, benchmarks, documentation and everything inbetween!

## Table of Contents

- [Code of Conduct](#code-of-conduct)
- [Getting Started](#getting-started)
- [Repository Structure](#repository-structure)
- [How to Contribute](#how-to-contribute)
- [Pull Request Guidelines](#pull-request-guidelines)
- [Testing & Validation](#testing--validation)

## Code of Conduct

Please be kind, respectful and helpful to all community members. Harrassment and discrimination of any kind will not be tolerated.

## Getting Started

1. **Fork** the repository and clone it locally.
2. Install .NET SDK 8.0+ 
3. Run `dotnet restore` at root.
4. Open `Nebula.sln` in your IDE of choice
5. User the `sandbox/` project to experiment with the libraries and test your changes (set this as startup project).

## Repository Structure

Nebula is organized into several modular libraries, each with its own purpose.
Below is a summary of each library found within Nebula:

| Project | Description |
| --- | --- |
| **Nebula.Core** | The core library that contains the base classes and interfaces for the framework. |
| **Nebula.NICE** | The Numerical Inference & Computation Engine, which provides the core functionality for numerical operations. |
| **Nebula.Data** | A library for data extraction and manipulation. |
| **Nebula.ML** | A library for classic machine learning algorithms. |
| **Nebula.NN** | A library for neural networks and deep learning. |
| **Nebula.Sandbox** | A playground for trying out ideas and debugging. |

### Future Libraries
| Project | Description |
| --- | --- |
| **Nebula.Autograd** | A library for automatic differentiation. |
| **Nebula.Deploy** | A library for exporting and deploying models. |
| **Nebula.Plot** | A library for plotting and data visualization. |
| **Nebula.Text** | A library for natural language processing. |
| **Nebula.Vision** | A library for computer vision. |
| **Nebula.Reinforcement** | A library for reinforcement learning. |

## How to Contribute

### Bug Reports & Feature Requests

- Please use the GitHub issue tracker to report bugs or request features.
- Please use the labels `bug`, `feature`, `question`, or `help wanted` to categorize your issue. You can do this by going to **[Issue Tracker](../../issues)**, clearing the input field, select `label` and selecting the relevant option.

### Code Contributions

- Pick an open issue or suggest a new feature.
- Write your code, create tests and benchmarks. Ensure that all tests pass and there is no degredation to existing benchmarks.
- Raise a pull request against the `master` branch.

## Pull Request Guidelines

- Merge to `Master` unless otherwise specified.
- Ensure that your code passes all tests and benchmarks.
- Write clear and concise commit messages.
- Ensure that there is no degradation to existing benchmarks.
- Add new benchmarks for any new features or changes.
- Ensure that pull requests are small and focused. Large pull requests are harder to review and may be rejected.

**PR Checklist**
- [ ] Code compiles
- [ ] Tests pass (run `dotnet test` to verify)
- [ ] Benchmarks pass and are unaffected or improved (run `dotnet run -c Release --project Benchmarks/benchmarktorun` to verify)
- [ ] Benchmark results are included in the pull request
- [ ] Documentation is updated (if required)

## Testing & Validation

- Each library has its own set of unit tests. Please ensure you new tests or modify existing tests to support your changes.
- For consistency, please write all tests in `xUnit` and use the `FluentAssertions` library for assertions.
