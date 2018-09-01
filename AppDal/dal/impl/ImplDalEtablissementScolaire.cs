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
    /// Summary description for ImplDalEtablissementScolaire
    /// </summary>
    public class ImplDalEtablissementScolaire : IntfDalEtablissementScolaire
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalEtablissementScolaire(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalEtablissementScolaire()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion

        #region IntfDalEtablissementScolaire Members

        string IntfDalEtablissementScolaire.insertEtablissementScolaire(crlEtablissementScolaire etablissementScolaire, string sigleAgence)
        {
            #region declaration
            string numEtablissementScolaire = "";
            int nbInsert = 0;
            IntfDalEtablissementScolaire serviceEtablissementScolaire = new ImplDalEtablissementScolaire();
            string numQuartier = "";
            #endregion

            #region implementation
            if (etablissementScolaire != null) 
            {
                if (etablissementScolaire.NumQuartier != "")
                {
                    numQuartier = "'" + etablissementScolaire.NumQuartier + "'";
                }
                etablissementScolaire.NumEtablissementScolaire = serviceEtablissementScolaire.getNumEtablissementScolaire(sigleAgence);

                this.strCommande = "INSERT INTO `etablissementscolaire` (`numEtablissementScolaire`,`etablissementScolaire`,";
                this.strCommande += "`typeEtablissementScolaire`,`adresseEtablissementScolaire`,`telephoneFixeEtablissementScolaire`,";
                this.strCommande += "`telephonePortableEtablissementScolaire`,`mailEtablissementScolaire`,`secteurEtablissementScolaire`,";
                this.strCommande += " `numQuartier`,`numIndividuResponsable`,`isCheque`,`isBonCommande`) VALUES";
                this.strCommande += "('" + etablissementScolaire.NumEtablissementScolaire + "','" + etablissementScolaire.EtablissementScolaire + "',";
                this.strCommande += "'" + etablissementScolaire.TypeEtablissementScolaire + "','" + etablissementScolaire.AdresseEtablissementScolaire + "',";
                this.strCommande += "'" + etablissementScolaire.TelephoneFixeEtablissementScolaire + "','" + etablissementScolaire.TelephonePortableEtablissementScolaire + "',";
                this.strCommande += "'" + etablissementScolaire.MailEtablissementScolaire + "','" + etablissementScolaire.SecteurEtablissementScolaire + "',";
                this.strCommande += " " + numQuartier + ", '" + etablissementScolaire.NumIndividuResponsable + "',";
                this.strCommande += " '" + etablissementScolaire.IsCheque + "','" + etablissementScolaire.IsBonCommande + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1) 
                {
                    numEtablissementScolaire = etablissementScolaire.NumEtablissementScolaire;
                }
                this.serviceConnectBase.closeConnection();

            }
            #endregion

            return numEtablissementScolaire;
        }

        bool IntfDalEtablissementScolaire.updateEtablissementScolaire(crlEtablissementScolaire etablissementScolaire)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            string numQuartier = "";
            #endregion

            #region implementation
            if (etablissementScolaire != null) 
            {
                if (etablissementScolaire.NumQuartier != "")
                {
                    numQuartier = "'" + etablissementScolaire.NumQuartier + "'";
                }
                this.strCommande = "UPDATE `etablissementscolaire` SET `etablissementScolaire`='" + etablissementScolaire.EtablissementScolaire + "',";
                this.strCommande += "`typeEtablissementScolaire`='" + etablissementScolaire.TypeEtablissementScolaire + "',";
                this.strCommande += "`adresseEtablissementScolaire`='" + etablissementScolaire.AdresseEtablissementScolaire + "',";
                this.strCommande += "`telephoneFixeEtablissementScolaire`='" + etablissementScolaire.TelephoneFixeEtablissementScolaire + "',";
                this.strCommande += "`telephonePortableEtablissementScolaire`='" + etablissementScolaire.TelephonePortableEtablissementScolaire + "',";
                this.strCommande += "`mailEtablissementScolaire`='" + etablissementScolaire.MailEtablissementScolaire + "',";
                this.strCommande += "`secteurEtablissementScolaire`='" + etablissementScolaire.SecteurEtablissementScolaire + "',";
                this.strCommande += "`numQuartier`=" + numQuartier + ",";
                this.strCommande += "`numIndividuResponsable`='" + etablissementScolaire.NumIndividuResponsable + "'";

                if (etablissementScolaire.IsCheque >= 0)
                {
                    this.strCommande += " ,`isCheque`='" + etablissementScolaire.IsCheque + "'";
                }
                if (etablissementScolaire.IsBonCommande >= 0)
                {
                    this.strCommande += " ,`isBonCommande`='" + etablissementScolaire.IsBonCommande + "'";
                }

                this.strCommande += " WHERE `numEtablissementScolaire`='" + etablissementScolaire.NumEtablissementScolaire + "'";

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

        crlEtablissementScolaire IntfDalEtablissementScolaire.selectEtablissementScolaire(string numEtablissementScolaire)
        {
            #region declaration
            crlEtablissementScolaire etablissementScolaire = null;
            IntfDalIndividu serviceIndividu = new ImplDalIndividu();
            #endregion

            #region implementation
            if (numEtablissementScolaire != "") 
            {
                this.strCommande = "SELECT * FROM `etablissementscolaire` WHERE `numEtablissementScolaire`='" + numEtablissementScolaire + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            etablissementScolaire = new crlEtablissementScolaire();
                            etablissementScolaire.AdresseEtablissementScolaire = this.reader["adresseEtablissementScolaire"].ToString();
                            etablissementScolaire.EtablissementScolaire = this.reader["etablissementScolaire"].ToString();
                            etablissementScolaire.MailEtablissementScolaire = this.reader["mailEtablissementScolaire"].ToString();
                            etablissementScolaire.NumEtablissementScolaire = this.reader["numEtablissementScolaire"].ToString();
                            etablissementScolaire.TelephoneFixeEtablissementScolaire = this.reader["telephoneFixeEtablissementScolaire"].ToString();
                            etablissementScolaire.TelephonePortableEtablissementScolaire = this.reader["telephonePortableEtablissementScolaire"].ToString();
                            etablissementScolaire.TypeEtablissementScolaire = this.reader["typeEtablissementScolaire"].ToString();
                            etablissementScolaire.SecteurEtablissementScolaire = this.reader["secteurEtablissementScolaire"].ToString();
                            etablissementScolaire.NumQuartier = this.reader["numQuartier"].ToString();
                            etablissementScolaire.NumIndividuResponsable = this.reader["numIndividuResponsable"].ToString();
                            try
                            {
                                etablissementScolaire.IsCheque = int.Parse(this.reader["isCheque"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                etablissementScolaire.IsBonCommande = int.Parse(this.reader["isBonCommande"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (etablissementScolaire != null)
                {
                    if (etablissementScolaire.NumIndividuResponsable != "")
                    {
                        etablissementScolaire.individuResponsable = serviceIndividu.selectIndividu(etablissementScolaire.NumIndividuResponsable);
                    }
                }
            }
            #endregion

            return etablissementScolaire;
        }

        string IntfDalEtablissementScolaire.isEtablissementScolaire(crlEtablissementScolaire etablissementScolaire)
        {
            #region declaration
            string numEtablissementScolaire = "";
            #endregion

            #region implementation
            if (etablissementScolaire != null) 
            {
                this.strCommande = "SELECT * FROM `etablissementscolaire` WHERE (`etablissementScolaire`='" + etablissementScolaire.EtablissementScolaire + "'";
                this.strCommande += " AND `typeEtablissementScolaire`='" + etablissementScolaire.TypeEtablissementScolaire + "' AND";
                this.strCommande += " `secteurEtablissementScolaire`='" + etablissementScolaire.SecteurEtablissementScolaire + "' AND";
                this.strCommande += " `numQuartier`='" + etablissementScolaire.NumQuartier + "' AND";
                this.strCommande += " `numEtablissementScolaire`<>'" + etablissementScolaire.NumEtablissementScolaire + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            numEtablissementScolaire = this.reader["numEtablissementScolaire"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numEtablissementScolaire;
        }

        string IntfDalEtablissementScolaire.getNumEtablissementScolaire(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numSociete = "00001";
            string[] tempNumSociete = null;
            string strDate = "ES" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT etablissementscolaire.numEtablissementScolaire AS maxNum FROM etablissementscolaire";
            this.strCommande += " WHERE etablissementscolaire.numEtablissementScolaire LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumSociete = reader["maxNum"].ToString().ToString().Split('/');
                        numSociete = tempNumSociete[tempNumSociete.Length - 1];
                    }
                    numTemp = double.Parse(numSociete) + 1;
                    if (numTemp < 10)
                        numSociete = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numSociete = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numSociete = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numSociete = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numSociete = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numSociete = strDate + "/" + sigleAgence + "/" + numSociete;
            #endregion

            return numSociete;
        }

        void IntfDalEtablissementScolaire.insertToGridEtablissementScolaire(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalEtablissementScolaire serviceEtablissementScolaire = new ImplDalEtablissementScolaire();
            #endregion

            #region implementation

            this.strCommande = "SELECT etablissementscolaire.numEtablissementScolaire, etablissementscolaire.etablissementScolaire,";
            this.strCommande += " etablissementscolaire.typeEtablissementScolaire, etablissementscolaire.secteurEtablissementScolaire,";
            this.strCommande += " etablissementscolaire.adresseEtablissementScolaire, etablissementscolaire.numQuartier,";
            this.strCommande += " etablissementscolaire.telephoneFixeEtablissementScolaire, etablissementscolaire.telephonePortableEtablissementScolaire,";
            this.strCommande += " etablissementscolaire.mailEtablissementScolaire, etablissementscolaire.numIndividuResponsable,";
            this.strCommande += " individu.numIndividu, individu.civiliteIndividu, individu.nomIndividu, individu.prenomIndividu,";
            this.strCommande += " individu.cinIndividu, individu.adresse, individu.profession, individu.telephoneFixeIndividu,";
            this.strCommande += " individu.telephoneMobileIndividu, individu.dateNaissanceIndividu, individu.lieuNaissanceIndividu,";
            this.strCommande += " quartier.quartier, quartier.numQuartier, quartier.numCommune, quartier.numArrondissement,";
            this.strCommande += " individu.mailIndividu, individu.numQuartier FROM etablissementscolaire";
            this.strCommande += " Left Join individu ON individu.numIndividu = etablissementscolaire.numIndividuResponsable";
            this.strCommande += " Left Join quartier ON quartier.numQuartier = etablissementscolaire.numQuartier";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceEtablissementScolaire.getDataTableEtablissementScolaire(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalEtablissementScolaire.getDataTableEtablissementScolaire(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();

            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numEtablissementScolaire", typeof(string));
            dataTable.Columns.Add("nomEtablissementScolaire", typeof(string));
            dataTable.Columns.Add("typeEtablissementScolaire", typeof(string));
            dataTable.Columns.Add("secteurEtablissementScolaire", typeof(string));
            dataTable.Columns.Add("adresseEtablissementScolaire", typeof(string));
            dataTable.Columns.Add("responsable", typeof(string));
            dataTable.Columns.Add("adresseResponsable", typeof(string));
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

                        dr["numEtablissementScolaire"] = reader["numEtablissementScolaire"].ToString();
                        dr["nomEtablissementScolaire"] = reader["etablissementScolaire"].ToString();
                        dr["typeEtablissementScolaire"] = reader["typeEtablissementScolaire"].ToString();
                        dr["secteurEtablissementScolaire"] = reader["secteurEtablissementScolaire"].ToString();
                        dr["adresseEtablissementScolaire"] = reader["adresseEtablissementScolaire"].ToString() + " " + reader["quartier"].ToString();

                        dr["responsable"] = reader["nomIndividu"].ToString() + " " + reader["prenomIndividu"].ToString();

                        dr["adresseResponsable"] = reader["adresse"].ToString();
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

        #region IntfDalEtablissementScolaire Members


        void IntfDalEtablissementScolaire.loadDddlTypeEtablissementScolaire(DropDownList ddl)
        {
            #region implementation
            if (ddl != null) 
            {
                ddl.Items.Clear();
                ddl.Items.Add("");
                ddl.Items.Add("privé");
                ddl.Items.Add("public");
            }
            #endregion
        }

        void IntfDalEtablissementScolaire.loadDddlEtablissementScolaire(DropDownList ddl)
        {
            #region implemenbtation
            if (ddl != null) 
            {
                ddl.Items.Clear();
                ddl.Items.Add("");

                this.strCommande = "SELECT etablissementscolaire.numEtablissementScolaire,";
                this.strCommande += "etablissementscolaire.etablissementScolaire FROM etablissementscolaire";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        while (this.reader.Read()) 
                        {
                            ddl.Items.Add(new ListItem(this.reader["etablissementScolaire"].ToString(), this.reader["numEtablissementScolaire"].ToString()));
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }

        #endregion

        #region IntfDalEtablissementScolaire Members


        string IntfDalEtablissementScolaire.insertEtablissementScolaire(crlEtablissementScolaire etablissementScolaire, string sigleAgence, HtmlGenericControl divIndication)
        {
            #region declaration
            string numEtablissementScolaire = "";
            string strIndication = "";
            IntfDalIndividu serviceIndividu = new ImplDalIndividu();
            IntfDalEtablissementScolaire serviceEtablissementScolaire = new ImplDalEtablissementScolaire();
            #endregion

            #region implementation
            divIndication.Style.Add("font-size", "14px");
            divIndication.Style.Add("color", "Red");
            if (etablissementScolaire != null)
            {
                if (etablissementScolaire.individuResponsable != null)
                {
                    etablissementScolaire.NumIndividuResponsable = serviceIndividu.insertIndividu(etablissementScolaire.individuResponsable, sigleAgence, divIndication);
                    if (etablissementScolaire.NumIndividuResponsable != "")
                    {
                        etablissementScolaire.NumEtablissementScolaire = serviceEtablissementScolaire.isEtablissementScolaire(etablissementScolaire);
                        if (etablissementScolaire.NumEtablissementScolaire.Equals(""))
                        {
                            etablissementScolaire.NumEtablissementScolaire = serviceEtablissementScolaire.insertEtablissementScolaire(etablissementScolaire, sigleAgence);
                            if (etablissementScolaire.NumEtablissementScolaire.Equals(""))
                            {
                                strIndication = "Une erreur ce produit durant l'enregistrement!";
                                divIndication.InnerHtml = "<p>" + strIndication + "</p>";
                            }
                            else
                            {
                                numEtablissementScolaire = etablissementScolaire.NumEtablissementScolaire;
                            }
                        }
                        else
                        {
                            if (serviceEtablissementScolaire.updateEtablissementScolaire(etablissementScolaire))
                            {
                                numEtablissementScolaire = etablissementScolaire.NumEtablissementScolaire;
                            }
                            else
                            {
                                strIndication = "Une erreur ce produit durant l'enregistrement!";
                                divIndication.InnerHtml = "<p>" + strIndication + "</p>";
                            }
                        }
                    }
                }
                else
                {
                    strIndication = "Une erreur ce produit durant l'enregistrement!";
                    divIndication.InnerHtml = "<p>" + strIndication + "</p>";
                }
            }
            #endregion

            return numEtablissementScolaire;
        }

        bool IntfDalEtablissementScolaire.updateEtablissementScolaire(crlEtablissementScolaire etablissementScolaire, HtmlGenericControl divIndication, string numIndividu, string sigleAgence)
        {
            #region declaration
            bool isUpdate = false;
            string numEtablissementScolaire = "";
            string strIndication = "";
            IntfDalIndividu serviceIndividu = new ImplDalIndividu();
            IntfDalEtablissementScolaire serviceEtablissementScolaire = new ImplDalEtablissementScolaire();
            #endregion

            #region implementation
            divIndication.Style.Add("font-size", "14px");
            divIndication.Style.Add("color", "Red");
            if (etablissementScolaire != null)
            {
                if (etablissementScolaire.individuResponsable != null)
                {
                    if (numIndividu.Equals(""))
                    {
                        etablissementScolaire.NumIndividuResponsable = serviceIndividu.insertIndividu(etablissementScolaire.individuResponsable, sigleAgence, divIndication);
                        if (etablissementScolaire.NumIndividuResponsable != "")
                        {
                            numEtablissementScolaire = serviceEtablissementScolaire.isEtablissementScolaire(etablissementScolaire);
                            if (numEtablissementScolaire.Equals(""))
                            {
                                isUpdate = serviceEtablissementScolaire.updateEtablissementScolaire(etablissementScolaire);
                                if (!isUpdate)
                                {
                                    strIndication = "Une erreur ce produit durant l'enregistrement!";
                                    divIndication.InnerHtml = "<p>" + strIndication + "</p>";
                                }
                            }
                            else
                            {
                                strIndication = "Information déjà enregistrer dans la base de données!";
                                divIndication.InnerHtml = "<p>" + strIndication + "</p>";
                            }
                        }
                    }
                    else
                    {
                        if (serviceIndividu.updateIndividu(etablissementScolaire.individuResponsable))
                        {
                            numEtablissementScolaire = serviceEtablissementScolaire.isEtablissementScolaire(etablissementScolaire);
                            if (numEtablissementScolaire.Equals(""))
                            {
                                isUpdate = serviceEtablissementScolaire.updateEtablissementScolaire(etablissementScolaire);
                                if (!isUpdate)
                                {
                                    strIndication = "Une erreur ce produit durant l'enregistrement!";
                                    divIndication.InnerHtml = "<p>" + strIndication + "</p>";
                                }
                            }
                            else
                            {
                                strIndication = "Information déjà enregistrer dans la base de données!";
                                divIndication.InnerHtml = "<p>" + strIndication + "</p>";
                            }
                        }
                        else
                        {
                            strIndication = "Une erreur ce produit durant la modification!";
                            divIndication.InnerHtml = "<p>" + strIndication + "</p>";
                        }
                    }
                }
                else
                {
                    strIndication = "Une erreur ce produit durant l'enregistrement!";
                    divIndication.InnerHtml = "<p>" + strIndication + "</p>";
                }
            }
            #endregion

            return isUpdate;
        }

        #endregion
    }
}
