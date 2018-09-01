using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.crl;

namespace arch.dal.intf
{
    /// <summary>
    /// Interface du service Session agence
    /// </summary>
    public interface IntfDalSessionAgence
    {
        crlSessionAgence selectSessionAgence(string numSessionAgence);
        string insertSessionAgence(crlSessionAgence sessionAgence, string sigleAgence);
        bool updateSessionAgence(crlSessionAgence sessionAgence);

        crlSessionAgence getSessionAgenceEncours(string numAgence);

        string getNumSessionAgence(string sigleAgence);

        double getMontantTotalBillet(string numSessionAgence);
        double getMontantTotalCommission(string numSessionAgence);
        double getMontantTotalDureeAbonnement(string numSessionAgence);
        double getMontantTotalVoyageAbonnement(string numSessionAgence);
        double getMontantTotalRecuEncaisser(string numSessionAgence);
        double getMontantTotalRecuEncaisserCheque(string numSessionAgence);
        double getMontantTotalRecuEncaisserEspece(string numSessionAgence);
        double getMontantTotalRecuAD(string numSessionAgence);
    }
}