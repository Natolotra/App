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

namespace arch.dal.impl
{
    public class ImplDalServicePage : IntfDalServicePage
    {
        #region variable de class
        MySqlConnection mySqlConnect = null;
        MySqlCommand mySqlCmd = null;
        MySqlDataAdapter mySqlDataAdapt = null;

        DataSet dsTable = null;
        DataView dvTable = null;
        DataTable dtTable = null;

        string strConnection = "";
        string strCommande = "";

        IntfDalServiceRessource serviceRessource = null;
        #endregion

        #region constructeur
        public ImplDalServicePage(string strConnection)
        {
            this.strConnection = strConnection;
        }
        public ImplDalServicePage()
        {
            serviceRessource = new ImplDalServiceRessource();
            strConnection = serviceRessource.getDefaultStrConnection();
        }
        #endregion

        #region IntfDalServicePage Membres

        DataView IntfDalServicePage.getDateTable(string strCommand)
        {
            #region Region declaration des variables
            mySqlConnect = new MySqlConnection(this.strConnection);
            #endregion

            string sqlReq = null;
            //Contruction de la requete
            sqlReq = strCommand;

            //Ouverture de la connexion à la base de données
            mySqlConnect.Open();

            try
            {
                //Faire l'objet commande
                mySqlCmd = new MySqlCommand(sqlReq, mySqlConnect);

                //Ajouter les paramètres
                dvTable = new DataView();
                dsTable = new DataSet();
                mySqlDataAdapt = new MySqlDataAdapter(mySqlCmd);

                

                mySqlDataAdapt.Fill(dsTable, "table");
                dvTable = dsTable.Tables["table"].DefaultView;
            }
            catch (Exception erreur)
            {
                throw new Exception("une erreur est recuperée sur : " + erreur.Message);
            }
            finally
            {
                mySqlDataAdapt.Dispose();
                mySqlCmd.Dispose();
                mySqlConnect.Close();
                mySqlConnect = null;
            }
            return dvTable;
        }

        #endregion

        #region IntfDalServicePage Members

        void IntfDalServicePage.insertToGridView(GridView gridView, string nomTable, string param)
        {
            #region Region initialisation
            IntfDalServicePage servicePage = new ImplDalServicePage();
            #endregion

            this.strCommande = "SELECT * FROM `" + nomTable + "` ORDER BY " + param + " ASC";

            dtTable = servicePage.getDateTable(strCommande).ToTable();
            gridView.DataSource = dtTable;
            gridView.DataBind();
        }

        void IntfDalServicePage.insertToGridView(GridView gridView, string nomTable, string param, string paramLike, string valueLike)
        {
            #region Region initialisation
            IntfDalServicePage servicePage = new ImplDalServicePage();
            #endregion

            this.strCommande = "SELECT * FROM `" + nomTable + "` WHERE `" + paramLike + "` LIKE '%" + valueLike + "%' ORDER BY `" + param + "` ASC";

            dtTable = servicePage.getDateTable(strCommande).ToTable();
            gridView.DataSource = dtTable;
            gridView.DataBind();
        }

        #endregion

        #region IntfDalServicePage Members

        bool IntfDalServicePage.testConnection()
        {
            #region declaration
            ImplDalConnectBase connection = null;
            bool isConnection = false;
            #endregion

            #region implementation
            connection = new ImplDalConnectBase(this.strConnection);
            connection.openConnection();
            isConnection = connection.IsConnection;
            connection.closeConnection();
            #endregion

            return isConnection;
        }

        #endregion

        #region insert to grid agent

        void IntfDalServicePage.insertToGridViewAgentVerificateur(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region Region initialisation
            IntfDalServicePage servicePage = new ImplDalServicePage();
            #endregion

            this.strCommande = "SELECT * FROM `agent` WHERE `typeAgent`='verificateur' AND `" + paramLike + "` LIKE '%" + valueLike + "%' ORDER BY `" + param + "` ASC";

            dtTable = servicePage.getDateTable(strCommande).ToTable();
            gridView.DataSource = dtTable;
            gridView.DataBind();
        }

        #endregion

        #region insert to grid voiture
        void IntfDalServicePage.insertToGridViewVoiture(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region Region initialisation
            IntfDalServicePage servicePage = new ImplDalServicePage();
            #endregion

            this.strCommande = "SELECT voiture.numVehicule, voiture.numLicence As numLV, voiture.couleur, voiture.`type`,";
            this.strCommande += " voiture.marque, voiture.nombrePlace, voiture.numImmatricule, licence.numLicence As numLL,";
            this.strCommande += " licence.numerosSociete, licence.nom, licence.prenom, licence.domiciliee, licence.datePremierExploitation,";
            this.strCommande += " licence.dateValideDu, licence.dateValideAu FROM voiture Inner Join licence ON licence.numLicence = voiture.numLicence";
            this.strCommande += " WHERE `" + paramLike + "` LIKE '%" + valueLike + "%' ORDER BY `" + param + "` ASC";

            dtTable = servicePage.getDateTable(strCommande).ToTable();
            gridView.DataSource = dtTable;
            gridView.DataBind();
        }
        #endregion

