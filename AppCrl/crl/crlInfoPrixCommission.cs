

namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlInfoPrixCommission
    /// </summary>
    public class crlInfoPrixCommission
    {
        #region variable
        private string numInfoPrixCommission;
        private string prix;
        private int paiement;
        #endregion

        #region encapsulation
        public string NumInfoPrixCommission
        {
            get
            {
                return numInfoPrixCommission;
            }
            set
            {
                numInfoPrixCommission = value;
            }
        }
        public string Prix
        {
            get
            {
                return prix;
            }
            set
            {
                prix = value;
            }
        }
        public int Paiement
        {
            get
            {
                return paiement;
            }
            set
            {
                paiement = value;
            }
        }
        #endregion

        #region constructeur
        public crlInfoPrixCommission()
        {
            this.NumInfoPrixCommission = "";
            this.Prix = "";
            this.Paiement = 0;
        }
        #endregion
    }
}