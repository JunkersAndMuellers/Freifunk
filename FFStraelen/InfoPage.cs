using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Linq;
using Freifunk;
using System.Collections.Generic;
using System.Globalization;

namespace FFStraelen
{
    public class InfoPage : ContentPage
    {

        public InfoPage()
        {
            this.Content = SetContent();
        }

        public View SetContent()
        {
            var logo = new Image { Aspect = Aspect.AspectFit, WidthRequest = 100, HeightRequest = 100 };
            logo.Source = ImageSource.FromFile("freewifi.png");

            // ********* Web ****************
            var lblWeb = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = "\nWeb : freifunk.straelen.eu",
                TextColor = Color.Green,
                Font = Device.OnPlatform(
                    iOS: Font.OfSize("GillSans-Light", 16),
                    Android: Font.OfSize("Droid Sans", 16),
                    WinPhone: Font.OfSize("Segoe UI", 18)
                ),
            };

            TapGestureRecognizer tabweb = new TapGestureRecognizer();
            tabweb.Tapped += (object sender, EventArgs e) =>
                {
                    Device.OpenUri(new Uri("http://freifunk.straelen.eu"));

                };
            lblWeb.GestureRecognizers.Add(tabweb);

            // ********* Mail ****************
            var lblMail = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = "eMail : freifunk@straelen.eu",
                TextColor = Color.Green,
                Font = Device.OnPlatform(
                    iOS: Font.OfSize("GillSans-Light", 16),
                    Android: Font.OfSize("Droid Sans", 16),
                    WinPhone: Font.OfSize("Segoe UI", 18)
                ),
            };

            TapGestureRecognizer tabmail = new TapGestureRecognizer();
            tabmail.Tapped += (object sender, EventArgs e) =>
                {
                    Device.OpenUri(new Uri("mailto:freifunk@straelen.eu"));

                };
            lblMail.GestureRecognizers.Add(tabmail);

            // ********* Description ****************
            string txtDescription =
                "\nFreifunk Straelen ist eine nichtkommerzielle Initiative. Unser Ziel ist ein flächendeckender drahtloser und kostenloser Internetzugang in Straelen für alle bereit zu stellen um die Attraktivität zu erhöhen. Zusätzlich schaffen wir ein Netzwerk, das den Austausch von Informationen ermöglicht und begünstigt und dabei unabhängig von Unternehmen und Behörden betrieben werden kann.\n\nEs greift das Prinzip des Gebens und Nehmens. Jeder stellt einen Teil seines ohnehin vorhandenen Internetzugangs für die Allgemeinheit zur Verfügung. Dies hat, in den meisten Fällen, keinen merklichen   Effekt auf die eigene Internet-Geschwindigkeit. Im Gegenzug stehen dann jedem diese Hotspots des Netzwerkes zur Verfügung.\n\nNatürlich auch den Besuchern aus dem In- und Ausland. Im Idealfall spannt sich so ein flächendeckendes   Netz   aus   Zugangspunkten   über   unsere   gesamte   Stadt   und   der Internetzugang ist zu jeder Zeit und an jedem Ort gewährleistet.\n\nDer Schutz persönlicher Daten ist eines unserer höchsten Güter. Die Freifunk-Zugangspunkte trennen das Freifunk-Netz vollständig vom jeweiligen privaten WLAN. Niemand, der den Zugangspunkt eines Internetspenders verwendet erhält in irgendeiner Weise Zugriff auf den Datenverkehr dessen privaten Netzes.\n\nDurch ihre dezentrale Struktur haben Freifunk-Netze eine hohe Ausfallsicherheit. Es gibt niemanden, dem alles gehört. Also können die Netze auch nicht einfach abgeschaltet werden. Jedes Gerät gehört dem jeweiligen Betreiber.\n\nDie Freifunk-Geräte verbinden sich untereinander und tauschen so Daten aus. Je mehr Geräte es gibt, desto leistungsfähiger wird das Netz. So ist es beispielsweise nicht schlimm, wenn der Internetzugang an einem Gerät ausfällt. Solange ein anderes Gerät mit Internetzugang in der Nähe ist, hat auch das getrennte Gerät Internetzugang.\n\nEin Freifunk-Netz ist also mehr als eine große Menge kostenloser Hotspots. Es funktioniert wie ein „lokales Internet“. In diesem Netzwerk sind alle Angebote möglich, die wir auch aus dem „normalen“ Internet kennen, allerdings beschränkt auf die lokale Ebene. Es können beispielsweise  Nachrichtendienste,  Soziale  Netzwerke,  Video-  und  Musikdatenbank  und vieles mehr betrieben werden, ohne auf kommerzielle Anbieter angewiesen zu sein.\n\nZukünftig ist geplant, lokale Angebote einzuführen, die es Gewerbetreibenden ermöglicht Kunden zu gewinnen und Zusatzangebote zu unterbreiten.\n\nWir sind davon überzeugt, dass diese Maßnahme die Attraktivität der Stadt Straelen erhöht und wir viele neue Gäste begrüßen dürfen.\n\nWenn Sie fragen haben, wenden Sie sich an Christian Nowak oder Axel Stallknecht Sie erreichen uns über eine Mail an freifunk@straelen.eu";
                
            var lblDescription = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = txtDescription,
                TextColor = Color.Green,
                Font = Device.OnPlatform(
                    iOS: Font.OfSize("GillSans-Light", 16),
                    Android: Font.OfSize("Droid Sans", 16),
                    WinPhone: Font.OfSize("Segoe UI", 18)
                ),
            };

            StackLayout stack = new StackLayout()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(10, Device.OnPlatform(0, 0, 0), 10, 10),
                Spacing = 5,
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    logo,
                    lblWeb,
                    lblMail,
                    lblDescription
                },

            };
            ScrollView sv = new ScrollView();
            sv.Content = stack;

            return sv;
        }
    }
}

