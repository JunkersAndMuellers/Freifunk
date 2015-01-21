using System;

using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms.Labs.Droid;
using Xamarin.Forms.Labs;
using Xamarin.Forms.Labs.Mvvm;
using Xamarin.Forms.Labs.Services;
using Xamarin.Forms.Labs.Services.Serialization;
//using Xamarin.Forms.Labs.Caching.SQLiteNet;
using System.IO;

using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Xamarin;


namespace FFStraelen.Android
{
	//[Activity (Label = "Freifunk Straelen", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [Activity(Label = "Freifunk Straelen",Theme="@android:style/Theme.Holo", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : XFormsApplicationDroid
	{
		protected override void OnCreate (Bundle bundle)
		{
            Insights.Initialize("cd1354072db8a5c501c58b5e6815133dec23e0da", ApplicationContext);

			base.OnCreate (bundle);

			//if (!Xamarin.Forms.Labs.Services.Resolver.IsSet)
			{
				this.SetIoc();
			}
			//else
			{
			//	var app = Xamarin.Forms.Labs.Services.Resolver.Resolve<IXFormsApp>() as IXFormsApp<XFormsApplicationDroid>;
			//	app.AppContext = this;
			}
				
			Xamarin.Forms.Forms.Init (this, bundle);
			Xamarin.FormsMaps.Init (this, bundle);

			SetPage (App.GetMainPage ());
		}

        protected override void OnResume()
        {
            base.OnResume();
            //MessagingCenter.Send<FFStraelen.MainPage>(new FFStraelen.MainPage(false), "Refresh");
        }

		private void SetIoc()
		{
			var resolverContainer = new SimpleContainer();

			var app = new XFormsAppDroid();

			app.Init(this);

			var documents = app.AppDataDirectory;
			//var pathToDatabase = Path.Combine(documents, "xforms.db");

			resolverContainer.Register<IDevice> (t => AndroidDevice.CurrentDevice);
			Resolver.SetResolver(resolverContainer.GetResolver());
		}
	}
}

