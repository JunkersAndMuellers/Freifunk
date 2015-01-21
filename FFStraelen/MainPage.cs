using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using System.Threading.Tasks;

namespace FFStraelen
{
    public class MainPage : TabbedPage
    {
        public MainPage(bool StartMessageListener)
        {
            this.Title = "Freifunk Straelen";
            if (StartMessageListener)
            {
                MessagingCenter.Subscribe<MainPage>(this, "Refresh", (sender) =>
                    {
                        RefreshList();
                    });
            }
            RefreshList();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        public void GPSChanged(GPSInfo gpsinfo)
        {
            foreach (Page p in this.Children)
            {
                if (p.GetType() == typeof(ListPage))
                    ((ListPage)p).GPSChangend(gpsinfo);
                if (p.GetType() == typeof(MapPage))
                    ((MapPage)p).GPSChangend(gpsinfo);
                //string st = p.ToString ();
            }
        }

        public void RefreshList()
        {
            Children.Clear();
            this.IsBusy = true;

            this.Title = "Lade Daten ........";
            IGetNodeList igl;
            List<Freifunk.FFRecord> NodeList = new List<Freifunk.FFRecord>();

            igl = DependencyService.Get<IGetNodeList>();

            List<Freifunk.FFRecord> noli = igl.GetCompleteNodeList();
            NodeList = noli;

            if (NodeList == null)
            {

            }

            var nlist = from nl in NodeList
                                 orderby nl.NodeName
                                 where nl.NodeName != null
                                 select nl;
            NodeList = nlist.ToList();

            int nodesonline = 0;
            int clients = 0;
            foreach (Freifunk.FFRecord fr in NodeList)
            {
                if (fr.Online)
                {
                    nodesonline++;
                    clients += fr.CurrentClients;
                }
            }
            this.Title = "Freifunk - Straelen (" + nodesonline.ToString() + "/" + clients.ToString() + ")";
            Children.Clear();
            Children.Add(new ListPage(NodeList) { Title = "Liste", Icon = "List.png" });
            Children.Add(new MapPage(NodeList) { Title = "Karte", Icon = "Map.png" });
            Children.Add(new InfoPage() { Title = "Info", Icon = "Info.png" });
            this.IsBusy = false;
        }
    }
}