using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForEachLoopProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            var ChunckOfValues = "12, 4, 8, 23, 15";
            foreach(var IndividualValue in ChunckOfValues.Split(
                new char[] {',', ' '},
                StringSplitOptions.RemoveEmptyEntries))
            {
                Console.WriteLine(int.Parse(IndividualValue));
            }
        }
    }
}
