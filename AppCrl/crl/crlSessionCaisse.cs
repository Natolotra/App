using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlSessionCaisse
    /// </summary>
    public class crlSessionCaisse
    {
        #region variable
        private string numSessionCaisse;
        private string matriculeAgent;
        private string matriculeAgentFermeture;
        private string matriculeAgentOuverture;
        private DateTime dateHeureDebutSession;
        private DateTime dateHeureFinSession;
        private double fondCaisse;
        private string numSessionAgence;
        #endregion

        #region encapsulation
        public string NumSessionCaisse
        {
            get
            {
                return numSessionCaisse;
            }
            set
            {
                numSessionCaisse = value;
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
        public DateTime DateHeureDebutSession
        {
            get
            {
                return dateHeureDebutSession;
            }
            set
            {
                dateHeureDebutSession = value;
            }
        }
        public DateTime DateHeureFinSession
        {
            get
            {
                return dateHeureFinSession;
            }
            set
            {
                dateHeureFinSession = value;
            }
        }
        public double FondCaisse
        {
            get
            {
                return fondCaisse;
            }
            set
            {
                fondCaisse = value;
            }
        }
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
        #endregion

        #region variable d'objet
        /*
        public crlAgent agentFermeture;
        public crlAgent agentOuverture;
         * */
        #endregion

        #region constructeur
        public crlSessionCaisse()
        {
            this.NumSessionCaisse = "";
            this.MatriculeAgent = "";
            this.MatriculeAgentFermeture = "";
            this.MatriculeAgentOuverture = "";
            this.DateHeureDebutSession = DateTime.Now;
            this.DateHeureFinSession = DateTime.Now;
            this.FondCaisse = 0;
            this.NumSessionAgence = "";

            /*this.agentFermeture = null;
            this.agentOuverture = null;*/
        }
        #endregion
    }
}