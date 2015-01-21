using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

using Xamarin.Forms;
//using Xamarin.Forms.Labs;
using XLabs.Forms;
//using Xamarin.Forms.Labs.Services;
using XLabs.Forms.Services;
using System.Xml;
using Xamarin;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services.GeoLocation;

namespace FFStraelen.iOS
{
	[Register ("AppDelegate")]

    public partial class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;
        //GetForeground_iOS gfg = new GetForeground_iOS();

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
            //Insights.Initialize("cd1354072db8a5c501c58b5e6815133dec23e0da");

			this.SetIoc();
			Forms.Init ();

			window = new UIWindow (UIScreen.MainScreen.Bounds);
			Xamarin.FormsMaps.Init ();

			window.RootViewController = App.GetMainPage ().CreateViewController ();
			window.MakeKeyAndVisible ();
			return true;
		}

		private void SetIoc()
		{
			var resolverContainer = new SimpleContainer();
            DependencyService.Register<XLabs.Platform.Services.Geolocation.Geolocator>();
            resolverContainer.Register<XLabs.Platform.Device.IDevice> (t => AppleDevice.CurrentDevice);
            Resolver.SetResolver(resolverContainer.GetResolver());
		}

        public override void OnActivated(UIApplication application)
        {
            //MessagingCenter.Send<FFStraelen.MainPage>(new FFStraelen.MainPage(false), "Refresh");
        }
	}
}

