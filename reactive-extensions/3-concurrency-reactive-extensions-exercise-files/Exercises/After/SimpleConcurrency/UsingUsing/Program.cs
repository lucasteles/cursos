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
using System.Reactive.Concurrency;
using System.Reactive.Linq;

#endregion

namespace UsingUsing
{
    // Class that implments dispose 
    // used to show basic mechanics of Using
    class MyDispose : IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("I've been disposed");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var query = from number in Enumerable.Range(1, 10) select number;
            // observable sequence run on a new thread
            var observableQuery = query.ToObservable(Scheduler.NewThread);
            // observable sequence that adds disposable object to the
            // objects that Rx will clean up when when it is finish processing
            // sequence
            var observableWithDispose = Observable.Using<int, MyDispose>(
                // object that implements IDisposable, that Rx will Dispose on 
                // once the sequence has been processed
                () => new MyDispose(),
                // observable sequence this disposable object is being 
                // attached to. Notice that the disp param isn't used in this example
                disp => observableQuery);
            // check console output and you will see that MyDispose.Dispose
            // is called after the sequence has been processed
            observableWithDispose.Subscribe(Console.WriteLine); 

        }
    }
}
