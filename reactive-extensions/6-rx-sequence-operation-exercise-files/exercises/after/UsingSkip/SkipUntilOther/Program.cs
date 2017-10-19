using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

namespace SkipUntilOther
{
    class Program
    {
        static void Main(string[] args)
        {
            var firstSequence = (from number in new int[] { 4, 5, 3, 6, 2, 1, 0 }
                 select number)
                 .ToObservable();

            var secondSequence = (from number in Enumerable.Range(0, 10)
                                 select number).ToObservable().Skip(3)
                                 .Do(number => Console.WriteLine("Do {0}", number))
                                 .Finally(() => Console.WriteLine("second finally"));

            var sequence = firstSequence.SkipUntil(secondSequence)
                .Subscribe(Console.WriteLine);
        }
    }
}
