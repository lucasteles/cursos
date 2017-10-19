using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace ODataObservable
{
    // TEntry is the type of the value in the OData sequence
    public class DataSequence<TEntry> : IObservable<TEntry>
    {
        // keep track of query and position of first value
        public DataSequence(IQueryable<TEntry> query, int position = 0)
        {
            _query = query;
            _position = position;
        }
        private readonly int _position;
        private readonly IQueryable<TEntry> _query;

        // state of sequence
        // odata is page oriented and this class is used
        // to keep track of each page
        private class State : IDisposable
        {
            // current position in the sequence
            public int Position { get; set; }
            // current position at beginning of state
            public int StartingPosition { get; set; }
            // checks if previous pass found any values
            private bool _canIContinue;

            public bool CanIContinue
            {
                get
                {
                    return _canIContinue;
                }
                set { _canIContinue = value; }
            }

            // shuts down generation of values
            public void Dispose()
            {
                CanIContinue = false;
            }
        }

        public IDisposable Subscribe(IObserver<TEntry> observer)
        {
            // initial state for subscription
            
            var initialState = new State
                                   {
                                       CanIContinue = true,
                                       Position = _position,
                                       StartingPosition = -1
                                   };
            var seq = Observable.Generate(
                initialState
                // checks to see if generation should proceed
                , state => state.CanIContinue
                // moves to next state in sequence
                , MoveToNextState
                // gets current value, which is an observable sequence of values
                , GetCurrentValue,
                Scheduler.ThreadPool)
                // merges observable sequences of values into
                // a sequence of values
                .Merge();
            // when subscription is disposed of the state must be disposed too
            return new CompositeDisposable(initialState, seq.Subscribe(observer));
        }


        private static State MoveToNextState(State state)
        {
            // if the last pass didn't produce any values we are done
            state.CanIContinue = state.CanIContinue && (state.StartingPosition != state.Position);
            // remember where we started for the next pass
            state.StartingPosition = state.Position;
            return state;
        }

        private IObservable<TEntry> GetCurrentValue(State state)
        {
            return _query.Skip(state.Position).ToObservable().Do(e => state.Position++);
        }
    }

}   
    
    
    
