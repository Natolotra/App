using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlTarifBaseCommission
    /// </summary>
    public class crlTarifBaseCommission
    {
        #region variable
        private string numTarifBaseCommission;
        private string numTarifCommissionPar;
        private double montantTarifBaseCommission;
        #endregion

        #region encapsulation
        public string NumTarifBaseCommission
        {
            get
            {
                return numTarifBaseCommission;
            }
            set
            {
                numTarifBaseCommission = value;
            }
        }
        public string NumTarifCommissionPar
        {
            get
            {
                return numTarifCommissionPar;
            }
            set
            {
                numTarifCommissionPar = value;
            }
        }
        public double MontantTarifBaseCommission
        {
            get
            {
                return montantTarifBaseCommission;
            }
            set
            {
                montantTarifBaseCommission = value;
            }
        }
        #endregion

        #region variable d'objet
        public crlTarifCommissionPar tarifCommissionPar; 
        #endregion

        #region construction
        public crlTarifBaseCommission()
        {
            this.NumTarifBaseCommission = "";
            this.NumTarifCommissionPar = "";
            this.tarifCommissionPar = null;
        }
        #endregion
    }
}