        #region insert to grid Autorisation
        void IntfDalServicePage.insertToGridViewAutorisation(GridView gridView, string param, string paramLike, string valueLike, string numAgence)
        {
            #region Region initialisation
            IntfDalServicePage servicePage = new ImplDalServicePage();
            #endregion

            this.strCommande = "SELECT autorisationvoyage.numerosAV, autorisationvoyage.idVerification, autorisationvoyage.matriculeAgent,";
            this.strCommande += " autorisationvoyage.datePrevueDepart, verification.idVerification, verification.numVehicule,";
            this.strCommande += " verification.idChauffeur, verification.matriculeAgent, verification.verificationTechnique,";
            this.strCommande += " verification.aVoireVT, verification.verificationPapier, verification.aVoireVP, verification.observationProfessionnelle,";
            this.strCommande += " verification.dateVerification, verification.planDepart, chauffeur.idChauffeur,";
            this.strCommande += " chauffeur.nomChauffeur, chauffeur.prenomChauffeur, chauffeur.cinChauffeur, chauffeur.adresseChauffeur,";
            this.strCommande += " chauffeur.telephoneChauffeur, chauffeur.telephoneMobileChauffeur, voiture.numVehicule,";
            this.strCommande += " voiture.numLicence, voiture.couleur, voiture.type, voiture.marque, voiture.nombrePlace, voiture.numImmatricule,";
            this.strCommande += " agent.matriculeAgent, agent.numerosGareRoutiere, agent.typeAgent, agent.nomAgent,";
            this.strCommande += " agent.prenomAgent, agent.dateNaissanceAgent, agent.lieuNaissanceAgent, agent.loginAgent,";
            this.strCommande += " agent.motDePasseAgent, agent.cinAgent, agent.adresseAgent, agent.telephoneAgent, agent.telephoneMobileAgent";
            this.strCommande += " FROM autorisationvoyage Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
            this.strCommande += " Inner Join chauffeur ON chauffeur.idChauffeur = verification.idChauffeur";
            this.strCommande += " Inner Join voiture ON voiture.numVehicule = verification.numVehicule";
            this.strCommande += " Inner Join agent ON agent.matriculeAgent = autorisationvoyage.matriculeAgent";
            this.strCommande += " WHERE agent.numAgence = '" + numAgence + "' AND ";
            this.strCommande += " `" + paramLike + "` LIKE '%" + valueLike + "%' ORDER BY `" + param + "` ASC";

            dtTable = servicePage.getDateTable(strCommande).ToTable();
            gridView.DataSource = dtTable;
            gridView.DataBind();
        }
        #endregion

        #region insert to grid Autorisation sans fiche de bord
        void IntfDalServicePage.insertToGridViewAutorisationSanFicheBord(GridView gridView, string param, string paramLike, string valueLike, string numAgence)
        {
            #region Region initialisation
            IntfDalServicePage servicePage = new ImplDalServicePage();
            #endregion

            this.strCommande = "SELECT autorisationvoyage.numerosAV, voiture.numImmatricule, chauffeur.nomChauffeur,";
            this.strCommande += " chauffeur.prenomChauffeur, autorisationvoyage.datePrevueDepart, itineraire.villeItineraireDebut,";
            this.strCommande += " itineraire.villeItineraireFin FROM autorisationvoyage";
            this.strCommande += " Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
            this.strCommande += " Inner Join voiture ON voiture.numVehicule = verification.numVehicule";
            this.strCommande += " Inner Join chauffeur ON chauffeur.idChauffeur = verification.idChauffeur";
            this.strCommande += " Inner Join itineraire ON itineraire.idItineraire = verification.idItineraire";
            this.strCommande += " Left Join fichebord ON fichebord.numerosAV = autorisationvoyage.numerosAV";
            this.strCommande += " Inner Join agent ON agent.matriculeAgent = autorisationvoyage.matriculeAgent";
            this.strCommande += " WHERE agent.numAgence = '" + numAgence + "' AND ";
            this.strCommande += " fichebord.numerosAV IS NULL AND";
            this.strCommande += " " + paramLike + " LIKE '%" + valueLike + "%' ORDER BY " + param + " ASC";

            dtTable = servicePage.getDateTable(strCommande).ToTable();
            gridView.DataSource = dtTable;
            gridView.DataBind();
        }
        #endregion
    }
}
