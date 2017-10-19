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
        private IDisposable _timerDispose;
        public MainWindow()
        {
            InitializeComponent();
            _netflix = new NetflixCatalog(new Uri("http://odata.netflix.com/Catalog"));
        }
        
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
            var titlesSequence = new DataSequence<Title>(
                titlesQuery);

            var rateLimit = 1.0 / double.Parse(Rate.Text);
            var timerSequence = Observable.Timer(DateTimeOffset.Now, TimeSpan.FromSeconds(rateLimit));
            // rate limit the titles sequence
            var rateLimitedSequence = timerSequence.Zip(titlesSequence, (l, r) => r);
            // Include the time interval since precious value
            _runningQuery = rateLimitedSequence.TimeInterval()
                .ObserveOnDispatcher()
                .Finally(Cleanup)
                .Subscribe(UpdateResultsListbox);
        }

        private void UpdateResultsListbox(TimeInterval<Title> timeInterval)
        {
            var item = Results.Items
                .Add(string.Format("{0} ∆ {1} ms", timeInterval.Value.ShortName,
                                   timeInterval.Interval.TotalMilliseconds));
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
