using System;
using System.Linq;
using System.Reactive.Linq;

namespace UsingZip
{
    class Program
    {
        static void Main(string[] args)
        {
            var sequence1 = (from number in Enumerable.Range(0, 10) select number)
                .ToObservable();
            var sequence2 = (from number in Enumerable.Range(0, 10) select number * number)
                .ToObservable();

        }
    }
}
