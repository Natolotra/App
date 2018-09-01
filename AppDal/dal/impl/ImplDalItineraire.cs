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
    /// Implementation du service itineraire
    /// </summary>
    public class ImplDalItineraire : IntfDalItineraire
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalItineraire()
        {
            this.serviceRessource = new ImplDalServiceRessource();
            this.serviceConnectBase = new ImplDalConnectBase(this.serviceRessource.getDefaultStrConnection());
        }
        public ImplDalItineraire(string strConnection)
        {
            this.serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region IntfDalItineraire Members

        string IntfDalItineraire.insertItineraire(crlItineraire Itineraire, string sigleAgence)
        {
            #region declaration
            IntfDalItineraire serviceItineraire = new ImplDalItineraire();
            int nombreInsertion = 0;
            string idItineraire = "";
            #endregion

            #region implementation
            if (Itineraire != null)
            {
                
                Itineraire.IdItineraire = serviceItineraire.getIdItineraire(sigleAgence);

                this.strCommande = "INSERT INTO `itineraire` (`idItineraire`,`distanceParcour`,`nombreRepos`,";
                this.strCommande += " `dureeTrajet`,`numVilleItineraireDebut`,`numVilleItineraireFin`,`numInfoBagage`)";
                this.strCommande +=" VALUES ('" + Itineraire.IdItineraire + "','" + Itineraire.DistanceParcour + "','" + Itineraire.NombreRepos + "'";
                this.strCommande +=" ,'" + Itineraire.DureeTrajet + "','" + Itineraire.NumVilleItineraireDebut + "'";
                this.strCommande +=" ,'" + Itineraire.NumVilleItineraireFin + "','" + Itineraire.NumInfoBagage + "')";

                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    idItineraire = Itineraire.IdItineraire;
                this.serviceConnectBase.closeConnection();

                if (Itineraire.villeD != null)
                {
                    serviceItineraire.insertAssociationVilleItineraire(Itineraire, Itineraire.villeD);
                }
                if(Itineraire.villeF != null)
                {
                    serviceItineraire.insertAssociationVilleItineraire(Itineraire, Itineraire.villeF);
                }
                /*if (Itineraire.infoPrixCommission != null)
                {
                    for (int i = 0; i < Itineraire.infoPrixCommission.Count; i++)
                    {
                        serviceItineraire.insertAssociationItineraireInfoPrixCommission(Itineraire, Itineraire.infoPrixCommission[i]);
                    }
                }*/
               
            }

            #endregion

            return idItineraire;
        }

        string IntfDalItineraire.insertItineraireAll(crlItineraire Itineraire, string sigleAgence)
        {
            #region declaration
            IntfDalItineraire serviceItineraire = new ImplDalItineraire();
            IntfDalInfoExedantBagage serviceInfoExedantBagage = new ImplDalInfoExedantBagage();
            IntfDalTrajet serviceTrajet = new ImplDalTrajet();

            string idItineraire = "";
            #endregion

            #region implementation
            if (Itineraire != null)
            {

                if (Itineraire.infoExedantBagage != null)
                {
                    Itineraire.infoExedantBagage.NumInfoBagage = serviceInfoExedantBagage.insertInfoExedantBagage(Itineraire.infoExedantBagage, sigleAgence);

                    if (Itineraire.infoExedantBagage.NumInfoBagage != "")
                    {
                        Itineraire.NumInfoBagage = Itineraire.infoExedantBagage.NumInfoBagage;

                        Itineraire.IdItineraire = serviceItineraire.insertItineraire(Itineraire, sigleAgence);

                        if (Itineraire.IdItineraire != "")
                        {
                            idItineraire = Itineraire.IdItineraire;

                            if (Itineraire.trajets != null)
                            {
                                for (int i = 0; i < Itineraire.trajets.Count; i++)
                                {
                                    Itineraire.trajets[i].NumTrajet = serviceTrajet.insertTrajetAll(Itineraire.trajets[i], sigleAgence);

                                    serviceItineraire.insertAssociationTrajetItineraire(Itineraire.trajets[i].NumTrajet, Itineraire.IdItineraire);
                                }
                            }
                        }

                    }
                }

            }
            #endregion

            return idItineraire;
        }

        bool IntfDalItineraire.insertAssociationVilleItineraire(crlItineraire Itineraire, crlVille Ville)
        {
            #region declaration
            bool isInsert = false;
            int nombreInsertion = 0;
            #endregion

            #region implementation
            if (Itineraire != null && Ville != null)
            {
                this.strCommande = "INSERT INTO `asociationvilleitineraire` (`idItineraire`,`numVille`)";
                this.strCommande += " VALUES ('" + Itineraire.IdItineraire + "','" + Ville.NumVille + "')";
                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    isInsert = true;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isInsert;
        }

        bool IntfDalItineraire.insertAssociationTrajetItineraire(string numTrajet, string idItineraire)
        {
            #region declaration
            bool isInsert = false;
            int nombreInsertion = 0;
            #endregion

            #region implementation
            if (numTrajet != "" && idItineraire != "")
            {
                this.strCommande = "INSERT INTO `associationtrajetitineraire` (`idItineraire`,`numTrajet`)";
                this.strCommande += " VALUES ('" + idItineraire + "','" + numTrajet + "')";
                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    isInsert = true;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isInsert;
        }

        bool IntfDalItineraire.insertAssociationItineraireInfoPrixCommission(crlItineraire Itineraire, crlInfoPrixCommission InfoPrixCommission)
        {
            #region declaration
            bool isInsert = false;
            int nombreInsertion = 0;
            #endregion

            #region implementation
            if (Itineraire != null && InfoPrixCommission != null)
            {
                this.strCommande = "INSERT INTO `associationitineraireinfoprixcommission` (`idItineraire`,`numInfoPrixCommission`)";
                this.strCommande += " VALUES ('" + Itineraire.IdItineraire + "','" + InfoPrixCommission.NumInfoPrixCommission + "')";
                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    isInsert = true;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isInsert;
        }

        bool IntfDalItineraire.insertAssoItineraireRouteNationale(string idItineraire, string routeNationale)
        {
            #region declaration
            bool isInsert = false;
            int nombreInsertion = 0;
            #endregion

            #region implementation
            if (idItineraire != "" && routeNationale != "")
            {
                this.strCommande = "INSERT INTO `assoitineraireroutenationale` (`idItineraire`,`routeNationale`)";
                this.strCommande += " VALUES ('" + idItineraire + "','" + routeNationale + "')";
                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    isInsert = true;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isInsert;
        }

        bool IntfDalItineraire.deleteAssociationTrajetItineraire(string numTrajet, string idItineraire)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (idItineraire != "" && numTrajet != "")
            {
                this.strCommande = "DELETE FROM `associationtrajetitineraire` WHERE (`idItineraire` = '" + idItineraire + "' AND `numTrajet`='" + numTrajet + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete > 0)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isDelete;
        }

        bool IntfDalItineraire.deleteAssoItineraireRouteNationale(string idItineraire, string routeNationale)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (idItineraire != "" && routeNationale != "")
            {
                this.strCommande = "DELETE FROM `assoitineraireroutenationale` WHERE (`idItineraire` = '" + idItineraire + "' AND `routeNationale`='" + routeNationale + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete > 0)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isDelete;
        }

        bool IntfDalItineraire.deleteAssociationItineraireInfoPrixCommission(string idItineraire)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (idItineraire != "")
            {
                this.strCommande = "DELETE FROM `associationitineraireinfoprixcommission` WHERE (`idItinerairet` = '" + idItineraire + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete > 0)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isDelete;
        }

        bool IntfDalItineraire.deleteAssociationVilleItineraire(string idItineraire)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (idItineraire != "")
            {
                this.strCommande = "DELETE FROM `asociationvilleitineraires` WHERE (`idItinerairet` = '" + idItineraire + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete > 0)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isDelete;
        }

        bool IntfDalItineraire.deleteItineraire(crlItineraire Itineraire)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            IntfDalItineraire serviceItineraire = new ImplDalItineraire();
            #endregion

            #region implementation
            if (Itineraire != null)
            {
                if (Itineraire.IdItineraire != "")
                {
                    serviceItineraire.deleteAssociationVilleItineraire(Itineraire.IdItineraire);
                    serviceItineraire.deleteAssociationItineraireInfoPrixCommission(Itineraire.IdItineraire);

                    this.strCommande = "DELETE FROM `itineraire` WHERE (`idItineraire` = '" + Itineraire.IdItineraire + "')";
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

        bool IntfDalItineraire.deleteItineraire(string idItineraire)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            IntfDalItineraire serviceItineraire = new ImplDalItineraire();
            #endregion

            #region implementation
            
            if (idItineraire != "")
            {
                serviceItineraire.deleteAssociationVilleItineraire(idItineraire);
                this.strCommande = "DELETE FROM `itineraire` WHERE (`idItineraire` = '" + idItineraire + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete == 1)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }
           
            #endregion

            return isDelete;
        }

        bool IntfDalItineraire.updateItineraire(crlItineraire Itineraire)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            #endregion

            #region implementation
            if (Itineraire != null)
            {
                if (Itineraire.IdItineraire != "")
                {
                    this.strCommande = "UPDATE `itineraire` SET `distanceParcour`='" + Itineraire.DistanceParcour + "'";
                    this.strCommande += ",`dureeTrajet`='" + Itineraire.DureeTrajet + "',`numVilleItineraireDebut`='" + Itineraire.NumVilleItineraireDebut + "'";
                    this.strCommande += ",`numVilleItineraireFin`='" + Itineraire.NumVilleItineraireFin + "',`nombreRepos`='" + Itineraire.NombreRepos + "',";
                    this.strCommande += "`numInfoBagage`='" + Itineraire.NumInfoBagage + "' WHERE (`idItineraire`='" + Itineraire.IdItineraire + "')";

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

        int IntfDalItineraire.isItineraire(crlItineraire Itineraire)
        {
            #region declaration
            int isItineraire = 0;
            int distance = 0;
            #endregion

            #region implementation
            if (Itineraire != null) 
            {
                this.strCommande = "SELECT itineraire.idItineraire FROM itineraire";
                this.strCommande += " WHERE ((itineraire.numVilleItineraireDebut = '" + Itineraire.NumVilleItineraireDebut + "' AND itineraire.numVilleItineraireFin = '" + Itineraire.NumVilleItineraireFin + "') OR";
                this.strCommande += " (itineraire.numVilleItineraireFin = '" + Itineraire.NumVilleItineraireDebut + "' AND itineraire.numVilleItineraireDebut = '" + Itineraire.NumVilleItineraireFin + "')) AND";
                this.strCommande += " itineraire.distanceParcour = '" + Itineraire.DistanceParcour + "' AND";
                this.strCommande += " itineraire.idItineraire <> '" + Itineraire.IdItineraire + "'";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null) 
                {
                    if (reader.HasRows) 
                    {
                        if (reader.Read()) 
                        {
                           
                            isItineraire = 1;
                               
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isItineraire;
        }

        string IntfDalItineraire.isItineraireStr(crlItineraire Itineraire)
        {
            #region declaration
            string isItineraire = "";
            int distance = 0;
            #endregion

            #region implementation
            if (Itineraire != null)
            {
                this.strCommande = "SELECT itineraire.idItineraire FROM itineraire";
                this.strCommande += " WHERE ((itineraire.numVilleItineraireDebut = '" + Itineraire.NumVilleItineraireDebut + "' AND itineraire.numVilleItineraireFin = '" + Itineraire.NumVilleItineraireFin + "') OR";
                this.strCommande += " (itineraire.numVilleItineraireFin = '" + Itineraire.NumVilleItineraireDebut + "' AND itineraire.numVilleItineraireDebut = '" + Itineraire.NumVilleItineraireFin + "')) AND";
                this.strCommande += " itineraire.distanceParcour = '" + Itineraire.DistanceParcour + "' AND";
                this.strCommande += " itineraire.idItineraire <> '" + Itineraire.IdItineraire + "'";
                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            
                            isItineraire = this.reader["idItineraire"].ToString();
                                
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isItineraire;
        }

        crlItineraire IntfDalItineraire.selectItineraire(string idItineraire)
        {
            #region declaration
            crlItineraire Itineraire = null;
            IntfDalVille serviceVille = new ImplDalVille();
            IntfDalInfoExedantBagage serviceInfoExedantBagage = new ImplDalInfoExedantBagage();
            IntfDalInfoPrixCommission serviceInfoPrixCommission = new ImplDalInfoPrixCommission();
            IntfDalRouteNationale serviceRouteNationale = new ImplDalRouteNationale();
            IntfDalTrajet serviceTrajet = new ImplDalTrajet();
            #endregion

            #region implementation
            if (idItineraire != "")
            {
                this.strCommande = "SELECT * FROM `itineraire` WHERE (`idItineraire`='" + idItineraire + "')";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read()) 
                        {
                            Itineraire = new crlItineraire();
                            Itineraire.DistanceParcour = int.Parse(reader["distanceParcour"].ToString());
                            Itineraire.DureeTrajet = reader["dureeTrajet"].ToString();
                            Itineraire.NombreRepos = int.Parse(reader["nombreRepos"].ToString());
                            Itineraire.IdItineraire = reader["idItineraire"].ToString();
                            Itineraire.NumVilleItineraireDebut = reader["numVilleItineraireDebut"].ToString();
                            Itineraire.NumVilleItineraireFin = reader["numVilleItineraireFin"].ToString();
                            Itineraire.NumInfoBagage = reader["numInfoBagage"].ToString();
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (Itineraire != null)
                {
                    if (Itineraire.NumVilleItineraireDebut != "")
                    {
                        Itineraire.villeD = serviceVille.selectVille(Itineraire.NumVilleItineraireDebut);
                    }
                    if (Itineraire.NumVilleItineraireFin != "")
                    {
                        Itineraire.villeF = serviceVille.selectVille(Itineraire.NumVilleItineraireFin);
                    }
                    if (Itineraire.IdItineraire != "")
                    {
                        Itineraire.villes = serviceVille.selectVillesForItineraire(Itineraire.IdItineraire);
                        Itineraire.routeNationale = serviceRouteNationale.selectRNForItineraire(Itineraire.IdItineraire);
                        Itineraire.trajets = serviceTrajet.selectTrajets(Itineraire.IdItineraire);
                    }
                    if (Itineraire.NumInfoBagage != "")
                    {
                        Itineraire.infoExedantBagage = serviceInfoExedantBagage.selectInfoExedantBagage(Itineraire.NumInfoBagage);
                    }
                    
                }
            }
            #endregion

            return Itineraire;
        }

        List<crlItineraire> IntfDalItineraire.selectAllItineraire()
        {
            #region declaration
            List<crlItineraire> itineraires = null;
            crlItineraire tempItineraire = null;
            IntfDalVille serviceVille = new ImplDalVille();
            IntfDalInfoExedantBagage serviceInfoExedantBagage = new ImplDalInfoExedantBagage();
            IntfDalInfoPrixCommission serviceInfoPrixCommission = new ImplDalInfoPrixCommission();
            IntfDalRouteNationale serviceRouteNationale = new ImplDalRouteNationale();
            IntfDalTrajet serviceTrajet = new ImplDalTrajet();
            #endregion

            #region implementation
           
                this.strCommande = "SELECT * FROM `itineraire`";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        itineraires = new List<crlItineraire>();

                        while (reader.Read())
                        {
                            tempItineraire = new crlItineraire();
                            tempItineraire.DistanceParcour = int.Parse(reader["distanceParcour"].ToString());
                            tempItineraire.DureeTrajet = reader["dureeTrajet"].ToString();
                            tempItineraire.NombreRepos = int.Parse(reader["nombreRepos"].ToString());
                            tempItineraire.IdItineraire = reader["idItineraire"].ToString();
                            tempItineraire.NumVilleItineraireDebut = reader["numVilleItineraireDebut"].ToString();
                            tempItineraire.NumVilleItineraireFin = reader["numVilleItineraireFin"].ToString();
                            tempItineraire.NumInfoBagage = reader["numInfoBagage"].ToString();

                            itineraires.Add(tempItineraire);
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                if (itineraires != null)
                {
                    for (int i = 0; i < itineraires.Count; i++)
                    {
                        if (itineraires[i].NumVilleItineraireDebut != "")
                        {
                            itineraires[i].villeD = serviceVille.selectVille(itineraires[i].NumVilleItineraireDebut);
                        }
                        if (itineraires[i].NumVilleItineraireFin != "")
                        {
                            itineraires[i].villeF = serviceVille.selectVille(itineraires[i].NumVilleItineraireFin);
                        }
                        if (itineraires[i].IdItineraire != "")
                        {
                            itineraires[i].villes = serviceVille.selectVillesForItineraire(itineraires[i].IdItineraire);
                            itineraires[i].routeNationale = serviceRouteNationale.selectRNForItineraire(itineraires[i].IdItineraire);
                            itineraires[i].trajets = serviceTrajet.selectTrajets(itineraires[i].IdItineraire);
                        }
                        if (itineraires[i].NumInfoBagage != "")
                        {
                            itineraires[i].infoExedantBagage = serviceInfoExedantBagage.selectInfoExedantBagage(itineraires[i].NumInfoBagage);
                        }
                    }
                }

                
                   

            
            
            #endregion

            return itineraires;
        }

        string IntfDalItineraire.getIdItineraire(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string idItineraire = "00001";
            string[] tempIdItineraire = null;
            string strDate = "IT" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT itineraire.idItineraire AS maxNum FROM itineraire";
            this.strCommande += " WHERE itineraire.idItineraire LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempIdItineraire = reader["maxNum"].ToString().ToString().Split('/');
                        idItineraire = tempIdItineraire[tempIdItineraire.Length - 1];
                    }
                    numTemp = double.Parse(idItineraire) + 1;
                    if (numTemp < 10)
                        idItineraire = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        idItineraire = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        idItineraire = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        idItineraire = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        idItineraire = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            idItineraire = strDate + "/" + sigleAgence + "/" + idItineraire;
            #endregion

            return idItineraire;
        }

        void IntfDalItineraire.loadDdlTri(DropDownList ddlTri)
        {
            ddlTri.Items.Clear();
            ddlTri.Items.Add(new ListItem("Numeros", "idItineraire"));
            ddlTri.Items.Add(new ListItem("Distance", "distanceParcour"));
            ddlTri.Items.Add(new ListItem("Durée trajet", "dureeTrajet"));
            ddlTri.Items.Add(new ListItem("Ville début", "villeItineraireDebut"));
            ddlTri.Items.Add(new ListItem("Ville destination", "villeItineraireFin"));
        }

        void IntfDalItineraire.loadDdlItineraireForCalendar(DropDownList ddlItineraire, string numVille)
        {
            #region declaration
            crlVille ville = null;
            IntfDalVille serviceVille = new ImplDalVille();
            #endregion

            #region implementation
            if (numVille != "")
            {
                this.strCommande = "SELECT itineraire.idItineraire, itineraire.numVilleItineraireDebut,";
                this.strCommande += " itineraire.numVilleItineraireFin FROM itineraire";
                this.strCommande += " WHERE itineraire.numVilleItineraireDebut = '" + numVille + "' OR";
                this.strCommande += " itineraire.numVilleItineraireFin = '" + numVille + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        ddlItineraire.Items.Clear();
                        while (this.reader.Read())
                        {
                            if (reader["numVilleItineraireDebut"].ToString().Trim() != numVille.Trim())
                            {
                                ville = serviceVille.selectVille(reader["numVilleItineraireDebut"].ToString().Trim());
                                ddlItineraire.Items.Add(new ListItem(ville.NomVille, reader["idItineraire"].ToString()));
                            }
                            else
                            {
                                ville = serviceVille.selectVille(reader["numVilleItineraireFin"].ToString().Trim());
                                ddlItineraire.Items.Add(new ListItem(ville.NomVille, reader["idItineraire"].ToString()));
                            }
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }

        void IntfDalItineraire.loadDdlItineraireVilleD(DropDownList ddlItineraire)
        {
            #region declaration
            #endregion

            #region implementation
            this.strCommande = "SELECT ville.numVille, ville.nomVille FROM itineraire";
            this.strCommande += " Inner Join ville ON ville.numVille = itineraire.numVilleItineraireDebut OR ville.numVille = itineraire.numVilleItineraireFin";
            this.strCommande += " GROUP BY ville.numVille";

            this.serviceConnectBase.openConnection();
            this.reader = this.serviceConnectBase.select(this.strCommande);
            if (this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    ddlItineraire.Items.Clear();
                    while (this.reader.Read())
                    {
                        ddlItineraire.Items.Add(new ListItem(this.reader["nomVille"].ToString(), this.reader["numVille"].ToString()));
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            #endregion
        }

        void IntfDalItineraire.loadDdlItineraireVille1(DropDownList ddlItineraire)
        {
            #region declaration
            #endregion

            #region implementation
            this.strCommande = "SELECT ville.numVille, ville.nomVille FROM itineraire";
            this.strCommande += " Inner Join ville ON ville.numVille = itineraire.numVilleItineraireDebut OR ville.numVille = itineraire.numVilleItineraireFin";
            this.strCommande += " GROUP BY ville.numVille";

            this.serviceConnectBase.openConnection();
            this.reader = this.serviceConnectBase.select(this.strCommande);
            if (this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    ddlItineraire.Items.Clear();
                    ddlItineraire.Items.Add("");
                    while (this.reader.Read())
                    {
                        ddlItineraire.Items.Add(new ListItem(this.reader["nomVille"].ToString(), this.reader["numVille"].ToString()));
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            #endregion
        }

        void IntfDalItineraire.loadDdlItineraireVille2(DropDownList ddlItineraire, string numVille)
        {
            #region declaration
            crlVille ville = null;
            IntfDalVille serviceVille = new ImplDalVille();
            #endregion

            #region implementation
            ddlItineraire.Items.Clear();
            ddlItineraire.Items.Add("");
            if (numVille != "")
            {
                this.strCommande = "SELECT itineraire.idItineraire, itineraire.numVilleItineraireDebut,";
                this.strCommande += " itineraire.numVilleItineraireFin FROM itineraire";
                this.strCommande += " WHERE itineraire.numVilleItineraireDebut = '" + numVille + "' OR";
                this.strCommande += " itineraire.numVilleItineraireFin = '" + numVille + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        
                        while (this.reader.Read())
                        {
                            if (reader["numVilleItineraireDebut"].ToString().Trim() != numVille.Trim())
                            {
                                ville = serviceVille.selectVille(reader["numVilleItineraireDebut"].ToString().Trim());
                                ddlItineraire.Items.Add(new ListItem(ville.NomVille, reader["idItineraire"].ToString()));
                            }
                            else
                            {
                                ville = serviceVille.selectVille(reader["numVilleItineraireFin"].ToString().Trim());
                                ddlItineraire.Items.Add(new ListItem(ville.NomVille, reader["idItineraire"].ToString()));
                            }
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }


        List<string> IntfDalItineraire.getNonVille(string numVilleDepart, string idItineraire)
        {
            #region declaration
            List<string> listeNomVille = new List<string>();
            crlVille tempVille = null;
            IntfDalVille serviceVille = new ImplDalVille();
            #endregion

            #region implementation
            if (numVilleDepart != "" && idItineraire != "")
            {
                tempVille = serviceVille.selectVille(numVilleDepart);
                if (tempVille != null)
                {
                    listeNomVille.Add(tempVille.NomVille);
                    tempVille = null;
                }

                this.strCommande = "SELECT trajet.numVilleD, trajet.numVilleF FROM trajet";
                this.strCommande += " Inner Join associationtrajetitineraire ON associationtrajetitineraire.numTrajet = trajet.numTrajet";
                this.strCommande += " WHERE (trajet.numVilleD = '" + numVilleDepart + "' OR trajet.numVilleF = '" + numVilleDepart + "') AND";
                this.strCommande += " associationtrajetitineraire.idItineraire = '" + idItineraire + "' ORDER BY trajet.distanceTrajet ASC";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            if (this.reader["numVilleD"].ToString().Equals(numVilleDepart))
                            {
                                tempVille = serviceVille.selectVille(this.reader["numVilleF"].ToString());
                            }
                            else if (this.reader["numVilleF"].ToString().Equals(numVilleDepart))
                            {
                                tempVille = serviceVille.selectVille(this.reader["numVilleD"].ToString());
                            }

                            if (tempVille != null)
                            {
                                listeNomVille.Add(tempVille.NomVille);
                                tempVille = null;
                            }
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return listeNomVille;
        }

        #endregion

        #region insert to grid
        void IntfDalItineraire.insertToGridItineraire(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalItineraire serviceItineraire = new ImplDalItineraire();
            #endregion

            #region implementation

            this.strCommande = "SELECT itineraire.idItineraire, itineraire.distanceParcour, itineraire.nombreRepos, itineraire.dureeTrajet,";
            this.strCommande += " itineraire.numVilleItineraireDebut, itineraire.numVilleItineraireFin, itineraire.numInfoBagage,";
            this.strCommande += " infoexedantbagage.numInfoBagage, infoexedantbagage.poidAutorise,";
            this.strCommande += " infoexedantbagage.prixExedantBagage FROM itineraire";
            this.strCommande += " Inner Join infoexedantbagage ON infoexedantbagage.numInfoBagage = itineraire.numInfoBagage";
            this.strCommande += " WHERE (" + paramLike + " LIKE  '%" + valueLike + "%')";
            this.strCommande += " ORDER BY " + param + " ASC";
            #endregion

            gridView.DataSource = serviceItineraire.getDataTableItineraire(this.strCommande);
            gridView.DataBind();
        }

        DataTable IntfDalItineraire.getDataTableItineraire(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalVille serviceVille = new ImplDalVille();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            IntfDalItineraire serviceItineraire = new ImplDalItineraire();

            crlVille villeD = new crlVille();
            crlVille villeF = new crlVille();
            crlItineraire itineraire = null;

            string strAxe = "";
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("idItineraire", typeof(string));
            dataTable.Columns.Add("distanceParcour", typeof(string));
            dataTable.Columns.Add("dureeTrajet", typeof(string));
            dataTable.Columns.Add("poidAutorise", typeof(string));
            dataTable.Columns.Add("itineraire", typeof(string));
            dataTable.Columns.Add("axe", typeof(string));
            
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

                        dr["idItineraire"] = reader["idItineraire"].ToString();
                        dr["distanceParcour"] = reader["distanceParcour"].ToString() + "Km";
                        dr["dureeTrajet"] = serviceGeneral.getTextTimeSpan(reader["dureeTrajet"].ToString());
                        dr["poidAutorise"] = reader["poidAutorise"].ToString() + "Kg";
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

                        itineraire = serviceItineraire.selectItineraire(reader["idItineraire"].ToString());
                        if (itineraire != null)
                        {
                            if (itineraire.routeNationale != null)
                            {
                                for (int i = 0; i < itineraire.routeNationale.Count; i++)
                                {
                                    if (i == 0)
                                    {
                                        strAxe += itineraire.routeNationale[i].RouteNationale;
                                    }
                                    else
                                    {
                                        strAxe += "-" + itineraire.routeNationale[i].RouteNationale;
                                    }
                                }
                            }
                        }

                        dr["axe"] = strAxe;

                        dataTable.Rows.Add(dr);

                        strAxe = "";
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }
        #endregion

        void IntfDalItineraire.insertToGridItineraireLicence(GridView gridView, string param, string paramLike, string valueLike, string numLicence)
        {
            #region declaration
            IntfDalItineraire serviceItineraire = new ImplDalItineraire();
            #endregion

            #region implementation

            this.strCommande = "SELECT itineraire.idItineraire, itineraire.distanceParcour, itineraire.nombreRepos,";
            this.strCommande += " itineraire.dureeTrajet, itineraire.numVilleItineraireDebut, itineraire.numVilleItineraireFin,";
            this.strCommande += " itineraire.numInfoBagage FROM itineraire";
            this.strCommande += " Inner Join assoitinerairelicence ON assoitinerairelicence.idItineraire = itineraire.idItineraire";
            this.strCommande += " WHERE assoitinerairelicence.numLicence = '" + numLicence + "' AND";
            this.strCommande += " (" + paramLike + " LIKE  '%" + valueLike + "%')";
            this.strCommande += " ORDER BY " + param + " ASC";
            #endregion

            gridView.DataSource = serviceItineraire.getDataTableItineraireLicence(this.strCommande);
            gridView.DataBind();
        }

        DataTable IntfDalItineraire.getDataTableItineraireLicence(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalVille serviceVille = new ImplDalVille();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            IntfDalItineraire serviceItineraire = new ImplDalItineraire();

            crlVille villeD = new crlVille();
            crlVille villeF = new crlVille();
            crlItineraire itineraire = null;

            string strAxe = "";
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("idItineraire", typeof(string));
            dataTable.Columns.Add("distanceParcour", typeof(string));
            dataTable.Columns.Add("dureeTrajet", typeof(string));
            dataTable.Columns.Add("itineraire", typeof(string));
            dataTable.Columns.Add("axe", typeof(string));

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

                        dr["idItineraire"] = reader["idItineraire"].ToString();
                        dr["distanceParcour"] = reader["distanceParcour"].ToString() + "Km";
                        dr["dureeTrajet"] = serviceGeneral.getTextTimeSpan(reader["dureeTrajet"].ToString());
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

                        itineraire = serviceItineraire.selectItineraire(reader["idItineraire"].ToString());
                        if (itineraire != null)
                        {
                            if (itineraire.routeNationale != null)
                            {
                                for (int i = 0; i < itineraire.routeNationale.Count; i++)
                                {
                                    if (i == 0)
                                    {
                                        strAxe += itineraire.routeNationale[i].RouteNationale;
                                    }
                                    else
                                    {
                                        strAxe += "-" + itineraire.routeNationale[i].RouteNationale;
                                    }
                                }
                            }
                        }

                        dr["axe"] = strAxe;

                        dataTable.Rows.Add(dr);

                        strAxe = "";
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }


        void IntfDalItineraire.insertToGridItineraireAll(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalItineraire serviceItineraire = new ImplDalItineraire();
            #endregion

            #region implementation

            this.strCommande = "SELECT itineraire.idItineraire, itineraire.distanceParcour, itineraire.nombreRepos, itineraire.dureeTrajet,";
            this.strCommande += " itineraire.numVilleItineraireDebut, itineraire.numVilleItineraireFin, itineraire.numInfoBagage,";
            this.strCommande += " ville.nomVille FROM itineraire";
            this.strCommande += " Inner Join ville ON ville.numVille = itineraire.numVilleItineraireDebut OR ville.numVille = itineraire.numVilleItineraireFin";
            this.strCommande += " WHERE (" + paramLike + " LIKE  '%" + valueLike + "%')";
            this.strCommande += " GROUP BY itineraire.idItineraire ";
            this.strCommande += " ORDER BY " + param + " ASC";
            #endregion

            gridView.DataSource = serviceItineraire.getDataTableItineraireAll(this.strCommande);
            gridView.DataBind();
        }

        DataTable IntfDalItineraire.getDataTableItineraireAll(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalVille serviceVille = new ImplDalVille();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            IntfDalItineraire serviceItineraire = new ImplDalItineraire();

            crlVille villeD = new crlVille();
            crlVille villeF = new crlVille();
            crlItineraire itineraire = null;

            string strAxe = "";
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("idItineraire", typeof(string));
            dataTable.Columns.Add("distanceParcour", typeof(string));
            dataTable.Columns.Add("dureeTrajet", typeof(string));
            dataTable.Columns.Add("itineraire", typeof(string));
            dataTable.Columns.Add("axe", typeof(string));

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

                        dr["idItineraire"] = reader["idItineraire"].ToString();
                        dr["distanceParcour"] = reader["distanceParcour"].ToString() + "Km";
                        dr["dureeTrajet"] = serviceGeneral.getTextTimeSpan(reader["dureeTrajet"].ToString());
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

                        itineraire = serviceItineraire.selectItineraire(reader["idItineraire"].ToString());
                        if (itineraire != null)
                        {
                            if (itineraire.routeNationale != null)
                            {
                                for (int i = 0; i < itineraire.routeNationale.Count; i++)
                                {
                                    if (i == 0)
                                    {
                                        strAxe += itineraire.routeNationale[i].RouteNationale;
                                    }
                                    else
                                    {
                                        strAxe += "-" + itineraire.routeNationale[i].RouteNationale;
                                    }
                                }
                            }
                        }

                        dr["axe"] = strAxe;

                        dataTable.Rows.Add(dr);

                        strAxe = "";
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
