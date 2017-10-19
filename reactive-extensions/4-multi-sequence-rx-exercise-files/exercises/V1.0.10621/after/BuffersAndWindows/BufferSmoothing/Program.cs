using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace BufferSmoothing
{
    class Program
    {
        static void Main(string[] args)
        {
            var sequence = (from number in Enumerable.Range(1, 103) select number)
                .ToObservable();
            var bufferedSequence = sequence.Buffer(3);
            bufferedSequence.Subscribe(list => 
                Console.WriteLine(list.Sum()/list.Count));
        }

    }
}



















#if false
        private static readonly Random Rand = new Random();
        static IEnumerable<double> RandomNumbers(int count)
        {
            while(count > 0)
            {
                count--;
                yield return Rand.Next(1, 1000);
            }
        }


#endif