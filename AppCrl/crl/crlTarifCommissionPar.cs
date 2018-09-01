using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlTarifCommissionPar
    /// </summary>
    public class crlTarifCommissionPar
    {
        #region variable
        private string numTarifCommissionPar;
        private string commentaireTarifCommissionPar;
        private int typeCalcule;
        #endregion

        #region encapsulation
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
        public string CommentaireTarifCommissionPar
        {
            get
            {
                return commentaireTarifCommissionPar;
            }
            set
            {
                commentaireTarifCommissionPar = value;
            }
        }
        public int TypeCalcule
        {
            get
            {
                return typeCalcule;
            }
            set
            {
                typeCalcule = value;
            }
        }
        #endregion

        #region constructeur
        public crlTarifCommissionPar()
        {
            this.NumTarifCommissionPar = "";
            this.CommentaireTarifCommissionPar = "";
            this.TypeCalcule = 0;
        }
        #endregion
    }
}