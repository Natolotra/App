using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Objet type recu
    /// </summary>
    public class crlTypeRecuAD
    {
        #region declaration
        private string typeRecuAD;
        private string commentaireTypeRecuAD;
        #endregion

        #region encapsulation
        public string TypeRecuAD
        {
            get
            {
                return typeRecuAD;
            }
            set
            {
                typeRecuAD = value;
            }
        }
        public string CommentaireTypeRecuAD
        {
            get
            {
                return commentaireTypeRecuAD;
            }
            set
            {
                commentaireTypeRecuAD = value;
            }
        }
        #endregion

        #region constructeur
        public crlTypeRecuAD()
        {
            this.TypeRecuAD = "";
            this.CommentaireTypeRecuAD = "";
        }
        #endregion
    }
}