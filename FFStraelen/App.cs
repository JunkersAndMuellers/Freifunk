using System;
using System.Diagnostics;
using Xamarin.Forms;
//using Xamarin.Forms.Labs.Services.Geolocation;
using XLabs.Ioc;
using XLabs.Forms.Services;
using System.Threading;
using System.IO;
//using Xamarin.Forms.Labs.Services;
//using Xamarin.Forms.Labs.Controls;
using XLabs.Forms.Controls;
//using Xamarin.Forms.Labs.Mvvm;
using XLabs.Forms.Mvvm;
using XLabs.Platform.Services.GeoLocation;

namespace FFStraelen
{
    public class App
    {
        public static MainPage mp = new MainPage(true);

        public static Page GetMainPage()
        {
            //Resolver.Resolve<IGeolocator>();
            var geolocator = DependencyService.Get<IGeolocator>();
            geolocator.PositionChanged += OnPositionChanged;
            geolocator.StartListening(30000, 0, true);

            //return new NavigationPage (new MainPage ());
            return new NavigationPage(mp);
        }

        static void OnPositionChanged(object sender, PositionEventArgs e)
        {

            Device.BeginInvokeOnMainThread(() =>
                {
                    GPSInfo gps = new GPSInfo();
                    try
                    {
                        gps.Timestamp = DateTime.Parse(e.Position.Timestamp.ToString());
                        gps.Latitude = e.Position.Latitude;
                        gps.Longitude = e.Position.Longitude;
                        gps.Accuracy = (double)e.Position.Accuracy;
                        if (e.Position.Altitude != null)
                            gps.Altitude = (double)e.Position.Altitude;
                        if (e.Position.AltitudeAccuracy != null)
                            gps.AltitudeAccuracy = (double)e.Position.AltitudeAccuracy;
                        if (e.Position.Speed != null)
                            gps.Speed = (double)e.Position.Speed;
                        if (e.Position.Heading != null)
                            gps.Heading = (double)e.Position.Heading;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                    mp.GPSChanged(gps);

                });
        }

    }

}



