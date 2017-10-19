using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading;

namespace RepeatInfinite
{
    class Program
    {
        static void Main(string[] args)
        {
            var subscribe = sequence.Subscribe(Console.WriteLine);
            Thread.Sleep(3000);
            subscribe.Dispose();
        }
    }
}
