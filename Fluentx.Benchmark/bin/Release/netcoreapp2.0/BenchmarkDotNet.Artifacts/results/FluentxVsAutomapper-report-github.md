``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.16299.371 (1709/FallCreatorsUpdate/Redstone3)
Intel Core i7-8550U CPU 1.80GHz (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
Frequency=1945311 Hz, Resolution=514.0566 ns, Timer=TSC
.NET Core SDK=2.1.104
  [Host]     : .NET Core 2.0.6 (CoreCLR 4.6.26212.01, CoreFX 4.6.26212.01), 64bit RyuJIT  [AttachedDebugger]
  DefaultJob : .NET Core 2.0.6 (CoreCLR 4.6.26212.01, CoreFX 4.6.26212.01), 64bit RyuJIT


```
|         Method |        Mean |      Error |     StdDev |
|--------------- |------------:|-----------:|-----------:|
| FluentxMapper1 | 21,185.2 ns | 415.975 ns | 635.238 ns |
|    Automapper1 |    238.1 ns |   4.622 ns |   5.138 ns |
