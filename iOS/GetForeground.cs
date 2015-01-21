using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Xml;
using FFStraelen.iOS;
using System.Net;
using System.IO;
using System.Text;

[assembly: Xamarin.Forms.Dependency(typeof(GetForeground_iOS))]

namespace FFStraelen.iOS
{
    public class GetForeground_iOS:FFStraelen.IGetForeground
    {

        public event EventHandler BecomeForeground;

        public void OnBecomeForeground(EventArgs e)
        {
            if (BecomeForeground != null)
                BecomeForeground(this, e);
        }
    }
}

