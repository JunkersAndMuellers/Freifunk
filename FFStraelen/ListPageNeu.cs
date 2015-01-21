using System;
using System.Collections.Generic;
using System.Reflection;
using Xamarin.Forms;
using System.Linq;
using Freifunk;
using Xamarin.Forms.Maps;
using System.Globalization;
using System.Collections.ObjectModel;

namespace FFStraelen
{
    public class Node
    {
        public string NodeName { get; set; }
        public int NodeID { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string Details { get; set; }
    }

    class ListPageNeu: ContentPage
    {
        ObservableCollection<Node> Nodes;
        ListView list = new ListView();

        public ListPageNeu(List<Freifunk.FFRecord> NodeList)
        {
            Nodes = new ObservableCollection<Node>();

            foreach (FFRecord ffr in NodeList)
            {
                if (!ffr.Online)
                    continue;
                Node n = new Node();
                n.NodeID = ffr.NodeID;
                n.NodeName = ffr.NodeName + " (" + ffr.NodeID.ToString() + ")";
                n.Details = ffr.CurrentClients.ToString() + " Clients/zul.ges.vor " + ffr.LastSeenMinutes.ToString() + " min";
                n.Lat = ffr.Lat;
                n.Lon = ffr.Long;
                Nodes.Add(n);
            }
            //Nodes = NodeList;
            Title = "Nodes";
            list.ItemsSource = Nodes;

            var cell = new DataTemplate(typeof(TextCell));
            cell.SetBinding(TextCell.TextProperty, "NodeName");
            cell.SetBinding(TextCell.DetailProperty, "Details");
            //cell.SetBinding(TextCell.TextColorProperty, "Color.Green);

            list.ItemTemplate = cell;
            list.ItemTapped += (sender, args) =>
            {
                var node = args.Item as Node;
                if (node == null)
                    return;

                var nlist = from n in NodeList
                            where n.NodeName + " (" + n.NodeID.ToString() + ")" ==  node.NodeName
                                        select n;
                if (nlist.Count() == 0)
                        return;
                //FFRecord ff = nlist.FirstOrDefault();
                //Navigation.PushAsync(new DetailPage(ff));
                // Reset the selected item
                list.SelectedItem = null;
            };

            var stack = new StackLayout
            {
                Children = { list }
            };

            Content = stack;
        }

        public void GPSChangend(GPSInfo gpsinfo)
        {
            //Map map = new Map ();
            foreach (Node tc in Nodes)
            {
                IFormatProvider en = new CultureInfo("en-US");
                //IFormatProvider de = new CultureInfo ("de-DE");
                double lon = 0;
                double lat = 0;

                foreach (Node fr in Nodes)
                {
                    if (tc.NodeName == fr.NodeName)
                    {
                        double.TryParse(fr.Lat, NumberStyles.Float, en, out lat);
                        double.TryParse(fr.Lon, NumberStyles.Float, en, out lon);
                        break;
                    }
                }
                double distance = 
                    FFStraelen.Location.TrackingHelper.CalculateDistance(
                        new FFStraelen.Location.Location() { Latitude = gpsinfo.Latitude, Longitude = gpsinfo.Longitude },
                        new FFStraelen.Location.Location() { Latitude = lat, Longitude = lon });

                string searched = " / Entf. ";
                int index = tc.Details.IndexOf(searched);
                if (index < 0)
                    tc.Details += searched + String.Format(en, "{0:##.##0} m", distance);
                else
                {
                    tc.Details = tc.Details.Substring(0, index) + searched + String.Format(en, "{0:##.##0} m", distance);
                }

            }
        }
    }
}
