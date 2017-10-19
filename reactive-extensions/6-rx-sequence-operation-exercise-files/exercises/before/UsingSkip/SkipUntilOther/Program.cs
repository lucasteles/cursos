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


            var sequence = firstSequence.SkipUntil(secondSequence)
                .Subscribe(Console.WriteLine);
        }
    }
}
