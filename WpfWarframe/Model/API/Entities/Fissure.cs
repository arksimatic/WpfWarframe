using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfWarframe.Model.API.Entities
{
    public class Fissure
    {
        public string id { get; set; }
        public DateTime activation { get; set; }
        public string startString { get; set; }
        public DateTime expiry { get; set; }
        public bool active { get; set; }
        public string node { get; set; }
        public string missionType { get; set; }
        public string enemy { get; set; }
        public string enemyKey { get; set; }
        public string nodeKey { get; set; }
        public string tier { get; set; }
        public int tierNum { get; set; }
        public bool expired { get; set; }
        public string eta { get; set; }

        private String remainingTime()
        {
            TimeSpan ts = expiry.AddHours(1) - DateTime.Now;
            String result;

            if (ts > TimeSpan.Zero)
                result = ts.ToString().Split('.')[0];
            else
                result = TimeSpan.Zero.ToString().Split('.')[0];

            return result;
        }

        public override string ToString()
        {
            return $"{node}\n" +
                   $"{missionType} {enemyKey} \n" +
                   $"{tier} {remainingTime()}";
        }
    }
}
