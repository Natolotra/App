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
    /// Description résumée de ImplDalTypeAgence
    /// </summary>
    public class ImplDalTypeAgence : IntfDalTypeAgence
    {
        #region declaration
        IntfDalConnectBase serviceConnection = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalTypeAgence()
        {
            this.serviceRessource = new ImplDalServiceRessource();
            this.serviceConnection = new ImplDalConnectBase(this.serviceRessource.getDefaultStrConnection());
        }
        public ImplDalTypeAgence(string strConnection)
        {
            this.serviceConnection = new ImplDalConnectBase(strConnection);
        }
        #endregion


        #region methode
        crlTypeAgence IntfDalTypeAgence.selectTypeAgence(string typeAgence)
        {
            #region declaration
            crlTypeAgence typeAgenceObj = null;
            #endregion

            #region implementation
            if (typeAgence != "")
            {
                this.strCommande = "SELECT * FROM `typeagence` WHERE `TypeAgence`='" + typeAgence + "'";
                this.serviceConnection.openConnection();
                this.reader = this.serviceConnection.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            typeAgenceObj = new crlTypeAgence();

                            typeAgenceObj.TypeAgence = this.reader["TypeAgence"].ToString();
                            typeAgenceObj.DescriptionTypeAgence = this.reader["DescriptionTypeAgence"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnection.closeConnection();
            }
            #endregion

            return typeAgenceObj;
        }

        void IntfDalTypeAgence.loadDddlTypeAgence(DropDownList ddl)
        {
            #region implementation
            ddl.Items.Clear();
            this.strCommande = "SELECT * FROM `typeagence`";
            this.serviceConnection.openConnection();
            reader = this.serviceConnection.select(this.strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    ddl.Items.Add("");
                    while (reader.Read())
                    {
                        ddl.Items.Add(reader["typeAgence"].ToString());
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