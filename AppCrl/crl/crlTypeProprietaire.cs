using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Objet type proprietaire
    /// Individu, Société, Organisme, ...
    /// </summary>
    public class crlTypeProprietaire
    {
        #region declaration
        private string typeProprietaire;
        #endregion

        #region encapsulation
        public string TypeProprietairePro
        {
            get
            {
                return typeProprietaire;
            }
            set
            {
                typeProprietaire = value;
            }
        }
        #endregion

        #region constructeur
        public crlTypeProprietaire()
        {
            this.TypeProprietairePro = "";
        }
        #endregion
    }
}