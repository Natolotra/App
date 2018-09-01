namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlAgence
    /// </summary>
    public class crlAgence
    {
        #region variable
        private string numAgence;
        private string typeAgence;
        private string numVille;
        private string sigleAgence;
        private string nomAgence;
        private string localisationAgence;
        #endregion

        #region encapsulation
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
        public string TypeAgence
        {
            get
            {
                return typeAgence;
            }
            set
            {
                typeAgence = value;
            }
        }
        public string NumVille
        {
            get
            {
                return numVille;
            }
            set
            {
                numVille = value;
            }
        }
        public string SigleAgence
        {
            get
            {
                return sigleAgence;
            }
            set
            {
                sigleAgence = value;
            }
        }
        public string NomAgence
        {
            get
            {
                return nomAgence;
            }
            set
            {
                nomAgence = value;
            }
        }
        public string LocalisationAgence
        {
            get
            {
                return localisationAgence;
            }
            set
            {
                localisationAgence = value;
            }
        }
        #endregion

        #region variable d'objet
        public crlTypeAgence typeAgenceObj;
        public crlVille ville;
        public crlSessionAgence sessionAgence;
        #endregion

        #region constructeur
        public crlAgence()
        {
            this.NumAgence = "";
            this.NumVille = "";
            this.TypeAgence = "";
            this.SigleAgence = "";
            this.NomAgence = "";
            this.LocalisationAgence = "";
            this.typeAgence = null;
            this.ville = null;
            this.sessionAgence = null;
        }
        #endregion
    }
}