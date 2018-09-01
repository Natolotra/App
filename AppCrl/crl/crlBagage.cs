namespace arch.crl
{
    /// <summary>
    /// Summary description for crlBagage
    /// </summary>
    public class crlBagage
    {
        #region variable
        private string idBagage;
        private double excedentPoid;
        private string prixExcedent;
        private string numRecu;
        #endregion

        #region encapsulation
        public string NumRecu
        {
            get { return numRecu; }
            set { numRecu = value; }
        }
        public string PrixExcedent
        {
            get { return prixExcedent; }
            set { prixExcedent = value; }
        }
        public double ExcedentPoid
        {
            get { return excedentPoid; }
            set { excedentPoid = value; }
        }
        public string IdBagage
        {
            get { return idBagage; }
            set { idBagage = value; }
        }
        #endregion

        #region variable d'objet
        //public crlRecu recu;
        public crlRecuEncaisser recu;
        #endregion

        #region constructeur
        public crlBagage()
        {
            this.ExcedentPoid = 0.00;
            this.IdBagage = "";
            this.NumRecu = "";
            this.PrixExcedent = "";
            this.recu = null;
        }
        #endregion
    }
}
