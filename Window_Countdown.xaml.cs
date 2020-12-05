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
using System.Windows.Shapes;
using countdown;
using countdowntimer;

namespace 倒计时
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public CountdownTimer timer;

        public Window1(Countdown countdown)
        {
            InitializeComponent();
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width;
            timer = new CountdownTimer(countdown);

            DataContext = timer;
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (timer.Currentstate == TimerState.未开始)
            {
                timer.Start();
            } else if (timer.Currentstate == TimerState.运行)
            {
                timer.Pause();
            } else if (timer.Currentstate == TimerState.暂停)
            {
                timer.Continuego();
            }
        } 

        private void BtnPause_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        protected override void OnClosed(EventArgs e)
        {
            // App.Current.MainWindow.Show();
            App.Current.MainWindow.WindowState = WindowState.Minimized;
            base.OnClosed(e);
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            timer.Stop();
            App.Current.MainWindow.WindowState = WindowState.Normal;
        }
    }
}
