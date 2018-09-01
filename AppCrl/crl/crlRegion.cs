using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Objet region
    /// </summary>
    public class crlRegion
    {
        #region declaration variable
        private string nomRegion;
        private string nomProvince;
        #endregion

        #region encapsulation
        public string NomRegion
        {
            get
            {
                return nomRegion;
            }
            set
            {
                nomRegion = value;
            }
        }
        public string NomProvince
        {
            get
            {
                return nomProvince;
            }
            set
            {
                nomProvince = value;
            }
        }
        #endregion

        #region constructeur
        public crlRegion()
        {
            this.NomRegion = "";
            this.NomProvince = "";
        }
        #endregion
    }
}