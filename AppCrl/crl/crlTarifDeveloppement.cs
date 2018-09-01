using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlTarifDeveloppement
    /// </summary>
    public class crlTarifDeveloppement
    {
        #region variable
        private string numTarifDeveloppement;
        private string zone;
        private double montantTarifDeveloppement;
        private string commentaireTarifDeveloppement;
        #endregion 

        #region encapsulation
        public string NumTarifDeveloppement
        {
            get
            {
                return numTarifDeveloppement;
            }
            set
            {
                numTarifDeveloppement = value;
            }
        }
        public string Zone
        {
            get
            {
                return zone;
            }
            set
            {
                zone = value;
            }
        }
        public double MontantTarifDeveloppement
        {
            get
            {
                return montantTarifDeveloppement;
            }
            set
            {
                montantTarifDeveloppement = value;
            }
        }
        public string CommentaireTarifDeveloppement
        {
            get
            {
                return commentaireTarifDeveloppement;
            }
            set
            {
                commentaireTarifDeveloppement = value;
            }
        }
        #endregion

        #region constructeur
        public crlTarifDeveloppement()
        {
            this.NumTarifDeveloppement = "";
            this.Zone = "";
            this.MontantTarifDeveloppement = 0.00;
            this.CommentaireTarifDeveloppement = "";
        }
        #endregion
    }
}