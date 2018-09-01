using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Queries.Trajet
{
    public class TrajetDto
    {
        public string numTrajet { get; set; }
        public int? distanceTrajet { get; set; }
        public string dureeTrajet { get; set; }
        public string numVilleD { get; set; }
        public string numVilleF { get; set; }
        public string numTarifBaseBillet { get; set; }
        public string VilleD { get; set; }
        public string VilleF { get; set; }
    }
}
