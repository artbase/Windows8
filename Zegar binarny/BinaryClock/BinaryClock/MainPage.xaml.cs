using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace BinaryClock
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MainPage : BinaryClock.Common.LayoutAwarePage
    {
        public MainPage()
        {
            this.InitializeComponent();
            DispatcherTimerSetup();
            txtDay.Text = DateTime.Now.DayOfWeek.ToString().Replace("Monday", "Poniedziałek").Replace("Tuesday", "Wtorek").Replace("Wednesday", "Środa").Replace("Thursday", "Czwartek").Replace("Friday", "Piątek").Replace("Saturday", "Sobota").Replace("Sunday", "Niedziela");
            txtDayWeek.Text = DateTime.Now.Date.Day + " " + DateTime.Now.Month.ToString().Replace("6", "Czerwiec").Replace("7", "Lipiec") + " " + DateTime.Now.Year.ToString();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }
        DispatcherTimer dispatcherTimer;
        DateTimeOffset startTime;
        DateTimeOffset lastTime;
        DateTimeOffset stopTime;
        int timesTicked = 1;
        int timesToTick = 10;
        public void DispatcherTimerSetup()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            ////IsEnabled defaults to false
            //TimerLog.Text += "dispatcherTimer.IsEnabled = " + dispatcherTimer.IsEnabled + "\n";
            //startTime = DateTimeOffset.Now;
            //lastTime = startTime;
            //TimerLog.Text += "Calling dispatcherTimer.Start()\n";
            dispatcherTimer.Start();
            ////IsEnabled should now be true after calling start
            //TimerLog.Text += "dispatcherTimer.IsEnabled = " + dispatcherTimer.IsEnabled + "\n";
        }

        SolidColorBrush myBrush = new SolidColorBrush(Colors.White);
        SolidColorBrush black = new SolidColorBrush(Colors.Black);

        void dispatcherTimer_Tick(object sender, object e)
        {
            DateTimeOffset time = DateTimeOffset.Now;

            String hour = Convert.ToString(time.Hour, 2);
            string minute = Convert.ToString(time.Minute, 2);
            string second = Convert.ToString(time.Second, 2);

            #region second
             _25.Fill = black;
             _24.Fill = black;
             _23.Fill = black;
             _22.Fill = black;
             _21.Fill = black;
             _20.Fill = black;
             _24.Fill = black;
             _23.Fill = black;
             _22.Fill = black;
             _21.Fill = black;
             _20.Fill = black;
             _23.Fill = black;
             _22.Fill = black;
             _21.Fill = black;
             _20.Fill = black;
             _22.Fill = black;
             _21.Fill = black;
             _20.Fill = black;
             _21.Fill = black;
             _20.Fill = black;
             _20.Fill = black;

             _15.Fill = black;
             _14.Fill = black;
             _13.Fill = black;
             _12.Fill = black;
             _11.Fill = black;
             _10.Fill = black;
             _14.Fill = black;
             _13.Fill = black;
             _12.Fill = black;
             _11.Fill = black;
             _10.Fill = black;
             _13.Fill = black;
             _12.Fill = black;
             _11.Fill = black;
             _10.Fill = black;
             _12.Fill = black;
             _11.Fill = black;
             _10.Fill = black;
             _11.Fill = black;
             _10.Fill = black;
             _10.Fill = black;

             _05.Fill = black;
             _04.Fill = black;
             _03.Fill = black;
             _02.Fill = black;
             _01.Fill = black;
             _04.Fill = black;
             _03.Fill = black;
             _02.Fill = black;
             _01.Fill = black;
             _03.Fill = black;
             _02.Fill = black;
             _01.Fill = black;
             _02.Fill = black;
             _01.Fill = black;
             _01.Fill = black;
            #endregion




            #region second

            if (second.Length == 6)
            {
                if (second[5] == '1') _25.Fill = myBrush;
                if (second[4] == '1') _24.Fill = myBrush;
                if (second[3] == '1') _23.Fill = myBrush;
                if (second[2] == '1') _22.Fill = myBrush;
                if (second[1] == '1') _21.Fill = myBrush;
                if (second[0] == '1') _20.Fill = myBrush;
            }
            if (second.Length == 5)
            {
                if (second[4] == '1') _24.Fill = myBrush;
                if (second[3] == '1') _23.Fill = myBrush;
                if (second[2] == '1') _22.Fill = myBrush;
                if (second[1] == '1') _21.Fill = myBrush;
                if (second[0] == '1') _20.Fill = myBrush;
            }
            if (second.Length == 4)
            {
                if (second[3] == '1') _23.Fill = myBrush;
                if (second[2] == '1') _22.Fill = myBrush;
                if (second[1] == '1') _21.Fill = myBrush;
                if (second[0] == '1') _20.Fill = myBrush;
            }
            if (second.Length == 3)
            {
                if (second[2] == '1') _22.Fill = myBrush;
                if (second[1] == '1') _21.Fill = myBrush;
                if (second[0] == '1') _20.Fill = myBrush;
            }
            if (second.Length == 2)
            {
                if (second[1] == '1') _21.Fill = myBrush;
                if (second[0] == '1') _20.Fill = myBrush;
            }
            if (second.Length == 1)
            {
                if (second[0] == '1') _20.Fill = myBrush;
            }


            #endregion

            #region minute

            if (minute.Length == 6)
            {
                if (minute[5] == '1') _15.Fill = myBrush;
                if (minute[4] == '1') _14.Fill = myBrush;
                if (minute[3] == '1') _13.Fill = myBrush;
                if (minute[2] == '1') _12.Fill = myBrush;
                if (minute[1] == '1') _11.Fill = myBrush;
                if (minute[0] == '1') _10.Fill = myBrush;
            }
            if (minute.Length == 5)
            {
                if (minute[4] == '1') _14.Fill = myBrush;
                if (minute[3] == '1') _13.Fill = myBrush;
                if (minute[2] == '1') _12.Fill = myBrush;
                if (minute[1] == '1') _11.Fill = myBrush;
                if (minute[0] == '1') _10.Fill = myBrush;
            }
            if (minute.Length == 4)
            {
                if (minute[3] == '1') _13.Fill = myBrush;
                if (minute[2] == '1') _12.Fill = myBrush;
                if (minute[1] == '1') _11.Fill = myBrush;
                if (minute[0] == '1') _10.Fill = myBrush;
            }
            if (minute.Length == 3)
            {
                if (minute[2] == '1') _12.Fill = myBrush;
                if (minute[1] == '1') _11.Fill = myBrush;
                if (minute[0] == '1') _10.Fill = myBrush;
            }
            if (minute.Length == 2)
            {
                if (minute[1] == '1') _11.Fill = myBrush;
                if (minute[0] == '1') _10.Fill = myBrush;
            }
            if (minute.Length == 1)
            {
                if (minute[0] == '1') _10.Fill = myBrush;
            }


            #endregion

            #region hour

            if (hour.Length == 5)
            {
                if (hour[4] == '1') _05.Fill = myBrush;
                if (hour[3] == '1') _04.Fill = myBrush;
                if (hour[2] == '1') _03.Fill = myBrush;
                if (hour[1] == '1') _02.Fill = myBrush;
                if (hour[0] == '1') _01.Fill = myBrush;
            }
            if (hour.Length == 4)
            {
                if (hour[3] == '1') _04.Fill = myBrush;
                if (hour[2] == '1') _03.Fill = myBrush;
                if (hour[1] == '1') _02.Fill = myBrush;
                if (hour[0] == '1') _01.Fill = myBrush;
            }
            if (hour.Length == 3)
            {
                if (hour[2] == '1') _03.Fill = myBrush;
                if (hour[1] == '1') _02.Fill = myBrush;
                if (hour[0] == '1') _01.Fill = myBrush;
            }
            if (hour.Length == 2)
            {
                if (hour[1] == '1') _02.Fill = myBrush;
                if (hour[0] == '1') _01.Fill = myBrush;
            }
            if (hour.Length == 1)
            {
                if (hour[0] == '1') _01.Fill = myBrush;
            }


            #endregion


            //TimeSpan span = time - lastTime;
            //lastTime = time;
            ////Time since last tick should be very very close to Interval
            //TimerLog.Text += timesTicked + "\t time since last tick: " + span.ToString() + "\n";
            //timesTicked++;
            //if (timesTicked > timesToTick)
            //{
            //    stopTime = time;
            //    TimerLog.Text += "Calling dispatcherTimer.Stop()\n";
            //    dispatcherTimer.Stop();
            //    //IsEnabled should now be false after calling stop
            //    TimerLog.Text += "dispatcherTimer.IsEnabled = " + dispatcherTimer.IsEnabled + "\n";
            //    span = stopTime - startTime;
            //    TimerLog.Text += "Total Time Start-Stop: " + span.ToString() + "\n";
            //}
        }
    }
}
