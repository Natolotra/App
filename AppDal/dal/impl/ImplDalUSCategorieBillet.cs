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
    /// Implementation du service ImplDalUSCategorieBillet
    /// </summary>
    public class ImplDalUSCategorieBillet : IntfDalUSCategorieBillet
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalUSCategorieBillet(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalUSCategorieBillet()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion



        #region IntfDalUSCategorieBillet Members

        string IntfDalUSCategorieBillet.insertUSCategorieBillet(crlUSCategorieBillet categorieBillet, string sigleAgence)
        {
            #region declaration
            string numCategorieBillet = "";
            IntfDalUSCategorieBillet serviceUSCategorieBillet = new ImplDalUSCategorieBillet();
            string reductionPourcentage = "NULL";
            string reductionMontant = "NULL";
            int nbInsert = 0;
            #endregion

            #region implementation
            if (categorieBillet != null) 
            {
                if (categorieBillet.ReductionPourcentage >= 0) 
                {
                    reductionPourcentage = "'" + categorieBillet.ReductionPourcentage.ToString() + "'";
                }
                if (categorieBillet.ReductionMontant >= 0) 
                {
                    reductionMontant = "'" + categorieBillet.ReductionMontant.ToString("0") + "'";
                }
                categorieBillet.NumCategorieBillet = serviceUSCategorieBillet.getNumUSCategorieBillet(sigleAgence);
                this.strCommande = "INSERT INTO `uscategoriebillet` (`numCategorieBillet`,`categorieBillet`,";
                this.strCommande += " `reductionPourcentage`,`reductionMontant`,`dureeMaxValidite`) VALUES";
                this.strCommande += " ('" + categorieBillet.NumCategorieBillet + "',";
                this.strCommande += " '" + categorieBillet.CategorieBillet + "'," + reductionPourcentage + ",";
                this.strCommande += " " + reductionMontant + ",'" + categorieBillet.DureeMaxValidite.ToString("0") + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1) 
                {
                    numCategorieBillet = categorieBillet.NumCategorieBillet;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numCategorieBillet;
        }

        bool IntfDalUSCategorieBillet.updateUSCategorieBillet(crlUSCategorieBillet categorieBillet)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            string reductionPourcentage = "NULL";
            string reductionMontant = "NULL";
            #endregion

            #region implementation
            if (categorieBillet != null) 
            {
                if (categorieBillet.ReductionPourcentage >= 0)
                {
                    reductionPourcentage = "'" + categorieBillet.ReductionPourcentage.ToString() + "'";
                }
                if (categorieBillet.ReductionMontant >= 0)
                {
                    reductionMontant = "'" + categorieBillet.ReductionMontant.ToString("0") + "'";
                }
                this.strCommande = "UPDATE `uscategoriebillet` SET `categorieBillet`='" + categorieBillet.CategorieBillet + "',";
                this.strCommande += " `reductionPourcentage`=" + reductionPourcentage + ",`reductionMontant`=" + reductionMontant + ",";
                this.strCommande += " `dureeMaxValidite`='" + categorieBillet.DureeMaxValidite.ToString("0") + "'";
                this.strCommande += " WHERE `numCategorieBillet`='" + categorieBillet.NumCategorieBillet + "'";
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

        crlUSCategorieBillet IntfDalUSCategorieBillet.selectUSCategorieBillet(string numCategorieBillet)
        {
            #region declaration
            crlUSCategorieBillet categorieBillet = null;
            #endregion

            #region implementation
            if (numCategorieBillet != "") 
            {
                this.strCommande = "SELECT * FROM `uscategoriebillet` WHERE `numCategorieBillet`='" + numCategorieBillet + "'";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            categorieBillet = new crlUSCategorieBillet();
                            categorieBillet.CategorieBillet = this.reader["categorieBillet"].ToString();
                            categorieBillet.NumCategorieBillet = this.reader["numCategorieBillet"].ToString();
                            try
                            {
                                categorieBillet.ReductionMontant = double.Parse(this.reader["reductionMontant"].ToString());
                            }
                            catch (Exception) { }
                            try
                            {
                                categorieBillet.ReductionPourcentage = double.Parse(this.reader["reductionPourcentage"].ToString());
                            }
                            catch (Exception) { }
                            try
                            {
                                categorieBillet.DureeMaxValidite = int.Parse(this.reader["dureeMaxValidite"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return categorieBillet;
        }

        string IntfDalUSCategorieBillet.getNumUSCategorieBillet(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numCategorieBillet = "00001";
            string[] tempNumCategorieBillet = null;
            string strDate = "CU" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT uscategoriebillet.numCategorieBillet AS maxNum FROM uscategoriebillet";
            this.strCommande += " WHERE uscategoriebillet.numCategorieBillet LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumCategorieBillet = reader["maxNum"].ToString().ToString().Split('/');
                        numCategorieBillet = tempNumCategorieBillet[tempNumCategorieBillet.Length - 1];
                    }
                    numTemp = double.Parse(numCategorieBillet) + 1;
                    if (numTemp < 10)
                        numCategorieBillet = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numCategorieBillet = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numCategorieBillet = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numCategorieBillet = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numCategorieBillet = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numCategorieBillet = strDate + "/" + sigleAgence + "/" + numCategorieBillet;
            #endregion

            return numCategorieBillet;
        }

        string IntfDalUSCategorieBillet.isCategorieBillet(crlUSCategorieBillet categorieBillet)
        {
            #region declaration
            string numCategorie = "";
            #endregion

            #region implementation
            if (categorieBillet != null)
            {
                this.strCommande = "SELECT uscategoriebillet.numCategorieBillet FROM uscategoriebillet";
                this.strCommande += " WHERE uscategoriebillet.categorieBillet = '" + categorieBillet.CategorieBillet + "' AND";
                this.strCommande += " uscategoriebillet.numCategorieBillet <> '" + categorieBillet.NumCategorieBillet + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            numCategorie = this.reader["numCategorieBillet"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

            }
            #endregion

            return numCategorie;
        }
        #endregion


        void IntfDalUSCategorieBillet.loadDdlCategorieBillet(DropDownList ddl)
        {
            #region implementation
            if (ddl != null)
            {
                ddl.Items.Clear();
                ddl.Items.Add("");

                this.strCommande = "SELECT * FROM `uscategoriebillet` ORDER BY uscategoriebillet.categorieBillet ASC";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            ddl.Items.Add(new ListItem(this.reader["categorieBillet"].ToString(), this.reader["numCategorieBillet"].ToString()));
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }


        void IntfDalUSCategorieBillet.insertToGridCategorieBillet(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalUSCategorieBillet serviceUSCategorieBillet = new ImplDalUSCategorieBillet();
            #endregion

            #region implementation

            this.strCommande = "SELECT uscategoriebillet.numCategorieBillet, uscategoriebillet.categorieBillet,";
            this.strCommande += " uscategoriebillet.reductionPourcentage, uscategoriebillet.reductionMontant,";
            this.strCommande += " uscategoriebillet.dureeMaxValidite FROM uscategoriebillet";
            this.strCommande += " WHERE " + paramLike + " LIKE  '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;


            gridView.DataSource = serviceUSCategorieBillet.getDataTableCategorieBillet(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalUSCategorieBillet.getDataTableCategorieBillet(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numCategorieBillet", typeof(string));
            dataTable.Columns.Add("categorieBillet", typeof(string));
            dataTable.Columns.Add("reductionPourcentage", typeof(string));
            dataTable.Columns.Add("reductionMontant", typeof(string));
            dataTable.Columns.Add("dureeMaxValidite", typeof(string));

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

                        dr["numCategorieBillet"] = reader["numCategorieBillet"].ToString();
                        dr["categorieBillet"] = reader["categorieBillet"].ToString();
                        dr["reductionPourcentage"] = reader["reductionPourcentage"].ToString();
                        dr["reductionMontant"] = reader["reductionMontant"].ToString();
                        dr["dureeMaxValidite"] = reader["dureeMaxValidite"].ToString();

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
