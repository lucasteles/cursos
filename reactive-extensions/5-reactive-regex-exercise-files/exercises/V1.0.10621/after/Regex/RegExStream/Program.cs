using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace RegExStream
{
    class Program
    {
        static void Main(string[] args)
        {
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

        }
        private static int _bufferCount = 0;
        static IEnumerable<string> EnumRegExMatches(Regex regex, string filePath )
        {
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var streamReader = new StreamReader(fileStream, Encoding.UTF8);
            var input = streamReader.ReadToEnd();
            streamReader.Close();
            var match = regex.Match(input);
            while(match.Success)
            {
                yield return match.Value;
                    match = match.NextMatch();
            }

        }
    }
}
