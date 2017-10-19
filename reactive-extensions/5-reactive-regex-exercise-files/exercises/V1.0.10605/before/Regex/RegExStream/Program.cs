using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace RegExStream
{
    class Program
    {
        static void Main(string[] args)
        {
#if false
            var done = new ManualResetEvent(false);
            const string filePath = @"..\..\..\..\..\log.txt";
            var matchCount = 0;
            EnumRegExMatches(new Regex(@"^501-[^\s]+\s+[^\s]+\s+[^\s]+\s+IMPETRO\s+.*$",
                RegexOptions.Compiled | RegexOptions.Multiline),
                filePath)
                .ToObservable()
                .Finally(() => done.Set())
                .Subscribe(m =>
                               {
                                   matchCount++;
                                   Console.WriteLine(m);
                               });
            done.WaitOne();

            Console.WriteLine("matches {0}", matchCount);
#endif
        }
        private static int _bufferCount = 0;
     }
}
