using System;
using System.Disposables;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexObservable
{
    public class RegexObservableFile : IObservable<Match>
    {
        private readonly Regex _regex;
        private readonly string _filePath;
        public RegexObservableFile(Regex regex, string filePath)
        {
            _regex = regex;
            _filePath = filePath;

        }
        private class State : IDisposable
        {
            internal bool Done = false;
            internal const int BufferOverlap = 1000;
            internal const int BufferLimit = 1000000;
            internal readonly char[] Buffer;
            internal string Input;
            internal int CharacterCount;
            internal int LastMatchPosition;
            internal Match Match;
            internal readonly StreamReader StreamReader;

            internal State(StreamReader streamReader)
            {
                Buffer = new char[BufferLimit];
                StreamReader = streamReader;
                CharacterCount = 0;
                LastMatchPosition = 0;
            }
        
            public void  Dispose()
            {
                Done = true;
                StreamReader.Dispose();
            }

        }

        private State MoveToNextState(State state)
        {

            return state;
        }
        private static Match GetValue(State state)
        {
            state.LastMatchPosition = state.Match.Index;
            return state.Match;
        }
        private bool OkToContinue(State state)
        {
            if(!state.Done && state.Input == null)
            {
            }
            return state.Match.Success;
        }
        private void AdvanceToNextMatch(State state)
        {
            while(!state.Match.Success)
            {
            }
            return;
        }
        private bool AdvanceToNextBuffer(State state)
        {
            // shift buffer
            var read = state.StreamReader
                .Read(state.Buffer, State.BufferOverlap,
                State.BufferLimit - State.BufferOverlap);
            if (read == 0) return false;
            state.CharacterCount = State.BufferOverlap + read;
            state.Input = new string(state.Buffer, 0, state.CharacterCount);
            state.Match = _regex.Match(state.Input, 1 + state.LastMatchPosition);
            return true;

        }
        public IDisposable Subscribe(IObserver<Match> observer)
        {
            var fileStream = new FileStream(_filePath, FileMode.Open, FileAccess.Read);
            var state = new State(new StreamReader(fileStream, Encoding.UTF8));
            var disposable = new CompositeDisposable(state,
                Observable.Generate(state, OkToContinue, MoveToNextState, GetValue)
                .Subscribe(observer));
            return disposable;

        }

    }

}
