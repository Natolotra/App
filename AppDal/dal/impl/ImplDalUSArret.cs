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
    /// Implementation du service Arret
    /// </summary>
    public class ImplDalUSArret : IntfDalUSArret
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalUSArret(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalUSArret()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion

        #region IntfDalUSArret Members

        string IntfDalUSArret.insertUSArret(crlUSArret arret, string sigleAgence)
        {
            #region declaration
            string numArret = "";
            IntfDalUSArret serviceUSArret = new ImplDalUSArret();
            int nbInsert = 0;
            #endregion

            #region implemenation
            if (arret != null && sigleAgence != "")
            {
                arret.NumArret = serviceUSArret.getNumUSArret(sigleAgence);
                this.strCommande = "INSERT INTO `usarret` (`numArret`,`numLieu`,`nomArret`,`descriptionArret`)";
                this.strCommande += " VALUES ('" + arret.NumArret + "','" + arret.NumLieu + "','" + arret.NomArret + "',";
                this.strCommande += " '" + arret.DescriptionArret + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    numArret = arret.NomArret;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numArret;
        }

        bool IntfDalUSArret.updateUSArret(crlUSArret arret)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            #endregion

            #region implementation
            if (arret != null)
            {
                this.strCommande = "UPDATE `usarret` SET `numLieu`='" + arret.NumLieu + "',";
                this.strCommande += " `nomArret`='" + arret.NomArret + "',";
                this.strCommande += " `descriptionArret`='" + arret.DescriptionArret + "' WHERE";
                this.strCommande += " `numArret`='" + arret.NumArret + "'";

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

        string IntfDalUSArret.isUSArret(crlUSArret arret)
        {
            #region declaration
            string numArret = "";
            #endregion

            #region implementation
            if (arret != null)
            {
                this.strCommande = "SELECT * FROM `usarret` WHERE";
                this.strCommande += " `nomArret`='" + arret.NomArret + "' AND";
                this.strCommande += " `numLieu`='" + arret.NumLieu + "' AND";
                this.strCommande += " `numArret`<>'" + arret.NumArret + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            numArret = this.reader["numArret"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numArret;
        }

        crlUSArret IntfDalUSArret.selectUSArret(string numArret)
        {
            #region declaration
            crlUSArret arret = null;

            #endregion

            #region implementation
            if (numArret != "")
            {
                this.strCommande = "SELECT * FROM `usarret` WHERE (`numArret`='" + numArret + "')";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            arret = new crlUSArret();
                            arret.DescriptionArret = this.reader["descriptionArret"].ToString();
                            arret.NomArret = this.reader["nomArret"].ToString();
                            arret.NumLieu = this.reader["numLieu"].ToString();
                            arret.NumArret = this.reader["numArret"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return arret;
        }

        string IntfDalUSArret.getNumUSArret(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numArret = "00001";
            string[] tempNumArret = null;
            string strDate = "AR" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT usarret.numArret AS maxNum FROM usarret";
            this.strCommande += " WHERE usarret.numArret LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumArret = reader["maxNum"].ToString().ToString().Split('/');
                        numArret = tempNumArret[tempNumArret.Length - 1];
                    }
                    numTemp = double.Parse(numArret) + 1;
                    if (numTemp < 10)
                        numArret = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numArret = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numArret = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numArret = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numArret = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numArret = strDate + "/" + sigleAgence + "/" + numArret;
            #endregion

            return numArret;
        }

        void IntfDalUSArret.loadDdlArret(DropDownList ddl, string numArret)
        {
            #region implementation
            if (ddl != null)
            {
                ddl.Items.Clear();
                ddl.Items.Add("");

                this.strCommande = "SELECT usarret.numArret, usarret.nomArret, uslieu.nomLieu";
                this.strCommande += " FROM usarret";
                this.strCommande += " Inner Join uslieu ON uslieu.numLieu = usarret.numLieu";
                this.strCommande += " WHERE usarret.numArret <> '" + numArret + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            ddl.Items.Add(new ListItem(this.reader["nomLieu"].ToString() + " / " + this.reader["nomArret"].ToString(), this.reader["numArret"].ToString()));
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }

        void IntfDalUSArret.loadDdlArretVoyage(DropDownList ddl, string numVoyage)
        {
            #region implementation
            if (ddl != null)
            {
                ddl.Items.Clear();
                ddl.Items.Add("");

                this.strCommande = "SELECT usarret.numArret, uslieu.nomLieu,";
                this.strCommande += " usarret.nomArret FROM usvoyage";
                this.strCommande += " Inner Join usligne ON usligne.numLigne = usvoyage.numLigne";
                this.strCommande += " Inner Join usassoclignearret ON usassoclignearret.numLigne = usligne.numLigne";
                this.strCommande += " Inner Join usarret ON usarret.numArret = usassoclignearret.numArret";
                this.strCommande += " Inner Join uslieu ON uslieu.numLieu = usarret.numLieu";
                this.strCommande += " WHERE usvoyage.numVoyage = '" + numVoyage + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            ddl.Items.Add(new ListItem(this.reader["nomLieu"].ToString() + " / " + this.reader["nomArret"].ToString(), this.reader["numArret"].ToString()));
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }
        #endregion

        void IntfDalUSArret.insertToGridArret(GridView gridView, string param, string paramLike, string valueLike, string numLieu)
        {
            #region declaration
            IntfDalUSArret serviceUSArret = new ImplDalUSArret();
            #endregion

            #region implementation

            this.strCommande = "SELECT usarret.numArret, usarret.numLieu, usarret.nomArret,";
            this.strCommande += " usarret.descriptionArret, uslieu.nomLieu, uszone.nomZone";
            this.strCommande += " FROM usarret";
            this.strCommande += " Inner Join uslieu ON uslieu.numLieu = usarret.numLieu";
            this.strCommande += " Inner Join uszone ON uszone.numZone = uslieu.numZone";
            this.strCommande += " WHERE " + paramLike + " LIKE  '%" + valueLike + "%' AND";
            this.strCommande += " usarret.numLieu LIKE '%" + numLieu + "%'";
            this.strCommande += " ORDER BY " + param;


            gridView.DataSource = serviceUSArret.getDataTableArret(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalUSArret.getDataTableArret(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numArret", typeof(string));
            dataTable.Columns.Add("nomArret", typeof(string));
            dataTable.Columns.Add("nomLieu", typeof(string));
            dataTable.Columns.Add("nomZone", typeof(string));
            dataTable.Columns.Add("descriptionArret", typeof(string));

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

                        dr["numArret"] = reader["numArret"].ToString();
                        dr["nomArret"] = reader["nomArret"].ToString();
                        dr["nomLieu"] = reader["nomLieu"].ToString();
                        dr["nomZone"] = reader["nomZone"].ToString();
                        dr["descriptionArret"] = reader["descriptionArret"].ToString();

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }

        void IntfDalUSArret.insertToGridArretLigne(GridView gridView, string param, string paramLike, string valueLike, string numLigne)
        {
            #region declaration
            IntfDalUSArret serviceUSArret = new ImplDalUSArret();
            #endregion

            #region implementation
            this.strCommande = "SELECT usarret.numArret, usarret.numLieu, usarret.nomArret,";
            this.strCommande += " usarret.descriptionArret, uslieu.nomLieu,";
            this.strCommande += " uszone.nomZone FROM usarret";
            this.strCommande += " Inner Join uslieu ON uslieu.numLieu = usarret.numLieu";
            this.strCommande += " Inner Join uszone ON uszone.numZone = uslieu.numZone";
            this.strCommande += " Inner Join usassoclignearret ON usassoclignearret.numArret = usarret.numArret";
            this.strCommande += " WHERE usassoclignearret.numLigne = '" + numLigne + "' AND";
            this.strCommande += " " + paramLike + " LIKE  '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;


            gridView.DataSource = serviceUSArret.getDataTableArretLigne(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalUSArret.getDataTableArretLigne(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numArret", typeof(string));
            dataTable.Columns.Add("nomArret", typeof(string));
            dataTable.Columns.Add("nomLieu", typeof(string));
            dataTable.Columns.Add("nomZone", typeof(string));
            dataTable.Columns.Add("descriptionArret", typeof(string));

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

                        dr["numArret"] = reader["numArret"].ToString();
                        dr["nomArret"] = reader["nomArret"].ToString();
                        dr["nomLieu"] = reader["nomLieu"].ToString();
                        dr["nomZone"] = reader["nomZone"].ToString();
                        dr["descriptionArret"] = reader["descriptionArret"].ToString();

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }

        void IntfDalUSArret.insertToGridArretNonLigne(GridView gridView, string param, string paramLike, string valueLike, string numLigne)
        {
            #region declaration
            IntfDalUSArret serviceUSArret = new ImplDalUSArret();
            #endregion

            #region implementation

            this.strCommande = "SELECT usarret.numArret, usarret.numLieu, usarret.nomArret,";
            this.strCommande += " usarret.descriptionArret, uslieu.nomLieu,";
            this.strCommande += " uszone.nomZone FROM usarret";
            this.strCommande += " Inner Join uslieu ON uslieu.numLieu = usarret.numLieu";
            this.strCommande += " Inner Join uszone ON uszone.numZone = uslieu.numZone";
            this.strCommande += " Left Join usassoclignearret ON usassoclignearret.numArret = usarret.numArret";
            this.strCommande += " WHERE (usassoclignearret.numLigne IS NULL OR";
            this.strCommande += " usassoclignearret.numLigne <> '" + numLigne + "') AND";
            this.strCommande += " " + paramLike + " LIKE  '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;


            gridView.DataSource = serviceUSArret.getDataTableArretNonLigne(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalUSArret.getDataTableArretNonLigne(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numArret", typeof(string));
            dataTable.Columns.Add("nomArret", typeof(string));
            dataTable.Columns.Add("nomLieu", typeof(string));
            dataTable.Columns.Add("nomZone", typeof(string));
            dataTable.Columns.Add("descriptionArret", typeof(string));

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

                        dr["numArret"] = reader["numArret"].ToString();
                        dr["nomArret"] = reader["nomArret"].ToString();
                        dr["nomLieu"] = reader["nomLieu"].ToString();
                        dr["nomZone"] = reader["nomZone"].ToString();
                        dr["descriptionArret"] = reader["descriptionArret"].ToString();

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
