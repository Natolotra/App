using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.dal.intf;
using MySql.Data.MySqlClient;
using arch.crl;
using System.Data;
using System.Web.UI.WebControls;

namespace arch.dal.impl
{
    /// <summary>
    /// Implementation du service observation agent
    /// </summary>
    public class ImplDalObservationAgent : IntfDalObservationAgent
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalObservationAgent()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalObservationAgent(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion


        #region methode
        string IntfDalObservationAgent.insertObservationAgent(crlObservationAgent observation, string sigleAgence)
        {
            #region declaration
            string numObservation = "";
            IntfDalObservationAgent serviceObservationAgent = new ImplDalObservationAgent();
            int nbInsert = 0;
            #endregion

            #region implementation
            if (observation != null && sigleAgence != "")
            {
                observation.NumObservation = serviceObservationAgent.getNumObservation(sigleAgence);

                this.strCommande = "INSERT INTO `observationagent` (`numObservation`,`matriculeAgent`,`textObesvation`,";
                this.strCommande += "`dateObservation`,`isListeNoire`) VALUES ('" + observation.NumObservation + "',";
                this.strCommande += "'" + observation.MatriculeAgent + "','" + observation.TextObesvation + "',";
                this.strCommande += "'" + observation.DateObservation.ToString("yyyy-MM-dd") + "','" + observation.IsListeNoire + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    numObservation = observation.NumObservation;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numObservation;
        }

        bool IntfDalObservationAgent.updateObservationAgent(crlObservationAgent observation)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            #endregion

            #region implementation
            if (observation != null)
            {
                this.strCommande = "UPDATE `observationagent` SET `matriculeAgent`='" + observation.MatriculeAgent + "',";
                this.strCommande += "`textObesvation`='" + observation.TextObesvation + "',";
                this.strCommande += "`dateObservation`='" + observation.DateObservation.ToString("yyyy-MM-dd") + "',";
                this.strCommande += "`isListeNoire`='" + observation.IsListeNoire.ToString("0") + "'";
                this.strCommande += " WHERE `numObservation`='" + observation.NumObservation + "'";

                this.serviceConnectBase.openConnection();
                nbUpdate = this.serviceConnectBase.requete(this.strCommande);
                if (nbUpdate == 1)
                    isUpdate = true;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isUpdate;
        }

        crlObservationAgent IntfDalObservationAgent.selectObservationAgent(string numObservation)
        {
            #region declaration
            crlObservationAgent observationChauffeur = null;
            #endregion

            #region implementation
            if (numObservation != "")
            {
                this.strCommande = "SELECT * FROM `observationagent` WHERE `numObservation`='" + numObservation + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            observationChauffeur = new crlObservationAgent();
                            try
                            {
                                observationChauffeur.DateObservation = Convert.ToDateTime(this.reader["dateObservation"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            observationChauffeur.MatriculeAgent = this.reader["matriculeAgent"].ToString();
                            try
                            {
                                observationChauffeur.IsListeNoire = int.Parse(this.reader["isListeNoire"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            observationChauffeur.NumObservation = this.reader["numObservation"].ToString();
                            observationChauffeur.TextObesvation = this.reader["textObesvation"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return observationChauffeur;
        }

        string IntfDalObservationAgent.getNumObservation(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numObservation = "00001";
            string[] tempNumObservation = null;
            string strDate = sigleAgence + DateTime.Now.ToString("yyMMdd");
            #endregion

            #region implementation
            this.strCommande = "SELECT observationagent.numObservation AS maxNum FROM observationagent";
            this.strCommande += " WHERE observationagent.numObservation LIKE '%" + strDate + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumObservation = reader["maxNum"].ToString().ToString().Split('/');
                        numObservation = tempNumObservation[tempNumObservation.Length - 1];
                    }
                    numTemp = double.Parse(numObservation) + 1;

                    numObservation = numTemp.ToString("00000");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numObservation = strDate + "/" + numObservation;
            #endregion

            return numObservation;
        }

        int IntfDalObservationAgent.getObservationAgent(string matriculeAgent)
        {
            #region declaration
            int isListeNoire = 0;
            #endregion

            #region implementation
            if (matriculeAgent != "")
            {
                this.strCommande = "SELECT observationagent.isListeNoire FROM observationagent";
                this.strCommande += " WHERE observationagent.matriculeAgent = '" + matriculeAgent + "' AND";
                this.strCommande += " observationagent.isListeNoire > '0'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            try
                            {
                                if (int.Parse(this.reader["isListeNoire"].ToString()) > isListeNoire)
                                    isListeNoire = int.Parse(this.reader["isListeNoire"].ToString());
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

            return isListeNoire;
        }

        string IntfDalObservationAgent.getObservationAgent(string matriculeAgent, int isListeNoire)
        {
            #region declaration
            string observationDateHtml = "";
            string strObservation = "";
            DateTime dateObservation;
            #endregion

            #region implementation
            if (matriculeAgent != "")
            {
                this.strCommande = "SELECT observationagent.textObesvation, observationagent.dateObservation";
                this.strCommande += " FROM observationagent WHERE observationagent.matriculeAgent = '" + matriculeAgent + "' AND";
                this.strCommande += " observationagent.isListeNoire = '" + isListeNoire + "'";
                this.strCommande += " ORDER BY observationagent.dateObservation DESC";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            strObservation = this.reader["textObesvation"].ToString();
                            try
                            {
                                dateObservation = Convert.ToDateTime(this.reader["dateObservation"].ToString());
                            }
                            catch (Exception)
                            {
                                dateObservation = DateTime.Now;
                            }

                            observationDateHtml += "------------------\n";
                            observationDateHtml += dateObservation.ToString("dd MMMM yyyy") + "\n\n";
                            observationDateHtml += strObservation + "\n";
                            observationDateHtml += "------------------\n";
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return observationDateHtml;
        }
        #endregion

        #region insert to grid
        void IntfDalObservationAgent.insertToGridObservationAgent(GridView gridView, string param, string paramLike, string valueLike, string matriculeAgent)
        {
            #region declaration
            IntfDalObservationAgent serviceObservationAgent = new ImplDalObservationAgent();
            #endregion

            #region implementation
            this.strCommande = "SELECT observationagent.numObservation, observationagent.matriculeAgent,";
            this.strCommande += " observationagent.textObesvation, observationagent.dateObservation,";
            this.strCommande += " observationagent.isListeNoire, agent.matriculeAgent, agent.typeAgent,";
            this.strCommande += " agent.numAgence, agent.nomAgent, agent.prenomAgent,";
            this.strCommande += " agent.dateNaissanceAgent, agent.lieuNaissanceAgent, agent.cinAgent,";
            this.strCommande += " agent.adresseAgent, agent.telephoneAgent, agent.telephoneMobileAgent,";
            this.strCommande += " agent.imageAgent, agent.situationFamilialeAgent FROM observationagent";
            this.strCommande += " Inner Join agent ON agent.matriculeAgent = observationagent.matriculeAgent";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%' AND";
            this.strCommande += " observationagent.matriculeAgent LIKE '%" + matriculeAgent + "%'";
            this.strCommande += " ORDER BY " + param + " ASC";

            gridView.DataSource = serviceObservationAgent.getDataTableObservationAgent(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalObservationAgent.getDataTableObservationAgent(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numObservation", typeof(string));
            dataTable.Columns.Add("agent", typeof(string));
            dataTable.Columns.Add("textObesvation", typeof(string));
            dataTable.Columns.Add("dateObservation", typeof(DateTime));
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

                        dr["numObservation"] = reader["numObservation"].ToString();
                        dr["agent"] = reader["nomAgent"].ToString() + " " + reader["prenomAgent"].ToString();
                        dr["textObesvation"] = reader["textObesvation"].ToString();
                        try
                        {
                            dr["dateObservation"] = Convert.ToDateTime(reader["dateObservation"].ToString());
                        }
                        catch (Exception)
                        {
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