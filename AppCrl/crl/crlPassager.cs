

namespace arch.crl
{
    /// <summary>
    /// Objet Passager
    /// </summary>
    public class crlPassager
    {
        #region variable
        private string idPassager;
        private string nomPassager;
        private string prenomPassager;
        private string pieceIdentite;
        private string telephonePassager;
        #endregion

        #region encapsulation
        public string IdPassager
        {
            get { return idPassager; }
            set { idPassager = value; }
        }
        public string NomPassager
        {
            get { return nomPassager; }
            set { nomPassager = value; }
        }
        public string PrenomPassager
        {
            get { return prenomPassager; }
            set { prenomPassager = value; }
        }
        public string PieceIdentite
        {
            get { return pieceIdentite; }
            set { pieceIdentite = value; }
        }
        public string TelephonePassager
        {
            get { return telephonePassager; }
            set { telephonePassager = value; }
        }
        #endregion

        #region constructeur
        public crlPassager()
        {
            this.IdPassager = "";
            this.NomPassager = "";
            this.PieceIdentite = "";
            this.PrenomPassager = "";
            this.TelephonePassager = "";
        }
        #endregion
    }
}
