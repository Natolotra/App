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
    /// Summary description for ImplDalDureeAbonnement
    /// </summary>
    public class ImplDalDureeAbonnement : IntfDalDureeAbonnement
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalDureeAbonnement()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalDureeAbonnement(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region IntfDalDureeAbonnement Members

        crlDureeAbonnement IntfDalDureeAbonnement.selectDureeAbonnement(string numDureeAbonnement)
        {
            #region declaration
            crlDureeAbonnement dureeAbonnement = null;

            IntfDalAgent serviceAgent = new ImplDalAgent();
            IntfDalTrajet serviceTrajet = new ImplDalTrajet();
            IntfDalZone serviceZone = new ImplDalZone();
            IntfDalAbonnement serviceAbonnement = new ImplDalAbonnement();
            IntfDalCalculCategorieBillet serviceCalculCategorieBillet = new ImplDalCalculCategorieBillet();
            IntfDalCalculReductionBillet serviceCalculReductionBillet = new ImplDalCalculReductionBillet();
            #endregion

            #region implementation
            if (numDureeAbonnement != "")
            {
                this.strCommande = "SELECT * FROM `dureeabonnement` WHERE (`numDureeAbonnement`='" + numDureeAbonnement + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            dureeAbonnement = new crlDureeAbonnement();
                            try
                            {
                                dureeAbonnement.DateDureeAbonnement = Convert.ToDateTime(this.reader["dateDureeAbonnement"].ToString());
                            }
                            catch (Exception) { }
                            dureeAbonnement.MatriculeAgent = this.reader["matriculeAgent"].ToString();
                            dureeAbonnement.NumAbonnement = this.reader["numAbonnement"].ToString();
                            dureeAbonnement.NumDureeAbonnement = this.reader["numDureeAbonnement"].ToString();
                            dureeAbonnement.NumTrajet = this.reader["numTrajet"].ToString();
                            try
                            {
                                dureeAbonnement.PrixTotal = double.Parse(this.reader["prixTotal"].ToString());
                            }
                            catch (Exception) { }
                            try
                            {
                                dureeAbonnement.PrixUnitaire = double.Parse(this.reader["prixUnitaire"].ToString());
                            }
                            catch (Exception) { }
                            try
                            {
                                dureeAbonnement.ValideAu = Convert.ToDateTime(this.reader["valideAu"].ToString());
                            }
                            catch (Exception) { }
                            try
                            {
                                dureeAbonnement.ValideDu = Convert.ToDateTime(this.reader["valideDu"].ToString());
                            }
                            catch (Exception) { }
                            dureeAbonnement.Zone = this.reader["zone"].ToString();
                            dureeAbonnement.NumCalculCategorieBillet = this.reader["numCalculCategorieBillet"].ToString();
                            dureeAbonnement.NumCalculReductionBillet = this.reader["numCalculReductionBillet"].ToString();
                            dureeAbonnement.ModePaiement = this.reader["modePaiement"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                if (dureeAbonnement != null)
                {
                    if (dureeAbonnement.MatriculeAgent != "")
                    {
                        dureeAbonnement.agent = serviceAgent.selectAgent(dureeAbonnement.MatriculeAgent);
                    }
                    if (dureeAbonnement.NumAbonnement != "")
                    {
                        dureeAbonnement.abonnement = serviceAbonnement.selectAbonnement(dureeAbonnement.NumAbonnement);
                    }
                    if (dureeAbonnement.NumTrajet != "")
                    {
                        dureeAbonnement.trajet = serviceTrajet.selectTrajet(dureeAbonnement.NumTrajet);
                    }
                    if (dureeAbonnement.Zone != "")
                    {
                        dureeAbonnement.zoneObj = serviceZone.selectZone(dureeAbonnement.Zone);
                    }
                    if (dureeAbonnement.NumCalculCategorieBillet != "")
                    {
                        dureeAbonnement.calculCategorieBillet = serviceCalculCategorieBillet.selectCalculCategorieBillet(dureeAbonnement.NumCalculCategorieBillet);
                    }
                    if (dureeAbonnement.NumCalculReductionBillet != "")
                    {
                        dureeAbonnement.calculReductionBillet = serviceCalculReductionBillet.selectCalculReductionBillet(dureeAbonnement.NumCalculReductionBillet);
                    }
                }
            }
            #endregion

            return dureeAbonnement;
        }

        string IntfDalDureeAbonnement.insertDureeAbonnement(crlDureeAbonnement dureeAbonnement)
        {
            #region declaration
            string numDureeAbonnement = "";
            int nombreInsert = 0;
            IntfDalDureeAbonnement serviceDureeAbonnement = new ImplDalDureeAbonnement();
            string strTrajet = "";
            string numCalculCategorieBillet = "NULL";
            string numCalculReductionBillet = "NULL";
            string modePaiement = "NULL";
            #endregion

            #region implementation
            if (dureeAbonnement != null)
            {
                if (dureeAbonnement.agent != null)
                {
                    if (dureeAbonnement.NumTrajet != "")
                    {
                        strTrajet = "'" + dureeAbonnement.NumTrajet + "'";
                    }
                    else
                    {
                        strTrajet = "NULL";
                    }
                    if (dureeAbonnement.NumCalculCategorieBillet != "")
                    {
                        numCalculCategorieBillet = "'" + dureeAbonnement.NumCalculCategorieBillet + "'";
                    }
                    if (dureeAbonnement.NumCalculReductionBillet != "")
                    {
                        numCalculReductionBillet = "'" + dureeAbonnement.NumCalculReductionBillet + "'";
                    }
                    if (dureeAbonnement.ModePaiement != "")
                    {
                        modePaiement = "'" + dureeAbonnement.ModePaiement + "'";
                    }

                    dureeAbonnement.NumDureeAbonnement = serviceDureeAbonnement.getNumDureeAbonnement(dureeAbonnement.agent.agence.SigleAgence);
                    this.strCommande = "INSERT INTO `dureeabonnement` (`numDureeAbonnement`,`numTrajet`,`zone`,`prixUnitaire`,";
                    this.strCommande += " `numAbonnement`,`valideDu`,`valideAu`,`prixTotal`,`matriculeAgent`,`dateDureeAbonnement`,";
                    this.strCommande += " `numCalculCategorieBillet`,`numCalculReductionBillet`,`modePaiement`)";
                    this.strCommande += " VALUES ('" + dureeAbonnement.NumDureeAbonnement + "'," + strTrajet + ",";
                    this.strCommande += " '" + dureeAbonnement.Zone + "','" + dureeAbonnement.PrixUnitaire + "',";
                    this.strCommande += " '" + dureeAbonnement.NumAbonnement + "','" + dureeAbonnement.ValideDu.ToString("yyyy-MM-dd") + "',";
                    this.strCommande += " '" + dureeAbonnement.ValideAu.ToString("yyyy-MM-dd") + "','" + dureeAbonnement.PrixTotal + "',";
                    this.strCommande += " '" + dureeAbonnement.MatriculeAgent + "','" + dureeAbonnement.DateDureeAbonnement.ToString("yyyy-MM-dd") + "',";
                    this.strCommande += " " + numCalculCategorieBillet + "," + numCalculReductionBillet + "," + modePaiement + ")";

                    this.serviceConnectBase.openConnection();
                    nombreInsert = this.serviceConnectBase.requete(this.strCommande);
                    if (nombreInsert == 1)
                    {
                        numDureeAbonnement = dureeAbonnement.NumDureeAbonnement;
                    }
                    this.serviceConnectBase.closeConnection();
                }
            }
            #endregion

            return numDureeAbonnement;
        }

        bool IntfDalDureeAbonnement.updateDureeAbonnement(crlDureeAbonnement dureeAbonnement)
        {
            #region declaration
            bool isUpdate = false;
            string strTrajet = "";
            int nombreUpdate = 0;
            string numCalculCategorieBillet = "NULL";
            string numCalculReductionBillet = "NULL";
            string modePaiement = "NULL";
            #endregion

            #region implementation
            if (dureeAbonnement != null)
            {
                if (dureeAbonnement.NumTrajet != "")
                {
                    strTrajet = "'" + dureeAbonnement.NumTrajet + "'";
                }
                else
                {
                    strTrajet = "NULL";
                }
                if (dureeAbonnement.NumCalculCategorieBillet != "")
                {
                    numCalculCategorieBillet = "'" + dureeAbonnement.NumCalculCategorieBillet + "'";
                }
                if (dureeAbonnement.NumCalculReductionBillet != "")
                {
                    numCalculReductionBillet = "'" + dureeAbonnement.NumCalculReductionBillet + "'";
                }
                if (dureeAbonnement.ModePaiement != "")
                {
                    modePaiement = "'" + dureeAbonnement.ModePaiement + "'";
                }

                this.strCommande = "UPDATE `dureeabonnement` SET `dateDureeAbonnement`='" + dureeAbonnement.DateDureeAbonnement.ToString("yyyy-MM-dd") + "',";
                this.strCommande += " `matriculeAgent`='" + dureeAbonnement.MatriculeAgent + "',`numAbonnement`='" + dureeAbonnement.NumAbonnement + "',";
                this.strCommande += " `numTrajet`=" + strTrajet + ",`prixTotal`='" + dureeAbonnement.PrixTotal + "',";
                this.strCommande += " `prixUnitaire`='" + dureeAbonnement.PrixUnitaire + "',`valideAu`='" + dureeAbonnement.ValideAu.ToString("yyyy-MM-dd") + "',";
                this.strCommande += " `valideDu`='" + dureeAbonnement.ValideDu.ToString("yyyy-MM-dd") + "',`zone`='" + dureeAbonnement.Zone + "',";
                this.strCommande += " `numCalculCategorieBillet`=" + numCalculCategorieBillet + ", `numCalculReductionBillet`=" + numCalculReductionBillet + ",";
                this.strCommande += " `modePaiement`= " + modePaiement;
                this.strCommande += " WHERE `numDureeAbonnement`='" + dureeAbonnement.NumDureeAbonnement + "'";

                this.serviceConnectBase.openConnection();
                nombreUpdate = this.serviceConnectBase.requete(this.strCommande);
                if (nombreUpdate == 1)
                {
                    isUpdate = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isUpdate;
        }

        string IntfDalDureeAbonnement.getNumDureeAbonnement(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numDureeAbonnement = "00001";
            string[] tempNumDureeAbonnement = null;
            string strDate = "DA" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT dureeabonnement.numDureeAbonnement AS maxNum FROM dureeabonnement";
            this.strCommande += " WHERE dureeabonnement.numDureeAbonnement LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumDureeAbonnement = reader["maxNum"].ToString().ToString().Split('/');
                        numDureeAbonnement = tempNumDureeAbonnement[tempNumDureeAbonnement.Length - 1];
                    }
                    numTemp = double.Parse(numDureeAbonnement) + 1;
                    if (numTemp < 10)
                        numDureeAbonnement = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numDureeAbonnement = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numDureeAbonnement = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numDureeAbonnement = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numDureeAbonnement = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numDureeAbonnement = strDate + "/" + sigleAgence + "/" + numDureeAbonnement;
            #endregion

            return numDureeAbonnement;
        }

        #endregion


        #region insert to grid
        void IntfDalDureeAbonnement.insertToGridDureeAbonnementNonRecu(GridView gridView, string param, string paramLike, string valueLike, string numAbonnement)
        {
            #region declaration
            IntfDalDureeAbonnement serviceDureeAbonnement = new ImplDalDureeAbonnement();
            #endregion

            #region implementation

            this.strCommande = "SELECT dureeabonnement.numDureeAbonnement, dureeabonnement.numTrajet, dureeabonnement.zone,";
            this.strCommande += " dureeabonnement.prixUnitaire, dureeabonnement.numAbonnement, dureeabonnement.valideDu, dureeabonnement.valideAu,";
            this.strCommande += " dureeabonnement.prixTotal, dureeabonnement.matriculeAgent, dureeabonnement.dateDureeAbonnement FROM dureeabonnement";
            this.strCommande += " Left Join recuabonnement ON recuabonnement.numDureeAbonnement = dureeabonnement.numDureeAbonnement";
            this.strCommande += " Left Join bondecommande ON bondecommande.numDureeAbonnement = dureeabonnement.numDureeAbonnement";
            this.strCommande += " WHERE recuabonnement.numDureeAbonnement IS NULL  AND";
            this.strCommande += " bondecommande.numDureeAbonnement IS NULL  AND";
            this.strCommande += " dureeabonnement.numAbonnement = '" + numAbonnement + "' AND";
            this.strCommande += " " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceDureeAbonnement.getDataTableDureeAbonnementNonRecu(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalDureeAbonnement.getDataTableDureeAbonnementNonRecu(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            IntfDalTrajet serviceTrajet = new ImplDalTrajet();
            crlTrajet trajet = null;
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numDureeAbonnement", typeof(string));
            dataTable.Columns.Add("zone", typeof(string));
            dataTable.Columns.Add("valideDu", typeof(DateTime));
            dataTable.Columns.Add("valideAu", typeof(DateTime));
            dataTable.Columns.Add("prixTotal", typeof(string));
            dataTable.Columns.Add("trajet", typeof(string));
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

                        dr["numDureeAbonnement"] = reader["numDureeAbonnement"].ToString();
                        dr["zone"] = reader["zone"].ToString();
                        try
                        {
                            dr["valideDu"] = Convert.ToDateTime(reader["valideDu"].ToString());
                        }
                        catch (Exception)
                        {
                        }

                        try
                        {
                            dr["valideAu"] = Convert.ToDateTime(this.reader["valideAu"].ToString());
                        }
                        catch (Exception)
                        {
                        }

                        dr["prixTotal"] = serviceGeneral.separateurDesMilles(this.reader["prixTotal"].ToString()) + "Ar";

                        trajet = serviceTrajet.selectTrajet(this.reader["numTrajet"].ToString());
                        if (trajet != null)
                        {
                            dr["trajet"] = trajet.villeD.NomVille + "-" + trajet.villeF.NomVille;
                        }
                        else
                        {
                            dr["trajet"] = "";
                        }
                        trajet = null;
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


        void IntfDalDureeAbonnement.insertToGridDureeAbonnementValide(GridView gridView, string param, string paramLike, string valueLike, string numAbonnement)
        {
            #region declaration
            IntfDalDureeAbonnement serviceDureeAbonnement = new ImplDalDureeAbonnement();
            #endregion

            #region implementation

            this.strCommande = "SELECT dureeabonnement.numDureeAbonnement, dureeabonnement.numTrajet, dureeabonnement.zone,";
            this.strCommande += " dureeabonnement.prixUnitaire, dureeabonnement.numAbonnement, dureeabonnement.valideDu,";
            this.strCommande += " dureeabonnement.valideAu, dureeabonnement.prixTotal, dureeabonnement.matriculeAgent,";
            this.strCommande += " dureeabonnement.dateDureeAbonnement, dureeabonnement.numCalculCategorieBillet,";
            this.strCommande += " dureeabonnement.numCalculReductionBillet, dureeabonnement.modePaiement FROM dureeabonnement";
            this.strCommande += " WHERE dureeabonnement.numAbonnement = '" + numAbonnement + "' AND";
            this.strCommande += " dureeabonnement.valideAu >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND";
            this.strCommande += " " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceDureeAbonnement.getDataTableDureeAbonnementValide(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalDureeAbonnement.getDataTableDureeAbonnementValide(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            IntfDalTrajet serviceTrajet = new ImplDalTrajet();
            crlTrajet trajet = null;
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numDureeAbonnement", typeof(string));
            dataTable.Columns.Add("zone", typeof(string));
            dataTable.Columns.Add("valideDu", typeof(DateTime));
            dataTable.Columns.Add("valideAu", typeof(DateTime));
            dataTable.Columns.Add("prixTotal", typeof(string));
            dataTable.Columns.Add("trajet", typeof(string));
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

                        dr["numDureeAbonnement"] = reader["numDureeAbonnement"].ToString();
                        dr["zone"] = reader["zone"].ToString();
                        try
                        {
                            dr["valideDu"] = Convert.ToDateTime(reader["valideDu"].ToString());
                        }
                        catch (Exception)
                        {
                        }

                        try
                        {
                            dr["valideAu"] = Convert.ToDateTime(this.reader["valideAu"].ToString());
                        }
                        catch (Exception)
                        {
                        }

                        dr["prixTotal"] = serviceGeneral.separateurDesMilles(this.reader["prixTotal"].ToString()) + "Ar";

                        trajet = serviceTrajet.selectTrajet(this.reader["numTrajet"].ToString());
                        if (trajet != null)
                        {
                            dr["trajet"] = trajet.villeD.NomVille + "-" + trajet.villeF.NomVille;
                        }
                        else
                        {
                            dr["trajet"] = "";
                        }
                        trajet = null;
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