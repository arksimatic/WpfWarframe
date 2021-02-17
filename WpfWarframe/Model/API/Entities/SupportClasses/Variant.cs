using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfWarframe.Model.API.Entities.SupportClasses
{
    public class Variant
    {
        public string boss { get; set; }
        public string planet { get; set; }
        public string missionType { get; set; }
        public string modifier { get; set; }
        public string modifierDescription { get; set; }
        public string node { get; set; }

        public override string ToString()
        {
            return $"{missionType} on {node}({planet}) with {modifier}";
        }
    }
}
