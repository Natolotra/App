using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// objet recu
    /// </summary>
    public class crlRecu
    {
        #region variable
        private string numRecu;
        private string libele;
        private string montant;
        private DateTime date;
        private string modePaiement;
        private string matriculeAgent;
        #endregion

        #region encapsulation
        public string NumRecu
        {
            get
            {
                return numRecu;
            }
            set
            {
                numRecu = value;
            }
        }
        public string Libele
        {
            get
            {
                return libele;
            }
            set
            {
                libele = value;
            }
        }
        public string Montant
        {
            get
            {
                return montant;
            }
            set
            {
                montant = value;
            }
        }
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
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
        #endregion

        #region variable d'objet
        public crlAgent agent;
        #endregion

        #region constructeur
        public crlRecu()
        {
            this.NumRecu = "";
            this.Libele = "";
            this.Montant = "";
            this.modePaiement = "";
            this.Date = DateTime.Now;
            this.MatriculeAgent = "";
            this.agent = null;
        }
        #endregion
    }
}