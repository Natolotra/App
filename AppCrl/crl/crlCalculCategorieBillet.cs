using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlCalculCategorieBillet
    /// </summary>
    public class crlCalculCategorieBillet
    {
        #region variable
        private string numCalculCategorieBillet;
        private double pourcentagePrixBillet;
        private string indicateurPrixBillet;
        #endregion

        #region encapsulation
        public string NumCalculCategorieBillet
        {
            get
            {
                return numCalculCategorieBillet;
            }
            set
            {
                numCalculCategorieBillet = value;
            }
        }
        public double PourcentagePrixBillet
        {
            get
            {
                return pourcentagePrixBillet;
            }
            set
            {
                pourcentagePrixBillet = value;
            }
        }
        public string IndicateurPrixBillet
        {
            get
            {
                return indicateurPrixBillet;
            }
            set
            {
                indicateurPrixBillet = value;
            }
        }
        #endregion

        #region constructeur
        public crlCalculCategorieBillet()
        {
            this.IndicateurPrixBillet = "";
            this.NumCalculCategorieBillet = "";
            this.PourcentagePrixBillet = 0;
        }
        #endregion
    }
}