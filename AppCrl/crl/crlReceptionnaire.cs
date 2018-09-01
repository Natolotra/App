

namespace arch.crl
{
    /// <summary>
    /// objet personne
    /// </summary>
    public class crlReceptionnaire
    {
        #region variable
        private string idPersonne;
        private string nomPersonne;
        private string prenomPersonne;
        private string adressePersonne;
        private string telephone;
        #endregion

        #region encapsulation
        public string IdPersonne
        {
            get { return idPersonne; }
            set { idPersonne = value; }
        }
        public string NomPersonne
        {
            get { return nomPersonne; }
            set { nomPersonne = value; }
        }
        public string PrenomPersonne
        {
            get { return prenomPersonne; }
            set { prenomPersonne = value; }
        }
        public string AdressePersonne
        {
            get
            {
                return adressePersonne;
            }
            set
            {
                adressePersonne = value;
            }
        }
        public string Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        }
        #endregion

        #region constructeur
        public crlReceptionnaire()
        {
           
            this.IdPersonne = "";
            this.NomPersonne = "";
            this.PrenomPersonne = "";
            this.AdressePersonne = "";
            this.Telephone = "";
        }
        #endregion
    }
}
