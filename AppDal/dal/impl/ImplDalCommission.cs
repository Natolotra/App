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
using System.Collections.Generic;

namespace arch.dal.impl
{
    /// <summary>
    /// Implementation duservice commission
    /// </summary>
    public class ImplDalCommission : IntfDalCommission
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalCommission()
        {
            this.serviceRessource = new ImplDalServiceRessource();
            this.serviceConnectBase = new ImplDalConnectBase(this.serviceRessource.getDefaultStrConnection());
        }
        public ImplDalCommission(string strConnection)
        {
            this.serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region IntfDalCommission Membres

        string IntfDalCommission.insertCommission(crlCommission Commission)
        {
            #region declaration
            IntfDalCommission serviceCommission = new ImplDalCommission();
            int nombreInsertion = 0;
            string idCommission = "";
            string matriculeAgentDelivreur = "";
            string modePaiement = "";
            string numRecepteur = "";
            #endregion

            #region implementation
            if (Commission != null)
            {
                if (Commission.MatriculeAgentDelivreur != "")
                {
                    matriculeAgentDelivreur = "'" + Commission.MatriculeAgentDelivreur + "'";
                }
                else
                {
                    matriculeAgentDelivreur = "NULL";
                }

                if (Commission.ModePaiement != "")
                {
                    modePaiement = "'" + Commission.ModePaiement + "'";
                }
                else
                {
                    modePaiement = "NULL";
                }

                if (Commission.NumRecepteur != "")
                {
                    numRecepteur = "'" + Commission.NumRecepteur + "'";
                }
                else
                {
                    numRecepteur = "NULL";
                }
               
                Commission.IdCommission = serviceCommission.getidCommission(Commission.agent.agence.SigleAgence);
                this.strCommande = "INSERT INTO `commission` (`idCommission`,`dateCommission`,`matriculeAgent`,`destination`,`poids`,`nombre`,";
                this.strCommande += "`pieceJustificatif`, `fraisEnvoi`,`numExpediteur`,`numRecepteur`,`numDesignation`,`typeCommission`,`isRecu`,";
                this.strCommande += "`numTrajet`,`matriculeAgentDelivreur`,`modePaiement`,`dateLivraison`) VALUES ";
                this.strCommande += "('" + Commission.IdCommission + "','" + Commission.DateCommission.ToString("yyyy-MM-dd") + "',";
                this.strCommande += "'" + Commission.MatriculeAgent + "','" + Commission.Destination + "',"; 
                this.strCommande += "'" + Commission.Poids + "','" + Commission.Nombre + "','" + Commission.PieceJustificatif + "',";
                this.strCommande += "'" + Commission.FraisEnvoi + "', '" + Commission.NumExpediteur + "', " + numRecepteur + ",";
                this.strCommande += "'" + Commission.NumDesignation + "', '" + Commission.TypeCommission + "','" + Commission.IsRecu + "',";
                this.strCommande += "'" + Commission.NumTrajet + "'," + matriculeAgentDelivreur + "," + modePaiement + ",";
                this.strCommande += "'" + Commission.DateLivraison.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    idCommission = Commission.IdCommission;
                this.serviceConnectBase.closeConnection();
               
            }
            #endregion

            return idCommission;
        }

        string IntfDalCommission.insertCommissionAll(crlCommission Commission)
        {
            #region declaration
            string idCommission = "";
            IntfDalRecu serviceRecu = new ImplDalRecu();
            IntfDalClient serviceClient = new ImplDalClient();
            IntfDalReceptionnaire serviceReceptionnaire = new ImplDalReceptionnaire();
            IntfDalCommission serviceCommission = new ImplDalCommission();
            #endregion

            #region implementation
            if (Commission != null)
            {
                if (Commission.expediteur != null && Commission.recepteur != null && Commission.agent != null)
                {
                    Commission.recepteur.IdPersonne = serviceReceptionnaire.isPersonne(Commission.recepteur);
                    Commission.expediteur.NumClient = serviceClient.isClient(Commission.expediteur);

                    

                    if (Commission.recepteur.IdPersonne != "")
                    {
                        serviceReceptionnaire.updatePersonne(Commission.recepteur);
                        Commission.NumRecepteur = Commission.recepteur.IdPersonne;
                    }
                    else
                    {
                        Commission.recepteur.IdPersonne = serviceReceptionnaire.insertPersonne(Commission.recepteur, Commission.agent.agence.SigleAgence);
                        if (Commission.recepteur.IdPersonne != "")
                        {
                            Commission.NumRecepteur = Commission.recepteur.IdPersonne;
                        }
                    }

                    if (Commission.expediteur.NumClient != "")
                    {
                        serviceClient.updateClient(Commission.expediteur);
                        Commission.NumExpediteur = Commission.expediteur.NumClient;
                    }
                    else
                    {
                        Commission.expediteur.NumClient = serviceClient.insertClient(Commission.expediteur, Commission.agent.agence.SigleAgence);
                        if (Commission.expediteur.NumClient != "")
                        {
                            Commission.NumExpediteur = Commission.expediteur.NumClient;
                        }
                    }

                    

                    Commission.IdCommission = serviceCommission.isCommission(Commission);
                    
                    if (Commission.IdCommission.Equals(""))
                    {
                        if (Commission.NumExpediteur != "" && Commission.NumRecepteur != "")
                        {
                            Commission.IdCommission = serviceCommission.insertCommission(Commission);
                            if (Commission.IdCommission != "")
                            {
                                idCommission = Commission.IdCommission;
                            }
                        }
                    }
                }
            }
            #endregion

            return idCommission;
        }

        bool IntfDalCommission.insertCommissionToFB(string idCommission, string numerosFB)
        {
            #region declaration
            bool isInsert = false;
            int nombreInsert = 0;
            #endregion

            #region implementation
            if (idCommission != "" && numerosFB != "")
            {
                this.strCommande = "INSERT INTO `associationfichebordcommission`";
                this.strCommande += " (`idCommission`,`numerosFB`) VALUES";
                this.strCommande += " ('" + idCommission + "','" + numerosFB + "')";
                this.serviceConnectBase.openConnection();
                nombreInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsert > 0)
                {
                    isInsert = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isInsert;
        }

        bool IntfDalCommission.deleteCommission(crlCommission Commission)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (Commission != null)
            {
                if (Commission.IdCommission != "")
                {
                    this.strCommande = "DELETE FROM `commission` WHERE (`idCommission` = '" + Commission.IdCommission + "')";
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

        bool IntfDalCommission.deleteCommissionToFB(string idCommission, string numerosFB)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation

            if (idCommission != "")
            {
                this.strCommande = "DELETE FROM `associationfichebordcommission` WHERE (`idCommission` = '" + idCommission + "' AND `numerosFB`='" + numerosFB + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete == 1)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }

            #endregion

            return isDelete;
        }

        bool IntfDalCommission.deleteCommission(string idCommission)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation

            if (idCommission != "")
            {
                this.strCommande = "DELETE FROM `commission` WHERE (`idCommission` = '" + idCommission + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete == 1)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }
           
            #endregion

            return isDelete;
        }

