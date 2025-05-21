| Method                                           | RecordCount | Mean         | Error      | StdDev     | Gen0      | Gen1      | Gen2     | Allocated   |
|------------------------------------------------- |------------ |-------------:|-----------:|-----------:|----------:|----------:|---------:|------------:|
| 'Read from csv file and returns new data table.' | 100         |     90.18 us |   1.207 us |   1.070 us |    1.9531 |    0.1221 |        - |    25.02 KB |
| 'Read from csv file and returns new data table.' | 1000        |    120.88 us |   1.765 us |   1.474 us |   11.2305 |    2.6855 |        - |   138.78 KB |
| 'Read from csv file and returns new data table.' | 10000       |  1,386.54 us |  27.654 us |  63.539 us |  142.5781 |  140.6250 |  41.0156 |  1380.37 KB |
| 'Read from csv file and returns new data table.' | 50000       |  6,345.50 us | 126.790 us | 325.010 us |  656.2500 |  640.6250 | 359.3750 |  6591.02 KB |
| 'Read from csv file and returns new data table.' | 100000      | 16,448.12 us | 326.805 us | 479.027 us | 1406.2500 | 1343.7500 | 687.5000 | 13165.87 KB |