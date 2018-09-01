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

namespace arch.dal.impl
{
    /// <summary>
    /// Implementation du service Autorisation de voyage
    /// </summary>
    public class ImplAutorisationVoyage : IntfAutorisationVoyage
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplAutorisationVoyage()
        {
            this.serviceRessource = new ImplDalServiceRessource();
            this.serviceConnectBase = new ImplDalConnectBase(this.serviceRessource.getDefaultStrConnection());
        }
        public ImplAutorisationVoyage(string strConnection)
        {
            this.serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region IntfAutorisationVoyage Members

        string IntfAutorisationVoyage.insertAutorisationVoyage(crlAutorisationVoyage AutorisationVoyage)
        {
            #region declaration
            int nombreInsertion = 0;
            string numerosAV = "";
            IntfAutorisationVoyage serviceAutorisationVoyage = new ImplAutorisationVoyage();
            #endregion

            #region implementation
            if (AutorisationVoyage != null)
            {
                AutorisationVoyage.NumerosAV = serviceAutorisationVoyage.getNumerosAV(AutorisationVoyage.Agent.agence.SigleAgence);

                this.strCommande = "INSERT INTO `autorisationvoyage` (`numerosAV`,`idVerification`,";
                this.strCommande += " `matriculeAgent`,`datePrevueDepart`) ";
                this.strCommande += " VALUES ('" + AutorisationVoyage.NumerosAV + "','" + AutorisationVoyage.IdVerification + "'";
                this.strCommande += " ,'" + AutorisationVoyage.MatriculeAgent + "','" + AutorisationVoyage.DatePrevueDepart.ToString("yyyy-MM-dd") + "')";
              

                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    numerosAV = AutorisationVoyage.NumerosAV;
                this.serviceConnectBase.closeConnection();
               
            }
            #endregion

            return numerosAV;
        }

        bool IntfAutorisationVoyage.deleteAutorisationVoyage(crlAutorisationVoyage AutorisationVoyage)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (AutorisationVoyage != null)
            {
                if (AutorisationVoyage.NumerosAV != "")
                {
                    this.strCommande = "DELETE FROM `autorisationvoyage` WHERE (`numerosAV` = '" + AutorisationVoyage.NumerosAV+ "')";
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

        bool IntfAutorisationVoyage.deleteAutorisationVoyage(string numerosAV)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
           
            if (numerosAV != "")
            {
                this.strCommande = "DELETE FROM `autorisationvoyage` WHERE (`numerosAV` = '" + numerosAV + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete == 1)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }
           
            #endregion

            return isDelete;
        }

        bool IntfAutorisationVoyage.updateAutorisationVoyage(crlAutorisationVoyage AutorisationVoyage)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            #endregion

            #region implementation
            if (AutorisationVoyage!= null)
            {
                if (AutorisationVoyage.NumerosAV != "")
                {
                    this.strCommande = "UPDATE `autorisationvoyage` SET ";
                    this.strCommande += " `idVerification`='" + AutorisationVoyage.IdVerification + "',";
                    this.strCommande += " `matriculeAgent`='" + AutorisationVoyage.MatriculeAgent + "',";
                    this.strCommande += " `datePrevueDepart`='" + AutorisationVoyage.DatePrevueDepart.ToString("yyyy-MM-dd") +"'";
                    this.strCommande += " WHERE (`numerosAV`='" + AutorisationVoyage.NumerosAV + "')";

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

        int IntfAutorisationVoyage.isAutorisationVoyage(crlAutorisationVoyage AutorisationVoyage)
        {
            throw new NotImplementedException();
        }

        crlAutorisationVoyage IntfAutorisationVoyage.selectAutorisationVoyage(string numerosAV)
        {
            #region declaration
            IntfDalAgent serviceAgent = new ImplDalAgent();
            IntfDalVerification serviceVerification = new ImplDalVerification();

            crlAutorisationVoyage AutorisationVoyage = null; 
            #endregion

            #region implementation
            if (numerosAV != "") 
            {
                this.strCommande = "SELECT * FROM `autorisationvoyage` WHERE (`numerosAV`='" + numerosAV + "')";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null) 
                {
                    if (reader.HasRows) 
                    {
                        if (reader.Read()) 
                        {
                            AutorisationVoyage = new crlAutorisationVoyage();
                            AutorisationVoyage.NumerosAV = reader["numerosAV"].ToString();
                            AutorisationVoyage.MatriculeAgent = reader["matriculeAgent"].ToString();
                            AutorisationVoyage.IdVerification = reader["idVerification"].ToString();
                            AutorisationVoyage.DatePrevueDepart = Convert.ToDateTime(reader["datePrevueDepart"].ToString());
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (AutorisationVoyage != null) 
                {
                    AutorisationVoyage.Verification = serviceVerification.selectVerification(AutorisationVoyage.IdVerification);
                    AutorisationVoyage.Agent = serviceAgent.selectAgent(AutorisationVoyage.MatriculeAgent);
                }
            }
            #endregion

            return AutorisationVoyage;
        }

        string IntfAutorisationVoyage.getNumerosAV(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numerosAV = "00001";
            string[] tempNumerosAV = null;
            string strDate = "AV" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT autorisationvoyage.numerosAV AS maxNum FROM autorisationvoyage";
            this.strCommande += " WHERE autorisationvoyage.numerosAV LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumerosAV = reader["maxNum"].ToString().ToString().Split('/');
                        numerosAV = tempNumerosAV[tempNumerosAV.Length - 1];
                    }
                    numTemp = double.Parse(numerosAV) + 1;
                    if (numTemp < 10)
                        numerosAV = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numerosAV = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numerosAV = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numerosAV = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numerosAV = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numerosAV = strDate + "/" + sigleAgence + "/" + numerosAV;
            #endregion

            return numerosAV;
        }

        void IntfAutorisationVoyage.loadDdlTri(DropDownList ddlTri)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region insert to grid
        void IntfAutorisationVoyage.insertToGridAutorisationSansFicheBord(GridView gridView, string param, string paramLike, string valueLike, string numAgence)
        {
            #region declaration
            IntfAutorisationVoyage serviceAutorisationVoyage = new ImplAutorisationVoyage();
            #endregion

            #region implementation

            this.strCommande = "SELECT autorisationvoyage.numerosAV, autorisationvoyage.datePrevueDepart,";
            this.strCommande += " itineraire.numVilleItineraireDebut, itineraire.numVilleItineraireFin, vehicule.matriculeVehicule,";
            this.strCommande += " vehicule.marqueVehicule, vehicule.couleurVehicule,";
            this.strCommande += " chauffeur.nomChauffeur, chauffeur.prenomChauffeur FROM autorisationvoyage";
            this.strCommande += " Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
            this.strCommande += " Inner Join itineraire ON itineraire.idItineraire = verification.idItineraire";
            this.strCommande += " Inner Join licence ON licence.numLicence = verification.numLicence";
            this.strCommande += " Inner Join vehicule ON vehicule.numVehicule = licence.numVehicule";
            this.strCommande += " Inner Join chauffeur ON chauffeur.idChauffeur = verification.idChauffeur";
            this.strCommande += " Inner Join agent ON agent.matriculeAgent = autorisationvoyage.matriculeAgent";
            this.strCommande += " Left Join fichebord ON fichebord.numerosAV = autorisationvoyage.numerosAV";
            this.strCommande += " WHERE agent.numAgence =  '" + numAgence + "' AND";
            this.strCommande += " fichebord.numerosAV IS NULL  AND (" + paramLike + " LIKE  '%" + valueLike + "%')";
            this.strCommande += " GROUP BY autorisationvoyage.numerosAV ORDER BY " + param + " DESC";


            gridView.DataSource = serviceAutorisationVoyage.getDataTableAutorisationSansFicheBord(this.strCommande);
            gridView.DataBind();
            #endregion
        }
        DataTable IntfAutorisationVoyage.getDataTableAutorisationSansFicheBord(string strRqst)
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
            dataTable.Columns.Add("numerosAV", typeof(string));
            dataTable.Columns.Add("numImmatricule", typeof(string));
            dataTable.Columns.Add("chauffeur", typeof(string));
            dataTable.Columns.Add("datePrevueDepart", typeof(DateTime));
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

                        dr["numerosAV"] = reader["numerosAV"].ToString();
                        dr["numImmatricule"] = reader["matriculeVehicule"].ToString() + " " + reader["marqueVehicule"].ToString() + " " + reader["couleurVehicule"].ToString();
                        dr["chauffeur"] = reader["nomChauffeur"].ToString() + " " + reader["prenomChauffeur"].ToString();
                        dr["datePrevueDepart"] = Convert.ToDateTime(reader["datePrevueDepart"].ToString());

                      
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
        void IntfAutorisationVoyage.insertToGridAutorisationSansFicheBordASC(GridView gridView, string param, string paramLike, string valueLike, string numAgence)
        {
            #region declaration
            IntfAutorisationVoyage serviceAutorisationVoyage = new ImplAutorisationVoyage();
            #endregion

            #region implementation

            this.strCommande = "SELECT autorisationvoyage.numerosAV, autorisationvoyage.datePrevueDepart,";
            this.strCommande += " itineraire.numVilleItineraireDebut, itineraire.numVilleItineraireFin, vehicule.matriculeVehicule, ";
            this.strCommande += " vehicule.marqueVehicule, vehicule.couleurVehicule,";
            this.strCommande += " chauffeur.nomChauffeur, chauffeur.prenomChauffeur FROM autorisationvoyage";
            this.strCommande += " Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
            this.strCommande += " Inner Join itineraire ON itineraire.idItineraire = verification.idItineraire";
            this.strCommande += " Inner Join licence ON licence.numLicence = verification.numLicence";
            this.strCommande += " Inner Join vehicule ON vehicule.numVehicule = licence.numVehicule";
            this.strCommande += " Inner Join chauffeur ON chauffeur.idChauffeur = verification.idChauffeur";
            this.strCommande += " Inner Join agent ON agent.matriculeAgent = autorisationvoyage.matriculeAgent";
            this.strCommande += " Left Join fichebord ON fichebord.numerosAV = autorisationvoyage.numerosAV";
            this.strCommande += " WHERE agent.numAgence =  '" + numAgence + "' AND";
            this.strCommande += " fichebord.numerosAV IS NULL  AND (" + paramLike + " LIKE  '%" + valueLike + "%')";
            this.strCommande += " GROUP BY autorisationvoyage.numerosAV ORDER BY " + param + " ASC";


            gridView.DataSource = serviceAutorisationVoyage.getDataTableAutorisationSansFicheBord(this.strCommande);
            gridView.DataBind();
            #endregion
        }
        #endregion

    }
}