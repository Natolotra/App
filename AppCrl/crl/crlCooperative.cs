
namespace arch.crl
{
    /// <summary>
    /// Objet Cooperative
    /// Les societes de transport
    /// </summary>
    public class crlCooperative
    {
        #region declaration
        private string numCooperative;
        private string nomCooperative;
        private string sigleCooperative;
        private string adresseCooperative;
        private string zone;
        private string telephoneFixeCooperative;
        private string telephoneMobileCooperative;
        private string numVille;
        private string nomResponsable;
        private string prenomResponsable;
        private string cinResponsable;
        private string adressseResponsable;
        private string telephoneFixeResponsable;
        private string telephoneMobileResponsable;
        #endregion

        #region encapsulation
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
        public string NomCooperative
        {
            get
            {
                return nomCooperative;
            }
            set
            {
                nomCooperative = value;
            }
        }
        public string SigleCooperative
        {
            get
            {
                return sigleCooperative;
            }
            set
            {
                sigleCooperative = value;
            }
        }
        public string AdresseCooperative
        {
            get
            {
                return adresseCooperative;
            }
            set
            {
                adresseCooperative = value;
            }
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
        public string TelephoneFixeCooperative
        {
            get
            {
                return telephoneFixeCooperative;
            }
            set
            {
                telephoneFixeCooperative = value;
            }
        }
        public string TelephoneMobileCooperative
        {
            get
            {
                return telephoneMobileCooperative;
            }
            set
            {
                telephoneMobileCooperative = value;
            }
        }
        public string NumVille
        {
            get
            {
                return numVille;
            }
            set
            {
                numVille = value;
            }
        }
        public string NomResponsable
        {
            get
            {
                return nomResponsable;
            }
            set
            {
                nomResponsable = value;
            }
        }
        public string PrenomResponsable
        {
            get
            {
                return prenomResponsable;
            }
            set
            {
                prenomResponsable = value;
            }
        }
        public string CinResponsable
        {
            get
            {
                return cinResponsable;
            }
            set
            {
                cinResponsable = value;
            }
        }
        public string AdressseResponsable
        {
            get
            {
                return adressseResponsable;
            }
            set
            {
                adressseResponsable = value;
            }
        }
        public string TelephoneFixeResponsable
        {
            get
            {
                return telephoneFixeResponsable;
            }
            set
            {
                telephoneFixeResponsable = value;
            }
        }
        public string TelephoneMobileResponsable
        {
            get
            {
                return telephoneMobileResponsable;
            }
            set
            {
                telephoneMobileResponsable = value;
            }
        }
        #endregion

       
        #region constructeur
        public crlCooperative()
        {
            this.NumCooperative = "";
            this.NomCooperative = "";
            this.SigleCooperative = "";
            this.AdresseCooperative = "";
            this.Zone = "";
            this.TelephoneFixeCooperative = "";
            this.TelephoneMobileCooperative = "";
            this.NumVille = "";
            this.NomResponsable = "";
            this.PrenomResponsable = "";
            this.AdressseResponsable = "";
            this.CinResponsable = "";
            this.TelephoneFixeResponsable = "";
            this.TelephoneMobileResponsable = "";
        }
        #endregion
    }
}