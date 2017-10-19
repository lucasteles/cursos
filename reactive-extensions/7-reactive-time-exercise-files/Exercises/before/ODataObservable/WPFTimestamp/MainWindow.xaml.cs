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
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            // dispose of running query to stop it.
            _runningQuery.Dispose();
        }
    }
}
