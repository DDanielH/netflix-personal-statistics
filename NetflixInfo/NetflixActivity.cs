using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetflixInfo
{
    class NetflixActivity
    {
        public string codeName { get; set; }
        public int page { get; set; }
        public int size { get; set; }
        public int trkid { get; set; }
        public string tz { get; set; }
        public List<NetflixItem> viewedItems { get; set; }
    }
}
