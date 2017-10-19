using System;
using System.Linq;
using System.Reactive.Linq;
using System.Text.RegularExpressions;

namespace RegexObservableEx
{
    public class RegexObservableFileEx : IObservable<Match>
    {
        private readonly Regex _regex;
        private readonly string[] _filePaths;
        public RegexObservableFileEx(Regex regex, params string[] filePaths)
        {
            _regex = regex;
            _filePaths = filePaths;

        }
        
        public IDisposable Subscribe(IObserver<Match> observer)
        {
            IObservable<Match> sequence = new RegexObservableFile(_regex, _filePaths[0]);
             return sequence.Subscribe(observer);

        }
    }

}
    