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
    /// Implementation du service observation chauffeur
    /// </summary>
    public class ImplDalObservationChauffeur : IntfDalObservationChauffeur
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalObservationChauffeur()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalObservationChauffeur(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region IntfDalObservationChauffeur Members

        string IntfDalObservationChauffeur.insertObservationChauffeur(crlObservationChauffeur observation, string sigleAgence)
        {
            #region declaration
            string numObservation = "";
            IntfDalObservationChauffeur serviceObservationChauffeur = new ImplDalObservationChauffeur();
            int nbInsert = 0;
            #endregion

            #region implementation
            if (observation != null && sigleAgence != "") 
            {
                observation.NumObservation = serviceObservationChauffeur.getNumObservation(sigleAgence);

                this.strCommande = "INSERT INTO `observationchauffeur` (`numObservation`,`idChauffeur`,`textObesvation`,";
                this.strCommande += "`dateObservation`,`isListeNoire`) VALUES ('" + observation.NumObservation + "',";
                this.strCommande += "'" + observation.IdChauffeur + "','" + observation.TextObesvation + "',";
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

        bool IntfDalObservationChauffeur.updateObservationChauffeur(crlObservationChauffeur observation)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            #endregion

            #region implementation
            if (observation != null) 
            {
                this.strCommande = "UPDATE `observationchauffeur` SET `idChauffeur`='" + observation.IdChauffeur + "',";
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

        crlObservationChauffeur IntfDalObservationChauffeur.selectObservationChauffeur(string numObservation)
        {
            #region declaration
            crlObservationChauffeur observationChauffeur = null;
            #endregion

            #region implementation
            if (numObservation != "") 
            {
                this.strCommande = "SELECT * FROM `observationchauffeur` WHERE `numObservation`='" + numObservation + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            observationChauffeur = new crlObservationChauffeur();
                            try
                            {
                                observationChauffeur.DateObservation = Convert.ToDateTime(this.reader["dateObservation"].ToString());
                            }
                            catch (Exception) { }
                            observationChauffeur.IdChauffeur = this.reader["idChauffeur"].ToString();
                            try
                            {
                                observationChauffeur.IsListeNoire = int.Parse(this.reader["isListeNoire"].ToString());
                            }
                            catch (Exception) { }
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

        string IntfDalObservationChauffeur.getNumObservation(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numObservation = "00001";
            string[] tempNumObservation = null;
            string strDate = "OC" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT observationchauffeur.numObservation AS maxNum FROM observationchauffeur";
            this.strCommande += " WHERE observationchauffeur.numObservation LIKE '%" + sigleAgence + "%'";
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
                    if (numTemp < 10)
                        numObservation = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numObservation = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numObservation = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numObservation = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numObservation = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numObservation = strDate + "/" + sigleAgence + "/" + numObservation;
            #endregion

            return numObservation;
        }

        int IntfDalObservationChauffeur.getObservationChauffeur(string idChauffeur)
        {
            #region declaration
            int isListeNoire = 0;
            #endregion

            #region implementation
            if (idChauffeur != "")
            {
                this.strCommande = "SELECT observationchauffeur.isListeNoire FROM observationchauffeur";
                this.strCommande += " WHERE observationchauffeur.idChauffeur = '" + idChauffeur + "' AND";
                this.strCommande += " observationchauffeur.isListeNoire > '0'";

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

        string IntfDalObservationChauffeur.getObservationChauffeur(string idChauffeur, int isListeNoire)
        {
            #region declaration
            string observationDateHtml = "";
            string strObservation = "";
            DateTime dateObservation;
            #endregion

            #region implementation
            if (idChauffeur != "")
            {
                this.strCommande = "SELECT observationchauffeur.textObesvation, observationchauffeur.dateObservation";
                this.strCommande += " FROM observationchauffeur WHERE observationchauffeur.idChauffeur = '" + idChauffeur + "' AND";
                this.strCommande += " observationchauffeur.isListeNoire = '" + isListeNoire + "'";
                this.strCommande += " ORDER BY observationchauffeur.dateObservation DESC";

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

        #region IntfDalObservationChauffeur Members


        void IntfDalObservationChauffeur.insertToGridObservationChauffeur(GridView gridView, string param, string paramLike, string valueLike, string idChauffeur)
        {
            #region declaration
            IntfDalObservationChauffeur serviceObservationChauffeur = new ImplDalObservationChauffeur();
            #endregion

            #region implementation
            this.strCommande = "SELECT observationchauffeur.numObservation, observationchauffeur.idChauffeur,";
            this.strCommande += " observationchauffeur.textObesvation, observationchauffeur.dateObservation,";
            this.strCommande += " observationchauffeur.isListeNoire, chauffeur.idChauffeur, chauffeur.nomChauffeur,";
            this.strCommande += " chauffeur.prenomChauffeur, chauffeur.cinChauffeur, chauffeur.adresseChauffeur,";
            this.strCommande += " chauffeur.telephoneChauffeur, chauffeur.telephoneMobileChauffeur,";
            this.strCommande += " chauffeur.imageChauffeur FROM observationchauffeur";
            this.strCommande += " Inner Join chauffeur ON chauffeur.idChauffeur = observationchauffeur.idChauffeur";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%' AND";
            this.strCommande += " observationchauffeur.idChauffeur LIKE '%" + idChauffeur  + "%'";
            this.strCommande += " ORDER BY " + param + " ASC";

            gridView.DataSource = serviceObservationChauffeur.getDataTableObservationChauffeur(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalObservationChauffeur.getDataTableObservationChauffeur(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numObservation", typeof(string));
            dataTable.Columns.Add("chauffeur", typeof(string));
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
                        dr["chauffeur"] = reader["prenomChauffeur"].ToString() + " " + reader["nomChauffeur"].ToString();
                        dr["textObesvation"] = reader["textObesvation"].ToString();
                        try
                        {
                            dr["dateObservation"] = Convert.ToDateTime(reader["dateObservation"].ToString());
                        }
                        catch (Exception) { }

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
