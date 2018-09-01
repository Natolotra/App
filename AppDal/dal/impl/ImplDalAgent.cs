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

namespace arch.dal.impl
{
    /// <summary>
    /// Implementaion du service agent
    /// </summary>
    public class ImplDalAgent : IntfDalAgent
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalAgent(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalAgent()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion

        #region IntfDalAgent Members

        string IntfDalAgent.insertAgent(crlAgent Agent)
        {
            #region declaration
            IntfDalAgent serviceAgent = new ImplDalAgent();
            int nombreInsertion = 0;
            string matriculeAgent = "";
            #endregion

            #region implementation
            if (Agent != null)
            {
                if (Agent.agence != null)
                {
                    Agent.matriculeAgent = serviceAgent.getMatriculeAgent(Agent.agence.SigleAgence);
                    this.strCommande = "INSERT INTO `agent` (`matriculeAgent`,`numAgence`,`typeAgent`,`nomAgent`,`prenomAgent`,";
                    this.strCommande += "`dateNaissanceAgent`, `lieuNaissanceAgent`,`loginAgent`,`motDePasseAgent`,`cinAgent`,`adresseAgent`,";
                    this.strCommande += "`telephoneAgent`,`telephoneMobileAgent`,`imageAgent`,`situationFamilialeAgent`) VALUES ('" + Agent.matriculeAgent + "','" + Agent.numAgence + "',";
                    this.strCommande += "'" + Agent.typeAgent + "', '" + Agent.nomAgent + "', '" + Agent.prenomAgent + "', '" + Agent.dateNaissanceAgent.ToString("yyyy-MM-dd") + "',";
                    this.strCommande += "'" + Agent.lieuNaissanceAgent + "', '" + Agent.loginAgent + "', '" + Agent.motDePasseAgent + "', '" + Agent.cinAgent + "',";
                    this.strCommande += "'" + Agent.adresseAgent + "', '" + Agent.telephoneAgent + "', '" + Agent.telephoneMobileAgent + "','" + Agent.ImageAgent + "',";
                    this.strCommande += "'" + Agent.SituationFamilialeAgent + "')";

                    this.serviceConnectBase.openConnection();
                    nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                    if (nombreInsertion == 1)
                        matriculeAgent = Agent.matriculeAgent;
                    this.serviceConnectBase.closeConnection();
                }
            }
            #endregion

            return matriculeAgent;
        }

        bool IntfDalAgent.deleteAgent(crlAgent Agent)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (Agent != null)
            {
                if (Agent.matriculeAgent != "")
                {
                    this.strCommande = "DELETE FROM `agent` WHERE (`matriculeAgent` = '" + Agent.matriculeAgent + "')";
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

        bool IntfDalAgent.deleteAgent(string matriculeAgent)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
           
            if (matriculeAgent != "")
            {
                this.strCommande = "DELETE FROM `agent` WHERE (`matriculeAgent` = '" + matriculeAgent + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete == 1)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }
       
            #endregion

            return isDelete;
        }

        bool IntfDalAgent.updateAgent(crlAgent Agent)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            #endregion

            #region implementation
            if (Agent != null)
            {
                if (Agent.matriculeAgent != "")
                {
                    this.strCommande = "UPDATE `agent` SET `adresseAgent`='" + Agent.adresseAgent + "', `cinAgent`='" + Agent.cinAgent + "',";
                    this.strCommande += " `dateNaissanceAgent`='" + Agent.dateNaissanceAgent.ToString("yyyy-MM-dd") + "', `lieuNaissanceAgent`='" + Agent.lieuNaissanceAgent + "',";
                    this.strCommande += " `loginAgent`='" + Agent.loginAgent + "', `motDePasseAgent`='" + Agent.motDePasseAgent + "',";
                    this.strCommande += " `nomAgent`='" + Agent.nomAgent + "', `numAgence`='" + Agent.numAgence + "', `prenomAgent`='" + Agent.prenomAgent + "',";
                    this.strCommande += " `telephoneAgent`='" + Agent.telephoneAgent + "', `telephoneMobileAgent`='" + Agent.telephoneMobileAgent + "', `typeAgent`='" + Agent.typeAgent + "',";
                    this.strCommande += " `imageAgent`='" + Agent.ImageAgent + "', `situationFamilialeAgent`='" + Agent.SituationFamilialeAgent + "'";
                    this.strCommande += " WHERE (`matriculeAgent`='" + Agent.matriculeAgent + "')";

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

        int IntfDalAgent.isAgent(crlAgent Agent)
        {
            #region declaration
            int isAgent = 0;
            #endregion

            #region implementation
            if (Agent != null)
            {
                if (Agent.loginAgent != "")
                {
                    this.strCommande = "SELECT * FROM `agent` WHERE (`matriculeAgent`<>'" + Agent.matriculeAgent + "') AND";
                    this.strCommande += " (`cinAgent`='" + Agent.cinAgent + "' OR `loginAgent`='" + Agent.loginAgent + "')";
                }
                else
                {
                    this.strCommande = "SELECT * FROM `agent` WHERE (`matriculeAgent`<>'" + Agent.matriculeAgent + "') AND";
                    this.strCommande += " (`cinAgent`='" + Agent.cinAgent + "')";
                }
                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (Agent.loginAgent != "")
                            {
                                if (Agent.cinAgent.Trim().ToLower().Equals(reader["cinAgent"].ToString().Trim().ToLower()))
                                {
                                    isAgent = 1;
                                    break;
                                }
                                else if (Agent.loginAgent.Trim().ToLower().Equals(reader["loginAgent"].ToString().Trim().ToLower()))
                                {
                                    isAgent = 2;
                                    break;
                                }
                            }
                            else
                            {
                                if (Agent.cinAgent.Trim().ToLower().Equals(reader["cinAgent"].ToString().Trim().ToLower()))
                                {
                                    isAgent = 1;
                                    break;
                                }
                            }
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isAgent;
        }

        crlAgent IntfDalAgent.selectAgent(string matriculeAgent)
        {
            #region declaration
            crlAgent agent = null;
            crlTypeAgent typeAgence = null;
            crlProvince province = null;
            IntfDalAgence serviceAgence = new ImplDalAgence();
            IntfDalSessionCaisse serviceSessionCaisse = new ImplDalSessionCaisse();
            #endregion

            #region implementation
            if (matriculeAgent != "")
            {
                this.strCommande = "SELECT agent.matriculeAgent, agent.numAgence, agent.typeAgent,";
                this.strCommande +=" agent.nomAgent, agent.prenomAgent, agent.dateNaissanceAgent, agent.lieuNaissanceAgent,";
                this.strCommande +=" agent.loginAgent, agent.motDePasseAgent, agent.cinAgent, agent.adresseAgent,";
                this.strCommande += " agent.telephoneAgent, agent.telephoneMobileAgent, agent.imageAgent, agent.situationFamilialeAgent";
                this.strCommande +=" FROM agent WHERE (`matriculeAgent`='" + matriculeAgent + "')";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        agent = new crlAgent();
                        province = new crlProvince();
                        typeAgence = new crlTypeAgent();
                        
                        reader.Read();
                        agent.adresseAgent = reader["adresseAgent"].ToString();
                        agent.cinAgent = reader["cinAgent"].ToString();
                        try
                        {
                            agent.dateNaissanceAgent = Convert.ToDateTime(reader["dateNaissanceAgent"].ToString());
                        }catch(Exception)
                        {
                        }
                        agent.lieuNaissanceAgent = reader["lieuNaissanceAgent"].ToString();
                        agent.loginAgent = reader["loginAgent"].ToString();
                        agent.matriculeAgent = reader["matriculeAgent"].ToString();
                        agent.motDePasseAgent = reader["motDePasseAgent"].ToString();
                        agent.nomAgent = reader["nomAgent"].ToString();
                        agent.numAgence = reader["numAgence"].ToString();
                        agent.prenomAgent = reader["prenomAgent"].ToString();
                        agent.telephoneAgent = reader["telephoneAgent"].ToString();
                        agent.telephoneMobileAgent = reader["telephoneMobileAgent"].ToString();
                        agent.typeAgent = reader["typeAgent"].ToString();
                        agent.ImageAgent = reader["imageAgent"].ToString();
                        agent.SituationFamilialeAgent = reader["situationFamilialeAgent"].ToString();

                        typeAgence.typeAgent = reader["typeAgent"].ToString();
                        agent.typeAgentObj = typeAgence;

                        

                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (agent != null) 
                {
                    if(agent.numAgence != "")
                    {
                        agent.agence = serviceAgence.selectAgence(agent.numAgence);
                    }
                    if (agent.matriculeAgent != "") 
                    {
                        agent.sessionCaisse = serviceSessionCaisse.getSessionCaisseEncours(agent.matriculeAgent);
                    }
                }
            }
            #endregion

            return agent;
        }

        crlAgent IntfDalAgent.selectAgent(string param, string paramValue)
        {
            #region declaration
            crlAgent agent = null;
            crlTypeAgent typeAgence = null;
            crlProvince province = null;
            IntfDalAgence serviceAgence = new ImplDalAgence();
            IntfDalSessionCaisse serviceSessionCaisse = new ImplDalSessionCaisse();
            #endregion

            #region implementation
            if (param != "" && paramValue != "")
            {
                this.strCommande = "SELECT agent.matriculeAgent, agent.numAgence, agent.typeAgent,";
                this.strCommande += " agent.nomAgent, agent.prenomAgent, agent.dateNaissanceAgent, agent.lieuNaissanceAgent,";
                this.strCommande += " agent.loginAgent, agent.motDePasseAgent, agent.cinAgent, agent.adresseAgent,";
                this.strCommande += " agent.telephoneAgent, agent.telephoneMobileAgent, agent.imageAgent";
                this.strCommande += " FROM agent WHERE (`" + param + "`='" + paramValue + "')";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        agent = new crlAgent();
                        province = new crlProvince();
                        typeAgence = new crlTypeAgent();

                        reader.Read();
                        agent.adresseAgent = reader["adresseAgent"].ToString();
                        agent.cinAgent = reader["cinAgent"].ToString();
                        try
                        {
                            agent.dateNaissanceAgent = Convert.ToDateTime(reader["dateNaissanceAgent"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        agent.lieuNaissanceAgent = reader["lieuNaissanceAgent"].ToString();
                        agent.loginAgent = reader["loginAgent"].ToString();
                        agent.matriculeAgent = reader["matriculeAgent"].ToString();
                        agent.motDePasseAgent = reader["motDePasseAgent"].ToString();
                        agent.nomAgent = reader["nomAgent"].ToString();
                        agent.numAgence = reader["numAgence"].ToString();
                        agent.prenomAgent = reader["prenomAgent"].ToString();
                        agent.telephoneAgent = reader["telephoneAgent"].ToString();
                        agent.telephoneMobileAgent = reader["telephoneMobileAgent"].ToString();
                        agent.typeAgent = reader["typeAgent"].ToString();
                        agent.ImageAgent = reader["imageAgent"].ToString();
                        agent.SituationFamilialeAgent = reader["situationFamilialeAgent"].ToString();

                        typeAgence.typeAgent = reader["typeAgent"].ToString();
                        agent.typeAgentObj = typeAgence;



                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (agent != null)
                {
                    if (agent.numAgence != "")
                    {
                        agent.agence = serviceAgence.selectAgence(agent.numAgence);
                    }
                    if (agent.matriculeAgent != "")
                    {
                        agent.sessionCaisse = serviceSessionCaisse.getSessionCaisseEncours(agent.matriculeAgent);
                    }
                }
            }
            #endregion

            return agent;
        }

        string IntfDalAgent.getMatriculeAgent(string sigle)
        {
            #region declaration
            double numTemp = 0;
            string matriculeAgent = "00001";
            string[] tempMatriculeAgent = null;
            string strDate = "AG" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT agent.matriculeAgent AS maxNum FROM agent";
            this.strCommande += " WHERE agent.matriculeAgent LIKE '%" + sigle + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempMatriculeAgent = reader["maxNum"].ToString().ToString().Split('/');
                        matriculeAgent = tempMatriculeAgent[tempMatriculeAgent.Length - 1];
                    }
                    numTemp = double.Parse(matriculeAgent) + 1;
                    if (numTemp < 10)
                        matriculeAgent = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        matriculeAgent = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        matriculeAgent = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        matriculeAgent = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        matriculeAgent = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            matriculeAgent = strDate + "/" + sigle + "/" + matriculeAgent;
            #endregion

            return matriculeAgent;
        }

        void IntfDalAgent.loadDdlTri(DropDownList ddlTri)
        {
            ddlTri.Items.Clear();
            ddlTri.Items.Add(new ListItem("Matricule", "matriculeAgent"));
            ddlTri.Items.Add(new ListItem("Nom", "nomAgent"));
            ddlTri.Items.Add(new ListItem("Prénom", "prenomAgent"));
            ddlTri.Items.Add(new ListItem("Adresse", "adresseAgent"));
            ddlTri.Items.Add(new ListItem("Téléphone", "telephoneAgent"));
            ddlTri.Items.Add(new ListItem("Mobile", "telephoneMobileAgent"));
        }

        crlAgent IntfDalAgent.login(string login, string motDePasse)
        {
            #region declaration
            crlAgent agent = new crlAgent();

            IntfDalAgence serviceAgence = new ImplDalAgence();
            IntfDalSessionCaisse serviceSessionCaisse = new ImplDalSessionCaisse();
            #endregion

            #region implementation
            if (login != "" && motDePasse != "")
            {
                this.strCommande = "SELECT * FROM `agent` WHERE (`loginAgent`='" + login + "' AND `motDePasseAgent`='" + motDePasse + "')";
                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        agent.adresseAgent = reader["adresseAgent"].ToString();
                        agent.cinAgent = reader["cinAgent"].ToString();
                        agent.dateNaissanceAgent = Convert.ToDateTime(reader["dateNaissanceAgent"].ToString());
                        agent.lieuNaissanceAgent = reader["lieuNaissanceAgent"].ToString();
                        agent.loginAgent = reader["loginAgent"].ToString();
                        agent.matriculeAgent = reader["matriculeAgent"].ToString();
                        agent.motDePasseAgent = reader["motDePasseAgent"].ToString();
                        agent.nomAgent = reader["nomAgent"].ToString();
                        agent.numAgence = reader["numAgence"].ToString();
                        agent.prenomAgent = reader["prenomAgent"].ToString();
                        agent.telephoneAgent = reader["telephoneAgent"].ToString();
                        agent.telephoneMobileAgent = reader["telephoneMobileAgent"].ToString();
                        agent.typeAgent = reader["typeAgent"].ToString();
                        agent.ImageAgent = reader["imageAgent"].ToString();
                        agent.SituationFamilialeAgent = reader["situationFamilialeAgent"].ToString();
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (agent != null)
                {
                    if (agent.numAgence != "")
                    {
                        agent.agence = serviceAgence.selectAgence(agent.numAgence);
                    }
                    if (agent.matriculeAgent != "")
                    {
                        agent.sessionCaisse = serviceSessionCaisse.getSessionCaisseEncours(agent.matriculeAgent);
                    }
                }
            }
            #endregion

            return agent;
        }

        #endregion

        #region insert to grid
        void IntfDalAgent.insertToGridAgent(GridView gridView, string param, string paramLike, string valueLike, string numAgence)
        {
            #region declaration
            IntfDalAgent serviceAgent = new ImplDalAgent();
            #endregion

            #region implementation
            this.strCommande = "SELECT agent.matriculeAgent, agent.typeAgent, agent.numAgence, agent.nomAgent,";
            this.strCommande += " agent.prenomAgent, agent.dateNaissanceAgent, agent.lieuNaissanceAgent, agent.loginAgent,";
            this.strCommande += " agent.motDePasseAgent, agent.cinAgent, agent.adresseAgent, agent.telephoneAgent,";
            this.strCommande += " agent.telephoneMobileAgent, agent.imageAgent FROM agent";
            this.strCommande += " WHERE agent.numAgence = '" + numAgence + "' AND";
            this.strCommande += " " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param + " ASC";

            gridView.DataSource = serviceAgent.getDataTableAgent(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalAgent.getDataTableAgent(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            crlAgent agent = null;
            IntfDalAgent serviceAgent = new ImplDalAgent();
            #endregion

            #region implemntation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("matriculeAgent", typeof(string));
            dataTable.Columns.Add("agent", typeof(string));
            dataTable.Columns.Add("adresse", typeof(string));
            dataTable.Columns.Add("contact", typeof(string));
            dataTable.Columns.Add("statut", typeof(string));
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

                        agent = serviceAgent.selectAgent(this.reader["matriculeAgent"].ToString());

                        dr["matriculeAgent"] = this.reader["matriculeAgent"].ToString();
                        dr["agent"] = this.reader["prenomAgent"].ToString() + " " + this.reader["nomAgent"].ToString();
                        dr["adresse"] = this.reader["adresseAgent"].ToString();
                        dr["contact"] = this.reader["telephoneAgent"].ToString() + " / " + this.reader["telephoneMobileAgent"].ToString();

                        if (agent.sessionCaisse != null)
                        {
                            dr["statut"] = "vert16.png";
                        }
                        else
                        {
                            dr["statut"] = "rouge16.png";
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

        void IntfDalAgent.insertToGridAgentListe(GridView gridView, string param, string paramLike, string valueLike, string numAgence)
        {
            #region declaration
            IntfDalAgent serviceAgent = new ImplDalAgent();
            #endregion

            #region implementation
            this.strCommande = "SELECT agent.matriculeAgent, agent.typeAgent, agent.numAgence, agent.nomAgent,";
            this.strCommande += " agent.prenomAgent, agent.dateNaissanceAgent, agent.lieuNaissanceAgent, agent.loginAgent,";
            this.strCommande += " agent.motDePasseAgent, agent.cinAgent, agent.adresseAgent, agent.telephoneAgent,";
            this.strCommande += " agent.telephoneMobileAgent, agent.imageAgent FROM agent";
            this.strCommande += " WHERE agent.numAgence LIKE '%" + numAgence + "%' AND";
            this.strCommande += " " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param + " ASC";

            gridView.DataSource = serviceAgent.getDataTableAgentListe(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalAgent.getDataTableAgentListe(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            crlAgent agent = null;
            IntfDalAgent serviceAgent = new ImplDalAgent();
            #endregion

            #region implemntation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("matriculeAgent", typeof(string));
            dataTable.Columns.Add("agent", typeof(string));
            dataTable.Columns.Add("adresse", typeof(string));
            dataTable.Columns.Add("contact", typeof(string));
            dataTable.Columns.Add("type");
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

                        agent = serviceAgent.selectAgent(this.reader["matriculeAgent"].ToString());

                        dr["matriculeAgent"] = this.reader["matriculeAgent"].ToString();
                        dr["agent"] = this.reader["prenomAgent"].ToString() + " " + this.reader["nomAgent"].ToString();
                        dr["adresse"] = this.reader["adresseAgent"].ToString();
                        dr["contact"] = this.reader["telephoneAgent"].ToString() + " / " + this.reader["telephoneMobileAgent"].ToString();

                        dr["type"] = this.reader["typeAgent"].ToString();

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }

            this.serviceConnectBase.closeConnection();


            #endregion

            return dataTable;
        }


        void IntfDalAgent.insertToGridAgentParType(GridView gridView, string param, string paramLike, string valueLike, string numAgence, string typeAgent)
        {
            #region declaration
            IntfDalAgent serviceAgent = new ImplDalAgent();
            #endregion

            #region implementation
            this.strCommande = "SELECT agent.matriculeAgent, agent.typeAgent, agent.numAgence, agent.nomAgent,";
            this.strCommande += " agent.prenomAgent, agent.dateNaissanceAgent, agent.lieuNaissanceAgent,";
            this.strCommande += " agent.loginAgent, agent.motDePasseAgent, agent.cinAgent, agent.adresseAgent,";
            this.strCommande += " agent.telephoneAgent, agent.telephoneMobileAgent, agent.imageAgent,";
            this.strCommande += " agent.situationFamilialeAgent FROM agent";
            this.strCommande += " WHERE agent.typeAgent LIKE  '%" + typeAgent + "%' AND";
            this.strCommande += " agent.numAgence LIKE  '%" + numAgence + "%' AND";
            this.strCommande += " " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param + " ASC";

            gridView.DataSource = serviceAgent.getDataTableAgentParType(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalAgent.getDataTableAgentParType(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            crlAgent agent = null;
            IntfDalAgent serviceAgent = new ImplDalAgent();
            #endregion

            #region implemntation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("matriculeAgent", typeof(string));
            dataTable.Columns.Add("agent", typeof(string));
            dataTable.Columns.Add("adresse", typeof(string));
            dataTable.Columns.Add("contact", typeof(string));
            dataTable.Columns.Add("type");
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

                        agent = serviceAgent.selectAgent(this.reader["matriculeAgent"].ToString());

                        dr["matriculeAgent"] = this.reader["matriculeAgent"].ToString();
                        dr["agent"] = this.reader["prenomAgent"].ToString() + " " + this.reader["nomAgent"].ToString();
                        dr["adresse"] = this.reader["adresseAgent"].ToString();
                        dr["contact"] = this.reader["telephoneAgent"].ToString() + " / " + this.reader["telephoneMobileAgent"].ToString();

                        dr["type"] = this.reader["typeAgent"].ToString();

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



        void IntfDalAgent.insertToGridAgentListeNoire(GridView gridView, string param, string paramLike, string valueLike, string numAgence)
        {
            #region declaration
            IntfDalAgent serviceAgent = new ImplDalAgent();
            #endregion

            #region implementation
            this.strCommande = "SELECT agent.matriculeAgent, agent.typeAgent, agent.numAgence, agent.nomAgent,";
            this.strCommande += " agent.prenomAgent, agent.dateNaissanceAgent, agent.lieuNaissanceAgent,";
            this.strCommande += " agent.loginAgent, agent.motDePasseAgent, agent.cinAgent, agent.adresseAgent,";
            this.strCommande += " agent.telephoneAgent, agent.telephoneMobileAgent, agent.imageAgent,";
            this.strCommande += " agent.situationFamilialeAgent FROM agent";
            this.strCommande += " Inner Join observationagent ON observationagent.matriculeAgent = agent.matriculeAgent";
            this.strCommande += " WHERE observationagent.isListeNoire = '2' AND";
            this.strCommande += " agent.numAgence LIKE '%" + numAgence + "%' AND";
            this.strCommande += " " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param + " ASC";

            gridView.DataSource = serviceAgent.getDataTableAgentListe(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalAgent.getDataTableAgentListeNoire(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            crlAgent agent = null;
            IntfDalAgent serviceAgent = new ImplDalAgent();
            #endregion

            #region implemntation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("matriculeAgent", typeof(string));
            dataTable.Columns.Add("agent", typeof(string));
            dataTable.Columns.Add("adresse", typeof(string));
            dataTable.Columns.Add("contact", typeof(string));
            dataTable.Columns.Add("type");
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

                        agent = serviceAgent.selectAgent(this.reader["matriculeAgent"].ToString());

                        dr["matriculeAgent"] = this.reader["matriculeAgent"].ToString();
                        dr["agent"] = this.reader["prenomAgent"].ToString() + " " + this.reader["nomAgent"].ToString();
                        dr["adresse"] = this.reader["adresseAgent"].ToString();
                        dr["contact"] = this.reader["telephoneAgent"].ToString() + " / " + this.reader["telephoneMobileAgent"].ToString();

                        dr["type"] = this.reader["typeAgent"].ToString();

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
