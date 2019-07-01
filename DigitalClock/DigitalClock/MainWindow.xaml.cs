using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DigitalClock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer Timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Tick += new EventHandler(timer_Tick);
            Timer.Start();           
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Time.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void Eastern_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Central_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Western_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Foreign_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
