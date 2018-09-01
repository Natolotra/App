using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.dal.intf;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;
using System.Data;
using arch.crl;

namespace arch.dal.impl
{
    /// <summary>
    /// Implementation pour le service type commission
    /// </summary>
    public class ImplDalTypeCommission : IntfDalTypeCommission
    {
        #region declaration
        IntfDalConnectBase serviceConnection = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalTypeCommission()
        {
            this.serviceRessource = new ImplDalServiceRessource();
            this.serviceConnection = new ImplDalConnectBase(this.serviceRessource.getDefaultStrConnection());
        }
        public ImplDalTypeCommission(string strConnection)
        {
            this.serviceConnection = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region methode
        void IntfDalTypeCommission.loadDddlTypeCommission(DropDownList ddl)
        {
            #region implementation
            ddl.Items.Clear();
            this.strCommande = "SELECT * FROM `typecommission`";
            this.serviceConnection.openConnection();
            reader = this.serviceConnection.select(this.strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ddl.Items.Add(reader["typeCommission"].ToString());
                    }
                }
                reader.Dispose();
            }
            this.serviceConnection.closeConnection();
            #endregion
        }

        crlTypeCommssion IntfDalTypeCommission.selectTypeCommission(string typeCommissionStr)
        {
            #region declaration
            crlTypeCommssion typeCommission = null;
            #endregion

            #region implementation
            if (typeCommissionStr != "")
            {
                this.strCommande = "SELECT * FROM `typecommission`";
                this.strCommande += " WHERE `typeCommission`='" + typeCommissionStr + "'";

                this.serviceConnection.openConnection();
                this.reader = this.serviceConnection.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        typeCommission = new crlTypeCommssion();
                        if (this.reader.Read())
                        {
                            typeCommission.TypeCommission = this.reader["typeCommission"].ToString();
                            typeCommission.CommentaireTypeCommission = this.reader["commentaireTypeCommission"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnection.closeConnection();
            }
            #endregion

            return typeCommission;
        }

        string IntfDalTypeCommission.insertTypeCommission(crlTypeCommssion typeCommission)
        {
            #region declaration
            string typeCommissionStr = "";
            int nbInsert = 0;
            #endregion

            #region implementation
            if (typeCommission != null)
            {
                this.strCommande = "INSERT INTO `typecommission` (`typeCommission`,`commentaireTypeCommission`)";
                this.strCommande += " VALUES ('" + typeCommission.TypeCommission + "','" + typeCommission.CommentaireTypeCommission + "')";

                this.serviceConnection.openConnection();
                nbInsert = this.serviceConnection.requete(this.strCommande);
                if (nbInsert == 1)
                    typeCommissionStr = typeCommission.TypeCommission;
                this.serviceConnection.closeConnection();
            }
            #endregion

            return typeCommissionStr;
        }

        bool IntfDalTypeCommission.updateTypeCommission(crlTypeCommssion typeCommission, string typeCommissionStr)
        {
            #region declaration
            bool isUpDate = false;
            int nbUpdate = 0;
            #endregion

            #region implementation
            if (typeCommission != null)
            {
                this.strCommande = "UPDATE `typecommission` SET `typeCommission`='" + typeCommission.TypeCommission + "',";
                this.strCommande += " `commentaireTypeCommission`='" + typeCommission.CommentaireTypeCommission + "'";
                this.strCommande += " WHERE `typeCommission`='" + typeCommissionStr + "'";

                this.serviceConnection.openConnection();
                nbUpdate = this.serviceConnection.requete(this.strCommande);
                if (nbUpdate == 1)
                    isUpDate = true;
                this.serviceConnection.closeConnection();
            }
            #endregion

            return isUpDate;
        }

        string IntfDalTypeCommission.isTypeCommission(crlTypeCommssion typeCommission, string typeCommissionStr)
        {
            #region declaration
            string typeComimssionStr = "";
            #endregion

            #region implementation
            if (typeCommission != null)
            {
                this.strCommande = "SELECT * FROM `typecommission`";
                this.strCommande += " WHERE `typeCommission`<>'" + typeCommissionStr + "' AND";
                this.strCommande += " `typeCommission`='" + typeCommission.TypeCommission + "'";

                this.serviceConnection.openConnection();
                this.reader = this.serviceConnection.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            typeComimssionStr = this.reader["typeCommission"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnection.closeConnection();
            }
            #endregion

            return typeComimssionStr;
        }
        #endregion

        #region insert to grid
        void IntfDalTypeCommission.insertToGridTypeCommission(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalTypeCommission serviceTypeCommission = new ImplDalTypeCommission();
            #endregion

            #region implementation

            this.strCommande = "SELECT typecommission.typeCommission, typecommission.commentaireTypeCommission";
            this.strCommande += " FROM typecommission";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceTypeCommission.getDataTableTypeCommission(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalTypeCommission.getDataTableTypeCommission(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("typeCommission", typeof(string));
            dataTable.Columns.Add("commentaireTypeCommission", typeof(string));
            DataRow dr;
            #endregion

            this.serviceConnection.openConnection();
            this.reader = this.serviceConnection.select(strRqst);
            if (this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    while (this.reader.Read())
                    {
                        dr = dataTable.NewRow();

                        dr["typeCommission"] = reader["typeCommission"].ToString();
                        dr["commentaireTypeCommission"] = reader["commentaireTypeCommission"].ToString();

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnection.closeConnection();

            #endregion

            return dataTable;
        }
        #endregion

    }
}