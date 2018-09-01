using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Objet ville
    /// </summary>
    public class crlVille
    {
        #region variable
        private string numVille;
        private string nomVille;
        private string nomRegion;
        private string nomProvince;
        #endregion

        #region encapsulation
        public string NumVille
        {
            get
            {
                return numVille;
            }
            set
            {
                numVille = value;
            }
        }
        public string NomVille
        {
            get
            {
                return nomVille;
            }
            set
            {
                nomVille = value;
            }
        }
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

        #region variable d'objet
        public crlRegion region;
        public crlProvince province;
        #endregion

        #region construction
        public crlVille()
        {
            this.NomProvince = "";
            this.NomRegion = "";
            this.NomVille = "";
            this.NumVille = "";
            this.region = null;
            this.province = null;
        }
        #endregion
    }
}