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
    /// Implementation du service voyage us
    /// </summary>
    public class ImplDalUSVoyage : IntfDalUSVoyage
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalUSVoyage(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalUSVoyage()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion


        #region IntfDalUSVoyage Members

        string IntfDalUSVoyage.insertUSVoyage(crlUSVoyage voyage, string sigleAgence)
        {
            #region declaration
            string numVoyage = "";
            IntfDalUSVoyage serviceUSVoyage = new ImplDalUSVoyage();
            string matriculeAgentArrive = "NULL";
            string matriculeAgentControleur = "NULL";
            string numFacture = "NULL";
            string numAppareil = "NULL";
            int nbInsert = 0;
            #endregion

            #region implementation
            if (voyage != null) 
            {
                if (voyage.MatriculeAgentArrive != "") 
                {
                    matriculeAgentArrive = "'" + voyage.MatriculeAgentArrive + "'";
                }
                if (voyage.MatriculeAgentControleur != "") 
                {
                    matriculeAgentControleur = "'" + voyage.MatriculeAgentControleur + "'";
                }
                if (voyage.NumFacture != "")
                {
                    numFacture = "'" + voyage.NumFacture + "'";
                }
                if (voyage.NumAppareil != "")
                {
                    numAppareil = "'" + voyage.NumAppareil + "'";
                }
                voyage.NumVoyage = serviceUSVoyage.getNumUSVoyage(sigleAgence);

                this.strCommande = "INSERT INTO `usvoyage` (`numVoyage`,`dateHeureDepart`,`dateHeureArrive`,";
                this.strCommande += " `numLicence`,`matriculeAgentDepart`,`matriculeAgentArrive`,`matriculeAgentChauffeur`,";
                this.strCommande += " `matriculeAgentReceveur`,`matriculeAgentControleur`,`numLigne`,`numFacture`,`numAppareil`)";
                this.strCommande += " VALUES ('" + voyage.NumVoyage + "','" + voyage.DateHeureDepart.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                this.strCommande += " '" + voyage.DateHeureArrive.ToString("yyyy-MM-dd HH:mm:ss") + "','" + voyage.NumLicence + "',";
                this.strCommande += " '" + voyage.MatriculeAgentDepart + "'," + matriculeAgentArrive + ",";
                this.strCommande += " '" + voyage.MatriculeAgentChauffeur + "','" + voyage.MatriculeAgentReceveur + "',";
                this.strCommande += " " + matriculeAgentControleur + ",'" + voyage.NumLigne + "'," + numFacture + "," + numAppareil + ")";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1) 
                {
                    numVoyage = voyage.NumVoyage;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numVoyage;
        }

        bool IntfDalUSVoyage.updateUSVoyage(crlUSVoyage voyage)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            string matriculeAgentArrive = "NULL";
            string matriculeAgentControleur = "NULL";
            string numFacture = "NULL";
            string numAppareil = "NULL";
            #endregion

            #region implementation
            if (voyage != null)
            {
                if (voyage.MatriculeAgentArrive != "")
                {
                    matriculeAgentArrive = "'" + voyage.MatriculeAgentArrive + "'";
                }
                if (voyage.MatriculeAgentControleur != "")
                {
                    matriculeAgentControleur = "'" + voyage.MatriculeAgentControleur + "'";
                }
                if (voyage.NumFacture != "")
                {
                    numFacture = "'" + voyage.NumFacture + "'";
                }
                if (voyage.NumAppareil != "")
                {
                    numAppareil = "'" + voyage.NumAppareil + "'";
                }

                this.strCommande = "UPDATE `usvoyage` SET `dateHeureDepart`='" + voyage.DateHeureDepart.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                this.strCommande += " `dateHeureArrive`='" + voyage.DateHeureArrive.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                this.strCommande += " `numLicence`='" + voyage.NumLicence + "',`matriculeAgentDepart`='" + voyage.MatriculeAgentDepart + "',";
                this.strCommande += " `matriculeAgentArrive`=" + matriculeAgentArrive + ",";
                this.strCommande += " `matriculeAgentChauffeur`='" + voyage.MatriculeAgentChauffeur + "',";
                this.strCommande += " `matriculeAgentReceveur`='" + voyage.MatriculeAgentReceveur + "',";
                this.strCommande += " `matriculeAgentControleur`=" + matriculeAgentControleur + ",";
                this.strCommande += " `numLigne`='" + voyage.NumLigne + "',";
                this.strCommande += " `numAppareil`=" + numAppareil + ",";
                this.strCommande += " `numFacture`=" + numFacture;
                this.strCommande += " WHERE `numVoyage`='" + voyage.NumVoyage + "'";

                this.serviceConnectBase.openConnection();
                nbUpdate = this.serviceConnectBase.requete(this.strCommande);
                if (nbUpdate == 1) 
                {
                    isUpdate = true;
                }
                this.serviceConnectBase.closeConnection();
                
            }
            #endregion

            return isUpdate;
        }

        crlUSVoyage IntfDalUSVoyage.selectUSVoyage(string numVoyage)
        {
            #region declaration
            crlUSVoyage voyage = null;
            IntfDalAgent serviceAgent = new ImplDalAgent();
            #endregion

            #region implementation
            if (numVoyage != "") 
            {
                this.strCommande = "SELECT * FROM `usvoyage` WHERE `numVoyage`='" + numVoyage + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            voyage = new crlUSVoyage();
                            try
                            {
                                voyage.DateHeureArrive = Convert.ToDateTime(this.reader["dateHeureArrive"].ToString());
                            }
                            catch (Exception) { }
                            try
                            {
                                voyage.DateHeureDepart = Convert.ToDateTime(this.reader["dateHeureDepart"].ToString());
                            }
                            catch (Exception) { }
                            voyage.MatriculeAgentArrive = this.reader["matriculeAgentArrive"].ToString();
                            voyage.MatriculeAgentChauffeur = this.reader["matriculeAgentChauffeur"].ToString();
                            voyage.MatriculeAgentControleur = this.reader["matriculeAgentControleur"].ToString();
                            voyage.MatriculeAgentDepart = this.reader["matriculeAgentDepart"].ToString();
                            voyage.MatriculeAgentReceveur = this.reader["matriculeAgentReceveur"].ToString();
                            voyage.NumLicence = this.reader["numLicence"].ToString();
                            voyage.NumVoyage = this.reader["numVoyage"].ToString();
                            voyage.NumLigne = this.reader["numLigne"].ToString();
                            voyage.NumFacture = this.reader["numFacture"].ToString();
                            voyage.NumAppareil = this.reader["numAppareil"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (voyage != null) 
                {
                    if (voyage.MatriculeAgentArrive != "") 
                    {
                        voyage.agentArrive = serviceAgent.selectAgent(voyage.MatriculeAgentArrive);
                    }
                    if (voyage.MatriculeAgentChauffeur != "") 
                    {
                        voyage.agentChauffeur = serviceAgent.selectAgent(voyage.MatriculeAgentChauffeur);
                    }
                    if (voyage.MatriculeAgentControleur != "") 
                    {
                        voyage.agentControleur = serviceAgent.selectAgent(voyage.MatriculeAgentControleur);
                    }
                    if (voyage.MatriculeAgentDepart != "") 
                    {
                        voyage.agentDepart = serviceAgent.selectAgent(voyage.MatriculeAgentDepart);
                    }
                    if (voyage.MatriculeAgentReceveur != "") 
                    {
                        voyage.agentReceveur = serviceAgent.selectAgent(voyage.MatriculeAgentReceveur);
                    }
                }
            }
            #endregion

            return voyage;
        }

        string IntfDalUSVoyage.getNumUSVoyage(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numVoyage = "000001";
            string[] tempNumVoyage = null;
            string strDate = sigleAgence + DateTime.Now.ToString("yyMMdd");
            #endregion

            #region implementation
            this.strCommande = "SELECT usvoyage.numVoyage AS maxNum FROM usvoyage";
            this.strCommande += " WHERE usvoyage.numVoyage LIKE '%" + strDate + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumVoyage = reader["maxNum"].ToString().ToString().Split('/');
                        numVoyage = tempNumVoyage[tempNumVoyage.Length - 1];
                    }
                    numTemp = double.Parse(numVoyage) + 1;

                    numVoyage = numTemp.ToString("000000");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numVoyage = strDate + "/" + numVoyage;
            #endregion

            return numVoyage;
        }

        double IntfDalUSVoyage.montantTotalVoyage(string numVoyage)
        {
            #region declaration
            double montantTotal = 0;
            #endregion

            #region implementation
            if (numVoyage != "")
            {
                this.strCommande = "SELECT Sum(usbillet.montant) AS montantTotal FROM usbillet";
                this.strCommande += " Inner Join usassocvoyagebillet ON usassocvoyagebillet.numBillet = usbillet.numBillet";
                this.strCommande += " WHERE usassocvoyagebillet.numVoyage = '" + numVoyage + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            montantTotal = double.Parse(this.reader["montantTotal"].ToString());
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return montantTotal;
        }




        bool IntfDalUSVoyage.isChauffeurVoyage(string matriculeAgent, string matriculeAgentN)
        {
            #region declaration
            bool isVoyage = false;
            #endregion

            #region implementation
            if (matriculeAgent != "")
            {
                this.strCommande = "SELECT usvoyage.numVoyage FROM usvoyage";
                this.strCommande += " WHERE usvoyage.matriculeAgentArrive IS NULL  AND";
                this.strCommande += " usvoyage.matriculeAgentChauffeur = '" + matriculeAgent + "' AND";
                this.strCommande += " usvoyage.matriculeAgentChauffeur <> '" + matriculeAgentN + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            isVoyage = true;
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isVoyage;
        }

        bool IntfDalUSVoyage.isReceveurVoyage(string matriculeAgent, string matriculeAgentN)
        {
            #region declaration
            bool isVoyage = false;
            #endregion

            #region implementation
            if (matriculeAgent != "")
            {
                this.strCommande = "SELECT usvoyage.numVoyage FROM usvoyage";
                this.strCommande += " WHERE usvoyage.matriculeAgentArrive IS NULL  AND";
                this.strCommande += " usvoyage.matriculeAgentReceveur = '" + matriculeAgent + "' AND";
                this.strCommande += " usvoyage.matriculeAgentReceveur <> '" + matriculeAgentN + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            isVoyage = true;
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isVoyage;
        }

        bool IntfDalUSVoyage.isControleVoyage(string matriculeAgent, string matriculeAgentN)
        {
            #region declaration
            bool isVoyage = false;
            #endregion

            #region implementation
            if (matriculeAgent != "")
            {
                this.strCommande = "SELECT usvoyage.numVoyage FROM usvoyage";
                this.strCommande += " WHERE usvoyage.matriculeAgentArrive IS NULL  AND";
                this.strCommande += " usvoyage.matriculeAgentControleur = '" + matriculeAgent + "' AND";
                this.strCommande += " usvoyage.matriculeAgentControleur <> '" + matriculeAgentN + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            isVoyage = true;
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isVoyage;
        }

        bool IntfDalUSVoyage.isVehiculeVoyage(string numLicence, string numLicenceN)
        {
            #region declaration
            bool isVoyage = false;
            #endregion

            #region implementation
            if (numLicence != "")
            {
                this.strCommande = "SELECT usvoyage.numVoyage FROM usvoyage";
                this.strCommande += " WHERE usvoyage.matriculeAgentArrive IS NULL  AND";
                this.strCommande += " usvoyage.numLicence = '" + numLicence + "' AND";
                this.strCommande += " usvoyage.numLicence <> '" + numLicenceN + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            isVoyage = true;
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isVoyage;
        }

        bool IntfDalUSVoyage.isMaterielVoyage(string numAppareil, string numAppareilN)
        {
            #region declaration
            bool isVoyage = false;
            #endregion

            #region implementation
            if (numAppareil != "")
            {
                this.strCommande = "SELECT usvoyage.numVoyage FROM usvoyage";
                this.strCommande += " WHERE usvoyage.matriculeAgentArrive IS NULL  AND";
                this.strCommande += " usvoyage.numAppareil = '" + numAppareil + "' AND";
                this.strCommande += " usvoyage.numAppareil <> '" + numAppareilN + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            isVoyage = true;
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isVoyage;
        }
        #endregion

        void IntfDalUSVoyage.insertToGridUSVoyageNonArrive(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalUSVoyage serviceUSVoyage = new ImplDalUSVoyage();
            #endregion

            #region implementation

            this.strCommande = "SELECT usvoyage.dateHeureDepart, usligne.nomLigne,";
            this.strCommande += " vehicule.marqueVehicule, vehicule.matriculeVehicule,";
            this.strCommande += " vehicule.couleurVehicule, usvoyage.matriculeAgentDepart,";
            this.strCommande += " usvoyage.matriculeAgentChauffeur, usvoyage.matriculeAgentReceveur,";
            this.strCommande += " usvoyage.matriculeAgentControleur, usvoyage.numVoyage";
            this.strCommande += " FROM usvoyage";
            this.strCommande += " Inner Join usligne ON usligne.numLigne = usvoyage.numLigne";
            this.strCommande += " Inner Join licence ON licence.numLicence = usvoyage.numLicence";
            this.strCommande += " Inner Join vehicule ON vehicule.numVehicule = licence.numVehicule";
            this.strCommande += " Left Join agent ON agent.matriculeAgent = usvoyage.matriculeAgentChauffeur";
            this.strCommande += " OR agent.matriculeAgent = usvoyage.matriculeAgentReceveur";
            this.strCommande += " OR agent.matriculeAgent = usvoyage.matriculeAgentControleur";
            this.strCommande += " WHERE " + paramLike + " LIKE  '%" + valueLike + "%' AND";
            this.strCommande += " usvoyage.matriculeAgentArrive IS NULL";
            this.strCommande += " GROUP BY usvoyage.numVoyage";
            this.strCommande += " ORDER BY " + param;


            gridView.DataSource = serviceUSVoyage.getDataTableUSVoyageNonArrive(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalUSVoyage.getDataTableUSVoyageNonArrive(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();

            IntfDalAgent serviceAgent = new ImplDalAgent();
            crlAgent agentChauffeur = null;
            crlAgent agentReceveur = null;
            crlAgent agentControleur = null;
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numVoyage", typeof(string));
            dataTable.Columns.Add("nomLigne", typeof(string));
            dataTable.Columns.Add("vehicule", typeof(string));
            dataTable.Columns.Add("chauffeur", typeof(string));
            dataTable.Columns.Add("receveur", typeof(string));
            dataTable.Columns.Add("controleur", typeof(string));

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

                        dr["numVoyage"] = reader["numVoyage"].ToString();
                        dr["nomLigne"] = reader["nomLigne"].ToString();
                        dr["vehicule"] = reader["matriculeVehicule"].ToString() + " " + reader["marqueVehicule"].ToString() + " " + reader["couleurVehicule"].ToString();

                        agentChauffeur = serviceAgent.selectAgent(reader["matriculeAgentChauffeur"].ToString());
                        agentReceveur = serviceAgent.selectAgent(reader["matriculeAgentReceveur"].ToString());
                        agentControleur = serviceAgent.selectAgent(reader["matriculeAgentControleur"].ToString());

                        if (agentChauffeur != null)
                        {
                            dr["chauffeur"] = agentChauffeur.nomAgent + " " + agentChauffeur.prenomAgent;
                        }
                        else
                        {
                            dr["chauffeur"] = "";
                        }
                        if (agentReceveur != null)
                        {
                            dr["receveur"] = agentReceveur.nomAgent + " " + agentReceveur.prenomAgent;
                        }
                        else
                        {
                            dr["receveur"] = "";
                        }
                        if (agentControleur != null)
                        {
                            dr["controleur"] = agentControleur.nomAgent + " " + agentControleur.prenomAgent;
                        }
                        else
                        {
                            dr["controleur"] = "";
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


        
    }
}
