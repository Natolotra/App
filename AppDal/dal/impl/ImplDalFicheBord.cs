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
    /// Implementation du service fiche de bord
    /// </summary>
    public class ImplDalFicheBord : IntfDalFicheBord
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalFicheBord()
        {
            this.serviceRessource = new ImplDalServiceRessource();
            this.serviceConnectBase = new ImplDalConnectBase(this.serviceRessource.getDefaultStrConnection());
        }
        public ImplDalFicheBord(string strConnection)
        {
            this.serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region IntfDalFicheBord Members

        string IntfDalFicheBord.insertFicheBord(crlFicheBord FicheBord)
        {
            #region declaration
            int nombreInsertion = 0;
            string numerosFB = "";
            IntfDalFicheBord serviceFicheBord = new ImplDalFicheBord();
            #endregion

            #region implementation
            if (FicheBord != null)
            {
                FicheBord.NumerosFB = serviceFicheBord.getNumerosFB(FicheBord.agent.agence.SigleAgence);

                this.strCommande = "INSERT INTO `fichebord` (`numerosFB`,`matriculeAgent`";
                this.strCommande += " ,`numerosAV`,`dateHeurDepart`,`dateHeurPrevue`) ";
                this.strCommande += " VALUES ('" + FicheBord.NumerosFB + "','" + FicheBord.MatriculeAgent + "'";
                this.strCommande += " ,'" + FicheBord.NumerosAV + "','" + FicheBord.DateHeurDepart.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                this.strCommande += " ,'" + FicheBord.DateHeurPrevue.ToString("yyyy-MM-dd HH:mm:ss") + "')";


                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    numerosFB = FicheBord.NumerosFB;
                this.serviceConnectBase.closeConnection();

            }
            #endregion

            return numerosFB;
        }

        bool IntfDalFicheBord.deleteFicheBord(crlFicheBord FicheBord)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (FicheBord != null)
            {
                if (FicheBord.NumerosFB != "")
                {
                    this.strCommande = "DELETE FROM `fichebord` WHERE (`numerosFB` = '" + FicheBord.NumerosFB + "')";
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

        bool IntfDalFicheBord.deleteFicheBord(string numerosFB)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (numerosFB != "")
            {
                this.strCommande = "DELETE FROM `fichebord` WHERE (`numerosFB` = '" + numerosFB + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete == 1)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }

            #endregion

            return isDelete;
        }

        bool IntfDalFicheBord.updateFicheBord(crlFicheBord FicheBord)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            #endregion

            #region implementation
            if (FicheBord != null)
            {
                if (FicheBord.NumerosFB != "")
                {
                    this.strCommande = "UPDATE `fichebord` SET ";
                    this.strCommande += " `numerosAV`='" + FicheBord.NumerosAV + "',`matriculeAgent`='" + FicheBord.MatriculeAgent + "'";
                    this.strCommande += ", `dateHeurDepart`='" + FicheBord.DateHeurDepart.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    this.strCommande += ", `dateHeurPrevue`='" + FicheBord.DateHeurPrevue.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    this.strCommande += " WHERE (`numerosFB`='" + FicheBord.NumerosFB + "')";

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

        int IntfDalFicheBord.isFicheBord(crlFicheBord FicheBord)
        {
            throw new NotImplementedException();
        }

        crlFicheBord IntfDalFicheBord.selectFicheBord(string numerosFB)
        {
            #region declaration
            crlFicheBord FicheBord = null;
            IntfDalAgent serviceAgent = new ImplDalAgent(); 
            IntfAutorisationVoyage serviceAutorisationVoyage = new ImplAutorisationVoyage();
            IntfDalFicheBord serviceFicheBord = new ImplDalFicheBord();
            IntfDalPlaceFB servicePlaceFB = new ImplDalPlaceFB();
            #endregion

            #region implementation
            if (numerosFB != "") 
            {
                this.strCommande = "SELECT * FROM `fichebord` WHERE (`numerosFB`='" + numerosFB + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            FicheBord = new crlFicheBord();
                            FicheBord.NumerosFB = reader["numerosFB"].ToString();
                            FicheBord.MatriculeAgent = reader["matriculeAgent"].ToString();
                            FicheBord.NumerosAV = reader["numerosAV"].ToString();
                            FicheBord.DateHeurDepart = Convert.ToDateTime(reader["dateHeurDepart"].ToString());
                            FicheBord.DateHeurPrevue = Convert.ToDateTime(reader["dateHeurPrevue"].ToString());
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if(FicheBord != null)
                {
                    FicheBord.agent = serviceAgent.selectAgent(FicheBord.MatriculeAgent);
                    FicheBord.autorisationVoyage = serviceAutorisationVoyage.selectAutorisationVoyage(FicheBord.NumerosAV);

                    FicheBord.commission = serviceFicheBord.getCommission(FicheBord.NumerosFB);
                    //FicheBord.escorteVoyage = serviceFicheBord.getEscorteVoyage(FicheBord.NumerosFB);
                    FicheBord.voyage = serviceFicheBord.getVoyage(FicheBord.NumerosFB);
                    FicheBord.placeFB = servicePlaceFB.selectPlaceFB(FicheBord.NumerosFB);
                }
            }
            #endregion

            return FicheBord;
        }

        string IntfDalFicheBord.getNumerosFB(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numerosFB = "00001";
            string[] tempNumerosFB = null;
            string strDate = "FB" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT fichebord.numerosFB AS maxNum FROM fichebord";
            this.strCommande += " WHERE fichebord.numerosFB LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumerosFB = reader["maxNum"].ToString().ToString().Split('/');
                        numerosFB = tempNumerosFB[tempNumerosFB.Length - 1];
                    }
                    numTemp = double.Parse(numerosFB) + 1;
                    if (numTemp < 10)
                        numerosFB = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numerosFB = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numerosFB = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numerosFB = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numerosFB = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numerosFB = strDate + "/" + sigleAgence + "/" + numerosFB;
            #endregion

            return numerosFB;
        }

        void IntfDalFicheBord.loadDdlTri(DropDownList ddlTri)
        {
            throw new NotImplementedException();
        }

        List<crlCommission> IntfDalFicheBord.getCommission(string numerosFB)
        {
            #region declaration
            List<crlCommission> Commissions = new List<crlCommission>();
            crlCommission tempCommission = null;
            IntfDalReceptionnaire servicePersonne = new ImplDalReceptionnaire();
            IntfDalRecu serviceRecu = new ImplDalRecu();
            IntfDalClient serviceClient = new ImplDalClient();
            IntfDalDesignationCommission serviceDesignationCommission = new ImplDalDesignationCommission();
            IntfDalAgent serviceAgent = new ImplDalAgent();
            #endregion

            #region implementation
            if (numerosFB != "")
            {
                this.strCommande = "SELECT * FROM commission";
                this.strCommande += " Inner Join associationfichebordcommission ON associationfichebordcommission.idCommission = commission.idCommission";
                this.strCommande += " WHERE (associationfichebordcommission.numerosFB ='" + numerosFB + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while(this.reader.Read())
                        {
                            tempCommission = new crlCommission();
                            tempCommission.IdCommission = reader["idCommission"].ToString();
                            tempCommission.Destination = reader["destination"].ToString();
                            tempCommission.Poids = reader["poids"].ToString();
                            try
                            {
                                tempCommission.Nombre = int.Parse(reader["nombre"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                tempCommission.IsRecu = int.Parse(reader["isRecu"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            tempCommission.PieceJustificatif = reader["pieceJustificatif"].ToString();
                            tempCommission.FraisEnvoi = reader["fraisEnvoi"].ToString();
                            tempCommission.NumExpediteur = reader["numExpediteur"].ToString();
                            tempCommission.NumRecepteur = reader["numRecepteur"].ToString();
                            tempCommission.NumDesignation = reader["numDesignation"].ToString();
                            tempCommission.TypeCommission = reader["typeCommission"].ToString();
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
                            tempCommission.MatriculeAgent = reader["matriculeAgent"].ToString();
                            tempCommission.MatriculeAgentDelivreur = reader["matriculeAgentDelivreur"].ToString();
                            tempCommission.ModePaiement = reader["modePaiement"].ToString();
                            

                            Commissions.Add(tempCommission);
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                for (int i = 0; i < Commissions.Count; i++)
                {
                    if (Commissions[i] != null)
                    {
                        Commissions[i].expediteur = serviceClient.selectClient(Commissions[i].NumExpediteur);
                        Commissions[i].recepteur = servicePersonne.selectPersonne(Commissions[i].NumRecepteur);
                        Commissions[i].designationCommission = serviceDesignationCommission.selectDesignationCommission(Commissions[i].NumDesignation);
                        Commissions[i].typeCommssionObjet = new crlTypeCommssion();
                        Commissions[i].typeCommssionObjet.TypeCommission = Commissions[i].TypeCommission;
                        Commissions[i].agent = serviceAgent.selectAgent(Commissions[i].MatriculeAgent);
                        Commissions[i].agentDelivreur = serviceAgent.selectAgent(Commissions[i].MatriculeAgentDelivreur);
                    }
                }
            }
            #endregion

            return Commissions;
        }

        List<crlVoyage> IntfDalFicheBord.getVoyage(string numerosFB)
        {
            #region declaration
            List<crlVoyage> Voyages = new List<crlVoyage>();
            crlVoyage tempVoyage = null;

            IntfDalBagage serviceBagage = new ImplDalBagage();
            IntfDalIndividu serviceIndividu = new ImplDalIndividu();
            IntfDalBillet serviceBillet = new ImplDalBillet();
            IntfDalPlaceFB servicePlaceFB = new ImplDalPlaceFB();
            #endregion

            #region implementation
            if (numerosFB != "")
            {
                this.strCommande = "SELECT * FROM `voyage` WHERE (`numerosFB`='" + numerosFB + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            tempVoyage = new crlVoyage();
                            tempVoyage.IdVoyage = reader["idVoyage"].ToString();
                            tempVoyage.NumIndividu = reader["numIndividu"].ToString();
                            tempVoyage.NumerosFB = reader["numerosFB"].ToString();
                            try
                            {
                                tempVoyage.PoidBagage = double.Parse(reader["poidBagage"].ToString());
                            }
                            catch (Exception)
                            {}
                            tempVoyage.Destination = reader["destination"].ToString();
                            tempVoyage.NumBillet = reader["numBillet"].ToString();
                            tempVoyage.NumPlace = reader["numPlace"].ToString();
                            tempVoyage.PieceIdentite = reader["pieceIdentite"].ToString();

                            Voyages.Add(tempVoyage);
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                for (int i = 0; i < Voyages.Count; i++)
                {
                    if (Voyages[i] != null)
                    {
                       
                        Voyages[i].billet = serviceBillet.selectBillet(Voyages[i].NumBillet);
                        Voyages[i].individu = serviceIndividu.selectIndividu(Voyages[i].NumIndividu);
                        Voyages[i].placeFB = servicePlaceFB.selectPlaceFB(Voyages[i].NumerosFB, Voyages[i].NumPlace);
                        Voyages[i].bagage = serviceBagage.selectBagageForVoyage(Voyages[i].IdVoyage);
                    }
                }

            }
            #endregion

            return Voyages;
        }

        List<crlEscorteVoyage> IntfDalFicheBord.getEscorteVoyage(string numerosFB)
        {
            #region declaration
            List<crlEscorteVoyage> EscorteVoyages = new List<crlEscorteVoyage>();
            crlEscorteVoyage tempEscorteVoyage = null;

            IntfDalEscorte serviceEscorte = new ImplDalEscorte();
            #endregion

            #region implementation
            if (numerosFB != "")
            {
                this.strCommande = "SELECT * FROM `escortevoyage` WHERE (`numerosFB`='" + numerosFB + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            tempEscorteVoyage = new crlEscorteVoyage();
                            tempEscorteVoyage.IdEscorteVoyage = reader["idEscorteVoyage"].ToString();
                            tempEscorteVoyage.MatriculeEscorte = reader["matriculeEscorte"].ToString();
                            tempEscorteVoyage.NumerosFB = reader["numerosFB"].ToString();

                            EscorteVoyages.Add(tempEscorteVoyage);
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                for (int i = 0; i < EscorteVoyages.Count;i++ )
                {
                    if (EscorteVoyages[i] != null)
                    {
                        EscorteVoyages[i].Escorte = serviceEscorte.selectEscorte(EscorteVoyages[i].MatriculeEscorte);
                    }
                }
            }
            #endregion

            return EscorteVoyages;
        }

        int IntfDalFicheBord.getNombreTotalPassager(string numerosFB)
        {
            #region declaration
            int nombreTotalPassager = 0;
            #endregion

            #region implementation
            if (numerosFB != "")
            {
                this.strCommande = "SELECT Count(placefb.numPlace) AS nbPassager FROM fichebord";
                this.strCommande += " Inner Join placefb ON placefb.numerosFB = fichebord.numerosFB";
                this.strCommande += " WHERE placefb.isOccuper =  '1' AND fichebord.numerosFB =  '" + numerosFB + "'";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            try
                            {
                                nombreTotalPassager = int.Parse(reader["nbPassager"].ToString());
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

            return nombreTotalPassager;
        }

        double IntfDalFicheBord.getPoidTotalBagage(string numerosFB)
        {
            #region declaration
            double poidTotal = 0;
            #endregion

            #region implementation
            if (numerosFB != "")
            {
                this.strCommande = "SELECT Sum(voyage.poidBagage) As poidsTotal FROM fichebord";
                this.strCommande += " Inner Join voyage ON voyage.numerosFB = fichebord.numerosFB";
                this.strCommande += " WHERE fichebord.numerosFB =  '" + numerosFB + "'";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            try
                            {
                                poidTotal = double.Parse(reader["poidsTotal"].ToString());
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

            return poidTotal;
        }

        double IntfDalFicheBord.getPoidTotalCommission(string numerosFB)
        {
            #region declaration
            double poidTotal = 0;
            #endregion

            #region implementation
            if (numerosFB != "")
            {
                this.strCommande = "SELECT Sum(commission.poids) As poidsTotal FROM fichebord";
                this.strCommande += " Inner Join associationfichebordcommission ON associationfichebordcommission.numerosFB = fichebord.numerosFB";
                this.strCommande += " Inner Join commission ON commission.idCommission = associationfichebordcommission.idCommission";
                this.strCommande += " WHERE fichebord.numerosFB = '" + numerosFB + "'";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            try
                            {
                                poidTotal = double.Parse(reader["poidsTotal"].ToString());
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

            return poidTotal;
        }

        double IntfDalFicheBord.getPrixTotalBillet(string numerosFB)
        {
            #region declaration
            double prixTotal = 0;
            #endregion

            #region implementation
            if (numerosFB != "")
            {
                this.strCommande = "SELECT Sum(billet.prixBillet) As prixTotal FROM fichebord";
                this.strCommande += " Inner Join voyage ON voyage.numerosFB = fichebord.numerosFB";
                this.strCommande += " Inner Join billet ON billet.numBillet = voyage.numBillet";
                this.strCommande += " WHERE fichebord.numerosFB = '" + numerosFB + "'";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            try
                            {
                                prixTotal = double.Parse(reader["prixTotal"].ToString());
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

            return prixTotal;
        }

        double IntfDalFicheBord.getPrixTotalBagage(string numerosFB)
        {
            #region declaration
            double prixTotal = 0;
            #endregion

            #region implementation
            if (numerosFB != "")
            {
                this.strCommande = "SELECT Sum(recuencaisser.montantRecuEncaisser) AS prixTotal FROM recuencaisser";
                this.strCommande += " Inner Join bagage ON bagage.numRecu = recuencaisser.numRecuEncaisser";
                this.strCommande += " Inner Join associationvoyagebagage ON associationvoyagebagage.idBagage = bagage.idBagage";
                this.strCommande += " Inner Join voyage ON voyage.idVoyage = associationvoyagebagage.idVoyage";
                this.strCommande += " WHERE voyage.numerosFB = '" + numerosFB + "'";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            try
                            {
                                prixTotal = double.Parse(reader["prixTotal"].ToString());
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

            return prixTotal;
        }

        double IntfDalFicheBord.getPrixTotalCommission(string numerosFB)
        {
            #region declaration
            double prixTotal = 0;
            #endregion

            #region implementation
            if (numerosFB != "")
            {
                this.strCommande = "SELECT Sum(commission.fraisEnvoi) As prixTotal FROM fichebord";
                this.strCommande += " Inner Join associationfichebordcommission ON associationfichebordcommission.numerosFB = fichebord.numerosFB";
                this.strCommande += " Inner Join commission ON commission.idCommission = associationfichebordcommission.idCommission";
                this.strCommande += " WHERE fichebord.numerosFB = '" + numerosFB + "'";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            try
                            {
                                prixTotal = double.Parse(reader["prixTotal"].ToString());
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

            return prixTotal;
        }
        #endregion

        #region insert to grid
        void IntfDalFicheBord.insertToGridFicheBordSansAD(GridView gridView, string param, string paramLike, string valueLike, string numAgence)
        {
            #region declaration
            IntfDalFicheBord serviceFicheBord = new ImplDalFicheBord();
            #endregion

            #region implementation

            this.strCommande = "SELECT fichebord.dateHeurPrevue, fichebord.numerosFB AS numFb, vehicule.matriculeVehicule,";
            this.strCommande += " Sum(placefb.isOccuper) AS nbOccuper, Count(placefb.numerosFB) AS nbPlace, vehicule.marqueVehicule,";
            this.strCommande += " itineraire.numVilleItineraireDebut, itineraire.numVilleItineraireFin, chauffeur.nomChauffeur,";
            this.strCommande += " chauffeur.prenomChauffeur FROM fichebord";
            this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.numerosAV = fichebord.numerosAV";
            this.strCommande += " Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
            this.strCommande += " Inner Join licence ON licence.numLicence = verification.numLicence";
            this.strCommande += " Inner Join vehicule ON vehicule.numVehicule = licence.numVehicule";
            this.strCommande += " Inner Join itineraire ON itineraire.idItineraire = verification.idItineraire";
            this.strCommande += " Inner Join placefb ON placefb.numerosFB = fichebord.numerosFB";
            this.strCommande += " Inner Join chauffeur ON chauffeur.idChauffeur = verification.idChauffeur";
            this.strCommande += " Left Join autorisationdepart ON autorisationdepart.numerosFB = fichebord.numerosFB";
            this.strCommande += " Inner Join agent ON agent.matriculeAgent = fichebord.matriculeAgent";
            this.strCommande += " WHERE autorisationdepart.numAutorisationDepart IS NULL";
            this.strCommande += " AND agent.numAgence = '" + numAgence + "'";
            this.strCommande += " AND (" + paramLike + " LIKE  '%" + valueLike + "%')";
            this.strCommande += " GROUP BY fichebord.numerosFB ORDER BY " + param + " ASC";


            gridView.DataSource = serviceFicheBord.getDataTableFicheBordSansAD(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalFicheBord.getDataTableFicheBordSansAD(string strRqst)
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
            dataTable.Columns.Add("numFb", typeof(string));
            dataTable.Columns.Add("dateHeurPrevue", typeof(DateTime));
            dataTable.Columns.Add("voiture", typeof(string));
            dataTable.Columns.Add("chauffeur", typeof(string));
            dataTable.Columns.Add("itineraire", typeof(string));
            dataTable.Columns.Add("nbPlaceLibre", typeof(string));
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

                        dr["numFb"] = reader["numFb"].ToString();
                        dr["dateHeurPrevue"] = Convert.ToDateTime(reader["dateHeurPrevue"].ToString());
                        dr["voiture"] = reader["matriculeVehicule"].ToString() + " " + reader["marqueVehicule"].ToString();
                        dr["chauffeur"] = reader["nomChauffeur"].ToString() + " " + reader["prenomChauffeur"].ToString();
                        villeD = serviceVille.selectVille(reader["numVilleItineraireDebut"].ToString());
                        villeF = serviceVille.selectVille(reader["numVilleItineraireFin"].ToString());
                        if (villeD != null && villeF != null)
                        {
                            dr["itineraire"] = villeD.NomVille + "-" + villeF.NomVille;
                        }
                        else
                        {
                            dr["itineraire"] = reader["numVilleItineraireDebut"].ToString() + "-" + reader["numVilleItineraireFin"].ToString();
                        }

                        dr["nbPlaceLibre"] = (int.Parse(reader["nbPlace"].ToString()) - int.Parse(reader["nbOccuper"].ToString())).ToString();
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


        List<crlFicheBord> IntfDalFicheBord.selectFicheBord(DateTime date, string heure, string idItineraire, string numAgence)
        {
            #region declaration
            List<crlFicheBord> ficheBords = null;
            crlFicheBord tempFicheBord = null;
            IntfDalAgent serviceAgent = new ImplDalAgent(); 
            IntfAutorisationVoyage serviceAutorisationVoyage = new ImplAutorisationVoyage();
            IntfDalFicheBord serviceFicheBord = new ImplDalFicheBord();
            IntfDalPlaceFB servicePlaceFB = new ImplDalPlaceFB();
            #endregion

            #region implementation
            if (heure != "" && idItineraire != "" && numAgence != "")
            {
                this.strCommande = "SELECT (fichebord.numerosFB) AS numFB, (fichebord.matriculeAgent) AS matrAgent, (fichebord.numerosAV) AS numAV,";
                this.strCommande += " fichebord.dateHeurDepart, fichebord.dateHeurPrevue FROM fichebord";
                this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.numerosAV = fichebord.numerosAV";
                this.strCommande += " Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
                this.strCommande += " Left Join autorisationdepart ON autorisationdepart.numerosFB = fichebord.numerosFB";
                this.strCommande += " Inner Join agent ON agent.matriculeAgent = fichebord.matriculeAgent";
                this.strCommande += " WHERE fichebord.dateHeurPrevue LIKE  '" + date.ToString("yyyy-MM-dd") +  " " + heure + "%' AND";
                this.strCommande += " autorisationdepart.numerosFB IS NULL  AND";
                this.strCommande += " agent.numAgence = '" + numAgence + "' AND";
                this.strCommande += " verification.idItineraire = '" + idItineraire + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        ficheBords = new List<crlFicheBord>();
                        while (this.reader.Read())
                        {
                            tempFicheBord = new crlFicheBord();
                            tempFicheBord.NumerosFB = reader["numFB"].ToString();
                            tempFicheBord.MatriculeAgent = reader["matrAgent"].ToString();
                            tempFicheBord.NumerosAV = reader["numAV"].ToString();
                            tempFicheBord.DateHeurDepart = Convert.ToDateTime(reader["dateHeurDepart"].ToString());
                            tempFicheBord.DateHeurPrevue = Convert.ToDateTime(reader["dateHeurPrevue"].ToString());

                            ficheBords.Add(tempFicheBord);
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                if (ficheBords != null)
                {
                    for (int i = 0; i < ficheBords.Count; i++)
                    {
                        ficheBords[i].agent = serviceAgent.selectAgent(ficheBords[i].MatriculeAgent);
                        ficheBords[i].autorisationVoyage = serviceAutorisationVoyage.selectAutorisationVoyage(ficheBords[i].NumerosAV);

                        ficheBords[i].commission = serviceFicheBord.getCommission(ficheBords[i].NumerosFB);
                        //FicheBord.escorteVoyage = serviceFicheBord.getEscorteVoyage(FicheBord.NumerosFB);
                        ficheBords[i].voyage = serviceFicheBord.getVoyage(ficheBords[i].NumerosFB);
                        ficheBords[i].placeFB = servicePlaceFB.selectPlaceFB(ficheBords[i].NumerosFB);
                    }
                    
                }
            }
            #endregion

            return ficheBords;
        }
    }
}