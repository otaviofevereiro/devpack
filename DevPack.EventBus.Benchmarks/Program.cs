using BenchmarkDotNet.Running;
using System;

namespace DevPack.EventBus.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<EventBusBenchmark>();
        }
    }
}
