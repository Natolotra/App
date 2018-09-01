
namespace arch.crl
{
    /// <summary>
    /// Summary description for crlUSCategorieBillet
    /// duree max de validite seulement en jour!
    /// </summary>
    public class crlUSCategorieBillet
    {
        #region variable
        private string numCategorieBillet;
        private string categorieBillet;
        private double reductionPourcentage;
        private double reductionMontant;
        private int dureeMaxValidite;
        #endregion

        #region encapsulation
        public string NumCategorieBillet
        {
            get
            {
                return numCategorieBillet;
            }
            set
            {
                numCategorieBillet = value;
            }
        }
        public string CategorieBillet
        {
            get
            {
                return categorieBillet;
            }
            set
            {
                categorieBillet = value;
            }
        }
        public double ReductionPourcentage
        {
            get
            {
                return reductionPourcentage;
            }
            set
            {
                reductionPourcentage = value;
            }
        }
        public double ReductionMontant
        {
            get { return reductionMontant; }
            set { reductionMontant = value; }
        }
        public int DureeMaxValidite
        {
            get
            {
                return dureeMaxValidite;
            }
            set
            {
                dureeMaxValidite = value;
            }
        }
        #endregion

        #region constructeur
        public crlUSCategorieBillet()
        {
            this.NumCategorieBillet = "";
            this.CategorieBillet = "";
            this.ReductionPourcentage = -1;
            this.ReductionMontant = -1;
            this.DureeMaxValidite = 180;
        }
        #endregion
    }
}