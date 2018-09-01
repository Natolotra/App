using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.dal.intf;
using MySql.Data.MySqlClient;
using arch.crl;
using System.Web.UI.WebControls;

namespace arch.dal.impl
{
    /// <summary>
    /// Description résumée de ImplDalTypePrelevement
    /// </summary>
    public class ImplDalTypePrelevement : IntfDalTypePrelevement
    {
        #region declaration
        IntfDalConnectBase serviceConnection = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalTypePrelevement()
        {
            this.serviceRessource = new ImplDalServiceRessource();
            this.serviceConnection = new ImplDalConnectBase(this.serviceRessource.getDefaultStrConnection());
        }
        public ImplDalTypePrelevement(string strConnection)
        {
            this.serviceConnection = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region methode
        crlTypePrelevement IntfDalTypePrelevement.selectTypePrelevement(string typePrelevement)
        {
            #region declaration
            crlTypePrelevement objTypePrelevement = null;
            #endregion

            #region implementation
            if (typePrelevement != "")
            {
                this.strCommande = "SELECT * FROM `typeprelevement` WHERE (`typePrelevement`='" + typePrelevement + "')";
                this.serviceConnection.openConnection();
                this.reader = this.serviceConnection.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            objTypePrelevement = new crlTypePrelevement();
                            objTypePrelevement.TypePrelevement = this.reader["typePrelevement"].ToString();
                            objTypePrelevement.Commentaire = this.reader["commentaire"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnection.closeConnection();
            }
            #endregion

            return objTypePrelevement;
        }

        void IntfDalTypePrelevement.loadDddlTypePrelevement(DropDownList ddl)
        {
            #region implementation
            ddl.Items.Clear();
            this.strCommande = "SELECT * FROM `typeprelevement`";
            this.serviceConnection.openConnection();
            reader = this.serviceConnection.select(this.strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    ddl.Items.Add("");
                    while (reader.Read())
                    {
                        ddl.Items.Add(new ListItem(this.reader["commentaire"].ToString(), this.reader["typePrelevement"].ToString()));
                    }
                }
                reader.Dispose();
            }
            this.serviceConnection.closeConnection();
            #endregion
        }
        #endregion

    }
}