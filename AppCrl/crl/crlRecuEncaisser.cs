using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlRecuEncaisser
    /// </summary>
    public class crlRecuEncaisser
    {
        #region variable
        private string numRecuEncaisser;
        private string matriculeAgent;
        private string numCheque;
        private string modePaiement;
        private DateTime dateRecuEncaisser;
        private double montantRecuEncaisser;
        private string libelleRecuEncaisser;
        #endregion

        #region encapulation
        public string NumRecuEncaisser
        {
            get
            {
                return numRecuEncaisser;
            }
            set
            {
                numRecuEncaisser = value;
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
        public DateTime DateRecuEncaisser
        {
            get
            {
                return dateRecuEncaisser;
            }
            set
            {
                dateRecuEncaisser = value;
            }
        }
        public double MontantRecuEncaisser
        {
            get
            {
                return montantRecuEncaisser;
            }
            set
            {
                montantRecuEncaisser = value;
            }
        }
        public string LibelleRecuEncaisser
        {
            get
            {
                return libelleRecuEncaisser;
            }
            set
            {
                libelleRecuEncaisser = value;
            }
        }
        #endregion

        #region variable d'objet
        public crlAgent agent;
        public crlCheque cheque;
        #endregion

        #region construction
        public crlRecuEncaisser()
        {
            this.DateRecuEncaisser = DateTime.Now;
            this.LibelleRecuEncaisser = "";
            this.MatriculeAgent = "";
            this.ModePaiement = "";
            this.MontantRecuEncaisser = 0;
            this.NumCheque = "";
            this.NumRecuEncaisser = "";

            this.agent = null;
            this.cheque = null;
        }
        #endregion
    }
}