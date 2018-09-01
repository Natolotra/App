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
    /// Summary description for ImplDalObservationMateriel
    /// </summary>
    public class ImplDalObservationMateriel : IntfDalObservationMateriel
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalObservationMateriel()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalObservationMateriel(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion


        #region IntfDalObservationMateriel Members

        string IntfDalObservationMateriel.insertObservationMateriel(crlObservationMateriel observation, string sigleAgence)
        {
            #region declaration
            string numObservation = "";
            IntfDalObservationMateriel serviceObservationMateriel = new ImplDalObservationMateriel();
            int nbInsert = 0;
            #endregion

            #region implementation
            if (observation != null && sigleAgence != "")
            {
                observation.NumObservation = serviceObservationMateriel.getNumObservation(sigleAgence);

                this.strCommande = "INSERT INTO `observationmateriel` (`numObservation`,`numAppareil`,`textObservation`,";
                this.strCommande += "`dateObservation`,`isListeNoire`) VALUES ('" + observation.NumObservation + "',";
                this.strCommande += "'" + observation.NumAppareil + "','" + observation.TextObesvation + "',";
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

        bool IntfDalObservationMateriel.updateObservationMateriel(crlObservationMateriel observation)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            #endregion

            #region implementation
            if (observation != null)
            {
                this.strCommande = "UPDATE `observationmateriel` SET `numAppareil`='" + observation.NumAppareil + "',";
                this.strCommande += "`textObservation`='" + observation.TextObesvation + "',";
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

        crlObservationMateriel IntfDalObservationMateriel.selectObservationMateriel(string numObservation)
        {
            #region declaration
            crlObservationMateriel observationMateriel = null;
            #endregion

            #region implementation
            if (numObservation != "")
            {
                this.strCommande = "SELECT * FROM `observationmateriel` WHERE `numObservation`='" + numObservation + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            observationMateriel = new crlObservationMateriel();
                            try
                            {
                                observationMateriel.DateObservation = Convert.ToDateTime(this.reader["dateObservation"].ToString());
                            }
                            catch (Exception) { }
                            observationMateriel.NumAppareil = this.reader["numAppareil"].ToString();
                            try
                            {
                                observationMateriel.IsListeNoire = int.Parse(this.reader["isListeNoire"].ToString());
                            }
                            catch (Exception) { }
                            observationMateriel.NumObservation = this.reader["numObservation"].ToString();
                            observationMateriel.TextObesvation = this.reader["textObservation"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return observationMateriel;
        }

        string IntfDalObservationMateriel.getNumObservation(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numObservation = "00001";
            string[] tempNumObservation = null;
            string strDate = "OP" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT observationmateriel.numObservation AS maxNum FROM observationmateriel";
            this.strCommande += " WHERE observationmateriel.numObservation LIKE '%" + sigleAgence + "%'";
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

        int IntfDalObservationMateriel.getObservationMateriel(string numAppareil)
        {
            #region declaration
            int isListeNoire = 0;
            #endregion

            #region implementation
            if (numAppareil != "")
            {
                this.strCommande = "SELECT observationmateriel.isListeNoire FROM observationmateriel";
                this.strCommande += " WHERE observationmateriel.numAppareil = '" + numAppareil + "' AND";
                this.strCommande += " observationmateriel.isListeNoire > '0'";

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

        string IntfDalObservationMateriel.getObservationMateriel(string numAppareil, int isListeNoire)
        {
            #region declaration
            string observationDateHtml = "";
            string strObservation = "";
            DateTime dateObservation;
            #endregion

            #region implementation
            if (numAppareil != "")
            {
                this.strCommande = "SELECT observationmateriel.textObservation, observationmateriel.dateObservation";
                this.strCommande += " FROM observationmateriel WHERE observationmateriel.numAppareil = '" + numAppareil + "' AND";
                this.strCommande += " observationmateriel.isListeNoire = '" + isListeNoire + "'";
                this.strCommande += " ORDER BY observationmateriel.dateObservation DESC";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            strObservation = this.reader["textObservation"].ToString();
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

        void IntfDalObservationMateriel.insertToGridObservationMateriel(GridView gridView, string param, string paramLike, string valueLike, string numAppareil)
        {
            #region declaration
            IntfDalObservationMateriel serviceObservationMateriel = new ImplDalObservationMateriel();
            #endregion

            #region implementation
            this.strCommande = "SELECT observationmateriel.numObservation, observationmateriel.numAppareil,";
            this.strCommande += " observationmateriel.textObservation, observationmateriel.dateObservation,";
            this.strCommande += " observationmateriel.isListeNoire, usappareil.numAppareil,";
            this.strCommande += " usappareil.typeAppareil, usappareil.numSerie FROM observationmateriel";
            this.strCommande += " Inner Join usappareil ON usappareil.numAppareil = observationmateriel.numAppareil";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%' AND";
            this.strCommande += " observationmateriel.numAppareil LIKE '%" + numAppareil + "%'";
            this.strCommande += " ORDER BY " + param + " ASC";

            gridView.DataSource = serviceObservationMateriel.getDataTableObservationMateriel(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalObservationMateriel.getDataTableObservationMateriel(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numObservation", typeof(string));
            dataTable.Columns.Add("appareil", typeof(string));
            dataTable.Columns.Add("textObservation", typeof(string));
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
                        dr["appareil"] = reader["typeAppareil"].ToString() + " " + reader["numSerie"].ToString();
                        dr["textObservation"] = reader["textObservation"].ToString();
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
