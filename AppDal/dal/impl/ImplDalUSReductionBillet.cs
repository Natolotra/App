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
    /// Summary description for ImplDalUSReductionBillet
    /// </summary>
    public class ImplDalUSReductionBillet : IntfDalUSReductionBillet
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalUSReductionBillet(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalUSReductionBillet()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion



        #region IntfDalUSReductionBillet Members

        string IntfDalUSReductionBillet.insertUSReductionBillet(crlUSReductionBillet reductionBillet, string sigleAgence)
        {
            #region declaration
            string numReductionBillet = "";
            int nbInsert = 0;
            string reductionPourcentage = "NULL";
            string reductionMontant = "NULL";
            IntfDalUSReductionBillet serviceUSReductionBillet = new ImplDalUSReductionBillet();
            #endregion

            #region implementation
            if (reductionBillet != null) 
            {
                if (reductionBillet.ReductionPourcentage >= 0)
                {
                    reductionPourcentage = "'" + reductionBillet.ReductionPourcentage.ToString() + "'";
                }
                if (reductionBillet.ReductionMontant >= 0)
                {
                    reductionMontant = "'" + reductionBillet.ReductionMontant.ToString("0") + "'";
                }
                reductionBillet.NumReductionBillet = serviceUSReductionBillet.getNumUSReductionBillet(sigleAgence);
                this.strCommande = "INSERT INTO `usreductionbillet` (`numReductionBillet`,`reductionBillet`,";
                this.strCommande += " `reductionPourcentage`,`reductionMontant`) VALUES";
                this.strCommande += " ('" + reductionBillet.NumReductionBillet + "','" + reductionBillet.ReductionBillet + "',";
                this.strCommande += " " + reductionPourcentage + "," + reductionMontant + ")";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1) 
                {
                    numReductionBillet = reductionBillet.NumReductionBillet;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numReductionBillet;
        }

        bool IntfDalUSReductionBillet.updateUSReductionBillet(crlUSReductionBillet reductionBillet)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            string reductionPourcentage = "NULL";
            string reductionMontant = "NULL";
            #endregion

            #region implementation
            if (reductionBillet != null) 
            {
                if (reductionBillet.ReductionPourcentage >= 0)
                {
                    reductionPourcentage = "'" + reductionBillet.ReductionPourcentage.ToString() + "'";
                }
                if (reductionBillet.ReductionMontant >= 0)
                {
                    reductionMontant = "'" + reductionBillet.ReductionMontant.ToString("0") + "'";
                }
                this.strCommande = "UPDATE `usreductionbillet` SET `reductionBillet`='" + reductionBillet.ReductionBillet + "',";
                this.strCommande += " `reductionPourcentage`=" + reductionPourcentage + ",`reductionMontant`=" + reductionMontant;
                this.strCommande += " WHERE `numReductionBillet`='" + reductionBillet.NumReductionBillet + "'";
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

        crlUSReductionBillet IntfDalUSReductionBillet.selectUSReductionBillet(string numReductionBillet)
        {
            #region declaration
            crlUSReductionBillet reductionBillet = null;
            #endregion

            #region implementation
            if (numReductionBillet != "") 
            {
                this.strCommande = "SELECT * FROM `usreductionbillet` WHERE `numReductionBillet`='" + numReductionBillet + "'";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            reductionBillet = new crlUSReductionBillet();
                            reductionBillet.NumReductionBillet = this.reader["numReductionBillet"].ToString();
                            reductionBillet.ReductionBillet = this.reader["reductionBillet"].ToString();
                            try
                            {
                                reductionBillet.ReductionMontant = double.Parse(this.reader["reductionMontant"].ToString());
                            }
                            catch (Exception) { }
                            try
                            {
                                reductionBillet.ReductionPourcentage = double.Parse(this.reader["reductionPourcentage"].ToString());
                            }
                            catch (Exception) { }
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return reductionBillet;
        }

        string IntfDalUSReductionBillet.getNumUSReductionBillet(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numReductionBillet = "00001";
            string[] tempNumReductionBillet = null;
            string strDate = "RB" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT usreductionbillet.numReductionBillet AS maxNum FROM usreductionbillet";
            this.strCommande += " WHERE usreductionbillet.numReductionBillet LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumReductionBillet = reader["maxNum"].ToString().ToString().Split('/');
                        numReductionBillet = tempNumReductionBillet[tempNumReductionBillet.Length - 1];
                    }
                    numTemp = double.Parse(numReductionBillet) + 1;
                    if (numTemp < 10)
                        numReductionBillet = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numReductionBillet = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numReductionBillet = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numReductionBillet = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numReductionBillet = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numReductionBillet = strDate + "/" + sigleAgence + "/" + numReductionBillet;
            #endregion

            return numReductionBillet;
        }


        void IntfDalUSReductionBillet.loadDddlReductionBillet(DropDownList ddl)
        {
            #region declaration
            #endregion

            #region implementation
            this.strCommande = "SELECT * FROM `usreductionbillet`";

            ddl.Items.Clear();
            ddl.Items.Add("");

            this.serviceConnectBase.openConnection();
            this.reader = this.serviceConnectBase.select(this.strCommande);
            if (this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    while(this.reader.Read())
                    {
                        ddl.Items.Add(new ListItem(this.reader["reductionBillet"].ToString(), this.reader["numReductionBillet"].ToString()));
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            #endregion
        }
        #endregion


        void IntfDalUSReductionBillet.insertToGridUSReductionBillet(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalUSReductionBillet serviceUSReductionBillet = new ImplDalUSReductionBillet();
            #endregion

            #region implementation

            this.strCommande = "SELECT usreductionbillet.numReductionBillet, usreductionbillet.reductionBillet,";
            this.strCommande += " usreductionbillet.reductionPourcentage, usreductionbillet.reductionMontant";
            this.strCommande += " FROM usreductionbillet";
            this.strCommande += " WHERE " + paramLike + " LIKE  '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;


            gridView.DataSource = serviceUSReductionBillet.getDataTableUSReductionBillet(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalUSReductionBillet.getDataTableUSReductionBillet(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numReductionBillet", typeof(string));
            dataTable.Columns.Add("reductionBillet", typeof(string));
            dataTable.Columns.Add("reductionPourcentage", typeof(string));
            dataTable.Columns.Add("reductionMontant", typeof(string));

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

                        dr["numReductionBillet"] = reader["numReductionBillet"].ToString();
                        dr["reductionBillet"] = reader["reductionBillet"].ToString();
                        dr["reductionMontant"] = serviceGeneral.separateurDesMilles(reader["reductionMontant"].ToString()) + " Ar";
                        dr["reductionPourcentage"] = reader["reductionPourcentage"].ToString() + " %";

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
