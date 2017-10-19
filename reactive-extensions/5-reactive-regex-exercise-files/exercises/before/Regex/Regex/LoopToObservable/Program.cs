using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LoopToObservable
{
    class Program
    {
        static void Main(string[] args)
        {
            const string input = "xyz  abc   ysr   abcd   lgb12  abc  trvs  abc9 r  r98";
            var regex = new Regex(@"abc[^ ][^\s]*");
            var match = regex.Match(input);
#if true
            while(match.Success)
            {
                Console.WriteLine(match.Value);
                match = match.NextMatch();
            }
#endif
            var sequence = RegexMatchToEnum(regex, input).ToObservable();
            sequence.Subscribe(m => Console.WriteLine(m.Value));

        }
    }
}
