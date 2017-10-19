using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

namespace UsingThrow2
{
    class Program
    {
        static void Main(string[] args)
        {
            var sequence = Observable.Throw<int>(new InvalidOperationException("bad stuff"));
            sequence
                .Subscribe(Console.WriteLine);

        }
    }
}
