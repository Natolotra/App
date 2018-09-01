

namespace arch.crl
{
    /// <summary>
    /// Objet infoExedant bagage
    /// </summary>
    public class crlInfoExedantBagage
    {
        #region variable
        private string numInfoBagage;
        private double poidAutorise;
        private string prixExedantBagage;
        #endregion

        #region encapsulation
        public string NumInfoBagage
        {
            get
            {
                return numInfoBagage;
            }
            set
            {
                numInfoBagage = value;
            }
        }
        public double PoidAutorise
        {
            get
            {
                return poidAutorise;
            }
            set
            {
                poidAutorise = value;
            }
        }
        public string PrixExedantBagage
        {
            get
            {
                return prixExedantBagage;
            }
            set
            {
                prixExedantBagage = value;
            }
        }
        #endregion

        #region constructeur
        public crlInfoExedantBagage()
        {
            this.NumInfoBagage = "";
            this.PoidAutorise = 0.00;
            this.PrixExedantBagage = "";
        }
        #endregion
    }
}