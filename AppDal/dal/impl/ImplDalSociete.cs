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
    /// Implementaion du service societe
    /// </summary>
    public class ImplDalSociete : IntfDalSociete
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalSociete(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalSociete()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion

        #region IntfDalSociete Members

        string IntfDalSociete.insertSociete(crlSociete societe, string sigleAgence)
        {
            #region declaration
            string numSociete = "";
            int nbInsert = 0;
            IntfDalSociete serviceSociete = new ImplDalSociete();
            #endregion

            #region impelementation
            if (societe != null && sigleAgence != "") 
            {
                societe.NumSociete = serviceSociete.getNumSociete(sigleAgence);
                this.strCommande = "INSERT INTO `societe` (`numSociete`,`nomSociete`,`adresseSociete`,`numQuartier`,";
                this.strCommande += " `telephoneFixeSociete`,`telephoneMobileSociete`,`mailSociete`,";
                this.strCommande += " `secteurActiviteSociete`,`isReductionUS`,`numIndividuResponsable`,";
                this.strCommande += " `isCheque`,`isBonCommande`) VALUES";
                this.strCommande += " ('" + societe.NumSociete + "','" + societe.NomSociete + "',";
                this.strCommande += " '" + societe.AdresseSociete + "','" + societe.NumQuartier + "',";
                this.strCommande += " '" + societe.TelephoneFixeSociete + "','" + societe.TelephoneMobileSociete + "',";
                this.strCommande += " '"  + societe.MailSociete + "','" + societe.SecteurActiviteSociete + "',";
                this.strCommande += " '" + societe.IsReductionUS + "','" + societe.NumIndividuResponsable + "',";
                this.strCommande += " '" + societe.IsCheque + "','" + societe.IsBonCommande + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1) 
                {
                    numSociete = societe.NumSociete;
                }
                this.serviceConnectBase.closeConnection();

            }
            #endregion

            return numSociete;
        }

        bool IntfDalSociete.updateSociete(crlSociete societe)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            #endregion

            #region implementation
            if (societe != null)
            {
                this.strCommande = "UPDATE `societe` SET `adresseSociete`='" + societe.AdresseSociete + "',";
                this.strCommande += " `numQuartier`='" + societe.NumQuartier + "',`mailSociete`='" + societe.MailSociete + "',";
                this.strCommande += " `nomSociete`='" + societe.NomSociete + "',";
                this.strCommande += " `secteurActiviteSociete`='" + societe.SecteurActiviteSociete + "',";
                this.strCommande += " `telephoneFixeSociete`='" + societe.TelephoneFixeSociete + "',";
                this.strCommande += " `telephoneMobileSociete`='" + societe.TelephoneMobileSociete + "',";
                this.strCommande += " `isReductionUS`='" + societe.IsReductionUS + "',";
                this.strCommande += " `numIndividuResponsable`='" + societe.NumIndividuResponsable + "'";
                if (societe.IsCheque >= 0)
                {
                    this.strCommande += " ,`isCheque`='" + societe.IsCheque + "'";
                }
                if (societe.IsBonCommande >= 0)
                {
                    this.strCommande += " ,`isBonCommande`='" + societe.IsBonCommande + "'";
                }
                this.strCommande += " WHERE `numSociete`='" + societe.NumSociete + "'";

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

        crlSociete IntfDalSociete.selectSociete(string numSociete)
        {
            #region declaration
            crlSociete societe = null;
            IntfDalIndividu serviceIndividu = new ImplDalIndividu();
            #endregion

            #region implementation
            if (numSociete != "") 
            {
                this.strCommande = "SELECT * FROM `societe` WHERE `numSociete`='" + numSociete + "'";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            societe = new crlSociete();
                            societe.AdresseSociete = this.reader["adresseSociete"].ToString();
                            societe.NumQuartier = this.reader["numQuartier"].ToString();
                            societe.MailSociete = this.reader["mailSociete"].ToString();
                            societe.NomSociete = this.reader["nomSociete"].ToString();
                            societe.NumSociete = this.reader["numSociete"].ToString();
                            societe.SecteurActiviteSociete = this.reader["secteurActiviteSociete"].ToString();
                            societe.TelephoneFixeSociete = this.reader["telephoneFixeSociete"].ToString();
                            societe.TelephoneMobileSociete = this.reader["telephoneMobileSociete"].ToString();
                            try
                            {
                                societe.IsReductionUS = int.Parse(this.reader["isReductionUS"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            societe.NumIndividuResponsable = this.reader["numIndividuResponsable"].ToString();
                            try
                            {
                                societe.IsCheque = int.Parse(this.reader["isCheque"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                societe.IsBonCommande = int.Parse(this.reader["isBonCommande"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (societe != null)
                {
                    if (societe.NumIndividuResponsable != "")
                    {
                        societe.individuResponsable = serviceIndividu.selectIndividu(societe.NumIndividuResponsable);
                    }
                }
            }
            #endregion

            return societe;
        }

        string IntfDalSociete.isSociete(crlSociete societe)
        {
            #region declaration
            string numSociete = "";
            #endregion

            #region implementation
            if (societe != null) 
            {
                strCommande = "SELECT * FROM `societe` WHERE (`nomSociete`='" + societe.NomSociete + "' AND";
                strCommande += " `numIndividuResponsable`='" + societe.NumIndividuResponsable + "' AND";
                this.strCommande += " `secteurActiviteSociete`='" + societe.SecteurActiviteSociete + "' AND";
                this.strCommande += " `numSociete`<>'" + societe.NumSociete + "')";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            numSociete = this.reader["numSociete"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numSociete;
        }

        string IntfDalSociete.getNumSociete(string sigleAgence)
        {
            
            #region declaration
            double numTemp = 0;
            string numSociete = "00001";
            string[] tempNumSociete = null;
            string strDate = "SO" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT societe.numSociete AS maxNum FROM societe";
            this.strCommande += " WHERE societe.numSociete LIKE '%" + sigleAgence + "%'";
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

        int IntfDalSociete.isBonCommande(string numSociete)
        {
            #region declaration
            int isBonCommande = 0;
            #endregion

            #region implementation
            if (numSociete != "")
            {
                this.strCommande = "SELECT Count(bondecommande.numBonDeCommande) AS nbCommande FROM bondecommande";
                this.strCommande += " Left Join assocrecuencaisserproformabondecommande ON assocrecuencaisserproformabondecommande.numBonDeCommande = bondecommande.numBonDeCommande";
                this.strCommande += " Inner Join proforma ON proforma.numProforma = bondecommande.numProforma";
                this.strCommande += " Inner Join societe ON societe.numSociete = proforma.numSociete";
                this.strCommande += " WHERE assocrecuencaisserproformabondecommande.numBonDeCommande IS NULL AND";
                this.strCommande += " societe.numSociete = '" + numSociete + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            try
                            {
                                isBonCommande = int.Parse(this.reader["nbCommande"].ToString());
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

            return isBonCommande;
        }
        #endregion

        #region insert to grid
        void IntfDalSociete.insertToGridSociete(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalSociete serviceSociete = new ImplDalSociete();
            #endregion

            #region implementation

            this.strCommande = "SELECT societe.numSociete, societe.nomSociete, societe.adresseSociete,";
            this.strCommande += " societe.numQuartier, societe.telephoneFixeSociete, societe.telephoneMobileSociete,";
            this.strCommande += " societe.mailSociete, societe.secteurActiviteSociete, societe.numIndividuResponsable,";
            this.strCommande += " societe.isReductionUS, individu.numIndividu, individu.civiliteIndividu,";
            this.strCommande += " individu.nomIndividu, individu.prenomIndividu, individu.cinIndividu,";
            this.strCommande += " individu.adresse, individu.profession, individu.telephoneFixeIndividu,";
            this.strCommande += " individu.telephoneMobileIndividu, individu.dateNaissanceIndividu,";
            this.strCommande += " individu.lieuNaissanceIndividu, individu.mailIndividu, individu.numQuartier,";
            this.strCommande += " quartier.quartier FROM societe";
            this.strCommande += " Inner Join individu ON individu.numIndividu = societe.numIndividuResponsable";
            this.strCommande += " Inner Join quartier ON quartier.numQuartier = societe.numQuartier";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceSociete.getDataTableSociete(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalSociete.getDataTableSociete(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numSociete", typeof(string));
            dataTable.Columns.Add("nomSociete", typeof(string));
            dataTable.Columns.Add("secteurActiviteSociete", typeof(string));
            dataTable.Columns.Add("adresseSociete", typeof(string));
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

                        dr["numSociete"] = reader["numSociete"].ToString();
                        dr["nomSociete"] = reader["nomSociete"].ToString();
                        dr["secteurActiviteSociete"] = reader["secteurActiviteSociete"].ToString();
                        dr["adresseSociete"] = reader["adresseSociete"].ToString() + " " + reader["quartier"].ToString();

                        dr["responsable"] = reader["prenomIndividu"].ToString() + " " + reader["nomIndividu"].ToString();

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

        void IntfDalSociete.insertToGridSocieteReduction(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalSociete serviceSociete = new ImplDalSociete();
            #endregion

            #region implementation

            this.strCommande = "SELECT societe.numSociete, societe.nomSociete, societe.adresseSociete,";
            this.strCommande += " societe.numQuartier, societe.telephoneFixeSociete, societe.telephoneMobileSociete,";
            this.strCommande += " societe.mailSociete, societe.secteurActiviteSociete, societe.numIndividuResponsable,";
            this.strCommande += " societe.isReductionUS, individu.numIndividu, individu.civiliteIndividu,";
            this.strCommande += " individu.nomIndividu, individu.prenomIndividu, individu.cinIndividu,";
            this.strCommande += " individu.adresse, individu.profession, individu.telephoneFixeIndividu,";
            this.strCommande += " individu.telephoneMobileIndividu, individu.dateNaissanceIndividu,";
            this.strCommande += " individu.lieuNaissanceIndividu, individu.mailIndividu, individu.numQuartier,";
            this.strCommande += " quartier.quartier FROM societe";
            this.strCommande += " Inner Join individu ON individu.numIndividu = societe.numIndividuResponsable";
            this.strCommande += " Inner Join quartier ON quartier.numQuartier = societe.numQuartier";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%' AND";
            this.strCommande += " societe.isReductionUS > '0'";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceSociete.getDataTableSocieteReduction(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalSociete.getDataTableSocieteReduction(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();

            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numSociete", typeof(string));
            dataTable.Columns.Add("nomSociete", typeof(string));
            dataTable.Columns.Add("secteurActiviteSociete", typeof(string));
            dataTable.Columns.Add("adresseSociete", typeof(string));
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

                        dr["numSociete"] = reader["numSociete"].ToString();
                        dr["nomSociete"] = reader["nomSociete"].ToString();
                        dr["secteurActiviteSociete"] = reader["secteurActiviteSociete"].ToString();
                        dr["adresseSociete"] = reader["adresseSociete"].ToString() + " " + reader["quartier"].ToString();

                        dr["responsable"] = reader["prenomIndividu"].ToString() + " " + reader["nomIndividu"].ToString();

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

        #region IntfDalSociete Members


        void IntfDalSociete.loadDddlSocieteReduction(DropDownList ddl)
        {
            #region implementation
            if (ddl != null) 
            {
                ddl.Items.Clear();
                ddl.Items.Add("");

                this.strCommande = "SELECT societe.nomSociete, societe.numSociete";
                this.strCommande += " FROM societe WHERE societe.isReductionUS = '1'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        while (this.reader.Read()) 
                        {
                            ddl.Items.Add(new ListItem(this.reader["nomSociete"].ToString(), this.reader["numSociete"].ToString()));
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }

        #endregion

        #region IntfDalSociete Members


        string IntfDalSociete.insertSociete(crlSociete societe, string sigleAgence, HtmlGenericControl divIndication)
        {
            #region declaration
            string numSociete = "";
            string strIndication = "";
            IntfDalIndividu serviceIndividu = new ImplDalIndividu();
            IntfDalSociete serviceSociete = new ImplDalSociete();
            #endregion

            #region implementation
            divIndication.Style.Add("font-size", "14px");
            divIndication.Style.Add("color", "Red");
            if (societe != null) 
            {
                if (societe.individuResponsable != null)
                {
                    societe.NumIndividuResponsable = serviceIndividu.insertIndividu(societe.individuResponsable, sigleAgence, divIndication);
                    if (societe.NumIndividuResponsable != "") 
                    {
                        societe.NumSociete = serviceSociete.isSociete(societe);
                        if (societe.NumSociete.Equals(""))
                        {
                            societe.NumSociete = serviceSociete.insertSociete(societe, sigleAgence);
                            if (societe.NumSociete.Equals(""))
                            {
                                strIndication = "Une erreur ce produit durant l'enregistrement!";
                                divIndication.InnerHtml = "<p>" + strIndication + "</p>";
                            }
                            else 
                            {
                                numSociete = societe.NumSociete;
                            }
                        }
                        else 
                        {
                            if (serviceSociete.updateSociete(societe))
                            {
                                numSociete = societe.NumSociete;
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

            return numSociete;
        }

        bool IntfDalSociete.updateSociete(crlSociete societe, HtmlGenericControl divIndication, string numIndividu, string sigleAgence)
        {
            #region declaration
            bool isUpdate = false;
            string numSociete = "";
            string strIndication = "";
            IntfDalIndividu serviceIndividu = new ImplDalIndividu();
            IntfDalSociete serviceSociete = new ImplDalSociete();
            #endregion

            #region implementation
            divIndication.Style.Add("font-size", "14px");
            divIndication.Style.Add("color", "Red");
            if (societe != null) 
            {
                if (societe.individuResponsable != null)
                {
                    if (numIndividu.Equals(""))
                    {
                        societe.NumIndividuResponsable = serviceIndividu.insertIndividu(societe.individuResponsable, sigleAgence, divIndication);
                        if (societe.NumIndividuResponsable != "")
                        {
                            numSociete = serviceSociete.isSociete(societe);
                            if (numSociete.Equals(""))
                            {
                                isUpdate = serviceSociete.updateSociete(societe);
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
                        if (serviceIndividu.updateIndividu(societe.individuResponsable))
                        {
                            numSociete = serviceSociete.isSociete(societe);
                            if (numSociete.Equals(""))
                            {
                                isUpdate = serviceSociete.updateSociete(societe);
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
