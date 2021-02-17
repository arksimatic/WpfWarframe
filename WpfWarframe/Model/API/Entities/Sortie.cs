using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfWarframe.Model.API.Entities.SupportClasses;

namespace WpfWarframe.Model.API.Entities
{
    public class Sortie
    {
        public string id { get; set; }
        public DateTime activation { get; set; }
        public string startString { get; set; }
        public DateTime expiry { get; set; }
        public bool active { get; set; }
        public string rewardPool { get; set; }
        public Variant[] variants { get; set; }
        public string boss { get; set; }
        public string faction { get; set; }
        public bool expired { get; set; }
        public string eta { get; set; }

        public override string ToString()
        {
            return $"{variants[0]}, \n" +
                   $"{variants[1]}, \n" +
                   $"{variants[2]}  \n" +
                   $"Time left: {eta}";
        }
    }
}
