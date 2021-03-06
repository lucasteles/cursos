﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Reactive.Linq;

namespace UsingEmpty
{
    class Program
    {
        static void Main(string[] args)
        {
            var sequence = Observable.Empty<string>();
            sequence.Subscribe(number => Console.WriteLine("OnNext {0}", number),
                               (Exception ex) => Console.WriteLine("OnError {0}", ex.Message),
                               () => Console.WriteLine("OnCompleted"));
            Console.WriteLine("Application Done");

        }
    }
}
