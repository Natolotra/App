using System;
namespace arch.crl
{
    /// <summary>
    /// Objet Individu
    /// </summary>
    public class crlIndividu
    {
        #region declaration
        private string numIndividu;
        private string civiliteIndividu;
        private string nomIndividu;
        private string prenomIndividu;
        private DateTime dateNaissanceIndividu;
        private string lieuNaissanceIndividu;
        private string cinIndividu;
        private string adresse;
        private string profession;
        private string telephoneFixeIndividu;
        private string telephoneMobileIndividu;
        private string mailIndividu;
        private string numQuartier;
        private int isCheque;
        private int isBonCommande;
        #endregion

        #region encapsulation
        public string NumIndividu
        {
            get
            {
                return numIndividu;
            }
            set
            {
                numIndividu = value;
            }
        }
        public string CiviliteIndividu
        {
            get
            {
                return civiliteIndividu;
            }
            set
            {
                civiliteIndividu = value;
            }
        }
        public string NomIndividu
        {
            get
            {
                return nomIndividu;
            }
            set
            {
                nomIndividu = value;
            }
        }
        public string PrenomIndividu
        {
            get
            {
                return prenomIndividu;
            }
            set
            {
                prenomIndividu = value;
            }
        }
        public DateTime DateNaissanceIndividu
        {
            get
            {
                return dateNaissanceIndividu;
            }
            set
            {
                dateNaissanceIndividu = value;
            }
        }
        public string LieuNaissanceIndividu
        {
            get
            {
                return lieuNaissanceIndividu;
            }
            set
            {
                lieuNaissanceIndividu = value;
            }
        }
        public string CinIndividu
        {
            get
            {
                return cinIndividu;
            }
            set
            {
                cinIndividu = value;
            }
        }
        public string Adresse
        {
            get
            {
                return adresse;
            }
            set
            {
                adresse = value;
            }
        }
        public string Profession
        {
            get
            {
                return profession;
            }
            set
            {
                profession = value;
            }
        }
        public string TelephoneFixeIndividu
        {
            get
            {
                return telephoneFixeIndividu;
            }
            set
            {
                telephoneFixeIndividu = value;
            }
        }
        public string TelephoneMobileIndividu
        {
            get
            {
                return telephoneMobileIndividu;
            }
            set
            {
                telephoneMobileIndividu = value;
            }
        }
        public string MailIndividu
        {
            get
            {
                return mailIndividu;
            }
            set
            {
                mailIndividu = value;
            }
        }
        public string NumQuartier
        {
            get
            {
                return numQuartier;
            }
            set
            {
                numQuartier = value;
            }
        }
        public int IsCheque
        {
            get
            {
                return isCheque;
            }
            set
            {
                isCheque = value;
            }
        }
        public int IsBonCommande
        {
            get
            {
                return isBonCommande;
            }
            set
            {
                isBonCommande = value;
            }
        }
        #endregion

        #region constructeur
        public crlIndividu()
        {
            this.NumIndividu = "";
            this.CiviliteIndividu = "";
            this.NomIndividu = "";
            this.PrenomIndividu = "";
            this.DateNaissanceIndividu = new DateTime(1, 1, 1);
            this.LieuNaissanceIndividu = "";
            this.CinIndividu = "";
            this.Adresse = "";
            this.Profession = "";
            this.TelephoneFixeIndividu = "";
            this.TelephoneMobileIndividu = "";
            this.MailIndividu = "";
            this.NumQuartier = "";
            this.IsCheque = -1;
            this.IsBonCommande = -1;
        }
        #endregion
    }
}