using System;
using System.Collections.Generic;

namespace arch.crl
{
    /// <summary>
    /// Objet Licence
    /// </summary>
    public class crlLicence
    {
        #region declaration 
        private string numLicence;
        private string numerosLicence;
        private string zone;
        private string numCooperative;
        private string numVehicule;
        private DateTime datePremiereMiseCiculation;
        private DateTime datePremiereExploitation;
        private DateTime valideAu;
        private DateTime valideDu;
        private int nombrePlacePayante;
        private int isProvisoire;
        #endregion

        #region encapsulation
        public string NumLicence
        {
            get
            {
                return numLicence;
            }
            set
            {
                numLicence = value;
            }
        }
        public string NumerosLicence
        {
          get { return numerosLicence; }
          set { numerosLicence = value; }
        }
        public string Zone
        {
            get
            {
                return zone;
            }
            set
            {
                zone = value;
            }
        }
        public string NumCooperative
        {
            get
            {
                return numCooperative;
            }
            set
            {
                numCooperative = value;
            }
        }
        public string NumVehicule
        {
            get
            {
                return numVehicule;
            }
            set
            {
                numVehicule = value;
            }
        }
        public DateTime DatePremiereMiseCiculation
        {
            get
            {
                return datePremiereMiseCiculation;
            }
            set
            {
                datePremiereMiseCiculation = value;
            }
        }
        public DateTime DatePremiereExploitation
        {
            get
            {
                return datePremiereExploitation;
            }
            set
            {
                datePremiereExploitation = value;
            }
        }
        public DateTime ValideAu
        {
            get
            {
                return valideAu;
            }
            set
            {
                valideAu = value;
            }
        }
        public DateTime ValideDu
        {
            get
            {
                return valideDu;
            }
            set
            {
                valideDu = value;
            }
        }
        public int NombrePlacePayante
        {
            get
            {
                return nombrePlacePayante;
            }
            set
            {
                nombrePlacePayante = value;
            }
        }
        public int IsProvisoire
        {
            get
            {
                return isProvisoire;
            }
            set
            {
                isProvisoire = value;
            }
        }
        #endregion

        #region variable d'objet
        public crlZone zoneObj;
        public crlVehicule vehicule;
        public crlCooperative cooperative;
        public List<crlItineraire> itineraires;
        #endregion

        #region constructeur
        public crlLicence()
        {
            this.NumLicence = "";
            this.Zone = "";
            this.NumCooperative = "";
            this.NumVehicule = "";
            this.DatePremiereMiseCiculation = new DateTime();
            this.DatePremiereExploitation = new DateTime();
            this.ValideAu = new DateTime();
            this.ValideDu = new DateTime();
            this.NombrePlacePayante = 0;
            this.IsProvisoire = 0;
            this.zoneObj = null;
            this.vehicule = null;
            this.cooperative = null;
            this.itineraires = null;
        }
        #endregion
    }
}