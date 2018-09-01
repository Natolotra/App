using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Objet situation familiale
    /// </summary>
    public class crlSituationFamiliale
    {
        #region variable
        private string situationFamiliale;
        #endregion

        #region accesseur
        public string SituationFamiliale
        {
            get
            {
                return situationFamiliale;
            }
            set
            {
                situationFamiliale = value;
            }
        }
        #endregion

        #region constructeur
        public crlSituationFamiliale()
        {
            this.SituationFamiliale = "";
        }
        #endregion
    }
}