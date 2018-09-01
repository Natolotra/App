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
using MySql.Data.MySqlClient;
using arch.dal.intf;
using arch.crl;

namespace arch.dal.impl
{
    /// <summary>
    /// Implementation du service vehicule
    /// </summary>
    public class ImplDalObservationVehicule : IntfDalObservationVehicule
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalObservationVehicule()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalObservationVehicule(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region IntfDalObservationVehicule Members

        string IntfDalObservationVehicule.insertObservationVehicule(crlObservationVehicule observation, string sigleAgence)
        {
            #region declaration
            string numObservation = "";
            IntfDalObservationVehicule serviceObservationVehicule = new ImplDalObservationVehicule();
            int nbInsert = 0;
            #endregion

            #region implementation
            if (observation != null && sigleAgence != "")
            {
                observation.NumObservationVehicule = serviceObservationVehicule.getNumObservationVehicule(sigleAgence);

                this.strCommande = "INSERT INTO `observationvehicule` (`numObservationVehicule`,`numVehicule`,`textObesvationVehicule`,";
                this.strCommande += "`dateObservation`,`isListeNoire`) VALUES ('" + observation.NumObservationVehicule + "',";
                this.strCommande += "'" + observation.NumVehicule + "','" + observation.TextObesvationVehicule + "',";
                this.strCommande += "'" + observation.DateObservation.ToString("yyyy-MM-dd") + "','" + observation.IsListeNoire + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    numObservation = observation.NumObservationVehicule;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numObservation;
        }

        bool IntfDalObservationVehicule.updateObservationVehicule(crlObservationVehicule observation)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            #endregion

            #region implementation
            if (observation != null)
            {
                this.strCommande = "UPDATE `observationvehicule` SET `numVehicule`='" + observation.NumVehicule + "',";
                this.strCommande += "`textObesvationVehicule`='" + observation.TextObesvationVehicule + "',";
                this.strCommande += "`dateObservation`='" + observation.DateObservation.ToString("yyyy-MM-dd") + "',";
                this.strCommande += "`isListeNoire`='" + observation.IsListeNoire.ToString("0") + "'";
                this.strCommande += " WHERE `numObservationVehicule`='" + observation.NumObservationVehicule + "'";

                this.serviceConnectBase.openConnection();
                nbUpdate = this.serviceConnectBase.requete(this.strCommande);
                if (nbUpdate == 1)
                    isUpdate = true;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isUpdate;
        }

        crlObservationVehicule IntfDalObservationVehicule.selectObservationVehicule(string numObservationVehicule)
        {
            #region declaration
            crlObservationVehicule observationVehicule = null;
            #endregion

            #region implementation
            if (numObservationVehicule != "")
            {
                this.strCommande = "SELECT * FROM `observationvehicule` WHERE `numObservationVehicule`='" + numObservationVehicule + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            observationVehicule = new crlObservationVehicule();
                            try
                            {
                                observationVehicule.DateObservation = Convert.ToDateTime(this.reader["dateObservation"].ToString());
                            }
                            catch (Exception) { }
                            observationVehicule.NumVehicule = this.reader["numVehicule"].ToString();
                            try
                            {
                                observationVehicule.IsListeNoire = int.Parse(this.reader["isListeNoire"].ToString());
                            }
                            catch (Exception) { }
                            observationVehicule.NumObservationVehicule = this.reader["numObservationVehicule"].ToString();
                            observationVehicule.TextObesvationVehicule = this.reader["textObesvationVehicule"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return observationVehicule;
        }

        string IntfDalObservationVehicule.getNumObservationVehicule(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numObservationVehicule = "00001";
            string[] tempNumObservationVehicule = null;
            string strDate = "OV" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT observationvehicule.numObservationVehicule AS maxNum FROM observationvehicule";
            this.strCommande += " WHERE observationvehicule.numObservationVehicule LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumObservationVehicule = reader["maxNum"].ToString().ToString().Split('/');
                        numObservationVehicule = tempNumObservationVehicule[tempNumObservationVehicule.Length - 1];
                    }
                    numTemp = double.Parse(numObservationVehicule) + 1;
                    if (numTemp < 10)
                        numObservationVehicule = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numObservationVehicule = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numObservationVehicule = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numObservationVehicule = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numObservationVehicule = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numObservationVehicule = strDate + "/" + sigleAgence + "/" + numObservationVehicule;
            #endregion

            return numObservationVehicule;
        }

        int IntfDalObservationVehicule.getObservationVehicule(string numVehicule)
        {
            #region declaration
            int isListeNoire = 0;
            #endregion

            #region implementation
            if (numVehicule != "")
            {
                this.strCommande = "SELECT observationvehicule.isListeNoire FROM observationvehicule";
                this.strCommande += " WHERE observationvehicule.numVehicule = '" + numVehicule + "' AND";
                this.strCommande += " observationvehicule.isListeNoire > '0'";

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

        string IntfDalObservationVehicule.getObservationVehicule(string numVehicule, int isListeNoire)
        {
            #region declaration
            string observationDateHtml = "";
            string strObservation = "";
            DateTime dateObservation;
            #endregion

            #region implementation
            if (numVehicule != "")
            {
                this.strCommande = "SELECT observationvehicule.textObesvationVehicule, observationvehicule.dateObservation";
                this.strCommande += " FROM observationvehicule WHERE observationvehicule.numVehicule = '" + numVehicule + "' AND";
                this.strCommande += " observationvehicule.isListeNoire = '" + isListeNoire + "'";
                this.strCommande += " ORDER BY observationvehicule.dateObservation DESC";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            strObservation = this.reader["textObesvationVehicule"].ToString();
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
        void IntfDalObservationVehicule.insertToGridObservationVehicule(GridView gridView, string param, string paramLike, string valueLike, string numVehicule)
        {
            #region declaration
            IntfDalObservationVehicule serviceObservationVehicule = new ImplDalObservationVehicule();
            #endregion
            
            #region implementation
            this.strCommande = "SELECT observationvehicule.numObservationVehicule, observationvehicule.numVehicule,";
            this.strCommande += " observationvehicule.textObesvationVehicule, observationvehicule.dateObservation,";
            this.strCommande += " observationvehicule.isListeNoire, vehicule.numVehicule, vehicule.numParamVehicule,";
            this.strCommande += " vehicule.sourceEnergie, vehicule.numProprietaire, vehicule.matriculeVehicule,";
            this.strCommande += " vehicule.marqueVehicule, vehicule.typeVehicule, vehicule.numSerieVehicule,";
            this.strCommande += " vehicule.numMoteurVehicule, vehicule.puissanceVehicule, vehicule.couleurVehicule,";
            this.strCommande += " vehicule.placesAssiseVehicule, vehicule.nombreColoneVehicule, vehicule.poidsTotalVehicule,";
            this.strCommande += " vehicule.poidsVideVehicule, vehicule.imageVehicule FROM observationvehicule";
            this.strCommande += " Inner Join vehicule ON vehicule.numVehicule = observationvehicule.numVehicule";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%' AND";
            this.strCommande += " observationvehicule.numVehicule LIKE '%" + numVehicule + "%'";
            this.strCommande += " ORDER BY " + param + " ASC";
            
            gridView.DataSource = serviceObservationVehicule.getDataTableObservationVehicule(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalObservationVehicule.getDataTableObservationVehicule(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numObservationVehicule", typeof(string));
            dataTable.Columns.Add("vehicule", typeof(string));
            dataTable.Columns.Add("textObesvationVehicule", typeof(string));
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

                        dr["numObservationVehicule"] = reader["numObservationVehicule"].ToString();
                        dr["vehicule"] = reader["matriculeVehicule"].ToString() + " " + reader["marqueVehicule"].ToString() + " " + reader["couleurVehicule"].ToString();
                        dr["textObesvationVehicule"] = reader["textObesvationVehicule"].ToString();
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
