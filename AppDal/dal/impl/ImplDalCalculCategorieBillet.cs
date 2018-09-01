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
using arch.crl;
using MySql.Data.MySqlClient;

namespace arch.dal.impl
{
    /// <summary>
    /// Summary description for ImplDalCalculCategorieBillet
    /// </summary>
    public class ImplDalCalculCategorieBillet : IntfDalCalculCategorieBillet
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalCalculCategorieBillet()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalCalculCategorieBillet(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion



        #region IntfDalCalculCategorieBillet Members

        crlCalculCategorieBillet IntfDalCalculCategorieBillet.selectCalculCategorieBillet(string numCalculCategorieBillet)
        {
            #region declaration
            crlCalculCategorieBillet CalculPrixBilletCategorieBillet = null;
            #endregion

            #region implementation
            if (numCalculCategorieBillet != "")
            {
                this.strCommande = "SELECT * FROM `calculcategoriebillet` WHERE (`numCalculCategorieBillet`='" + numCalculCategorieBillet + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            CalculPrixBilletCategorieBillet = new crlCalculCategorieBillet();
                            CalculPrixBilletCategorieBillet.NumCalculCategorieBillet = this.reader["numCalculCategorieBillet"].ToString();
                            CalculPrixBilletCategorieBillet.IndicateurPrixBillet = this.reader["indicateurPrixBillet"].ToString();
                            try
                            {
                                CalculPrixBilletCategorieBillet.PourcentagePrixBillet = double.Parse(this.reader["pourcentagePrixBillet"].ToString());
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

            return CalculPrixBilletCategorieBillet;
        }

        crlCalculCategorieBillet IntfDalCalculCategorieBillet.selectCalculCategorieBilletIndicateur(string indicateurCalculCategorieBillet)
        {
            #region declaration
            crlCalculCategorieBillet CalculPrixBilletCategorieBillet = null;
            #endregion

            #region implementation
            if (indicateurCalculCategorieBillet != "")
            {
                this.strCommande = "SELECT * FROM `calculcategoriebillet` WHERE (`indicateurPrixBillet`='" + indicateurCalculCategorieBillet + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            CalculPrixBilletCategorieBillet = new crlCalculCategorieBillet();
                            CalculPrixBilletCategorieBillet.NumCalculCategorieBillet = this.reader["numCalculCategorieBillet"].ToString();
                            CalculPrixBilletCategorieBillet.IndicateurPrixBillet = this.reader["indicateurPrixBillet"].ToString();
                            try
                            {
                                CalculPrixBilletCategorieBillet.PourcentagePrixBillet = double.Parse(this.reader["pourcentagePrixBillet"].ToString());
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

            return CalculPrixBilletCategorieBillet;
        }

        void IntfDalCalculCategorieBillet.loadDdlCulculeCategorieBillet(DropDownList ddl)
        {
            #region implementation
            if (ddl != null)
            {
                this.strCommande = "SELECT * FROM `calculcategoriebillet` ORDER BY calculcategoriebillet.numCalculCategorieBillet ASC";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        ddl.Items.Clear();
                        ddl.Items.Add(new ListItem("", ""));
                        while (this.reader.Read())
                        {
                            ddl.Items.Add(new ListItem(this.reader["indicateurPrixBillet"].ToString(), this.reader["numCalculCategorieBillet"].ToString()));
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }

        void IntfDalCalculCategorieBillet.loadDdlCalculeCategorieBillet(DropDownList ddl, string strWhere)
        {
            #region declaration
            string[] strTab = null;
            string strWhereVar = "";
            #endregion

            #region implementation
            if (ddl != null)
            {
                strTab = strWhere.Split(';');

                if (strTab.Length > 0)
                {
                    for (int i = 0; i < strTab.Length; i++)
                    {
                        if (i == 0)
                        {
                            strWhereVar = " WHERE calculcategoriebillet.indicateurPrixBillet <> '" + strTab[i] + "'";
                        }
                        else
                        {
                            strWhereVar += " OR calculcategoriebillet.indicateurPrixBillet <> '" + strTab[i] + "'";
                        }
                    }
                }

                this.strCommande = "SELECT * FROM `calculcategoriebillet` " + strWhereVar;
                this.strCommande += " ORDER BY calculcategoriebillet.numCalculCategorieBillet ASC";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        ddl.Items.Clear();
                        while (this.reader.Read())
                        {
                            ddl.Items.Add(new ListItem(this.reader["indicateurPrixBillet"].ToString(), this.reader["numCalculCategorieBillet"].ToString()));
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }

        string IntfDalCalculCategorieBillet.insertCalculeCategorieBillet(crlCalculCategorieBillet calculCategorieBillet, string sigleAgence)
        {
            #region declaration
            string numCalculCategorieBillet = "";
            int nbInsert = 0;
            IntfDalCalculCategorieBillet serviceCalculCategorieBillet = new ImplDalCalculCategorieBillet();
            #endregion

            #region implementation
            if (calculCategorieBillet != null && sigleAgence != "")
            {
                calculCategorieBillet.NumCalculCategorieBillet = serviceCalculCategorieBillet.getNumCalculCategorieBillet(sigleAgence);

                this.strCommande = "INSERT INTO `calculcategoriebillet` (`numCalculCategorieBillet`,`pourcentagePrixBillet`,";
                this.strCommande += "`indicateurPrixBillet`) VALUES ('" + calculCategorieBillet.NumCalculCategorieBillet + "',";
                this.strCommande += "'" + calculCategorieBillet.PourcentagePrixBillet.ToString().Replace(',','.') + "',";
                this.strCommande += "'" + calculCategorieBillet.IndicateurPrixBillet + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    numCalculCategorieBillet = calculCategorieBillet.NumCalculCategorieBillet;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numCalculCategorieBillet;
        }

        string IntfDalCalculCategorieBillet.isCalculeCategorieBillet(crlCalculCategorieBillet calculCategorieBillet)
        {
            #region declaration
            string numCalculCategorieBillet = "";
            #endregion

            #region implementation
            if (calculCategorieBillet != null)
            {
                this.strCommande = "SELECT calculcategoriebillet.numCalculCategorieBillet  FROM `calculcategoriebillet` WHERE";
                this.strCommande += " `numCalculCategorieBillet`<>'" + calculCategorieBillet.NumCalculCategorieBillet + "' AND";
                this.strCommande += " `indicateurPrixBillet`='" + calculCategorieBillet.IndicateurPrixBillet + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            numCalculCategorieBillet = this.reader["numCalculCategorieBillet"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numCalculCategorieBillet;
        }

        bool IntfDalCalculCategorieBillet.updateCalculCategorieBillet(crlCalculCategorieBillet calculCategorieBillet)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            #endregion

            #region implementation
            if (calculCategorieBillet != null)
            {
                this.strCommande = "UPDATE `calculcategoriebillet` SET `pourcentagePrixBillet`='" + calculCategorieBillet.PourcentagePrixBillet.ToString().Replace(',', '.') + "',";
                this.strCommande += " `indicateurPrixBillet`='" + calculCategorieBillet.IndicateurPrixBillet + "' WHERE";
                this.strCommande += " `numCalculCategorieBillet`='" + calculCategorieBillet.NumCalculCategorieBillet + "'";

                this.serviceConnectBase.openConnection();
                nbUpdate = this.serviceConnectBase.requete(this.strCommande);
                if (nbUpdate == 1)
                    isUpdate = true;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isUpdate;
        }

        string IntfDalCalculCategorieBillet.getNumCalculCategorieBillet(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numCalculCategorieBillet = "00001";
            string[] tempNumCalculCategorieBillet = null;
            string strDate = "CC" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT calculcategoriebillet.numCalculCategorieBillet AS maxNum FROM calculcategoriebillet";
            this.strCommande += " WHERE calculcategoriebillet.numCalculCategorieBillet LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumCalculCategorieBillet = reader["maxNum"].ToString().ToString().Split('/');
                        numCalculCategorieBillet = tempNumCalculCategorieBillet[tempNumCalculCategorieBillet.Length - 1];
                    }
                    numTemp = double.Parse(numCalculCategorieBillet) + 1;
                    if (numTemp < 10)
                        numCalculCategorieBillet = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numCalculCategorieBillet = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numCalculCategorieBillet = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numCalculCategorieBillet = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numCalculCategorieBillet = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numCalculCategorieBillet = strDate + "/" + sigleAgence + "/" + numCalculCategorieBillet;
            #endregion

            return numCalculCategorieBillet;
        }
        #endregion


        #region insert to grid
        void IntfDalCalculCategorieBillet.insertToGridCalculCategorieBillet(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalCalculCategorieBillet serviceCalculCategorieBillet = new ImplDalCalculCategorieBillet();
            #endregion

            #region implementation

            this.strCommande = "SELECT calculcategoriebillet.numCalculCategorieBillet, calculcategoriebillet.pourcentagePrixBillet,";
            this.strCommande += " calculcategoriebillet.indicateurPrixBillet FROM calculcategoriebillet";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceCalculCategorieBillet.getDataTableCalculCategorieBillet(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalCalculCategorieBillet.getDataTableCalculCategorieBillet(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numCalculCategorieBillet", typeof(string));
            dataTable.Columns.Add("indicateurPrixBillet", typeof(string));
            dataTable.Columns.Add("pourcentagePrixBillet", typeof(string));
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

                        dr["numCalculCategorieBillet"] = reader["numCalculCategorieBillet"].ToString();
                        dr["indicateurPrixBillet"] = reader["indicateurPrixBillet"].ToString();
                        dr["pourcentagePrixBillet"] = reader["pourcentagePrixBillet"].ToString() + "%";

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
