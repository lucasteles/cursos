using System;
using System.Linq;
using System.Reactive.Linq;

namespace ZipDifferentLengths
{
    class Program
    {
        static void Main(string[] args)
        {
            var sequence1 = Observable.Repeat(10);
            var sequence2 = (from number in Enumerable.Range(0, 10) select number)
                .ToObservable();
            var zippedSequence = sequence1.Zip(sequence2, (left, right) => new { left, right });
            zippedSequence.Subscribe(
                zip => Console.WriteLine("Number {0}  Product {1} ",
                       zip.right, zip.left * zip.right));
        }
    }
}
