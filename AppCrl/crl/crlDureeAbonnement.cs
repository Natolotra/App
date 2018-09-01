using System;

namespace arch.crl
{
    /// <summary>
    /// Objet durée abonement
    /// </summary>
    public class crlDureeAbonnement
    {
        #region variable
        private string numDureeAbonnement;
        private string numTrajet;
        private string zone;
        private double prixUnitaire;
        private string numAbonnement;
        private DateTime valideDu;
        private DateTime valideAu;
        private double prixTotal;
        private string matriculeAgent;
        private DateTime dateDureeAbonnement;
        private string numCalculCategorieBillet;
        private string numCalculReductionBillet;
        private string modePaiement;
        #endregion

        #region encapsulation
        public string NumDureeAbonnement
        {
            get { return numDureeAbonnement; }
            set { numDureeAbonnement = value; }
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
        public string NumAbonnement
        {
            get { return numAbonnement; }
            set { numAbonnement = value; }
        }
        public DateTime ValideDu
        {
            get { return valideDu; }
            set { valideDu = value; }
        }
        public DateTime ValideAu
        {
            get { return valideAu; }
            set { valideAu = value; }
        }
        public double PrixTotal
        {
            get { return prixTotal; }
            set { prixTotal = value; }
        }
        public string MatriculeAgent
        {
            get { return matriculeAgent; }
            set { matriculeAgent = value; }
        }
        public DateTime DateDureeAbonnement
        {
            get { return dateDureeAbonnement; }
            set { dateDureeAbonnement = value; }
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
        public crlDureeAbonnement()
        {
            this.MatriculeAgent = "";
            this.NumAbonnement = "";
            this.NumDureeAbonnement = "";
            this.NumTrajet = "";
            this.PrixTotal = 0.00;
            this.PrixUnitaire = 0.00;
            this.ValideAu = DateTime.Now;
            this.ValideDu = DateTime.Now;
            this.Zone = "";
            this.DateDureeAbonnement = DateTime.Now;
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
