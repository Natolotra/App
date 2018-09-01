using System;

namespace arch.crl
{
    /// <summary>
    /// Objet voyage
    /// </summary>
    public class crlUSVoyage
    {
        #region variable
        private string numVoyage;
        private DateTime dateHeureDepart;
        private DateTime dateHeureArrive;
        private string numLicence;
        private string matriculeAgentDepart;
        private string matriculeAgentArrive;
        private string matriculeAgentChauffeur;
        private string matriculeAgentReceveur;
        private string matriculeAgentControleur;
        private string numLigne;
        private string numFacture;
        private string numAppareil;
        #endregion

        #region encapsulation
        public string NumVoyage
        {
            get { return numVoyage; }
            set { numVoyage = value; }
        }
        public DateTime DateHeureDepart
        {
            get { return dateHeureDepart; }
            set { dateHeureDepart = value; }
        }
        public DateTime DateHeureArrive
        {
            get { return dateHeureArrive; }
            set { dateHeureArrive = value; }
        }
        public string NumLicence
        {
            get { return numLicence; }
            set { numLicence = value; }
        }
        public string MatriculeAgentDepart
        {
            get { return matriculeAgentDepart; }
            set { matriculeAgentDepart = value; }
        }
        public string MatriculeAgentArrive
        {
            get { return matriculeAgentArrive; }
            set { matriculeAgentArrive = value; }
        }
        public string MatriculeAgentChauffeur
        {
            get { return matriculeAgentChauffeur; }
            set { matriculeAgentChauffeur = value; }
        }
        public string MatriculeAgentReceveur
        {
            get { return matriculeAgentReceveur; }
            set { matriculeAgentReceveur = value; }
        }
        public string MatriculeAgentControleur
        {
            get { return matriculeAgentControleur; }
            set { matriculeAgentControleur = value; }
        }
        public string NumLigne
        {
            get { return numLigne; }
            set { numLigne = value; }
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
        public string NumAppareil
        {
            get
            {
                return numAppareil;
            }
            set
            {
                numAppareil = value;
            }
        }
        #endregion

        #region variable d'objet
        public crlAgent agentDepart;
        public crlAgent agentArrive;
        public crlAgent agentChauffeur;
        public crlAgent agentReceveur;
        public crlAgent agentControleur;
        #endregion

        #region constructeur
        public crlUSVoyage()
        {
            this.NumVoyage = "";
            this.DateHeureArrive = DateTime.Now;
            this.DateHeureDepart = DateTime.Now;
            this.NumLicence = "";
            this.MatriculeAgentArrive = "";
            this.MatriculeAgentChauffeur = "";
            this.MatriculeAgentControleur = "";
            this.MatriculeAgentDepart = "";
            this.MatriculeAgentReceveur = "";
            this.NumLigne = "";
            this.NumAppareil = "";
            this.NumFacture = "";

            this.agentArrive = null;
            this.agentChauffeur = null;
            this.agentControleur = null;
            this.agentDepart = null;
            this.agentReceveur = null;
        }
        #endregion
    }
}