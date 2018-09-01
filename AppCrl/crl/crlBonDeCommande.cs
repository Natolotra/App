using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlBonDeCommande
    /// </summary>
    public class crlBonDeCommande
    {
        #region declaration
        private string numBonDeCommande;
        private string matriculeAgent;
        private string descriptionBC;
        private DateTime dateBC;
        private DateTime datePaiementBC;
        private double montantBC;
        private string numProforma;
        #endregion

        #region encapsulation
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
        public string DescriptionBC
        {
            get
            {
                return descriptionBC;
            }
            set
            {
                descriptionBC = value;
            }
        }
        public DateTime DateBC
        {
            get
            {
                return dateBC;
            }
            set
            {
                dateBC = value;
            }
        }
        public DateTime DatePaiementBC
        {
            get
            {
                return datePaiementBC;
            }
            set
            {
                datePaiementBC = value;
            }
        }
        public double MontantBC
        {
            get
            {
                return montantBC;
            }
            set
            {
                montantBC = value;
            }
        }
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
        #endregion

        #region declaration variable
        public crlAgent agent;
        public crlProforma proforma;
        #endregion

        #region construction
        public crlBonDeCommande()
        {
            this.NumBonDeCommande = "";
            this.DateBC = DateTime.Now;
            this.DatePaiementBC = DateTime.Now;
            this.DescriptionBC = "";
            this.MatriculeAgent = "";
            this.MontantBC = 0;
            this.NumProforma = "";

            this.proforma = null;
            this.agent = null;
        }
        #endregion
    }
}