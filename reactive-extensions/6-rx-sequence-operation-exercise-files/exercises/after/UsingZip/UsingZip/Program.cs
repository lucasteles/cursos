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
            var zippedSequence = sequence1.Zip(sequence2, (left, right) => new {left, right});
            zippedSequence.Subscribe(zip =>
                Console.WriteLine("Number {0}  Square {1}", zip.left, zip.right));

        }
    }
}
