using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

namespace SkipIndex
{
    class Program
    {
        static void Main(string[] args)
        {
            var sequence = (from number in new int[] { 4, 5, 2, 6, 3, 1, 0 } select number)
                .ToObservable();

        }
    }
}
