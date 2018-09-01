using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlQuartier
    /// </summary>
    public class crlQuartier
    {
        #region variable
        private string numQuartier;
        private string quartier;
        private string numCommune;
        private string numArrondissement;
        #endregion

        #region encapsulation
        public string NumQuartier
        {
            get
            {
                return numQuartier;
            }
            set
            {
                numQuartier = value;
            }
        }
        public string Quartier
        {
            get
            {
                return quartier;
            }
            set
            {
                quartier = value;
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
        public string NumArrondissement
        {
            get
            {
                return numArrondissement;
            }
            set
            {
                numArrondissement = value;
            }
        }
        #endregion

        #region constructeur
        public crlQuartier()
        {
            this.NumQuartier = "";
            this.Quartier = "";
            this.NumArrondissement = "";
            this.NumCommune = "";
        }
        #endregion
    }
}