using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlUSPrixBase
    /// </summary>
    public class crlUSPrixBase
    {
        #region variable
        private string numPrixBase;
        private double montantPrixBase;
        private string descriptionPrixBase;
        private int niveauPrixBase;
        #endregion

        #region accesseur
        public string NumPrixBase
        {
            get
            {
                return numPrixBase;
            }
            set
            {
                numPrixBase = value;
            }
        }
        public double MontantPrixBase
        {
            get
            {
                return montantPrixBase;
            }
            set
            {
                montantPrixBase = value;
            }
        }
        public string DescriptionPrixBase
        {
            get
            {
                return descriptionPrixBase;
            }
            set
            {
                descriptionPrixBase = value;
            }
        }
        public int NiveauPrixBase
        {
            get
            {
                return niveauPrixBase;
            }
            set
            {
                niveauPrixBase = value;
            }
        }
        #endregion

        #region constructeur
        public crlUSPrixBase()
        {
            this.DescriptionPrixBase = "";
            this.MontantPrixBase = 0;
            this.NiveauPrixBase = 0;
            this.NumPrixBase = "";
        }
        #endregion
    }
}