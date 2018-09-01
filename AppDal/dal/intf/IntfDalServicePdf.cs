using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace arch.dal.intf
{
    /// <summary>
    /// Interface du service pdf
    /// </summary>
    public interface IntfDalServicePdf
    {
        bool printAutorisationVoyage(string numerosAV, string urlSaving);
        bool printVerification(string idVerification, string urlSaving);
        bool printFicheBord(string numAutorisationDepart, string urlSaving);
        bool printFacture(string numFacture, string urlSaving);
        bool printRecu(string numRecu, string urlSaving);
        bool printRecuEncaisser(string numRecuEncaisser, string urlSaving);
        bool printRecuDecaisser(string numRecuDecaisser, string urlSaving);
        bool printRecuAD(string numRecuAD, string urlSaving);
        bool printCommission(string numCommission, string urlSaving);
        bool printBillet(string numBillet, string urlSaving);
        bool printVoyage(string idVoyage, string urlSaving);
        bool printProforma(string numProforma, string urlSaving);
        bool printBonCommande(string numBonDeCommande, string urlSaving);

        bool printUSBillet(string numBillet, string urlSaving);
    }
}