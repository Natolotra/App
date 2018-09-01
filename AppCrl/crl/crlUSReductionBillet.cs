

namespace arch.crl
{
    /// <summary>
    /// Objet reduction billet Urbaine suburbaine
    /// </summary>
    public class crlUSReductionBillet
    {
        #region variable
        private string numReductionBillet;
        private string reductionBillet;
        private double reductionPourcentage;
        private double reductionMontant;
        #endregion

        #region encapsulation
        public string NumReductionBillet
        {
            get { return numReductionBillet; }
            set { numReductionBillet = value; }
        }
        public string ReductionBillet
        {
            get { return reductionBillet; }
            set { reductionBillet = value; }
        }
        public double ReductionPourcentage
        {
            get { return reductionPourcentage; }
            set { reductionPourcentage = value; }
        }
        public double ReductionMontant
        {
            get { return reductionMontant; }
            set { reductionMontant = value; }
        }
        #endregion

        #region constructeur
        public crlUSReductionBillet()
        {
            this.NumReductionBillet = "";
            this.ReductionBillet = "";
            this.ReductionMontant = -1;
            this.ReductionPourcentage = -1;
        }
        #endregion
    }
}