using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Linq;
using Freifunk;
using System.Collections.Generic;
using System.Globalization;

namespace FFStraelen
{
    public class DetailPage : ContentPage
    {
        public FFRecord ffr;
        Map map;
        Label lblDistance = new Label();

        public DetailPage(FFRecord Node)
        {
            ffr = Node;
            lblDistance = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = "Ihre Entfernung zu " + Node.NodeName + " beträgt " + String.Format("{0:##,##0} m", ffr.Distance),
                TextColor = Color.Green,
                Font = Device.OnPlatform(
                    iOS: Font.OfSize("GillSans-Light", 12),
                    Android: Font.OfSize("Droid Sans", 16),
                    WinPhone: Font.OfSize("Segoe UI", 18)
                ),
            };

            this.Content = this.SetContent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        public View SetContent()
        {
            Frame frameMap = new Frame()
            { Padding = 1, VerticalOptions = LayoutOptions.FillAndExpand,
                WidthRequest = 200, HeightRequest = 200
            };
            IFormatProvider en = new CultureInfo("en-US");
            double lon = 0;
            double lat = 0;
            double.TryParse(ffr.Lat, NumberStyles.Float, en, out lat);
            double.TryParse(ffr.Long, NumberStyles.Float, en, out lon);
            //Position loc = new Position { Latitude = lat, Longitude = lon };
            map = new Map(
                MapSpan.FromCenterAndRadius(
                    new Position(lat, lon), Distance.FromMiles(0.2)))
            {
                    IsShowingUser = true,
                    VerticalOptions = LayoutOptions.FillAndExpand,
            };
            Pin pin = new Pin()
            {
                Type = PinType.Place,
                Position = new Position(lat, lon),
                Label = ffr.NodeName,
            };

            map.Pins.Add(pin);

            frameMap.Content = map;
            StackLayout stackAddr = this.GetAddrLayout();

            var logo = new Image { Aspect = Aspect.AspectFit, WidthRequest = 100, HeightRequest = 100 };
            if (ffr.LogoURL != null)
            {
                logo.Source = ImageSource.FromUri(new Uri(ffr.LogoURL));
            }

            StackLayout stack = new StackLayout()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(10, Device.OnPlatform(0, 0, 0), 10, 10),
                Spacing = 5,
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    logo,
                    stackAddr,
                    frameMap,
                    lblDistance
                },

            };
            ScrollView sv = new ScrollView();
            sv.Content = stack;

