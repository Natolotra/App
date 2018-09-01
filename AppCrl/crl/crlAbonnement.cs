using System;
using System.Collections.Generic;

namespace arch.crl
{
    /// <summary>
    /// Objet abonnement
    /// </summary>
    public class crlAbonnement
    {
        #region variable
        private string numAbonnement;
        private string matriculeAgent;
        private string numIndividu;
        private string numSociete;
        private string numOrganisme;
        private DateTime dateAbonnement;
        private string imageAbonner;
        #endregion

        #region encapsulation
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
        public string MatriculeAgent
        {
            get
            {
                return matriculeAgent;
            }
            set
            {
                matriculeAgent = value;
            }
        }
        public string NumSociete
        {
            get { return numSociete; }
            set { numSociete = value; }
        }
        public string NumOrganisme
        {
            get { return numOrganisme; }
            set { numOrganisme = value; }
        }
        public DateTime DateAbonnement
        {
            get
            {
                return dateAbonnement;
            }
            set
            {
                dateAbonnement = value;
            }
        }
        public string ImageAbonner
        {
            get
            {
                return imageAbonner;
            }
            set
            {
                imageAbonner = value;
            }
        }
        #endregion

        #region variable d'objet
        public crlIndividu individu;
        public crlAgent agent;
        public List<crlBillet> billets;
        public crlOrganisme organisme;
        public crlSociete societe;
        #endregion

        #region construction
        public crlAbonnement()
        {
            this.NumAbonnement = "";
            this.NumIndividu = "";
            this.MatriculeAgent = "";
            this.NumSociete = "";
            this.NumOrganisme = "";
            this.DateAbonnement = DateTime.Now;
            this.ImageAbonner = "abonner.png";

            this.individu = null;
            this.agent = null;
            this.billets = null;
            this.organisme = null;
            this.societe = null;
        }
        #endregion
    }
}