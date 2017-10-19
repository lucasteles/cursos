using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

namespace UsingReturn
{
    class Program
    {
        static void Main(string[] args)
        {
            var sequence = Observable.Return("some string");
            Console.WriteLine("Application Done");

        }
    }

}
