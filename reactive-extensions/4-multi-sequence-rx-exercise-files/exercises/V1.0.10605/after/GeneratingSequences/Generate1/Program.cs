using System;
using System.Linq;
using System.Reactive.Linq;

namespace GenerateCountingNumbers
{
    class Program
    {
        static void Main()
        {
            var seq = Observable.Generate(
                2, TestStateForContinuation, NextState, ValueOfState);
            seq.Subscribe(Console.WriteLine);

        }
        static bool TestStateForContinuation(int number)
        {
            return number <= 15;
        }
        static int NextState(int number)
        {
            return number + 1;
        }
        static int ValueOfState(int number)
        {
            return number;
        }
    }
}


































