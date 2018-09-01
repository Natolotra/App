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
using AppRessources.Ressources;

namespace arch.dal.impl
{
    /// <summary>
    /// implementation du service verification
    /// </summary>
    public class ImplDalVerification : IntfDalVerification
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalVerification()
        {
            this.serviceRessource = new ImplDalServiceRessource();
            this.serviceConnectBase = new ImplDalConnectBase(this.serviceRessource.getDefaultStrConnection());
        }
        public ImplDalVerification(string strConnection)
        {
            this.serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region IntfDalVerification Members

        string IntfDalVerification.insertVerification(crlVerification Verification, string sigleAgence)
        {
            #region declaration
            int nombreInsertion = 0;
            string idVerification = "";
            IntfDalVerification serviceVerification = new ImplDalVerification();
            #endregion

            #region implementation
            if (Verification != null)
            {
                Verification.IdVerification = serviceVerification.getIdVerification(sigleAgence);

                this.strCommande = "INSERT INTO `verification` (`idVerification`,`numLicence`,`idItineraire`,";
                this.strCommande += " `idChauffeur`,`matriculeAgent`,`verificationTechnique`,`aVoireVT`,`verificationPapier`,`aVoireVP`,`dateVerification`,`planDepart`) ";
                this.strCommande += " VALUES ('" + Verification.IdVerification + "','" + Verification.NumLicence + "','" + Verification.IdItineraire + "'";
                this.strCommande += " ,'" + Verification.IdChauffeur + "','" + Verification.MatriculeAgent + "'";
                this.strCommande += " ,'" + Verification.VerificationTechnique + "', '" + Verification.AVoireVT + "'";
                this.strCommande += " ,'" + Verification.VerificationPapier +"', '" + Verification.AVoireVP + "', '" + Verification.DateVerification.ToString("yyyy-MM-dd") + "','" + Verification.PlanDepart + "')";

                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    idVerification = Verification.IdVerification;
                this.serviceConnectBase.closeConnection();

            }
            #endregion

            return idVerification;
        }

        bool IntfDalVerification.deleteVerification(crlVerification Verification)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (Verification!= null)
            {
                if (Verification.IdVerification != "")
                {
                    this.strCommande = "DELETE FROM `verification` WHERE (`idVerification` = '" + Verification.IdVerification + "')";
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

        bool IntfDalVerification.deleteVerification(string idVerification)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (idVerification != "")
            {
                this.strCommande = "DELETE FROM `verification` WHERE (`idVerification` = '" + idVerification + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete == 1)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }
            
            #endregion

            return isDelete;
        }

        bool IntfDalVerification.updateVerification(crlVerification Verification)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            #endregion

            #region implementation
            if (Verification != null)
            {
                if (Verification.IdVerification != "")
                {
                    this.strCommande = "UPDATE `verification` SET ";
                    this.strCommande += ",`numLicence`='" + Verification.NumLicence + "',`idChauffeur`='" + Verification.IdChauffeur + "',`idItineraire`='" + Verification.IdItineraire + "'";
                    this.strCommande += ",`matriculeAgent`='" + Verification.MatriculeAgent + "',`verificationTechnique`='" + Verification.VerificationTechnique + "'";
                    this.strCommande += ",`aVoireVT`='" + Verification.AVoireVT + "',`verificationPapier`='" + Verification.VerificationPapier + "'";
                    this.strCommande += ",`aVoireVP`='" + Verification.AVoireVP + "', `planDepart`='" + Verification.PlanDepart + "'";
                    this.strCommande += " WHERE (`idVerification`='" + Verification.IdVerification + "')";

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

        int IntfDalVerification.isVerification(crlVerification Verification)
        {
            #region declaration
            int isTest = 0;
            int nbVerification = 0;
            #endregion

            #region implementation
            if (Verification != null) 
            {
                this.strCommande = "SELECT Count(verification.idVerification) As nbVerification FROM verification";
                this.strCommande += " WHERE verification.dateVerification = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
                this.strCommande += " AND (verification.numLicence = '" + Verification.NumLicence + "' OR";
                this.strCommande += " verification.idChauffeur = '" + Verification.IdChauffeur + "')";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        nbVerification = int.Parse(reader["nbVerification"].ToString());
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                if (nbVerification < int.Parse(ReGlobalParam.nbVerificationParJourParVehicule))
                    isTest = 1;
            }
            #endregion

            return isTest;
        }

        crlVerification IntfDalVerification.selectVerification(string idVerification)
        {
            #region declaration
            IntfDalLicence serviceLicence = new ImplDalLicence();
            IntfDalAgent serviceAgent = new ImplDalAgent();
            IntfDalChauffeur serviceChauffeur = new ImplDalChauffeur();
            IntfDalItineraire serviceItineraire = new ImplDalItineraire();

            crlVerification verification = null;
            #endregion

            #region implementation
            if (idVerification != "") 
            {
                this.strCommande = "SELECT * FROM `verification` WHERE (`idVerification`='" + idVerification + "')";
                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null) 
                {
                    if (reader.HasRows) 
                    {
                        if (reader.Read()) 
                        {
                            verification = new crlVerification();
                            verification.AVoireVP = reader["aVoireVP"].ToString();
                            verification.AVoireVT = reader["aVoireVT"].ToString();
                            verification.IdItineraire = reader["idItineraire"].ToString();
                            try
                            {
                                verification.DateVerification = Convert.ToDateTime(reader["dateVerification"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            verification.IdChauffeur = reader["idChauffeur"].ToString();
                            verification.IdVerification = reader["idVerification"].ToString();
                            verification.MatriculeAgent = reader["matriculeAgent"].ToString();
                            verification.NumLicence = reader["numLicence"].ToString();
                            verification.ObservationProfessionnelle = reader["observationProfessionnelle"].ToString();
                            verification.VerificationPapier = int.Parse(reader["verificationPapier"].ToString());
                            verification.VerificationTechnique = int.Parse(reader["verificationTechnique"].ToString());
                            verification.PlanDepart = int.Parse(reader["planDepart"].ToString());
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (verification != null)
                {
                    verification.Licence = serviceLicence.selectLicence(verification.NumLicence);
                    verification.Agent = serviceAgent.selectAgent(verification.MatriculeAgent);
                    verification.Chauffeur = serviceChauffeur.selectChauffeur(verification.IdChauffeur);
                    verification.Itineraire = serviceItineraire.selectItineraire(verification.IdItineraire);
                }
            }
            #endregion

            return verification;
        }

        string IntfDalVerification.getIdVerification(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string idVerification = "00001";
            string[] tempIdVerification = null;
            string strDate = "VR" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT verification.idVerification AS maxNum FROM verification";
            this.strCommande += " WHERE verification.idVerification LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempIdVerification = reader["maxNum"].ToString().ToString().Split('/');
                        idVerification = tempIdVerification[tempIdVerification.Length - 1];
                    }
                    numTemp = double.Parse(idVerification) + 1;
                    if (numTemp < 10)
                        idVerification = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        idVerification = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        idVerification = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        idVerification = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        idVerification = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            idVerification = strDate + "/" + sigleAgence + "/" + idVerification;
            #endregion

            return idVerification;
        }

        void IntfDalVerification.loadDdlTri(DropDownList ddlTri)
        {
            ddlTri.Items.Clear();
            ddlTri.Items.Add(new ListItem("Numeros", "idVerification"));
            ddlTri.Items.Add(new ListItem("Numeros Voiture", "numVehicule"));
            ddlTri.Items.Add(new ListItem("Numeros Chauffeur", "idChauffeur"));
            ddlTri.Items.Add(new ListItem("Matricule agent", "matriculeAgent"));
            ddlTri.Items.Add(new ListItem("A voire VT", "aVoireVT"));
            ddlTri.Items.Add(new ListItem("A voire VP", "aVoireVP"));
        }

        #endregion

        #region insert to grid
        void IntfDalVerification.insertToGridVerificationNonValider(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalVerification serviceVerification = new ImplDalVerification();
            #endregion

            #region implementation

            this.strCommande = "SELECT chauffeur.idChauffeur, chauffeur.nomChauffeur, chauffeur.prenomChauffeur,";
            this.strCommande += " chauffeur.cinChauffeur, chauffeur.adresseChauffeur, chauffeur.telephoneChauffeur,";
            this.strCommande += " chauffeur.telephoneMobileChauffeur, chauffeur.imageChauffeur, chauffeur.numCooperative,";
            this.strCommande += " chauffeur.situationFamilialeChauffeur, chauffeur.dateNaissanceChauffeur, verification.dateVerification,";
            this.strCommande += " chauffeur.lieuNaissanceChauffeur, verification.idVerification, vehicule.numVehicule,";
            this.strCommande += " vehicule.numParamVehicule, vehicule.sourceEnergie, vehicule.numProprietaire,";
            this.strCommande += " vehicule.matriculeVehicule, vehicule.marqueVehicule, vehicule.typeVehicule,";
            this.strCommande += " vehicule.numSerieVehicule, vehicule.numMoteurVehicule, vehicule.puissanceVehicule,";
            this.strCommande += " vehicule.couleurVehicule, vehicule.placesAssiseVehicule, vehicule.nombreColoneVehicule,";
            this.strCommande += " vehicule.poidsTotalVehicule, vehicule.poidsVideVehicule, vehicule.imageVehicule,";
            this.strCommande += " itineraire.numVilleItineraireDebut, itineraire.numVilleItineraireFin FROM verification";
            this.strCommande += " Inner Join chauffeur ON chauffeur.idChauffeur = verification.idChauffeur";
            this.strCommande += " Inner Join licence ON licence.numLicence = verification.numLicence";
            this.strCommande += " Inner Join vehicule ON vehicule.numVehicule = licence.numVehicule";
            this.strCommande += " Left Join autorisationvoyage ON autorisationvoyage.idVerification = verification.idVerification";
            this.strCommande += " Inner Join itineraire ON itineraire.idItineraire = verification.idItineraire";
            this.strCommande += " WHERE autorisationvoyage.idVerification IS NULL AND";
            this.strCommande += " (" + paramLike + " LIKE  '%" + valueLike + "%')";
            this.strCommande += " ORDER BY " + param + " DESC";


            gridView.DataSource = serviceVerification.getDataTableVerificationNonValider(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalVerification.getDataTableVerificationNonValider(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalVille serviceVille = new ImplDalVille();

            crlVille villeD = null;
            crlVille villeF = null;
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("idVerification", typeof(string));
            dataTable.Columns.Add("dateVerification", typeof(DateTime));
            dataTable.Columns.Add("vehicule", typeof(string));
            dataTable.Columns.Add("chauffeur", typeof(string));
            dataTable.Columns.Add("itineraire", typeof(string));
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

                        dr["idVerification"] = reader["idVerification"].ToString();
                        dr["dateVerification"] = Convert.ToDateTime(reader["dateVerification"].ToString());
                        dr["vehicule"] = reader["matriculeVehicule"].ToString() + " " + reader["marqueVehicule"].ToString() + " " + reader["couleurVehicule"].ToString();
                        dr["chauffeur"] = reader["prenomChauffeur"].ToString() + " " + reader["nomChauffeur"].ToString();

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
