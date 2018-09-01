using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Objet type commission
    /// </summary>
    public class crlTypeCommssion
    {
        #region declaration
        private string typeCommission;
        private string commentaireTypeCommission;
        #endregion

        #region encapsulation
        public string TypeCommission
        {
            get
            {
                return typeCommission;
            }
            set
            {
                typeCommission = value;
            }
        }
        public string CommentaireTypeCommission
        {
            get
            {
                return commentaireTypeCommission;
            }
            set
            {
                commentaireTypeCommission = value;
            }
        } 
        #endregion

        #region constructeur
        public crlTypeCommssion()
        {
            this.TypeCommission = "";
            this.CommentaireTypeCommission = "";
        }
        #endregion
    }
}