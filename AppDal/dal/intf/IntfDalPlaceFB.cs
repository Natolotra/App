using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.crl;
using System.Web.UI.WebControls;

namespace arch.dal.intf
{
    /// <summary>
    /// Interface du service placeFB
    /// </summary>
    public interface IntfDalPlaceFB
    {
        bool insertPlaceFB(crlPlaceFB PlaceFB);
        string insertPlaceForFB(crlFicheBord FicheBord);
        bool deletePlaceFB(crlPlaceFB PlaceFB);
        bool deletePlaceFB(string numerosFB);
        bool updatePlaceFB(crlPlaceFB PlaceFB);

        List<crlPlaceFB> selectPlaceFB(string numerosFB);
        crlPlaceFB selectPlaceFB(string numerosFB, string numPlace);

        string getNumPlaceFB(string numerosFB);

        string getNombrePlaceLibre(string numerosFB);

        void loadDdlPlaceFBLibre(DropDownList ddl,string numerosFB);
    }
}