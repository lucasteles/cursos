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
using System.Threading;

#endregion

namespace WorkInSubscription
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Application thread {0}", Thread.CurrentThread.ManagedThreadId);
            //--------------------------------This AddOne will be part of the scheduler delegate
            var q = from number in Enumerable.Range(1, 3) select SubscriptionAddOne(number);
            var oq = q.ToObservable().SubscribeOn(Scheduler.NewThread).ObserveOn(Scheduler.NewThread);
            oq.Subscribe(number => Console.WriteLine("Observation Thread {0} Value {1}", 
                Thread.CurrentThread.ManagedThreadId,
                //This AddOne will be part of an observer delegate
                ObservationAddOne(number)));
            //Check the output and you will see that ObservationAddOne is always run on the 
            //same thread as the WriteLine is.
            //The SubscriptionAddOne is run on a different thread, the thread 
            //the subscription delegate is run on.
        }

        static int ObservationAddOne(int number)
        {
            // check the output and you will see AddOne is invoked twice for
            // each value, each time on a different thread
            Console.WriteLine("Observation AddOne {0}", Thread.CurrentThread.ManagedThreadId);
            return number+1;
        }
        static int SubscriptionAddOne(int number)
        {
            // check the output and you will see AddOne is invoked twice for
            // each value, each time on a different thread
            Console.WriteLine("Subscription AddOn {0}", Thread.CurrentThread.ManagedThreadId);
            return number + 1;
        }
    }

}