        bool IntfDalCommission.updateCommission(crlCommission Commission)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            string matriculeAgentDelivreur = "";
            string modePaiement = "";
            string numRecepteur = "";
            #endregion

            #region implementation
            if (Commission != null)
            {
                if (Commission.IdCommission != "")
                {
                    if (Commission.MatriculeAgentDelivreur != "")
                    {
                        matriculeAgentDelivreur = "'" + Commission.MatriculeAgentDelivreur + "'";
                    }
                    else
                    {
                        matriculeAgentDelivreur = "NULL";
                    }

                    if (Commission.ModePaiement != "")
                    {
                        modePaiement = "'" + Commission.ModePaiement + "'";
                    }
                    else
                    {
                        modePaiement = "NULL";
                    }
                    if (Commission.NumRecepteur != "")
                    {
                        numRecepteur = "'" + Commission.NumRecepteur + "'";
                    }
                    else
                    {
                        numRecepteur = "NULL";
                    }

                    this.strCommande = "UPDATE `commission` SET `dateCommission`='" + Commission.DateCommission.ToString("yyyy-MM-dd") + "',";
                    this.strCommande += "`matriculeAgent`='" + Commission.MatriculeAgent + "',`matriculeAgentDelivreur`=" + matriculeAgentDelivreur + ",";
                    this.strCommande += "`destination`='" + Commission.Destination + "', `poids`='" + Commission.Poids + "', `nombre`='" + Commission.Nombre + "',";
                    this.strCommande += "`pieceJustificatif`='" + Commission.PieceJustificatif + "', `fraisEnvoi`='" + Commission.FraisEnvoi + "', ";
                    this.strCommande += "`numExpediteur`='" + Commission.NumExpediteur + "', `numRecepteur`=" + numRecepteur + ", ";
                    this.strCommande += "`numDesignation`='" + Commission.NumDesignation + "', `typeCommission`='" + Commission.TypeCommission + "', ";
                    this.strCommande += "`modePaiement`=" + modePaiement + ",`dateLivraison`='" + Commission.DateLivraison.ToString("yyyy-MM-dd") + "',";
                    this.strCommande += "`isRecu`='" + Commission.IsRecu + "', `numTrajet`='" + Commission.NumTrajet + "' ";
                    this.strCommande += "WHERE (`idCommission`='" + Commission.IdCommission + "')";

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

        bool IntfDalCommission.updateCommissionAll(crlCommission Commission)
        {
            #region declaration
            bool isUpdate = false;
            IntfDalRecu serviceRecu = new ImplDalRecu();
            IntfDalClient serviceClient = new ImplDalClient();
            IntfDalReceptionnaire serviceReceptionnaire = new ImplDalReceptionnaire();
            IntfDalCommission serviceCommission = new ImplDalCommission();

            int isClient = 0;
            int isCommission = 0;
            #endregion

            #region implementation
            if (Commission != null)
            {
                if (Commission.expediteur != null && Commission.recepteur != null)
                {
                    isClient = serviceReceptionnaire.isPersonneInt(Commission.recepteur);

                    if (isClient == 0)
                    {
                        serviceReceptionnaire.updatePersonne(Commission.recepteur);
                        isClient = 0;
                    }

                    isClient = serviceClient.isClientInt(Commission.expediteur);

                    if (isClient == 0)
                    {
                        serviceClient.updateClient(Commission.expediteur);
                    }

                    isCommission = serviceCommission.isCommissionInt(Commission);

                    if (isCommission == 0)
                    {
                        isUpdate = serviceCommission.updateCommission(Commission);
                    }

                }
            }
            #endregion

            return isUpdate;
        }


        //tsy ilaina//
        string IntfDalCommission.isCommission(crlCommission Commission)
        {
            #region declaration
            string idCommission = "";
            #endregion

            #region implementation
            this.strCommande = "SELECT commission.idCommission, commission.numRecu, ";
            this.strCommande += " commission.destination, commission.poids, commission.nombre, commission.pieceJustificatif, commission.fraisEnvoi,";
            this.strCommande += " commission.numExpediteur, commission.numRecepteur, commission.numDesignation, commission.typeCommission ";
            this.strCommande += " FROM commission Inner Join fichebord ON commission.numerosFB = fichebord.numerosFB";
            this.strCommande += " WHERE fichebord.dateHeurDepart LIKE '%" + DateTime.Now.ToString("yyyy-MM-dd") + "%'";

            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(this.strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (Commission.NumRecepteur.Equals(reader["numExpediteur"].ToString()) && Commission.recepteur.Equals(reader["numRecepteur"].ToString()) &&
                            Commission.TypeCommission.Equals(reader["typeCommission"].ToString()) && Commission.NumDesignation.Equals(reader["numDesignation"].ToString()))
                        {
                            idCommission = reader["idCommission"].ToString();
                        }
                    }
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            #endregion

            return idCommission;
        }

        int IntfDalCommission.isCommissionInt(crlCommission Commission)
        {
            #region declaration
            int isCommission = 0;
            #endregion

            #region implementation
            this.strCommande = "SELECT commission.idCommission, commission.numRecu, ";
            this.strCommande += " commission.destination, commission.poids, commission.nombre, commission.pieceJustificatif, commission.fraisEnvoi,";
            this.strCommande += " commission.numExpediteur, commission.numRecepteur, commission.numDesignation, commission.typeCommission ";
            this.strCommande += " FROM commission Inner Join fichebord ON commission.numerosFB = fichebord.numerosFB";
            this.strCommande += " WHERE fichebord.dateHeurDepart LIKE '%" + DateTime.Now.ToString("yyyy-MM-dd") + "%'";


            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(this.strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (Commission.NumRecepteur.Equals(reader["numExpediteur"].ToString()) && Commission.recepteur.Equals(reader["numRecepteur"].ToString()) &&
                           Commission.TypeCommission.Equals(reader["typeCommission"].ToString()) && Commission.NumDesignation.Equals(reader["numDesignation"].ToString()))
                        {
                            isCommission = 1;
                        }
                    }
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            #endregion

            return isCommission;
        }
        //atreto//


        crlCommission IntfDalCommission.selectCommission(string idCommission)      
        {
            #region initialisation
            IntfDalClient serviceClient = new ImplDalClient();
            IntfDalReceptionnaire serviceReceptionnaire = new ImplDalReceptionnaire();
            IntfDalDesignationCommission serviceDesignationCommission = new ImplDalDesignationCommission();
            crlCommission Commission = null;
            IntfDalAgent serviceAgent = new ImplDalAgent();
            #endregion

            #region implementation
            if (idCommission != "") 
            {
                this.strCommande = "SELECT * FROM `commission` WHERE (`idCommission`='" + idCommission + "')";
                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null) 
                {
                    if (reader.HasRows) 
                    {
                        reader.Read();
                        Commission = new crlCommission();
                        Commission.Destination = reader["destination"].ToString();
                        Commission.FraisEnvoi = reader["fraisEnvoi"].ToString();
                        Commission.IdCommission = reader["idCommission"].ToString();
                        Commission.NumExpediteur = reader["numExpediteur"].ToString();
                        Commission.NumRecepteur = reader["numRecepteur"].ToString();
                        Commission.PieceJustificatif = reader["pieceJustificatif"].ToString();
                        Commission.Poids = reader["poids"].ToString();
                        Commission.TypeCommission = reader["typeCommission"].ToString();
                        Commission.NumDesignation = reader["numDesignation"].ToString();
                        try
                        {
                            Commission.Nombre = int.Parse(reader["nombre"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            Commission.IsRecu = int.Parse(reader["isRecu"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        Commission.NumTrajet = reader["numTrajet"].ToString();
                        try
                        {
                            Commission.DateCommission = Convert.ToDateTime(reader["dateCommission"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            Commission.DateLivraison = Convert.ToDateTime(reader["dateLivraison"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        Commission.MatriculeAgent = reader["matriculeAgent"].ToString();
                        Commission.MatriculeAgentDelivreur = reader["matriculeAgentDelivreur"].ToString();
                        Commission.ModePaiement = reader["modePaiement"].ToString();
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (Commission != null)
                {
                    Commission.expediteur = serviceClient.selectClient(Commission.NumExpediteur);
                    Commission.recepteur = serviceReceptionnaire.selectPersonne(Commission.NumRecepteur);
                    Commission.typeCommssionObjet = new crlTypeCommssion();
                    Commission.typeCommssionObjet.TypeCommission = Commission.TypeCommission;
                    Commission.designationCommission = serviceDesignationCommission.selectDesignationCommission(Commission.NumDesignation);
                    Commission.agent = serviceAgent.selectAgent(Commission.MatriculeAgent);
                    Commission.agentDelivreur = serviceAgent.selectAgent(Commission.MatriculeAgentDelivreur);
                }
            }
            #endregion

            return Commission;
        }

        string IntfDalCommission.getidCommission(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string idCommission = "00001";
            string[] tempIdCommission = null;
            string strDate = "CO" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT commission.idCommission AS maxNum FROM commission";
            this.strCommande += " WHERE commission.idCommission LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempIdCommission = reader["maxNum"].ToString().ToString().Split('/');
                        idCommission = tempIdCommission[tempIdCommission.Length - 1];
                    }
                    numTemp = double.Parse(idCommission) + 1;
                    if (numTemp < 10)
                        idCommission = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        idCommission = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        idCommission = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        idCommission = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        idCommission = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            idCommission = strDate + "/" + sigleAgence + "/" + idCommission;
            #endregion

            return idCommission;
        }

        void IntfDalCommission.loadDdlTri(DropDownList ddlTri)
        {
            throw new NotImplementedException();
        }

        List<crlCommission> IntfDalCommission.selectCommissionFB(string numerosFB)
        {
            #region initialisation
            IntfDalAgent serviceAgent = new ImplDalAgent();
            IntfDalClient serviceClient = new ImplDalClient();
            IntfDalReceptionnaire serviceReceptionnaire = new ImplDalReceptionnaire();
            IntfDalDesignationCommission serviceDesignationCommission = new ImplDalDesignationCommission();
            List<crlCommission> Commissions = null;
            crlCommission tempCommission = null;
            #endregion

            #region implementation
            if (numerosFB != "")
            {
                this.strCommande = "SELECT commission.idCommission, commission.destination,";
                this.strCommande += " commission.poids, commission.nombre, commission.pieceJustificatif, commission.fraisEnvoi,";
                this.strCommande += " commission.numExpediteur, commission.numRecepteur, commission.numDesignation,";
                this.strCommande += " commission.dateCommission, commission.matriculeAgent, commission.matriculeAgentDelivreur";
                this.strCommande += "commission.dateLivraison, commission.modePaiement";
                this.strCommande += " commission.typeCommission, commission.isRecu, commission.numTrajet FROM commission";
                this.strCommande += " Inner Join associationfichebordcommission ON associationfichebordcommission.idCommission = commission.idCommission";
                this.strCommande += " WHERE associationfichebordcommission.numerosFB ='" + numerosFB + "')";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        Commissions = new List<crlCommission>();
                        while (reader.Read())
                        {
                            tempCommission = new crlCommission();
                            tempCommission.Destination = reader["destination"].ToString();
                            tempCommission.FraisEnvoi = reader["fraisEnvoi"].ToString();
                            tempCommission.IdCommission = reader["idCommission"].ToString();
                            tempCommission.NumExpediteur = reader["numExpediteur"].ToString();
                            tempCommission.NumRecepteur = reader["numRecepteur"].ToString();
                            tempCommission.PieceJustificatif = reader["pieceJustificatif"].ToString();
                            tempCommission.Poids = reader["poids"].ToString();
                            try
                            {
                                tempCommission.Nombre = int.Parse(reader["nombre"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            tempCommission.TypeCommission = reader["typeCommission"].ToString();
                            tempCommission.NumDesignation = reader["numDesignation"].ToString();
                            try
                            {
                                tempCommission.IsRecu = int.Parse(reader["isRecu"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            tempCommission.NumTrajet = reader["numTrajet"].ToString();
                            try
                            {
                                tempCommission.DateCommission = Convert.ToDateTime(reader["dateCommission"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                tempCommission.DateLivraison = Convert.ToDateTime(reader["dateLivraison"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            tempCommission.MatriculeAgent = reader["matriculeAgent "].ToString();
                            tempCommission.MatriculeAgentDelivreur = reader["matriculeAgentDelivreur"].ToString();
                            tempCommission.ModePaiement = reader["modePaiement"].ToString();

                            Commissions.Add(tempCommission);
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (Commissions != null)
                {
                    for (int i = 0; i < Commissions.Count; i++)
                    {
                        Commissions[i].recepteur = serviceReceptionnaire.selectPersonne(Commissions[i].NumRecepteur);
                        Commissions[i].expediteur = serviceClient.selectClient(Commissions[i].NumExpediteur);
                        Commissions[i].typeCommssionObjet = new crlTypeCommssion();
                        Commissions[i].typeCommssionObjet.TypeCommission = Commissions[i].TypeCommission;
                        Commissions[i].designationCommission = serviceDesignationCommission.selectDesignationCommission(Commissions[i].NumDesignation);
                        Commissions[i].agent = serviceAgent.selectAgent(Commissions[i].MatriculeAgent);
                        Commissions[i].agentDelivreur = serviceAgent.selectAgent(Commissions[i].MatriculeAgentDelivreur);
                    }
                }
            }
            #endregion

            return Commissions;
        }
        #endregion

        #region insert into grid view commission
        void IntfDalCommission.insertToGridCommissionFB(GridView gridView, string param, string paramLike, string valueLike, string numerosFB)
        {
            #region declaration
            IntfDalCommission serviceCommission = new ImplDalCommission();
            #endregion

            #region implementation

            this.strCommande = "SELECT commission.idCommission, commission.typeCommission,";
            this.strCommande += " commission.fraisEnvoi, client.nomClient, client.prenomClient, commission.poids,";
            this.strCommande += " commission.nombre, designationcommission.designation, commission.destination,";
            this.strCommande += " receptionnaire.nomPersonne, receptionnaire.prenomPersonne FROM commission";
            this.strCommande += " Inner Join client ON client.numClient = commission.numExpediteur";
            this.strCommande += " Inner Join designationcommission ON designationcommission.numDesignation = commission.numDesignation";
            this.strCommande += " Left Join associationfichebordcommission ON associationfichebordcommission.idCommission = commission.idCommission";
            this.strCommande += " Left Join receptionnaire ON receptionnaire.idPersonne = commission.numRecepteur";
            this.strCommande += " WHERE associationfichebordcommission.numerosFB = '" + numerosFB + "'";
            this.strCommande += " AND " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceCommission.getDataTableCommissionFB(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalCommission.getDataTableCommissionFB(string strCommande)
        {
            #region declaration
            DataTable dataTable = new DataTable();

            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            #endregion

            #region implementation
            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("id", typeof(string));
            dataTable.Columns.Add("type", typeof(string));
            dataTable.Columns.Add("poids", typeof(string));
            dataTable.Columns.Add("nombre", typeof(string));
            dataTable.Columns.Add("client", typeof(string));
            dataTable.Columns.Add("recepteur", typeof(string));
            dataTable.Columns.Add("frais", typeof(string));
            dataTable.Columns.Add("destination", typeof(string));
            DataRow dr;
            #endregion


            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dr = dataTable.NewRow();

                        dr["id"] = reader["idCommission"].ToString();
                        dr["type"] = reader["typeCommission"].ToString();
                        dr["poids"] = reader["poids"].ToString() + "Kg";
                        dr["nombre"] = reader["nombre"].ToString();
                        dr["frais"] = serviceGeneral.separateurDesMilles(reader["fraisEnvoi"].ToString()) + "Ar";
                        dr["client"] = reader["nomClient"].ToString() + " " + reader["prenomClient"].ToString();
                        dr["recepteur"] = reader["nomPersonne"].ToString() + " " + reader["prenomPersonne"].ToString();
                        dr["destination"] = reader["destination"].ToString();
                        dataTable.Rows.Add(dr);
                    }
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }


        void IntfDalCommission.insertToGridCommission(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalCommission serviceCommission = new ImplDalCommission();
            #endregion

            #region implementation

            this.strCommande = "SELECT commission.idCommission, commission.typeCommission, commission.destination,";
            this.strCommande += " receptionnaire.nomPersonne, receptionnaire.prenomPersonne, client.nomClient,";
            this.strCommande += " client.prenomClient, commission.poids, commission.nombre, commission.fraisEnvoi";
            this.strCommande += " FROM commission";
            this.strCommande += " Left Join associationfichebordcommission ON associationfichebordcommission.idCommission = commission.idCommission";
            this.strCommande += " Inner Join receptionnaire ON receptionnaire.idPersonne = commission.numRecepteur";
            this.strCommande += " Inner Join client ON client.numClient = commission.numExpediteur";
            this.strCommande += " WHERE associationfichebordcommission.numerosFB IS NULL";
            this.strCommande += " AND " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param + " DESC";

            gridView.DataSource = serviceCommission.getDataTableCommission(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalCommission.getDataTableCommission(string strCommande)
        {
            #region declaration
            DataTable dataTable = new DataTable();

            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            #endregion

            #region implementation
            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("id", typeof(string));
            dataTable.Columns.Add("type", typeof(string));
            dataTable.Columns.Add("poids", typeof(string));
            dataTable.Columns.Add("nombre", typeof(string));
            dataTable.Columns.Add("client", typeof(string));
            dataTable.Columns.Add("recepteur", typeof(string));
            dataTable.Columns.Add("frais", typeof(string));
            DataRow dr;
            #endregion


            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        dr = dataTable.NewRow();

                        dr["id"] = reader["idCommission"].ToString();
                        dr["type"] = reader["typeCommission"].ToString();
                        dr["poids"] = reader["poids"].ToString() + "Kg";
                        dr["nombre"] = reader["nombre"].ToString();
                        dr["frais"] = serviceGeneral.separateurDesMilles(reader["fraisEnvoi"].ToString()) + "Ar";
                        dr["client"] = reader["nomClient"].ToString() + " " + reader["prenomClient"].ToString();
                        dr["recepteur"] = reader["nomPersonne"].ToString() + " " + reader["prenomPersonne"].ToString();

                        dataTable.Rows.Add(dr);
                    }
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }

        void IntfDalCommission.insertToGridCommissionNonFB(GridView gridView, string param, string paramLike, string valueLike, string idItineraire, string numerosGareRoutiere)
        {
            #region declaration
            IntfDalCommission serviceCommission = new ImplDalCommission();
            #endregion

            #region implementation

            this.strCommande = "SELECT commission.idCommission, commission.typeCommission,";
            this.strCommande += " commission.fraisEnvoi, client.nomClient, client.prenomClient, commission.poids,";
            this.strCommande += " commission.nombre, designationcommission.designation, commission.destination,";
            this.strCommande += " receptionnaire.nomPersonne, receptionnaire.prenomPersonne FROM commission";
            this.strCommande += " Inner Join client ON client.numClient = commission.numExpediteur";
            this.strCommande += " Inner Join designationcommission ON designationcommission.numDesignation = commission.numDesignation";
            this.strCommande += " Left Join associationfichebordcommission ON associationfichebordcommission.idCommission = commission.idCommission";
            this.strCommande += " Left Join receptionnaire ON receptionnaire.idPersonne = commission.numRecepteur";
            this.strCommande += " Inner Join associationtrajetitineraire ON associationtrajetitineraire.numTrajet = commission.numTrajet";
            this.strCommande += " Inner Join agent ON agent.matriculeAgent = commission.matriculeAgent";
            this.strCommande += " WHERE associationtrajetitineraire.idItineraire = '" + idItineraire + "' AND";
            this.strCommande += " associationfichebordcommission.numerosFB IS NULL ";
            this.strCommande += " AND " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " AND agent.numAgence = '" + numerosGareRoutiere + "'";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceCommission.getDataTableCommissionNonFB(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalCommission.getDataTableCommissionNonFB(string strCommande)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            #endregion

            #region implementation
            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("id", typeof(string));
            dataTable.Columns.Add("type", typeof(string));
            dataTable.Columns.Add("poids", typeof(string));
            dataTable.Columns.Add("nombre", typeof(string));
            dataTable.Columns.Add("client", typeof(string));
            dataTable.Columns.Add("recepteur", typeof(string));
            dataTable.Columns.Add("frais", typeof(string));
            dataTable.Columns.Add("destination", typeof(string));
            DataRow dr;
            #endregion


            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dr = dataTable.NewRow();

                        dr["id"] = reader["idCommission"].ToString();
                        dr["type"] = reader["typeCommission"].ToString();
                        dr["poids"] = reader["poids"].ToString() + "Kg";
                        dr["nombre"] = reader["nombre"].ToString();
                        dr["frais"] = serviceGeneral.separateurDesMilles(reader["fraisEnvoi"].ToString()) + "Ar";
                        dr["client"] = reader["nomClient"].ToString() + " " + reader["prenomClient"].ToString();
                        dr["recepteur"] = reader["nomPersonne"].ToString() + " " + reader["prenomPersonne"].ToString();
                        dr["destination"] = reader["destination"].ToString();
                        dataTable.Rows.Add(dr);
                    }
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }
        #endregion


        void IntfDalCommission.insertToGridCommissionAutorisationDepart(GridView gridView, string param, string paramLike, string valueLike, string numerosFB)
        {
            #region declaration
            IntfDalCommission serviceCommission = new ImplDalCommission();
            #endregion

            #region implementation

            this.strCommande = "SELECT commission.typeCommission, designationcommission.designation, commission.poids,";
            this.strCommande += " client.nomClient, client.prenomClient, receptionnaire.nomPersonne, receptionnaire.prenomPersonne,";
            this.strCommande += " commission.pieceJustificatif, commission.fraisEnvoi FROM commission";
            this.strCommande += " Inner Join designationcommission ON designationcommission.numDesignation = commission.numDesignation";
            this.strCommande += " Inner Join client ON client.numClient = commission.numExpediteur";
            this.strCommande += " Left Join receptionnaire ON receptionnaire.idPersonne = commission.numRecepteur";
            this.strCommande += " Inner Join associationfichebordcommission ON associationfichebordcommission.idCommission = commission.idCommission";
            this.strCommande += " WHERE associationfichebordcommission.numerosFB = '" + numerosFB + "'";
            this.strCommande += " AND " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceCommission.getDataTableCommissionAutorisationDepar(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalCommission.getDataTableCommissionAutorisationDepar(string strCommande)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            #endregion

            #region implementation
            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("type", typeof(string));
            dataTable.Columns.Add("designation", typeof(string));
            dataTable.Columns.Add("poids", typeof(string));
            dataTable.Columns.Add("expediteur", typeof(string));
            dataTable.Columns.Add("recepteur", typeof(string));
            dataTable.Columns.Add("piece", typeof(string));
            dataTable.Columns.Add("frais", typeof(string));
            DataRow dr;
            #endregion


            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dr = dataTable.NewRow();

                        dr["type"] = reader["typeCommission"].ToString();
                        dr["designation"] = reader["designation"].ToString();
                        
                        dr["poids"] = reader["poids"].ToString() + "Kg";
                        dr["expediteur"] = reader["nomClient"].ToString() + " " + reader["prenomClient"].ToString();
                        dr["recepteur"] = reader["nomPersonne"].ToString() + " " + reader["prenomPersonne"].ToString();
                        dr["piece"] = reader["pieceJustificatif"].ToString();
                        dr["frais"] = serviceGeneral.separateurDesMilles(reader["fraisEnvoi"].ToString()) + "Ar";

                        dataTable.Rows.Add(dr);
                    }
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }


        void IntfDalCommission.insertToGridCommissionArrive(GridView gridView, string param, string paramLike, string valueLike, string numVille)
        {
            #region declaration
            IntfDalCommission serviceCommission = new ImplDalCommission();
            #endregion

            #region implementation

            this.strCommande = "SELECT commission.idCommission, commission.destination, commission.poids, commission.nombre,";
            this.strCommande += " commission.pieceJustificatif, commission.fraisEnvoi, commission.numExpediteur, commission.numRecepteur,";
            this.strCommande += " commission.numDesignation, commission.typeCommission, commission.numTrajet, commission.dateCommission,";
            this.strCommande += " commission.matriculeAgent, commission.matriculeAgentDelivreur, commission.dateLivraison, commission.isRecu,";
            this.strCommande += " commission.modePaiement, client.numClient, client.nomClient, client.prenomClient, client.adresseClient,";
            this.strCommande += " client.cinClient, client.telephoneClient, client.mobileClient, client.isCheque, client.isBonCommande,";
            this.strCommande += " receptionnaire.idPersonne, receptionnaire.nomPersonne, receptionnaire.prenomPersonne,";
            this.strCommande += " receptionnaire.adressePersonne, receptionnaire.telephone, vehicule.numVehicule, vehicule.numParamVehicule,";
            this.strCommande += " vehicule.sourceEnergie, vehicule.numProprietaire, vehicule.matriculeVehicule, vehicule.marqueVehicule,";
            this.strCommande += " vehicule.typeVehicule, vehicule.numSerieVehicule, vehicule.numMoteurVehicule, vehicule.puissanceVehicule,";
            this.strCommande += " vehicule.couleurVehicule, vehicule.placesAssiseVehicule, vehicule.nombreColoneVehicule, vehicule.poidsTotalVehicule,";
            this.strCommande += " vehicule.poidsVideVehicule, vehicule.imageVehicule FROM commission";
            this.strCommande += " Inner Join associationfichebordcommission ON associationfichebordcommission.idCommission = commission.idCommission";
            this.strCommande += " Inner Join fichebord ON fichebord.numerosFB = associationfichebordcommission.numerosFB";
            this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.numerosAV = fichebord.numerosAV";
            this.strCommande += " Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
            this.strCommande += " Inner Join itineraire ON itineraire.idItineraire = verification.idItineraire";
            this.strCommande += " Inner Join associationtrajetitineraire ON associationtrajetitineraire.idItineraire = itineraire.idItineraire";
            this.strCommande += " Inner Join trajet ON trajet.numTrajet = associationtrajetitineraire.numTrajet";
            this.strCommande += " Inner Join agent ON agent.matriculeAgent = verification.matriculeAgent";
            this.strCommande += " Inner Join agence ON agence.numAgence = agent.numAgence";
            this.strCommande += " Inner Join client ON client.numClient = commission.numExpediteur";
            this.strCommande += " Left Join receptionnaire ON receptionnaire.idPersonne = commission.numRecepteur";
            this.strCommande += " Inner Join licence ON licence.numLicence = verification.numLicence";
            this.strCommande += " Inner Join vehicule ON vehicule.numVehicule = licence.numVehicule";
            this.strCommande += " WHERE (trajet.numVilleD = '" + numVille + "' OR";
            this.strCommande += " trajet.numVilleF = '" + numVille + "') AND";
            this.strCommande += " agence.numVille <>  '" + numVille + "'";
            this.strCommande += " AND " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " GROUP BY commission.idCommission";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceCommission.getDataTableCommissionArrive(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalCommission.getDataTableCommissionArrive(string strCommande)
        {
            throw new NotImplementedException();
        }


        void IntfDalCommission.insertToGridCommissionDepart(GridView gridView, string param, string paramLike, string valueLike, string numVille)
        {
            #region declaration
            IntfDalCommission serviceCommission = new ImplDalCommission();
            #endregion

            #region implementation

            this.strCommande = "SELECT commission.idCommission, commission.destination, commission.poids,";
            this.strCommande += " commission.nombre, commission.pieceJustificatif, commission.fraisEnvoi,";
            this.strCommande += " commission.numExpediteur, commission.numRecepteur, designationcommission.designation,";
            this.strCommande += " commission.numDesignation, commission.typeCommission,";
            this.strCommande += " commission.numTrajet, commission.dateCommission, commission.matriculeAgent,";
            this.strCommande += " commission.matriculeAgentDelivreur, commission.dateLivraison,";
            this.strCommande += " commission.isRecu, commission.modePaiement FROM commission";
            this.strCommande += " Inner Join agent ON agent.matriculeAgent = commission.matriculeAgent";
            this.strCommande += " Inner Join agence ON agence.numAgence = agent.numAgence";
            this.strCommande += " Inner Join designationcommission ON designationcommission.numDesignation = commission.numDesignation";
            this.strCommande += " Left Join associationfichebordcommission ON associationfichebordcommission.idCommission = commission.idCommission";
            this.strCommande += " WHERE agence.numVille = '" + numVille + "'";
            this.strCommande += " AND associationfichebordcommission.numerosFB IS NULL";
            this.strCommande += " AND " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " GROUP BY commission.idCommission";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceCommission.getDataTableCommissionDepart(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalCommission.getDataTableCommissionDepart(string strCommande)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            #endregion

            #region implementation
            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("idCommission", typeof(string));
            dataTable.Columns.Add("destination", typeof(string));
            dataTable.Columns.Add("typeCommission", typeof(string));
            dataTable.Columns.Add("designation", typeof(string));
            dataTable.Columns.Add("poids", typeof(string));
            dataTable.Columns.Add("nombre", typeof(string));
            DataRow dr;
            #endregion


            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dr = dataTable.NewRow();

                        dr["idCommission"] = reader["idCommission"].ToString();
                        dr["destination"] = reader["destination"].ToString();
                        dr["typeCommission"] = reader["typeCommission"].ToString();
                        dr["designation"] = reader["designation"].ToString();
                        dr["poids"] = reader["poids"].ToString() + "Kg";
                        dr["nombre"] = reader["nombre"].ToString();
                       

                        dataTable.Rows.Add(dr);
                    }
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }
    }
}
