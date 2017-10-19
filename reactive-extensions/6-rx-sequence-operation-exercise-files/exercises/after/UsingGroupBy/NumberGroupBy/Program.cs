using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

namespace NumberGroupBy
{
    class Program
    {
        static void Main(string[] args)
        {
            var sequence = (from number in Enumerable.Range(0, 100) select number)
                .ToObservable();
            sequence.GroupBy(number => number % 2)
                .Subscribe(group => 
                Console.WriteLine("Remainder {0}  Count {1}", group.Key, group.Count().First()));
        }
    }
}
