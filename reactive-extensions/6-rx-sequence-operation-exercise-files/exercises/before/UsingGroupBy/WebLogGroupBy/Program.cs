using System;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using RegexObservableEx;

namespace WebLogGroupBy
{
    class Program
    {
        static void Main(string[] args)
        {
            var paths =
            new string[] {    
            @"..\..\..\..\..\log.txt",
            @"..\..\..\..\..\log1.txt",
            @"..\..\..\..\..\log2.txt",
            };
            var seq = new RegexObservableFileEx(
                new Regex(@"^(?<year>\d+)-[^\s]+\s+[^\s]+\s+[^\s]+\s+(?<method>[^\s]*)\s+.*$",
                        RegexOptions.Compiled | RegexOptions.Multiline), paths)
                 .Subscribe(c => c.Count()
                    .Subscribe(v => Console.WriteLine("Key {0}  Count {1}", c.Key, v)));
        }
    }
}
