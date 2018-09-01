using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Objet recu Facture
    /// </summary>
    public class crlRecuFac
    {
        #region variable
        private string numRecuFac;
        private string matriculeAgent;
        private string libele;
        private string montant;
        private DateTime date;
        private string numFacture;
        #endregion

        #region encapsulation
        public string NumRecuFac
        {
            get
            {
                return numRecuFac;
            }
            set
            {
                numRecuFac = value;
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
        public string NumFacture
        {
            get
            {
                return numFacture;
            }
            set
            {
                numFacture = value;
            }
        }
        #endregion

        #region variable d'objet
        public crlAgent agent;
        public crlFacture facture;
        #endregion

        #region constructeur
        public crlRecuFac()
        {
            this.Date = DateTime.Now;
            this.Libele = "";
            this.MatriculeAgent = "";
            this.Montant = "";
            this.NumFacture = "";
            this.NumRecuFac = "";
            this.agent = null;
            this.facture = null;
        }
        #endregion
    }
}