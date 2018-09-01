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
using arch.crl;
using System.Collections.Generic;

namespace arch.dal.intf
{
    /// <summary>
    /// Interface du service licence
    /// </summary>
    public interface IntfDalLicence
    {
        string insertLicence(crlLicence Licence, string sigleAgence);
        bool deleteLicence(crlLicence Licence);
        bool deleteLicence(string numLicence);
        bool updateLicence(crlLicence Licence);
        bool insertAssoItineraireLicence(string numLicence, string idItineraire);
        bool deleteAssoItineraireLicence(string numLicence, string idItineraire);

        string insertLicenceUS(crlLicence Licence, string sigleAgence);
        bool updateLicenceUS(crlLicence Licence);

        /// <summary>
        /// isLicence
        /// </summary>
        /// <param name="Agent"></param>
        /// <returns>s'il retourne 0 fales s'il retourne 1 numLicence</returns>
        int isLicence(crlLicence Licence);
        crlLicence selectLicence(string numLicence);
        void loadDdlTri(DropDownList ddlTri);
        List<crlItineraire> selectItineraire(string numLicence);

        string getNumLicence(string sigleAgence);

        void loadDdlItineraire(DropDownList ddlItineraire, List<crlItineraire> Itineraires);

        void insertToGridLicence(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableLicence(string strRqst);

        void insertToGridLicenceProprietaire(GridView gridView, string param, string paramLike, string valueLike, string numProprietaire);
        DataTable getDataTableLicenceProprietaire(string strRqst);

        void insertToGridLicenceUS(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableLicenceUS(string strRqst);

        void insertToGridLicenceUSAvecPropriete(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableLicenceUSAvecPropriete(string strRqst);
    }
}
