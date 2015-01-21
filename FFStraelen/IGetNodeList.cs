using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFStraelen
{
    public interface IGetNodeList
    {
        List<Freifunk.FFRecord> GetCompleteNodeList();

        Freifunk.FFRecord GetNodeDetails(int node);
    }
}
