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
    /// Implementation du service trajet (US)
    /// </summary>
    public class ImplDalUSTrajet : IntfDalUSTrajet
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalUSTrajet(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalUSTrajet()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion

        #region IntfDalUSTrajet Members

        string IntfDalUSTrajet.insertUSTrajet(crlUSTrajet trajet, string sigleAgence)
        {
            #region declaration
            string numTrajet = "";
            IntfDalUSTrajet serviceUSTrajet = new ImplDalUSTrajet();
            int nbInsert = 0;
            #endregion

            #region implemenation
            if (trajet != null && sigleAgence != "")
            {
                trajet.NumTrajet = serviceUSTrajet.getNumUSTrajet(sigleAgence);
                this.strCommande = "INSERT INTO `ustrajet` (`numTrajet`,`distanceTrajet`,`dureeTrajet`,`numArretD`,`numArretF`)";
                this.strCommande += " VALUES ('" + trajet.NumTrajet + "','" + trajet.DistanceTrajet + "','" + trajet.DureeTrajet + "',";
                this.strCommande += " '" + trajet.NumArretD + "','" + trajet.NumArretF + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    numTrajet = trajet.NumTrajet;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numTrajet;
        }

        bool IntfDalUSTrajet.updateUSTrajet(crlUSTrajet trajet)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            #endregion

            #region implementation
            if (trajet != null)
            {
                this.strCommande = "UPDATE `ustrajet` SET `distanceTrajet`='" + trajet.DistanceTrajet + "',";
                this.strCommande += " `dureeTrajet`='" + trajet.DureeTrajet + "',";
                this.strCommande += " `numArretD`='" + trajet.NumArretD + "',`numArretF`='" + trajet.NumArretF + "' WHERE";
                this.strCommande += " `numTrajet`='" + trajet.NumTrajet + "'";

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

        crlUSTrajet IntfDalUSTrajet.selectUSTrajet(string numTrajet)
        {
            #region declaration
            crlUSTrajet trajet = null;
            IntfDalUSArret serviceUSArret = new ImplDalUSArret();
            #endregion

            #region implementation
            if (numTrajet != "")
            {
                this.strCommande = "SELECT * FROM `ustrajet` WHERE (`numTrajet`='" + numTrajet + "')";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            trajet = new crlUSTrajet();
                            trajet.NumArretD = this.reader["numArretD"].ToString();
                            trajet.NumArretF = this.reader["numArretF"].ToString();
                            try
                            {
                                trajet.DistanceTrajet = double.Parse(this.reader["distanceTrajet"].ToString());
                            }
                            catch (Exception) { }
                            trajet.DureeTrajet = this.reader["dureeTrajet"].ToString();
                            trajet.NumTrajet = this.reader["numTrajet"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                if (trajet != null)
                {
                    trajet.arretD = serviceUSArret.selectUSArret(trajet.NumArretD);
                    trajet.arretF = serviceUSArret.selectUSArret(trajet.NumArretF);
                }
            }
            #endregion

            return trajet;
        }

        string IntfDalUSTrajet.getNumUSTrajet(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numTrajet = "00001";
            string[] tempNumTrajet = null;
            string strDate = "TU" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT ustrajet.numTrajet AS maxNum FROM ustrajet";
            this.strCommande += " WHERE ustrajet.numTrajet LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumTrajet = reader["maxNum"].ToString().ToString().Split('/');
                        numTrajet = tempNumTrajet[tempNumTrajet.Length - 1];
                    }
                    numTemp = double.Parse(numTrajet) + 1;
                    if (numTemp < 10)
                        numTrajet = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numTrajet = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numTrajet = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numTrajet = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numTrajet = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numTrajet = strDate + "/" + sigleAgence + "/" + numTrajet;
            #endregion

            return numTrajet;
        }

        string IntfDalUSTrajet.isUSTrajet(crlUSTrajet trajet)
        {
            #region declaration
            string numTrajet = "";
            #endregion

            #region implementation
            if (trajet != null)
            {
                this.strCommande = "SELECT * FROM `ustrajet` WHERE";
                this.strCommande += " `distanceTrajet`=" + trajet.DistanceTrajet + " AND";
                this.strCommande += " `dureeTrajet`='" + trajet.DureeTrajet + "' AND";
                this.strCommande += " `numArretD`='" + trajet.NumArretD + "' AND";
                this.strCommande += " `numArretF`='" + trajet.NumArretF + "' AND";
                this.strCommande += " `numTrajet`<>'" + trajet.NumTrajet + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            numTrajet = this.reader["numTrajet"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numTrajet;
        }

        crlUSTrajet IntfDalUSTrajet.getTrajet(string numArretD, string numArretF)
        {
            #region declaration
            crlUSTrajet trajet = null;
            #endregion

            #region implementation
            if (numArretD != "" && numArretF != "")
            {
                this.strCommande = "SELECT ustrajet.numTrajet, ustrajet.distanceTrajet, ustrajet.dureeTrajet,";
                this.strCommande += " ustrajet.numArretD, ustrajet.numArretF FROM ustrajet";
                this.strCommande += " WHERE (ustrajet.numArretD = '" + numArretD + "' AND";
                this.strCommande += " ustrajet.numArretF = '" + numArretF + "') OR";
                this.strCommande += " (ustrajet.numArretD = '" + numArretF + "' AND";
                this.strCommande += " ustrajet.numArretF = '" + numArretD + "')";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            trajet = new crlUSTrajet();
                            try
                            {
                                trajet.DistanceTrajet = double.Parse(this.reader["distanceTrajet"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            trajet.DureeTrajet = this.reader["dureeTrajet"].ToString();
                            trajet.NumArretD = this.reader["numArretD"].ToString();
                            trajet.NumArretF = this.reader["numArretF"].ToString();
                            trajet.NumTrajet = this.reader["numTrajet"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return trajet;
        }
        #endregion


        void IntfDalUSTrajet.insertToGridTrajet(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalUSTrajet serviceUSTrajet = new ImplDalUSTrajet();
            #endregion

            #region implementation

            this.strCommande = "SELECT ustrajet.numTrajet, ustrajet.distanceTrajet, ustrajet.dureeTrajet,";
            this.strCommande += " ustrajet.numArretD, ustrajet.numArretF FROM ustrajet";
            this.strCommande += " Left Join usarret ON usarret.numArret = ustrajet.numArretD OR usarret.numArret = ustrajet.numArretF";
            this.strCommande += " Left Join uslieu ON uslieu.numLieu = usarret.numLieu";
            this.strCommande += " WHERE " + paramLike + " LIKE  '%" + valueLike + "%'";
            this.strCommande += " GROUP BY ustrajet.numTrajet";
            this.strCommande += " ORDER BY " + param;


            gridView.DataSource = serviceUSTrajet.getDataTableTrajet(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalUSTrajet.getDataTableTrajet(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            crlUSArret arretD = null;
            crlUSArret arretF = null;
            crlUSLieu lieuD = null;
            crlUSLieu lieuF = null;
            crlQuartier quartierD = null;
            crlQuartier quartierF = null;

            IntfDalUSArret serviceUSArret = new ImplDalUSArret();
            IntfDalUSLieu serviceUSLieu = new ImplDalUSLieu();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            IntfDalQuartier serviceQuartier = new ImplDalQuartier();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numTrajet", typeof(string));
            dataTable.Columns.Add("distanceTrajet", typeof(string));
            dataTable.Columns.Add("dureeTrajet", typeof(string));
            dataTable.Columns.Add("trajet", typeof(string));

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

                        dr["numTrajet"] = reader["numTrajet"].ToString();
                        dr["distanceTrajet"] = reader["distanceTrajet"].ToString() + "Km";
                        dr["dureeTrajet"] = serviceGeneral.getTextTimeSpan(reader["dureeTrajet"].ToString());

                        arretD = serviceUSArret.selectUSArret(this.reader["numArretD"].ToString());
                        arretF = serviceUSArret.selectUSArret(this.reader["numArretF"].ToString());

                        if (arretD != null && arretF != null)
                        {
                            lieuD = serviceUSLieu.selectUSLieu(arretD.NumLieu);
                            lieuF = serviceUSLieu.selectUSLieu(arretF.NumLieu);

                            if (lieuD != null && lieuF != null)
                            {
                                quartierD = serviceQuartier.selectQuartier(lieuD.NumQuartier);
                                quartierF = serviceQuartier.selectQuartier(lieuF.NumQuartier);

                                if (quartierF != null && quartierF != null)
                                {
                                    dr["trajet"] = quartierD.Quartier + "/" + arretD.NomArret + "-" + quartierF.Quartier + "/" + arretF.NomArret;
                                }
                                else
                                {
                                    dr["trajet"] = arretD.NomArret + "-" + arretF.NomArret;
                                }
                            }
                            else
                            {
                                dr["trajet"] = arretD.NomArret + "-" + arretF.NomArret;
                            }
                        }
                        else
                        {
                            dr["trajet"] = this.reader["numArretD"].ToString() + "-" + this.reader["numArretF"].ToString();
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