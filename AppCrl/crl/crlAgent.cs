using System;

namespace arch.crl
{
    /// <summary>
    /// Objet Agent
    /// </summary>
    public class crlAgent
    {
        #region variable
        private string MatriculeAgent;
        private string NomAgent;
        private string PrenomAgent;
        private DateTime DateNaissanceAgent;
        private string LieuNaissanceAgent;
        private string LoginAgent;
        private string MotDePasseAgent;
        private string CinAgent;
        private string AdresseAgent;
        private string TelephoneAgent;
        private string TelephoneMobileAgent;
        private string TypeAgent;
        private string NumAgence;
        private string imageAgent;
        private string situationFamilialeAgent;
        #endregion

        #region Variable d'objet
        public crlAgence agence;
        public crlTypeAgent typeAgentObj;
        public crlSessionCaisse sessionCaisse;
        #endregion

        #region encapsulation
        public string matriculeAgent
        {
            get
            {
                return MatriculeAgent;
            }
            set
            {
                MatriculeAgent = value;
            }
        }
        public string nomAgent
        {
            get
            {
                return NomAgent;
            }
            set
            {
                NomAgent = value;
            }
        }
        public string prenomAgent
        {
            get
            {
                return PrenomAgent;
            }
            set
            {
                PrenomAgent = value;
            }
        }
        public DateTime dateNaissanceAgent
        {
            get
            {
                return DateNaissanceAgent;
            }
            set
            {
                DateNaissanceAgent = value;
            }
        }
        public string lieuNaissanceAgent
        {
            get
            {
                return LieuNaissanceAgent;
            }
            set
            {
                LieuNaissanceAgent = value;
            }
        }
        public string loginAgent
        {
            get
            {
                return LoginAgent;
            }
            set
            {
                LoginAgent = value;
            }
        }
        public string motDePasseAgent
        {
            get
            {
                return MotDePasseAgent;
            }
            set
            {
                MotDePasseAgent = value;
            }
        }
        public string cinAgent
        {
            get
            {
                return CinAgent;
            }
            set
            {
                CinAgent = value;
            }
        }
        public string adresseAgent
        {
            get
            {
                return AdresseAgent;
            }
            set
            {
                AdresseAgent = value;
            }
        }
        public string telephoneAgent
        {
            get
            {
                return TelephoneAgent;
            }
            set
            {
                TelephoneAgent = value;
            }
        }
        public string telephoneMobileAgent
        {
            get
            {
                return TelephoneMobileAgent;
            }
            set
            {
                TelephoneMobileAgent = value;
            }
        }
        public string typeAgent
        {
            get
            {
                return TypeAgent;
            }
            set
            {
                TypeAgent = value;
            }
        }
        public string numAgence
        {
            get
            {
                return NumAgence;
            }
            set
            {
                NumAgence = value;
            }
        }
        public string ImageAgent
        {
            get
            {
                return imageAgent;
            }
            set
            {
                imageAgent = value;
            }
        }
        public string SituationFamilialeAgent
        {
            get
            {
                return situationFamilialeAgent;
            }
            set
            {
                situationFamilialeAgent = value;
            }
        }
        
        #endregion

        #region constructeur
        public crlAgent()
        {
            this.adresseAgent = "";
            this.cinAgent = "";
            this.dateNaissanceAgent = DateTime.Now;
            this.lieuNaissanceAgent = "";
            this.loginAgent = "";
            this.matriculeAgent = "";
            this.motDePasseAgent = "";
            this.nomAgent = "";
            this.numAgence = "";
            this.prenomAgent = "";
            this.telephoneAgent = "";
            this.telephoneMobileAgent = "";
            this.typeAgent = "";
            this.ImageAgent = "agent.png";
            this.SituationFamilialeAgent = "";

            agence = null;
            typeAgentObj = null;
            sessionCaisse = null;
        }
        #endregion
    }
}