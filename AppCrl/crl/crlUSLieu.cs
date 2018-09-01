using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Objet pour lieu
    /// </summary>
    public class crlUSLieu
    {
        #region variable
        private string numLieu;
        private string descriptionLieu;
        private string numZone;
        private string numQuartier;
        #endregion

        #region accesseur
        public string NumLieu
        {
            get
            {
                return numLieu;
            }
            set
            {
                numLieu = value;
            }
        }
        public string DescriptionLieu
        {
            get
            {
                return descriptionLieu;
            }
            set
            {
                descriptionLieu = value;
            }
        }
        public string NumZone
        {
            get
            {
                return numZone;
            }
            set
            {
                numZone = value;
            }
        }
        public string NumQuartier
        {
            get
            {
                return numQuartier;
            }
            set
            {
                numQuartier = value;
            }
        }
        #endregion

        #region variable d'objet
        public List<crlUSArret> arrets;
        #endregion

        #region constructeur
        public crlUSLieu()
        {
            this.NumLieu = "";
            this.DescriptionLieu = "";
            this.NumZone = "";
            this.NumQuartier = "";

            this.arrets = null;
        }
        #endregion
    }
}