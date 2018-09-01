using System;

namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlCheque
    /// </summary>
    public class crlCheque
    {
        #region variable
        private string numCheque;
        private string matriculeAgent;
        private string numerosCheque;
        private DateTime dateCheque;
        private double montantCheque;
        private string banque;
        private string numCompte;
        private string titulaireCheque;
        private string adresseTitulaireCheque;
        #endregion

        #region implementation
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
        public string NumerosCheque
        {
            get
            {
                return numerosCheque;
            }
            set
            {
                numerosCheque = value;
            }
        }
        public DateTime DateCheque
        {
            get
            {
                return dateCheque;
            }
            set
            {
                dateCheque = value;
            }
        }
        public double MontantCheque
        {
            get
            {
                return montantCheque;
            }
            set
            {
                montantCheque = value;
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
        public string Banque
        {
            get
            {
                return banque;
            }
            set
            {
                banque = value;
            }
        }
        public string NumCompte
        {
            get
            {
                return numCompte;
            }
            set
            {
                numCompte = value;
            }
        }
        public string TitulaireCheque
        {
            get
            {
                return titulaireCheque;
            }
            set
            {
                titulaireCheque = value;
            }
        }
        public string AdresseTitulaireCheque
        {
            get
            {
                return adresseTitulaireCheque;
            }
            set
            {
                adresseTitulaireCheque = value;
            }
        }
        #endregion

        #region variable d'objet
        public crlAgent agent;
        #endregion

        #region constructeur
        public crlCheque()
        {
            this.NumCheque = "";
            this.DateCheque = DateTime.Now;
            this.NumerosCheque = "";
            this.MontantCheque = 0;
            this.MatriculeAgent = "";
            this.Banque = "";
            this.NumCompte = "";
            this.TitulaireCheque = "";
            this.AdresseTitulaireCheque = "";

            this.agent = null;
        }
        #endregion
    }
}