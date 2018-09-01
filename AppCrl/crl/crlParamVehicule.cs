using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlParamVehicule
    /// </summary>
    public class crlParamVehicule
    {
        #region declaration
        private string numParamVehicule;
        private int nbPassagerMin;
        private double avanceCarburantMax;
        private double avanceChauffeurMax;
        private double poidBagageMax;
        private double fond;
        #endregion

        #region encapsulation
        public string NumParamVehicule
        {
            get
            {
                return numParamVehicule;
            }
            set
            {
                numParamVehicule = value;
            }
        }
        public int NbPassagerMin
        {
            get
            {
                return nbPassagerMin;
            }
            set
            {
                nbPassagerMin = value;
            }
        }
        public double AvanceCarburantMax
        {
            get
            {
                return avanceCarburantMax;
            }
            set
            {
                avanceCarburantMax = value;
            }
        }
        public double AvanceChauffeurMax
        {
            get
            {
                return avanceChauffeurMax;
            }
            set
            {
                avanceChauffeurMax = value;
            }
        }
        public double PoidBagageMax
        {
            get
            {
                return poidBagageMax;
            }
            set
            {
                poidBagageMax = value;
            }
        }
        public double Fond
        {
            get
            {
                return fond;
            }
            set
            {
                fond = value;
            }
        }
        #endregion

        #region construction
        public crlParamVehicule()
        {
            this.NumParamVehicule = "";
            this.NbPassagerMin = 0;
            this.AvanceCarburantMax = 0.00;
            this.AvanceChauffeurMax = 0.00;
            this.PoidBagageMax = 0.00;
            this.Fond = 0.00;
        }
        #endregion
    }
}