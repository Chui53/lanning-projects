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
        DateTime currentTime;
        TimeZoneInfo timeInfo;
        bool east;
        bool west;
        bool cent;
        bool fore;
        public MainWindow()
        {
            InitializeComponent();
            timeInfo = TimeZoneInfo.FindSystemTimeZoneById("US Eastern Standard Time");        
            east = true;
            Timer.Interval = TimeSpan.FromMilliseconds(1);
            Timer.Tick += new EventHandler(Timer_Tick);
            Timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            currentTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeInfo);
            Time.Text = currentTime.ToString("HH:mm:ss");
            if (east)
            {
                USEastern.IsEnabled = false;
                USWestern.IsEnabled = true;
                USCentral.IsEnabled = true;
                Foreign.IsEnabled = true;
            } else if (west)
            {
                USEastern.IsEnabled = true;
                USWestern.IsEnabled = false;
                USCentral.IsEnabled = true;
                Foreign.IsEnabled = true;
            } else if (cent)
            {
                USEastern.IsEnabled = true;
                USWestern.IsEnabled = true;
                USCentral.IsEnabled = false;
                Foreign.IsEnabled = true;
            } else if (fore)
            {
                USEastern.IsEnabled = true;
                USWestern.IsEnabled = true;
                USCentral.IsEnabled = true;
                Foreign.IsEnabled = false;
            }
        }

        private void Eastern_Click(object sender, RoutedEventArgs e)
        {
            timeInfo = TimeZoneInfo.FindSystemTimeZoneById("US Eastern Standard Time");
            east = true;
            cent = false;
            west = false;
            fore = false;
        }

        private void Central_Click(object sender, RoutedEventArgs e)
        {
            timeInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            cent = true;
            east = false;
            west = false;
            fore = false;
        }

        private void Western_Click(object sender, RoutedEventArgs e)
        {
            timeInfo = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
            west = true;
            east = false;
            cent = false;
            fore = false;
        }

        private void Foreign_Click(object sender, RoutedEventArgs e)
        {
            timeInfo = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
            fore = true;
            east = false;
            west = false;
            cent = false;
        }
    }
}
