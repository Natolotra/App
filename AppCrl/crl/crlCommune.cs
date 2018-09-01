
namespace arch.crl
{
    /// <summary>
    /// Summary description for crlCommune
    /// </summary>
    public class crlCommune
    {
        #region variable
        string numCommune;
        string commune;
        string numDistrict;
        #endregion

        #region encapsulation
        public string NumCommune
        {
            get { return numCommune; }
            set { numCommune = value; }
        }
        public string Commune
        {
            get { return commune; }
            set { commune = value; }
        }
        public string NumDistrict
        {
            get { return numDistrict; }
            set { numDistrict = value; }
        }
        #endregion

        #region constructeur
        public crlCommune()
        {
            this.Commune = "";
            this.NumCommune = "";
            this.NumDistrict = "";
        }
        #endregion
    }
}