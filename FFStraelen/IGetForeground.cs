using System;
using System.Threading;
using System.Threading.Tasks;

namespace FFStraelen
{
    public interface IGetForeground
    {
        //
        // Events
        //
        event EventHandler BecomeForeground;
        void OnBecomeForeground(EventArgs e);
    }
}
