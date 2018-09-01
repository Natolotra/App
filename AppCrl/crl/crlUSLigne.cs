

namespace arch.crl
{
    /// <summary>
    /// Summary description for crlUSLigne
    /// </summary>
    public class crlUSLigne
    {
        #region variable
        private string numLigne;
        private string numCooperative;
        private string nomLigne;
        private string descriptionLigne;
        private string numArretD;
        private string numArretF;
        private string zone;
        private string numAxe;
        #endregion

        #region accesseur
        public string NumLigne
        {
            get { return numLigne; }
            set { numLigne = value; }
        }
        public string NumCooperative
        {
            get { return numCooperative; }
            set { numCooperative = value; }
        }
        public string NomLigne
        {
            get { return nomLigne; }
            set { nomLigne = value; }
        }
        public string DescriptionLigne
        {
            get { return descriptionLigne; }
            set { descriptionLigne = value; }
        }
        public string NumArretD
        {
            get { return numArretD; }
            set { numArretD = value; }
        }
        public string NumArretF
        {
            get { return numArretF; }
            set { numArretF = value; }
        }
        public string Zone
        {
            get { return zone; }
            set { zone = value; }
        }
        public string NumAxe
        {
            get
            {
                return numAxe;
            }
            set
            {
                numAxe = value;
            }
        }
        #endregion

        #region variable d'objet
        public crlUSArret arretD;
        public crlUSArret arretF;
        #endregion

        #region constructeur
        public crlUSLigne()
        {
            this.DescriptionLigne = "";
            this.NomLigne = "";
            this.NumCooperative = "";
            this.NumLigne = "";
            this.NumArretD = "";
            this.NumArretF = "";
            this.Zone = "";
            this.NumAxe = "";

            this.arretD = null;
            this.arretF = null;
        }
        #endregion
    }
}
