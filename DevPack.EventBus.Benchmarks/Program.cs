using BenchmarkDotNet.Running;

namespace DevPack.Observer.Tests.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<NotifierBenchmark>();
        }
    }
}
