using System;
using System.Linq;
using System.Reactive.Linq;

namespace UsingThrow
{
    class Program
    {
        static void Main(string[] args)
        {
            var sequence = (from number in Enumerable.Range(0, 10) select number).ToObservable();
            sequence.Select(number =>
                {
                    if (number == 7)
                    {
                        throw new InvalidOperationException("not good");
                    }
                    return number;
                })
                    .Catch(( ex) =>
                    {
                        Console.WriteLine("exception");
                        return Observable.Empty<int>();
                    }
                    )
                    .Catch(( ex) =>
                    {
                        Console.WriteLine("invalid op");
                        return Observable.Empty<int>();
                    }
                    )
                .Subscribe(Console.WriteLine);
        }
    }
}
