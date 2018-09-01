using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlRouteNationale
    /// </summary>
    public class crlRouteNationale
    {
        #region variable
        private string routeNationale;
        private double distanceRN;
        #endregion

        #region encapsulation
        public string RouteNationale
        {
            get
            {
                return routeNationale;
            }
            set
            {
                routeNationale = value;
            }
        }
        public double DistanceRN
        {
            get
            {
                return distanceRN;
            }
            set
            {
                distanceRN = value;
            }
        }
        #endregion

        #region variable d'objet
        public List<crlVille> villes;
        #endregion

        #region constructeur
        public crlRouteNationale()
        {
            this.RouteNationale = "";
            this.DistanceRN = 0.00;
            this.villes = null;
        }
        #endregion
    }
}