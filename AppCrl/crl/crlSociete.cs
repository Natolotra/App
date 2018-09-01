using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Objet Société
    /// </summary>
    public class crlSociete
    {
        #region declaration
        private string numSociete;
        private string nomSociete;
        private string adresseSociete;
        private string numQuartier;
        private string telephoneFixeSociete;
        private string telephoneMobileSociete;
        private string mailSociete;
        private string secteurActiviteSociete;
        private string numIndividuResponsable;
        private int isReductionUS;
        private int isCheque;
        private int isBonCommande;
        #endregion

        #region encapsulation
        public string NumSociete
        {
            get
            {
                return numSociete;
            }
            set
            {
                numSociete = value;
            }
        }
        public string NomSociete
        {
            get
            {
                return nomSociete;
            }
            set
            {
                nomSociete = value;
            }
        }
        public string AdresseSociete
        {
            get
            {
                return adresseSociete;
            }
            set
            {
                adresseSociete = value;
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
        public string TelephoneFixeSociete
        {
            get
            {
                return telephoneFixeSociete;
            }
            set
            {
                telephoneFixeSociete = value;
            }
        }
        public string TelephoneMobileSociete
        {
            get
            {
                return telephoneMobileSociete;
            }
            set
            {
                telephoneMobileSociete = value;
            }
        }
        public string MailSociete
        {
            get
            {
                return mailSociete;
            }
            set
            {
                mailSociete = value;
            }
        }
        public string SecteurActiviteSociete
        {
            get
            {
                return secteurActiviteSociete;
            }
            set
            {
                secteurActiviteSociete = value;
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
        public int IsReductionUS
        {
            get
            {
                return isReductionUS;
            }
            set
            {
                isReductionUS = value;
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
        public crlSociete()
        {
            this.NumSociete = "";
            this.NomSociete = "";
            this.MailSociete = "";
            this.AdresseSociete = "";
            this.NumQuartier = "";
            this.TelephoneFixeSociete = "";
            this.TelephoneMobileSociete = "";
            this.SecteurActiviteSociete = "";
            this.NumIndividuResponsable = "";
            this.IsReductionUS = 0;
            this.IsCheque = -1;
            this.IsBonCommande = -1;

            this.individuResponsable = null;
        }
        #endregion
    }
}