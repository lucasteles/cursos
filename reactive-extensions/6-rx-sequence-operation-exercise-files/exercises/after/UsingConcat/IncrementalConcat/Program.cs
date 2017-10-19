using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

namespace IncrementalConcat
{
    class Program
    {
        static void Main(string[] args)
        {
            const int start = 0;
            const int length = 10;
            var sequence = (from number in Enumerable.Range(start, length) select number)
                .ToObservable();
            for(var index = 2; index < 5; index++)
            {
                sequence.Concat(
                    (from number in Enumerable.Range(length * index, length) select number)
                    .ToObservable());
            }
            sequence.Subscribe(Console.WriteLine);

        }
    }
}
