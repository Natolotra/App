using System;

namespace arch.crl
{
    /// <summary>
    /// Objet chauffeur
    /// </summary>
    public class crlChauffeur
    {
        #region variable
        private string IdChauffeur;
        private string NomChauffeur;
        private string PrenomChauffeur;
        private string CinChauffeur;
        private string AdresseChauffeur;
        private string TelephoneChauffeur;
        private string TelephoneMobileChauffeur;
        private string imageChauffeur;
        private string numCooperative;
        private string situationFamilialeChauffeur;
        private DateTime dateNaissanceChauffeur;
        private string lieuNaissanceChauffeur;
        #endregion

        #region ecapsulation
        public string idChauffeur
        {
            get
            {
                return IdChauffeur;
            }
            set
            {
                IdChauffeur = value;
            }
        }
        public string nomChauffeur
        {
            get
            {
                return NomChauffeur;
            }
            set
            {
                NomChauffeur = value;
            }
        }
        public string prenomChauffeur
        {
            get
            {
                return PrenomChauffeur;
            }
            set
            {
                PrenomChauffeur = value;
            }
        }
        public string cinChauffeur
        {
            get
            {
                return CinChauffeur;
            }
            set
            {
                CinChauffeur = value;
            }
        }
        public string adresseChauffeur
        {
            get
            {
                return AdresseChauffeur;
            }
            set
            {
                AdresseChauffeur = value;
            }
        }
        public string telephoneChauffeur
        {
            get
            {
                return TelephoneChauffeur;
            }
            set
            {
                TelephoneChauffeur = value;
            }
        }
        public string telephoneMobileChauffeur
        {
            get
            {
                return TelephoneMobileChauffeur;
            }
            set
            {
                TelephoneMobileChauffeur = value;
            }
        }
        public string ImageChauffeur
        {
            get
            {
                return imageChauffeur;
            }
            set
            {
                imageChauffeur = value;
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
        public string SituationFamilialeChauffeur
        {
            get
            {
                return situationFamilialeChauffeur;
            }
            set
            {
                situationFamilialeChauffeur = value;
            }
        }
        public DateTime DateNaissanceChauffeur
        {
            get
            {
                return dateNaissanceChauffeur;
            }
            set
            {
                dateNaissanceChauffeur = value;
            }
        }
        public string LieuNaissanceChauffeur
        {
            get
            {
                return lieuNaissanceChauffeur;
            }
            set
            {
                lieuNaissanceChauffeur = value;
            }
        }
        #endregion

        #region constructeur
        public crlChauffeur()
        {
            this.idChauffeur = "";
            this.nomChauffeur = "";
            this.prenomChauffeur = "";
            this.adresseChauffeur = "";
            this.cinChauffeur = "";
            this.telephoneChauffeur = "";
            this.telephoneMobileChauffeur = "";
            this.ImageChauffeur = "chauffeur.png";
            this.NumCooperative = "";
            this.SituationFamilialeChauffeur = "";
            this.DateNaissanceChauffeur = DateTime.Now;
            this.LieuNaissanceChauffeur = "";
        }
        #endregion
    }
}
