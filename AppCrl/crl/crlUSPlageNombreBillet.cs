using System;


namespace arch.crl
{
    /// <summary>
    /// Objet crlUSPlageNombreBillet
    /// </summary>
    public class crlUSPlageNombreBillet
    {
        #region variable
        private string numPlageNombreBillet;
        private int nombreD;
        private int nombreF;
        private string numReductionBillet;
        private TimeSpan dureeDeValidite;
        #endregion

        #region encapsulation
        public string NumPlageNombreBillet
        {
            get
            {
                return numPlageNombreBillet;
            }
            set
            {
                numPlageNombreBillet = value;
            }
        }
        public int NombreD
        {
            get
            {
                return nombreD;
            }
            set
            {
                nombreD = value;
            }
        }
        public int NombreF
        {
            get
            {
                return nombreF;
            }
            set
            {
                nombreF = value;
            }
        }
        public string NumReductionBillet
        {
            get
            {
                return numReductionBillet;
            }
            set
            {
                numReductionBillet = value;
            }
        }
        public TimeSpan DureeDeValidite
        {
            get
            {
                return dureeDeValidite;
            }
            set
            {
                dureeDeValidite = value;
            }
        }
        #endregion

        #region constructeur
        public crlUSPlageNombreBillet()
        {
            this.NombreD = 0;
            this.NombreF = 0;
            this.NumPlageNombreBillet = "";
            this.NumReductionBillet = "";
            this.DureeDeValidite = new TimeSpan(0, 0, 0, 0);
        }
        #endregion
    }
}
