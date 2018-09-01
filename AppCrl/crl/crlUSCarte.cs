using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Objet carte
    /// </summary>
    public class crlUSCarte
    {
        #region variable
        private string numCarte;
        private double prixCarte;
        private string numAbonnement;
        private string numAgence;
        private string numAbonnementNVDevis;
        private string numAbonnementNV;
        private string numUSReductionParticulier;
        private string numUSValidationReduction;
        #endregion

        #region encapsulation
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
        public double PrixCarte
        {
            get
            {
                return prixCarte;
            }
            set
            {
                prixCarte = value;
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
        public string NumAbonnementNVDevis
        {
            get
            {
                return numAbonnementNVDevis;
            }
            set
            {
                numAbonnementNVDevis = value;
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
        public string NumUSReductionParticulier
        {
            get
            {
                return numUSReductionParticulier;
            }
            set
            {
                numUSReductionParticulier = value;
            }
        }
        public string NumUSValidationReduction
        {
            get
            {
                return numUSValidationReduction;
            }
            set
            {
                numUSValidationReduction = value;
            }
        }
        #endregion

        #region variable d'objet
        public crlAbonnement abonnement;
        public crlUSReductionParticulier reductionParticulier;
        #endregion

        #region constructeur
        public crlUSCarte()
        {
            this.NumCarte = "";
            this.PrixCarte = 0;
            this.NumAbonnement = "";
            this.NumAgence = "";
            this.NumAbonnementNV = "";
            this.NumAbonnementNVDevis = "";
            this.NumUSReductionParticulier = "";
            this.NumUSValidationReduction = "";

            this.abonnement = null;
            this.reductionParticulier = null;
        }
        #endregion
    }
}