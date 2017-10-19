#region PluralsightExerciseExample
// Copyright Pluralsight LLC 2011
// 
// This example is for the exclusive use of subscribers to a Pluralsight On Demand!
// subscription that includes example code.
// 
// Please do not post these examples or pass them around.
// 
// This code is meant to be used for training purposes only.
// 
// No representations are made as to its usefullness or correctness.
// 
// This code may not be used in any applications or production code.
// 
// Thank You!
#endregion

#region

using System;

using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Windows;

#endregion

namespace DispatchScheduler
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            Generate.IsEnabled = false;
            Stop.IsEnabled = true;
            Sequence.Clear();
            var query = from number in Enumerable.Range(
                        int.Parse(First.Text), 
                        int.Parse(ValueCount.Text))
                        select Slow(number);
            var observableSequence = query.ToObservable()
                .SubscribeOn(Scheduler.ThreadPool)
                .ObserveOnDispatcher()
                .Finally(() =>
                    {
                        Stop.IsEnabled = false;
                        Generate.IsEnabled = true;
                    });
            _subscribe = observableSequence.Subscribe(
                number => Sequence.Text += number.ToString() + " ");
        }
        static int Slow(int number)
        {
            Thread.Sleep(300);
            return number;
        }
        private IDisposable _subscribe;
        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            _subscribe.Dispose();
        }

    }
}
