

namespace arch.crl
{
    /// <summary>
    /// Objet arret
    /// </summary>
    public class crlUSArret
    {
        #region variable
        private string numArret;
        private string numLieu;
        private string nomArret;
        private string descriptionArret;
        #endregion

        #region accesseur
        public string NumArret
        {
            get { return numArret; }
            set { numArret = value; }
        }
        public string NumLieu
        {
            get { return numLieu; }
            set { numLieu = value; }
        }
        public string NomArret
        {
            get { return nomArret; }
            set { nomArret = value; }
        }
        public string DescriptionArret
        {
            get { return descriptionArret; }
            set { descriptionArret = value; }
        }
        #endregion

        #region constructeur
        public crlUSArret()
        {
            this.DescriptionArret = "";
            this.NomArret = "";
            this.NumArret = "";
            this.NumLieu = "";
        }
        #endregion
    }
}
