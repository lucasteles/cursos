using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace LinqGroupBy
{
    class Program
    {
        static void Main(string[] args)
        {
            const string path =
            @"..\..\..\..\..\log.txt";
            var regex = new Regex(
                @"^(?<year>\d+)-[^\s]+\s+[^\s]+\s+[^\s]+\s+(?<method>[^\s]*)\s+.*$",
                RegexOptions.Compiled);

            foreach(var group in query)
            {
                Console.WriteLine("key {0}  count {1}", group.key, group.count);
            }

        }
        static IEnumerable<string> FileStrings(string path)
        {
            var file = new FileStream(path, FileMode.Open, FileAccess.Read);
            var stm = new StreamReader(file);
            string line;
            while (!string.IsNullOrEmpty(line = stm.ReadLine()))
            {
                yield return line;
            }
        }
    }
}
