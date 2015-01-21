using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Xml;
using FFStraelen.Android;
using System.Net;
using System.IO;
using System.Text;

[assembly: Xamarin.Forms.Dependency(typeof(GetNodeList_Android))]

namespace FFStraelen.Android
{
    public class GetNodeList_Android:FFStraelen.IGetNodeList
    {
        public GetNodeList_Android()
        { 
        }

        public List<Freifunk.FFRecord> GetCompleteNodeList()
        {
            List<Freifunk.FFRecord> fflist = new List<Freifunk.FFRecord>();
            var request = HttpWebRequest.Create(@"http://ff.straelen.eu/ffservice.asmx/GetCompleteNodeData");
            request.ContentType = "text/xml";
            request.Method = "GET";
            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        Console.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        var content = reader.ReadToEnd();

                        using (XmlReader rd = XmlReader.Create(new StringReader(content)))
                        {
                            XmlWriterSettings ws = new XmlWriterSettings();
                            ws.Indent = true;
                            // Parse the file and display each of the nodes.
                            rd.MoveToContent();
                            Freifunk.FFRecord frrecord = new Freifunk.FFRecord();
                            while (rd.Read())
                            {

                                switch (rd.NodeType)
                                {
                                    case XmlNodeType.Element:
                                        if (rd.Name == "FFRecord")
                                        {
                                            if (frrecord.NodeID != 0)
                                                fflist.Add(frrecord);
                                            frrecord = new Freifunk.FFRecord();
                                        }
                                        else
                                        {
                                            switch (rd.Name)
                                            {
                                                case "NodeID":
                                                    frrecord.NodeID = int.Parse(rd.ReadString());
                                                    break;
                                                case "NodeName":
                                                    frrecord.NodeName = rd.ReadString();
                                                    break;
                                                case "AdminURL":
                                                    frrecord.AdminURL = rd.ReadString();
                                                    break;
                                                case "CurrentClients":
                                                    frrecord.CurrentClients = int.Parse(rd.ReadString());
                                                    break;
                                                case "Firmware":
                                                    frrecord.Firmware = rd.ReadString();
                                                    break;
                                                case "GWQ":
                                                    frrecord.GWQ = int.Parse(rd.ReadString());
                                                    break;
                                                case "HWID":
                                                    frrecord.HWID = rd.ReadString();
                                                    break;
                                                case "IPv6":
                                                    frrecord.IPv6 = rd.ReadString();
                                                    break;
                                                case "LastSeen":
                                                    frrecord.LastSeen = rd.ReadString();
                                                    break;
                                                case "LastSeenJSON":
                                                    string st = rd.ReadString();
                                                    int i = 0;
                                                    int.TryParse(st, out i);
                                                    frrecord.LastSeenJSON = i;
                                                    break;
                                                case "LastSeenMinutes":
                                                    frrecord.LastSeenMinutes = int.Parse(rd.ReadString());
                                                    break;
                                                case "Lat":
                                                    frrecord.Lat = rd.ReadString();
                                                    break;
                                                case "Long":
                                                    frrecord.Long = rd.ReadString();
                                                    break;
                                                case "MapURL":
                                                    frrecord.MapURL = rd.ReadString();
                                                    break;
                                                case "MeshLinks":
                                                    frrecord.MeshLinks = rd.ReadString();
                                                    break;
                                                case "MeshNodes":
                                                //frrecord.MeshNodes = rd.ReadString();
                                                    break;
                                                case "NodeURL":
                                                    frrecord.NodeURL = rd.ReadString();
                                                    break;
                                                case "VPNActive":
                                                    frrecord.VPNActive = (rd.ReadString() == "True");
                                                    break;
                                                case "Address":
                                                    frrecord.Address = rd.ReadString();
                                                    break;
                                                case "Phone":
                                                    frrecord.Phone = rd.ReadString();
                                                    break;
                                                case "Fax":
                                                    frrecord.Fax = rd.ReadString();
                                                    break;
                                                case "Mail":
                                                    frrecord.Mail = rd.ReadString();
                                                    break;
                                                case "Web":
                                                    frrecord.Web = rd.ReadString();
                                                    break;
                                                case "Description":
                                                    frrecord.Description = rd.ReadString();
                                                    break;
                                                case "OpeningHours":
                                                    frrecord.OpeningHours = rd.ReadString();
                                                    break;
                                                case "LogoURL":
                                                    frrecord.LogoURL = rd.ReadString();
                                                    break;
                                            }
                                        }

                                        break;
                                    case XmlNodeType.Text:
                                        break;
                                    case XmlNodeType.XmlDeclaration:
                                    case XmlNodeType.ProcessingInstruction:
                                        break;
                                    case XmlNodeType.Comment:
                                        break;
                                    case XmlNodeType.EndElement:
                                        break;
                                }
                            }
                            if (frrecord.NodeID != 0)
                                fflist.Add(frrecord);
                        }
                        // do something with downloaded string, do UI interaction on main thread       
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return fflist;
        }

        public Freifunk.FFRecord GetNodeDetails(int node)
        {
            return null;
        }
    }
}

