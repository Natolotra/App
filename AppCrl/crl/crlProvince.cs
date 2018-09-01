

namespace arch.crl
{
    /// <summary>
    /// province,
    /// variable NomProvince
    /// </summary>
    public class crlProvince
    {
        #region declaration variable
        private string NomProvince;
        #endregion

        #region encapsulation
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
        #endregion

        #region constructeur
        public crlProvince()
        {
            this.nomProvince = "";
        }
        #endregion
    }
}
