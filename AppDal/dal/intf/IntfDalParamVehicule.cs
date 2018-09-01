using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.crl;

namespace arch.dal.intf
{
    /// <summary>
    /// Description résumée de IntfDalParamVehicule
    /// </summary>
    public interface IntfDalParamVehicule
    {
        crlParamVehicule selectParamVehicule(string numParamVehicule);
        string insertParamVehicule(crlParamVehicule paramVehicule, string sigleAgence);
        bool upDateParamVehicule(crlParamVehicule paramVehicule);
        string getNumParamVehicule(string sigleAgence);
    }
}