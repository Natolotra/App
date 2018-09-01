using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlUSAppareil
    /// </summary>
    public class crlUSAppareil
    {
        #region variable
        private string numAppareil;
        private string typeAppareil;
        private string numSerie;
        #endregion

        #region encapsulation
        public string NumAppareil
        {
            get
            {
                return numAppareil;
            }
            set
            {
                numAppareil = value;
            }
        }
        public string TypeAppareil
        {
            get
            {
                return typeAppareil;
            }
            set
            {
                typeAppareil = value;
            }
        }
        public string NumSerie
        {
            get
            {
                return numSerie;
            }
            set
            {
                numSerie = value;
            }
        }
        #endregion

        #region construction
        public crlUSAppareil()
        {
            this.NumAppareil = "";
            this.TypeAppareil = "";
            this.NumSerie = "";
        }
        #endregion
    }
}