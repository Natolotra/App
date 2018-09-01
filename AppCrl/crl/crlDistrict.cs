

namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlDistrict
    /// </summary>
    public class crlDistrict
    {
        #region variable
        private string numDistrict;
        private string district;
        private string nomRegion;
        #endregion

        #region encapsulation
        public string NumDistrict
        {
            get
            {
                return numDistrict;
            }
            set
            {
                numDistrict = value;
            }
        }
        public string District
        {
            get
            {
                return district;
            }
            set
            {
                district = value;
            }
        }
        public string NomRegion
        {
            get
            {
                return nomRegion;
            }
            set
            {
                nomRegion = value;
            }
        }
        #endregion

        #region constructeur
        public crlDistrict()
        {
            this.District = "";
            this.NomRegion = "";
            this.NumDistrict = "";
        }
        #endregion
    }
}