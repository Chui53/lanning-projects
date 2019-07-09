using System;
using System.Collections.ObjectModel;
using System.Windows;
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
        ReadOnlyCollection<TimeZoneInfo> timezones;
        public MainWindow()
        {
            InitializeComponent();
            timezones = TimeZoneInfo.GetSystemTimeZones();
            foreach (TimeZoneInfo tz in timezones)
            {
                TimeBox.Items.Add(tz.Id);
            }
            timeInfo = TimeZoneInfo.FindSystemTimeZoneById("US Eastern Standard Time");
            Timer.Interval = TimeSpan.FromMilliseconds(1);
            Timer.Tick += new EventHandler(Timer_Tick);
            Timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(TimeBox.SelectedValue != null)
            {
                timeInfo = TimeZoneInfo.FindSystemTimeZoneById(TimeBox.SelectedValue.ToString());
            }
            currentTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeInfo);
            Time.Text = currentTime.ToString("HH:mm:ss");
            
        }
    }
}
