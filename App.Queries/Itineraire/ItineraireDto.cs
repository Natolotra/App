using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Queries.Itineraire
{
    public class ItineraireDto
    {
        public string idItineraire { get; set; }
        public int distanceParcour { get; set; }
        public int nombreRepos { get; set; }
        public string dureeTrajet { get; set; }
        public string numVilleItineraireDebut { get; set; }
        public string numVilleItineraireFin { get; set; }
        public string numInfoBagage { get; set; }
        public string VilleD { get; set; }
        public string VilleF { get; set; }
    }
}
