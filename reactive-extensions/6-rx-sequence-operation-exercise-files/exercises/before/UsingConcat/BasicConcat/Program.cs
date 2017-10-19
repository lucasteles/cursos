using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

namespace BasicConcat
{
    class Program
    {
        static void Main(string[] args)
        {
            const int start = 1;
            const int length = 10;

            var seq1 = (from number in Enumerable.Range(start, length) select number)
                .ToObservable();
            var seq2 = (from number in Enumerable.Range(start+length, length) select number)
                .ToObservable();

        }
    }
}
