```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.4061)
12th Gen Intel Core i7-12650H, 1 CPU, 16 logical and 10 physical cores
.NET SDK 8.0.302
  [Host]     : .NET 8.0.6 (8.0.624.26715), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.6 (8.0.624.26715), X64 RyuJIT AVX2


```
| Method                                               | SampleCount | FeatureCount | Mean          | Error         | StdDev        | Allocated |
|----------------------------------------------------- |------------ |------------- |--------------:|--------------:|--------------:|----------:|
| **&#39;Trains the Linear Regression model over 100 epochs&#39;** | **100**         | **2**            |      **72.47 μs** |      **0.598 μs** |      **0.530 μs** |     **120 B** |
| **&#39;Trains the Linear Regression model over 100 epochs&#39;** | **100**         | **5**            |      **88.27 μs** |      **1.179 μs** |      **1.103 μs** |     **144 B** |
| **&#39;Trains the Linear Regression model over 100 epochs&#39;** | **100**         | **20**           |     **203.05 μs** |      **1.026 μs** |      **0.960 μs** |     **264 B** |
| **&#39;Trains the Linear Regression model over 100 epochs&#39;** | **100**         | **50**           |     **571.22 μs** |      **8.125 μs** |      **7.600 μs** |     **504 B** |
| **&#39;Trains the Linear Regression model over 100 epochs&#39;** | **1000**        | **2**            |     **708.97 μs** |      **1.031 μs** |      **0.861 μs** |     **120 B** |
| **&#39;Trains the Linear Regression model over 100 epochs&#39;** | **1000**        | **5**            |     **847.53 μs** |      **2.799 μs** |      **2.481 μs** |     **144 B** |
| **&#39;Trains the Linear Regression model over 100 epochs&#39;** | **1000**        | **20**           |   **1,986.78 μs** |     **13.158 μs** |     **12.308 μs** |     **266 B** |
| **&#39;Trains the Linear Regression model over 100 epochs&#39;** | **1000**        | **50**           |   **5,626.94 μs** |     **77.204 μs** |     **72.216 μs** |     **507 B** |
| **&#39;Trains the Linear Regression model over 100 epochs&#39;** | **10000**       | **2**            |   **7,178.96 μs** |     **32.191 μs** |     **30.112 μs** |     **123 B** |
| **&#39;Trains the Linear Regression model over 100 epochs&#39;** | **10000**       | **5**            |   **8,438.33 μs** |     **35.497 μs** |     **33.204 μs** |     **150 B** |
| **&#39;Trains the Linear Regression model over 100 epochs&#39;** | **10000**       | **20**           |  **20,039.28 μs** |    **277.385 μs** |    **259.466 μs** |     **276 B** |
| **&#39;Trains the Linear Regression model over 100 epochs&#39;** | **10000**       | **50**           |  **57,204.39 μs** |    **954.962 μs** |    **893.272 μs** |     **554 B** |
| **&#39;Trains the Linear Regression model over 100 epochs&#39;** | **50000**       | **2**            |  **36,989.20 μs** |    **716.163 μs** |  **1,156.470 μs** |     **147 B** |
| **&#39;Trains the Linear Regression model over 100 epochs&#39;** | **50000**       | **5**            |  **44,746.97 μs** |    **477.684 μs** |    **446.826 μs** |     **180 B** |
| **&#39;Trains the Linear Regression model over 100 epochs&#39;** | **50000**       | **20**           | **102,979.11 μs** |  **1,191.476 μs** |  **1,056.213 μs** |     **344 B** |
| **&#39;Trains the Linear Regression model over 100 epochs&#39;** | **50000**       | **50**           | **294,197.06 μs** |  **3,230.093 μs** |  **7,862.493 μs** |     **904 B** |
| **&#39;Trains the Linear Regression model over 100 epochs&#39;** | **100000**      | **2**            |  **72,440.65 μs** |    **597.359 μs** |    **466.379 μs** |     **177 B** |
| **&#39;Trains the Linear Regression model over 100 epochs&#39;** | **100000**      | **5**            |  **86,377.20 μs** |    **467.844 μs** |    **414.732 μs** |     **211 B** |
| **&#39;Trains the Linear Regression model over 100 epochs&#39;** | **100000**      | **20**           | **209,274.07 μs** |  **1,828.147 μs** |  **2,440.524 μs** |     **397 B** |
| **&#39;Trains the Linear Regression model over 100 epochs&#39;** | **100000**      | **50**           | **720,270.45 μs** | **14,177.706 μs** | **12,568.167 μs** |     **904 B** |
