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
using System.Collections.Generic;

namespace arch.dal.impl
{
    /// <summary>
    /// Summary description for ImplDalDureeAbonnementDevis
    /// </summary>
    public class ImplDalDureeAbonnementDevis : IntfDalDureeAbonnementDevis
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalDureeAbonnementDevis()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalDureeAbonnementDevis(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region IntfDalDureeAbonnementDevis Members

        crlDureeAbonnementDevis IntfDalDureeAbonnementDevis.selectDureeAbonnementDevis(string numDureeAbonnementDevis)
        {
            #region declaration
            crlDureeAbonnementDevis dureeAbonnementDevis = null;

            IntfDalTrajet serviceTrajet = new ImplDalTrajet();
            IntfDalZone serviceZone = new ImplDalZone();
            IntfDalCalculCategorieBillet serviceCalculCategorieBillet = new ImplDalCalculCategorieBillet();
            IntfDalCalculReductionBillet serviceCalculReductionBillet = new ImplDalCalculReductionBillet();
            #endregion

            #region implementation
            if (numDureeAbonnementDevis != "")
            {
                this.strCommande = "SELECT * FROM `dureeabonnementdevis` WHERE (`numDureeAbonnementDevis`='" + numDureeAbonnementDevis + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            dureeAbonnementDevis = new crlDureeAbonnementDevis();


                            dureeAbonnementDevis.NumProforma = this.reader["numProforma"].ToString();
                            dureeAbonnementDevis.NumDureeAbonnementDevis = this.reader["numDureeAbonnementDevis"].ToString();
                            dureeAbonnementDevis.NumTrajet = this.reader["numTrajet"].ToString();
                            try
                            {
                                dureeAbonnementDevis.PrixTotal = double.Parse(this.reader["prixTotal"].ToString());
                            }
                            catch (Exception) { }
                            try
                            {
                                dureeAbonnementDevis.PrixUnitaire = double.Parse(this.reader["prixUnitaire"].ToString());
                            }
                            catch (Exception) { }
                            try
                            {
                                dureeAbonnementDevis.ValideAu = Convert.ToDateTime(this.reader["valideAu"].ToString());
                            }
                            catch (Exception) { }
                            try
                            {
                                dureeAbonnementDevis.ValideDu = Convert.ToDateTime(this.reader["valideDu"].ToString());
                            }
                            catch (Exception) { }
                            dureeAbonnementDevis.Zone = this.reader["zone"].ToString();
                            try
                            {
                                dureeAbonnementDevis.NombreDureeAbonnement = int.Parse(this.reader["nombreDureeAbonnement"].ToString());
                            }
                            catch (Exception) { }
                            dureeAbonnementDevis.NumCalculCategorieBillet = this.reader["numCalculCategorieBillet"].ToString();
                            dureeAbonnementDevis.NumCalculReductionBillet = this.reader["numCalculReductionBillet"].ToString();
                            dureeAbonnementDevis.NumAbonnement = this.reader["numAbonnement"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                if (dureeAbonnementDevis != null)
                {
                    if (dureeAbonnementDevis.NumTrajet != "")
                    {
                        dureeAbonnementDevis.trajet = serviceTrajet.selectTrajet(dureeAbonnementDevis.NumTrajet);
                    }
                    if (dureeAbonnementDevis.Zone != "")
                    {
                        dureeAbonnementDevis.zoneObj = serviceZone.selectZone(dureeAbonnementDevis.Zone);
                    }
                    if (dureeAbonnementDevis.NumCalculCategorieBillet != "")
                    {
                        dureeAbonnementDevis.calculCategorieBillet = serviceCalculCategorieBillet.selectCalculCategorieBillet(dureeAbonnementDevis.NumCalculCategorieBillet);
                    }
                    if (dureeAbonnementDevis.NumCalculReductionBillet != "")
                    {
                        dureeAbonnementDevis.calculReductionBillet = serviceCalculReductionBillet.selectCalculReductionBillet(dureeAbonnementDevis.NumCalculReductionBillet);
                    }
                }
            }
            #endregion

            return dureeAbonnementDevis;
        }

        string IntfDalDureeAbonnementDevis.insertDureeAbonnementDevis(crlDureeAbonnementDevis dureeAbonnementDevis, string sigleAgence)
        {
            #region declaration
            string numDureeAbonnementDevis = "";
            int nombreInsert = 0;
            IntfDalDureeAbonnementDevis serviceDureeAbonnementDevis = new ImplDalDureeAbonnementDevis();
            string strTrajet = "";
            string numCalculCategorieBillet = "";
            string numCalculReductionBillet = "";
            #endregion

            #region implementation
            if (dureeAbonnementDevis != null)
            {
                
                if (dureeAbonnementDevis.NumTrajet != "")
                {
                    strTrajet = "'" + dureeAbonnementDevis.NumTrajet + "'";
                }
                else
                {
                    strTrajet = "NULL";
                }
                if (dureeAbonnementDevis.NumCalculCategorieBillet != "")
                {
                    numCalculCategorieBillet = "'" + dureeAbonnementDevis.NumCalculCategorieBillet + "'";
                }
                else
                {
                    numCalculCategorieBillet = "NULL";
                }
                if (dureeAbonnementDevis.NumCalculReductionBillet != "")
                {
                    numCalculReductionBillet = "'" + dureeAbonnementDevis.NumCalculReductionBillet + "'";
                }
                else
                {
                    numCalculReductionBillet = "NULL";
                }

                dureeAbonnementDevis.NumDureeAbonnementDevis = serviceDureeAbonnementDevis.getNumDureeAbonnementDevis(sigleAgence);
                this.strCommande = "INSERT INTO `dureeabonnementdevis` (`numDureeAbonnementDevis`,`numTrajet`,`zone`,`prixUnitaire`,";
                this.strCommande += " `numProforma`,`valideDu`,`valideAu`,`prixTotal`,`nombreDureeAbonnement`,`numCalculCategorieBillet`,`numCalculReductionBillet`,`numAbonnement`)";
                this.strCommande += " VALUES ('" + dureeAbonnementDevis.NumDureeAbonnementDevis + "'," + strTrajet + ",";
                this.strCommande += " '" + dureeAbonnementDevis.Zone + "','" + dureeAbonnementDevis.PrixUnitaire + "',";
                this.strCommande += " '" + dureeAbonnementDevis.NumProforma + "','" + dureeAbonnementDevis.ValideDu.ToString("yyyy-MM-dd") + "',";
                this.strCommande += " '" + dureeAbonnementDevis.ValideAu.ToString("yyyy-MM-dd") + "','" + dureeAbonnementDevis.PrixTotal + "',";
                this.strCommande += " '" + dureeAbonnementDevis.NombreDureeAbonnement + "'," + numCalculCategorieBillet + "," + numCalculReductionBillet + ",'" + dureeAbonnementDevis.NumAbonnement + "')";

                this.serviceConnectBase.openConnection();
                nombreInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsert == 1)
                {
                    numDureeAbonnementDevis = dureeAbonnementDevis.NumDureeAbonnementDevis;
                }
                this.serviceConnectBase.closeConnection();
                
            }
            #endregion

            return numDureeAbonnementDevis;
        }

        bool IntfDalDureeAbonnementDevis.updateDureeAbonnementDevis(crlDureeAbonnementDevis dureeAbonnementDevis)
        {
            #region declaration
            bool isUpdate = false;
            string strTrajet = "";
            int nombreUpdate = 0;
            string numCalculCategorieBillet = "";
            string numCalculReductionBillet = "";
            #endregion

            #region implementation
            if (dureeAbonnementDevis != null)
            {
                if (dureeAbonnementDevis.NumTrajet != "")
                {
                    strTrajet = "'" + dureeAbonnementDevis.NumTrajet + "'";
                }
                else
                {
                    strTrajet = "NULL";
                }
                if (dureeAbonnementDevis.NumCalculCategorieBillet != "")
                {
                    numCalculCategorieBillet = "'" + dureeAbonnementDevis.NumCalculCategorieBillet + "'";
                }
                else
                {
                    numCalculCategorieBillet = "NULL";
                }
                if (dureeAbonnementDevis.NumCalculReductionBillet != "")
                {
                    numCalculReductionBillet = "'" + dureeAbonnementDevis.NumCalculReductionBillet + "'";
                }
                else
                {
                    numCalculReductionBillet = "NULL";
                }

                this.strCommande = "UPDATE `dureeabonnementdevis` SET ";
                this.strCommande += " `numProforma`='" + dureeAbonnementDevis.NumProforma + "',";
                this.strCommande += " `numTrajet`=" + strTrajet + ",`prixTotal`='" + dureeAbonnementDevis.PrixTotal + "',";
                this.strCommande += " `prixUnitaire`='" + dureeAbonnementDevis.PrixUnitaire + "',`valideAu`='" + dureeAbonnementDevis.ValideAu.ToString("yyyy-MM-dd") + "',";
                this.strCommande += " `valideDu`='" + dureeAbonnementDevis.ValideDu.ToString("yyyy-MM-dd") + "',`zone`='" + dureeAbonnementDevis.Zone + "',";
                this.strCommande += " `nombreDureeAbonnement`='" + dureeAbonnementDevis.NombreDureeAbonnement + "',";
                this.strCommande += " `numCalculCategorieBillet`=" + numCalculCategorieBillet + ",`numCalculReductionBillet`=" + numCalculReductionBillet + ",";
                this.strCommande += " `numAbonnement`='" + dureeAbonnementDevis.NumAbonnement + "'";
                this.strCommande += " WHERE `numDureeAbonnementDevis`='" + dureeAbonnementDevis.NumDureeAbonnementDevis + "'";
                
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

        List<crlDureeAbonnement> IntfDalDureeAbonnementDevis.getDureeAbonnement(crlDureeAbonnementDevis dureeAbonnementDevis, crlAgent agent)
        {
            #region declaration
            List<crlDureeAbonnement> dureeAbonnement = null;
            crlDureeAbonnement tempDureeAbonnement = null;
            #endregion

            #region implementation
            if(dureeAbonnementDevis != null && agent != null)
            {
                if (dureeAbonnementDevis.NombreDureeAbonnement > 0)
                {
                    dureeAbonnement = new List<crlDureeAbonnement>();
                    for (int i = 0; i < dureeAbonnementDevis.NombreDureeAbonnement; i++)
                    {
                        tempDureeAbonnement = new crlDureeAbonnement();
                        tempDureeAbonnement.MatriculeAgent = agent.matriculeAgent;
                        tempDureeAbonnement.agent = agent;
                        tempDureeAbonnement.calculCategorieBillet = dureeAbonnementDevis.calculCategorieBillet;
                        tempDureeAbonnement.calculReductionBillet = dureeAbonnementDevis.calculReductionBillet;
                        tempDureeAbonnement.ModePaiement = "Commande";
                        tempDureeAbonnement.NumAbonnement = dureeAbonnementDevis.NumAbonnement;
                        tempDureeAbonnement.NumCalculCategorieBillet = dureeAbonnementDevis.NumCalculCategorieBillet;
                        tempDureeAbonnement.NumCalculReductionBillet = dureeAbonnementDevis.NumCalculReductionBillet;
                        tempDureeAbonnement.NumTrajet = dureeAbonnementDevis.NumTrajet;
                        tempDureeAbonnement.PrixTotal = dureeAbonnementDevis.PrixTotal;
                        tempDureeAbonnement.PrixUnitaire = dureeAbonnementDevis.PrixUnitaire;
                        tempDureeAbonnement.ValideAu = dureeAbonnementDevis.ValideAu;
                        tempDureeAbonnement.ValideDu = dureeAbonnementDevis.ValideDu;
                        tempDureeAbonnement.Zone = dureeAbonnementDevis.Zone;
                        tempDureeAbonnement.zoneObj = dureeAbonnementDevis.zoneObj;
                        tempDureeAbonnement.trajet = dureeAbonnementDevis.trajet;

                        dureeAbonnement.Add(tempDureeAbonnement);
                        tempDureeAbonnement = null;
                    }
                }
            }
            #endregion

            return dureeAbonnement;
        }

        string IntfDalDureeAbonnementDevis.getNumDureeAbonnementDevis(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numDureeAbonnementDevis = "00001";
            string[] tempNumDureeAbonnementDevis = null;
            string strDate = "DD" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT dureeabonnementdevis.numDureeAbonnementDevis AS maxNum FROM dureeabonnementdevis";
            this.strCommande += " WHERE dureeabonnementdevis.numDureeAbonnementDevis LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumDureeAbonnementDevis = reader["maxNum"].ToString().ToString().Split('/');
                        numDureeAbonnementDevis = tempNumDureeAbonnementDevis[tempNumDureeAbonnementDevis.Length - 1];
                    }
                    numTemp = double.Parse(numDureeAbonnementDevis) + 1;
                    if (numTemp < 10)
                        numDureeAbonnementDevis = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numDureeAbonnementDevis = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numDureeAbonnementDevis = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numDureeAbonnementDevis = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numDureeAbonnementDevis = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numDureeAbonnementDevis = strDate + "/" + sigleAgence + "/" + numDureeAbonnementDevis;
            #endregion

            return numDureeAbonnementDevis;
        }

        #endregion

        #region insert to grid

        void IntfDalDureeAbonnementDevis.insertToGridDureeAbonnementDevis(GridView gridView, string param, string paramLike, string valueLike, string numProforma)
        {
            #region declaration
            IntfDalDureeAbonnementDevis serviceDureeAbonnementDevis = new ImplDalDureeAbonnementDevis();
            #endregion

            #region implementation

            this.strCommande = "SELECT dureeabonnementdevis.numDureeAbonnementDevis, dureeabonnementdevis.numTrajet,";
            this.strCommande += " dureeabonnementdevis.zone, dureeabonnementdevis.numProforma, dureeabonnementdevis.prixUnitaire,";
            this.strCommande += " dureeabonnementdevis.valideDu, dureeabonnementdevis.valideAu, dureeabonnementdevis.nombreDureeAbonnement,";
            this.strCommande += " dureeabonnementdevis.prixTotal FROM dureeabonnementdevis";
            this.strCommande += " WHERE dureeabonnementdevis.numProforma = '" + numProforma + "' AND";
            this.strCommande += " " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceDureeAbonnementDevis.getDataTableDureeAbonnementDevis(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalDureeAbonnementDevis.getDataTableDureeAbonnementDevis(string strRqst)
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
            dataTable.Columns.Add("numDureeAbonnementDevis", typeof(string));
            dataTable.Columns.Add("zone", typeof(string));
            dataTable.Columns.Add("valideDu", typeof(DateTime));
            dataTable.Columns.Add("valideAu", typeof(DateTime));
            dataTable.Columns.Add("prixTotal", typeof(string));
            dataTable.Columns.Add("nombre", typeof(string));
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

                        dr["numDureeAbonnementDevis"] = reader["numDureeAbonnementDevis"].ToString();
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
                        dr["nombre"] = this.reader["nombreDureeAbonnement"].ToString();

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


        
    }
}
