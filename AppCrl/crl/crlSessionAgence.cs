using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlSessionAgence
    /// </summary>
    public class crlSessionAgence
    {
        #region variable
        private string numSessionAgence;
        private string numAgence;
        private string matriculeAgentFermeture;
        private string matriculeAgentOuverture;
        private DateTime dateHeureOuverture;
        private DateTime dateHeureFermeture;
        #endregion

        #region encapsulation
        public string NumSessionAgence
        {
            get
            {
                return numSessionAgence;
            }
            set
            {
                numSessionAgence = value;
            }
        }
        public string NumAgence
        {
            get
            {
                return numAgence;
            }
            set
            {
                numAgence = value;
            }
        }
        public string MatriculeAgentFermeture
        {
            get
            {
                return matriculeAgentFermeture;
            }
            set
            {
                matriculeAgentFermeture = value;
            }
        }
        public string MatriculeAgentOuverture
        {
            get
            {
                return matriculeAgentOuverture;
            }
            set
            {
                matriculeAgentOuverture = value;
            }
        }
        public DateTime DateHeureOuverture
        {
            get
            {
                return dateHeureOuverture;
            }
            set
            {
                dateHeureOuverture = value;
            }
        }
        public DateTime DateHeureFermeture
        {
            get
            {
                return dateHeureFermeture;
            }
            set
            {
                dateHeureFermeture = value;
            }
        }
        #endregion

        #region constructeur
        public crlSessionAgence()
        {
            this.DateHeureFermeture = DateTime.Now;
            this.DateHeureOuverture = DateTime.Now;
            this.MatriculeAgentFermeture = "";
            this.MatriculeAgentOuverture = "";
            this.NumAgence = "";
            this.NumSessionAgence = "";

        }
        #endregion
    }
}