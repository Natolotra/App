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
    /// Implementation du service axe
    /// </summary>
    public class ImplDalUSAxe : IntfDalUSAxe
    {
        
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalUSAxe(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalUSAxe()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion

        #region IntfDalUSAxe Members

        string IntfDalUSAxe.insertUSAxe(crlUSAxe axe, string sigleAgence)
        {
            #region declaration
            string numAxe = "";
            IntfDalUSAxe serviceUSAxe = new ImplDalUSAxe();
            int nbInsert = 0;
            #endregion

            #region implemenation
            if (axe != null && sigleAgence != "")
            {
                axe.NumAxe = serviceUSAxe.getNumUSAxe(sigleAgence);
                this.strCommande = "INSERT INTO `usaxe` (`numAxe`,`nomAxe`,`descriptionAxe`)";
                this.strCommande += " VALUES ('" + axe.NumAxe + "','" + axe.NomAxe + "',";
                this.strCommande += " '" + axe.DescriptionAxe + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    numAxe = axe.NumAxe;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numAxe;
        }

        bool IntfDalUSAxe.updateUSAxe(crlUSAxe axe)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            #endregion

            #region implementation
            if (axe != null)
            {
                this.strCommande = "UPDATE `usaxe` SET `nomAxe`='" + axe.NomAxe + "',";
                this.strCommande += " `descriptionAxe`='" + axe.DescriptionAxe + "' WHERE";
                this.strCommande += " `numAxe`='" + axe.NumAxe + "'";

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

        crlUSAxe IntfDalUSAxe.selectUSAxe(string numAxe)
        {
            #region declaration
            crlUSAxe axe = null;
            #endregion

            #region implementation
            if (numAxe != "")
            {
                this.strCommande = "SELECT * FROM `usaxe` WHERE (`numAxe`='" + numAxe + "')";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            axe = new crlUSAxe();
                            axe.NumAxe = this.reader["numAxe"].ToString();
                            axe.NomAxe = this.reader["nomAxe"].ToString();
                            axe.DescriptionAxe = this.reader["descriptionAxe"].ToString();
                           
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return axe;
        }

        crlUSAxe IntfDalUSAxe.selectUSAxeLieu(string numLieu)
        {
            #region declaration
            crlUSAxe axe = null;
            #endregion

            #region implementation
            if (numLieu != "")
            {
                this.strCommande = "SELECT usaxe.numAxe, usaxe.nomAxe, usaxe.descriptionAxe FROM `usaxe`";
                this.strCommande += " Inner Join usassocaxelieu ON usassocaxelieu.numAxe = usaxe.numAxe";
                this.strCommande += " WHERE (usassocaxelieu.numLieu='" + numLieu + "')";
                this.strCommande += " ORDER BY usaxe.numAxe DESC";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            axe = new crlUSAxe();
                            axe.NumAxe = this.reader["numAxe"].ToString();
                            axe.NomAxe = this.reader["nomAxe"].ToString();
                            axe.DescriptionAxe = this.reader["descriptionAxe"].ToString();

                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return axe;
        }

        string IntfDalUSAxe.isUSAxe(crlUSAxe axe)
        {
            #region declaration
            string numAxe = "";
            #endregion

            #region implementation
            if (axe != null)
            {
                this.strCommande = "SELECT * FROM `usaxe` WHERE (`numAxe`<>'" + axe.NumAxe + "' AND `nomAxe`='" + axe.NomAxe + "')";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            numAxe = this.reader["numAxe"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numAxe;
        }

        string IntfDalUSAxe.getNumUSAxe(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numAxe = "00001";
            string[] tempNumAxe = null;
            string strDate = "AX" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT usaxe.numAxe AS maxNum FROM usaxe";
            this.strCommande += " WHERE usaxe.numAxe LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumAxe = reader["maxNum"].ToString().ToString().Split('/');
                        numAxe = tempNumAxe[tempNumAxe.Length - 1];
                    }
                    numTemp = double.Parse(numAxe) + 1;
                    if (numTemp < 10)
                        numAxe = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numAxe = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numAxe = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numAxe = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numAxe = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numAxe = strDate + "/" + sigleAgence + "/" + numAxe;
            #endregion

            return numAxe;
        }

        

        bool IntfDalUSAxe.insertUSAssocAxeLieu(string numAxe, string numLieu)
        {
            #region declaration
            bool isInsert = false;
            int nbInsert = 0;
            #endregion

            #region implementation
            if (numLieu != "" && numAxe != "")
            {
                this.strCommande = "INSERT INTO `usassocaxelieu` (`numLieu`,`numAxe`)";
                this.strCommande += " VALUES ('" + numLieu + "','" + numAxe + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    isInsert = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isInsert;
        }

        bool IntfDalUSAxe.deleteUSAssocAxeLieu(string numAxe, string numLieu)
        {
            #region declaration
            bool isDelete = false;
            int nbDelete = 0;
            #endregion

            #region implementation
            if (numLieu != "" && numAxe != "")
            {
                this.strCommande = "DELETE FROM `usassocaxelieu` WHERE";
                this.strCommande += " `numLieu`='" + numLieu + "' AND `numAxe`='" + numAxe + "'";

                this.serviceConnectBase.openConnection();
                nbDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nbDelete == 1)
                {
                    isDelete = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isDelete;
        }


        void IntfDalUSAxe.loadDdlUSAxe(DropDownList ddl)
        {
            #region implementation
            if (ddl != null)
            {
                ddl.Items.Clear();
                ddl.Items.Add("");
                this.strCommande = "SELECT * FROM `usaxe`";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            ddl.Items.Add(new ListItem(this.reader["nomAxe"].ToString(), this.reader["numAxe"].ToString()));
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }
        #endregion



        void IntfDalUSAxe.insertToGridAxe(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalUSAxe serviceUSAxe = new ImplDalUSAxe();
            #endregion

            #region implementation

            this.strCommande = "SELECT usaxe.numAxe, usaxe.nomAxe, usaxe.descriptionAxe FROM usaxe";
            this.strCommande += " WHERE " + paramLike + " LIKE  '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;


            gridView.DataSource = serviceUSAxe.getDataTableAxe(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalUSAxe.getDataTableAxe(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numAxe", typeof(string));
            dataTable.Columns.Add("nomAxe", typeof(string));
            dataTable.Columns.Add("descriptionAxe", typeof(string));

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

                        dr["numAxe"] = reader["numAxe"].ToString();
                        dr["nomAxe"] = reader["nomAxe"].ToString();
                        dr["descriptionAxe"] = reader["descriptionAxe"].ToString();

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }

    }
}
