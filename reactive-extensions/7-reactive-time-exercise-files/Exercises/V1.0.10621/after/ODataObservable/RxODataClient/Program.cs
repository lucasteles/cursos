using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using ODataObservable;
using RxODataClient.Netflix;

namespace RxODataClient
{
    class Program
    {
        static void Main()
        {
            var done = new ManualResetEvent(false);
            // make LINQ provder for Netflix OData endpoint
            var netflix = new NetflixCatalog(new Uri("http://odata.netflix.com/Catalog"));
            // make observable sequence out of a query
            var sequence = new DataSequence<Title>(
                 from title in netflix.Titles select title
            );
            // keep track of the number of titles we get back
            var titleCount = 0;
            sequence.Take(600)
                .Finally(
                // cleanup sets the done event
                () => done.Set())
                // subscribe increments the title count and output the title
                .Subscribe(title =>
                {
                    titleCount++;
                    Console.WriteLine(title.Name);
                });
            // wait for end of sequence to be processed
            done.WaitOne();
            // show how many titles we ended up with
            Console.WriteLine("==============\n{0}", titleCount);
        }
    }
}
