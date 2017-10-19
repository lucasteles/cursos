using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace UsingTimer
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Starting");
            var period = TimeSpan.FromSeconds(.5);
            var done = new ManualResetEvent(false);

            sequence.Subscribe(t => Console.WriteLine("Step {0}  Elapsed {1} seconds", t, t*period.TotalSeconds));
            done.WaitOne();
            Console.WriteLine("Finished");
        }
    }
}
