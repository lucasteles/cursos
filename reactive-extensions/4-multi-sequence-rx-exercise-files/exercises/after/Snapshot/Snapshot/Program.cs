using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;

namespace Snapshot
{
    class Program
    {
        static void Main(string[] args)
        {
            // sequence this produces depends on current time
            var query = from number in Enumerable.Range(1, 3) select DateTimeOffset.UtcNow + new TimeSpan(0, 0, number);
            var sequence = query.ToObservable();
            sequence.Subscribe(d => Console.WriteLine(d.ToString()));
            // wait a bit
            Thread.Sleep(2000);
            // subscribe again and you get a different sequence
            Console.WriteLine("----");
            sequence.Subscribe(d => Console.WriteLine(d.ToString()));
            // to make a snap show you convert the to a new observable sequence
            // with just a single value in it, an array of values,
            // then convert that array into an observable sequence
            Console.WriteLine("Snapshot");
            var snapshot = sequence.ToArray().First().ToObservable();
            snapshot.Subscribe<DateTimeOffset>(n => Console.WriteLine(n.ToString()));
            // wait a bit and you still get the same sequence
            Thread.Sleep(2000);
            Console.WriteLine("----");
            snapshot.Subscribe<DateTimeOffset>(n => Console.WriteLine(n.ToString()));


        }
    }
}
