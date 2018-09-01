using System;

namespace arch.crl
{
    /// <summary>
    /// objet voyageAbonnement 
    /// </summary>
    public class crlVoyageAbonnement
    {
        #region variable
        private string numVoyageAbonnement;
        private string numTrajet;
        private string zone;
        private double prixUnitaire;
        private int nbVoyageAbonnement;
        private string numAbonnement;
        private string matriculeAgent;
        private DateTime dateVoyageAbonnement;
        private string numCalculCategorieBillet;
        private string numCalculReductionBillet;
        private string modePaiement;
        #endregion

        #region encapsulation
        public string NumVoyageAbonnement
        {
            get { return numVoyageAbonnement; }
            set { numVoyageAbonnement = value; }
        }
        public string NumTrajet
        {
            get { return numTrajet; }
            set { numTrajet = value; }
        }
        public string Zone
        {
            get { return zone; }
            set { zone = value; }
        }
        public double PrixUnitaire
        {
            get { return prixUnitaire; }
            set { prixUnitaire = value; }
        }
        public int NbVoyageAbonnement
        {
            get { return nbVoyageAbonnement; }
            set { nbVoyageAbonnement = value; }
        }
        public string NumAbonnement
        {
            get { return numAbonnement; }
            set { numAbonnement = value; }
        }
        public string MatriculeAgent
        {
            get { return matriculeAgent; }
            set { matriculeAgent = value; }
        }
        public DateTime DateVoyageAbonnement
        {
            get { return dateVoyageAbonnement; }
            set { dateVoyageAbonnement = value; }
        }
        public string NumCalculCategorieBillet
        {
            get
            {
                return numCalculCategorieBillet;
            }
            set
            {
                numCalculCategorieBillet = value;
            }
        }
        public string NumCalculReductionBillet
        {
            get
            {
                return numCalculReductionBillet;
            }
            set
            {
                numCalculReductionBillet = value;
            }
        }
        public string ModePaiement
        {
            get
            {
                return modePaiement;
            }
            set
            {
                modePaiement = value;
            }
        }
        #endregion

        #region variable d'objet
        public crlTrajet trajet;
        public crlZone zoneObj;
        public crlAbonnement abonnement;
        public crlAgent agent;
        public crlCalculCategorieBillet calculCategorieBillet;
        public crlCalculReductionBillet calculReductionBillet;
        #endregion

        #region constructeur
        public crlVoyageAbonnement()
        {
            this.MatriculeAgent = "";
            this.NbVoyageAbonnement = 0;
            this.NumAbonnement = "";
            this.NumTrajet = "";
            this.NumVoyageAbonnement = "";
            this.PrixUnitaire = 0.00;
            this.Zone = "";
            this.DateVoyageAbonnement = DateTime.Now;
            this.NumCalculCategorieBillet = "";
            this.NumCalculReductionBillet = "";
            this.ModePaiement = "";

            this.trajet = null;
            this.zoneObj = null;
            this.abonnement = null;
            this.agent = null;
            this.calculCategorieBillet = null;
            this.calculReductionBillet = null;
        }
        #endregion
    }
}
