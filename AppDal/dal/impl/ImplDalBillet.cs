using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using arch.dal.intf;
using arch.crl;
using System.Data;
using System.Web.UI.WebControls;

namespace arch.dal.impl
{
    /// <summary>
    /// Implementation du service Billet
    /// </summary>
    public class ImplDalBillet : IntfDalBillet
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalBillet()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalBillet(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region IntfDalAgent Members
        string IntfDalBillet.insertBillet(crlBillet Billet)
        {
            #region declaration
            string numBillet = "";
            int nombreInsert = 0;
            IntfDalBillet serviceBillet = new ImplDalBillet();
            string modePaiement = "";
            string numIndividu = "";
            string numCalculCategorieBillet = "";
            string numCalculReductionBillet = "";
            string numTrajet = "NULL";
            string numDureeAbonnement = "NULL";
            string numVoyageAbonnement = "NULL";
            string numBilletCommande = "NULL";
            #endregion

            #region implementation
            if (Billet != null)
            {
                if (Billet.ModePaiement != "")
                {
                    modePaiement = "'" + Billet.ModePaiement + "'";
                }
                else
                {
                    modePaiement = "NULL";
                }
                if (Billet.NumIndividu != "")
                {
                    numIndividu = "'" + Billet.NumIndividu + "'";
                }
                else 
                {
                    numIndividu = "NULL";
                }
                if (Billet.NumCalculCategorieBillet != "") 
                {
                    numCalculCategorieBillet = "'" + Billet.NumCalculCategorieBillet + "'";
                }
                else
                {
                    numCalculCategorieBillet = "NULL";
                }
                if (Billet.NumCalculReductionBillet != "")
                {
                    numCalculReductionBillet = "'" + Billet.NumCalculReductionBillet + "'";
                }
                else 
                {
                    numCalculReductionBillet = "NULL";
                }
                if (Billet.NumTrajet != "")
                {
                    numTrajet = "'" + Billet.NumTrajet + "'";
                }
                if (Billet.NumDureeAbonnement != "")
                {
                    numDureeAbonnement = "'" + Billet.NumDureeAbonnement + "'";
                }
                if (Billet.NumVoyageAbonnement != "")
                {
                    numVoyageAbonnement = "'" + Billet.NumVoyageAbonnement + "'";
                }
                if (Billet.NumBilletCommande != "")
                {
                    numBilletCommande = "'" + Billet.NumBilletCommande + "'";
                }

                Billet.NumBillet = serviceBillet.getNumBillet(Billet.agent.agence.SigleAgence);



                this.strCommande = "INSERT INTO `billet` (`numBillet`,`dateDeValidite`,`numTrajet`,`modePaiement`,`numIndividu`,`matriculeAgent`,";
                this.strCommande += " `prixBillet`,`numCalculCategorieBillet`,`numCalculReductionBillet`,`dateBillet`,`numDureeAbonnement`,";
                this.strCommande += " `numVoyageAbonnement`,`numBilletCommande`)";
                this.strCommande += " VALUES ('" + Billet.NumBillet + "','" + Billet.DateDeValidite.ToString("yyyy-MM-dd") + "',";
                this.strCommande += " " + numTrajet + "," + modePaiement + "," + numIndividu + ",";
                this.strCommande += " '" + Billet.MatriculeAgent + "','" + Billet.PrixBillet + "'," + numCalculCategorieBillet + ",";
                this.strCommande += " " + numCalculReductionBillet + ",'" + Billet.DateBillet.ToString("yyyy-MM-dd") + "',";
                this.strCommande += " " + numDureeAbonnement + "," + numVoyageAbonnement + "," + numBilletCommande + ")";
                
                this.serviceConnectBase.openConnection();
                nombreInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsert == 1)
                    numBillet = Billet.NumBillet;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numBillet;
        }

        bool IntfDalBillet.deleteBillet(crlBillet Billet)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (Billet != null)
            {
                if (Billet.NumBillet != "")
                {
                    this.strCommande = "DELETE FROM `billet` WHERE (`numBillet` = '" + Billet.NumBillet + "')";
                    this.serviceConnectBase.openConnection();
                    nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                    if (nombreDelete == 1)
                        isDelete = true;
                    this.serviceConnectBase.closeConnection();
                }
            }
            #endregion

            return isDelete;
        }

