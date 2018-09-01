using System;


namespace arch.crl
{
    /// <summary>
    /// Summary description for crlUSAbonnementNVDevis
    /// </summary>
    public class crlUSAbonnementNVDevis
    {
        #region variable
        string numAbonnementNVDevis;
        string numAbonnement;
        double prixUnitaireNV;
        double montantNV;
        string numZoneD;
        string numZoneF;
        string numReductionBillet;
        string numCategorieBillet;
        string numProforma;
        string numInfoPasse;
        string numCarte;
        double montantCarte;
        DateTime dateAbonnementNVDevis;
        string numAbonnementNV;
        int nombreVoyage;
        #endregion

        #region accesseur
        public string NumAbonnementNVDevis
        {
            get { return numAbonnementNVDevis; }
            set { numAbonnementNVDevis = value; }
        }
        public string NumAbonnement
        {
            get { return numAbonnement; }
            set { numAbonnement = value; }
        }
        public double PrixUnitaireNV
        {
            get { return prixUnitaireNV; }
            set { prixUnitaireNV = value; }
        }
        public double MontantNV
        {
            get { return montantNV; }
            set { montantNV = value; }
        }
        public string NumZoneD
        {
            get { return numZoneD; }
            set { numZoneD = value; }
        }
        public string NumZoneF
        {
            get { return numZoneF; }
            set { numZoneF = value; }
        }
        public string NumReductionBillet
        {
            get { return numReductionBillet; }
            set { numReductionBillet = value; }
        }
        public string NumCategorieBillet
        {
            get { return numCategorieBillet; }
            set { numCategorieBillet = value; }
        }
        public string NumProforma
        {
            get { return numProforma; }
            set { numProforma = value; }
        }
        public string NumInfoPasse
        {
            get
            {
                return numInfoPasse;
            }
            set
            {
                numInfoPasse = value;
            }
        }
        public string NumCarte
        {
            get
            {
                return numCarte;
            }
            set
            {
                numCarte = value;
            }
        }
        public double MontantCarte
        {
            get
            {
                return montantCarte;
            }
            set
            {
                montantCarte = value;
            }
        }
        public DateTime DateAbonnementNVDevis
        {
            get
            {
                return dateAbonnementNVDevis;
            }
            set
            {
                dateAbonnementNVDevis = value;
            }
        }
        public string NumAbonnementNV
        {
            get
            {
                return numAbonnementNV;
            }
            set
            {
                numAbonnementNV = value;
            }
        }
        public int NombreVoyage
        {
            get
            {
                return nombreVoyage;
            }
            set
            {
                nombreVoyage = value;
            }
        }
        #endregion

        #region variable d'objet
        public crlUSZone zoneD;
        public crlUSZone zoneF;
        public crlUSInfoPasse infoPasse;
        #endregion

        #region constructeur
        public crlUSAbonnementNVDevis()
        {
            this.MontantNV = 0;
            this.NumAbonnement = "";
            this.NumAbonnementNVDevis = "";
            this.NumCategorieBillet = "";
            this.NumProforma = "";
            this.NumReductionBillet = "";
            this.NumZoneD = "";
            this.NumZoneF = "";
            this.PrixUnitaireNV = 0;
            this.NumInfoPasse = "";
            this.NumCarte = "";
            this.MontantCarte = 0;
            this.DateAbonnementNVDevis = DateTime.Now;
            this.NumAbonnementNV = "";
            this.nombreVoyage = 0;

            infoPasse = null;
            zoneD = null;
            zoneF = null;
        }
        #endregion
    }
}
