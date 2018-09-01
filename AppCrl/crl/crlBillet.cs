using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Objet Billet
    /// </summary>
    public class crlBillet
    {
        #region variable
        private string numBillet;
        private DateTime dateDeValidite;
        private string numTrajet;
        private string modePaiement;
        private string numIndividu;
        private string matriculeAgent;
        private string prixBillet;
        private string numCalculCategorieBillet;
        private string numCalculReductionBillet;
        private DateTime dateBillet;
        private string numDureeAbonnement;
        private string numVoyageAbonnement;
        private string numBilletCommande;
        #endregion

        #region encapsulation
        public string NumBillet
        {
            get
            {
                return numBillet;
            }
            set
            {
                numBillet = value;
            }
        }
        public DateTime DateDeValidite
        {
            get
            {
                return dateDeValidite;
            }
            set
            {
                dateDeValidite = value;
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
        public string MatriculeAgent
        {
            get
            {
                return matriculeAgent;
            }
            set
            {
                matriculeAgent = value;
            }
        }
        public string PrixBillet
        {
            get
            {
                return prixBillet;
            }
            set
            {
                prixBillet = value;
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
        public DateTime DateBillet
        {
            get
            {
                return dateBillet;
            }
            set
            {
                dateBillet = value;
            }
        }
        public string NumDureeAbonnement
        {
            get
            {
                return numDureeAbonnement;
            }
            set
            {
                numDureeAbonnement = value;
            }
        }
        public string NumVoyageAbonnement
        {
            get
            {
                return numVoyageAbonnement;
            }
            set
            {
                numVoyageAbonnement = value;
            }
        }
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
        #endregion

        #region variable d'objet
        public crlTrajet trajet;
        public crlIndividu individu;
        public crlAgent agent;
        public crlCalculCategorieBillet calculCategorieBillet;
        public crlCalculReductionBillet calculReductionBillet;
        #endregion

        #region construction
        public crlBillet()
        {
            this.NumBillet = "";
            this.NumTrajet = "";
            this.DateDeValidite = DateTime.Now;
            this.ModePaiement = "";
            this.NumIndividu = "";
            this.matriculeAgent = "";
            this.PrixBillet = "0,00";
            this.NumCalculCategorieBillet = "";
            this.NumCalculReductionBillet = "";
            this.DateBillet = DateTime.Now;
            this.NumDureeAbonnement = "";
            this.NumVoyageAbonnement = "";
            this.NumBilletCommande = "";

            this.individu = null;
            this.trajet = null;
            this.agent = null;
            this.calculCategorieBillet = null;
            this.calculReductionBillet = null;
        }
        #endregion
    }
}