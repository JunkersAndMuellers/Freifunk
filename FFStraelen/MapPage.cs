using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Linq;
using Freifunk;
using System.Collections.Generic;
using System.Globalization;

namespace FFStraelen
{
    public class MapPage : ContentPage
    {
        Map map;
        List<FFRecord> _NodeList;

        public MapPage(List<FFRecord> NodeList)
        {
            _NodeList = NodeList;

            //TextColor = Color.Black,
            map = new Map
            { 
                IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            map.MoveToRegion(MapSpan.FromCenterAndRadius(
				//new Position (36.9628066,-122.0194722), Distance.FromMiles (3))); // Santa Cruz golf course
                    new Position(51.44095161184099, 6.2565146768094415), Distance.FromMiles(3))); // Santa Cruz golf course

            IFormatProvider en = new CultureInfo("en-US");

            // Add Pins
            foreach (FFRecord fr in NodeList)
            {
                if (fr.Online)
                {
                    double lon = 0;
                    double lat = 0;
                    double.TryParse(fr.Lat, NumberStyles.Float, en, out lat);
                    double.TryParse(fr.Long, NumberStyles.Float, en, out lon);
                    Pin p = new Pin
                    {
                        Position = new Position(lat, lon),
                        Label = fr.NodeName + "/" + fr.CurrentClients.ToString() + " Clients"
                    };
                    map.Pins.Add(p);
                }
            }

            // 
            map.MoveToRegion(MapSpan.FromCenterAndRadius(
                    new Position(51.4447212, 6.2668271), Distance.FromMiles(0.4)));

            // put the page together
            Content = new StackLayout
            {
                Spacing = 0,
                Children =
                {
                    map
                }
            };
        }

        public void GPSChangend(GPSInfo gpsinfo)
        {
            IFormatProvider en = new CultureInfo("en-US");
            //IFormatProvider de = new CultureInfo ("de-DE");
            double lon = 0;
            double lat = 0;
            string lbl = "";
            int nodenr = 0;

            foreach (Pin p in map.Pins)
            {
                foreach (Freifunk.FFRecord fr in _NodeList)
                {
                    if (fr.NodeName == null)
                        continue;
                    lbl = fr.NodeName + "/" + fr.CurrentClients.ToString() + " C";
                    if (p.Label.StartsWith(fr.NodeName))
                    {
                        double.TryParse(fr.Lat, NumberStyles.Float, en, out lat);
                        double.TryParse(fr.Long, NumberStyles.Float, en, out lon);
                        nodenr = fr.NodeID;
                        break;
                    }
                }
                double distance = 
                    FFStraelen.Location.TrackingHelper.CalculateDistance(
                        new FFStraelen.Location.Location() { Latitude = gpsinfo.Latitude, Longitude = gpsinfo.Longitude },
                        new FFStraelen.Location.Location() { Latitude = lat, Longitude = lon });
             
                distance *= 1000;
                p.Label = lbl + "/Entf." + String.Format("{0:##,##0} m", distance);
                var item = (from n in _NodeList
                                        where n.NodeID == nodenr
                                        select n).FirstOrDefault();
                item.Distance = (Int64)distance;
            }
        }
    }
}

