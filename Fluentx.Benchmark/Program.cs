using BenchmarkDotNet.Running;
using System;

namespace Fluentx.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            //Validate Fluentx
            var instance = new FluentxVsAutomapper();
            //var result1 = instance.FluentxMapper();
            //var result2 = instance.Automapper();
            //var summary = BenchmarkRunner.Run<FluentxVsAutomapper>();

            var result1 = instance.FluentxMapper1();
        }
    }
}
