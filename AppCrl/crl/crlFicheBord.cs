using System;
using System.Collections.Generic;

namespace arch.crl
{
    /// <summary>
    /// Objet fiche de bord
    /// </summary>
    public class crlFicheBord
    {
        #region variable
        private string numerosFB;
        private string matriculeAgent;
        private string numerosAV;
        private DateTime dateHeurDepart;
        private DateTime dateHeurPrevue;
        #endregion

        #region encapsulation
        public string NumerosFB
        {
            get { return numerosFB; }
            set { numerosFB = value; }
        }
        public DateTime DateHeurDepart
        {
            get { return dateHeurDepart; }
            set { dateHeurDepart = value; }
        }
        public string NumerosAV
        {
            get { return numerosAV; }
            set { numerosAV = value; }
        }
        public string MatriculeAgent
        {
            get { return matriculeAgent; }
            set { matriculeAgent = value; }
        }
        public DateTime DateHeurPrevue
        {
            get
            {
                return dateHeurPrevue;
            }
            set
            {
                dateHeurPrevue = value;
            }
        }
        #endregion

        #region variable d'objet
        public crlAutorisationVoyage autorisationVoyage;
        public crlAgent agent;
        public List<crlCommission> commission;
        public List<crlVoyage> voyage;
        //public List<crlEscorteVoyage> escorteVoyage;
        public List<crlPlaceFB> placeFB;
        #endregion

        #region constructeur
        public crlFicheBord()
        {
            this.NumerosFB = "";
            this.NumerosAV = "";
            this.MatriculeAgent = "";
            this.DateHeurDepart = DateTime.Now;
            this.DateHeurPrevue = DateTime.Now;
            this.autorisationVoyage = null;
            this.agent = null;
            this.commission = null;
            this.voyage = null;
            //this.escorteVoyage = null;
            this.placeFB = null;
        }
        #endregion
    }
}