            return sv;
        }

        private StackLayout GetAddrLayout()
        {
         
            StackLayout stackLabel = this.GetLabels();

            StackLayout stack = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                { 
                    stackLabel,
                },
            };

            return stack;
        }

        private StackLayout GetLabels()
        {
            // ********* Name ****************
            var lblHeaderName = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = "Name :",
                TextColor = Color.Green,
                Font = Device.OnPlatform(
                    iOS: Font.OfSize("GillSans-Light", 16),
                    Android: Font.OfSize("Droid Sans", 16),
                    WinPhone: Font.OfSize("Segoe UI", 18)
                ),
            };

            Label lblName = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                    TextColor = Device.OnPlatform(
                        iOS: Color.Black,
                        Android: Color.White,
                        WinPhone: Color.White),
                Font = Device.OnPlatform(
                    iOS: Font.OfSize("GillSans-Light", 12),
                    Android: Font.OfSize("Droid Sans Mono", 14),
                    WinPhone: Font.OfSize("Segoe UI", 14)
                ),
            };

            lblName.SetBinding(Label.TextProperty, "NodeName");
            lblName.BindingContext = ffr;

            // ********* Address ****************
            var lblHeaderAddress = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = "Addresse :",
                TextColor = Color.Green,
                Font = Device.OnPlatform(
                    iOS: Font.OfSize("GillSans-Light", 16),
                    Android: Font.OfSize("Droid Sans", 16),
                    WinPhone: Font.OfSize("Segoe UI", 18)
                ),
            };

            Label lblAddr = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                    TextColor = Device.OnPlatform(
                        iOS: Color.Black,
                        Android: Color.White,
                        WinPhone: Color.White),
                Font = Device.OnPlatform(
                    iOS: Font.OfSize("GillSans-Light", 12),
                    Android: Font.OfSize("Droid Sans Mono", 14),
                    WinPhone: Font.OfSize("Segoe UI", 14)
                ),
            };
            lblAddr.SetBinding(Label.TextProperty, "Address");
            lblAddr.BindingContext = ffr;

            // ********* Phone ****************
            var lblHeaderPhone = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = "Telefon :",
                TextColor = Color.Green,
                Font = Device.OnPlatform(
                    iOS: Font.OfSize("GillSans-Light", 16),
                    Android: Font.OfSize("Droid Sans", 16),
                    WinPhone: Font.OfSize("Segoe UI", 18)
                ),
            };

            Label lblPhone = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                    TextColor = Device.OnPlatform(
                        iOS: Color.Black,
                        Android: Color.White,
                        WinPhone: Color.White),
                Font = Device.OnPlatform(
                    iOS: Font.OfSize("GillSans-Light", 12),
                    Android: Font.OfSize("Droid Sans Mono", 14),
                    WinPhone: Font.OfSize("Segoe UI", 14)
                ),
            };
            lblPhone.SetBinding(Label.TextProperty, "Phone");
            lblPhone.BindingContext = ffr;

            // ********* Fax ****************
            var lblHeaderFax = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = "Telefax :",
                TextColor = Color.Green,
                Font = Device.OnPlatform(
                    iOS: Font.OfSize("GillSans-Light", 16),
                    Android: Font.OfSize("Droid Sans", 16),
                    WinPhone: Font.OfSize("Segoe UI", 18)
                ),
            };

            Label lblFax = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                    TextColor = Device.OnPlatform(
                        iOS: Color.Black,
                        Android: Color.White,
                        WinPhone: Color.White),
                Font = Device.OnPlatform(
                    iOS: Font.OfSize("GillSans-Light", 12),
                    Android: Font.OfSize("Droid Sans Mono", 14),
                    WinPhone: Font.OfSize("Segoe UI", 14)
                ),
            };
            lblFax.SetBinding(Label.TextProperty, "Fax");
            lblFax.BindingContext = ffr;

            // ********* Mail ****************
            var lblHeaderMail = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = "eMail :",
                TextColor = Color.Green,
                Font = Device.OnPlatform(
                    iOS: Font.OfSize("GillSans-Light", 16),
                    Android: Font.OfSize("Droid Sans", 16),
                    WinPhone: Font.OfSize("Segoe UI", 18)
                ),
            };

            Label lblMail = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                    TextColor = Device.OnPlatform(
                        iOS: Color.Black,
                        Android: Color.White,
                        WinPhone: Color.White),
                Font = Device.OnPlatform(
                    iOS: Font.OfSize("GillSans-Light", 12),
                    Android: Font.OfSize("Droid Sans Mono", 14),
                    WinPhone: Font.OfSize("Segoe UI", 14)
                ),
            };
            lblMail.SetBinding(Label.TextProperty, "Mail");
            lblMail.BindingContext = ffr;

            // ********* Web ****************
            var lblHeaderWeb = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = "Web :",
                TextColor = Color.Green,
                Font = Device.OnPlatform(
                    iOS: Font.OfSize("GillSans-Light", 16),
                    Android: Font.OfSize("Droid Sans", 16),
                    WinPhone: Font.OfSize("Segoe UI", 18)
                ),
            };

            Label lblWeb = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                    TextColor = Device.OnPlatform(
                        iOS: Color.Black,
                        Android: Color.White,
                        WinPhone: Color.White),
                Font = Device.OnPlatform(
                    iOS: Font.OfSize("GillSans-Light", 12),
                    Android: Font.OfSize("Droid Sans Mono", 14),
                    WinPhone: Font.OfSize("Segoe UI", 14)
                ),
            };
            lblWeb.SetBinding(Label.TextProperty, "Web");
            lblWeb.BindingContext = ffr;

            TapGestureRecognizer tab = new TapGestureRecognizer();

            tab.Tapped += (object sender, EventArgs e) =>
            {
                    Device.OpenUri(new Uri(ffr.Web));

            };
            lblWeb.GestureRecognizers.Add(tab);

            // ********* OpeningHours ****************
            var lblHeaderOpening = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = "Öffnungszeiten :",
                TextColor = Color.Green,
                Font = Device.OnPlatform(
                    iOS: Font.OfSize("GillSans-Light", 16),
                    Android: Font.OfSize("Droid Sans", 16),
                    WinPhone: Font.OfSize("Segoe UI", 18)
                ),
            };

            Label lblOpening = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                    TextColor = Device.OnPlatform(
                        iOS: Color.Black,
                        Android: Color.White,
                        WinPhone: Color.White),
                Font = Device.OnPlatform(
                    iOS: Font.OfSize("GillSans-Light", 12),
                    Android: Font.OfSize("Droid Sans Mono", 14),
                    WinPhone: Font.OfSize("Segoe UI", 14)
                ),
            };
            lblOpening.SetBinding(Label.TextProperty, "OpeningHours");
            lblOpening.BindingContext = ffr;

            // ********* Description ****************
            var lblHeaderDesc = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = "Beschreibung :",
                TextColor = Color.Green,
                Font = Device.OnPlatform(
                    iOS: Font.OfSize("GillSans-Light", 16),
                    Android: Font.OfSize("Droid Sans", 16),
                    WinPhone: Font.OfSize("Segoe UI", 18)
                ),
            };

            Label lblDesc = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                    TextColor = Device.OnPlatform(
                        iOS: Color.Black,
                        Android: Color.White,
                        WinPhone: Color.White),
                Font = Device.OnPlatform(
                    iOS: Font.OfSize("GillSans-Light", 12),
                    Android: Font.OfSize("Droid Sans Mono", 14),
                    WinPhone: Font.OfSize("Segoe UI", 14)
                ),
            };
            lblDesc.SetBinding(Label.TextProperty, "Description");
            lblDesc.BindingContext = ffr;

            StackLayout stack = new StackLayout()
            {
                Children =
                {
                    lblHeaderName,
                    lblName,
                    lblHeaderAddress,
                    lblAddr,
                    lblHeaderPhone,
                    lblPhone,
                    lblHeaderFax,
                    lblFax,
                    lblHeaderMail,
                    lblMail,
                    lblHeaderWeb,
                    lblWeb,
                    lblHeaderOpening,
                    lblOpening,
                    lblHeaderDesc,
                    lblDesc,
                },
                Spacing = 0,
            };

            return stack;
        }

    }
}

