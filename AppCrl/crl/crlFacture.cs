using System;
using System.Collections.Generic;

namespace arch.crl
{
    /// <summary>
    /// objet facture
    /// </summary>
    public class crlFacture
    {
        #region variable
        private string numFacture;
        private string libele;
        private string montant;
        private DateTime dateFacturation;
        private string matriculeAgent;
        #endregion

        #region encapsulation
        public string NumFacture
        {
            get { return numFacture; }
            set { numFacture = value; }
        }
        public string Libele
        {
            get { return libele; }
            set { libele = value; }
        }
        public string Montant
        {
            get { return montant; }
            set { montant = value; }
        }
        public DateTime DateFacturation
        {
            get { return dateFacturation; }
            set { dateFacturation = value; }
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
        public List<crlAutorisationDepart> autorisationDeparts;
        #endregion

        #region constructeur
        public crlFacture()
        {
            this.NumFacture = "";
            this.Libele = "";
            this.Montant = "";
            this.DateFacturation = DateTime.Now;
            this.MatriculeAgent = "";
            this.agent = null;
            autorisationDeparts = null;
        }
        #endregion
    }
}
