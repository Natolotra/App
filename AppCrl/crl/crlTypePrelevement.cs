using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlTypePrelevement
    /// </summary>
    public class crlTypePrelevement
    {
        #region variable
        private string typePrelevement;
        private string commentaire;
        #endregion

        #region encapsulation
        public string TypePrelevement
        {
            get
            {
                return typePrelevement;
            }
            set
            {
                typePrelevement = value;
            }
        }
        public string Commentaire
        {
            get
            {
                return commentaire;
            }
            set
            {
                commentaire = value;
            }
        }
        #endregion

        #region constructeur
        public crlTypePrelevement()
        {
            this.TypePrelevement = "";
            this.Commentaire = "";
        }
        #endregion
    }
}