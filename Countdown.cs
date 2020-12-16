using System;
using System.Xml;
using System.Xml.Serialization;

namespace countdown 
{ 
	public class Countdown
	{
		public string Name { get; set; }
		[XmlIgnore]
		public TimeSpan Totaltime {
			get;
			set; 
		}
		public string Totaltimestr
        {
			get
            {
				return XmlConvert.ToString(Totaltime);
            }
			set
            {
				Totaltime = string.IsNullOrEmpty(value) ? TimeSpan.Zero : XmlConvert.ToTimeSpan(value);
            }
        }
	}
}