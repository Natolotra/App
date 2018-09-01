using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace arch.crl
{
    /// <summary>
    /// Objet de l'abonnement par nombre ne voyage
    /// </summary>
    public class crlUSAbonnementNV
    {
        #region variable
        string numAbonnementNV;
        DateTime dateValideAu;
        string numAbonnement;
        string numZoneD;
        string numZoneF;
        string numCarte;
        #endregion

        #region accesseur
        public string NumAbonnementNV
        {
            get
            {
                return numAbonnementNV;
            }
            set
            {
                numAbonnementNV = value;
            }
        }
        public DateTime DateValideAu
        {
            get
            {
                return dateValideAu;
            }
            set
            {
                dateValideAu = value;
            }
        }
        public string NumAbonnement
        {
            get
            {
                return numAbonnement;
            }
            set
            {
                numAbonnement = value;
            }
        }
        public string NumZoneD
        {
            get
            {
                return numZoneD;
            }
            set
            {
                numZoneD = value;
            }
        }
        public string NumZoneF
        {
            get
            {
                return numZoneF;
            }
            set
            {
                numZoneF = value;
            }
        }
        public string NumCarte
        {
            get
            {
                return numCarte;
            }
            set
            {
                numCarte = value;
            }
        }
        #endregion

        #region variable d'objet
        public List<crlUSAbonnementNVDevis> abonnementNVDevis;
        #endregion

        #region constructeur
        public crlUSAbonnementNV()
        {
            this.NumAbonnementNV = "";
            this.DateValideAu = DateTime.Now;
            this.NumAbonnement = "";
            this.NumZoneD = "";
            this.NumZoneF = "";
            this.NumCarte = "";

            this.abonnementNVDevis = null;
        }
        #endregion
    }
}