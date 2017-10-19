using System;
using System.Reactive.Concurrency;
using System.Linq;
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

        public MainWindow()
        {
            InitializeComponent();
            _netflix = new NetflixCatalog(new Uri("http://odata.netflix.com/Catalog"));
        }

        private IDisposable _timerDispose = null;
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Start.IsEnabled = false;
            Stop.IsEnabled = true;
            TimeoutInfo.Text = "";
            _timerDispose = Observable.Timer(DateTimeOffset.Now, TimeSpan.FromSeconds(1))
                .SubscribeOn(Scheduler.ThreadPool)
                .ObserveOnDispatcher()
                .Subscribe(n => Elapsed.Text = n.ToString());
            Results.Items.Clear();
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


            _runningQuery = new DataSequence<Title>(titlesQuery, 0)
                .Timestamp()
                // cause timeout 16 seconds after query starts
                .Timeout(DateTimeOffset.Now + TimeSpan.FromSeconds(16),
                            DispatcherScheduler.Instance)
                // timeout will cause exception, catch it here
                .Catch((TimeoutException excp) => Timeout(excp))
                .ObserveOn(DispatcherScheduler.Instance)
                .SubscribeOn(Scheduler.ThreadPool)
                .Finally(Cleanup)
                .Subscribe(UpdateResultsListbox);

        }

        // handle timeout
        private IObservable<System.Reactive.Timestamped<Title>> Timeout(TimeoutException to)
        {
            // timeouts and exceptions are handled on the 
            // scheduler you specify in the Timeout extension
            // method
            TimeoutInfo.Text = to.Message;
            // you could inject another observable sequence here
            // but this example doesn't do that
            return Observable.Empty<System.Reactive.Timestamped<Title>>();
        }

        private void UpdateResultsListbox(System.Reactive.Timestamped<Title> timestamp)
        {
            var item = Results.Items
                .Add(string.Format("{0} {1} ", timestamp.Value.ShortName, timestamp.Value.TinyUrl));
            Results.Items.MoveCurrentToLast();
            Results.ScrollIntoView(Results.Items[item]);
        }
        void Cleanup()
        {
            //TimeoutInfo.Text = _timeoutInfo;
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
