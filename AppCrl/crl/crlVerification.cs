using System;

namespace arch.crl
{
    /// <summary>
    /// Objet verfication
    /// </summary>
    public class crlVerification
    {
        #region declaration
        private string idVerification;
        private int verificationTechnique;
        private string aVoireVT;
        private int verificationPapier;
        private string aVoireVP;
        private string observationProfessionnelle;
        private DateTime dateVerification;
        private string matriculeAgent;
        private string numLicence;
        private string idChauffeur;
        private int planDepart;
        private string idItineraire;
        #endregion

        #region encapsulation
        public string IdVerification
        {
            get { return idVerification; }
            set { idVerification = value; }
        }
        public int VerificationTechnique
        {
            get { return verificationTechnique; }
            set { verificationTechnique = value; }
        }
        public string AVoireVT
        {
            get { return aVoireVT; }
            set { aVoireVT = value; }
        }
        public int VerificationPapier
        {
            get { return verificationPapier; }
            set { verificationPapier = value; }
        }
        public string AVoireVP
        {
            get { return aVoireVP; }
            set { aVoireVP = value; }
        }
        public string ObservationProfessionnelle
        {
            get { return observationProfessionnelle; }
            set { observationProfessionnelle = value; }
        }
        public DateTime DateVerification
        {
            get { return dateVerification; }
            set { dateVerification = value; }
        }
        public string MatriculeAgent
        {
            get { return matriculeAgent; }
            set { matriculeAgent = value; }
        }
        public string NumLicence
        {
            get
            {
                return numLicence;
            }
            set
            {
                numLicence = value;
            }
        }
        public string IdChauffeur
        {
            get { return idChauffeur; }
            set { idChauffeur = value; }
        }
        public int PlanDepart
        {
            get { return planDepart; }
            set { planDepart = value; }
        }
        public string IdItineraire
        {
            get { return idItineraire; }
            set { idItineraire = value; }
        }
        #endregion

        #region variable d'objet
        public crlAgent Agent;
        public crlLicence Licence;
        public crlChauffeur Chauffeur;
        public crlItineraire Itineraire; 
        #endregion

        #region construction
        public crlVerification()
        {
            this.AVoireVT = "";
            this.AVoireVP = "";
            this.DateVerification = DateTime.Now;
            this.IdVerification = "";
            this.ObservationProfessionnelle = "";
            this.VerificationPapier = 0;
            this.VerificationTechnique = 0;
            this.PlanDepart = 0;
            this.IdItineraire = "";
            this.MatriculeAgent = "";
            this.NumLicence = "";
            this.IdChauffeur = "";
            this.Agent = null;
            this.Licence = null;
            this.Chauffeur = null;
            this.Itineraire = null;
        }
        #endregion
    }
}
