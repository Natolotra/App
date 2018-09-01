

namespace arch.crl
{
    /// <summary>
    /// objet escort
    /// </summary>
    public class crlEscorte
    {
        #region variable
        private string matriculeEscorte;
        private string nomEscorte;
        private string prenomEscorte;
        private string cinEscorte;
        private string adresseEscorte;
        private string telephoneEscorte;
        private string telephoneMobileEscorte;
        #endregion

        #region encapsulation
        public string MatriculeEscorte
        {
            get { return matriculeEscorte; }
            set { matriculeEscorte = value; }
        }
        public string NomEscorte
        {
            get { return nomEscorte; }
            set { nomEscorte = value; }
        }
        public string PrenomEscorte
        {
            get { return prenomEscorte; }
            set { prenomEscorte = value; }
        }
        public string CinEscorte
        {
            get { return cinEscorte; }
            set { cinEscorte = value; }
        }
        public string AdresseEscorte
        {
            get { return adresseEscorte; }
            set { adresseEscorte = value; }
        }
        public string TelephoneEscorte
        {
            get { return telephoneEscorte; }
            set { telephoneEscorte = value; }
        }
        public string TelephoneMobileEscorte
        {
            get { return telephoneMobileEscorte; }
            set { telephoneMobileEscorte = value; }
        }
        #endregion

        #region constructeur
        public crlEscorte()
        {
            this.AdresseEscorte = "";
            this.CinEscorte = "";
            this.MatriculeEscorte = "";
            this.NomEscorte = "";
            this.PrenomEscorte = "";
            this.TelephoneEscorte = "";
            this.TelephoneMobileEscorte = "";
        }
        #endregion
    }
}