        bool IntfDalBillet.deleteBillet(string numBillet)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
           
            if (numBillet != "")
            {
                this.strCommande = "DELETE FROM `billet` WHERE (`numBillet` = '" + numBillet + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete == 1)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }
            
            #endregion

            return isDelete;
        }

        bool IntfDalBillet.updateBillet(crlBillet Billet)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            string modePaiement = "";
            string numIndividu = "";
            string numCalculCategorieBillet = "";
            string numCalculReductionBillet = "";
            string numTrajet = "NULL";
            string numDureeAbonnement = "NULL";
            string numVoyageAbonnement = "NULL";
            string numBilletCommande = "NULL";
            #endregion

            #region implementation
            if (Billet != null)
            {
                if (Billet.NumBillet != "")
                {
                    if (Billet.ModePaiement != "")
                    {
                        modePaiement = "'" + Billet.ModePaiement + "'";
                    }
                    else
                    {
                        modePaiement = "NULL";
                    }
                    if (Billet.NumIndividu != "")
                    {
                        numIndividu = "'" + Billet.NumIndividu + "'";
                    }
                    else 
                    {
                        numIndividu = "NULL";
                    }
                    if (Billet.NumCalculCategorieBillet != "")
                    {
                        numCalculCategorieBillet = "'" + Billet.NumCalculCategorieBillet + "'";
                    }
                    else
                    {
                        numCalculCategorieBillet = "NULL";
                    }
                    if (Billet.NumCalculReductionBillet != "")
                    {
                        numCalculReductionBillet = "'" + Billet.NumCalculReductionBillet + "'";
                    }
                    else
                    {
                        numCalculReductionBillet = "NULL";
                    }
                    if (Billet.NumTrajet != "")
                    {
                        numTrajet = "'" + Billet.NumTrajet + "'";
                    }
                    if (Billet.NumDureeAbonnement != "")
                    {
                        numDureeAbonnement = "'" + Billet.NumDureeAbonnement + "'";
                    }
                    if (Billet.NumVoyageAbonnement != "")
                    {
                        numVoyageAbonnement = "'" + Billet.NumVoyageAbonnement + "'";
                    }
                    if (Billet.NumBilletCommande != "")
                    {
                        numBilletCommande = "'" + Billet.NumBilletCommande + "'";
                    }

                    this.strCommande = "UPDATE `billet` SET `dateDeValidite`='" + Billet.DateDeValidite.ToString("yyyy-MM-dd") + "',";
                    this.strCommande += " `numTrajet`=" + numTrajet + ",`modePaiement`=" + modePaiement + ", `numIndividu`=" + numIndividu + ",";
                    this.strCommande += " `matriculeAgent`= '" + Billet.MatriculeAgent + "', `prixBillet`='" + Billet.PrixBillet + "',";
                    this.strCommande += " `numCalculCategorieBillet`=" + numCalculCategorieBillet + ",`numCalculReductionBillet`=" + numCalculReductionBillet + ",";
                    this.strCommande += " `dateBillet`='" + Billet.DateBillet.ToString("yyyy-MM-dd") + "',`numDureeAbonnement`=" + numDureeAbonnement + ",";
                    this.strCommande += " `numVoyageAbonnement`=" + numVoyageAbonnement + ",`numBilletCommande`=" + numBilletCommande;
                    this.strCommande += " WHERE (`numBillet`='" + Billet.NumBillet + "')";

                    this.serviceConnectBase.openConnection();
                    nombreUpdate = this.serviceConnectBase.requete(this.strCommande);
                    if (nombreUpdate == 1)
                        isUpdate = true;
                    this.serviceConnectBase.closeConnection();
                }
            }
            #endregion

            return isUpdate;
        }

        crlBillet IntfDalBillet.selectBillet(string numBillet)
        {
            #region declaration
            crlBillet Billet = null;
            IntfDalTrajet serviceTrajet = new ImplDalTrajet();
            IntfDalIndividu serviceIndividu = new ImplDalIndividu();
            IntfDalAgent serviceAgent = new ImplDalAgent();
            IntfDalCalculCategorieBillet serviceCalculCategorieBillet = new ImplDalCalculCategorieBillet();
            IntfDalCalculReductionBillet serviceCalculReductionBillet = new ImplDalCalculReductionBillet();
            #endregion

            #region implementation
            if (numBillet != "")
            {
                this.strCommande = "SELECT * FROM `billet` WHERE (`numBillet`='" + numBillet + "')";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            Billet = new crlBillet();
                            Billet.NumBillet = reader["numBillet"].ToString();
                            Billet.DateDeValidite = Convert.ToDateTime(reader["dateDeValidite"].ToString());
                            Billet.NumTrajet = reader["numTrajet"].ToString();
                            Billet.ModePaiement = reader["modePaiement"].ToString();
                            Billet.NumIndividu = reader["numIndividu"].ToString();
                            Billet.MatriculeAgent = reader["matriculeAgent"].ToString();
                            Billet.PrixBillet = reader["prixBillet"].ToString();
                            Billet.NumCalculCategorieBillet = reader["numCalculCategorieBillet"].ToString();
                            Billet.NumCalculReductionBillet = reader["numCalculReductionBillet"].ToString();
                            Billet.DateBillet = Convert.ToDateTime(reader["dateBillet"].ToString());
                            Billet.NumDureeAbonnement = reader["numDureeAbonnement"].ToString();
                            Billet.NumVoyageAbonnement = reader["numVoyageAbonnement"].ToString();
                            Billet.NumBilletCommande = reader["numBilletCommande"].ToString();
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                if (Billet != null)
                {
                    if (Billet.NumTrajet != "")
                    {
                        Billet.trajet = serviceTrajet.selectTrajet(Billet.NumTrajet);
                    }
                    if (Billet.NumIndividu != "")
                    {
                        Billet.individu = serviceIndividu.selectIndividu(Billet.NumIndividu);
                    }
                    if (Billet.MatriculeAgent != "")
                    {
                        Billet.agent = serviceAgent.selectAgent(Billet.MatriculeAgent);
                    }
                    if (Billet.NumCalculCategorieBillet != "")
                    {
                        Billet.calculCategorieBillet = serviceCalculCategorieBillet.selectCalculCategorieBillet(Billet.NumCalculCategorieBillet);
                    }
                    if (Billet.NumCalculReductionBillet != "") 
                    {
                        Billet.calculReductionBillet = serviceCalculReductionBillet.selectCalculReductionBillet(Billet.NumCalculReductionBillet);
                    }
                }
            }
            #endregion

            return Billet;
        }

        crlBillet IntfDalBillet.isValide(string numBillet, string idItineraire)
        {
            #region declaration
            crlBillet Billet = null;
            IntfDalTrajet serviceTrajet = new ImplDalTrajet();
            IntfDalIndividu serviceIndividu = new ImplDalIndividu();
            IntfDalAgent serviceAgent = new ImplDalAgent();
            IntfDalCalculCategorieBillet serviceCalculCategorieBillet = new ImplDalCalculCategorieBillet();
            IntfDalCalculReductionBillet serviceCalculReductionBillet = new ImplDalCalculReductionBillet();
            #endregion

            #region implementation
            if (numBillet != "")
            {
                this.strCommande = "SELECT billet.numBillet, billet.dateDeValidite, billet.numTrajet,";
                this.strCommande += " billet.modePaiement, billet.numIndividu, billet.matriculeAgent, billet.prixBillet,";
                this.strCommande += " billet.numCalculCategorieBillet, billet.numCalculReductionBillet, billet.dateBillet,";
                this.strCommande += " billet.numDureeAbonnement, billet.numVoyageAbonnement, billet.numBilletCommande FROM billet";
                this.strCommande += " Left Join voyage ON voyage.numBillet = billet.numBillet";
                this.strCommande += " Inner Join trajet ON trajet.numTrajet = billet.numTrajet";
                this.strCommande += " Inner Join associationtrajetitineraire ON associationtrajetitineraire.numTrajet = trajet.numTrajet";
                this.strCommande += " WHERE voyage.numBillet IS NULL  AND";
                this.strCommande += " billet.numBillet =  '" + numBillet + "' AND";
                this.strCommande += " billet.dateDeValidite >=  '" + DateTime.Now.ToString("yyyyMMdd") + "' AND";
                this.strCommande += " associationtrajetitineraire.idItineraire = '" + idItineraire + "'";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            Billet = new crlBillet();
                            Billet.NumBillet = reader["numBillet"].ToString();
                            Billet.DateDeValidite = Convert.ToDateTime(reader["dateDeValidite"].ToString());
                            Billet.NumTrajet = reader["numTrajet"].ToString();
                            Billet.ModePaiement = reader["modePaiement"].ToString();
                            Billet.NumIndividu = reader["numIndividu"].ToString();
                            Billet.MatriculeAgent = reader["matriculeAgent"].ToString();
                            Billet.PrixBillet = reader["prixBillet"].ToString();
                            Billet.NumCalculCategorieBillet = reader["numCalculCategorieBillet"].ToString();
                            Billet.NumCalculReductionBillet = reader["numCalculReductionBillet"].ToString();
                            Billet.DateBillet = Convert.ToDateTime(reader["dateBillet"].ToString());
                            Billet.NumDureeAbonnement = reader["numDureeAbonnement"].ToString();
                            Billet.NumVoyageAbonnement = reader["numVoyageAbonnement"].ToString();
                            Billet.NumBilletCommande = reader["numBilletCommande"].ToString();
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                if (Billet != null)
                {
                    if (Billet.NumTrajet != "")
                    {
                        Billet.trajet = serviceTrajet.selectTrajet(Billet.NumTrajet);
                    }
                    if (Billet.NumIndividu != "")
                    {
                        Billet.individu = serviceIndividu.selectIndividu(Billet.NumIndividu);
                    }
                    if (Billet.MatriculeAgent != "")
                    {
                        Billet.agent = serviceAgent.selectAgent(Billet.MatriculeAgent);
                    }
                    if (Billet.NumCalculCategorieBillet != "")
                    {
                        Billet.calculCategorieBillet = serviceCalculCategorieBillet.selectCalculCategorieBillet(Billet.NumCalculCategorieBillet);
                    }
                    if (Billet.NumCalculReductionBillet != "") 
                    {
                        Billet.calculReductionBillet = serviceCalculReductionBillet.selectCalculReductionBillet(Billet.NumCalculReductionBillet);
                    }
                }
            }
            #endregion

            return Billet;
        }

        List<crlBillet> IntfDalBillet.selectBilletsForTrajet(string numTrajet)
        {
            throw new NotImplementedException();
        }

        string IntfDalBillet.getNumBillet(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numBillet = "00001";
            string[] tempNumBillet = null;
            string strDate = "BI" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT billet.numBillet AS maxNum FROM billet";
            this.strCommande += " WHERE billet.numBillet LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumBillet = reader["maxNum"].ToString().ToString().Split('/');
                        numBillet = tempNumBillet[tempNumBillet.Length - 1];
                    }
                    numTemp = double.Parse(numBillet) + 1;
                    if (numTemp < 10)
                        numBillet = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numBillet = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numBillet = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numBillet = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numBillet = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numBillet = strDate + "/" + sigleAgence + "/" + numBillet;
            #endregion

            return numBillet;
        }


        double IntfDalBillet.getTotalBilletEncaisser(string matriculeAgent, DateTime dateDebut, DateTime dateFin)
        {
            #region declaration
            double totalEncaisser = 0.00;
            #endregion

            #region implementation
            if (matriculeAgent != "")
            {
                this.strCommande = "SELECT Sum(billet.prixBillet) AS totalEncaisser FROM billet";
                this.strCommande += " WHERE billet.matriculeAgent = '" + matriculeAgent + "' AND";
                this.strCommande += " billet.modePaiement = 'Espèce' AND";
                this.strCommande += " billet.dateBillet <= '" + dateFin.ToString("yyyy-MM-dd") + "' AND";
                this.strCommande += " billet.dateBillet >= '" + dateDebut.ToString("yyyy-MM-dd") + "'";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            try
                            {
                                totalEncaisser = double.Parse(reader["totalEncaisser"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return totalEncaisser;
        }
        #endregion

        #region insert to grid
        void IntfDalBillet.insertToGridBillet(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalBillet serviceBillet = new ImplDalBillet(); 
            #endregion

            #region implementation

            this.strCommande = "SELECT billet.numBillet, billet.dateDeValidite, billet.numTrajet, billet.modePaiement,";
            this.strCommande += " billet.numIndividu, trajet.numTrajet, billet.prixBillet, trajet.distanceTrajet,";
            this.strCommande += " trajet.dureeTrajet, trajet.numVilleD, trajet.numVilleF,";
            this.strCommande += " individu.nomIndividu, individu.prenomIndividu, individu.cinIndividu FROM billet";
            this.strCommande += " Inner Join trajet ON trajet.numTrajet = billet.numTrajet";
            this.strCommande += " Left Join voyage ON voyage.numBillet = billet.numBillet";
            this.strCommande += " Left Join individu ON individu.numIndividu = billet.numIndividu";
            this.strCommande += " WHERE voyage.numBillet IS NULL  AND " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param + " DESC";

            gridView.DataSource = serviceBillet.getDataTableBillet(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalBillet.getDataTableBillet(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalVille serviceVille = new ImplDalVille();

            crlVille villeD = new crlVille();
            crlVille villeF = new crlVille();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numBillet", typeof(string));
            dataTable.Columns.Add("dateValidite", typeof(DateTime));
            dataTable.Columns.Add("client", typeof(string));
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

                        dr["numBillet"] = reader["numBillet"].ToString();
                        dr["dateValidite"] = Convert.ToDateTime(reader["dateDeValidite"].ToString());
                        dr["client"] = reader["nomIndividu"].ToString() + " " + reader["prenomIndividu"].ToString();
                        
                        villeD = serviceVille.selectVille(reader["numVilleD"].ToString());
                        villeF = serviceVille.selectVille(reader["numVilleF"].ToString());

                        if (villeD != null && villeF != null)
                        {
                            dr["trajet"] = villeD.NomVille + "-" + villeF.NomVille;
                        }
                        else
                        {
                            dr["trajet"] = reader["numVilleD"].ToString() + "-" + reader["numVilleF"].ToString();
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

        void IntfDalBillet.insertToGridBilletInsertToFB(GridView gridView, string param, string paramLike, string valueLike, string numVille)
        {
            #region declaration
            IntfDalBillet serviceBillet = new ImplDalBillet();
            #endregion

            #region implementation

            this.strCommande = "SELECT individu.nomIndividu, individu.prenomIndividu, individu.cinIndividu, individu.numIndividu,";
            this.strCommande += " individu.civiliteIndividu, individu.adresse, individu.profession, individu.telephoneFixeIndividu,";
            this.strCommande += " individu.telephoneMobileIndividu, individu.dateNaissanceIndividu,  individu.lieuNaissanceIndividu,";
            this.strCommande += " individu.mailIndividu, individu.numQuartier, individu.isCheque, individu.isBonCommande,";
            this.strCommande += " trajet.numVilleD, trajet.numVilleF FROM billet";
            this.strCommande += " Inner Join individu ON individu.numIndividu = billet.numIndividu";
            this.strCommande += " Inner Join trajet ON trajet.numTrajet = billet.numTrajet";
            this.strCommande += " Left Join voyage ON voyage.numBillet = billet.numBillet";
            this.strCommande += " WHERE (trajet.numVilleD =  '" + numVille + "' OR trajet.numVilleF =  '" + numVille + "')";
            this.strCommande += " AND billet.dateDeValidite >=  '" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND";
            this.strCommande += " voyage.idVoyage IS NULL   AND " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceBillet.getDataTableBilletInsertToFB(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalBillet.getDataTableBilletInsertToFB(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalVille serviceVille = new ImplDalVille();

            crlVille villeD = new crlVille();
            crlVille villeF = new crlVille();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numBillet", typeof(string));
            dataTable.Columns.Add("dateValidite", typeof(DateTime));
            dataTable.Columns.Add("client", typeof(string));
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

                        dr["numBillet"] = reader["numBillet"].ToString();
                        dr["dateValidite"] = Convert.ToDateTime(reader["dateDeValidite"].ToString());
                        dr["client"] = reader["nomIndividu"].ToString() + " " + reader["prenomIndividu"].ToString();

                        villeD = serviceVille.selectVille(reader["numVilleD"].ToString());
                        villeF = serviceVille.selectVille(reader["numVilleF"].ToString());

                        if (villeD != null && villeF != null)
                        {
                            dr["trajet"] = villeD.NomVille + "-" + villeF.NomVille;
                        }
                        else
                        {
                            dr["trajet"] = reader["numVilleD"].ToString() + "-" + reader["numVilleF"].ToString();
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

        void IntfDalBillet.insertToGridBilletEncaisse(GridView gridView, string param, string paramLike, string valueLike, string matriculeAgent, DateTime dateDebut, DateTime dateFin)
        {
            #region declaration
            IntfDalBillet serviceBillet = new ImplDalBillet();
            #endregion

            #region implementation

            this.strCommande = "SELECT billet.numBillet, billet.dateDeValidite, billet.numTrajet, billet.modePaiement,";
            this.strCommande += " billet.numIndividu, billet.matriculeAgent, billet.prixBillet, billet.numCalculCategorieBillet,";
            this.strCommande += " billet.dateBillet FROM billet";
            this.strCommande += " WHERE billet.matriculeAgent = '" + matriculeAgent + "' AND";
            this.strCommande += " billet.dateBillet <= '" + dateFin.ToString("yyyy-MM-dd") + "' AND";
            this.strCommande += " billet.dateBillet >= '" + dateDebut.ToString("yyyy-MM-dd") + "' AND";
            this.strCommande += " " + paramLike + " LIKE '%" + valueLike + "%' AND";
            this.strCommande += " billet.modePaiement = 'Espèce'";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceBillet.getDataTableBilletEncaisse(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalBillet.getDataTableBilletEncaisse(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();

            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numBillet", typeof(string));
            dataTable.Columns.Add("dateValidite", typeof(DateTime));
            dataTable.Columns.Add("modePaiement", typeof(string));
            dataTable.Columns.Add("prixBillet", typeof(string));
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

                        dr["numBillet"] = reader["numBillet"].ToString();
                        dr["dateValidite"] = Convert.ToDateTime(reader["dateDeValidite"].ToString());
                        dr["modePaiement"] = reader["modePaiement"].ToString();
                        dr["prixBillet"] = serviceGeneral.separateurDesMilles(reader["prixBillet"].ToString()) + "Ar";

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }

        void IntfDalBillet.insertToGridBilletAbonnement(GridView gridView, string param, string paramLike, string valueLike, string numAbonnement)
        {
            #region declaration
            IntfDalBillet serviceBillet = new ImplDalBillet();
            #endregion

            #region implementation

            this.strCommande = "SELECT billet.numBillet, billet.dateDeValidite, billet.numTrajet, billet.modePaiement,";
            this.strCommande += " billet.numIndividu, billet.matriculeAgent, billet.prixBillet, billet.numCalculCategorieBillet,";
            this.strCommande += " billet.dateBillet, trajet.numVilleD,trajet.numVilleF FROM billet";
            this.strCommande += " Inner Join assoabonnementbillet ON assoabonnementbillet.numBillet = billet.numBillet";
            this.strCommande += " Inner Join trajet ON trajet.numTrajet = billet.numTrajet";
            this.strCommande += " WHERE assoabonnementbillet.numAbonnement = '" + numAbonnement + "' AND";
            this.strCommande += " billet.dateDeValidite >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND";
            this.strCommande += " " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceBillet.getDataTableBilletAbonnement(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalBillet.getDataTableBilletAbonnement(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();

            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            IntfDalVille serviceVille = new ImplDalVille();

            crlVille VilleD = null;
            crlVille VilleF = null;
            string strTrajet = "";
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numBillet", typeof(string));
            dataTable.Columns.Add("dateValidite", typeof(DateTime));
            dataTable.Columns.Add("modePaiement", typeof(string));
            dataTable.Columns.Add("prixBillet", typeof(string));
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

                        dr["numBillet"] = reader["numBillet"].ToString();
                        dr["dateValidite"] = Convert.ToDateTime(reader["dateDeValidite"].ToString());
                        dr["modePaiement"] = reader["modePaiement"].ToString();
                        dr["prixBillet"] = serviceGeneral.separateurDesMilles(reader["prixBillet"].ToString()) + "Ar";

                        VilleD = serviceVille.selectVille(reader["numVilleD"].ToString());
                        VilleF = serviceVille.selectVille(reader["numVilleF"].ToString());

                        if (VilleD != null)
                        {
                            strTrajet = VilleD.NomVille + "-";
                        }
                        else 
                        {
                            strTrajet = reader["numVilleD"].ToString() + "-";
                        }

                        if (VilleF != null)
                        {
                            strTrajet += VilleF.NomVille;
                        }
                        else 
                        {
                            strTrajet += reader["numVilleF"].ToString();
                        }

                        dr["trajet"] = strTrajet;

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