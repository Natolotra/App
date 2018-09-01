using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Objet Organisme
    /// </summary>
    public class crlOrganisme
    {
        #region declaration
        private string numOrganisme;
        private string nomOrganisme;
        private string adresseOrganisme;
        private string numQuartier;
        private string mailOrganisme;
        private string telephoneFixeOrganisme;
        private string telephoneMobileOrganisme;
        private string numIndividuResponsable;
        private int isCheque;
        private int isBonCommande;
        #endregion

        #region encapsulation
        public string NumOrganisme
        {
            get
            {
                return numOrganisme;
            }
            set
            {
                numOrganisme = value;
            }
        }
        public string NomOrganisme
        {
            get
            {
                return nomOrganisme;
            }
            set
            {
                nomOrganisme = value;
            }
        }
        public string AdresseOrganisme
        {
            get
            {
                return adresseOrganisme;
            }
            set
            {
                adresseOrganisme = value;
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
        public string MailOrganisme
        {
            get
            {
                return mailOrganisme;
            }
            set
            {
                mailOrganisme = value;
            }
        }
        public string TelephoneFixeOrganisme
        {
            get
            {
                return telephoneFixeOrganisme;
            }
            set
            {
                telephoneFixeOrganisme = value;
            }
        }
        public string TelephoneMobileOrganisme
        {
            get
            {
                return telephoneMobileOrganisme;
            }
            set
            {
                telephoneMobileOrganisme = value;
            }
        }
        public string NumIndividuResponsable
        {
            get
            {
                return numIndividuResponsable;
            }
            set
            {
                numIndividuResponsable = value;
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

        #region variable d'objet
        public crlIndividu individuResponsable;
        #endregion

        #region constructeur
        public crlOrganisme()
        {
            this.NumOrganisme = "";
            this.NomOrganisme = "";
            this.AdresseOrganisme = "";
            this.NumQuartier = "";
            this.MailOrganisme = "";
            this.TelephoneFixeOrganisme = "";
            this.TelephoneMobileOrganisme = "";
            this.NumIndividuResponsable = "";
            this.IsCheque = -1;
            this.IsBonCommande = -1;

            this.individuResponsable = null;
        }
        #endregion
    }
}