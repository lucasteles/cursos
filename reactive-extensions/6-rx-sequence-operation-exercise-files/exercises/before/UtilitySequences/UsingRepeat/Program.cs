﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

namespace UsingRepeat
{
    class Program
    {
        static void Main(string[] args)
        {
            var sequence = Observable.Repeat(Observable.Return(100), 4);
            Console.WriteLine("Application Done");

        }
    }
}
