using System;
using System.Linq;
using System.Reactive.Linq;

namespace FixedBuffer
{
    class Program
    {
        static void Main(string[] args)
        {
            var sequence = (from number in Enumerable.Range(1, 103) select number)
                .ToObservable();
            var bufferedSequence = sequence.Buffer(10);
            bufferedSequence.Subscribe(buffer =>
                    {
                        Console.WriteLine("Buffer size {0}", buffer.Count);
                        foreach (var i in buffer)
                        {
                            Console.WriteLine("  {0}", i);
                        }
                    });
        }
    }
}












#if false
                {
                    Console.WriteLine("Buffer size {0}", buffer.Count);
                    foreach (var i in buffer)
                    {
                        Console.WriteLine("  {0}", i);
                    }
                }

#endif