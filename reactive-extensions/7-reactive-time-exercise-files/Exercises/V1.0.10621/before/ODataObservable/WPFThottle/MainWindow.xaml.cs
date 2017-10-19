using System;
using System.Concurrency;
using System.Linq;
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
            // reset titles found to zero

            // clear out listbox to make room for new titles found

            // holds query... which one depends if a release year was specified or not
            IQueryable<Title> query = null;

            // turn off Start so we don't end up with two queries running at once
            Start.IsEnabled = false;
            // enable stop so we can stop the query
            Stop.IsEnabled = true;
            // make an observable sequence out of the query and subscribe to it
            _runningQuery = 
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            // dispose of running query to stop it.
            _runningQuery.Dispose();
        }
    }
}
