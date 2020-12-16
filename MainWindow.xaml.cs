using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
using System.Xml.Serialization;
using countdown;
using Microsoft.Win32;

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

            string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".countdown.json");
            if (File.Exists(filename))
            {
                LoadSettings(filename); //从文件加载
            }
            else
            {
                AddContent(); //直接写入
            }
        }

        public void LoadSettings(string filename)
        {
            string str = File.ReadAllText(filename);
            var cds = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Countdown>>(str);
            lv_countdown.ItemsSource = cds;
        }

        public void SaveSettings(string filename, List<Countdown> cds)
        {
            var str = Newtonsoft.Json.JsonConvert.SerializeObject(cds);
            File.WriteAllText(filename, str);
        }

        public void AddContent()
        {
            var cds = new List<Countdown>
            {
                new Countdown() { Name = "PPT-10秒钟", Totaltime = new TimeSpan(0, 0, 10) },
                new Countdown() { Name = "PPT-1分钟", Totaltime = new TimeSpan(0, 1, 00) },
                new Countdown() { Name = "PPT-10分钟", Totaltime = new TimeSpan(0, 10, 0) },
                new Countdown() { Name = "PPT-1 小时", Totaltime = new TimeSpan(1, 0, 0) }
            };
            lv_countdown.ItemsSource = cds;
            // SaveSettings(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".countdown.json"),cds);
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

        private void OpenCommnad_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog()==true)
            {
                MessageBox.Show(openFileDialog.FileName);
            }
        }

        private void OpenCommnad_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SaveCommnad_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("The New command was invoked");
        }

        private void SaveCommnad_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void SaveAsCommnad_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("The New command was invoked");
        }

        private void SaveAsCommnad_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CloseCommnad_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void CloseCommnad_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void BtnAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("欢迎联系作者：745566143@qq.com","关于");
        }
    }
}
