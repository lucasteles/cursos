using System;
using System.Collections.Generic;
using System.Reactive.Linq;
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
#if false
            while(match.Success)
            {
                Console.WriteLine(match.Value);
                match = match.NextMatch();
            }
#endif
            var sequence = RegexMatchToEnum(regex, input).ToObservable();
            sequence.Subscribe(m => Console.WriteLine(m.Value));

        }
        static IEnumerable<Match> RegexMatchToEnum(Regex regex, string input)
        {
            var match = regex.Match(input);
            while(match.Success)
            {
                yield return match;
                match = match.NextMatch();
            }
        }
    }
}
