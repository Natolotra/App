using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.crl;
using System.Web.UI.WebControls;
using System.Data;


namespace arch.dal.intf
{
    /// <summary>
    /// Description résumée de IntfDalRouteNationale
    /// </summary>
    public interface IntfDalRouteNationale
    {
        string insertRouteNationale(crlRouteNationale routeNationale);

        crlRouteNationale selectRouteNationale(string routeNationale);
        List<crlRouteNationale> selectRNForItineraire(string idItineraire);

        bool insertAssocVilleRouteNationale(string numVille, string routeNationale);
        bool deleteAssocVilleRouteNationale(string numVille, string routeNationale);

        bool updateRouteNationale(crlRouteNationale routeNationale, string strRouteNationale);

        bool deleteRouteNationale(string routeNationale);

        bool isRouteNationale(crlRouteNationale routeNationale);
        string isRouteNationale(crlRouteNationale routeNationale, string strRouteNationale);

        void insertToGridRouteNationale(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableRouteNationale(string strRqst);

        void insertToGridRouteNationaleItineraire(GridView gridView, string param, string paramLike, string valueLike, string idItineraire);
        DataTable getDataTableRouteNationaleItineraire(string strRqst);

        void insertToGridRouteNationaleNotItineraire(GridView gridView, string param, string paramLike, string valueLike, List<crlRouteNationale> routeNationales);
        DataTable getDataTableRouteNationaleNotItineraire(string strRqst);
    }
}