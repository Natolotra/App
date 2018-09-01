
namespace arch.crl
{
    /// <summary>
    /// Objet trajet
    /// </summary>
    public class crlUSTrajet
    {
        #region variable
        private string numTrajet;
        private double distanceTrajet;
        private string dureeTrajet;
        private string numArretD;
        private string numArretF;
        #endregion

        #region accesseur
        public string NumTrajet
        {
            get { return numTrajet; }
            set { numTrajet = value; }
        }
        public double DistanceTrajet
        {
            get { return distanceTrajet; }
            set { distanceTrajet = value; }
        }
        public string DureeTrajet
        {
            get { return dureeTrajet; }
            set { dureeTrajet = value; }
        }
        public string NumArretD
        {
            get { return numArretD; }
            set { numArretD = value; }
        }
        public string NumArretF
        {
            get { return numArretF; }
            set { numArretF = value; }
        }
        #endregion

        #region variable d'objet
        public crlUSArret arretD;
        public crlUSArret arretF;
        #endregion

        #region constructeur
        public crlUSTrajet()
        {
            this.NumTrajet = "";
            this.DistanceTrajet = 0;
            this.DureeTrajet = "";
            this.NumArretD = "";
            this.NumArretF = "";

            this.arretD = null;
            this.arretF = null;
        }
        #endregion
    }
}