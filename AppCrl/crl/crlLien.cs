using System.Collections.Generic;

namespace arch.crl
{
    /// <summary>
    /// Objet lien
    /// </summary>
    public class crlLien
    {
        #region variable
        private string numLien;
        private string url;
        private string imageUrl;
        private string textLien;
        private int niveau;
        private string numLienMere;
        #endregion

        #region accesseur
        public string NumLien
        {
            get
            {
                return numLien;
            }
            set
            {
                numLien = value;
            }
        }
        public string Url
        {
            get
            {
                return url;
            }
            set
            {
                url = value;
            }
        }
        public string ImageUrl
        {
            get
            {
                return imageUrl;
            }
            set
            {
                imageUrl = value;
            }
        }
        public string TextLien
        {
            get
            {
                return textLien;
            }
            set
            {
                textLien = value;
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
        public string NumLienMere
        {
            get
            {
                return numLienMere;
            }
            set
            {
                numLienMere = value;
            }
        }
        #endregion

        #region variable d'objet
        public List<crlLien> liens;
        #endregion

        #region constructeur
        public crlLien()
        {
            this.ImageUrl = "home24.png";
            this.Niveau = 0;
            this.NumLien = "";
            this.NumLienMere = "";
            this.TextLien = "";
            this.Url = "Default.aspx";

            this.liens = null;
        }
        #endregion
    }
}