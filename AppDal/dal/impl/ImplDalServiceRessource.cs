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
using arch.dal.intf;
using System.IO;

namespace arch.dal.impl
{
    public class ImplDalServiceRessource : IntfDalServiceRessource
    {
        #region variable de class
        private string appPath = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
        #endregion

        #region constructeur
        public ImplDalServiceRessource()
        { }
        #endregion

        #region IntfDalServiceRessource Membres
        string IntfDalServiceRessource.getDefaultStrConnection()
        {
            return ConfigurationManager.ConnectionStrings["DbApp"].ConnectionString;
        }

        bool IntfDalServiceRessource.testBase(string strConnection)
        {
            bool isConnection = false;
            ImplDalConnectBase connection = new ImplDalConnectBase(strConnection);

            connection.openConnection();
            isConnection = connection.IsConnection;
            connection.closeConnection();

            return isConnection;
        }
        #endregion
    }
}