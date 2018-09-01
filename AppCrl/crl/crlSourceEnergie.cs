using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Objet source d'energie
    /// Source d'energie pour voiture, GASOIL,ESSENCE,...
    /// </summary>
    public class crlSourceEnergie
    {
        #region declaration
        private string sourceEnergie;
        private string commentaireSourceEnergie;
        #endregion

        #region encapsulation
        public string SourceEnergiePro
        {
            get
            {
                return sourceEnergie;
            }
            set
            {
                sourceEnergie = value;
            }
        }
        public string CommentaireSourceEnergie
        {
            get
            {
                return commentaireSourceEnergie;
            }
            set
            {
                commentaireSourceEnergie = value;
            }
        }
        #endregion

        #region constructeur
        public crlSourceEnergie()
        {
            this.SourceEnergiePro = "";
            this.CommentaireSourceEnergie = "";
        }
        #endregion
    }
}