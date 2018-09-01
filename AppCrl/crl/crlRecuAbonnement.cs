using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Objet recu abonnement
    /// </summary>
    public class crlRecuAbonnement
    {
        #region declaration
        private string numRecuAbonnement;
        private string numVoyageAbonnement;
        private string numDureeAbonnement;
        private string modePaiement;
        private string numCheque;
        private string numBonDeCommande;
        private string matriculeAgent;
        private DateTime dateRecuAbonnement;
        private double montantRecuAbonnement;
        #endregion

        #region encapsulation
        public string NumRecuAbonnement
        {
            get
            {
                return numRecuAbonnement;
            }
            set
            {
                numRecuAbonnement = value;
            }
        }
        public string NumVoyageAbonnement
        {
            get
            {
                return numVoyageAbonnement;
            }
            set
            {
                numVoyageAbonnement = value;
            }
        }
        public string NumDureeAbonnement
        {
            get
            {
                return numDureeAbonnement;
            }
            set
            {
                numDureeAbonnement = value;
            }
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
        public string NumCheque
        {
            get
            {
                return numCheque;
            }
            set
            {
                numCheque = value;
            }
        }
        public string NumBonDeCommande
        {
            get
            {
                return numBonDeCommande;
            }
            set
            {
                numBonDeCommande = value;
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
        public DateTime DateRecuAbonnement
        {
            get
            {
                return dateRecuAbonnement;
            }
            set
            {
                dateRecuAbonnement = value;
            }
        }
        public double MontantRecuAbonnement
        {
            get
            {
                return montantRecuAbonnement;
            }
            set
            {
                montantRecuAbonnement = value;
            }
        }
        #endregion

        #region variable d'objet
        public crlCheque cheque;
        public crlBonDeCommande bonDeCommande;
        public crlDureeAbonnement dureeAbonnement;
        public crlVoyageAbonnement voyageAbonnement;
        public crlAgent agent;
        #endregion

        #region constructeur
        public crlRecuAbonnement()
        {
            this.DateRecuAbonnement = DateTime.Now;
            this.MatriculeAgent = "";
            this.ModePaiement = "";
            this.MontantRecuAbonnement = 0.00;
            this.NumBonDeCommande = "";
            this.NumCheque = "";
            this.NumDureeAbonnement = "";
            this.NumRecuAbonnement = "";
            this.NumVoyageAbonnement = "";

            this.agent = null;
            this.bonDeCommande = null;
            this.cheque = null;
            this.dureeAbonnement = null;
            this.voyageAbonnement = null;
        }
        #endregion
    }
}