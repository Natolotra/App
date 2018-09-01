

namespace arch.crl
{
    /// <summary>
    /// objet societe de transport
    /// </summary>
    public class crlSocieteTransport
    {
        #region variable
        private string numerosSociete;
        private string nomSociete;
        #endregion

        #region encapsulation
        public string NumerosSociete
        {
            get { return numerosSociete; }
            set { numerosSociete = value; }
        }
        public string NomSociete
        {
            get { return nomSociete; }
            set { nomSociete = value; }
        }
        #endregion

        #region constructeur
        public crlSocieteTransport()
        {
            this.NomSociete = "";
            this.NumerosSociete = "";
        }
        #endregion
    }
}
