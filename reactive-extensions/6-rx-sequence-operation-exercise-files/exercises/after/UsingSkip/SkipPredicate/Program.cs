﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

namespace SkipPredicate
{
    class Program
    {
        static void Main(string[] args)
        {
            var sequence = (from number in new int[] {4, 5, 3, 6, 2, 1, 0} select number)
                .ToObservable();
            sequence.SkipWhile(value => value <= 5).Subscribe(Console.WriteLine);
        }
    }
}
