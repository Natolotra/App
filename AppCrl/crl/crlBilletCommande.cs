using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlBilletCommande
    /// </summary>
    public class crlBilletCommande
    {
        #region variable
        private string numBilletCommande;
        private string numTrajet;
        private string numProforma;
        private double montantBilletCommande;
        private int nombreBilletCommande;
        private string numCalculCategorieBillet;
        private string numCalculReductionBillet;
        private string numIndividu;
        #endregion

        #region encapsulation
        public string NumBilletCommande
        {
            get
            {
                return numBilletCommande;
            }
            set
            {
                numBilletCommande = value;
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
        public double MontantBilletCommande
        {
            get
            {
                return montantBilletCommande;
            }
            set
            {
                montantBilletCommande = value;
            }
        }
        public int NombreBilletCommande
        {
            get
            {
                return nombreBilletCommande;
            }
            set
            {
                nombreBilletCommande = value;
            }
        }
        public string NumCalculCategorieBillet
        {
            get { return numCalculCategorieBillet; }
            set { numCalculCategorieBillet = value; }
        }
        public string NumCalculReductionBillet
        {
            get { return numCalculReductionBillet; }
            set { numCalculReductionBillet = value; }
        }
        public string NumIndividu
        {
            get
            {
                return numIndividu;
            }
            set
            {
                numIndividu = value;
            }
        }
        #endregion

        #region variable d'objet
        public crlTrajet trajet;
        public crlCalculCategorieBillet calculCategorieBillet;
        public crlCalculReductionBillet calculReductionBillet;
        public crlIndividu individu;
        #endregion

        #region constructeur
        public crlBilletCommande()
        {
            this.MontantBilletCommande = 0;
            this.NombreBilletCommande = 0;
            this.NumBilletCommande = "";
            this.NumProforma = "";
            this.NumTrajet = "";
            this.NumCalculCategorieBillet = "";
            this.NumCalculReductionBillet = "";
            this.NumIndividu = "";

            this.trajet = null;
            this.calculCategorieBillet = null;
            this.calculReductionBillet = null;
            this.individu = null;
        }
        #endregion
    }
}