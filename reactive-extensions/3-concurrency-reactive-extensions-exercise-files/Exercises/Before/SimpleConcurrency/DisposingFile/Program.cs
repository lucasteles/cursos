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
using System.Concurrency;
using System.IO;
using System.Linq;

#endregion

namespace DisposingFile
{
    class Program
    {
        static void Main(string[] args)
        {
            // This observable sequence is based on a file stream that
            // must be cleaned up by calling dispose on it once
            // the sequence has been processed
            var observableCharacterSequence = Observable.Using<char, StreamReader>(() =>
                // StreamReader converts the stream of bytes from "characters.txt"
                // into a stream of characters or a string depending.
                // StreamReader implements IDisposable
                new StreamReader(new FileStream("characters.txt", FileMode.Open)),
                // file is converted to string which converted to an array of
                // characters that is enumerated by query
                sr => (from c in sr.ReadToEnd().ToCharArray() select c)
                    .ToObservable(Scheduler.NewThread));
            observableCharacterSequence.Subscribe(Console.WriteLine);
        }
    }
}
