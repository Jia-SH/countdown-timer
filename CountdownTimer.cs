using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using countdown;

namespace countdowntimer
{
	public enum TimerState
    {
		未开始,
		运行,
		暂停
    }
	public class CountdownTimer : INotifyPropertyChanged
	{
		private string _name;
		public string Name { 
			get { 
				return _name; 
			}
			set
			{
				_name = value;
				OnPropertyRaised("Name");
			}
		}
		public TimeSpan totaltime;
		private TimeSpan _currenttime;
		public TimeSpan Currenttime {
			get { return _currenttime; }
			set {
				_currenttime = value;
				OnPropertyRaised("Currenttime");
			} }
		private TimerState _currentstate;
		public TimerState Currentstate 
		{
			get { return _currentstate; }
			set {
				_currentstate = value;
				OnPropertyRaised("CurrentState");
			}
		}
		private string _btnnamestart;
		public string Btnnamestart
		{
			get { return _btnnamestart; }
			set
			{
				_btnnamestart = value;
				OnPropertyRaised("Btnnamestart");
			}
		}

		private bool _isEnabledPause;
		public bool IsEnabledPause
		{ 
			get
            {
				return _isEnabledPause;
            }
			set
            {
				_isEnabledPause = value;
				OnPropertyRaised("IsEnabledPause");
            }
		}

		private SolidColorBrush _textBackgroundColor;
		public SolidColorBrush TextBackgroundColor
        {
			get
            {
				return _textBackgroundColor;
            }
			set
            {
				_textBackgroundColor = value;
				OnPropertyRaised("TextBackgroundColor");
            }
        }

		private void OnPropertyRaised(string propertyname)
		{
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

		readonly BackgroundWorker timer;
		private ManualResetEvent manualReset;
		public event PropertyChangedEventHandler PropertyChanged;

		public void NotifyPropertyChanged(string propName)
		{
			if (this.PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propName));
		}

		public CountdownTimer(Countdown countdown)
		{
			// timer.WorkerReportsProgress = true;
			// timer.ProgressChanged += timer_ProgressChanged;
			Name = countdown.name;
			totaltime = countdown.totaltime;
			Currenttime = totaltime;
			Currentstate = TimerState.未开始;
			Btnnamestart = "开始";
			IsEnabledPause = false;
			TextBackgroundColor = Brushes.Blue;

            timer = new BackgroundWorker
            {
                WorkerSupportsCancellation = true
            };
            timer.DoWork += Timer_Tick;
			timer.RunWorkerCompleted += Timer_RunWorkerCompleted;

			manualReset = new ManualResetEvent(true);
	}

		private void Timer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			// throw new NotImplementedException();
			if (e.Cancelled == false)
			{
				MessageBox.Show("时间到！");
			}
		}

		~CountdownTimer()
		{
			this.Stop();
		}

		void Timer_Tick(object sender, DoWorkEventArgs e)
		{
			while ((int)Currenttime.TotalSeconds > 0)
			{
				if (timer.CancellationPending) // 用户取消操作
				{
					e.Cancel = true;
					timer.CancelAsync();
					return;
				}


				if (Currenttime.TotalSeconds <=3)
                {
					TextBackgroundColor = Brushes.Red;
				}
				else if (Currenttime.TotalSeconds <= 10)
				{
					TextBackgroundColor = Brushes.OrangeRed;
				}
				Console.WriteLine("Start!" + Currenttime.ToString());
				Currenttime = Currenttime.Add(TimeSpan.FromSeconds(-1));
				manualReset.WaitOne();
				Thread.Sleep(1000);
			}
		}

		public void Start()
		{
			Currenttime = totaltime;
			Currentstate = TimerState.运行;
			Btnnamestart = "暂停";
			IsEnabledPause = true;
			timer.RunWorkerAsync();
		}

		public void Pause()
		{
			Currentstate = TimerState.暂停;
			Btnnamestart = "继续";
			IsEnabledPause = true;
			manualReset.Reset();
		}

		public void Restart()
		{
			timer.CancelAsync();
			Start();
		}

		public void Continuego()
		{
			Currentstate = TimerState.运行;
			Btnnamestart = "暂停";
			IsEnabledPause = true;
			manualReset.Set();
		}

		public void Stop()
		{
			Currentstate = TimerState.未开始;
			Currenttime = totaltime;
			Btnnamestart = "开始";
			IsEnabledPause = false;
			timer.CancelAsync();
			manualReset = new ManualResetEvent(true);
		}
	}
}