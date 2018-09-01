using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlRecuAD
    /// </summary>
    public class crlRecuAD
    {
        #region variable
        private string numRecuAD;
        private string matriculeAgent;
        private string libele;
        private string montant;
        private DateTime date;
        private string numPrelevement;
        private string numFacture;
        #endregion

        #region encapsulation
        public string NumRecuAD
        {
            get
            {
                return numRecuAD;
            }
            set
            {
                numRecuAD = value;
            }
        }
        public string NumPrelevement
        {
            get
            {
                return numPrelevement;
            }
            set
            {
                numPrelevement = value;
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
        public crlPrelevement prelevement;
        #endregion

        #region constructeur
        public crlRecuAD()
        {
            this.NumRecuAD = "";
            this.MatriculeAgent = "";
            this.Libele = "";
            this.Montant = "";
            this.Date = DateTime.Now;
            this.NumPrelevement = "";
            this.NumFacture = "";

            this.agent = null;
            this.prelevement = null;
        }
        #endregion
    }
}