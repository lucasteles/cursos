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

        public MainWindow()
        {
            InitializeComponent();
            _netflix = new NetflixCatalog(new Uri("http://odata.netflix.com/Catalog"));
        }

        private IDisposable _timerDispose = null;
        private void Start_Click(object sender, RoutedEventArgs e)
        {
        }

        // handle timeout
        private IObservable<Timestamped<Title>> Timeout(TimeoutException to)
        {
            // timeouts and exceptions are handled on the 
            // scheduler you specify in the Timeout extension
            // method
            TimeoutInfo.Text = to.Message;
            // you could inject another observable sequence here
            // but this example doesn't do that
            return Observable.Empty<Timestamped<Title>>();
        }

        private void UpdateResultsListbox(Timestamped<Title> timestamp)
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
