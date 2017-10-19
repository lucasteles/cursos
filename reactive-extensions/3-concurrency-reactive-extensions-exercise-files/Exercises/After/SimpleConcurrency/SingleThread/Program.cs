#region PluralsightExerciseExample
// Copyright Pluralsight LLC 2011
// 
// This example is for the exclusive use of subscribers to a Pluralsight On Demand!
// subscription that includes example code.
// 
// Please do not post these examples or pass them around.
// 
// This code is meant to be used for training purposes only.
// 
// No representations are made as to its usefullness or correctness.
// 
// This code may not be used in any applications or production code.
// 
// Thank You!
#endregion

#region

using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;

#endregion

namespace SingleThread
{
    class Program
    {
        static void Main()
        {
            // write out thread application is running on
            Console.WriteLine("Application\tThread: {0}", Thread.CurrentThread.ManagedThreadId);
            // make a sequence of numbers
            var numbers = from number in new int[] { 1, 2, 3 } select ProcessNumber(number);
            var observableNumbers = numbers.ToObservable();
            // subscribe and run callbacks
            observableNumbers.Subscribe(Output, Oops, ImDone);

        }
        // methods to use as delegate for processing observableNumbers
        // and keeping track of thread they were run on

        // processes value by writing it along with thread id
        static void Output(int number)
        {
            Console.WriteLine("  Value: {0}\tThread: {1}", number,
                Thread.CurrentThread.ManagedThreadId);
        }
        // process error by writing message and thread id
        static void Oops(Exception exception)
        {
            Console.WriteLine("Message: {0}\tThread: {1}",
                exception.Message, Thread.CurrentThread.ManagedThreadId);
        }
        // processes completion by writing thread id 
        static void ImDone()
        {
            Console.WriteLine("I'm all done on Thread\t{0}", Thread.CurrentThread.ManagedThreadId);
        }
        // method used to simulate work
        static int ProcessNumber(int number)
        {
            Console.WriteLine("Processing on Thread\t{0}", Thread.CurrentThread.ManagedThreadId);
            return number;
        }

    }
}
