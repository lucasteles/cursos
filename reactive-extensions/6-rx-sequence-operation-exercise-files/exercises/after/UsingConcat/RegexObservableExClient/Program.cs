using System;
using System.Text.RegularExpressions;
using RegexObservableEx;

namespace RegexObservableExClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var matchCount = 0;
            var rof = new RegexObservableFileEx(new Regex(@"^(201|560|650)-[^\s]+\s+[^\s]+\s+[^\s]+\s+IMPETRO\s+.*$",
                RegexOptions.Compiled | RegexOptions.Multiline),
                @"..\..\..\..\..\log.txt", @"..\..\..\..\..\log1.txt", @"..\..\..\..\..\log2.txt");
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
