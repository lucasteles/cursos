using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace RegExString
{
    class Program
    {
        static void Main(string[] args)
        {
            const string filePath = @"..\..\..\..\..\log.txt"; ;
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var streamReader = new StreamReader(fileStream, Encoding.UTF8);
            var text = streamReader.ReadToEnd();
            var regex = new Regex(
               @"^201-[^\s]+\s+[^\s]+\s+[^\s]+\s+IMPETRO\s+.*$", 
               RegexOptions.Compiled | RegexOptions.Multiline);
            var match = regex.Match(text);

            while (match.Success)
            {
                for (var index = 0; index < match.Captures.Count; index++)
                {
                    Console.WriteLine(match.Captures[index].Value);
                }
                match = match.NextMatch();
            }

        }
    }
}
