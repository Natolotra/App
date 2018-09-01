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
    /// Implementation du service Utorisation de depart
    /// </summary>
    public class ImplDalAutorisationDepart : IntfDalAutorisationDepart
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public  ImplDalAutorisationDepart()
        {
            this.serviceRessource = new ImplDalServiceRessource();
            this.serviceConnectBase = new ImplDalConnectBase(this.serviceRessource.getDefaultStrConnection());
        }
        public  ImplDalAutorisationDepart(string strConnection)
        {
            this.serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region IntfDalAutorisationDepart Members
        string IntfDalAutorisationDepart.insertAutorisationDepart(crlAutorisationDepart AutorisationDepart)
        {
            #region declaration
            int nombreInsertion = 0;
            string numAutorisationDepart = "";
            IntfDalAutorisationDepart serviceAutorisationDepart = new ImplDalAutorisationDepart();
            #endregion

            #region implementation
            if (AutorisationDepart != null)
            {
                AutorisationDepart.NumAutorisationDepart = serviceAutorisationDepart.getNumAutorisationDepart(AutorisationDepart.agent.agence.SigleAgence);

                this.strCommande = "INSERT INTO `autorisationdepart` (`numAutorisationDepart`";
                this.strCommande += " ,`numerosFB`,`matriculeAgent`,`dateAD`,`recetteTotale`,`resteRegle`) ";
                this.strCommande += " VALUES ('" + AutorisationDepart.NumAutorisationDepart+ "'";
                this.strCommande += " ,'" + AutorisationDepart.NumerosFB + "','" + AutorisationDepart.MatriculeAgent + "',";
                this.strCommande += " '" + AutorisationDepart.DateAD.ToString("yyyy-MM-dd") + "','" + AutorisationDepart.RecetteTotale + "',";
                this.strCommande += " '" + AutorisationDepart.ResteRegle + "')";


                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    numAutorisationDepart = AutorisationDepart.NumAutorisationDepart;
                this.serviceConnectBase.closeConnection();

            }
            #endregion

            return numAutorisationDepart;
        }

        /*
        string IntfDalAutorisationDepart.insertADFacture(crlAutorisationDepart AutorisationDepart, double prixDeveloppement)
        {
            #region declaration
            string numAutorisationDepart = "";

            crlRecuAD recuAD = null;

            IntfDalAutorisationDepart serviceAutorisationDepart = new ImplDalAutorisationDepart();
            IntfDalRecuAD serviceRecuAD = new ImplDalRecuAD();
            #endregion

            #region implementation
            if (AutorisationDepart != null)
            {
                
                AutorisationDepart.NumAutorisationDepart = serviceAutorisationDepart.insertAutorisationDepart(AutorisationDepart);
                if (AutorisationDepart.NumAutorisationDepart != "")
                {
                    recuAD = new crlRecuAD();
                    recuAD.MatriculeAgent = AutorisationDepart.MatriculeAgent;
                    recuAD.Montant = prixDeveloppement.ToString("0");
                    recuAD.TypeRecuAD = "Développement";
                    recuAD.Date = DateTime.Now;
                    recuAD.Libele = "Développement";

                    recuAD.NumRecuAD = serviceRecuAD.insertRecuAD(recuAD);

                    if (recuAD.NumRecuAD != "")
                    {
                        if (serviceRecuAD.insertAssociationRecuADAD(recuAD.NumRecuAD, AutorisationDepart.NumAutorisationDepart))
                        {
                            numAutorisationDepart = AutorisationDepart.NumAutorisationDepart;
                        }
                    }
                        
                    
                }
            }
            #endregion

            return numAutorisationDepart;
        }*/

        bool IntfDalAutorisationDepart.deleteAutorisationDepart(crlAutorisationDepart AutorisationDepart)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (AutorisationDepart != null)
            {
                if (AutorisationDepart.NumAutorisationDepart != "")
                {
                    this.strCommande = "DELETE FROM `autorisationdepart` WHERE (`numAutorisationDepart` = '" + AutorisationDepart.NumAutorisationDepart + "')";
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

        bool IntfDalAutorisationDepart.deleteAutorisationDepart(string numAutorisationDepart)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
           
            if (numAutorisationDepart != "")
            {
                this.strCommande = "DELETE FROM `autorisationdepart` WHERE (`numAutorisationDepart` = '" + numAutorisationDepart + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete == 1)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }
            
            #endregion

            return isDelete;
        }

        bool IntfDalAutorisationDepart.updateAutorisationDepart(crlAutorisationDepart AutorisationDepart)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            #endregion

            #region implementation
            if (AutorisationDepart != null)
            {
                if (AutorisationDepart.NumAutorisationDepart != "")
                {
                    this.strCommande = "UPDATE `autorisationdepart` SET ";
                    this.strCommande += " `numerosFB`='" + AutorisationDepart.NumerosFB + "'";
                    this.strCommande += ", `matriculeAgent`='" +AutorisationDepart.MatriculeAgent + "'";
                    this.strCommande += ", `dateAD`='" + AutorisationDepart.DateAD.ToString("yyyy-MM-dd") + "'";
                    this.strCommande += ", `recetteTotale`='" + AutorisationDepart.RecetteTotale + "'";
                    this.strCommande += ", `resteRegle`='" + AutorisationDepart.ResteRegle + "'";
                    this.strCommande += " WHERE (`numAutorisationDepart`='" + AutorisationDepart.NumAutorisationDepart + "')";

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

        crlAutorisationDepart IntfDalAutorisationDepart.selectAutorisationDepart(string numAutorisationDepart)
        {
            #region declaration
            crlAutorisationDepart AutorisationDepart = null;
            IntfDalAgent serviceAgent = new ImplDalAgent();
            IntfDalFacture serviceFacture = new ImplDalFacture();
            IntfDalFicheBord serviceFicheBord = new ImplDalFicheBord();
            #endregion

            #region implementation
            if (numAutorisationDepart != "")
            {
                this.strCommande = "SELECT * FROM `autorisationdepart` WHERE (`numAutorisationDepart`='" + numAutorisationDepart + "')";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            AutorisationDepart = new crlAutorisationDepart();
                            AutorisationDepart.NumAutorisationDepart = reader["numAutorisationDepart"].ToString();
                            AutorisationDepart.NumerosFB = reader["numerosFB"].ToString();
                            AutorisationDepart.MatriculeAgent = reader["matriculeAgent"].ToString();
                            try
                            {
                                AutorisationDepart.DateAD = Convert.ToDateTime(this.reader["dateAD"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                AutorisationDepart.RecetteTotale = double.Parse(this.reader["recetteTotale"].ToString());
                                AutorisationDepart.ResteRegle = double.Parse(this.reader["resteRegle"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (AutorisationDepart != null)
                {
                    if (AutorisationDepart.MatriculeAgent != "")
                    {
                        AutorisationDepart.agent = serviceAgent.selectAgent(AutorisationDepart.MatriculeAgent);
                    }
                    if (AutorisationDepart.NumerosFB != "")
                    {
                        AutorisationDepart.ficheBord = serviceFicheBord.selectFicheBord(AutorisationDepart.NumerosFB);
                    }
                    
                }
            }
            #endregion

            return AutorisationDepart;
        }

        string IntfDalAutorisationDepart.getNumAutorisationDepart(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numAutorisationDepart = "00001";
            string[] tempNumAutorisationDepart = null;
            string strDate = "AD" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT autorisationdepart.numAutorisationDepart AS maxNum FROM autorisationdepart";
            this.strCommande += " WHERE autorisationdepart.numAutorisationDepart LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumAutorisationDepart = reader["maxNum"].ToString().ToString().Split('/');
                        numAutorisationDepart = tempNumAutorisationDepart[tempNumAutorisationDepart.Length - 1];
                    }
                    numTemp = double.Parse(numAutorisationDepart) + 1;
                    if (numTemp < 10)
                        numAutorisationDepart = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numAutorisationDepart = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numAutorisationDepart = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numAutorisationDepart = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numAutorisationDepart = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numAutorisationDepart = strDate + "/" + sigleAgence + "/" + numAutorisationDepart;
            #endregion

            return numAutorisationDepart;
        }

        double IntfDalAutorisationDepart.getMontanRecu(string numAutorisationDepart)
        {
            #region declaration
            double montant = 0;
            #endregion

            #region implementation
            if (numAutorisationDepart != "")
            {
                this.strCommande = "SELECT Sum(recuad.montant) AS montantTotal FROM recuad";
                this.strCommande += " Inner Join prelevement ON prelevement.numPrelevement = recuad.numPrelevement";
                this.strCommande += " WHERE prelevement.numAutorisationDepart = '" + numAutorisationDepart + "'";

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
                                montant = double.Parse(reader["montantTotal"].ToString());
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

            return montant;
        }

        double IntfDalAutorisationDepart.getMontanPrelevement(string numAutorisationDepart)
        {
            #region declaration
            double montant = 0;
            #endregion

            #region implementation
            if (numAutorisationDepart != "")
            {
                this.strCommande = "SELECT Sum(prelevement.montantPrelevement) AS montantTotal FROM prelevement";
                this.strCommande += " WHERE prelevement.numAutorisationDepart = '" + numAutorisationDepart + "'";

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
                                montant = double.Parse(reader["montantTotal"].ToString());
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

            return montant;
        }

        double IntfDalAutorisationDepart.getMontanDevelopement(string numAutorisationDepart)
        {
            #region declaration
            double montant = 0;
            #endregion

            #region implementation
            if (numAutorisationDepart != "")
            {
                this.strCommande = "SELECT Sum(prelevement.montantPrelevement) AS montantTotal FROM prelevement";
                this.strCommande += " WHERE prelevement.numAutorisationDepart = '" + numAutorisationDepart + "' AND";
                this.strCommande += " prelevement.typePrelevement = 'Développement'";

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
                                montant = double.Parse(reader["montantTotal"].ToString());
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

            return montant;
        }

        List<crlAutorisationDepart> IntfDalAutorisationDepart.selectADProprietaireFactureIsNull(string numProprietaire)
        {
            #region declaration
            crlAutorisationDepart tempAutorisationDepart = null;
            List<crlAutorisationDepart> autorisationDeparts = null;

            IntfDalAgent serviceAgent = new ImplDalAgent();
            IntfDalFacture serviceFacture = new ImplDalFacture();
            IntfDalFicheBord serviceFicheBord = new ImplDalFicheBord();
            #endregion

            #region implementation
            if (numProprietaire != "")
            {
                this.strCommande = "SELECT (autorisationdepart.numAutorisationDepart) AS numAD, (autorisationdepart.numerosFB) AS numFB,";
                this.strCommande += " autorisationdepart.matriculeAgent FROM autorisationdepart";
                this.strCommande += " Inner Join fichebord ON fichebord.numerosFB = autorisationdepart.numerosFB";
                this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.numerosAV = fichebord.numerosAV";
                this.strCommande += " Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
                this.strCommande += " Inner Join licence ON licence.numLicence = verification.numLicence";
                this.strCommande += " Inner Join vehicule ON vehicule.numVehicule = licence.numVehicule";
                this.strCommande += " Left Join assocautorisationdepartfacture ON assocautorisationdepartfacture.numAutorisationDepart = autorisationdepart.numAutorisationDepart";
                this.strCommande += " WHERE assocautorisationdepartfacture.numFacture IS NULL  AND";
                this.strCommande += " vehicule.numProprietaire = '" + numProprietaire + "'";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        autorisationDeparts = new List<crlAutorisationDepart>();
                        while(reader.Read())
                        {
                            tempAutorisationDepart = new crlAutorisationDepart();
                            tempAutorisationDepart.NumAutorisationDepart = reader["numAD"].ToString();
                            tempAutorisationDepart.NumerosFB = reader["numFB"].ToString();
                            tempAutorisationDepart.MatriculeAgent = reader["matriculeAgent"].ToString();

                            autorisationDeparts.Add(tempAutorisationDepart);
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (autorisationDeparts != null)
                {
                    for (int i = 0; i < autorisationDeparts.Count; i++)
                    {
                        if (autorisationDeparts[i].MatriculeAgent != "")
                        {
                            autorisationDeparts[i].agent = serviceAgent.selectAgent(autorisationDeparts[i].MatriculeAgent);
                        }
                        if (autorisationDeparts[i].NumerosFB != "")
                        {
                            autorisationDeparts[i].ficheBord = serviceFicheBord.selectFicheBord(autorisationDeparts[i].NumerosFB);
                        }
                    }
                        

                }
            }
            #endregion

            return autorisationDeparts;
        }

        List<crlAutorisationDepart> IntfDalAutorisationDepart.selectADProprietaireResteNonNull(string numProprietaire)
        {
            #region declaration
            crlAutorisationDepart tempAutorisationDepart = null;
            List<crlAutorisationDepart> autorisationDeparts = null;

            IntfDalAgent serviceAgent = new ImplDalAgent();
            IntfDalFacture serviceFacture = new ImplDalFacture();
            IntfDalFicheBord serviceFicheBord = new ImplDalFicheBord();
            #endregion

            #region implementation
            if (numProprietaire != "")
            {
                this.strCommande = "SELECT (autorisationdepart.numAutorisationDepart) AS numAD, (autorisationdepart.numerosFB) AS numFB,";
                this.strCommande += " autorisationdepart.matriculeAgent, autorisationdepart.recetteTotale, autorisationdepart.dateAD,";
                this.strCommande += " autorisationdepart.resteRegle FROM autorisationdepart";
                this.strCommande += " Inner Join fichebord ON fichebord.numerosFB = autorisationdepart.numerosFB";
                this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.numerosAV = fichebord.numerosAV";
                this.strCommande += " Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
                this.strCommande += " Inner Join licence ON licence.numLicence = verification.numLicence";
                this.strCommande += " Inner Join vehicule ON vehicule.numVehicule = licence.numVehicule";
                this.strCommande += " WHERE autorisationdepart.resteRegle > 0  AND";
                this.strCommande += " vehicule.numProprietaire = '" + numProprietaire + "'";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        autorisationDeparts = new List<crlAutorisationDepart>();
                        while (reader.Read())
                        {
                            tempAutorisationDepart = new crlAutorisationDepart();
                            tempAutorisationDepart.NumAutorisationDepart = reader["numAD"].ToString();
                            tempAutorisationDepart.NumerosFB = reader["numFB"].ToString();
                            tempAutorisationDepart.MatriculeAgent = reader["matriculeAgent"].ToString();
                            try
                            {
                                tempAutorisationDepart.DateAD = Convert.ToDateTime(reader["dateAD"].ToString());
                            }
                            catch (Exception)
                            {
                            }

                            try
                            {
                                tempAutorisationDepart.RecetteTotale = double.Parse(reader["recetteTotale"].ToString());
                                tempAutorisationDepart.ResteRegle = double.Parse(reader["resteRegle"].ToString());
                            }
                            catch (Exception)
                            {
                            }

                            autorisationDeparts.Add(tempAutorisationDepart);
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (autorisationDeparts != null)
                {
                    for (int i = 0; i < autorisationDeparts.Count; i++)
                    {
                        if (autorisationDeparts[i].MatriculeAgent != "")
                        {
                            autorisationDeparts[i].agent = serviceAgent.selectAgent(autorisationDeparts[i].MatriculeAgent);
                        }
                        if (autorisationDeparts[i].NumerosFB != "")
                        {
                            autorisationDeparts[i].ficheBord = serviceFicheBord.selectFicheBord(autorisationDeparts[i].NumerosFB);
                        }
                    }


                }
            }
            #endregion

            return autorisationDeparts;
        }

        List<crlAutorisationDepart> IntfDalAutorisationDepart.selectADFacture(string numFacture)
        {
            #region declaration
            crlAutorisationDepart tempAutorisationDepart = null;
            List<crlAutorisationDepart> autorisationDeparts = null;

            IntfDalAgent serviceAgent = new ImplDalAgent();
            IntfDalFacture serviceFacture = new ImplDalFacture();
            IntfDalFicheBord serviceFicheBord = new ImplDalFicheBord();
            #endregion

            #region implementation
            if (numFacture != "")
            {
                this.strCommande = "SELECT (autorisationdepart.numAutorisationDepart)  AS numAD, autorisationdepart.numerosFB,";
                this.strCommande += " autorisationdepart.matriculeAgent, autorisationdepart.recetteTotale, autorisationdepart.dateAD,";
                this.strCommande += " autorisationdepart.resteRegle FROM autorisationdepart";
                this.strCommande += " Inner Join assocautorisationdepartfacture ON assocautorisationdepartfacture.numAutorisationDepart = autorisationdepart.numAutorisationDepart";
                this.strCommande += " WHERE assocautorisationdepartfacture.numFacture = '" + numFacture + "'";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        autorisationDeparts = new List<crlAutorisationDepart>();
                        while (reader.Read())
                        {
                            tempAutorisationDepart = new crlAutorisationDepart();
                            tempAutorisationDepart.NumAutorisationDepart = reader["numAD"].ToString();
                            tempAutorisationDepart.NumerosFB = reader["numerosFB"].ToString();
                            tempAutorisationDepart.MatriculeAgent = reader["matriculeAgent"].ToString();
                            try
                            {
                                tempAutorisationDepart.DateAD = Convert.ToDateTime(reader["dateAD"].ToString());
                            }
                            catch (Exception)
                            {
                            }

                            try
                            {
                                tempAutorisationDepart.RecetteTotale = Double.Parse(reader["recetteTotale"].ToString());
                            }catch(Exception){}

                            try
                            {
                                tempAutorisationDepart.ResteRegle = double.Parse(reader["resteRegle"].ToString());
                            }catch(Exception){}

                            autorisationDeparts.Add(tempAutorisationDepart);
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (autorisationDeparts != null)
                {
                    for (int i = 0; i < autorisationDeparts.Count; i++)
                    {
                        if (autorisationDeparts[i].MatriculeAgent != "")
                        {
                            autorisationDeparts[i].agent = serviceAgent.selectAgent(autorisationDeparts[i].MatriculeAgent);
                        }
                        if (autorisationDeparts[i].NumerosFB != "")
                        {
                            autorisationDeparts[i].ficheBord = serviceFicheBord.selectFicheBord(autorisationDeparts[i].NumerosFB);
                        }
                    }


                }
            }
            #endregion

            return autorisationDeparts;
        }
        #endregion

        #region liste autorisation depart
        void IntfDalAutorisationDepart.insertToGridAutorisationDepart(GridView gridView, string param, string paramLike, string valueLike, string numAgence)
        {
            #region declaration
            IntfDalAutorisationDepart serviceAutorisationDepart = new ImplDalAutorisationDepart();
            #endregion

            #region implementation
            this.strCommande = "SELECT Sum(recuad.montant) AS sommeRecu, fichebord.dateHeurDepart,";
            this.strCommande += " fichebord.numerosFB, itineraire.numVilleItineraireDebut, itineraire.numVilleItineraireFin,";
            this.strCommande += " vehicule.matriculeVehicule, chauffeur.nomChauffeur, chauffeur.prenomChauffeur, licence.nombrePlacePayante,";
            this.strCommande += " vehicule.marqueVehicule, vehicule.couleurVehicule,autorisationdepart.numAutorisationDepart FROM autorisationdepart";
            this.strCommande += " Inner Join prelevement ON prelevement.numAutorisationDepart = autorisationdepart.numAutorisationDepart";
            this.strCommande += " Inner Join recuad ON recuad.numPrelevement = prelevement.numPrelevement";
            this.strCommande += " Inner Join fichebord ON fichebord.numerosFB = autorisationdepart.numerosFB";
            this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.numerosAV = fichebord.numerosAV";
            this.strCommande += " Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
            this.strCommande += " Inner Join itineraire ON itineraire.idItineraire = verification.idItineraire";
            this.strCommande += " Inner Join licence ON licence.numLicence = verification.numLicence";
            this.strCommande += " Inner Join vehicule ON vehicule.numVehicule = licence.numVehicule";
            this.strCommande += " Inner Join chauffeur ON chauffeur.idChauffeur = verification.idChauffeur";
            this.strCommande += " Inner Join agent ON agent.matriculeAgent = autorisationdepart.matriculeAgent";
            this.strCommande += " Left Join assocautorisationdepartfacture ON assocautorisationdepartfacture.numAutorisationDepart = autorisationdepart.numAutorisationDepart";
            this.strCommande += " WHERE agent.numAgence =  '" + numAgence + "' AND";
            this.strCommande += " assocautorisationdepartfacture.numFacture IS NULL AND";
            this.strCommande += " " + paramLike + " LIKE  '%" + valueLike + "%'";
            this.strCommande += " GROUP BY autorisationdepart.numAutorisationDepart ORDER BY " + param + " ASC";

            gridView.DataSource = serviceAutorisationDepart.getDataTableAutorisationDepart(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalAutorisationDepart.getDataTableAutorisationDepart(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalVille serviceVille = new ImplDalVille();
            IntfDalFicheBord serviceFicheBord = new ImplDalFicheBord();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();

            crlVille villeD = new crlVille();
            crlVille villeF = new crlVille();

            double montant = 0.00;
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numAutorisationDepart", typeof(string));
            dataTable.Columns.Add("dateDepart", typeof(DateTime));
            dataTable.Columns.Add("itineraire", typeof(string));
            dataTable.Columns.Add("chauffeur", typeof(string));
            dataTable.Columns.Add("voiture", typeof(string));
            dataTable.Columns.Add("montant", typeof(string));
            dataTable.Columns.Add("montantRecu", typeof(string));
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

                        dr["numAutorisationDepart"] = reader["numAutorisationDepart"].ToString();
                        dr["dateDepart"] = Convert.ToDateTime(reader["dateHeurDepart"].ToString());

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

                        dr["chauffeur"] = reader["nomChauffeur"].ToString() + " " + reader["prenomChauffeur"].ToString();
                        dr["voiture"] = reader["matriculeVehicule"].ToString() + " " + reader["marqueVehicule"].ToString() + " " + reader["couleurVehicule"].ToString();

                        montant = serviceFicheBord.getPrixTotalBagage(reader["numerosFB"].ToString()) + serviceFicheBord.getPrixTotalBillet(reader["numerosFB"].ToString()) + serviceFicheBord.getPrixTotalCommission(reader["numerosFB"].ToString());

                        dr["montant"] = serviceGeneral.separateurDesMilles(montant.ToString("0")) + "Ar";
                        dr["montantRecu"] = serviceGeneral.separateurDesMilles(reader["sommeRecu"].ToString()) + "Ar";

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