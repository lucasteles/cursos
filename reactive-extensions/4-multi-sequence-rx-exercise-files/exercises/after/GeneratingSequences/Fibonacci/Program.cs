using System;

using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;

namespace Fibonacci
{
    class Program
    {
        static void Main()
        {
            var sequence = Observable.Generate(
                new SequenceState(-1, 2),
                sequenceState => sequenceState.GetValue() < 30,
                sequenceState => sequenceState.NextState(),
                sequenceState => sequenceState.GetValue())
                .SubscribeOn(Scheduler.NewThread)
                .ObserveOn(Scheduler.NewThread);
            sequence.Subscribe(number =>
                    {
                        Program.WhatThread();
                        Console.WriteLine(number);
                    });
        }
        public static void WhatThread()
        {
            var sf = new System.Diagnostics.StackFrame(1);
            Console.WriteLine("Method: {0}  Thread: {1}",
                sf.GetMethod().Name, Thread.CurrentThread.ManagedThreadId);

        }

    }
    class SequenceState
    {
        public SequenceState(int first, int second)
        {
            Program.WhatThread();
            Last = second;
            NextToLast = first;
            _firstPass = true;
        }
        public int GetValue()
        {
            Program.WhatThread();
            return _firstPass == true ? NextToLast : Last;
        }
        public SequenceState NextState()
        {
            Program.WhatThread();

            if (_firstPass)
            {
                _firstPass = false;
                return this;
            }
            var next = Last + NextToLast;
            NextToLast = Last;
            Last = next;
            return this;
        }
        private bool _firstPass;
        private int Last { get;  set; }
        private int NextToLast { get;  set; }
    }
}
