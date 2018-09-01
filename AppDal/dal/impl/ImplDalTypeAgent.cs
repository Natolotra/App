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
    /// implementation pour le service type agent
    /// </summary>
    public class ImplDalTypeAgent : IntfDalTypeAgent
    {
        #region declaration
        IntfDalConnectBase serviceConnection = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalTypeAgent()
        {
            this.serviceRessource = new ImplDalServiceRessource();
            this.serviceConnection = new ImplDalConnectBase(this.serviceRessource.getDefaultStrConnection());
        }
        public ImplDalTypeAgent(string strConnection)
        {
            this.serviceConnection = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region IntfDalTypeAgent Members

        void IntfDalTypeAgent.loadDddlTypeAgent(DropDownList ddl)
        {
            #region implementation
            ddl.Items.Clear();
            this.strCommande = "SELECT * FROM `typeagent`";
            this.serviceConnection.openConnection();
            reader = this.serviceConnection.select(this.strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    ddl.Items.Add("");
                    while(reader.Read())
                    {
                        ddl.Items.Add(reader["typeAgent"].ToString());
                    }
                }
                reader.Dispose();
            }
            this.serviceConnection.closeConnection();
            #endregion
        }

        crlTypeAgent IntfDalTypeAgent.selectTypeAgent(string typeAgent)
        {
            #region declaration
            crlTypeAgent objTypeAgent = null;
            #endregion

            #region implementation
            if (typeAgent != "")
            {
                this.strCommande = "SELECT * FROM `typeagent` WHERE `typeAgent`='" + typeAgent + "'";

                this.serviceConnection.openConnection();
                this.reader = this.serviceConnection.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            objTypeAgent = new crlTypeAgent();
                            objTypeAgent.CommentaireTypeAgent = this.reader["commentaireTypeAgent"].ToString();
                            objTypeAgent.typeAgent = this.reader["typeAgent"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnection.closeConnection();
            }
            #endregion

            return objTypeAgent;
        }


        bool IntfDalTypeAgent.insertTypeAgent(crlTypeAgent typeAgent)
        {
            #region declaration
            bool isInsert = false;
            int nbInsert = 0;
            #endregion

            #region implementation
            if(typeAgent != null)
            {
                this.strCommande = "INSERT INTO `typeagent` (`typeAgent`,`commentaireTypeAgent`)";
                this.strCommande += " VALUES ('" + typeAgent.typeAgent + "','" + typeAgent.CommentaireTypeAgent + "')";

                this.serviceConnection.openConnection();
                nbInsert = this.serviceConnection.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    isInsert = true;
                }
                this.serviceConnection.closeConnection();
            }
            #endregion

            return isInsert;
        }

        bool IntfDalTypeAgent.updateTypeAgent(crlTypeAgent typeAgent, string typeAgentStr)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            #endregion

            #region implementation
            if (typeAgent != null)
            {
                if (typeAgentStr != "")
                {
                    this.strCommande = "UPDATE `typeagent` SET `typeAgent`='" + typeAgent.typeAgent + "', ";
                    this.strCommande += " `commentaireTypeAgent`='" + typeAgent.CommentaireTypeAgent + "'";
                    this.strCommande += " WHERE (`typeAgent`='" + typeAgentStr + "')";

                    this.serviceConnection.openConnection();
                    nombreUpdate = this.serviceConnection.requete(this.strCommande);
                    if (nombreUpdate == 1)
                        isUpdate = true;
                    this.serviceConnection.closeConnection();
                }
            }
            #endregion

            return isUpdate;
        }

        string IntfDalTypeAgent.isTypeAgent(crlTypeAgent typeAgent, string typeAgentStr)
        {
            #region declaration
            string strTypeAgent = "";
            #endregion

            #region implementation
            if (typeAgent != null)
            {
                this.strCommande = "SELECT * FROM `typeagent` WHERE ";
                this.strCommande += " `typeAgent`='" + typeAgent.typeAgent + "' AND";
                this.strCommande += " `typeAgent`<>'" + typeAgentStr + "'";

                this.serviceConnection.openConnection();
                this.reader = this.serviceConnection.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            strTypeAgent = this.reader["typeAgent"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnection.closeConnection();
            }
            #endregion

            return strTypeAgent;
        }
        #endregion


        void IntfDalTypeAgent.insertToGridTypeAgent(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalTypeAgent serviceTypeAgent = new ImplDalTypeAgent();
            #endregion

            #region implementation
            this.strCommande = "SELECT typeagent.typeAgent, typeagent.commentaireTypeAgent";
            this.strCommande += " FROM typeagent";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param + " ASC";

            gridView.DataSource = serviceTypeAgent.getDataTableTypeAgent(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalTypeAgent.getDataTableTypeAgent(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implemntation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("typeAgent", typeof(string));
            dataTable.Columns.Add("commentaireTypeAgent", typeof(string));
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


                        dr["typeAgent"] = this.reader["typeAgent"].ToString();
                        dr["commentaireTypeAgent"] = this.reader["commentaireTypeAgent"].ToString();

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }

            this.serviceConnection.closeConnection();


            #endregion

            return dataTable;
        }


        
    }
}
