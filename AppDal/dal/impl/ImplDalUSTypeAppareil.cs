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
using MySql.Data.MySqlClient;
using arch.crl;

namespace arch.dal.impl
{
    /// <summary>
    /// Implementation du service type appareil
    /// </summary>
    public class ImplDalUSTypeAppareil : IntfDalUSTypeAppareil
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalUSTypeAppareil(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalUSTypeAppareil()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion



        #region IntfDalUSTypeAppareil Members

        void IntfDalUSTypeAppareil.loadDddlTypeAppareil(DropDownList ddl)
        {
            #region implementation
            if(ddl != null)
            {
                ddl.Items.Clear();
                ddl.Items.Add("");
                this.strCommande = "SELECT * FROM `ustypeappareil`";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        while (this.reader.Read()) 
                        {
                            ddl.Items.Add(this.reader["typeAppareil"].ToString());
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }

        #endregion

        #region IntfDalUSTypeAppareil Members


        string IntfDalUSTypeAppareil.insertTypeAppareil(crlUSTypeAppareil typeAppareil)
        {
            #region declaration
            string typeAppareilStr = "";
            int nbInsert = 0;
            #endregion

            #region implementation
            if (typeAppareil != null) 
            {
                this.strCommande = "INSERT INTO `ustypeappareil` (`typeAppareil`)";
                this.strCommande += " VALUES ('" + typeAppareil.TypeAppareil + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1) 
                {
                    typeAppareilStr = typeAppareil.TypeAppareil;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return typeAppareilStr;
        }

        bool IntfDalUSTypeAppareil.updateTypeAppareil(crlUSTypeAppareil typeAppareilObj, string typeAppareil)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            #endregion

            #region impementation
            if (typeAppareilObj != null && typeAppareil != "") 
            {
                this.strCommande = "UPDATE `ustypeappareil` SET `typeAppareil`='" + typeAppareilObj.TypeAppareil + "'";
                this.strCommande += " WHERE `typeAppareil`='" + typeAppareil + "'";

                this.serviceConnectBase.openConnection();
                nbUpdate = this.serviceConnectBase.requete(this.strCommande);
                if (nbUpdate == 1) 
                {
                    isUpdate = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isUpdate;
        }

        string IntfDalUSTypeAppareil.isTypeAppareil(crlUSTypeAppareil typeAppareilObj, string typeAppareil)
        {
            #region declaration
            string typeAppareilStr = "";
            #endregion

            #region implementation
            if (typeAppareilObj != null && typeAppareil != "") 
            {
                this.strCommande = "SELECT * FROM `ustypeappareil` WHERE `typeAppareil`='" + typeAppareilObj.TypeAppareil + "' AND";
                this.strCommande += " `typeAppareil`<>'" + typeAppareil + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            typeAppareilStr = this.reader["typeAppareil"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return typeAppareilStr;
        }

        void IntfDalUSTypeAppareil.insertToGridTypeAppareil(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalUSTypeAppareil serviceUSTypeAppareil = new ImplDalUSTypeAppareil();
            #endregion

            #region implementation

            this.strCommande = "SELECT ustypeappareil.typeAppareil FROM ustypeappareil";
            this.strCommande += " WHERE " + paramLike + " LIKE  '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;


            gridView.DataSource = serviceUSTypeAppareil.getDataTableTypeAppareil(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalUSTypeAppareil.getDataTableTypeAppareil(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("typeAppareil", typeof(string));

            DataRow dr;
            #endregion

            this.serviceConnectBase.openConnection();
            this.reader = this.serviceConnectBase.select(strRqst);
            if (this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    while (this.reader.Read())
                    {
                        dr = dataTable.NewRow();

                        dr["typeAppareil"] = reader["typeAppareil"].ToString();

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }

        #endregion
    }
}
