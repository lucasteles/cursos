using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

namespace AdHocEnumMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            var observable = MyAdHoSequence().ToObservable();
            observable.Subscribe(Console.WriteLine);
        }
        static IEnumerable<int> MyAdHoSequence()
        {
            yield return 1;
            yield return 2;
            yield return 3;
        }
    }
}
