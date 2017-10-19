using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Windows;
using ODataObservable;
using WPFTimer.Netflix;

namespace WPFTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly NetflixCatalog _netflix;
        private IDisposable _runningQuery;
        private DateTimeOffset _queryStartTime;
        public MainWindow()
        {
            InitializeComponent();
            _netflix = new NetflixCatalog(new Uri("http://odata.netflix.com/Catalog"));
        }

        private IDisposable _timerDispose;
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            _timerDispose = Observable.Timer(DateTimeOffset.Now, TimeSpan.FromSeconds(1))
                .SubscribeOn(Scheduler.ThreadPool)
                .ObserveOnDispatcher()
                .Subscribe(n => Elapsed.Text = n.ToString());
            Results.Items.Clear();
            _queryStartTime = DateTimeOffset.Now;
            IQueryable<Netflix.Title> titlesQuery;
            if (string.IsNullOrWhiteSpace(ReleaseYear.Text))
            {
                titlesQuery = from title in _netflix.Titles select title;
            }
            else
            {
                var releaseYear = int.Parse(ReleaseYear.Text);
                titlesQuery = from title in _netflix.Titles
                              where title.ReleaseYear == releaseYear
                              select title;
            }
            Start.IsEnabled = false;
            Stop.IsEnabled = true;
            // make a timestamped observable sequence of the titles
            // returned by the netflix query
            var titleSequence = new DataSequence<Title>(titlesQuery, 0)
                .Timestamp();
            // calculate the period needed to get the rate the user wants
            var rateLimit = 1.0 / double.Parse(Rate.Text);
            // make a timer sequence that runs at that rate
            var timerSequence = Observable.Timer(DateTimeOffset.Now, TimeSpan.FromSeconds(rateLimit));
            // zip together the timerSequence and the title sequence
            // titles will not be produced at a rate any greater than
            // then the timer sequence produces values
            // notice that only results from the title sequence are returned
            var rateLimitedSequence = timerSequence.Zip(titleSequence, (l, r) => r);
            // subscribe to the rateLimitedSequence
            _runningQuery = rateLimitedSequence
                // observe on dispatch so that the gui app remains response
                .ObserveOnDispatcher()
                // clean up when done
                .Finally(Cleanup)
                // update the gui for each result produced
                .Subscribe(UpdateResultsListbox);
        }

        private void UpdateResultsListbox(Timestamped<Title> timestamp)
        {
            var item = Results.Items
                .Add(string.Format("{0} {1} @ {2}", timestamp.Value.ShortName, timestamp.Value.TinyUrl,
                                   timestamp.Timestamp));
            Results.Items.MoveCurrentToLast();
            Results.ScrollIntoView(Results.Items[item]);
        }

        void Cleanup()
        {
            Start.IsEnabled = true;
            Stop.IsEnabled = false;
            _timerDispose.Dispose();

        }
        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            _runningQuery.Dispose();

        }

    }
}
