using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Objet type agence
    /// </summary>
    public class crlTypeAgence
    {
        #region variable
        private string typeAgence;
        private string descriptionTypeAgence;
        #endregion

        #region encapsulation
        public string TypeAgence
        {
            get
            {
                return typeAgence;
            }
            set
            {
                typeAgence = value;
            }
        }
        public string DescriptionTypeAgence
        {
            get
            {
                return descriptionTypeAgence;
            }
            set
            {
                descriptionTypeAgence = value;
            }
        }
        #endregion

        #region construction
        public crlTypeAgence()
        {
            this.TypeAgence = "";
            this.DescriptionTypeAgence = "";
        }
        #endregion
    }
}