using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.dal.intf;
using MySql.Data.MySqlClient;
using arch.crl;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;


namespace arch.dal.impl
{
    /// <summary>
    /// Implementation du service organisme
    /// </summary>
    public class ImplDalOrganisme : IntfDalOrganisme
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalOrganisme(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalOrganisme()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion

        #region IntfDalOrganisme Members
        crlOrganisme IntfDalOrganisme.selectOrganisme(string numOrganisme)
        {
            #region declaration
            crlOrganisme organisme = null;
            IntfDalIndividu serviceIndividu = new ImplDalIndividu();
            #endregion

            #region implementation
            if (numOrganisme != "")
            {
                this.strCommande = "SELECT * FROM organisme WHERE `numOrganisme`='" + numOrganisme + "'";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            organisme = new crlOrganisme();
                            organisme.AdresseOrganisme = this.reader["adresseOrganisme"].ToString();
                            organisme.MailOrganisme = this.reader["mailOrganisme"].ToString();
                            organisme.NomOrganisme = this.reader["nomOrganisme"].ToString();
                            organisme.NumOrganisme = this.reader["numOrganisme"].ToString();
                            organisme.TelephoneFixeOrganisme = this.reader["telephoneFixeOrganisme"].ToString();
                            organisme.TelephoneMobileOrganisme = this.reader["telephoneMobileOrganisme"].ToString();
                            organisme.NumQuartier = this.reader["numQuartier"].ToString();
                            organisme.NumIndividuResponsable = this.reader["numIndividuResponsable"].ToString();
                            try
                            {
                                organisme.IsCheque = int.Parse(this.reader["isCheque"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                organisme.IsBonCommande = int.Parse(this.reader["isBonCommande"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (organisme != null)
                {
                    if (organisme.NumIndividuResponsable != "")
                    {
                        organisme.individuResponsable = serviceIndividu.selectIndividu(organisme.NumIndividuResponsable);
                    }
                }
            }
            #endregion

            return organisme;
        }

        bool IntfDalOrganisme.updateOrganisme(crlOrganisme organisme)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            #endregion

            #region implementation
            if (organisme != null)
            {
                this.strCommande = "UPDATE `organisme` SET `adresseOrganisme`='" + organisme.AdresseOrganisme + "',";
                this.strCommande += " `mailOrganisme`='" + organisme.MailOrganisme + "',";
                this.strCommande += " `nomOrganisme`='" + organisme.NomOrganisme + "',";
                this.strCommande += " `telephoneFixeOrganisme`='" + organisme.TelephoneFixeOrganisme + "',";
                this.strCommande += " `telephoneMobileOrganisme`='" + organisme.TelephoneMobileOrganisme + "',";
                this.strCommande += " `numQuartier`='" + organisme.NumQuartier + "',";
                this.strCommande += " `numIndividuResponsable`='" + organisme.NumIndividuResponsable + "'";
                if (organisme.IsCheque >= 0)
                {
                    this.strCommande += " ,`isCheque`='" + organisme.IsCheque + "'";
                }
                if (organisme.IsBonCommande >= 0)
                {
                    this.strCommande += " ,`isBonCommande`='" + organisme.IsBonCommande + "'";
                }
                this.strCommande += " WHERE `numOrganisme`='" + organisme.NumOrganisme + "'";

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

        string IntfDalOrganisme.insertOrganisme(crlOrganisme organisme, string sigleAgence)
        {
            #region declaration
            string numSociete = "";
            int nbInsert = 0;
            IntfDalOrganisme serviceOrganisme = new ImplDalOrganisme();
            #endregion

            #region impelementation
            if (organisme != null && sigleAgence != "")
            {
                organisme.NumOrganisme = serviceOrganisme.getNumOrganisme(sigleAgence);
                this.strCommande = "INSERT INTO `organisme` (`numOrganisme`,`numQuartier`,";
                this.strCommande += " `nomOrganisme`,`adresseOrganisme`,`telephoneFixeOrganisme`,";
                this.strCommande += " `telephoneMobileOrganisme`,`mailOrganisme`,`numIndividuResponsable`,";
                this.strCommande += " `isCheque`,`isBonCommande`) VALUES";
                this.strCommande += " ('" + organisme.NumOrganisme + "','" + organisme.NumQuartier + "',";
                this.strCommande += " '" + organisme.NomOrganisme + "','" + organisme.AdresseOrganisme + "','" + organisme.TelephoneFixeOrganisme + "',";
                this.strCommande += " '" + organisme.TelephoneMobileOrganisme + "','" + organisme.MailOrganisme + "',";
                this.strCommande += " '" + organisme.NumIndividuResponsable + "','" + organisme.IsCheque + "',";
                this.strCommande += " '" + organisme.IsBonCommande + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    numSociete = organisme.NumOrganisme;
                }
                this.serviceConnectBase.closeConnection();

            }
            #endregion

            return numSociete;
        }

        string IntfDalOrganisme.isOrganisme(crlOrganisme organisme)
        {
            #region declaration
            string numOrganisme = "";
            #endregion

            #region implementation
            if (organisme != null) 
            {
                this.strCommande = "SELECT * FROM `organisme` WHERE (`nomOrganisme`='" + organisme.NomOrganisme + "' AND";
                this.strCommande += " `numIndividuResponsable`='" + organisme.NumIndividuResponsable + "' AND";
                this.strCommande += " `numOrganisme`<>'" + organisme.NumOrganisme + "')";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read())
                        {
                            numOrganisme = this.reader["numOrganisme"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numOrganisme;
        }

        string IntfDalOrganisme.getNumOrganisme(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numOrganisme = "00001";
            string[] tempNumOrganisme = null;
            string strDate = "OR" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT organisme.numOrganisme AS maxNum FROM organisme";
            this.strCommande += " WHERE organisme.numOrganisme LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumOrganisme = reader["maxNum"].ToString().ToString().Split('/');
                        numOrganisme = tempNumOrganisme[tempNumOrganisme.Length - 1];
                    }
                    numTemp = double.Parse(numOrganisme) + 1;
                    if (numTemp < 10)
                        numOrganisme = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numOrganisme = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numOrganisme = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numOrganisme = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numOrganisme = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numOrganisme = strDate + "/" + sigleAgence + "/" + numOrganisme;
            #endregion

            return numOrganisme;
        }

        int IntfDalOrganisme.isBonCommande(string numOrganisme)
        {
            #region declaration
            int isBonCommande = 0;
            #endregion

            #region implementation
            if (numOrganisme != "")
            {
                this.strCommande = "SELECT Count(bondecommande.numBonDeCommande) AS nbCommande FROM bondecommande";
                this.strCommande += " Left Join assocrecuencaisserproformabondecommande ON assocrecuencaisserproformabondecommande.numBonDeCommande = bondecommande.numBonDeCommande";
                this.strCommande += " Inner Join proforma ON proforma.numProforma = bondecommande.numProforma";
                this.strCommande += " Inner Join organisme ON organisme.numOrganisme = proforma.numOrganisme";
                this.strCommande += " WHERE assocrecuencaisserproformabondecommande.numBonDeCommande IS NULL AND";
                this.strCommande += " organisme.numOrganisme = '" + numOrganisme + "'";

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
        void IntfDalOrganisme.insertToGridOrganisme(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalOrganisme serviceOrganisme = new ImplDalOrganisme();
            #endregion

            #region implementation

            this.strCommande = "SELECT organisme.numOrganisme, organisme.nomOrganisme, organisme.adresseOrganisme,";
            this.strCommande += " organisme.numQuartier, organisme.mailOrganisme, organisme.telephoneFixeOrganisme,";
            this.strCommande += " organisme.telephoneMobileOrganisme, organisme.numIndividuResponsable,";
            this.strCommande += " quartier.quartier, individu.numIndividu, individu.civiliteIndividu, individu.nomIndividu,";
            this.strCommande += " individu.prenomIndividu, individu.cinIndividu, individu.adresse, individu.profession,";
            this.strCommande += " individu.telephoneFixeIndividu, individu.telephoneMobileIndividu,";
            this.strCommande += " individu.dateNaissanceIndividu, individu.lieuNaissanceIndividu, individu.mailIndividu,";
            this.strCommande += " individu.numQuartier FROM organisme";
            this.strCommande += " Inner Join quartier ON quartier.numQuartier = organisme.numQuartier";
            this.strCommande += " Inner Join individu ON individu.numIndividu = organisme.numIndividuResponsable";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceOrganisme.getDataTableOrganisme(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalOrganisme.getDataTableOrganisme(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();

            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numOrganisme", typeof(string));
            dataTable.Columns.Add("nomOrganisme", typeof(string));
            dataTable.Columns.Add("adresseOrganisme", typeof(string));
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

                        dr["numOrganisme"] = reader["numOrganisme"].ToString();
                        dr["nomOrganisme"] = reader["nomOrganisme"].ToString();
                        dr["adresseOrganisme"] = reader["adresseOrganisme"].ToString() + " " + reader["quartier"].ToString();

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


        #region IntfDalOrganisme Members


        string IntfDalOrganisme.insertOrganisme(crlOrganisme organisme, string sigleAgence, HtmlGenericControl divIndication)
        {
            #region declaration
            string numOrganisme = "";
            string strIndication = "";
            IntfDalIndividu serviceIndividu = new ImplDalIndividu();
            IntfDalOrganisme serviceOrganisme = new ImplDalOrganisme();
            #endregion

            #region implementation
            divIndication.Style.Add("font-size", "14px");
            divIndication.Style.Add("color", "Red");
            if (organisme != null)
            {
                if (organisme.individuResponsable != null)
                {
                    organisme.NumIndividuResponsable = serviceIndividu.insertIndividu(organisme.individuResponsable, sigleAgence, divIndication);
                    if (organisme.NumIndividuResponsable != "")
                    {
                        organisme.NumOrganisme = serviceOrganisme.isOrganisme(organisme);
                        if (organisme.NomOrganisme.Equals(""))
                        {
                            organisme.NumOrganisme = serviceOrganisme.insertOrganisme(organisme, sigleAgence);
                            if (organisme.NumOrganisme.Equals(""))
                            {
                                strIndication = "Une erreur ce produit durant l'enregistrement!";
                                divIndication.InnerHtml = "<p>" + strIndication + "</p>";
                            }
                            else
                            {
                                numOrganisme = organisme.NumOrganisme;
                            }
                        }
                        else
                        {
                            if (serviceOrganisme.updateOrganisme(organisme))
                            {
                                numOrganisme = organisme.NumOrganisme;
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

            return numOrganisme;
        }

        bool IntfDalOrganisme.updateOrganisme(crlOrganisme organisme, HtmlGenericControl divIndication, string numIndividu, string sigleAgence)
        {
            #region declaration
            bool isUpdate = false;
            string numOrganisme = "";
            string strIndication = "";
            IntfDalIndividu serviceIndividu = new ImplDalIndividu();
            IntfDalOrganisme serviceOrganisme = new ImplDalOrganisme();
            #endregion

            #region implementation
            divIndication.Style.Add("font-size", "14px");
            divIndication.Style.Add("color", "Red");
            if (organisme != null)
            {
                if (organisme.individuResponsable != null)
                {
                    if (numIndividu.Equals(""))
                    {
                        organisme.NumIndividuResponsable = serviceIndividu.insertIndividu(organisme.individuResponsable, sigleAgence, divIndication);
                        if (organisme.NumIndividuResponsable != "")
                        {
                            numOrganisme = serviceOrganisme.isOrganisme(organisme);
                            if (numOrganisme.Equals(""))
                            {
                                isUpdate = serviceOrganisme.updateOrganisme(organisme);
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
                        if (serviceIndividu.updateIndividu(organisme.individuResponsable))
                        {
                            numOrganisme = serviceOrganisme.isOrganisme(organisme);
                            if (numOrganisme.Equals(""))
                            {
                                isUpdate = serviceOrganisme.updateOrganisme(organisme);
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