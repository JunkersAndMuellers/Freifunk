using System;
using System.Collections.Generic;
using System.Reflection;
using Xamarin.Forms;
using System.Linq;
using Freifunk;
using Xamarin.Forms.Maps;
using System.Globalization;

namespace FFStraelen
{
    public class ListPage : ContentPage
    {
        public bool ReloadAllowed = true;

        public TableView NodeTable = new TableView();
        TableRoot tr = new TableRoot();
        TableSection tsonline = new TableSection("Online Nodes");
        TableSection tsoffline = new TableSection("Offline Nodes");
        List<FFRecord> _NodeList;

        public ListPage(List<FFRecord> NodeList)
        {
            _NodeList = NodeList;
            this.SetBinding(ContentPage.TitleProperty, "Name");

            int clients = 0;
            Button btnRefresh = new Button();
            btnRefresh.Text = "Aktualisieren";
            btnRefresh.Clicked += (sender, args) =>
            {
                MessagingCenter.Send<FFStraelen.MainPage>(new FFStraelen.MainPage(false), "Refresh");
            };

            foreach (Freifunk.FFRecord fr in NodeList)
            {
                TextCell tc = new TextCell();
                tc.Text = fr.NodeName + " (" + fr.NodeID.ToString() + ")";
                tc.Detail = fr.CurrentClients.ToString() + " Clients/zul.ges.vor " + fr.LastSeenMinutes.ToString() + " min";

                // Only set Tap event if Detail Informations exists on the server
                if (fr.Description != null)
                {
                    tc.Tapped += (sender, args) =>
                    {
                        var tcs = sender as TextCell;
                        if (tcs == null)
                            return;

                        var nlist = from n in NodeList
                                                      where tcs.Text == n.NodeName + " (" + n.NodeID.ToString() + ")"
                                                      select n;
                        if (nlist.Count() == 0)
                            return;
                        FFRecord ff = nlist.FirstOrDefault();
                        Navigation.PushAsync(new DetailPage(ff));
                        // Reset the selected item
                    };
                }

                if (fr.Online)
                {
                    tc.TextColor = Color.Green;
                    tsonline.Add(tc);
                    clients += fr.CurrentClients;
                }
                else
                {
                    tc.TextColor = Color.Red;
                    tsoffline.Add(tc);
                }
            }

            tr.Add(tsonline);
            tr.Add(tsoffline);

            NodeTable.Root = tr;
            NodeTable.Intent = TableIntent.Menu;
            this.Content = new StackLayout
            {
                Spacing = 0,
                Children =
                {
                    btnRefresh,
                    NodeTable
                }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ReloadAllowed = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ReloadAllowed = false;
        }

        public void GPSChangend(GPSInfo gpsinfo)
        {
            //Map map = new Map ();
            foreach (TextCell tc in tsonline)
            {
                IFormatProvider en = new CultureInfo("en-US");
                //IFormatProvider de = new CultureInfo("de-DE");
                double lon = 0;
                double lat = 0;
                int nodenr = 0;

                foreach (Freifunk.FFRecord fr in _NodeList)
                {
                    if (tc.Text == fr.NodeName + " (" + fr.NodeID.ToString() + ")")
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
                string searched = " / Entf. ";
                int index = tc.Detail.IndexOf(searched);
                if (index < 0)
                    tc.Detail += searched + String.Format("{0:##,##0} m", distance);
                else
                {
                    tc.Detail = tc.Detail.Substring(0, index) + searched + String.Format("{0:##,##0} m", distance);
                }
                var item = (from n in _NodeList
                                        where n.NodeID == nodenr
                                        select n).FirstOrDefault();
                item.Distance = (Int64)distance;
            }

        }
    }
}
