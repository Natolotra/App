using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlProforma
    /// </summary>
    public class crlProforma
    {
        #region variable
        private string numProforma;
        private string numSociete;
        private string numOrganisme;
        private string numIndividu;
        private DateTime dateProforma;
        private string matriculeAgent;
        private string modePaiement;
        #endregion

        #region encapsulation
        public string NumProforma
        {
            get
            {
                return numProforma;
            }
            set
            {
                numProforma = value;
            }
        }
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
        public DateTime DateProforma
        {
            get { return dateProforma; }
            set { dateProforma = value; }
        }
        public string MatriculeAgent
        {
            get { return matriculeAgent; }
            set { matriculeAgent = value; }
        }
        public string ModePaiement
        {
            get
            {
                return modePaiement;
            }
            set
            {
                modePaiement = value;
            }
        }
        #endregion

        #region variable d'objet
        public crlSociete societe;
        public crlOrganisme organisme;
        public crlIndividu individu;
        public List<crlBilletCommande> billetCommande;
        public List<crlCommissionDevis> commissionDevis;
        public List<crlDureeAbonnementDevis> dureeAbonnementDevis;
        public List<crlVoyageAbonnementDevis> voyageAbonnementDevis;
        public List<crlUSAbonnementNVDevis> uSAbonnementNVDevis;
        public crlAgent agent;
        #endregion

        #region construction
        public crlProforma()
        {
            this.NumIndividu = "";
            this.NumOrganisme = "";
            this.NumProforma = "";
            this.NumSociete = "";
            this.DateProforma = DateTime.Now;
            this.MatriculeAgent = "";
            this.ModePaiement = "";

            this.billetCommande = null;
            this.individu = null;
            this.organisme = null;
            this.societe = null;
            this.agent = null;
            this.commissionDevis = null;
            this.uSAbonnementNVDevis = null;
        }
        #endregion
    }
}