using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlUSAxe
    /// </summary>
    public class crlUSAxe
    {
        #region variable
        private string numAxe;
        private string nomAxe;
        private string descriptionAxe;
        #endregion

        #region accesseur
        public string NumAxe
        {
            get
            {
                return numAxe;
            }
            set
            {
                numAxe = value;
            }
        }
        public string NomAxe
        {
            get
            {
                return nomAxe;
            }
            set
            {
                nomAxe = value;
            }
        }
        public string DescriptionAxe
        {
            get { return descriptionAxe; }
            set { descriptionAxe = value; }
        }
        #endregion

        #region variable d'objet
        public List<crlUSLieu> lieux;
        #endregion

        #region constructeur
        public crlUSAxe()
        {
            this.NumAxe = "";
            this.NomAxe = "";
            this.DescriptionAxe = "";

            this.lieux = null;
        }
        #endregion
    }
}