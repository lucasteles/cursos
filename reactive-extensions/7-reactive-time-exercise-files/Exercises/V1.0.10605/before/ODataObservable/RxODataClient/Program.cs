using System;
using System.Linq;
using System.Threading;
using ODataObservable;
using RxODataClient.Netflix;

namespace RxODataClient
{
    class Program
    {
        static void Main()
        {
            // make LINQ provder for Netflix OData endpoint
 
            // make observable sequence out of a query

            // keep track of the number of titles we get back
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

            // show how many titles we ended up with

        }
    }
}
