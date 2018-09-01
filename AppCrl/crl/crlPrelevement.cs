using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlPrelevement
    /// </summary>
    public class crlPrelevement
    {
        #region variable
        private string numPrelevement;
        private string matriculeAgent;
        private string typePrelevement;
        private string numAutorisationDepart;
        private double montantPrelevement;
        private DateTime datePrelevement;
        #endregion

        #region encapsulation
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
        public string TypePrelevement
        {
            get
            {
                return typePrelevement;
            }
            set
            {
                typePrelevement = value;
            }
        }
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
        public double MontantPrelevement
        {
            get
            {
                return montantPrelevement;
            }
            set
            {
                montantPrelevement = value;
            }
        }
        public DateTime DatePrelevement
        {
            get
            {
                return datePrelevement;
            }
            set
            {
                datePrelevement = value;
            }
        }
        #endregion

        #region variable d'objet
        public crlAgent agent;
        public crlTypePrelevement objTypePrelevement;
        public crlAutorisationDepart autorisationDepart;
        #endregion

        #region constructeur
        public crlPrelevement()
        {
            this.NumPrelevement = "";
            this.MatriculeAgent = "";
            this.TypePrelevement = "";
            this.NumAutorisationDepart = "";
            this.MontantPrelevement = 0.00;
            this.DatePrelevement = DateTime.Now;
            this.agent = null;
            this.objTypePrelevement = null;
            this.autorisationDepart = null;
        }
        #endregion
    }
}