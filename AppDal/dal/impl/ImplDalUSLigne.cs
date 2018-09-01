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
    /// Implementation du service Ligne
    /// </summary>
    public class ImplDalUSLigne : IntfDalUSLigne
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalUSLigne(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalUSLigne()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion

        #region IntfDalUSLigne Members

        string IntfDalUSLigne.insertUSLigne(crlUSLigne ligne, string sigleAgence)
        {
            #region declaration
            string numLigne = "";
            IntfDalUSLigne serviceUSLigne = new ImplDalUSLigne();
            int nbInsert = 0;
            #endregion

            #region implemenation
            if (ligne != null && sigleAgence != "")
            {
                ligne.NumLigne = serviceUSLigne.getNumUSLigne(sigleAgence);
                this.strCommande = "INSERT INTO `usligne` (`numLigne`,`numCooperative`,`nomLigne`,`descriptionLigne`,`zone`,`numArretD`,`numArretF`,`numAxe`)";
                this.strCommande += " VALUES ('" + ligne.NumLigne + "','" + ligne.NumCooperative + "','" + ligne.NomLigne + "',";
                this.strCommande += " '" + ligne.DescriptionLigne + "','" + ligne.Zone + "','" + ligne.NumArretD + "','" + ligne.NumArretF + "','" + ligne.NumAxe + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    numLigne = ligne.NumLigne;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numLigne;
        }

        bool IntfDalUSLigne.updateUSLigne(crlUSLigne ligne)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            #endregion

            #region implementation
            if (ligne != null)
            {
                this.strCommande = "UPDATE `usligne` SET `numCooperative`='" + ligne.NumCooperative + "',";
                this.strCommande += " `nomLigne`='" + ligne.NomLigne + "',";
                this.strCommande += " `descriptionLigne`='" + ligne.DescriptionLigne + "',`zone`='" + ligne.Zone + "',";
                this.strCommande += " `numArretD`='" + ligne.NumArretD + "',`numArretF`='" + ligne.NumArretF + "',";
                this.strCommande += " `numAxe`='" + ligne.NumAxe + "' WHERE";
                this.strCommande += " `numLigne`='" + ligne.NumLigne + "'";

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

        crlUSLigne IntfDalUSLigne.selectUSLigne(string numLigne)
        {
            #region declaration
            crlUSLigne ligne = null;
            IntfDalUSArret serviceUSArret = new ImplDalUSArret();
            #endregion

            #region implementation
            if (numLigne != "")
            {
                this.strCommande = "SELECT * FROM `usligne` WHERE (`numLigne`='" + numLigne + "')";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            ligne = new crlUSLigne();
                            ligne.NumLigne = this.reader["numLigne"].ToString();
                            ligne.DescriptionLigne = this.reader["descriptionLigne"].ToString();
                            ligne.NumCooperative = this.reader["numCooperative"].ToString();
                            ligne.NomLigne = this.reader["nomLigne"].ToString();
                            ligne.Zone = this.reader["zone"].ToString();
                            ligne.NumArretD = this.reader["numArretD"].ToString();
                            ligne.NumArretF = this.reader["numArretF"].ToString();
                            ligne.NumAxe = this.reader["numAxe"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (ligne != null)
                {
                    ligne.arretD = serviceUSArret.selectUSArret(ligne.NumArretD);
                    ligne.arretF = serviceUSArret.selectUSArret(ligne.NumArretF);
                }
            }
            #endregion

            return ligne;
        }

        string IntfDalUSLigne.getNumUSLigne(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numLigne = "00001";
            string[] tempNumLigne = null;
            string strDate = "LG" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT usligne.numLigne AS maxNum FROM usligne";
            this.strCommande += " WHERE usligne.numLigne LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumLigne = reader["maxNum"].ToString().ToString().Split('/');
                        numLigne = tempNumLigne[tempNumLigne.Length - 1];
                    }
                    numTemp = double.Parse(numLigne) + 1;
                    if (numTemp < 10)
                        numLigne = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numLigne = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numLigne = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numLigne = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numLigne = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numLigne = strDate + "/" + sigleAgence + "/" + numLigne;
            #endregion

            return numLigne;
        }

        void IntfDalUSLigne.loadDdlLigne(DropDownList ddl)
        {
            #region implementation
            if (ddl != null)
            {
                ddl.Items.Clear();
                ddl.Items.Add("");

                this.strCommande = "SELECT * FROM `usligne`";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            ddl.Items.Add(new ListItem(this.reader["nomLigne"].ToString(), this.reader["numLigne"].ToString()));
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }

        /*bool IntfDalUSLigne.insertUSAssocLicenceLigne(string numLigne, string numLicence)
        {
            #region declaration
            bool isInsert = false;
            int nbInsert = 0;
            #endregion

            #region implementation
            if (numLicence != "" && numLigne != "")
            {
                this.strCommande = "INSERT INTO `usassoclicenceligne` (`numLigne`,`numLicence`)";
                this.strCommande += " VALUES ('" + numLigne + "','" + numLicence + "')";

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

        bool IntfDalUSLigne.deleteUSAssocLicenceLigne(string numLigne, string numLicence)
        {
            #region declaration
            bool isDelete = false;
            int nbDelete = 0;
            #endregion

            #region implementation
            if (numLicence != "" && numLigne != "")
            {
                this.strCommande = "DELETE FROM `usassoclicenceligne` WHERE";
                this.strCommande += " `numLigne`='" + numLigne + "' AND `numLicence`='" + numLicence + "'";

                this.serviceConnectBase.openConnection();
                nbDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nbDelete == 1)
                {
                    isDelete = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isDelete;
        }*/


        bool IntfDalUSLigne.insertUSAssocArretLigne(string numLigne, string numArret)
        {
            #region declaration
            bool isInsert = false;
            int nbInsert = 0;
            #endregion

            #region implementation
            if (numArret != "" && numLigne != "")
            {
                this.strCommande = "INSERT INTO `usassoclignearret` (`numLigne`,`numArret`)";
                this.strCommande += " VALUES ('" + numLigne + "','" + numArret + "')";

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

        bool IntfDalUSLigne.deleteUSAssocArretLigne(string numLigne, string numArret)
        {
            #region declaration
            bool isDelete = false;
            int nbDelete = 0;
            #endregion

            #region implementation
            if (numArret != "" && numLigne != "")
            {
                this.strCommande = "DELETE FROM `usassoclignearret` WHERE";
                this.strCommande += " `numLigne`='" + numLigne + "' AND `numArret`='" + numArret + "'";

                this.serviceConnectBase.openConnection();
                nbDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nbDelete == 1)
                {
                    isDelete = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isDelete;
        }

        #endregion


        void IntfDalUSLigne.insertToGridLigne(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalUSLigne serviceUSLigne = new ImplDalUSLigne();
            #endregion

            #region implementation

            this.strCommande = "SELECT usligne.numLigne, usligne.numCooperative, usligne.nomLigne,";
            this.strCommande += " usligne.descriptionLigne, usligne.numArretD, usligne.numArretF,";
            this.strCommande += " usligne.zone, cooperative.nomCooperative FROM usligne";
            this.strCommande += " Left Join usarret ON usarret.numArret = usligne.numArretF OR usarret.numArret = usligne.numArretD";
            this.strCommande += " Left Join uslieu ON uslieu.numLieu = usarret.numLieu";
            this.strCommande += " Inner Join cooperative ON cooperative.numCooperative = usligne.numCooperative";
            this.strCommande += " WHERE " + paramLike + " LIKE  '%" + valueLike + "%'";
            this.strCommande += " GROUP BY usligne.numLigne";
            this.strCommande += " ORDER BY " + param;


            gridView.DataSource = serviceUSLigne.getDataTableLigne(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalUSLigne.getDataTableLigne(string strRqst)
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
            dataTable.Columns.Add("numLigne", typeof(string));
            dataTable.Columns.Add("nomLigne", typeof(string));
            dataTable.Columns.Add("nomCooperative", typeof(string));
            dataTable.Columns.Add("zone", typeof(string));
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

                        dr["numLigne"] = reader["numLigne"].ToString();
                        dr["nomLigne"] = reader["nomLigne"].ToString();
                        dr["nomCooperative"] = reader["nomCooperative"].ToString();
                        dr["zone"] = reader["zone"].ToString();

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