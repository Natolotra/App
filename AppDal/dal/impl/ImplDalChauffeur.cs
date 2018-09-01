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
    /// Implementation du service chauffeur
    /// </summary>
    public class ImplDalChauffeur : IntfDalChauffeur
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalChauffeur()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalChauffeur(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region IntfDalChauffeur Members

        string IntfDalChauffeur.insertChauffeur(crlChauffeur Chauffeur, string sigleAgence)
        {
            #region declaration
            IntfDalChauffeur serviceChauffeur = new ImplDalChauffeur();
            string idChauffeur = "";
            int nombreInsert = 0;
            string numCooperative = "NULL";
            #endregion

            #region implementation
            if (Chauffeur != null)
            {
                if (Chauffeur.NumCooperative != "")
                {
                    numCooperative = "'" + Chauffeur.NumCooperative + "'";
                }
                Chauffeur.idChauffeur = serviceChauffeur.getIdChauffeur(sigleAgence);
                this.strCommande = "INSERT INTO `chauffeur` (`idChauffeur`, `nomChauffeur`, `prenomChauffeur`, `cinChauffeur`, ";
                this.strCommande += "`adresseChauffeur`, `telephoneChauffeur`, `telephoneMobileChauffeur`,`imageChauffeur`,`numCooperative`, ";
                this.strCommande += "`situationFamilialeChauffeur`,`dateNaissanceChauffeur`,`lieuNaissanceChauffeur`)";
                this.strCommande += "VALUES ('" + Chauffeur.idChauffeur + "', '" + Chauffeur.nomChauffeur + "', '" + Chauffeur.prenomChauffeur + "', ";
                this.strCommande += "'" + Chauffeur.cinChauffeur + "', '" + Chauffeur.adresseChauffeur + "', '" + Chauffeur.telephoneChauffeur + "', ";
                this.strCommande += "'" + Chauffeur.telephoneMobileChauffeur + "','" + Chauffeur.ImageChauffeur + "'," + numCooperative + ", ";
                this.strCommande += "'" + Chauffeur.SituationFamilialeChauffeur + "','" + Chauffeur.DateNaissanceChauffeur.ToString("yyyy-MM-dd") + "', ";
                this.strCommande += "'" + Chauffeur.LieuNaissanceChauffeur + "')";

                this.serviceConnectBase.openConnection();
                nombreInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsert == 1)
                    idChauffeur = Chauffeur.idChauffeur;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return idChauffeur;
        }

        bool IntfDalChauffeur.insertAssoVehiculeChauffeur(string numVehicule, string idChauffeur)
        {
            #region declaration
            bool isInsert = false;
            int nbInsert = 0;
            #endregion

            #region implementation
            if (numVehicule != "" && idChauffeur != "")
            {
                this.strCommande = "INSERT INTO `assovehiculechauffeur` (`numVehicule`,`idChauffeur`)";
                this.strCommande += " VALUES ('" + numVehicule + "','" + idChauffeur + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    isInsert = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isInsert;
        }

        bool IntfDalChauffeur.deleteChauffeur(crlChauffeur Chauffeur)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (Chauffeur != null)
            {
                if (Chauffeur.idChauffeur != "")
                {
                    this.strCommande = "DELETE FROM `chauffeur` WHERE (`idChauffeur` = '" + Chauffeur.idChauffeur + "')";
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

        bool IntfDalChauffeur.deleteChauffeur(string idChauffeur)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (idChauffeur != "")
            {
                this.strCommande = "DELETE FROM `chauffeur` WHERE (`idChauffeur` = '" + idChauffeur + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete == 1)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isDelete;
        }

        bool IntfDalChauffeur.deleteAssoVehiculeChauffeur(string numVehicule, string idChauffeur)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (idChauffeur != "" && numVehicule != "")
            {
                this.strCommande = "DELETE FROM `assovehiculechauffeur` WHERE (`idChauffeur` = '" + idChauffeur + "' AND";
                this.strCommande += " `numVehicule`='" + numVehicule + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete == 1)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isDelete;
        }

        bool IntfDalChauffeur.updateChauffeur(crlChauffeur Chauffeur)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            string numCooperative = "NULL";
            #endregion

            #region implementation
            if (Chauffeur != null)
            {
                if (Chauffeur.idChauffeur != "")
                {
                    if (Chauffeur.NumCooperative != "")
                    {
                        numCooperative = "'" + Chauffeur.NumCooperative + "'";
                    }

                    this.strCommande = "UPDATE `chauffeur` SET `nomChauffeur`='" + Chauffeur.nomChauffeur + "', `prenomChauffeur`='" + Chauffeur.prenomChauffeur + "', ";
                    this.strCommande += "`cinChauffeur`='" + Chauffeur.cinChauffeur + "', `adresseChauffeur`='" + Chauffeur.adresseChauffeur + "', ";
                    this.strCommande += "`telephoneChauffeur`='" + Chauffeur.telephoneChauffeur + "', `telephoneMobileChauffeur`='" + Chauffeur.telephoneMobileChauffeur + "', ";
                    this.strCommande += "`imageChauffeur`='" + Chauffeur.ImageChauffeur + "', `numCooperative`=" + numCooperative + ",";
                    this.strCommande += "`situationFamilialeChauffeur`='" + Chauffeur.SituationFamilialeChauffeur + "',";
                    this.strCommande += "`dateNaissanceChauffeur`='" + Chauffeur.DateNaissanceChauffeur.ToString("yyyy-MM-dd") + "',";
                    this.strCommande += "`lieuNaissanceChauffeur`='" + Chauffeur.LieuNaissanceChauffeur + "' WHERE (`idChauffeur`='" + Chauffeur.idChauffeur + "')";

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

        int IntfDalChauffeur.isChauffeur(crlChauffeur Chauffeur)
        {
            #region declaration
            int isChauffeur = 0;
            #endregion

            #region implementation
            if (Chauffeur != null)
            {
                this.strCommande = "SELECT * FROM `chauffeur` WHERE (`cinChauffeur`='" + Chauffeur.cinChauffeur + "' AND";
                this.strCommande += " `idChauffeur` <> '" + Chauffeur.idChauffeur + "')";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (Chauffeur.cinChauffeur.Trim().ToLower().Equals(this.reader["cinChauffeur"].ToString().Trim().ToLower()))
                            {
                                isChauffeur = 1;
                                break;
                            }
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isChauffeur;
        }

        string IntfDalChauffeur.isChauffeurStr(crlChauffeur Chauffeur)
        {
            #region declaration
            string idChauffeur = "";
            #endregion

            #region implementation
            if (Chauffeur != null)
            {
                this.strCommande = "SELECT * FROM `chauffeur` WHERE (`cinChauffeur`='" + Chauffeur.cinChauffeur + "' AND";
                this.strCommande += " `idChauffeur` <> '" + Chauffeur.idChauffeur + "')";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (Chauffeur.cinChauffeur.Trim().ToLower().Equals(this.reader["cinChauffeur"].ToString().Trim().ToLower()))
                            {
                                idChauffeur = this.reader["idChauffeur"].ToString();
                                break;
                            }
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return idChauffeur;
        }

        crlChauffeur IntfDalChauffeur.selectChauffeur(string idChauffeur)
        {
            #region declaration
            crlChauffeur Chauffeur = null;
            #endregion

            #region implementation
            if (idChauffeur != "")
            {
                this.strCommande = "SELECT * FROM `chauffeur` WHERE (`idChauffeur`='" + idChauffeur + "')";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        Chauffeur = new crlChauffeur();
                        if (this.reader.Read())
                        {
                            Chauffeur.idChauffeur = reader["idChauffeur"].ToString();
                            Chauffeur.nomChauffeur = reader["nomChauffeur"].ToString();
                            Chauffeur.prenomChauffeur = reader["prenomChauffeur"].ToString();
                            Chauffeur.cinChauffeur = reader["cinChauffeur"].ToString();
                            Chauffeur.adresseChauffeur = reader["adresseChauffeur"].ToString();
                            Chauffeur.telephoneChauffeur = reader["telephoneChauffeur"].ToString();
                            Chauffeur.telephoneMobileChauffeur = reader["telephoneMobileChauffeur"].ToString();
                            Chauffeur.ImageChauffeur = reader["imageChauffeur"].ToString();
                            Chauffeur.NumCooperative = reader["numCooperative"].ToString();
                            Chauffeur.SituationFamilialeChauffeur = this.reader["situationFamilialeChauffeur"].ToString();
                            try
                            {
                                Chauffeur.DateNaissanceChauffeur = Convert.ToDateTime(this.reader["dateNaissanceChauffeur"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            Chauffeur.LieuNaissanceChauffeur = this.reader["lieuNaissanceChauffeur"].ToString();
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return Chauffeur;
        }

        void IntfDalChauffeur.loadDdlTri(DropDownList ddlTri)
        {
            ddlTri.Items.Clear();
            ddlTri.Items.Add(new ListItem("N°", "chauffeur.idChauffeur"));
            ddlTri.Items.Add(new ListItem("Nom", "nomChauffeur"));
            ddlTri.Items.Add(new ListItem("Prénom", "prenomChauffeur"));
            //ddlTri.Items.Add(new ListItem("Adresse", "adresseChauffeur"));
            ddlTri.Items.Add(new ListItem("Téléphone", "telephoneChauffeur"));
            ddlTri.Items.Add(new ListItem("Mobile", "telephoneMobileChauffeur"));
        }

        string IntfDalChauffeur.getIdChauffeur(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string idChauffeur = "00001";
            string[] tempIdChauffeur = null;
            string strDate = "CF" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT chauffeur.idChauffeur AS maxNum FROM chauffeur";
            this.strCommande += " WHERE chauffeur.idChauffeur LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempIdChauffeur = reader["maxNum"].ToString().ToString().Split('/');
                        idChauffeur = tempIdChauffeur[tempIdChauffeur.Length - 1];
                    }
                    numTemp = double.Parse(idChauffeur) + 1;
                    if (numTemp < 10)
                        idChauffeur = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        idChauffeur = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        idChauffeur = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        idChauffeur = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        idChauffeur = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            idChauffeur = strDate + "/" + sigleAgence + "/" + idChauffeur;
            #endregion

            return idChauffeur;
        }

        #endregion

        #region insert to grid
        void IntfDalChauffeur.insertToGridChauffeur(GridView gridView, string param, string paramLike, string valueLike, string numVehicule)
        {
            #region declaration
            IntfDalChauffeur serviceChauffeur = new ImplDalChauffeur();
            #endregion

            #region implementation
            this.strCommande = "SELECT chauffeur.idChauffeur, chauffeur.nomChauffeur, chauffeur.prenomChauffeur,";
            this.strCommande += " chauffeur.cinChauffeur, chauffeur.adresseChauffeur, chauffeur.telephoneChauffeur,";
            this.strCommande += " chauffeur.telephoneMobileChauffeur FROM chauffeur";
            this.strCommande += " Left Join assovehiculechauffeur ON assovehiculechauffeur.idChauffeur = chauffeur.idChauffeur";
            this.strCommande += " WHERE assovehiculechauffeur.numVehicule LIKE  '%" + numVehicule + "%' AND";
            this.strCommande += " " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param + " ASC";

            gridView.DataSource = serviceChauffeur.getDataTableChauffeur(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalChauffeur.getDataTableChauffeur(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("idChauffeur", typeof(string));
            dataTable.Columns.Add("nomChauffeur", typeof(string));
            dataTable.Columns.Add("prenomChauffeur", typeof(string));
            dataTable.Columns.Add("cinChauffeur", typeof(string));
            dataTable.Columns.Add("adresseChauffeur", typeof(string));
            dataTable.Columns.Add("telephoneChauffeur", typeof(string));
            dataTable.Columns.Add("telephoneMobileChauffeur", typeof(string));
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

                        dr["idChauffeur"] = reader["idChauffeur"].ToString();
                        dr["nomChauffeur"] = reader["nomChauffeur"].ToString();
                        dr["prenomChauffeur"] = reader["prenomChauffeur"].ToString();
                        dr["cinChauffeur"] = reader["cinChauffeur"].ToString();
                        dr["adresseChauffeur"] = reader["adresseChauffeur"].ToString();
                        dr["telephoneChauffeur"] = reader["telephoneChauffeur"].ToString();
                        dr["telephoneMobileChauffeur"] = reader["telephoneMobileChauffeur"].ToString();

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }
        
        void IntfDalChauffeur.insertToGridChauffeurAll(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalChauffeur serviceChauffeur = new ImplDalChauffeur();
            #endregion

            #region implementation
            this.strCommande = "SELECT chauffeur.idChauffeur, chauffeur.nomChauffeur, chauffeur.prenomChauffeur,";
            this.strCommande += " chauffeur.cinChauffeur, chauffeur.adresseChauffeur, chauffeur.telephoneChauffeur,";
            this.strCommande += " chauffeur.telephoneMobileChauffeur FROM chauffeur";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param + " DESC";

            gridView.DataSource = serviceChauffeur.getDataTableChauffeurAll(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalChauffeur.getDataTableChauffeurAll(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("idChauffeur", typeof(string));
            dataTable.Columns.Add("nomChauffeur", typeof(string));
            dataTable.Columns.Add("prenomChauffeur", typeof(string));
            dataTable.Columns.Add("cinChauffeur", typeof(string));
            dataTable.Columns.Add("adresseChauffeur", typeof(string));
            dataTable.Columns.Add("telephoneChauffeur", typeof(string));
            dataTable.Columns.Add("telephoneMobileChauffeur", typeof(string));
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

                        dr["idChauffeur"] = reader["idChauffeur"].ToString();
                        dr["nomChauffeur"] = reader["nomChauffeur"].ToString();
                        dr["prenomChauffeur"] = reader["prenomChauffeur"].ToString();
                        dr["cinChauffeur"] = reader["cinChauffeur"].ToString();
                        dr["adresseChauffeur"] = reader["adresseChauffeur"].ToString();
                        dr["telephoneChauffeur"] = reader["telephoneChauffeur"].ToString();
                        dr["telephoneMobileChauffeur"] = reader["telephoneMobileChauffeur"].ToString();

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }

        void IntfDalChauffeur.insertToGridChauffeurListeNoire(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalChauffeur serviceChauffeur = new ImplDalChauffeur();
            #endregion

            #region implementation
            this.strCommande = "SELECT chauffeur.idChauffeur, chauffeur.nomChauffeur, chauffeur.prenomChauffeur,";
            this.strCommande += " chauffeur.cinChauffeur, chauffeur.adresseChauffeur, chauffeur.telephoneChauffeur,";
            this.strCommande += " chauffeur.telephoneMobileChauffeur, chauffeur.imageChauffeur FROM chauffeur";
            this.strCommande += " Inner Join observationchauffeur ON observationchauffeur.idChauffeur = chauffeur.idChauffeur";
            this.strCommande += " WHERE observationchauffeur.isListeNoire = '2' AND";
            this.strCommande += " " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " GROUP BY chauffeur.idChauffeur";
            this.strCommande += " ORDER BY " + param + " DESC";

            gridView.DataSource = serviceChauffeur.getDataTableChauffeurAll(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalChauffeur.getDataTableChauffeurListeNoire(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("idChauffeur", typeof(string));
            dataTable.Columns.Add("nomChauffeur", typeof(string));
            dataTable.Columns.Add("prenomChauffeur", typeof(string));
            dataTable.Columns.Add("cinChauffeur", typeof(string));
            dataTable.Columns.Add("adresseChauffeur", typeof(string));
            dataTable.Columns.Add("telephoneChauffeur", typeof(string));
            dataTable.Columns.Add("telephoneMobileChauffeur", typeof(string));
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

                        dr["idChauffeur"] = reader["idChauffeur"].ToString();
                        dr["nomChauffeur"] = reader["nomChauffeur"].ToString();
                        dr["prenomChauffeur"] = reader["prenomChauffeur"].ToString();
                        dr["cinChauffeur"] = reader["cinChauffeur"].ToString();
                        dr["adresseChauffeur"] = reader["adresseChauffeur"].ToString();
                        dr["telephoneChauffeur"] = reader["telephoneChauffeur"].ToString();
                        dr["telephoneMobileChauffeur"] = reader["telephoneMobileChauffeur"].ToString();

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