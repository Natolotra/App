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
    /// Description résumée de ImplDalVoyageAbonnementDevis
    /// </summary>
    public class ImplDalVoyageAbonnementDevis : IntfDalVoyageAbonnementDevis
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalVoyageAbonnementDevis()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalVoyageAbonnementDevis(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region IntfDalVoyageAbonnementDevis Members

        crlVoyageAbonnementDevis IntfDalVoyageAbonnementDevis.selectVoyageAbonnementDevis(string numVoyageAbonnementDevis)
        {
            #region declaration
            crlVoyageAbonnementDevis voyageAbonnementDevis = null;

            IntfDalTrajet serviceTrajet = new ImplDalTrajet();
            IntfDalZone serviceZone = new ImplDalZone();
            IntfDalCalculCategorieBillet serviceCalculCategorieBillet = new ImplDalCalculCategorieBillet();
            IntfDalCalculReductionBillet serviceCalculReductionBillet = new ImplDalCalculReductionBillet();
            #endregion

            #region implementation
            if (numVoyageAbonnementDevis != "")
            {
                this.strCommande = "SELECT * FROM `voyageabonnementdevis` WHERE `numVoyageAbonnementDevis`='" + numVoyageAbonnementDevis + "'";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            voyageAbonnementDevis = new crlVoyageAbonnementDevis();
                            
                            
                            try
                            {
                                voyageAbonnementDevis.NbVoyageAbonnement = int.Parse(this.reader["nbVoyageAbonnement"].ToString());
                            }
                            catch (Exception) { }
                            voyageAbonnementDevis.NumProforma = this.reader["numProforma"].ToString();
                            voyageAbonnementDevis.NumTrajet = this.reader["numTrajet"].ToString();
                            voyageAbonnementDevis.NumVoyageAbonnementDevis = this.reader["numVoyageAbonnementDevis"].ToString();
                            try
                            {
                                voyageAbonnementDevis.PrixUnitaire = double.Parse(this.reader["prixUnitaire"].ToString());
                            }
                            catch (Exception) { }
                            voyageAbonnementDevis.Zone = this.reader["zone"].ToString();
                            voyageAbonnementDevis.NumCalculCategorieBillet = this.reader["numCalculCategorieBillet"].ToString();
                            voyageAbonnementDevis.NumCalculReductionBillet = this.reader["numCalculReductionBillet"].ToString();
                            voyageAbonnementDevis.NumAbonnement = this.reader["numAbonnement"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                if (voyageAbonnementDevis != null)
                {
                    if (voyageAbonnementDevis.NumTrajet != "")
                    {
                        voyageAbonnementDevis.trajet = serviceTrajet.selectTrajet(voyageAbonnementDevis.NumTrajet);
                    }
                    if (voyageAbonnementDevis.Zone != "")
                    {
                        voyageAbonnementDevis.zoneObj = serviceZone.selectZone(voyageAbonnementDevis.Zone);
                    }
                    if (voyageAbonnementDevis.NumCalculCategorieBillet != "")
                    {
                        voyageAbonnementDevis.calculCategorieBillet = serviceCalculCategorieBillet.selectCalculCategorieBillet(voyageAbonnementDevis.NumCalculCategorieBillet);
                    }
                    if (voyageAbonnementDevis.NumCalculReductionBillet != "")
                    {
                        voyageAbonnementDevis.calculReductionBillet = serviceCalculReductionBillet.selectCalculReductionBillet(voyageAbonnementDevis.NumCalculReductionBillet);
                    }
                }
            }
            #endregion

            return voyageAbonnementDevis;
        }

        string IntfDalVoyageAbonnementDevis.insertVoyageAbonnementDevis(crlVoyageAbonnementDevis voyageAbonnementDevis, string sigleAgence)
        {
            #region declaration
            string numVoyageAbonnementDevis = "";
            int nombreInsert = 0;
            IntfDalVoyageAbonnementDevis serviceVoyageAbonnementDevis = new ImplDalVoyageAbonnementDevis();
            string strTrajet = "";
            string numCalculCategorieBillet = "";
            string numCalculReductionBillet = "";
            #endregion

            #region implementation
            if (voyageAbonnementDevis != null)
            {
                
                if (voyageAbonnementDevis.NumTrajet != "")
                {
                    strTrajet = "'" + voyageAbonnementDevis.NumTrajet + "'";
                }
                else
                {
                    strTrajet = "NULL";
                }
                if (voyageAbonnementDevis.NumCalculCategorieBillet != "")
                {
                    numCalculCategorieBillet = "'" + voyageAbonnementDevis.NumCalculCategorieBillet + "'";
                }
                else
                {
                    numCalculCategorieBillet = "NULL";
                }
                if (voyageAbonnementDevis.NumCalculReductionBillet != "")
                {
                    numCalculReductionBillet = "'" + voyageAbonnementDevis.NumCalculReductionBillet + "'";
                }
                else
                {
                    numCalculReductionBillet = "NULL";
                }

                voyageAbonnementDevis.NumVoyageAbonnementDevis = serviceVoyageAbonnementDevis.getNumVoyageAbonnementDevis(sigleAgence);
                this.strCommande = "INSERT INTO `voyageabonnementdevis` (`numVoyageAbonnementDevis`,`numTrajet`,`zone`,`prixUnitaire`,";
                this.strCommande += " `nbVoyageAbonnement`,`numProforma`,`numCalculCategorieBillet`,`numCalculReductionBillet`,`numAbonnement`)";
                this.strCommande += " VALUES ('" + voyageAbonnementDevis.NumVoyageAbonnementDevis + "'," + strTrajet + ",";
                this.strCommande += " '" + voyageAbonnementDevis.Zone + "','" + voyageAbonnementDevis.PrixUnitaire + "',";
                this.strCommande += " '" + voyageAbonnementDevis.NbVoyageAbonnement + "','" + voyageAbonnementDevis.NumProforma + "',";
                this.strCommande += " " + numCalculCategorieBillet + "," + numCalculReductionBillet + ",'" + voyageAbonnementDevis.NumAbonnement + "')";

                this.serviceConnectBase.openConnection();
                nombreInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsert == 1)
                {
                    numVoyageAbonnementDevis = voyageAbonnementDevis.NumVoyageAbonnementDevis;
                }
                this.serviceConnectBase.closeConnection();
               
            }
            #endregion

            return numVoyageAbonnementDevis;
        }

        bool IntfDalVoyageAbonnementDevis.updateVoyageAbonnementDevis(crlVoyageAbonnementDevis voyageAbonnementDevis)
        {
            #region declaration
            bool isUpdate = false;
            string strTrajet = "";
            int nombreUpdate = 0;
            string numCalculCategorieBillet = "";
            string numCalculReductionBillet = "";
            #endregion

            #region implementation
            if (voyageAbonnementDevis != null)
            {
                if (voyageAbonnementDevis.NumTrajet != "")
                {
                    strTrajet = "'" + voyageAbonnementDevis.NumTrajet + "'";
                }
                else
                {
                    strTrajet = "NULL";
                }
                if (voyageAbonnementDevis.NumCalculCategorieBillet != "")
                {
                    numCalculCategorieBillet = "'" + voyageAbonnementDevis.NumCalculCategorieBillet + "'";
                }
                else
                {
                    numCalculCategorieBillet = "NULL";
                }
                if (voyageAbonnementDevis.NumCalculReductionBillet != "")
                {
                    numCalculReductionBillet = "'" + voyageAbonnementDevis.NumCalculReductionBillet + "'";
                }
                else
                {
                    numCalculReductionBillet = "NULL";
                }

                this.strCommande = "UPDATE `voyageabonnementdevis` SET ";
                this.strCommande += " `numProforma`='" + voyageAbonnementDevis.NumProforma + "',";
                this.strCommande += " `numTrajet`=" + strTrajet + ",`prixUnitaire`='" + voyageAbonnementDevis.PrixUnitaire + "',";
                this.strCommande += " `zone`='" + voyageAbonnementDevis.Zone + "',`nbVoyageAbonnement`='" + voyageAbonnementDevis.NbVoyageAbonnement + "',";
                this.strCommande += " `numCalculCategorieBillet`=" + numCalculCategorieBillet + ",`numCalculReductionBillet`=" + numCalculReductionBillet + ",";
                this.strCommande += " `numAbonnement`='" + voyageAbonnementDevis.NumAbonnement + "'";
                this.strCommande += " WHERE `numVoyageAbonnementDevis`='" + voyageAbonnementDevis.NumVoyageAbonnementDevis + "'";

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

        string IntfDalVoyageAbonnementDevis.getNumVoyageAbonnementDevis(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numVoyageAbonnementDevis = "00001";
            string[] tempNumVoyageAbonnementDevis = null;
            string strDate = "VD" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT voyageabonnementdevis.numVoyageAbonnementDevis AS maxNum FROM voyageabonnementdevis";
            this.strCommande += " WHERE voyageabonnementdevis.numVoyageAbonnementDevis LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumVoyageAbonnementDevis = reader["maxNum"].ToString().ToString().Split('/');
                        numVoyageAbonnementDevis = tempNumVoyageAbonnementDevis[tempNumVoyageAbonnementDevis.Length - 1];
                    }
                    numTemp = double.Parse(numVoyageAbonnementDevis) + 1;
                    if (numTemp < 10)
                        numVoyageAbonnementDevis = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numVoyageAbonnementDevis = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numVoyageAbonnementDevis = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numVoyageAbonnementDevis = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numVoyageAbonnementDevis = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numVoyageAbonnementDevis = strDate + "/" + sigleAgence + "/" + numVoyageAbonnementDevis;
            #endregion

            return numVoyageAbonnementDevis;
        }

        crlVoyageAbonnement IntfDalVoyageAbonnementDevis.getVoyageAbonnement(crlVoyageAbonnementDevis voyageAbonnementDevis, crlAgent agent)
        {
            #region declaration
            crlVoyageAbonnement abonnementVoyage = null;
            #endregion

            #region implementation
            if (voyageAbonnementDevis != null && agent != null)
            {
                abonnementVoyage = new crlVoyageAbonnement();
                abonnementVoyage.agent = agent;
                abonnementVoyage.calculCategorieBillet = voyageAbonnementDevis.calculCategorieBillet;
                abonnementVoyage.calculReductionBillet = voyageAbonnementDevis.calculReductionBillet;
                abonnementVoyage.MatriculeAgent = agent.matriculeAgent;
                abonnementVoyage.ModePaiement = "Commande";
                abonnementVoyage.NbVoyageAbonnement = voyageAbonnementDevis.NbVoyageAbonnement;
                abonnementVoyage.NumAbonnement = voyageAbonnementDevis.NumAbonnement;
                abonnementVoyage.NumCalculCategorieBillet = voyageAbonnementDevis.NumCalculCategorieBillet;
                abonnementVoyage.NumCalculReductionBillet = voyageAbonnementDevis.NumCalculReductionBillet;
                abonnementVoyage.NumTrajet = voyageAbonnementDevis.NumTrajet;
                abonnementVoyage.PrixUnitaire = voyageAbonnementDevis.PrixUnitaire;
                abonnementVoyage.trajet = voyageAbonnementDevis.trajet;
                abonnementVoyage.Zone = voyageAbonnementDevis.Zone;
                abonnementVoyage.zoneObj = voyageAbonnementDevis.zoneObj;
            }
            #endregion

            return abonnementVoyage;
        }
        #endregion

        #region inser to grid

        void IntfDalVoyageAbonnementDevis.insertToGridVoyageAbonnementDevis(GridView gridView, string param, string paramLike, string valueLike, string numProforma)
        {
            #region declaration
            IntfDalVoyageAbonnementDevis serviceVoyageAbonnementDevis = new ImplDalVoyageAbonnementDevis();
            #endregion

            #region implementation

            this.strCommande = "SELECT voyageabonnementdevis.numVoyageAbonnementDevis, voyageabonnementdevis.numTrajet,";
            this.strCommande += " voyageabonnementdevis.zone, voyageabonnementdevis.numProforma, voyageabonnementdevis.prixUnitaire,";
            this.strCommande += " voyageabonnementdevis.nbVoyageAbonnement FROM voyageabonnementdevis";
            this.strCommande += " WHERE voyageabonnementdevis.numProforma = '" + numProforma + "' AND";
            this.strCommande += " " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceVoyageAbonnementDevis.getDataTableVoyageAbonnementDevis(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalVoyageAbonnementDevis.getDataTableVoyageAbonnementDevis(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            IntfDalTrajet serviceTrajet = new ImplDalTrajet();
            crlTrajet trajet = null;
            double montantTotal = 0;
            double prixUnitaire = 0;
            int nbVoyage = 0;
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numVoyageAbonnementDevis", typeof(string));
            dataTable.Columns.Add("zone", typeof(string));
            dataTable.Columns.Add("nbVoyageAbonnement", typeof(string));
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

                        dr["numVoyageAbonnementDevis"] = reader["numVoyageAbonnementDevis"].ToString();
                        dr["zone"] = reader["zone"].ToString();
                        

                        try
                        {
                            nbVoyage = int.Parse(this.reader["nbVoyageAbonnement"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            prixUnitaire = double.Parse(this.reader["prixUnitaire"].ToString());
                        }
                        catch (Exception)
                        {
                        }

                        dr["nbVoyageAbonnement"] = this.reader["nbVoyageAbonnement"].ToString();

                        montantTotal = prixUnitaire * nbVoyage;
                        dr["prixTotal"] = serviceGeneral.separateurDesMilles(montantTotal.ToString("0")) + "Ar";

                        trajet = serviceTrajet.selectTrajet(this.reader["numTrajet"].ToString());
                        if (trajet != null)
                        {
                            dr["trajet"] = trajet.villeD.NomVille + "-" + trajet.villeF.NomVille;
                        }
                        else
                        {
                            dr["trajet"] = "";
                        }

                        nbVoyage = 0;
                        montantTotal = 0;
                        prixUnitaire = 0;
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