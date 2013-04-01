using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml.Serialization;

namespace Part_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        Clock clock = new Clock();

        public MainWindow()
        {
            InitializeComponent();

            #region CLOCK

            spClock.DataContext = clock;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();

            #endregion // CLOCK
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            clock.Update();
        }

        #region MenuEvents

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion // MenuEvents

    }
}
