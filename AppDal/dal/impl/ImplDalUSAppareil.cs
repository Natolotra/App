using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.dal.intf;
using MySql.Data.MySqlClient;
using arch.crl;
using System.Web.UI.WebControls;
using System.Data;


namespace arch.dal.impl
{
    /// <summary>
    /// Description résumée de ImplDalUSAppareil
    /// </summary>
    public class ImplDalUSAppareil : IntfDalUSAppareil
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalUSAppareil(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalUSAppareil()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion


        #region IntfDalUSAppareil Members

        string IntfDalUSAppareil.insertUSAppareil(crlUSAppareil appareil, string sigleAgence)
        {
            #region declaration
            string numAppareil = "";
            IntfDalUSAppareil serviceUSAppareil = new ImplDalUSAppareil();
            int nbInsert = 0;
            #endregion

            #region implementation
            if (appareil != null && sigleAgence != "") 
            {
                appareil.NumAppareil = serviceUSAppareil.getNumUSAppareil(sigleAgence);
                this.strCommande = "INSERT INTO `usappareil` (`numAppareil`,`typeAppareil`,";
                this.strCommande += " `numSerie`) VALUES";
                this.strCommande += " ('" + appareil.NumAppareil + "','" + appareil.TypeAppareil + "',";
                this.strCommande += " '" + appareil.NumSerie + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1) 
                {
                    numAppareil = appareil.NumAppareil;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numAppareil;
        }

        bool IntfDalUSAppareil.updateUSArret(crlUSAppareil appareil)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            #endregion

            #region implementation
            if (appareil != null) 
            {
                this.strCommande = "UPDATE `usappareil` SET `typeAppareil`='" + appareil.TypeAppareil + "',";
                this.strCommande += " `numSerie`='" + appareil.NumSerie + "'";
                this.strCommande += " WHERE `numAppareil`='" + appareil.NumAppareil + "'";

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

        string IntfDalUSAppareil.isUSAppareil(crlUSAppareil appareil)
        {
            #region declaration
            string numAppareil = "";
            #endregion

            #region implementation
            if (appareil != null) 
            {
                this.strCommande = "SELECT usappareil.numAppareil FROM `usappareil` WHERE";
                this.strCommande += " usappareil.numSerie = '" + appareil.NumSerie + "' AND";
                this.strCommande += " usappareil.numAppareil <> '" + appareil.NumAppareil + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            numAppareil = this.reader["numAppareil"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numAppareil;
        }

        crlUSAppareil IntfDalUSAppareil.selectUSAppareil(string numAppareil)
        {
            #region declaration
            crlUSAppareil appareil = null;
            #endregion

            #region implementation
            if (numAppareil != "") 
            {
                this.strCommande = "SELECT * FROM `usappareil` WHERE `numAppareil`='" + numAppareil + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            appareil = new crlUSAppareil();
                            appareil.NumAppareil = this.reader["numAppareil"].ToString();
                            appareil.NumSerie = this.reader["numSerie"].ToString();
                            appareil.TypeAppareil = this.reader["typeAppareil"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return appareil;
        }

        string IntfDalUSAppareil.getNumUSAppareil(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numAppareil = "00001";
            string[] tempNumAppareil = null;
            string strDate = "AP" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT usappareil.numAppareil AS maxNum FROM usappareil";
            this.strCommande += " WHERE usappareil.numAppareil LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumAppareil = reader["maxNum"].ToString().ToString().Split('/');
                        numAppareil = tempNumAppareil[tempNumAppareil.Length - 1];
                    }
                    numTemp = double.Parse(numAppareil) + 1;
                    if (numTemp < 10)
                        numAppareil = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numAppareil = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numAppareil = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numAppareil = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numAppareil = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numAppareil = strDate + "/" + sigleAgence + "/" + numAppareil;
            #endregion

            return numAppareil;
        }




        void IntfDalUSAppareil.insertToGridAppareil(GridView gridView, string param, string paramLike, string valueLike, string typeAppareil)
        {
            #region declaration
            IntfDalUSAppareil serviceUSAppareil = new ImplDalUSAppareil();
            #endregion

            #region implementation

            this.strCommande = "SELECT usappareil.numAppareil, usappareil.typeAppareil,";
            this.strCommande += " usappareil.numSerie FROM usappareil";
            this.strCommande += " WHERE usappareil.typeAppareil LIKE '%" + typeAppareil + "%' AND";
            this.strCommande += " " + paramLike + " LIKE  '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;


            gridView.DataSource = serviceUSAppareil.getDataTableAppareil(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalUSAppareil.getDataTableAppareil(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numAppareil", typeof(string));
            dataTable.Columns.Add("typeAppareil", typeof(string));
            dataTable.Columns.Add("numSerie", typeof(string));

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

                        dr["numAppareil"] = reader["numAppareil"].ToString();
                        dr["typeAppareil"] = reader["typeAppareil"].ToString();
                        dr["numSerie"] = reader["numSerie"].ToString();

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


        void IntfDalUSAppareil.insertToGridAppareilListeNoire(GridView gridView, string param, string paramLike, string valueLike, string typeAppareil)
        {
            #region declaration
            IntfDalUSAppareil serviceUSAppareil = new ImplDalUSAppareil();
            #endregion

            #region implementation

            this.strCommande = "SELECT usappareil.numAppareil, usappareil.typeAppareil,";
            this.strCommande += " usappareil.numSerie FROM usappareil";
            this.strCommande += " Inner Join observationmateriel ON observationmateriel.numAppareil = usappareil.numAppareil";
            this.strCommande += " WHERE observationmateriel.isListeNoire = '2' AND";
            this.strCommande += " usappareil.typeAppareil LIKE '%" + typeAppareil + "%' AND";
            this.strCommande += " " + paramLike + " LIKE  '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;


            gridView.DataSource = serviceUSAppareil.getDataTableAppareilListeNoire(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalUSAppareil.getDataTableAppareilListeNoire(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numAppareil", typeof(string));
            dataTable.Columns.Add("typeAppareil", typeof(string));
            dataTable.Columns.Add("numSerie", typeof(string));

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

                        dr["numAppareil"] = reader["numAppareil"].ToString();
                        dr["typeAppareil"] = reader["typeAppareil"].ToString();
                        dr["numSerie"] = reader["numSerie"].ToString();

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