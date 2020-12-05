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
using countdown;

namespace 倒计时
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AddContent();
        }

        public void AddContent()
        {
            var users = new List<Countdown>
            {
                new Countdown() { name = "PPT-10秒钟", totaltime = new TimeSpan(0, 0, 10) },
                new Countdown() { name = "PPT-1分钟", totaltime = new TimeSpan(0, 1, 00) },
                new Countdown() { name = "PPT-10分钟", totaltime = new TimeSpan(0, 10, 0) },
                new Countdown() { name = "PPT-1 小时", totaltime = new TimeSpan(1, 0, 0) }
            };
            lv_countdown.ItemsSource = users;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var countdown = GetCountdown((Button)sender);
            Window1 win = new Window1(countdown);
            win.Show();
            this.WindowState = WindowState.Minimized;
            // this.Visibility = Visibility.Collapsed;
            // MessageBox.Show(countdown.totletime.ToString(), countdown.name);
        }

        private Countdown GetCountdown(Button bt1)
        {
            var stackpanel = (StackPanel)VisualTreeHelper.GetParent(bt1);
            var contentcontrol = (ContentPresenter)VisualTreeHelper.GetParent(stackpanel);
            var gridviewrow = (GridViewRowPresenter)VisualTreeHelper.GetParent(contentcontrol);

            return gridviewrow.Content as Countdown;
        }
    }
}
