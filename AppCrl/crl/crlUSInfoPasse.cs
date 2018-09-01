using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Objet info passe, Information sur les types de passe de voyage!
    /// </summary>
    public class crlUSInfoPasse
    {
        #region variable
        private string numInfoPasse;
        private int nombrePasse;
        private int niveau;
        private string numReductionBillet;
        #endregion

        #region accesseur
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
        public int NombrePasse
        {
            get
            {
                return nombrePasse;
            }
            set
            {
                nombrePasse = value;
            }
        }
        public int Niveau
        {
            get
            {
                return niveau;
            }
            set
            {
                niveau = value;
            }
        }
        public string NumReductionBillet
        {
            get
            {
                return numReductionBillet;
            }
            set
            {
                numReductionBillet = value;
            }
        }
        #endregion

        #region variable d'objet
        public crlUSReductionBillet reductionBillet;
        #endregion

        #region constructeur
        public crlUSInfoPasse()
        {
            this.NumInfoPasse = "";
            this.NombrePasse = 0;
            this.Niveau = 0;
            this.NumReductionBillet = "";

            this.reductionBillet = null;
        }
        #endregion
    }
}