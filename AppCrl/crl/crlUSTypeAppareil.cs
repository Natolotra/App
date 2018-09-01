

namespace arch.crl
{
    /// <summary>
    /// Implementation du service type appareil
    /// </summary>
    public class crlUSTypeAppareil
    {
        #region variable
        private string typeAppareil;
        #endregion

        #region encapsulation
        public string TypeAppareil
        {
            get { return typeAppareil; }
            set { typeAppareil = value; }
        }
        #endregion

        #region constrcteur
        public crlUSTypeAppareil()
        {
            this.TypeAppareil = "";
        }
        #endregion
    }
}
