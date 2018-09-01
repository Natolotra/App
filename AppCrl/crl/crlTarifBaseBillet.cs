using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace arch.crl
{
    /// <summary>
    /// Objet crlTarifBaseBillet
    /// </summary>
    public class crlTarifBaseBillet
    {
        #region variable
        private string numTarifBaseBillet;
        private double montantTarifBaseBillet;
        #endregion

        #region encapsulation
        public string NumTarifBaseBillet
        {
            get
            {
                return numTarifBaseBillet;
            }
            set
            {
                numTarifBaseBillet = value;
            }
        }
        public double MontantTarifBaseBillet
        {
            get
            {
                return montantTarifBaseBillet;
            }
            set
            {
                montantTarifBaseBillet = value;
            }
        }
        #endregion

        #region constructeur
        public crlTarifBaseBillet()
        {
            this.numTarifBaseBillet = "";
            this.MontantTarifBaseBillet = 0.00;
        }
        #endregion
    }
}