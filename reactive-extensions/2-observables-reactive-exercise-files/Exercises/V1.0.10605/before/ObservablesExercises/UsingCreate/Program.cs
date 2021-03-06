﻿#region PluralsightExerciseExample
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

#endregion

namespace UsingCreate
{
    class Program 
    {
        // this returns the action you want called do dispose
        // of whatever needs disposing
        // in effect this method is mimicing one
        // from an object that implements IDisposable
        static Action MySubscribe(IObserver<int> observer)
        {
        }

        static void Main(string[] args)
        {
            var observable = Observable.Create<int>(            
#if false                
                MySubscribe
#else
                observer =>
                // this is the implementation of the IObserver.Subscribe method
                   {
                    var divisor = 3;
                    var start = 10;
                    try
                    {
                        observer.OnNext(start / divisor);
                        divisor -= 1;
                        start += 10;
                        observer.OnNext(start/divisor);
                        divisor -= 1;
                        start += 10;
                        observer.OnNext(start / divisor);
                        observer.OnCompleted();

                    }
                    catch (Exception exception)
                    {
                        observer.OnError(exception);
                        
                    }
                    return () => { };
                   }
#endif
);
            // This is the ObservableExtensions.Subscribe method.
            // It will call the IObservable.Subscribe method.
            observable.Subscribe(Console.WriteLine, Oops, Done);
        }
        static void Oops(Exception exception)
        {
            Console.WriteLine(exception.Message);
        }
        static void Done()
        {
            Console.WriteLine("Done");
        }

        #region IDisposable Members

        public void Dispose()
        {
            Console.WriteLine("I'm disposed");
        }

        #endregion
    }
}
