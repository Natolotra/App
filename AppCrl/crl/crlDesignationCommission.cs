
namespace arch.crl
{
    /// <summary>
    /// Objet designation commission
    /// </summary>
    public class crlDesignationCommission
    {
        #region variable
        private string numDesignation;
        private string designation;
        private string typeCommission;
        private int paiement;
        #endregion

        #region encapsulation
        public string NumDesignation
        {
            get
            {
                return numDesignation;
            }
            set
            {
                numDesignation = value;
            }
        }
        public string Designation
        {
            get
            {
                return designation;
            }
            set
            {
                designation = value;
            }
        }
        public string TypeCommission
        {
            get
            {
                return typeCommission;
            }
            set
            {
                typeCommission = value;
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

        #region variable d'objet
        public crlTypeCommssion typeCommssionObj;
        #endregion

        #region constructeur
        public crlDesignationCommission()
        {
            this.Designation = "";
            this.NumDesignation = "";
            this.TypeCommission = "";
            this.Paiement = 0;
            this.typeCommssionObj = null;
        }
        #endregion
    }
}