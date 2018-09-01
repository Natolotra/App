using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.dal.intf;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace arch.dal.impl
{
    /// <summary>
    /// Description résumée de ImplDalSituationFamiliale
    /// </summary>
    public class ImplDalSituationFamiliale : IntfDalSituationFamiliale
    {
         #region declaration variable
        ImplDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalSituationFamiliale()
        {
            this.serviceRessource = new ImplDalServiceRessource();
            this.serviceConnectBase = new ImplDalConnectBase(this.serviceRessource.getDefaultStrConnection());
        }
        public ImplDalSituationFamiliale(string strConnection)
        {
            this.serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region methode
        void IntfDalSituationFamiliale.loadDddlSituationFamiliale(DropDownList ddl)
        {
            #region implementation
            ddl.Items.Clear();
            this.strCommande = "SELECT * FROM `situationfamiliale`";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(this.strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    ddl.Items.Add("");
                    while (reader.Read())
                    {
                        ddl.Items.Add(reader["situationFamiliale"].ToString());
                    }
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            #endregion
        }
        #endregion
    }
}