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
        }
    }
}
