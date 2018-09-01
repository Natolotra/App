using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlUSZone
    /// </summary>
    public class crlUSZone
    {
        #region variable
        private string numZone;
        private string nomZone;
        private int niveau;
        private string numCommune;
        #endregion

        #region accesseur
        public string NumZone
        {
            get
            {
                return numZone;
            }
            set
            {
                numZone = value;
            }
        }
        public string NomZone
        {
            get
            {
                return nomZone;
            }
            set
            {
                nomZone = value;
            }
        }
        public int Niveau
        {
            get
            {
                return niveau;
            }
            set
            {
                niveau = value;
            }
        }
        public string NumCommune
        {
            get
            {
                return numCommune;
            }
            set
            {
                numCommune = value;
            }
        }
        #endregion

        #region variable d'objet
        public List<crlUSLieu> lieux;
        #endregion

        #region constructeur
        public crlUSZone()
        {
            this.Niveau = 0;
            this.NumZone = "";
            this.NomZone = "";
            this.NumCommune = "";

            this.lieux = null;
        }
        #endregion
    }
}