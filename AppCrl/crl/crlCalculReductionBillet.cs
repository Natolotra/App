namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlCalculReductionBillet
    /// </summary>
    public class crlCalculReductionBillet
    {
        #region variable
        private string numCalculReductionBillet;
        private string indicateurCalculReductionBillet;
        private double pourcentagePrixBillet;
        #endregion

        #region encapsulation
        public string NumCalculReductionBillet
        {
            get
            {
                return numCalculReductionBillet;
            }
            set
            {
                numCalculReductionBillet = value;
            }
        }
        public string IndicateurCalculReductionBillet
        {
            get
            {
                return indicateurCalculReductionBillet;
            }
            set
            {
                indicateurCalculReductionBillet = value;
            }
        }
        public double PourcentagePrixBillet
        {
            get
            {
                return pourcentagePrixBillet;
            }
            set
            {
                pourcentagePrixBillet = value;
            }
        }
        #endregion

        #region construction
        public crlCalculReductionBillet()
        {
            this.IndicateurCalculReductionBillet = "";
            this.NumCalculReductionBillet = "";
            this.PourcentagePrixBillet = 0;
        }
        #endregion
    }
}