using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.dal.intf;
using arch.crl;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;
using System.Data;

namespace arch.dal.impl
{
    /// <summary>
    /// Implementation du service USLieu
    /// </summary>
    public class ImplDalUSLieu : IntfDalUSLieu
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalUSLieu(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalUSLieu()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion

        #region IntfDalUSLieu Members

        string IntfDalUSLieu.insertUSLieu(crlUSLieu lieu, string sigleAgence)
        {
            #region declaration
            string numLieu = "";
            IntfDalUSLieu serviceUSLieu = new ImplDalUSLieu();
            int nbInsert = 0;
            #endregion

            #region implemenation
            if (lieu != null && sigleAgence != "") 
            {
                
                lieu.NumLieu = serviceUSLieu.getNumUSLieu(sigleAgence);
                this.strCommande = "INSERT INTO `uslieu` (`numLieu`,`descriptionLieu`,`numZone`,`numQuartier`)";
                this.strCommande += " VALUES ('" + lieu.NumLieu + "','" + lieu.DescriptionLieu + "',";
                this.strCommande += " '" + lieu.NumZone + "','" + lieu.NumQuartier + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1) 
                {
                    numLieu = lieu.NumLieu;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numLieu;
        }

        bool IntfDalUSLieu.updateUSLieu(crlUSLieu lieu)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            #endregion

            #region implementation
            if (lieu != null) 
            {
                
                this.strCommande = "UPDATE `uslieu` SET";
                this.strCommande += " `descriptionLieu`='" + lieu.DescriptionLieu + "',";
                this.strCommande += " `numZone`='" + lieu.NumZone + "',";
                this.strCommande += " `numQuartier`='" + lieu.NumQuartier + "' WHERE";
                this.strCommande += " `numLieu`='" + lieu.NumLieu + "'";

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

        string IntfDalUSLieu.isUSLieu(crlUSLieu lieu)
        {
            #region declaration
            string numLieu = "";
            string numCommune = "";
            IntfDalUSZone serviceUSZone = new ImplDalUSZone();
            #endregion

            #region implementation
            if (lieu != null) 
            {
                numCommune = serviceUSZone.getNumCommune(lieu.NumZone);

                this.strCommande = "SELECT * FROM `uslieu`";
                this.strCommande += " Inner Join uszone ON uszone.numZone = uslieu.numZone WHERE";
                this.strCommande += " uszone.numCommune = '" + numCommune + "' AND";
                this.strCommande += " `numQuartier`='" + lieu.NumQuartier + "' AND";
                this.strCommande += " `numLieu`<>'" + lieu.NumLieu + "'";
                
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            numLieu = this.reader["numLieu"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numLieu;
        }

        string IntfDalUSLieu.getNumUSLieu(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numLieu = "00001";
            string[] tempNumLieu = null;
            string strDate = "LU" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT uslieu.numLieu AS maxNum FROM uslieu";
            this.strCommande += " WHERE uslieu.numLieu LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumLieu = reader["maxNum"].ToString().ToString().Split('/');
                        numLieu = tempNumLieu[tempNumLieu.Length - 1];
                    }
                    numTemp = double.Parse(numLieu) + 1;
                    if (numTemp < 10)
                        numLieu = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numLieu = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numLieu = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numLieu = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numLieu = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numLieu = strDate + "/" + sigleAgence + "/" + numLieu;
            #endregion

            return numLieu;
        }

        crlUSLieu IntfDalUSLieu.selectUSLieu(string numLieu)
        {
            #region declaration
            crlUSLieu lieu = null;

            #endregion

            #region implementation
            if (numLieu != "") 
            {
                this.strCommande = "SELECT * FROM `uslieu` WHERE (`numLieu`='" + numLieu + "')";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            lieu = new crlUSLieu();
                            lieu.DescriptionLieu = this.reader["descriptionLieu"].ToString();
                            lieu.NumLieu = this.reader["numLieu"].ToString();
                            lieu.NumZone = this.reader["numZone"].ToString();
                            lieu.NumQuartier = this.reader["numQuartier"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return lieu;
        }

        void IntfDalUSLieu.loadDdlUSLieu(DropDownList ddl)
        {
            #region implementation
            if (ddl != null)
            {
                ddl.Items.Clear();
                ddl.Items.Add("");

                this.strCommande = "SELECT quartier.quartier, uslieu.numLieu FROM uslieu";
                this.strCommande += " Inner Join quartier ON quartier.numQuartier = uslieu.numQuartier";
                this.strCommande += " ORDER BY quartier.quartier ASC";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            ddl.Items.Add(new ListItem(this.reader["quartier"].ToString(), this.reader["numLieu"].ToString()));
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }

        void IntfDalUSLieu.loadDdlUSLieu(DropDownList ddl, string numLieu)
        {
            #region declaration
            string[] numLignes;
            IntfDalUSLieu serviceUSLieu = new ImplDalUSLieu();
            string strWhere = "";
            #endregion

            #region implementation
            ddl.Items.Clear();
            ddl.Items.Add("");
            if (ddl != null && numLieu != "") 
            {

                numLignes = serviceUSLieu.getNumLignes(numLieu).Split(';');

                if (numLignes.Length > 0) 
                {
                    strWhere = " AND (";
                    for (int i = 0; i < numLignes.Length; i++) 
                    {
                        if (i == 0)
                        {
                            strWhere += "usassoclignearret.numLigne = '" + numLignes[i] + "'"; 
                        }
                        else 
                        {
                            strWhere += " OR usassoclignearret.numLigne = '" + numLignes[i] + "'"; 
                        }
                    }
                    strWhere += ")";
                }

                this.strCommande = "SELECT quartier.quartier, uslieu.numLieu FROM uslieu";
                this.strCommande += " Inner Join quartier ON quartier.numQuartier = uslieu.numQuartier";
                this.strCommande += " Inner Join usarret ON usarret.numLieu = uslieu.numLieu";
                this.strCommande += " Inner Join usassoclignearret ON usassoclignearret.numArret = usarret.numArret";
                this.strCommande += " WHERE uslieu.numLieu <> '" + numLieu + "'" + strWhere;
                this.strCommande += " GROUP BY uslieu.numLieu ORDER BY quartier.quartier ASC";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        while (this.reader.Read()) 
                        {
                            ddl.Items.Add(new ListItem(this.reader["quartier"].ToString(), this.reader["numLieu"].ToString()));
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }

        string IntfDalUSLieu.getNumLignes(string numLieu)
        {
            #region declaration
            string numLignes = "";
            bool isInitialise = true;
            #endregion

            #region implementation
            if (numLieu != "") 
            {
                this.strCommande = "SELECT usassoclignearret.numLigne FROM usassoclignearret";
                this.strCommande += " Inner Join usarret ON usarret.numArret = usassoclignearret.numArret";
                this.strCommande += " Inner Join uslieu ON uslieu.numLieu = usarret.numLieu";
                this.strCommande += " WHERE uslieu.numLieu = '" + numLieu + "'";
                this.strCommande += " GROUP BY usassoclignearret.numLigne";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            if (isInitialise)
                            {
                                numLignes = this.reader["numLigne"].ToString();
                                isInitialise = false;
                            }
                            else 
                            {
                                numLignes = ";" + this.reader["numLigne"].ToString();
                            }
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numLignes;
        }
        #endregion


        #region insert to grid
        void IntfDalUSLieu.insertToGridLieu(GridView gridView, string param, string paramLike, string valueLike, string numZone)
        {
            #region declaration
            IntfDalUSLieu serviceUSLieu = new ImplDalUSLieu();
            #endregion

            #region implementation

            this.strCommande = "SELECT uslieu.numLieu, uslieu.descriptionLieu, uslieu.numZone,";
            this.strCommande += " uslieu.numQuartier, uszone.numZone, uszone.nomZone,";
            this.strCommande += " uszone.niveau, uszone.numCommune, quartier.numQuartier,";
            this.strCommande += " quartier.quartier, quartier.numCommune,";
            this.strCommande += " quartier.numArrondissement FROM uslieu";
            this.strCommande += " Inner Join uszone ON uszone.numZone = uslieu.numZone";
            this.strCommande += " Inner Join quartier ON quartier.numQuartier = uslieu.numQuartier";
            this.strCommande += " WHERE " + paramLike + " LIKE  '%" + valueLike + "%' AND";
            this.strCommande += " uslieu.numZone LIKE '%" + numZone + "%'";
            this.strCommande += " ORDER BY " + param;


            gridView.DataSource = serviceUSLieu.getDataTableLieu(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalUSLieu.getDataTableLieu(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numLieu", typeof(string));
            dataTable.Columns.Add("nomLieu", typeof(string));
            dataTable.Columns.Add("descriptionLieu", typeof(string));
            dataTable.Columns.Add("nomZone", typeof(string));

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

                        dr["numLieu"] = reader["numLieu"].ToString();
                        dr["nomLieu"] = reader["quartier"].ToString();
                        dr["descriptionLieu"] = reader["descriptionLieu"].ToString();
                        dr["nomZone"] = reader["nomZone"].ToString();

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



        void IntfDalUSLieu.insertToGridLieuAxe(GridView gridView, string param, string paramLike, string valueLike, string numAxe)
        {
            #region declaration
            IntfDalUSLieu serviceUSLieu = new ImplDalUSLieu();
            #endregion

            #region implementation

            this.strCommande = "SELECT uslieu.numLieu, quartier.quartier, uslieu.descriptionLieu,";
            this.strCommande += " uslieu.numZone, uszone.nomZone FROM uslieu";
            this.strCommande += " Inner Join usassocaxelieu ON usassocaxelieu.numLieu = uslieu.numLieu";
            this.strCommande += " Inner Join uszone ON uszone.numZone = uslieu.numZone";
            this.strCommande += " Inner Join quartier ON quartier.numQuartier = uslieu.numQuartier";
            this.strCommande += " WHERE usassocaxelieu.numAxe = '" + numAxe + "' AND";
            this.strCommande += " " + paramLike + " LIKE  '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;


            gridView.DataSource = serviceUSLieu.getDataTableLieuAxe(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalUSLieu.getDataTableLieuAxe(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numLieu", typeof(string));
            dataTable.Columns.Add("nomLieu", typeof(string));
            dataTable.Columns.Add("descriptionLieu", typeof(string));
            dataTable.Columns.Add("nomZone", typeof(string));

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

                        dr["numLieu"] = reader["numLieu"].ToString();
                        dr["nomLieu"] = reader["quartier"].ToString();
                        dr["descriptionLieu"] = reader["descriptionLieu"].ToString();
                        dr["nomZone"] = reader["nomZone"].ToString();

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }

        void IntfDalUSLieu.insertToGridLieuNonAxe(GridView gridView, string param, string paramLike, string valueLike, string numAxe)
        {
            #region declaration
            IntfDalUSLieu serviceUSLieu = new ImplDalUSLieu();
            #endregion

            #region implementation

            this.strCommande = "SELECT uslieu.numLieu, quartier.quartier, uslieu.descriptionLieu,";
            this.strCommande += " uslieu.numZone, uszone.nomZone FROM uslieu";
            this.strCommande += " Left Join usassocaxelieu ON usassocaxelieu.numLieu = uslieu.numLieu";
            this.strCommande += " Inner Join uszone ON uszone.numZone = uslieu.numZone";
            this.strCommande += " Inner Join quartier ON quartier.numQuartier = uslieu.numQuartier";
            this.strCommande += " WHERE (usassocaxelieu.numAxe <> '" + numAxe + "' OR";
            this.strCommande += " usassocaxelieu.numAxe IS NULL) AND";
            this.strCommande += " " + paramLike + " LIKE  '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;


            gridView.DataSource = serviceUSLieu.getDataTableLieuAxe(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalUSLieu.getDataTableLieuNonAxe(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numLieu", typeof(string));
            dataTable.Columns.Add("nomLieu", typeof(string));
            dataTable.Columns.Add("descriptionLieu", typeof(string));
            dataTable.Columns.Add("nomZone", typeof(string));

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

                        dr["numLieu"] = reader["numLieu"].ToString();
                        dr["nomLieu"] = reader["quartier"].ToString();
                        dr["descriptionLieu"] = reader["descriptionLieu"].ToString();
                        dr["nomZone"] = reader["nomZone"].ToString();

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