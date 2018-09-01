

namespace arch.crl
{
    /// <summary>
    /// Objet gareRoutiere
    /// avec un variable d'objet crlProvince
    /// </summary>

    public class crlGareRoutiere
    {
        #region variable
        private string NumerosGareRoutiere;
        private string Localisation;
        private string SigleGare;
        private string NomProvince;
        private string numVille;
        #endregion

        #region Variable d'objet
        public crlProvince province;
        public crlVille ville;
        #endregion

        #region encapsulation
        public string numerosGareRoutiere
        {
            get
            {
                return NumerosGareRoutiere;
            }
            set
            {
                NumerosGareRoutiere = value;
            }
        }
        public string localisation
        {
            get
            {
                return Localisation;
            }
            set
            {
                Localisation = value;
            }
        }
        public string sigleGare
        {
            get
            {
                return SigleGare;
            }
            set
            {
                SigleGare = value;
            }
        }
        public string nomProvince
        {
            get
            {
                return NomProvince;
            }
            set
            {
                NomProvince = value;
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
        #endregion

        #region constructeur
        public crlGareRoutiere()
        {
            this.localisation = "";
            this.nomProvince = "";
            this.numerosGareRoutiere = "";
            this.sigleGare = "";
        }
        #endregion

    }
}