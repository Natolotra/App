using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Objet Vehicule
    /// </summary>
    public class crlVehicule
    {
        #region declaration
        private string numVehicule;
        private string numParamVehicule;
        private string sourceEnergie;
        private string numProprietaire;
        private string matriculeVehicule;
        private string marqueVehicule;
        private string typeVehicule;
        private string numSerieVehicule;
        private string numMoteurVehicule;
        private string puissanceVehicule;
        private string couleurVehicule;
        private int placesAssiseVehicule;
        private int nombreColoneVehicule;
        private double poidsTotalVehicule;
        private double poidsVideVehicule;
        private string imageVehicule;
        #endregion

        #region encapsulation
        public string NumVehicule
        {
            get
            {
                return numVehicule;
            }
            set
            {
                numVehicule = value;
            }
        }
        public string NumParamVehicule
        {
            get
            {
                return numParamVehicule;
            }
            set
            {
                numParamVehicule = value;
            }
        }
        public string SourceEnergie
        {
            get
            {
                return sourceEnergie;
            }
            set
            {
                sourceEnergie = value;
            }
        }
        public string NumProprietaire
        {
            get
            {
                return numProprietaire;
            }
            set
            {
                numProprietaire = value;
            }
        }
        public string MatriculeVehicule
        {
            get
            {
                return matriculeVehicule;
            }
            set
            {
                matriculeVehicule = value;
            }
        }
        public string MarqueVehicule
        {
            get
            {
                return marqueVehicule;
            }
            set
            {
                marqueVehicule = value;
            }
        }
        public string TypeVehicule
        {
            get
            {
                return typeVehicule;
            }
            set
            {
                typeVehicule = value;
            }
        }
        public string NumSerieVehicule
        {
            get
            {
                return numSerieVehicule;
            }
            set
            {
                numSerieVehicule = value;
            }
        }
        public string NumMoteurVehicule
        {
            get
            {
                return numMoteurVehicule;
            }
            set
            {
                numMoteurVehicule = value;
            }
        }
        public string PuissanceVehicule
        {
            get
            {
                return puissanceVehicule;
            }
            set
            {
                puissanceVehicule = value;
            }
        }
        public string CouleurVehicule
        {
            get
            {
                return couleurVehicule;
            }
            set
            {
                couleurVehicule = value;
            }
        }
        public int PlacesAssiseVehicule
        {
            get
            {
                return placesAssiseVehicule;
            }
            set
            {
                placesAssiseVehicule = value;
            }
        }
        public int NombreColoneVehicule
        {
            get
            {
                return nombreColoneVehicule;
            }
            set
            {
                nombreColoneVehicule = value;
            }
        }
        public double PoidsTotalVehicule
        {
            get
            {
                return poidsTotalVehicule;
            }
            set
            {
                poidsTotalVehicule = value;
            }
        }
        public double PoidsVideVehicule
        {
            get
            {
                return poidsVideVehicule;
            }
            set
            {
                poidsVideVehicule = value;
            }
        }
        public string ImageVehicule
        {
            get
            {
                return imageVehicule;
            }
            set
            {
                imageVehicule = value;
            }
        }
        #endregion

        #region variable d'objet
        public crlSourceEnergie sourceEnergieObj;
        public crlProprietaire proprietaire;
        public crlParamVehicule paramVehicule;
        #endregion

        #region constructeur
        public crlVehicule()
        {
            this.NumVehicule = "";
            this.NumParamVehicule = "";
            this.SourceEnergie = "";
            this.NumProprietaire = "";
            this.MatriculeVehicule = "";
            this.MarqueVehicule = "";
            this.TypeVehicule = "";
            this.NumSerieVehicule = "";
            this.NumMoteurVehicule = "";
            this.PuissanceVehicule = "";
            this.CouleurVehicule = "";
            this.PlacesAssiseVehicule = 0;
            this.NombreColoneVehicule = 0;
            this.PoidsTotalVehicule = 0.00;
            this.PoidsVideVehicule = 0.00;
            this.ImageVehicule = "vehicule.png";
            this.sourceEnergieObj = null;
            this.proprietaire = null;
        }
        #endregion
    }
}