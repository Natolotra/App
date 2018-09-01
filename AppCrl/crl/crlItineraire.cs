
using System.Collections.Generic;

namespace arch.crl
{
    /// <summary>
    /// Objet itineraire
    /// </summary>
    public class crlItineraire
    {
        #region variable
        private string idItineraire;
        private int distanceParcour;
        private int nombreRepos;
        private string dureeTrajet;
        private string numVilleItineraireDebut;
        private string numVilleItineraireFin;
        private string numInfoBagage;
        #endregion

        #region encapsulation
        public string IdItineraire
        {
            get { return idItineraire; }
            set { idItineraire = value; }
        }
        public int DistanceParcour
        {
            get { return distanceParcour; }
            set { distanceParcour = value; }
        }
        public int NombreRepos
        {
            get
            {
                return nombreRepos;
            }
            set
            {
                nombreRepos = value;
            }
        }
        public string DureeTrajet
        {
            get { return dureeTrajet; }
            set { dureeTrajet = value; }
        }
        public string NumVilleItineraireDebut
        {
            get
            {
                return numVilleItineraireDebut;
            }
            set
            {
                numVilleItineraireDebut = value;
            }
        }
        public string NumVilleItineraireFin
        {
            get
            {
                return numVilleItineraireFin;
            }
            set
            {
                numVilleItineraireFin = value;
            }
        }
        public string NumInfoBagage
        {
            get
            {
                return numInfoBagage;
            }
            set
            {
                numInfoBagage = value;
            }
        }
        #endregion

        #region variable d'objet
        public crlVille villeD;
        public crlVille villeF;
        public List<crlVille> villes;
        public crlInfoExedantBagage infoExedantBagage;
        public List<crlRouteNationale> routeNationale;
        public List<crlTrajet> trajets;
        #endregion

        #region constructeur
        public crlItineraire()
        {
            this.DistanceParcour = 0;
            this.DureeTrajet = "00:00";
            this.IdItineraire = "";
            this.NumVilleItineraireDebut = "";
            this.NumVilleItineraireFin = "";
            this.NumInfoBagage = "";
            this.nombreRepos = 0;
            this.infoExedantBagage = null;
            this.villeD = null;
            this.villeF = null;
            this.villes = null;
            this.routeNationale = null;
            this.trajets = null;
        }
        #endregion
    }
}
