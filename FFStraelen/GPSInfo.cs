using System;

namespace FFStraelen
{
	public delegate void GPSEventHandler(object source, GPSInfo gpsinfo);

	public class GPSInfo
	{
		public DateTime Timestamp;
		public double Latitude = 0;
		public double Longitude = 0;
		public double Accuracy = 0;
		public double Altitude = 0;
		public double AltitudeAccuracy = 0;
		public double Speed = 0;
		public double Heading  = 0;
		public bool DataValid = false;

		public GPSInfo ()
		{
			DataValid = false;
		}
	}
}

