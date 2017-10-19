using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

namespace WindowWithSkip
{
    class Program
    {
        static void Main(string[] args)
        {
            var sequence = (from number in Enumerable.Range(1, 103) select number).ToObservable();
            var windowedSequence = sequence.Window(10);
            windowedSequence.Subscribe(os =>
                    {
                        Console.WriteLine("Window");
                        os.Subscribe(Console.WriteLine);
                    }
                );
        }
    }
}
