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
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;

#endregion

namespace UsingSynchronize
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            var xs = Observable.Create<string>(observer =>
            {
                Console.WriteLine("{0} Side effect1", Thread.CurrentThread.ManagedThreadId);
                observer.OnNext("Hi");
                observer.OnNext("There");
                Console.WriteLine("{0} Side effect2", Thread.CurrentThread.ManagedThreadId);
                observer.OnCompleted();
                return Disposable.Empty;
            });
            var obj = new object();
            
            xs.SubscribeOn(Scheduler.NewThread).ObserveOn(Scheduler.NewThread).
            Take(3)
            .Subscribe(
                a =>
                Console.WriteLine("t: {0}   value: {1}", Thread.CurrentThread.ManagedThreadId,
                a));
#if false
            var numbers = from number in Enumerable.Range(1, 10000) select number;
            var oddsNEvens = new OddsNEvens();
            var observableNumbers1 = numbers.ToObservable(Scheduler.NewThread).Synchronize();
            var observableNumbers2 = numbers.ToObservable(Scheduler.NewThread).Synchronize();
            observableNumbers1.Subscribe(oddsNEvens.Check, () => Console.WriteLine(oddsNEvens));
            observableNumbers2.Subscribe(oddsNEvens.Check, () => Console.WriteLine(oddsNEvens));
#endif
        }
    }
    class OddsNEvens
    {
        public void Check(int number)
        {
            if((number % 2) == 0)
            {
                EvenCount += 1;
            }
            else
            {
                OddCount += 1;
            }
            
        }

        public int OddCount { get; private set; }

        public int EvenCount { get; private set; }
        public override string ToString()
        {
            return String.Format("Odds {0}  Evens {1}", OddCount, EvenCount);
        }
    }
}
