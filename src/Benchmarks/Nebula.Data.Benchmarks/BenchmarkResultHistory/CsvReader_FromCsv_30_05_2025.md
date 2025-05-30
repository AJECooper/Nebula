```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.4061)
12th Gen Intel Core i7-12650H, 1 CPU, 16 logical and 10 physical cores
.NET SDK 8.0.302
  [Host]     : .NET 8.0.6 (8.0.624.26715), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.6 (8.0.624.26715), X64 RyuJIT AVX2


```
| Method                                           | RecordCount | Mean         | Error       | StdDev      | Gen0      | Gen1      | Gen2      | Allocated    |
|------------------------------------------------- |------------ |-------------:|------------:|------------:|----------:|----------:|----------:|-------------:|
| **&#39;Read from csv file and returns new data table.&#39;** | **100**         |     **126.7 μs** |     **1.26 μs** |     **1.11 μs** |    **9.2773** |    **1.2207** |         **-** |    **115.81 KB** |
| **&#39;Read from csv file and returns new data table.&#39;** | **1000**        |     **474.4 μs** |     **4.71 μs** |     **4.17 μs** |   **86.9141** |   **41.0156** |         **-** |   **1067.17 KB** |
| **&#39;Read from csv file and returns new data table.&#39;** | **10000**       |  **14,007.9 μs** |   **279.03 μs** |   **694.88 μs** | **1187.5000** | **1156.2500** |  **343.7500** |  **10782.38 KB** |
| **&#39;Read from csv file and returns new data table.&#39;** | **50000**       |  **93,795.3 μs** | **1,810.62 μs** | **2,765.01 μs** | **5500.0000** | **5166.6667** | **1500.0000** |  **53373.47 KB** |
| **&#39;Read from csv file and returns new data table.&#39;** | **100000**      | **180,640.0 μs** | **3,564.63 μs** | **5,756.21 μs** | **9666.6667** | **8666.6667** | **2000.0000** | **106737.92 KB** |
