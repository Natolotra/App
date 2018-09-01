using System;

namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlDureeAbonnementDevis
    /// </summary>
    public class crlDureeAbonnementDevis
    {
        #region variable
        private string numDureeAbonnementDevis;
        private string numTrajet;
        private string zone;
        private double prixUnitaire;
        private DateTime valideDu;
        private DateTime valideAu;
        private double prixTotal;
        private int nombreDureeAbonnement;
        private string numProforma;
        private string numCalculCategorieBillet;
        private string numCalculReductionBillet;
        private string numAbonnement;
        #endregion

        #region encapsulation
        public string NumDureeAbonnementDevis
        {
            get
            {
                return numDureeAbonnementDevis;
            }
            set
            {
                numDureeAbonnementDevis = value;
            }
        }
        public string NumTrajet
        {
            get
            {
                return numTrajet;
            }
            set
            {
                numTrajet = value;
            }
        }
        public string Zone
        {
            get
            {
                return zone;
            }
            set
            {
                zone = value;
            }
        }
        public double PrixUnitaire
        {
            get
            {
                return prixUnitaire;
            }
            set
            {
                prixUnitaire = value;
            }
        }
        public DateTime ValideDu
        {
            get
            {
                return valideDu;
            }
            set
            {
                valideDu = value;
            }
        }
        public DateTime ValideAu
        {
            get
            {
                return valideAu;
            }
            set
            {
                valideAu = value;
            }
        }
        public double PrixTotal
        {
            get
            {
                return prixTotal;
            }
            set
            {
                prixTotal = value;
            }
        }
        public int NombreDureeAbonnement
        {
            get
            {
                return nombreDureeAbonnement;
            }
            set
            {
                nombreDureeAbonnement = value;
            }
        }
        public string NumProforma
        {
            get
            {
                return numProforma;
            }
            set
            {
                numProforma = value;
            }
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
        public string NumAbonnement
        {
            get
            {
                return numAbonnement;
            }
            set
            {
                numAbonnement = value;
            }
        }
        #endregion

        #region variable d'objet
        public crlTrajet trajet;
        public crlZone zoneObj;
        public crlCalculCategorieBillet calculCategorieBillet;
        public crlCalculReductionBillet calculReductionBillet;
        #endregion

        #region constructeur
        public crlDureeAbonnementDevis()
        {
            this.NumDureeAbonnementDevis = "";
            this.NumTrajet = "";
            this.PrixTotal = 0.00;
            this.PrixUnitaire = 0.00;
            this.ValideAu = DateTime.Now;
            this.ValideDu = DateTime.Now;
            this.Zone = "";
            this.NumProforma = "";
            this.NombreDureeAbonnement = 0;
            this.NumCalculCategorieBillet = "";
            this.NumCalculReductionBillet = "";
            this.NumAbonnement = "";

            this.trajet = null;
            this.zoneObj = null;
            this.calculCategorieBillet = null;
            this.calculReductionBillet = null;
        }
        #endregion
    }
}