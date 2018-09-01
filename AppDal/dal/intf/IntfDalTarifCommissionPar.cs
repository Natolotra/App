using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.crl;

namespace arch.dal.intf
{
    /// <summary>
    /// Description résumée de IntfDalTarifCommissionPar
    /// </summary>
    public interface IntfDalTarifCommissionPar
    {
        crlTarifCommissionPar selectTarifCommissionPar(string numTarifCommissionPar);
        crlTarifCommissionPar selectTarifCommissionPar(int typeCalcule);
    }
}