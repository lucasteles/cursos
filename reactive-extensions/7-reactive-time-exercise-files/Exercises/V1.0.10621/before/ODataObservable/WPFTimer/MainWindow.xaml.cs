using System;
using System.Collections.Generic;
using System.Concurrency;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
