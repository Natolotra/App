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
    /// Summary description for ImplDalUSInfoPasse
    /// </summary>
    public class ImplDalUSInfoPasse : IntfDalUSInfoPasse
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalUSInfoPasse(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalUSInfoPasse()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion



        #region IntfDalUSInfoPasse Members

        string IntfDalUSInfoPasse.insertUSInfoPasse(crlUSInfoPasse infoPasse, string sigleAgence)
        {
            #region declaration
            string numInfoPasse = "";
            int nbInsert = 0;
            IntfDalUSInfoPasse serviceUSInfoPasse = new ImplDalUSInfoPasse();
            #endregion

            #region implementation
            if (infoPasse != null && sigleAgence != "") 
            {
                infoPasse.NumInfoPasse = serviceUSInfoPasse.getNumUSInfoPasse(sigleAgence);
                this.strCommande = "INSERT INTO `usinfopasse` (`numInfoPasse`,`nombrePasse`,";
                this.strCommande += " `niveau`,`numReductionBillet`) VALUES";
                this.strCommande += " ('" + infoPasse.NumInfoPasse + "','" + infoPasse.NombrePasse.ToString("0") + "',";
                this.strCommande += " '" + infoPasse.Niveau.ToString("0") + "','" + infoPasse.NumReductionBillet + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1) 
                {
                    numInfoPasse = infoPasse.NumInfoPasse;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numInfoPasse;
        }

        bool IntfDalUSInfoPasse.updateUSInfoPasse(crlUSInfoPasse infoPasse)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            #endregion

            #region implementation
            if (infoPasse != null) 
            {
                this.strCommande = "UPDATE `usinfopasse` SET `nombrePasse`='" + infoPasse.NombrePasse.ToString("0") + "',";
                this.strCommande += " `niveau`='" + infoPasse.Niveau.ToString("0") + "',";
                this.strCommande += " `numReductionBillet`='" + infoPasse.NumReductionBillet + "'";
                this.strCommande += " WHERE `numInfoPasse`='" + infoPasse.NumInfoPasse + "'";

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

        string IntfDalUSInfoPasse.isUSInfoPasse(crlUSInfoPasse infoPasse)
        {
            #region declaration
            string numInfoPasse = "";
            #endregion

            #region implementation
            if (infoPasse != null) 
            {
                this.strCommande = "SELECT `numInfoPasse` FROM `usinfopasse`";
                this.strCommande += " WHERE `niveau`='" + infoPasse.Niveau.ToString("0") + "' AND";
                this.strCommande += " `nombrePasse`='" + infoPasse.NombrePasse.ToString("0") + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            numInfoPasse = this.reader["numInfoPasse"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numInfoPasse;
        }

        crlUSInfoPasse IntfDalUSInfoPasse.selectUSInfoPasse(string numInfoPasse)
        {
            #region delcaration
            crlUSInfoPasse infoPasse = null;
            IntfDalUSReductionBillet serviceUSReductionBillet = new ImplDalUSReductionBillet();
            #endregion

            #region implementation
            if (numInfoPasse != "") 
            {
                this.strCommande = "SELECT * FROM `usinfopasse` WHERE `numInfoPasse`='" + numInfoPasse + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            infoPasse = new crlUSInfoPasse();
                            try
                            {
                                infoPasse.Niveau = int.Parse(this.reader["niveau"].ToString());
                            }
                            catch (Exception) { }
                            try
                            {
                                infoPasse.NombrePasse = int.Parse(this.reader["nombrePasse"].ToString());
                            }
                            catch (Exception) { }
                            infoPasse.NumInfoPasse = this.reader["numInfoPasse"].ToString();
                            infoPasse.NumReductionBillet = this.reader["numReductionBillet"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (infoPasse != null)
                {
                    if (infoPasse.NumReductionBillet != "")
                    {
                        infoPasse.reductionBillet = serviceUSReductionBillet.selectUSReductionBillet(infoPasse.NumReductionBillet);
                    }
                }
            }
            #endregion

            return infoPasse;
        }

        string IntfDalUSInfoPasse.getNumUSInfoPasse(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numInfoPasse = "00001";
            string[] tempNumInfoPasse = null;
            string strDate = "IV" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT usinfopasse.numInfoPasse AS maxNum FROM usinfopasse";
            this.strCommande += " WHERE usinfopasse.numInfoPasse LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumInfoPasse = reader["maxNum"].ToString().ToString().Split('/');
                        numInfoPasse = tempNumInfoPasse[tempNumInfoPasse.Length - 1];
                    }
                    numTemp = double.Parse(numInfoPasse) + 1;
                    if (numTemp < 10)
                        numInfoPasse = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numInfoPasse = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numInfoPasse = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numInfoPasse = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numInfoPasse = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numInfoPasse = strDate + "/" + sigleAgence + "/" + numInfoPasse;
            #endregion

            return numInfoPasse;
        }

        void IntfDalUSInfoPasse.loadDdlInfoPasse(DropDownList ddl, int niveau)
        {
            #region implementation

            if (ddl != null)
            {
                ddl.Items.Clear();
                ddl.Items.Add("");

                this.strCommande = "SELECT usinfopasse.nombrePasse,";
                this.strCommande += " usinfopasse.numInfoPasse FROM usinfopasse";
                this.strCommande += " WHERE usinfopasse.niveau = '" + niveau + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            ddl.Items.Add(new ListItem(this.reader["nombrePasse"].ToString(), this.reader["numInfoPasse"].ToString()));
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }
        #endregion

        #region IntfDalUSInfoPasse Members


        void IntfDalUSInfoPasse.insertToGridInfoPasse(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalUSInfoPasse serviceUSInfoPasse = new ImplDalUSInfoPasse();
            #endregion

            #region implementation

            this.strCommande = "SELECT usinfopasse.numInfoPasse, usinfopasse.nombrePasse,";
            this.strCommande += " usinfopasse.niveau, usinfopasse.numReductionBillet";
            this.strCommande += " FROM usinfopasse";
            this.strCommande += " WHERE " + paramLike + " LIKE  '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;


            gridView.DataSource = serviceUSInfoPasse.getDataTableInfoPasse(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalUSInfoPasse.getDataTableInfoPasse(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            IntfDalUSPlageNombreBillet serviceUSPlageNombreBillet = new ImplDalUSPlageNombreBillet();
            IntfDalUSReductionBillet serviceUSReductionBillet = new ImplDalUSReductionBillet();
            IntfDalUSPrixBase serviceUSPrixBase = new ImplDalUSPrixBase();
            crlUSReductionBillet reduction = null;
            crlUSPlageNombreBillet plageNombreBillet = null;
            crlUSPrixBase prixBase = null;
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numInfoPasse", typeof(string));
            dataTable.Columns.Add("nombrePasse", typeof(string));
            dataTable.Columns.Add("niveau", typeof(string));
            dataTable.Columns.Add("prixUnitaire", typeof(string));
            dataTable.Columns.Add("dureeValidite", typeof(string));

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

                        dr["numInfoPasse"] = reader["numInfoPasse"].ToString();
                        dr["nombrePasse"] = reader["nombrePasse"].ToString();
                        dr["niveau"] = reader["niveau"].ToString();

                        reduction = serviceUSReductionBillet.selectUSReductionBillet(this.reader["numReductionBillet"].ToString());
                        try
                        {
                            prixBase = serviceUSPrixBase.selectUSPrixBase(int.Parse(reader["niveau"].ToString()));
                        }
                        catch (Exception) { }
                        try
                        {
                            plageNombreBillet = serviceUSPlageNombreBillet.getPlageNombreBillet(int.Parse(reader["nombrePasse"].ToString()));
                        }
                        catch (Exception) { }


                        if (prixBase != null)
                        {
                            if (reduction != null)
                            {
                                if (reduction.ReductionMontant > 0)
                                {
                                    dr["prixUnitaire"] = serviceGeneral.separateurDesMilles((prixBase.MontantPrixBase - reduction.ReductionMontant).ToString("0")) + " Ar";
                                }
                                else
                                {
                                    dr["prixUnitaire"] = serviceGeneral.separateurDesMilles((prixBase.MontantPrixBase - (prixBase.MontantPrixBase * reduction.ReductionPourcentage / 100)).ToString("0")) + " Ar";
                                }
                            }
                            else
                            {
                                dr["prixUnitaire"] = serviceGeneral.separateurDesMilles(prixBase.MontantPrixBase.ToString("0")) + " Ar";
                            }
                        }
                        else 
                        {
                            dr["prixUnitaire"] = "";
                        }

                        if (plageNombreBillet != null)
                        {
                            dr["dureeValidite"] = serviceGeneral.getTextTimeSpan(serviceGeneral.getStringTimeSpan(plageNombreBillet.DureeDeValidite));
                        }
                        else 
                        {
                            dr["dureeValidite"] = "";
                        }


                        reduction = null;
                        prixBase = null;
                        plageNombreBillet = null;
                        

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
