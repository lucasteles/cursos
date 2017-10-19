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
               @"^401-[^\s]+\s+[^\s]+\s+[^\s]+\s+(IMPETRO|SUMMITTO)\s+.*$", 
               RegexOptions.Compiled | RegexOptions.Multiline),
               @"..\..\..\..\..\log.txt");
            
            rof.Subscribe(m => Console.WriteLine(m.Value));

        }
    }
}
