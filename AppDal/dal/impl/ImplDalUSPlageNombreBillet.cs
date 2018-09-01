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
    /// Implementation du service ImplDalUSPlageNombreBillet
    /// </summary>
    public class ImplDalUSPlageNombreBillet : IntfDalUSPlageNombreBillet
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalUSPlageNombreBillet(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalUSPlageNombreBillet()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion



        #region IntfDalUSPlageNombreBillet Members

        string IntfDalUSPlageNombreBillet.insertUSPlageNombreBillet(crlUSPlageNombreBillet plageNombreBillet, string sigleAgence)
        {
            #region declaration
            string numPlageNombreBillet = "";
            IntfDalUSPlageNombreBillet serviceUSPlageNombreBillet = new ImplDalUSPlageNombreBillet();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            int nbInsert = 0;
            #endregion

            #region implementation
            if (plageNombreBillet != null) 
            {
                plageNombreBillet.NumPlageNombreBillet = serviceUSPlageNombreBillet.getNumUSPlageNombreBillet(sigleAgence);
                this.strCommande = "INSERT INTO `usplagenombrebillet` (`numPlageNombreBillet`,`nombreD`,`nombreF`,`numReductionBillet`,`dureeDeValidite`)";
                this.strCommande += " VALUES ('" + plageNombreBillet.NumPlageNombreBillet + "','" + plageNombreBillet.NombreD + "',";
                this.strCommande += " '" + plageNombreBillet.NombreF + "','" + plageNombreBillet.NumReductionBillet + "',";
                this.strCommande += " '" + serviceGeneral.getStringTimeSpan(plageNombreBillet.DureeDeValidite) + "')";
                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1) 
                {
                    numPlageNombreBillet = plageNombreBillet.NumPlageNombreBillet;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numPlageNombreBillet;
        }

        bool IntfDalUSPlageNombreBillet.updateUSPlageNombreBillet(crlUSPlageNombreBillet plageNombreBillet)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            #endregion

            #region implementation
            if (plageNombreBillet != null) 
            {
                this.strCommande = "UPDATE `usplagenombrebillet` SET `nombreD`='" + plageNombreBillet.NombreD + "',";
                this.strCommande += " `nombreF`='" + plageNombreBillet.NombreF + "',`numReductionBillet`='" + plageNombreBillet.NumReductionBillet + "',";
                this.strCommande += " `dureeDeValidite`='" + serviceGeneral.getStringTimeSpan(plageNombreBillet.DureeDeValidite) + "'";
                this.strCommande += " WHERE `numPlageNombreBillet`='" + plageNombreBillet.NumPlageNombreBillet + "'";
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

        string IntfDalUSPlageNombreBillet.isUSPlageNombreBillet(crlUSPlageNombreBillet plageNombreBillet)
        {
            #region declaration
            string numPlageNombreBillet = "";
            #endregion

            #region impementation
            if (plageNombreBillet != null) 
            {
                this.strCommande = "SELECT * FROM `usplagenombrebillet` WHERE ";
                this.strCommande += " `nombreD`='" + plageNombreBillet.NombreD + "' AND";
                this.strCommande += " `nombreF`='" + plageNombreBillet.NombreF + "' AND";
                this.strCommande += " `numPlageNombreBillet`<>'" + plageNombreBillet.NumPlageNombreBillet + "'";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            numPlageNombreBillet = this.reader["numPlageNombreBillet"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numPlageNombreBillet;
        }

        crlUSPlageNombreBillet IntfDalUSPlageNombreBillet.selectUSPlageNombreBillet(string numPlageNombreBillet)
        {
            #region declaration
            crlUSPlageNombreBillet plageNombreBillet = null;
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            #endregion

            #region impementation
            if (numPlageNombreBillet != "")
            {
                this.strCommande = "SELECT * FROM `usplagenombrebillet` WHERE ";
                this.strCommande += " `numPlageNombreBillet`='" + numPlageNombreBillet + "'";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            plageNombreBillet = new crlUSPlageNombreBillet();
                            plageNombreBillet.NumPlageNombreBillet = this.reader["numPlageNombreBillet"].ToString();
                            try
                            {
                                plageNombreBillet.NombreD = int.Parse(this.reader["nombreD"].ToString());
                            }
                            catch (Exception) { }
                            try
                            {
                                plageNombreBillet.NombreF = int.Parse(this.reader["nombreF"].ToString());
                            }
                            catch (Exception) { }
                            plageNombreBillet.NumReductionBillet = this.reader["numReductionBillet"].ToString();
                            plageNombreBillet.DureeDeValidite = serviceGeneral.getTimeSpan(this.reader["dureeDeValidite"].ToString());
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return plageNombreBillet;
        }

        string IntfDalUSPlageNombreBillet.getNumUSPlageNombreBillet(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numPlageNombreBillet = "00001";
            string[] tempNumPlageNombreBillet = null;
            string strDate = "PL" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT usplagenombrebillet.numPlageNombreBillet AS maxNum FROM usplagenombrebillet";
            this.strCommande += " WHERE usplagenombrebillet.numPlageNombreBillet LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumPlageNombreBillet = reader["maxNum"].ToString().ToString().Split('/');
                        numPlageNombreBillet = tempNumPlageNombreBillet[tempNumPlageNombreBillet.Length - 1];
                    }
                    numTemp = double.Parse(numPlageNombreBillet) + 1;
                    if (numTemp < 10)
                        numPlageNombreBillet = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numPlageNombreBillet = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numPlageNombreBillet = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numPlageNombreBillet = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numPlageNombreBillet = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numPlageNombreBillet = strDate + "/" + sigleAgence + "/" + numPlageNombreBillet;
            #endregion

            return numPlageNombreBillet;
        }

        crlUSReductionBillet IntfDalUSPlageNombreBillet.getReductionBillet(int nombreBillet)
        {
            #region declaration
            crlUSReductionBillet reductionBillet = null;
            #endregion

            #region implementation
            if (nombreBillet > 0) 
            {
                this.strCommande = "SELECT usreductionbillet.numReductionBillet,";
                this.strCommande += " usreductionbillet.reductionBillet, usreductionbillet.reductionPourcentage,";
                this.strCommande += " usreductionbillet.reductionMontant FROM usplagenombrebillet";
                this.strCommande += " Inner Join usreductionbillet ON usreductionbillet.numReductionBillet = usplagenombrebillet.numReductionBillet";
                this.strCommande += " WHERE usplagenombrebillet.nombreD <= '" + nombreBillet.ToString("0") + "' AND";
                this.strCommande += " usplagenombrebillet.nombreF >= '" + nombreBillet.ToString("0") + "'";

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

        crlUSPlageNombreBillet IntfDalUSPlageNombreBillet.getPlageNombreBillet(int nombreBillet)
        {
            #region declaration
            crlUSPlageNombreBillet plageNombreBillet = null;
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            #endregion

            #region implementation
            if (nombreBillet > 0)
            {
                this.strCommande = "SELECT usplagenombrebillet.numPlageNombreBillet, usplagenombrebillet.nombreD,";
                this.strCommande += " usplagenombrebillet.nombreF, usplagenombrebillet.numReductionBillet,";
                this.strCommande += " usplagenombrebillet.dureeDeValidite FROM usplagenombrebillet";
                this.strCommande += " WHERE usplagenombrebillet.nombreD <= '" + nombreBillet.ToString("0") + "' AND";
                this.strCommande += " usplagenombrebillet.nombreF >= '" + nombreBillet.ToString("0") + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            plageNombreBillet = new crlUSPlageNombreBillet();
                            try
                            {
                                plageNombreBillet.DureeDeValidite = serviceGeneral.getTimeSpan(this.reader["dureeDeValidite"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                plageNombreBillet.NombreD = int.Parse(this.reader["nombreD"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                plageNombreBillet.NombreF = int.Parse(this.reader["nombreF"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            plageNombreBillet.NumPlageNombreBillet = this.reader["numPlageNombreBillet"].ToString();
                            plageNombreBillet.NumReductionBillet = this.reader["numReductionBillet"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion


            return plageNombreBillet;
        }
        #endregion





        void IntfDalUSPlageNombreBillet.insertToGridPlageNombreBillet(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalUSPlageNombreBillet serviceUSPlageNombreBillet = new ImplDalUSPlageNombreBillet();
            #endregion

            #region implementation

            this.strCommande = "SELECT usplagenombrebillet.numPlageNombreBillet, usplagenombrebillet.nombreD,";
            this.strCommande += " usplagenombrebillet.nombreF, usplagenombrebillet.numReductionBillet, usplagenombrebillet.dureeDeValidite,";
            this.strCommande += " usreductionbillet.numReductionBillet, usreductionbillet.reductionBillet, usreductionbillet.reductionPourcentage,";
            this.strCommande += " usreductionbillet.reductionMontant FROM usplagenombrebillet";
            this.strCommande += " Inner Join usreductionbillet ON usreductionbillet.numReductionBillet = usplagenombrebillet.numReductionBillet";
            this.strCommande += " WHERE " + paramLike + " LIKE  '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;


            gridView.DataSource = serviceUSPlageNombreBillet.getDataTablePlageNombreBillet(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalUSPlageNombreBillet.getDataTablePlageNombreBillet(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numPlageNombreBillet", typeof(string));
            dataTable.Columns.Add("plage", typeof(string));
            dataTable.Columns.Add("dureeDeValidite", typeof(string));
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

                        dr["numPlageNombreBillet"] = reader["numPlageNombreBillet"].ToString();
                        dr["plage"] = reader["nombreD"].ToString() + " à " + reader["nombreF"].ToString();
                        dr["dureeDeValidite"] = serviceGeneral.getTextTimeSpan(reader["dureeDeValidite"].ToString());
                        dr["reductionBillet"] = reader["reductionBillet"].ToString();
                        dr["reductionPourcentage"] = reader["reductionPourcentage"].ToString() + " %";
                        dr["reductionMontant"] = reader["reductionMontant"].ToString() + " Ar";

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