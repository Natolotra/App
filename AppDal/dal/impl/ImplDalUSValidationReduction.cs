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
    /// Description résumée de ImplDalUSValidationReduction
    /// </summary>
    public class ImplDalUSValidationReduction : IntfDalUSValidationReduction
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalUSValidationReduction(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalUSValidationReduction()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion

        #region methode
        string IntfDalUSValidationReduction.insertUSValidationReduction(crlUSValidationReduction validationReduction, string sigleAgence)
        {
            #region declaration
            string numUSValidationReduction = "";
            IntfDalUSValidationReduction serviceUSValidationReduction = new ImplDalUSValidationReduction();
            int nbInsert = 0;
            string numCarte = "NULL";
            string matriculeAgentControleur = "NULL";
            #endregion

            #region implementation
            if (validationReduction != null)
            {
                if (validationReduction.NumCarte != "")
                {
                    numCarte = "'" + validationReduction.NumCarte + "'";
                }
                if (validationReduction.MatriculeAgentControleur != "")
                {
                    matriculeAgentControleur = "'" + validationReduction.MatriculeAgentControleur + "'";
                }
                validationReduction.NumUSValidationReduction = serviceUSValidationReduction.getNumUSValidationReduction(sigleAgence);

                this.strCommande = "INSERT INTO `usvalidationreduction` (`numUSValidationReduction`,`dateValidationReduction`,";
                this.strCommande += " `numUSReductionParticulier`,`numCarte`,`valideDu`,`valideAu`,`isLundi`,`isMardi`,";
                this.strCommande += " `isMercredi`,`isJeudi`,`isVendredi`,`isSamedi`,`isDimanche`,`matriculeAgent`,";
                this.strCommande += " `matriculeAgentControleur`,`isValider`) VALUES";
                this.strCommande += " ('" + validationReduction.NumUSValidationReduction + "','" + validationReduction.DateValidationReduction.ToString("yyyy-MM-dd") + "',";
                this.strCommande += " '" + validationReduction.NumUSReductionParticulier + "'," + numCarte + ",";
                this.strCommande += " '" + validationReduction.ValideDu.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                this.strCommande += " '" + validationReduction.ValideAu.ToString("yyyy-MM-dd HH:mm:ss") + "','" + validationReduction.IsLundi.ToString("0") + "',";
                this.strCommande += " '" + validationReduction.IsMardi.ToString("0") + "','" + validationReduction.IsMercredi.ToString("0") + "',";
                this.strCommande += " '" + validationReduction.IsJeudi.ToString("0") + "','" + validationReduction.IsVendredi.ToString("0") + "',";
                this.strCommande += " '" + validationReduction.IsSamedi.ToString("0") + "','" + validationReduction.IsDimanche.ToString("0") + "',";
                this.strCommande += " '" + validationReduction.MatriculeAgent + "'," + matriculeAgentControleur + ",";
                this.strCommande += " '" + validationReduction.IsValider.ToString("0") + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    numUSValidationReduction = validationReduction.NumUSValidationReduction;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numUSValidationReduction;
        }

        bool IntfDalUSValidationReduction.updateUSValidationReduction(crlUSValidationReduction validationReduction)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            string numCarte = "NULL";
            string matriculeAgentControleur = "NULL";
            #endregion

            #region implementation
            if (validationReduction != null)
            {
                if (validationReduction.NumCarte != "")
                {
                    numCarte = "'" + validationReduction.NumCarte + "'";
                }
                if (validationReduction.MatriculeAgentControleur != "")
                {
                    matriculeAgentControleur = "'" + validationReduction.MatriculeAgentControleur + "'";
                }
                this.strCommande = "UPDATE `usvalidationreduction` SET `dateValidationReduction`='" + validationReduction.DateValidationReduction.ToString("yyyy-MM-dd") + "',";
                this.strCommande += " `numUSReductionParticulier`='" + validationReduction.NumUSReductionParticulier + "',";
                this.strCommande += " `numCarte`=" + numCarte + ",`valideDu`='" + validationReduction.ValideDu.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                this.strCommande += " `valideAu`='" + validationReduction.ValideAu.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                this.strCommande += " `isLundi`='" + validationReduction.IsLundi.ToString("0") + "',`isMardi`='" + validationReduction.IsMardi.ToString("0") + "',";
                this.strCommande += " `isMercredi`='" + validationReduction.IsMercredi.ToString("0") + "',`isJeudi`='" + validationReduction.IsJeudi.ToString("0") + "',";
                this.strCommande += " `isVendredi`='" + validationReduction.IsVendredi.ToString("0") + "',`isSamedi`='" + validationReduction.IsSamedi.ToString("0") + "',";
                this.strCommande += " `isDimanche`='" + validationReduction.IsDimanche.ToString("0") + "',`matriculeAgent`='" + validationReduction.MatriculeAgent + "',";
                this.strCommande += " `matriculeAgentControleur`=" + matriculeAgentControleur + ",`isValider`='" + validationReduction.IsValider.ToString("0") + "'";
                this.strCommande += " WHERE `numUSValidationReduction`='" + validationReduction.NumUSValidationReduction + "'";

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

        string IntfDalUSValidationReduction.isUSValidationReduction(crlUSValidationReduction validationReduction)
        {
            #region declaration
            string numUSValidationReduction = "";
            #endregion

            #region implementation
            #endregion

            return numUSValidationReduction;
        }

        crlUSValidationReduction IntfDalUSValidationReduction.selectUSValidationReduction(string numUSValidationReduction)
        {
            #region declaration
            crlUSValidationReduction validationReduction = null;
            #endregion

            #region implementation
            if (numUSValidationReduction != "")
            {
                this.strCommande = "SELECT * FROM `usvalidationreduction` WHERE `numUSValidationReduction`='" + numUSValidationReduction + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            validationReduction = new crlUSValidationReduction();
                            try
                            {
                                validationReduction.DateValidationReduction = Convert.ToDateTime(this.reader["dateValidationReduction"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                validationReduction.IsDimanche = int.Parse(this.reader["isDimanche"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                validationReduction.IsJeudi = int.Parse(this.reader["isJeudi"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                validationReduction.IsLundi = int.Parse(this.reader["isLundi"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                validationReduction.IsMardi = int.Parse(this.reader["isMardi"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                validationReduction.IsMercredi = int.Parse(this.reader["isMercredi"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                validationReduction.IsSamedi = int.Parse(this.reader["isSamedi"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                validationReduction.IsVendredi = int.Parse(this.reader["isVendredi"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            
                            validationReduction.MatriculeAgent = this.reader["matriculeAgent"].ToString();
                            validationReduction.MatriculeAgentControleur = this.reader["matriculeAgentControleur"].ToString();
                            validationReduction.NumCarte = this.reader["numCarte"].ToString();
                            validationReduction.NumUSReductionParticulier = this.reader["numUSReductionParticulier"].ToString();
                            validationReduction.NumUSValidationReduction = this.reader["numUSValidationReduction"].ToString();
                            try
                            {
                                validationReduction.ValideAu = Convert.ToDateTime(this.reader["valideAu"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                validationReduction.ValideDu = Convert.ToDateTime(this.reader["valideDu"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                validationReduction.IsValider = int.Parse(this.reader["isValider"].ToString());
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

            return validationReduction;
        }

        crlUSValidationReduction IntfDalUSValidationReduction.selectUSValidationReductionCarte(string numCarte)
        {
            #region declaration
            crlUSValidationReduction validationReduction = null;
            #endregion

            #region implementation
            if (numCarte != "")
            {
                this.strCommande = "SELECT * FROM `usvalidationreduction` WHERE `numCarte`='" + numCarte + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            validationReduction = new crlUSValidationReduction();
                            try
                            {
                                validationReduction.DateValidationReduction = Convert.ToDateTime(this.reader["dateValidationReduction"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                validationReduction.IsDimanche = int.Parse(this.reader["isDimanche"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                validationReduction.IsJeudi = int.Parse(this.reader["isJeudi"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                validationReduction.IsLundi = int.Parse(this.reader["isLundi"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                validationReduction.IsMardi = int.Parse(this.reader["isMardi"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                validationReduction.IsMercredi = int.Parse(this.reader["isMercredi"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                validationReduction.IsSamedi = int.Parse(this.reader["isSamedi"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                validationReduction.IsVendredi = int.Parse(this.reader["isVendredi"].ToString());
                            }
                            catch (Exception)
                            {
                            }

                            validationReduction.MatriculeAgent = this.reader["matriculeAgent"].ToString();
                            validationReduction.MatriculeAgentControleur = this.reader["matriculeAgentControleur"].ToString();
                            validationReduction.NumCarte = this.reader["numCarte"].ToString();
                            validationReduction.NumUSReductionParticulier = this.reader["numUSReductionParticulier"].ToString();
                            validationReduction.NumUSValidationReduction = this.reader["numUSValidationReduction"].ToString();
                            try
                            {
                                validationReduction.ValideAu = Convert.ToDateTime(this.reader["valideAu"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                validationReduction.ValideDu = Convert.ToDateTime(this.reader["valideDu"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                validationReduction.IsValider = int.Parse(this.reader["isValider"].ToString());
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

            return validationReduction;
        }

        string IntfDalUSValidationReduction.getNumUSValidationReduction(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numUSValidationReduction = "00001";
            string[] tempNumUSValidationReduction = null;
            string strDate = "VR" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT usvalidationreduction.numUSValidationReduction AS maxNum FROM usvalidationreduction";
            this.strCommande += " WHERE usvalidationreduction.numUSValidationReduction LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumUSValidationReduction = reader["maxNum"].ToString().ToString().Split('/');
                        numUSValidationReduction = tempNumUSValidationReduction[tempNumUSValidationReduction.Length - 1];
                    }
                    numTemp = double.Parse(numUSValidationReduction) + 1;
                    if (numTemp < 10)
                        numUSValidationReduction = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numUSValidationReduction = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numUSValidationReduction = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numUSValidationReduction = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numUSValidationReduction = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numUSValidationReduction = strDate + "/" + sigleAgence + "/" + numUSValidationReduction;
            #endregion

            return numUSValidationReduction;
        }

        string IntfDalUSValidationReduction.isEncourValidation(crlUSValidationReduction validationReduction)
        {
            #region declaration
            string numUSValidationReduction = "";
            #endregion

            #region implementation
            if (validationReduction != null)
            {
                this.strCommande = "SELECT usvalidationreduction.numUSValidationReduction FROM usvalidationreduction";
                this.strCommande += " WHERE usvalidationreduction.numUSReductionParticulier = '" + validationReduction.NumUSReductionParticulier + "' AND";
                this.strCommande += " usvalidationreduction.isValider = '-1' AND";
                this.strCommande += " usvalidationreduction.numUSValidationReduction <> '" + validationReduction.NumUSValidationReduction + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            numUSValidationReduction = this.reader["numUSValidationReduction"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numUSValidationReduction;
        }

        string IntfDalUSValidationReduction.isEncourUtilisation(crlUSValidationReduction validationReduction)
        {
            #region declaration
            string numUSValidationReduction = "";
            #endregion

            #region implementation
            if (validationReduction != null)
            {
                this.strCommande = "SELECT usvalidationreduction.numUSValidationReduction FROM usvalidationreduction";
                this.strCommande += " WHERE usvalidationreduction.numUSReductionParticulier = '" + validationReduction.NumUSReductionParticulier + "' AND";
                this.strCommande += " usvalidationreduction.valideAu > '" + validationReduction.ValideDu.ToString("yyyy-MM-dd") + "' AND";
                this.strCommande += " usvalidationreduction.numUSValidationReduction <> '" + validationReduction.NumUSValidationReduction + "' AND";
                this.strCommande += " usvalidationreduction.isValider > 0";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            numUSValidationReduction = this.reader["numUSValidationReduction"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numUSValidationReduction;
        }


        void IntfDalUSValidationReduction.insertToGridUSValidationReduction(GridView gridView, string param, string paramLike, string valueLike, string numUSReductionParticulier)
        {
            #region declaration
            IntfDalUSValidationReduction serviceUSValidationReduction = new ImplDalUSValidationReduction();
            #endregion

            #region implementation

            this.strCommande = "SELECT usvalidationreduction.numUSValidationReduction, usvalidationreduction.dateValidationReduction,";
            this.strCommande += " usvalidationreduction.numUSReductionParticulier, usvalidationreduction.numCarte,";
            this.strCommande += " usvalidationreduction.valideDu, usvalidationreduction.valideAu,";
            this.strCommande += " usvalidationreduction.isLundi, usvalidationreduction.isMardi, usvalidationreduction.isMercredi,";
            this.strCommande += " usvalidationreduction.isJeudi, usvalidationreduction.isVendredi, usvalidationreduction.isSamedi,";
            this.strCommande += " usvalidationreduction.isDimanche, usvalidationreduction.matriculeAgentControleur,";
            this.strCommande += " usvalidationreduction.matriculeAgent FROM usvalidationreduction";
            this.strCommande += " WHERE " + paramLike + " LIKE  '%" + valueLike + "%' AND";
            this.strCommande += " usvalidationreduction.numUSReductionParticulier LIKE '%" + numUSReductionParticulier + "%'";
            this.strCommande += " ORDER BY " + param;


            gridView.DataSource = serviceUSValidationReduction.getDataTableUSValidationReduction(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalUSValidationReduction.getDataTableUSValidationReduction(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numUSValidationReduction", typeof(string));
            dataTable.Columns.Add("dateValidationReduction", typeof(DateTime));
            dataTable.Columns.Add("valideDu", typeof(DateTime));
            dataTable.Columns.Add("valideAu", typeof(DateTime));
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

                        dr["numUSValidationReduction"] = reader["numUSValidationReduction"].ToString();
                        try
                        {
                            dr["dateValidationReduction"] = Convert.ToDateTime(reader["dateValidationReduction"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            dr["valideDu"] = Convert.ToDateTime(reader["valideDu"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            dr["valideAu"] = Convert.ToDateTime(reader["valideAu"].ToString());
                        }
                        catch (Exception)
                        {
                        }

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }

        void IntfDalUSValidationReduction.loadDdlValidationReductionValide(DropDownList ddl, string numUSReductionParticulier, DateTime dateNow)
        {
            #region declaration
            string strDate = "";
            #endregion

            #region implementation
            if (ddl != null)
            {
                ddl.Items.Clear();
                ddl.Items.Add("");

                this.strCommande = "SELECT usvalidationreduction.valideDu, usvalidationreduction.valideAu,";
                this.strCommande += " usvalidationreduction.numUSValidationReduction FROM usvalidationreduction";
                this.strCommande += " WHERE usvalidationreduction.valideAu >= '" + dateNow.ToString("yyyy-MM-dd") + "' AND";
                this.strCommande += " usvalidationreduction.numUSReductionParticulier = '" + numUSReductionParticulier + "' AND";
                this.strCommande += " usvalidationreduction.isValider > 0";
                this.strCommande += " ORDER BY usvalidationreduction.dateValidationReduction DESC";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            try
                            {
                                strDate = " du " + Convert.ToDateTime(this.reader["valideDu"].ToString()).ToString("dd MMMM yyyy");
                                strDate += " au " + Convert.ToDateTime(this.reader["valideAu"].ToString()).ToString("dd MMMM yyyy");
                            }
                            catch (Exception)
                            {
                            }
                            ddl.Items.Add(new ListItem(strDate, this.reader["numUSValidationReduction"].ToString()));
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }

        void IntfDalUSValidationReduction.loadDdlValidationReductionValideNonCarte(DropDownList ddl, string numUSReductionParticulier, DateTime dateNow)
        {
            #region declaration
            string strDate = "";
            #endregion

            #region implementation
            if (ddl != null)
            {
                ddl.Items.Clear();
                ddl.Items.Add("");

                this.strCommande = "SELECT usvalidationreduction.valideDu, usvalidationreduction.valideAu,";
                this.strCommande += " usvalidationreduction.numUSValidationReduction FROM usvalidationreduction";
                this.strCommande += " Left Join uscarte ON uscarte.numCarte = usvalidationreduction.numCarte";
                this.strCommande += " WHERE usvalidationreduction.valideAu >= '" + dateNow.ToString("yyyy-MM-dd") + "' AND";
                this.strCommande += " usvalidationreduction.numUSReductionParticulier = '" + numUSReductionParticulier + "' AND";
                this.strCommande += " uscarte.numUSValidationReduction IS NULL AND";
                this.strCommande += " usvalidationreduction.isValider > 0";
                this.strCommande += " ORDER BY usvalidationreduction.dateValidationReduction DESC";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            try
                            {
                                strDate = " du " + Convert.ToDateTime(this.reader["valideDu"].ToString()).ToString("dd MMMM yyyy");
                                strDate += " au " + Convert.ToDateTime(this.reader["valideAu"].ToString()).ToString("dd MMMM yyyy");
                            }
                            catch (Exception)
                            {
                            }
                            ddl.Items.Add(new ListItem(strDate, this.reader["numUSValidationReduction"].ToString()));
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }
        #endregion


        void IntfDalUSValidationReduction.insertToGridUSValidationReductionEncourValidation(GridView gridView, string param, string paramLike, string valueLike, string numUSReductionParticulier)
        {
            #region declaration
            IntfDalUSValidationReduction serviceUSValidationReduction = new ImplDalUSValidationReduction();
            #endregion

            #region implementation

            this.strCommande = "SELECT usvalidationreduction.numUSValidationReduction, usvalidationreduction.dateValidationReduction,";
            this.strCommande += " usvalidationreduction.numUSReductionParticulier, usvalidationreduction.numCarte,";
            this.strCommande += " usvalidationreduction.valideDu, usvalidationreduction.valideAu,";
            this.strCommande += " usvalidationreduction.isLundi, usvalidationreduction.isMardi, usvalidationreduction.isMercredi,";
            this.strCommande += " usvalidationreduction.isJeudi, usvalidationreduction.isVendredi, usvalidationreduction.isSamedi,";
            this.strCommande += " usvalidationreduction.isDimanche, usvalidationreduction.matriculeAgentControleur,";
            this.strCommande += " usvalidationreduction.matriculeAgent FROM usvalidationreduction";
            this.strCommande += " WHERE " + paramLike + " LIKE  '%" + valueLike + "%' AND";
            this.strCommande += " usvalidationreduction.isValider = '-1' AND";
            this.strCommande += " usvalidationreduction.numUSReductionParticulier LIKE '%" + numUSReductionParticulier + "%'";
            this.strCommande += " ORDER BY " + param;


            gridView.DataSource = serviceUSValidationReduction.getDataTableUSValidationReductionEncourValidation(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalUSValidationReduction.getDataTableUSValidationReductionEncourValidation(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numUSValidationReduction", typeof(string));
            dataTable.Columns.Add("dateValidationReduction", typeof(DateTime));
            dataTable.Columns.Add("valideDu", typeof(DateTime));
            dataTable.Columns.Add("valideAu", typeof(DateTime));
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

                        dr["numUSValidationReduction"] = reader["numUSValidationReduction"].ToString();
                        try
                        {
                            dr["dateValidationReduction"] = Convert.ToDateTime(reader["dateValidationReduction"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            dr["valideDu"] = Convert.ToDateTime(reader["valideDu"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            dr["valideAu"] = Convert.ToDateTime(reader["valideAu"].ToString());
                        }
                        catch (Exception)
                        {
                        }

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }

        void IntfDalUSValidationReduction.insertToGridUSValidationReductionEncourUtilisation(GridView gridView, string param, string paramLike, string valueLike, string numUSReductionParticulier)
        {
            #region declaration
            IntfDalUSValidationReduction serviceUSValidationReduction = new ImplDalUSValidationReduction();
            #endregion

            #region implementation

            this.strCommande = "SELECT usvalidationreduction.numUSValidationReduction, usvalidationreduction.dateValidationReduction,";
            this.strCommande += " usvalidationreduction.numUSReductionParticulier, usvalidationreduction.numCarte,";
            this.strCommande += " usvalidationreduction.valideDu, usvalidationreduction.valideAu,";
            this.strCommande += " usvalidationreduction.isLundi, usvalidationreduction.isMardi, usvalidationreduction.isMercredi,";
            this.strCommande += " usvalidationreduction.isJeudi, usvalidationreduction.isVendredi, usvalidationreduction.isSamedi,";
            this.strCommande += " usvalidationreduction.isDimanche, usvalidationreduction.matriculeAgentControleur,";
            this.strCommande += " usvalidationreduction.matriculeAgent FROM usvalidationreduction";
            this.strCommande += " WHERE " + paramLike + " LIKE  '%" + valueLike + "%' AND";
            this.strCommande += " usvalidationreduction.isValider =  '1' AND";
            this.strCommande += " usvalidationreduction.valideAu >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND";
            this.strCommande += " usvalidationreduction.numUSReductionParticulier LIKE '%" + numUSReductionParticulier + "%'";
            this.strCommande += " ORDER BY " + param;


            gridView.DataSource = serviceUSValidationReduction.getDataTableUSValidationReductionEncourUtilisation(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalUSValidationReduction.getDataTableUSValidationReductionEncourUtilisation(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numUSValidationReduction", typeof(string));
            dataTable.Columns.Add("dateValidationReduction", typeof(DateTime));
            dataTable.Columns.Add("valideDu", typeof(DateTime));
            dataTable.Columns.Add("valideAu", typeof(DateTime));
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

                        dr["numUSValidationReduction"] = reader["numUSValidationReduction"].ToString();
                        try
                        {
                            dr["dateValidationReduction"] = Convert.ToDateTime(reader["dateValidationReduction"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            dr["valideDu"] = Convert.ToDateTime(reader["valideDu"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            dr["valideAu"] = Convert.ToDateTime(reader["valideAu"].ToString());
                        }
                        catch (Exception)
                        {
                        }

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }

        void IntfDalUSValidationReduction.insertToGridUSValidationReductionPerime(GridView gridView, string param, string paramLike, string valueLike, string numUSReductionParticulier)
        {
            #region declaration
            IntfDalUSValidationReduction serviceUSValidationReduction = new ImplDalUSValidationReduction();
            #endregion

            #region implementation

            this.strCommande = "SELECT usvalidationreduction.numUSValidationReduction, usvalidationreduction.dateValidationReduction,";
            this.strCommande += " usvalidationreduction.numUSReductionParticulier, usvalidationreduction.numCarte,";
            this.strCommande += " usvalidationreduction.valideDu, usvalidationreduction.valideAu,";
            this.strCommande += " usvalidationreduction.isLundi, usvalidationreduction.isMardi, usvalidationreduction.isMercredi,";
            this.strCommande += " usvalidationreduction.isJeudi, usvalidationreduction.isVendredi, usvalidationreduction.isSamedi,";
            this.strCommande += " usvalidationreduction.isDimanche, usvalidationreduction.matriculeAgentControleur,";
            this.strCommande += " usvalidationreduction.matriculeAgent FROM usvalidationreduction";
            this.strCommande += " WHERE " + paramLike + " LIKE  '%" + valueLike + "%' AND";
            this.strCommande += " usvalidationreduction.isValider =  '1' AND";
            this.strCommande += " usvalidationreduction.valideAu < '" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND";
            this.strCommande += " usvalidationreduction.numUSReductionParticulier LIKE '%" + numUSReductionParticulier + "%'";
            this.strCommande += " ORDER BY " + param;


            gridView.DataSource = serviceUSValidationReduction.getDataTableUSValidationReductionPerime(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalUSValidationReduction.getDataTableUSValidationReductionPerime(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numUSValidationReduction", typeof(string));
            dataTable.Columns.Add("dateValidationReduction", typeof(DateTime));
            dataTable.Columns.Add("valideDu", typeof(DateTime));
            dataTable.Columns.Add("valideAu", typeof(DateTime));
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

                        dr["numUSValidationReduction"] = reader["numUSValidationReduction"].ToString();
                        try
                        {
                            dr["dateValidationReduction"] = Convert.ToDateTime(reader["dateValidationReduction"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            dr["valideDu"] = Convert.ToDateTime(reader["valideDu"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            dr["valideAu"] = Convert.ToDateTime(reader["valideAu"].ToString());
                        }
                        catch (Exception)
                        {
                        }

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }

        void IntfDalUSValidationReduction.insertToGridUSValidationReductionNonValider(GridView gridView, string param, string paramLike, string valueLike, string numUSReductionParticulier)
        {
            #region declaration
            IntfDalUSValidationReduction serviceUSValidationReduction = new ImplDalUSValidationReduction();
            #endregion

            #region implementation

            this.strCommande = "SELECT usvalidationreduction.numUSValidationReduction, usvalidationreduction.dateValidationReduction,";
            this.strCommande += " usvalidationreduction.numUSReductionParticulier, usvalidationreduction.numCarte,";
            this.strCommande += " usvalidationreduction.valideDu, usvalidationreduction.valideAu,";
            this.strCommande += " usvalidationreduction.isLundi, usvalidationreduction.isMardi, usvalidationreduction.isMercredi,";
            this.strCommande += " usvalidationreduction.isJeudi, usvalidationreduction.isVendredi, usvalidationreduction.isSamedi,";
            this.strCommande += " usvalidationreduction.isDimanche, usvalidationreduction.matriculeAgentControleur,";
            this.strCommande += " usvalidationreduction.matriculeAgent FROM usvalidationreduction";
            this.strCommande += " WHERE " + paramLike + " LIKE  '%" + valueLike + "%' AND";
            this.strCommande += " usvalidationreduction.isValider = '0' AND";
            this.strCommande += " usvalidationreduction.numUSReductionParticulier LIKE '%" + numUSReductionParticulier + "%'";
            this.strCommande += " ORDER BY " + param;


            gridView.DataSource = serviceUSValidationReduction.getDataTableUSValidationReductionNonValider(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalUSValidationReduction.getDataTableUSValidationReductionNonValider(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numUSValidationReduction", typeof(string));
            dataTable.Columns.Add("dateValidationReduction", typeof(DateTime));
            dataTable.Columns.Add("valideDu", typeof(DateTime));
            dataTable.Columns.Add("valideAu", typeof(DateTime));
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

                        dr["numUSValidationReduction"] = reader["numUSValidationReduction"].ToString();
                        try
                        {
                            dr["dateValidationReduction"] = Convert.ToDateTime(reader["dateValidationReduction"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            dr["valideDu"] = Convert.ToDateTime(reader["valideDu"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            dr["valideAu"] = Convert.ToDateTime(reader["valideAu"].ToString());
                        }
                        catch (Exception)
                        {
                        }

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }





        void IntfDalUSValidationReduction.insertToGridUSValidationReductionAll(GridView gridView, string param, string paramLike, string valueLike, string numAgence)
        {
            #region declaration
            IntfDalUSValidationReduction serviceUSValidationReduction = new ImplDalUSValidationReduction();
            #endregion

            #region implementation

            this.strCommande = "SELECT usvalidationreduction.numUSValidationReduction, usvalidationreduction.dateValidationReduction,";
            this.strCommande += " usvalidationreduction.numUSReductionParticulier, usvalidationreduction.numCarte, usvalidationreduction.valideDu,";
            this.strCommande += " usvalidationreduction.valideAu, usvalidationreduction.isLundi, usvalidationreduction.isMardi, usvalidationreduction.isMercredi,";
            this.strCommande += " usvalidationreduction.isJeudi, usvalidationreduction.isVendredi, usvalidationreduction.isSamedi, usvalidationreduction.isDimanche,";
            this.strCommande += " usvalidationreduction.matriculeAgent, usvalidationreduction.matriculeAgentControleur, usvalidationreduction.isValider,";
            this.strCommande += " usreductionparticulier.numUSReductionParticulier, usreductionparticulier.numEtablissementScolaire, usreductionparticulier.numSociete,";
            this.strCommande += " usreductionparticulier.numCategorieBillet, usreductionparticulier.imageReductionParticulier, usreductionparticulier.numIndividu,";
            this.strCommande += " individu.numIndividu, individu.civiliteIndividu, individu.nomIndividu, individu.prenomIndividu, individu.cinIndividu, individu.adresse,";
            this.strCommande += " individu.profession, individu.telephoneFixeIndividu, individu.telephoneMobileIndividu, individu.dateNaissanceIndividu,";
            this.strCommande += " individu.lieuNaissanceIndividu, etablissementscolaire.etablissementScolaire, societe.nomSociete,";
            this.strCommande += " uscategoriebillet.categorieBillet FROM usvalidationreduction";
            this.strCommande += " Inner Join usreductionparticulier ON usreductionparticulier.numUSReductionParticulier = usvalidationreduction.numUSReductionParticulier";
            this.strCommande += " Inner Join individu ON individu.numIndividu = usreductionparticulier.numIndividu";
            this.strCommande += " Inner Join agent ON agent.matriculeAgent = usvalidationreduction.matriculeAgent";
            this.strCommande += " Left Join etablissementscolaire ON etablissementscolaire.numEtablissementScolaire = usreductionparticulier.numEtablissementScolaire";
            this.strCommande += " Left Join societe ON societe.numSociete = usreductionparticulier.numSociete";
            this.strCommande += " Inner Join uscategoriebillet ON uscategoriebillet.numCategorieBillet = usreductionparticulier.numCategorieBillet";
            this.strCommande += " WHERE " + paramLike + " LIKE  '%" + valueLike + "%' AND";
            this.strCommande += " usvalidationreduction.isValider = '-1' AND";
            this.strCommande += " agent.numAgence LIKE  '%" + numAgence + "%'";
            this.strCommande += " ORDER BY " + param;


            gridView.DataSource = serviceUSValidationReduction.getDataTableUSValidationReductionAll(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalUSValidationReduction.getDataTableUSValidationReductionAll(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numUSValidationReduction", typeof(string));
            dataTable.Columns.Add("numUSReductionParticulier", typeof(string));
            dataTable.Columns.Add("individu", typeof(string));
            dataTable.Columns.Add("categorie", typeof(string));
            dataTable.Columns.Add("dateValidationReduction", typeof(DateTime));
            dataTable.Columns.Add("valideDu", typeof(DateTime));
            dataTable.Columns.Add("valideAu", typeof(DateTime));
            dataTable.Columns.Add("etablissement", typeof(string));
            dataTable.Columns.Add("societe", typeof(string));
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

                        dr["numUSValidationReduction"] = reader["numUSValidationReduction"].ToString();
                        dr["numUSReductionParticulier"] = reader["numUSReductionParticulier"].ToString();
                        dr["individu"] = reader["nomIndividu"].ToString() + " " + reader["prenomIndividu"].ToString();
                        dr["categorie"] = reader["categorieBillet"].ToString();
                        try
                        {
                            dr["dateValidationReduction"] = Convert.ToDateTime(reader["dateValidationReduction"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            dr["valideDu"] = Convert.ToDateTime(reader["valideDu"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            dr["valideAu"] = Convert.ToDateTime(reader["valideAu"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        dr["etablissement"] = reader["etablissementScolaire"].ToString();
                        dr["societe"] = reader["nomSociete"].ToString();

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