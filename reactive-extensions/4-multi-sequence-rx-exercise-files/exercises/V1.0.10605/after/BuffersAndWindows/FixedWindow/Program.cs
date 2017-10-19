using System;
using System.Collections.Generic;

using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading;

namespace FixedWindow
{
    class Program
    {
        static void Main(string[] args)
        {
            var sequence = (from number in Enumerable.Range(1, 103) select number)
                .ToObservable();
            var windowedSequence = sequence.Window(10);
            windowedSequence.Subscribe(os =>
                                           {
                    os = os.SubscribeOn(Scheduler.NewThread);
                    Console.WriteLine("Window {0}", os.Count().FirstOrDefault());
                    os.Subscribe(Console.WriteLine);
                });
            Console.ReadKey();

        }
    }
}



























#if false
            windowedSequence.Subscribe(os =>
                        {
                            os.Subscribe(Console.WriteLine);
                        });

#endif
















#if false

os.Count().Subscribe(n => 
Console.WriteLine("Window {0}", n));
                                               


#endif

