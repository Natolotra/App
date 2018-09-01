using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Objet Zone
    /// Zone nationale, regionale, suburbaine
    /// </summary>
    public class crlZone
    {
        #region declaration
        private string zone;
        #endregion

        #region encapsulation
        public string ZonePro
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
        #endregion

        #region constructeur
        public crlZone()
        {
            this.ZonePro = "";
        }
        #endregion
    }
}