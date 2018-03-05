using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetflixInfo
{
    class NetflixItem
    {
        public string title { get; set; }
        public string videoTitle { get; set; }
        public int movieID { get; set; }
        public string country { get; set; }
        public int bookmark { get; set; }
        public int duration { get; set; }
        public long date { get; set; }
        public int deviceType { get; set; }
        public string dateStr { get; set; }
        public int index { get; set; }
        public string topNodeId { get; set; }
        public int series { get; set; }
        public string seriesTitle { get; set; }
        public string seasonDescriptor { get; set; }
        public string episodeTitle { get; set; }
        public string estRating { get; set; }

        public string csv = ";";

        public override string ToString()
        {

            return title + csv +
                   videoTitle + csv +
                   movieID + csv +
                   country + csv +
                   bookmark + csv +
                   duration + csv +
                   date + csv +
                   deviceType + csv +
                   dateStr + csv +
                   index + csv +
                   topNodeId + csv +
                   series + csv +
                   seriesTitle + csv +
                   seasonDescriptor + csv +
                   episodeTitle + csv +
                   estRating;
        }

        public static string GetTitles()
        {
            return "title;videoTitle;movieID;country;bookmark;duration;date;deviceType;dateStr;index;topNodeId;series;seriesTitle;seasonDescriptor;episodeTitle;estRating";
        }
    }
}
