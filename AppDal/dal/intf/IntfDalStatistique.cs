using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using arch.crl;

namespace arch.dal.intf
{
    /// <summary>
    /// Description résumée de IntfDalStatistique
    /// </summary>
    public interface IntfDalStatistique
    {
        double getNombrePassager(string idItineraire, string dateD, string dateF);

        void insertToGridCANombrePassagerJour(GridView gridView, DateTime dateDebut, DateTime dateFin, List<crlItineraire> itineraires);
        DataTable getDataTableNombrePassagerJour(DateTime dateDebut, DateTime dateFin, List<crlItineraire> itineraires);

        void insertToGridCANombrePassagerMois(GridView gridView, DateTime dateDebut, DateTime dateFin, List<crlItineraire> itineraires);
        DataTable getDataTableNombrePassagerMois(DateTime dateDebut, DateTime dateFin, List<crlItineraire> itineraires);

        void insertToGridCANombrePassagerAnnee(GridView gridView, DateTime dateDebut, DateTime dateFin, List<crlItineraire> itineraires);
        DataTable getDataTableNombrePassagerAnnee(DateTime dateDebut, DateTime dateFin, List<crlItineraire> itineraires);
    }
}