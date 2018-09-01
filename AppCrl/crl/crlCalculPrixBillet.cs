namespace arch.crl
{
    /// <summary>
    /// Objet crlCalculPrixBillet
    /// </summary>
    public class crlCalculPrixBillet
    {
        #region variable
        private string numCalculPrixBillet;
        private string indicateurCalculPrixBillet;
        private double pourcentagePrixBillet;
        #endregion

        #region encapsulation
        public string NumCalculPrixBillet
        {
            get
            {
                return numCalculPrixBillet;
            }
            set
            {
                numCalculPrixBillet = value;
            }
        }
        public string IndicateurCalculPrixBillet
        {
            get
            {
                return indicateurCalculPrixBillet;
            }
            set
            {
                indicateurCalculPrixBillet = value;
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

        #region constructeur
        public crlCalculPrixBillet()
        {
            this.IndicateurCalculPrixBillet = "";
            this.NumCalculPrixBillet = "";
            this.PourcentagePrixBillet = 0.00;
        }
        #endregion
    }
}