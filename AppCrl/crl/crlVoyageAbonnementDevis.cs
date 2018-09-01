using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlVoyageAbonnementDevis
    /// </summary>
    public class crlVoyageAbonnementDevis
    {
        #region variable
        private string numVoyageAbonnementDevis;
        private string numTrajet;
        private string zone;
        private double prixUnitaire;
        private int nbVoyageAbonnement;
        private string numProforma;
        private string numCalculCategorieBillet;
        private string numCalculReductionBillet;
        private string numAbonnement;
        #endregion

        #region encapsulation
        public string NumVoyageAbonnementDevis
        {
            get
            {
                return numVoyageAbonnementDevis;
            }
            set
            {
                numVoyageAbonnementDevis = value;
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
        public int NbVoyageAbonnement
        {
            get
            {
                return nbVoyageAbonnement;
            }
            set
            {
                nbVoyageAbonnement = value;
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
        public crlVoyageAbonnementDevis()
        {
            this.NbVoyageAbonnement = 0;
            this.NumTrajet = "";
            this.NumVoyageAbonnementDevis = "";
            this.PrixUnitaire = 0.00;
            this.Zone = "";
            this.NumProforma = "";
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