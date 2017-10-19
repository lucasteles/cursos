using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Windows;
using WPFThrottle.Netflix;
using ODataObservable;
namespace WPFThrottle
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
            // get the LINQ provider for the Netflix odata endpoint
            _netflix = new NetflixCatalog(new Uri("http://odata.netflix.com/Catalog"));
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            // start time to update GUI with elapsed seconds since Start was pushed
            var timer = Observable.Timer(DateTimeOffset.Now, TimeSpan.FromSeconds(1))
                .ObserveOnDispatcher()
                .Subscribe(number => Elapsed.Text = string.Format("{0} seconds", number.ToString()));
            // reset titles found to zero
            TitleCount.Text = "0";
            // clear out listbox to make room for new titles found
            Results.Items.Clear();

            // holds query... which one depends if a release year was specified or not
            IQueryable<Title> query = null;
            if (string.IsNullOrWhiteSpace(ReleaseYear.Text))
            {
                query = from title in _netflix.Titles select title;
            }
            else
            {
                var releaseYear = int.Parse(ReleaseYear.Text);
                query = from title in _netflix.Titles where title.ReleaseYear == releaseYear select title;
            }
            // turn off Start so we don't end up with two queries running at once
            Start.IsEnabled = false;
            // enable stop so we can stop the query
            Stop.IsEnabled = true;
            // make an observable sequence out of the query and subscribe to it
            _runningQuery = new DataSequence<Title>(query)
                // timestamp each value
                .Timestamp()
                // observe on dispatch so that application remains reponsive
                .ObserveOnDispatcher()
                // when done, clean up
                .Finally(() =>
                             {
                                 // reset things for next query
                                 Start.IsEnabled = true;
                                 Stop.IsEnabled = false;
                                 timer.Dispose();
                             })
                .Subscribe(timestamp =>
                               {
                                   // update the gui with running results
                                   var itemIndex = Results.Items
                                       .Add(string.Format("{0} @ {1}", timestamp.Value.ShortName,
                                                          timestamp.Timestamp));
                                   Results.ScrollIntoView(Results.Items[itemIndex]);
                                   TitleCount.Text = (int.Parse(TitleCount.Text) + 1).ToString();

                               });

        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            // dispose of running query to stop it.
            _runningQuery.Dispose();
        }
    }
}
