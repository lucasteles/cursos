using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

namespace SkipBeginning
{
    class Program
    {
        static void Main(string[] args)
        {
            var sequence = (from number in new int[] {1, 6, 4, 9, 2, 3, 5, 8} select number)
                .ToObservable();
            sequence.Where(number => number > 4).Skip(3).Subscribe(Console.WriteLine);
        }
    }
}
