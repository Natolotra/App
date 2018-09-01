using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Objet Trajet
    /// </summary>
    public class crlTrajet
    {
        #region variable
        private string numTrajet;
        private string numVilleD;
        private string numVilleF;
        private int distanceTrajet;
        private string dureeTrajet;
        private string numTarifBaseBillet;
        #endregion

        #region encapsulation
        public string NumTrajet
        {
            get
            {
                return numTrajet;
            }
            set
            {
                numTrajet = value;
            }
        }
        public string NumVilleD
        {
            get
            {
                return numVilleD;
            }
            set
            {
                numVilleD = value;
            }
        }
        public string NumVilleF
        {
            get
            {
                return numVilleF;
            }
            set
            {
                numVilleF = value;
            }
        }
        public int DistanceTrajet
        {
            get
            {
                return distanceTrajet;
            }
            set
            {
                distanceTrajet = value;
            }
        }
        public string DureeTrajet
        {
            get
            {
                return dureeTrajet;
            }
            set
            {
                dureeTrajet = value;
            }
        }
        public string NumTarifBaseBillet
        {
            get
            {
                return numTarifBaseBillet;
            }
            set
            {
                numTarifBaseBillet = value;
            }
        }
        #endregion

        #region variable d'objet
        public crlVille villeD;
        public crlVille villeF;
        public crlTarifBaseBillet tarifBaseBillet;
        public List<crlTarifBaseCommission> tarifBaseCommissions;
        #endregion

        #region constructeur
        public crlTrajet()
        {
            this.NumTrajet = "";
            this.NumVilleD = "";
            this.NumVilleF = "";
            this.DistanceTrajet = 0;
            this.DureeTrajet = "00:00";
            this.NumTarifBaseBillet = "";
            this.villeD = null;
            this.villeF = null;
            this.tarifBaseBillet = null;
            this.tarifBaseCommissions = null;
        }
        #endregion
    }
}