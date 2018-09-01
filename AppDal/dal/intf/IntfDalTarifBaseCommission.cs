using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.crl;

namespace arch.dal.intf
{
    /// <summary>
    /// Description résumée de IntfDalTarifBaseCommission
    /// </summary>
    public interface IntfDalTarifBaseCommission
    {
        crlTarifBaseCommission selectTarifBaseCommission(string numTarifBaseCommission);

        string insertTarifBaseCommission(crlTarifBaseCommission TarifBaseCommission, string sigleAgence);

        bool insertAssociationTrajetTarifCommission(string numTrajet, string numTarifBaseCommission);

        bool updateTarifBaseCommission(crlTarifBaseCommission TarifBaseCommission);

        List<crlTarifBaseCommission> selectTarifBaseCommissions(string numTrajet);

        string getNumerosTarifBaseCommission(string sigleAgence);
    }
}