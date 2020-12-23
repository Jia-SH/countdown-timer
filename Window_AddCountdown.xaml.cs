using countdown;
using System;
using System.Windows;
using System.Windows.Input;

namespace 倒计时
{
    /// <summary>
    /// Window_AddCountdown.xaml 的交互逻辑
    /// </summary>
    public partial class Window_AddCountdown : Window
    {
        public Countdown Cd
        {
            get; set;
        }

        public Window_AddCountdown()
        {
            Cd = new Countdown
            {
                Name = "",
                Totaltime = new TimeSpan(0, 0, 0)
            };
            InitializeComponent();
            DataContext = Cd;
        }
            
        private void Window_Loaded(object sender, EventArgs e)
        {
            title.SelectAll();
            title.Focus();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Timetick_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Cd.Totaltime = (TimeSpan)timetick.Value;
        }
    }
}
