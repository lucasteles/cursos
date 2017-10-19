using System;
using System.Concurrency;
using System.Linq;
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

        }

        private void UpdateResultsListbox(TimeInterval<Title> timeInterval)
        {
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
