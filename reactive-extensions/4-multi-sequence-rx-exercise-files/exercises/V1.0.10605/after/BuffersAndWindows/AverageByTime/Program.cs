using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;

namespace AverageByTime
{
    class Program
    {
        static void Main(string[] args)
        {
            var sequence = (from number in RandomNumbers(103) select Slow(number))
                .ToObservable();
            var bufferedSequence = sequence.Buffer(TimeSpan.FromSeconds(2));
            bufferedSequence.Subscribe(list =>
                        Console.WriteLine("Size {0} Average {1}",
                        list.Count,
                        list.Sum()/Math.Max(1, list.Count)));
            Console.ReadKey();
        }

        private static readonly Random Rand = new Random();
        static IEnumerable<double> RandomNumbers(int count)
        {
            while (count > 0)
            {
                count--;
                yield return Rand.Next(1, 1000);
            }
        }
        static double Slow(double number)
        {
            Thread.Sleep(Rand.Next(1, 30) * 100);
            return number;
        }

    }
}
















#if false
        static double Slow(double number)
        {
            Thread.Sleep(Rand.Next(1, 30) * 100);
            return number;
        }


#endif






