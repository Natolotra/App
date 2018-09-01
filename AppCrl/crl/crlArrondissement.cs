namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlArrondissement
    /// </summary>
    public class crlArrondissement
    {
        #region variable
        private string numArrondissement;
        private string arrondissement;
        private string numCommune;
        #endregion

        #region encapsulation
        public string NumArrondissement
        {
            get
            {
                return numArrondissement;
            }
            set
            {
                numArrondissement = value;
            }
        }
        public string Arrondissement
        {
            get
            {
                return arrondissement;
            }
            set
            {
                arrondissement = value;
            }
        }
        public string NumCommune
        {
            get
            {
                return numCommune;
            }
            set
            {
                numCommune = value;
            }
        }
        #endregion

        #region constructeur
        public crlArrondissement()
        {
            this.NumArrondissement = "";
            this.Arrondissement = "";
            this.NumCommune = "";
        }
        #endregion
    }
}