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
using System.Reactive.Linq;

#endregion

namespace PrimitiveSubscribe
{
    class Program
    {
        static void Main(string[] args)
        {
            var observable = (new int[] { 1, 2, 3 }).ToObservable();
            var myObserver = new MyObserver();
            observable.Subscribe(myObserver);
        }
    }
    class MyObserver : IObserver<int>
    {
        public void OnCompleted()
        {
            Console.WriteLine("I'm done");
        }
        public void OnError(Exception error)
        {
            Console.WriteLine("Error {0}", error.Message);
        }
        public void OnNext(int value)
        {
            Console.WriteLine(value);
        }
    }
}
