using System;
using System.Collections.Generic;

namespace arch.crl
{
    /// <summary>
    /// Objet autorisation de depart
    /// </summary>
    public class crlAutorisationDepart
    {
        #region variable
        private string numAutorisationDepart;
        private string numerosFB;
        private string matriculeAgent;
        private DateTime dateAD;
        private double recetteTotale;
        private double resteRegle;
        #endregion

        #region encapsulation
        public string NumAutorisationDepart
        {
            get
            {
                return numAutorisationDepart;
            }
            set
            {
                numAutorisationDepart = value;
            }
        }
        public string NumerosFB
        {
            get
            {
                return numerosFB;
            }
            set
            {
                numerosFB = value;
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
        public DateTime DateAD
        {
            get
            {
                return dateAD;
            }
            set
            {
                dateAD = value;
            }
        }
        public double RecetteTotale
        {
            get
            {
                return recetteTotale;
            }
            set
            {
                recetteTotale = value;
            }
        }
        public double ResteRegle
        {
            get
            {
                return resteRegle;
            }
            set
            {
                resteRegle = value;
            }
        }
        #endregion

        #region variable d'objet
        public crlFicheBord ficheBord;
        public crlAgent agent;
        public List<crlRecuAD> recus;
        #endregion

        #region construction
        public crlAutorisationDepart()
        {
            this.NumAutorisationDepart = "";
            this.NumerosFB = "";
            this.MatriculeAgent = "";
            this.DateAD = DateTime.Now;
            this.RecetteTotale = 0.00;
            this.ResteRegle = 0.00;
            this.ficheBord = null;
            this.agent = null;
            this.recus = null;
        }
        #endregion
    }
}