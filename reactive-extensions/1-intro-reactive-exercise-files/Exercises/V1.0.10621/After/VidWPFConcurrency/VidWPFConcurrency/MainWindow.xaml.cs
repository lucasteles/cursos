using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Windows;


namespace VidWPFConcurrency
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }
    private static string StringWait(string str)
    {
      Thread.Sleep(250);
      return str;
    }
    private void StartClick(object sender, RoutedEventArgs e)
    {
      var query = from number in Enumerable.Range(1, 25) 
                  select StringWait(number.ToString());
      var observableQuery = query.ToObservable(Scheduler.ThreadPool);
      observableQuery.ObserveOnDispatcher().Subscribe(n => Results.AppendText(
        string.Format("{0}\n", n)), ImDone);
    }
    private void ImDone()
    {
      Results.AppendText("I'm Done");
    }

  }
}
