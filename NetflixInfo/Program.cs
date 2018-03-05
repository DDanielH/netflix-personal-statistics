using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NetflixInfo
{
    class Program
    {


        static void Main(string[] args)
        {

            if (!Directory.Exists("cookies"))
                return;

            var files = Directory.GetFiles("cookies");
            //Export

            foreach (var file in files)
            {
                _items = new List<NetflixItem>();
                var f = new FileInfo(file);
                var exportFolder = f.Name;
                Directory.CreateDirectory(exportFolder);

                var c = File.ReadAllText(file);
                var  i = 0;
                while (LoadJson(i++,c)){ }



                List<string> lines = new List<string> {NetflixItem.GetTitles()};
                foreach (NetflixItem item in _items)
                {
                    lines.Add(item.ToString());
                }


                System.IO.File.WriteAllLines(exportFolder+@"\Netflix.csv", lines.ToArray());
            
                Dictionary<string , int> times = new Dictionary<string, int>();
                Dictionary<string, int> count = new Dictionary<string, int>();
                foreach (var value in _items)
                {
                    if (times.ContainsKey(value.dateStr))
                    {
                        times[value.dateStr] += value.duration;
                        count[value.dateStr] += 1;
                    }
                    else
                    {
                        times.Add(value.dateStr, value.duration);
                        count.Add(value.dateStr, 1);
                    }
                    
                }

                var lines2 = new List<string>();
                lines2.Add("Date;Count;Time in s;Time in m;Time in h");
                foreach (KeyValuePair<string, int> pair in times)
                {
                    lines2.Add(pair.Key+";"+ count[pair.Key] + ";"+pair.Value + ";" + ((float)pair.Value)/60 + ";" + ((float)pair.Value) / (60*60));
                }
                System.IO.File.WriteAllLines(exportFolder + @"\NetflixTimes.csv", lines2.ToArray());
                GenerateStats(exportFolder);
            }
        }

        private static void GenerateStats(string exportFolder)
        {
            var stats = new List<string>();
            //----------------------liefetime watched-------------------------------------
            float completeTime = 0;
            foreach (NetflixItem i in _items)
            {
                completeTime += i.duration;
            }
            stats.Add("Watched in s;" + completeTime);
            stats.Add("Watched in m;" + completeTime/60);
            stats.Add("Watched in h;" + completeTime/(60*60));


            //----------------------most watched serie-------------------------------------

            var serie = new Dictionary<string,int>();
            foreach (NetflixItem i in _items)
            {
                if(string.IsNullOrWhiteSpace(i.seriesTitle))
                    continue;
                if (serie.ContainsKey(i.seriesTitle))
                {
                    serie[i.seriesTitle]++;
                }
                else
                {
                    serie.Add(i.seriesTitle,1);
                }
            }

            var serieList = serie.ToList();
            serieList.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));

            for (int i = 0; i < 10; i++)
            {
                stats.Add("Most watched Serie Nr."+(i+1)+";" + serieList[i].Key+";"+serieList[i].Value);
            }

            System.IO.File.WriteAllLines(exportFolder + @"\NetflixStats.csv", stats.ToArray());

        }

        static List<NetflixItem> _items;
       
        static bool  LoadJson(int page, string cookie)
        {
            using (WebClient wc = new WebClient())
            {
                wc.Headers = new WebHeaderCollection { { "Cookie", cookie } };
                var json = wc.DownloadString("https://www.netflix.com/api/shakti/7742b8c7/viewingactivity?pg=" + page +"&pgSize=100");
                NetflixActivity stuff = JsonConvert.DeserializeObject<NetflixActivity>(json);
                if (stuff.viewedItems.Count > 0)
                {
                    foreach (NetflixItem t in stuff.viewedItems)
                    {
                        _items.Add(t);
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
