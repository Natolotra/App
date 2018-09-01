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
    /// Implementation du service VoyageAbonnement
    /// </summary>
    public class ImplDalVoyageAbonnement : IntfDalVoyageAbonnement
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalVoyageAbonnement()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalVoyageAbonnement(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion


        #region methode
        crlVoyageAbonnement IntfDalVoyageAbonnement.selectVoyageAbonnement(string numVoyageAbonnement)
        {
            #region declaration
            crlVoyageAbonnement voyageAbonnement = null;

            IntfDalAgent serviceAgent = new ImplDalAgent();
            IntfDalTrajet serviceTrajet = new ImplDalTrajet();
            IntfDalZone serviceZone = new ImplDalZone();
            IntfDalAbonnement serviceAbonnement = new ImplDalAbonnement();
            IntfDalCalculCategorieBillet serviceCalculCategorieBillet = new ImplDalCalculCategorieBillet();
            IntfDalCalculReductionBillet serviceCalculReductionBillet = new ImplDalCalculReductionBillet();
            #endregion

            #region implementation
            if (numVoyageAbonnement != "") 
            {
                this.strCommande = "SELECT * FROM `voyageabonnement` WHERE `numVoyageAbonnement`='" + numVoyageAbonnement + "'";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            voyageAbonnement = new crlVoyageAbonnement();
                            try
                            {
                                voyageAbonnement.DateVoyageAbonnement = Convert.ToDateTime(this.reader["dateVoyageAbonnement"].ToString()); 
                            }
                            catch (Exception) { }
                            voyageAbonnement.MatriculeAgent = this.reader["matriculeAgent"].ToString();
                            try
                            {
                                voyageAbonnement.NbVoyageAbonnement = int.Parse(this.reader["nbVoyageAbonnement"].ToString());
                            }
                            catch (Exception) { }
                            voyageAbonnement.NumAbonnement = this.reader["numAbonnement"].ToString();
                            voyageAbonnement.NumTrajet = this.reader["numTrajet"].ToString();
                            voyageAbonnement.NumVoyageAbonnement = this.reader["numVoyageAbonnement"].ToString();
                            try
                            {
                                voyageAbonnement.PrixUnitaire = double.Parse(this.reader["prixUnitaire"].ToString());
                            }
                            catch (Exception) { }
                            voyageAbonnement.Zone = this.reader["zone"].ToString();
                            voyageAbonnement.NumCalculCategorieBillet = this.reader["numCalculCategorieBillet"].ToString();
                            voyageAbonnement.NumCalculReductionBillet = this.reader["numCalculReductionBillet"].ToString();
                            voyageAbonnement.ModePaiement = this.reader["modePaiement"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                if (voyageAbonnement != null) 
                {
                    if (voyageAbonnement.MatriculeAgent != "") 
                    {
                        voyageAbonnement.agent = serviceAgent.selectAgent(voyageAbonnement.MatriculeAgent);
                    }
                    if (voyageAbonnement.NumAbonnement != "") 
                    {
                        voyageAbonnement.abonnement = serviceAbonnement.selectAbonnement(voyageAbonnement.NumAbonnement);
                    }
                    if (voyageAbonnement.NumTrajet != "") 
                    {
                        voyageAbonnement.trajet = serviceTrajet.selectTrajet(voyageAbonnement.NumTrajet);
                    }
                    if (voyageAbonnement.Zone != "") 
                    {
                        voyageAbonnement.zoneObj = serviceZone.selectZone(voyageAbonnement.Zone);
                    }
                    if (voyageAbonnement.NumCalculCategorieBillet != "")
                    {
                        voyageAbonnement.calculCategorieBillet = serviceCalculCategorieBillet.selectCalculCategorieBillet(voyageAbonnement.NumCalculCategorieBillet);
                    }
                    if (voyageAbonnement.NumCalculReductionBillet != "")
                    {
                        voyageAbonnement.calculReductionBillet = serviceCalculReductionBillet.selectCalculReductionBillet(voyageAbonnement.NumCalculReductionBillet);
                    }
                }
            }
            #endregion

            return voyageAbonnement;
        }

        string IntfDalVoyageAbonnement.insertVoyageAbonnement(crlVoyageAbonnement voyageAbonnement)
        {
            #region declaration
            string numVoyageAbonnement = "";
            int nombreInsert = 0;
            IntfDalVoyageAbonnement serviceVoyageAbonnement = new ImplDalVoyageAbonnement();
            string strTrajet = "";
            string numCalculCategorieBillet = "NULL";
            string numCalculReductionBillet = "NULL";
            string modePaiement = "NULL";
            #endregion

            #region implementation
            if (voyageAbonnement != null) 
            {
                if (voyageAbonnement.agent != null)
                {
                    if (voyageAbonnement.NumTrajet != "")
                    {
                        strTrajet = "'" + voyageAbonnement.NumTrajet + "'";
                    }
                    else 
                    {
                        strTrajet = "NULL";
                    }
                    if (voyageAbonnement.NumCalculCategorieBillet != "")
                    {
                        numCalculCategorieBillet = "'" + voyageAbonnement.NumCalculCategorieBillet + "'";
                    }
                    if (voyageAbonnement.NumCalculReductionBillet != "")
                    {
                        numCalculReductionBillet = "'" + voyageAbonnement.NumCalculReductionBillet + "'";
                    }
                    if (voyageAbonnement.ModePaiement != "")
                    {
                        modePaiement = "'" + voyageAbonnement.ModePaiement + "'";
                    }

                    voyageAbonnement.NumVoyageAbonnement = serviceVoyageAbonnement.getNumVoyageAbonnement(voyageAbonnement.agent.agence.SigleAgence);
                    this.strCommande = "INSERT INTO `voyageabonnement` (`numVoyageAbonnement`,`numTrajet`,`zone`,`prixUnitaire`,";
                    this.strCommande += " `nbVoyageAbonnement`,`numAbonnement`,`matriculeAgent`,`dateVoyageAbonnement`,";
                    this.strCommande += " `numCalculCategorieBillet`,`numCalculReductionBillet`,`modePaiement`)";
                    this.strCommande += " VALUES ('" + voyageAbonnement.NumVoyageAbonnement + "'," + strTrajet + ",";
                    this.strCommande += " '" + voyageAbonnement.Zone + "','" + voyageAbonnement.PrixUnitaire + "',";
                    this.strCommande += " '" + voyageAbonnement.NbVoyageAbonnement + "','" + voyageAbonnement.NumAbonnement + "',";
                    this.strCommande += " '" + voyageAbonnement.MatriculeAgent + "','" + voyageAbonnement.DateVoyageAbonnement.ToString("yyyy-MM-dd") + "',";
                    this.strCommande += " " + numCalculCategorieBillet + "," + numCalculReductionBillet + "," + modePaiement + ")";

                    this.serviceConnectBase.openConnection();
                    nombreInsert = this.serviceConnectBase.requete(this.strCommande);
                    if (nombreInsert == 1) 
                    {
                        numVoyageAbonnement = voyageAbonnement.NumVoyageAbonnement;
                    }
                    this.serviceConnectBase.closeConnection();
                }
            }
            #endregion

            return numVoyageAbonnement;
        }

        bool IntfDalVoyageAbonnement.updateVoyageAbonnement(crlVoyageAbonnement voyageAbonnement)
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
            if (voyageAbonnement != null)
            {
                if (voyageAbonnement.NumTrajet != "")
                {
                    strTrajet = "'" + voyageAbonnement.NumTrajet + "'";
                }
                else
                {
                    strTrajet = "NULL";
                }
                if (voyageAbonnement.NumCalculCategorieBillet != "")
                {
                    numCalculCategorieBillet = "'" + voyageAbonnement.NumCalculCategorieBillet + "'";
                }
                if (voyageAbonnement.NumCalculReductionBillet != "")
                {
                    numCalculReductionBillet = "'" + voyageAbonnement.NumCalculReductionBillet + "'";
                }
                if (voyageAbonnement.ModePaiement != "")
                {
                    modePaiement = "'" + voyageAbonnement.ModePaiement + "'";
                }

                this.strCommande = "UPDATE `voyageabonnement` SET `dateVoyageAbonnement`='" + voyageAbonnement.DateVoyageAbonnement.ToString("yyyy-MM-dd") + "',";
                this.strCommande += " `matriculeAgent`='" + voyageAbonnement.MatriculeAgent + "',`numAbonnement`='" + voyageAbonnement.NumAbonnement + "',";
                this.strCommande += " `numTrajet`=" + strTrajet + ",`prixUnitaire`='" + voyageAbonnement.PrixUnitaire + "',";
                this.strCommande += " `zone`='" + voyageAbonnement.Zone + "',`nbVoyageAbonnement`='" + voyageAbonnement.NbVoyageAbonnement + "',";
                this.strCommande += " `numCalculCategorieBillet`=" + numCalculCategorieBillet + ",`numCalculReductionBillet`=" + numCalculReductionBillet + ",";
                this.strCommande += " `modePaiement`=" + modePaiement;
                this.strCommande += " WHERE `numVoyageAbonnement`='" + voyageAbonnement.NumVoyageAbonnement + "'";

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

        string IntfDalVoyageAbonnement.getNumVoyageAbonnement(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numVoyageAbonnement = "00001";
            string[] tempNumVoyageAbonnement = null;
            string strDate = "VA" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT voyageabonnement.numVoyageAbonnement AS maxNum FROM voyageabonnement";
            this.strCommande += " WHERE voyageabonnement.numVoyageAbonnement LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumVoyageAbonnement = reader["maxNum"].ToString().ToString().Split('/');
                        numVoyageAbonnement = tempNumVoyageAbonnement[tempNumVoyageAbonnement.Length - 1];
                    }
                    numTemp = double.Parse(numVoyageAbonnement) + 1;
                    if (numTemp < 10)
                        numVoyageAbonnement = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numVoyageAbonnement = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numVoyageAbonnement = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numVoyageAbonnement = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numVoyageAbonnement = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numVoyageAbonnement = strDate + "/" + sigleAgence + "/" + numVoyageAbonnement;
            #endregion

            return numVoyageAbonnement;
        }
        #endregion

        #region insert to grid
        void IntfDalVoyageAbonnement.insertToGridVoyageAbonnementNonRecu(GridView gridView, string param, string paramLike, string valueLike, string numAbonnement)
        {
            #region declaration
            IntfDalVoyageAbonnement serviceVoyageAbonnement = new ImplDalVoyageAbonnement();
            #endregion

            #region implementation

            this.strCommande = "SELECT voyageabonnement.numVoyageAbonnement, voyageabonnement.numTrajet, voyageabonnement.zone,";
            this.strCommande += " voyageabonnement.prixUnitaire, voyageabonnement.nbVoyageAbonnement, voyageabonnement.numAbonnement,";
            this.strCommande += " voyageabonnement.matriculeAgent, voyageabonnement.dateVoyageAbonnement FROM voyageabonnement";
            this.strCommande += " Left Join recuabonnement ON recuabonnement.numVoyageAbonnement = voyageabonnement.numVoyageAbonnement";
            this.strCommande += " Left Join bondecommande ON bondecommande.numVoyageAbonnement = voyageabonnement.numVoyageAbonnement";
            this.strCommande += " WHERE recuabonnement.numVoyageAbonnement IS NULL  AND";
            this.strCommande += " bondecommande.numVoyageAbonnement IS NULL  AND";
            this.strCommande += " voyageabonnement.numAbonnement = '" + numAbonnement + "' AND";
            this.strCommande += " " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceVoyageAbonnement.getDataTableVoyageAbonnementNonRecu(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalVoyageAbonnement.getDataTableVoyageAbonnementNonRecu(string strRqst)
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
            dataTable.Columns.Add("numVoyageAbonnement", typeof(string));
            dataTable.Columns.Add("zone", typeof(string));
            dataTable.Columns.Add("dateVoyageAbonnement", typeof(DateTime));
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

                        dr["numVoyageAbonnement"] = reader["numVoyageAbonnement"].ToString();
                        dr["zone"] = reader["zone"].ToString();
                        try
                        {
                            dr["dateVoyageAbonnement"] = Convert.ToDateTime(reader["dateVoyageAbonnement"].ToString());
                        }
                        catch (Exception)
                        {
                        }

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


        void IntfDalVoyageAbonnement.insertToGridVoyageAbonnementValide(GridView gridView, string param, string paramLike, string valueLike, string numAbonnement)
        {
            #region declaration
            IntfDalVoyageAbonnement serviceVoyageAbonnement = new ImplDalVoyageAbonnement();
            #endregion

            #region implementation

            this.strCommande = "SELECT voyageabonnement.numVoyageAbonnement, voyageabonnement.numTrajet, voyageabonnement.zone,";
            this.strCommande += " voyageabonnement.prixUnitaire, voyageabonnement.nbVoyageAbonnement, voyageabonnement.numAbonnement,";
            this.strCommande += " voyageabonnement.matriculeAgent, voyageabonnement.dateVoyageAbonnement, voyageabonnement.numCalculCategorieBillet,";
            this.strCommande += " voyageabonnement.numCalculReductionBillet, voyageabonnement.modePaiement FROM voyageabonnement";
            this.strCommande += " WHERE voyageabonnement.nbVoyageAbonnement > 0 AND";
            this.strCommande += " voyageabonnement.numAbonnement = '" + numAbonnement + "' AND";
            this.strCommande += " " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceVoyageAbonnement.getDataTableVoyageAbonnementValide(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalVoyageAbonnement.getDataTableVoyageAbonnementValide(string strRqst)
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
            dataTable.Columns.Add("numVoyageAbonnement", typeof(string));
            dataTable.Columns.Add("zone", typeof(string));
            dataTable.Columns.Add("dateVoyageAbonnement", typeof(DateTime));
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

                        dr["numVoyageAbonnement"] = reader["numVoyageAbonnement"].ToString();
                        dr["zone"] = reader["zone"].ToString();
                        try
                        {
                            dr["dateVoyageAbonnement"] = Convert.ToDateTime(reader["dateVoyageAbonnement"].ToString());
                        }
                        catch (Exception)
                        {
                        }

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
    }
}