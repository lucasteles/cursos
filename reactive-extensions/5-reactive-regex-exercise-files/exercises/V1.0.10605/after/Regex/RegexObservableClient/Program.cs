using System;
using System.Text.RegularExpressions;
using RegexObservable;

namespace RegexObservableClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var matchCount = 0;
            var rof = new RegexObservableFile( new Regex(
               @"^401-[^\s]+\s+[^\s]+\s+[^\s]+\s+IMPETRO\s+.*$", 
               RegexOptions.Compiled | RegexOptions.Multiline),
               @"..\..\..\..\..\log.txt");
            var d = rof.Subscribe(m =>
                {
                    matchCount++;
                    Console.WriteLine(m.Value);
                }
            );
            Console.WriteLine(matchCount);

        }
    }
}
