using System.Threading;

namespace DevPack.Tests.ValueFactory
{
    public class Incrementer
    {
        public int Count = 0;

        public Incrementer()
        {
        }

        public Incrementer(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public void Add()
        {
            Thread.Sleep(500);

            Interlocked.Increment(ref Count);
        }
    }
}
