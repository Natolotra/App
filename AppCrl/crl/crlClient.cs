using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Objet client
    /// </summary>
    public class crlClient
    {
        #region variable
        private string numClient;
        private string nomClient;
        private string prenomClient;
        private string adresseClient;
        private string cinClient;
        private string telephoneClient;
        private string mobileClient;
        private int isCheque;
        private int isBonCommande;
        #endregion

        #region encapsulation
        public string NumClient
        {
            get
            {
                return numClient;
            }
            set
            {
                numClient = value;
            }
        }
        public string NomClient
        {
            get
            {
                return nomClient;
            }
            set
            {
                nomClient = value;
            }
        }
        public string PrenomClient
        {
            get
            {
                return prenomClient;
            }
            set
            {
                prenomClient = value;
            }
        }
        public string AdresseClient
        {
            get
            {
                return adresseClient;
            }
            set
            {
                adresseClient = value;
            }
        }
        public string CinClient
        {
            get
            {
                return cinClient;
            }
            set
            {
                cinClient = value;
            }
        }
        public string TelephoneClient
        {
            get
            {
                return telephoneClient;
            }
            set
            {
                telephoneClient = value;
            }
        }
        public string MobileClient
        {
            get
            {
                return mobileClient;
            }
            set
            {
                mobileClient = value;
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
        public crlClient()
        {
            this.AdresseClient = "";
            this.CinClient = "";
            this.MobileClient = "";
            this.NomClient = "";
            this.NumClient = "";
            this.PrenomClient = "";
            this.TelephoneClient = "";
            this.IsCheque = 0;
            this.IsBonCommande = 0;
        }
        #endregion
    }
}