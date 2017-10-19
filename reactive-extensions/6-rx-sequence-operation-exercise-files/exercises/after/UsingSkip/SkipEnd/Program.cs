using System;
using System.Linq;
using System.Reactive.Linq;

namespace SkipEnd
{
    class Program
    {
        static void Main(string[] args)
        {
            var sequence = (from number in new int[] { 4, 5, 3, 6, 2, 1, 0 } 
                select Work(number))
                .ToObservable();
            sequence.SkipLast(3).Subscribe(Console.WriteLine);

        }
        static int Work(int number)
        {
            Console.WriteLine("Work {0}", number);
            return number;
        }
    }
}
