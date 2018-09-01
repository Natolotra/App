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
    /// Implementation du service ImplDalIndividu
    /// </summary>
    public class ImplDalIndividu : IntfDalIndividu
    {
        #region declaration
        ImplDalConnectBase serviceConnection = null;
        IntfDalServiceRessource serviceRessource = null;
        MySqlDataReader reader = null;

        string strCommande = "";
        string strConnection = "";
        #endregion

        #region constructeur
        public ImplDalIndividu(string strConnection)
        {
            this.strConnection = strConnection;
            serviceConnection = new ImplDalConnectBase(strConnection);
        }
        public ImplDalIndividu()
        {
            this.serviceRessource = new ImplDalServiceRessource();
            this.strConnection = this.serviceRessource.getDefaultStrConnection();
            this.serviceConnection = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region methode
        crlIndividu IntfDalIndividu.selectIndividu(string numIndividu)
        {
            #region declaration
            crlIndividu Individu = null;

            IntfDalVille serviceVille = new ImplDalVille(); 
            #endregion

            #region implementation
            if (numIndividu != "")
            {
                this.strCommande = "SELECT * FROM `Individu` WHERE (`numIndividu`='" + numIndividu + "')";

                this.serviceConnection.openConnection();
                if (this.serviceConnection.IsConnection)
                {
                    this.reader = this.serviceConnection.select(this.strCommande);
                    if (this.reader != null)
                    {
                        if (this.reader.HasRows)
                        {
                            if (this.reader.Read())
                            {
                                Individu = new crlIndividu();
                                Individu.NumIndividu = this.reader["numIndividu"].ToString();
                                Individu.CiviliteIndividu = this.reader["civiliteIndividu"].ToString();
                                Individu.NomIndividu = this.reader["nomIndividu"].ToString();
                                Individu.PrenomIndividu = this.reader["prenomIndividu"].ToString();
                                Individu.CinIndividu = this.reader["cinIndividu"].ToString();
                                Individu.Adresse = this.reader["adresse"].ToString();
                                Individu.Profession = this.reader["profession"].ToString();
                                Individu.TelephoneFixeIndividu = this.reader["telephoneFixeIndividu"].ToString();
                                Individu.TelephoneMobileIndividu = this.reader["telephoneMobileIndividu"].ToString();
                                try
                                {
                                    Individu.DateNaissanceIndividu = Convert.ToDateTime(this.reader["dateNaissanceIndividu"].ToString());
                                }
                                catch (Exception)
                                {
                                }
                                Individu.LieuNaissanceIndividu = this.reader["lieuNaissanceIndividu"].ToString();
                                Individu.MailIndividu = this.reader["mailIndividu"].ToString();
                                Individu.NumQuartier = this.reader["numQuartier"].ToString();
                                try
                                {
                                    Individu.IsCheque = int.Parse(this.reader["isCheque"].ToString());
                                }
                                catch (Exception)
                                {
                                }
                                try
                                {
                                    Individu.IsBonCommande = int.Parse(this.reader["isBonCommande"].ToString());
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }
                        this.reader.Dispose();
                    }

                    while (this.serviceConnection.IsConnection)
                        this.serviceConnection.closeConnection();
                }

               
            }
            #endregion

            return Individu;
        }

        string IntfDalIndividu.insertIndividu(crlIndividu Individu, string sigleAgence)
        {
            #region declaration
            string numIndividu = "";
            string numQuartier = "NULL";
            int nombreInsert = 0;

            IntfDalIndividu serviceIndividu = new ImplDalIndividu();
            string dateNaissance = "NULL";
            #endregion

            #region implementation
            if (Individu != null)
            {
                if (Individu.NumQuartier != "") 
                {
                    numQuartier = "'" + Individu.NumQuartier + "'";
                }
                if (Individu.DateNaissanceIndividu.Year > 1900)
                {
                    dateNaissance = "'" + Individu.DateNaissanceIndividu.ToString("yyyy-MM-dd") + "'";
                }
                Individu.NumIndividu = serviceIndividu.getNumIndividu(sigleAgence);

                this.strCommande = "INSERT INTO `Individu` (`numIndividu`,`civiliteIndividu`,";
                this.strCommande += " `nomIndividu`,`prenomIndividu`,`cinIndividu`,`adresse`,";
                this.strCommande += " `profession`,`telephoneFixeIndividu`,`telephoneMobileIndividu`,";
                this.strCommande += " `dateNaissanceIndividu`,`lieuNaissanceIndividu`,";
                this.strCommande += " `mailIndividu`,`numQuartier`,`isCheque`,`isBonCommande`)";
                this.strCommande += " VALUES ('" + Individu.NumIndividu + "',";
                this.strCommande += " '" + Individu.CiviliteIndividu + "','" + Individu.NomIndividu + "',";
                this.strCommande += " '" + Individu.PrenomIndividu + "','" + Individu.CinIndividu + "',";
                this.strCommande += " '" + Individu.Adresse + "','" + Individu.Profession + "',";
                this.strCommande += " '" + Individu.TelephoneFixeIndividu + "','" + Individu.TelephoneMobileIndividu + "',";
                this.strCommande += " " + dateNaissance + ",'" + Individu.LieuNaissanceIndividu + "',";
                this.strCommande += " '" + Individu.MailIndividu + "'," + numQuartier + ",";
                this.strCommande += " '" + Individu.IsCheque + "','" + Individu.IsBonCommande + "')";

                this.serviceConnection.openConnection();
                if (this.serviceConnection.IsConnection)
                {
                    nombreInsert = this.serviceConnection.requete(this.strCommande);
                    if (nombreInsert == 1)
                    {
                        numIndividu = Individu.NumIndividu;
                    }

                    while (this.serviceConnection.IsConnection)
                        this.serviceConnection.closeConnection();
                }
            }
            #endregion

            return numIndividu;
        }

        string IntfDalIndividu.isIndividu(crlIndividu Individu)
        {
            #region declaration
            string numIndividu = "";
            #endregion

            #region implementation
            if (Individu != null)
            {
                if (Individu.DateNaissanceIndividu.Year > 1900)
                {
                    this.strCommande = "SELECT * FROM `Individu` WHERE (`numIndividu` <> '" + Individu.NumIndividu + "') AND";
                    this.strCommande += " (Individu.cinIndividu = '" + Individu.CinIndividu + "' OR";
                    this.strCommande += " (Individu.nomIndividu = '" + Individu.NomIndividu + "' AND";
                    this.strCommande += " Individu.prenomIndividu = '" + Individu.PrenomIndividu + "' AND";
                    this.strCommande += " Individu.dateNaissanceIndividu = '" + Individu.DateNaissanceIndividu.ToString("yyyy-MM-dd") + "' AND";
                    this.strCommande += " Individu.lieuNaissanceIndividu = '" + Individu.LieuNaissanceIndividu + "'))";
                }
                else 
                {
                    this.strCommande = "SELECT * FROM `Individu` WHERE (`numIndividu` <> '" + Individu.NumIndividu + "') AND";
                    this.strCommande += " (Individu.cinIndividu = '" + Individu.CinIndividu + "' OR";
                    this.strCommande += " (Individu.nomIndividu = '" + Individu.NomIndividu + "' AND";
                    this.strCommande += " Individu.prenomIndividu = '" + Individu.PrenomIndividu + "' AND";
                    this.strCommande += " Individu.dateNaissanceIndividu IS NULL AND";
                    this.strCommande += " Individu.lieuNaissanceIndividu = '" + Individu.LieuNaissanceIndividu + "'))";
                }

                this.serviceConnection.openConnection();
                if (this.serviceConnection.IsConnection)
                {
                    reader = this.serviceConnection.select(this.strCommande);
                    if (reader != null)
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                
                                numIndividu = reader["numIndividu"].ToString();
                                        
                            }
                        }
                        reader.Dispose();
                    }
                    while (this.serviceConnection.IsConnection)
                        this.serviceConnection.closeConnection();
                }
            }
            #endregion

            return numIndividu;
        }

        string IntfDalIndividu.isCINIndividu(crlIndividu Individu)
        {
            #region declaration
            string numIndividu = "";
            #endregion

            #region implementation
            if (Individu != null)
            {
                
                this.strCommande = "SELECT * FROM `Individu` WHERE `numIndividu` <> '" + Individu.NumIndividu + "' AND";
                this.strCommande += " Individu.cinIndividu = '" + Individu.CinIndividu + "'";
                

                this.serviceConnection.openConnection();
                if (this.serviceConnection.IsConnection)
                {
                    reader = this.serviceConnection.select(this.strCommande);
                    if (reader != null)
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                numIndividu = reader["numIndividu"].ToString();
                            }
                        }
                        reader.Dispose();
                    }
                    while (this.serviceConnection.IsConnection)
                        this.serviceConnection.closeConnection();
                }
            }
            #endregion

            return numIndividu;
        }

        string IntfDalIndividu.isNotCINIndividu(crlIndividu Individu)
        {
            #region declaration
            string numIndividu = "";
            #endregion

            #region implementation
            if (Individu != null)
            {
                if (Individu.DateNaissanceIndividu.Year > 1900)
                {
                    this.strCommande = "SELECT * FROM `Individu` WHERE (`numIndividu` <> '" + Individu.NumIndividu + "') AND";
                    this.strCommande += " (Individu.nomIndividu = '" + Individu.NomIndividu + "' AND";
                    this.strCommande += " Individu.prenomIndividu = '" + Individu.PrenomIndividu + "' AND";
                    this.strCommande += " Individu.dateNaissanceIndividu = '" + Individu.DateNaissanceIndividu.ToString("yyyy-MM-dd") + "' AND";
                    this.strCommande += " Individu.lieuNaissanceIndividu = '" + Individu.LieuNaissanceIndividu + "' AND";
                    this.strCommande += " Individu.civiliteIndividu = '" + Individu.CiviliteIndividu + "')";
                }
                else
                {
                    this.strCommande = "SELECT * FROM `Individu` WHERE (`numIndividu` <> '" + Individu.NumIndividu + "') AND";
                    this.strCommande += " (Individu.nomIndividu = '" + Individu.NomIndividu + "' AND";
                    this.strCommande += " Individu.prenomIndividu = '" + Individu.PrenomIndividu + "' AND";
                    this.strCommande += " Individu.dateNaissanceIndividu IS NULL AND";
                    this.strCommande += " Individu.lieuNaissanceIndividu = '" + Individu.LieuNaissanceIndividu + "' AND";
                    this.strCommande += " Individu.civiliteIndividu = '" + Individu.CiviliteIndividu + "')";
                }

                this.serviceConnection.openConnection();
                if (this.serviceConnection.IsConnection)
                {
                    reader = this.serviceConnection.select(this.strCommande);
                    if (reader != null)
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {

                                numIndividu = reader["numIndividu"].ToString();

                            }
                        }
                        reader.Dispose();
                    }
                    while (this.serviceConnection.IsConnection)
                        this.serviceConnection.closeConnection();
                }
            }
            #endregion

            return numIndividu;
        }

        string IntfDalIndividu.isAllIndividu(crlIndividu Individu)
        {
            #region declaration
            string numIndividu = "";
            #endregion

            #region implementation
            if (Individu != null)
            {
                if (Individu.DateNaissanceIndividu.Year > 1900)
                {
                    this.strCommande = "SELECT * FROM `Individu` WHERE (`numIndividu` <> '" + Individu.NumIndividu + "') AND";
                    this.strCommande += " (Individu.nomIndividu = '" + Individu.NomIndividu + "' AND";
                    this.strCommande += " Individu.prenomIndividu = '" + Individu.PrenomIndividu + "' AND";
                    this.strCommande += " Individu.dateNaissanceIndividu = '" + Individu.DateNaissanceIndividu.ToString("yyyy-MM-dd") + "' AND";
                    this.strCommande += " Individu.lieuNaissanceIndividu = '" + Individu.LieuNaissanceIndividu + "' AND";
                    this.strCommande += " Individu.civiliteIndividu = '" + Individu.CiviliteIndividu + "' AND";
                    this.strCommande += " Individu.cinIndividu = '" + Individu.CinIndividu + "')";
                }
                else
                {
                    this.strCommande = "SELECT * FROM `Individu` WHERE (`numIndividu` <> '" + Individu.NumIndividu + "') AND";
                    this.strCommande += " (Individu.nomIndividu = '" + Individu.NomIndividu + "' AND";
                    this.strCommande += " Individu.prenomIndividu = '" + Individu.PrenomIndividu + "' AND";
                    this.strCommande += " Individu.dateNaissanceIndividu IS NULL AND";
                    this.strCommande += " Individu.lieuNaissanceIndividu = '" + Individu.LieuNaissanceIndividu + "' AND";
                    this.strCommande += " Individu.civiliteIndividu = '" + Individu.CiviliteIndividu + "' AND";
                    this.strCommande += " Individu.cinIndividu = '" + Individu.CinIndividu + "')";
                }

                this.serviceConnection.openConnection();
                if (this.serviceConnection.IsConnection)
                {
                    reader = this.serviceConnection.select(this.strCommande);
                    if (reader != null)
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {

                                numIndividu = reader["numIndividu"].ToString();

                            }
                        }
                        reader.Dispose();
                    }
                    while (this.serviceConnection.IsConnection)
                        this.serviceConnection.closeConnection();
                }
            }
            #endregion

            return numIndividu;
        }

        string IntfDalIndividu.getNumIndividu(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numIndividu = "00001";
            string[] tempNumIndividu = null;
            string strDate = "IN" + DateTime.Now.ToString("yyMM") + "/" + sigleAgence;
            #endregion

            #region implementation
            this.strCommande = "SELECT Individu.numIndividu AS maxNum FROM Individu";
            this.strCommande += " WHERE Individu.numIndividu LIKE '%" + strDate + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnection.openConnection();
            reader = this.serviceConnection.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumIndividu = reader["maxNum"].ToString().ToString().Split('/');
                        numIndividu = tempNumIndividu[tempNumIndividu.Length - 1];
                    }
                    numTemp = double.Parse(numIndividu) + 1;
                    if (numTemp < 10)
                        numIndividu = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numIndividu = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numIndividu = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numIndividu = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numIndividu = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnection.closeConnection();
            numIndividu = strDate + "/" + numIndividu;
            #endregion

            return numIndividu;
        }

        bool IntfDalIndividu.updateIndividu(crlIndividu Individu)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            string dateNaissance = "NULL";
            string numQuartier = "NULL";
            #endregion

            #region implementation
            if (Individu != null)
            {
                if (Individu.NumQuartier != "")
                {
                    numQuartier = "'" + Individu.NumQuartier + "'";
                }
                if (Individu.DateNaissanceIndividu.Year > 1900)
                {
                    dateNaissance = "'" + Individu.DateNaissanceIndividu.ToString("yyyy-MM-dd") + "'";
                }
                this.strCommande = "UPDATE `Individu` SET `adresse`='" + Individu.Adresse + "',";
                this.strCommande += " `cinIndividu`='" + Individu.CinIndividu + "',`civiliteIndividu`='" + Individu.CiviliteIndividu + "',";
                this.strCommande += " `nomIndividu`='" + Individu.NomIndividu + "'";
                if (Individu.PrenomIndividu != "")
                {
                    this.strCommande += " ,`prenomIndividu`='" + Individu.PrenomIndividu + "'";
                }
                if (Individu.Profession != "")
                {
                    this.strCommande += " ,`profession`='" + Individu.Profession + "'"; 
                }
                if (Individu.TelephoneFixeIndividu != "")
                {
                    this.strCommande += " ,`telephoneFixeIndividu`='" + Individu.TelephoneFixeIndividu + "'";
                }
                if (Individu.TelephoneMobileIndividu != "")
                {
                    this.strCommande += " ,`telephoneMobileIndividu`='" + Individu.TelephoneMobileIndividu + "'";
                }
                if (dateNaissance != "")
                {
                    this.strCommande += " ,`dateNaissanceIndividu`=" + dateNaissance + "";
                }
                if (Individu.LieuNaissanceIndividu != "")
                {
                    this.strCommande += " ,`lieuNaissanceIndividu`='" + Individu.LieuNaissanceIndividu + "'";
                }
                if (Individu.MailIndividu != "")
                {
                    this.strCommande += " ,`mailIndividu`='" + Individu.MailIndividu + "'";
                }
                this.strCommande += " ,`numQuartier`=" + numQuartier;
                if (Individu.IsCheque >= 0)
                {
                    this.strCommande += " ,`isCheque`='" + Individu.IsCheque + "'";
                }
                if (Individu.IsBonCommande >= 0)
                {
                    this.strCommande += " ,`isBonCommande`='" + Individu.IsBonCommande + "'";
                }
                this.strCommande += " WHERE `numIndividu`='" + Individu.NumIndividu + "'";

                this.serviceConnection.openConnection();
                nbUpdate = this.serviceConnection.requete(this.strCommande);
                if (nbUpdate == 1)
                {
                    isUpdate = true;
                }
                this.serviceConnection.closeConnection();
            }
            #endregion

            return isUpdate;
        }
        #endregion


        #region IntfDalIndividu Members


        string IntfDalIndividu.insertIndividu(crlIndividu Individu, string sigleAgence, HtmlGenericControl divIndication)
        {
            #region declaration
            string strNumIndividuTemp = "";
            string numIndividu = "";
            string strIndication = "";
            crlIndividu individuIndication = null;
            IntfDalIndividu serviceIndividu = new ImplDalIndividu();
            #endregion

            #region implementation
            divIndication.Style.Add("font-size", "14px");
            divIndication.Style.Add("color", "Red");
            if (Individu != null)
            {
                if (Individu.CinIndividu != "")
                {
                    Individu.NumIndividu = serviceIndividu.isCINIndividu(Individu);
                    if (Individu.NumIndividu.Equals(""))
                    {
                        Individu.NumIndividu = serviceIndividu.insertIndividu(Individu, sigleAgence);
                        if (Individu.NumIndividu != "")
                        {
                            numIndividu = Individu.NumIndividu;
                        }
                        else
                        {
                            strIndication = "Une erreur ce produit durant l'enregistrement!";
                            divIndication.InnerHtml = "<p>" + strIndication + "</p>";
                        }
                    }
                    else
                    {
                        strNumIndividuTemp = Individu.NumIndividu;
                        Individu.NumIndividu = "";
                        Individu.NumIndividu = serviceIndividu.isAllIndividu(Individu);
                        if (Individu.NumIndividu.Equals(""))
                        {
                            strIndication = "CIN déjà enregistrer dans la base de données!<br/>";
                            individuIndication = serviceIndividu.selectIndividu(strNumIndividuTemp);
                            if (individuIndication != null)
                            {
                                strIndication += "Nom:" + individuIndication.NomIndividu + "<br/>";
                                strIndication += "Prénom:" + individuIndication.PrenomIndividu + "<br/>";
                            }
                            divIndication.InnerHtml = "<p>" + strIndication + "</p>";
                        }
                        else
                        {
                            if (serviceIndividu.updateIndividu(Individu))
                            {
                                numIndividu = Individu.NumIndividu;
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
                    Individu.NumIndividu = serviceIndividu.isNotCINIndividu(Individu);
                    if (individuIndication.NumIndividu.Equals(""))
                    {
                        Individu.NumIndividu = serviceIndividu.insertIndividu(Individu, sigleAgence);
                        if (Individu.NumIndividu != "")
                        {
                            numIndividu = Individu.NumIndividu;
                        }
                        else
                        {
                            strIndication = "Une erreur ce produit durant l'enregistrement!";
                            divIndication.InnerHtml = "<p>" + strIndication + "</p>";
                        }
                    }
                    else
                    {
                        if (serviceIndividu.updateIndividu(Individu))
                        {
                            numIndividu = Individu.NumIndividu;
                        }
                        else
                        {
                            strIndication = "Une erreur ce produit durant l'enregistrement!";
                            divIndication.InnerHtml = "<p>" + strIndication + "</p>";
                        }
                    }
                }
            }
            #endregion

            return numIndividu;
        }

        bool IntfDalIndividu.updateIndividu(crlIndividu Individu, HtmlGenericControl divIndication)
        {
            #region declaration
            bool isUpdate = false;
            string numIndividu = "";
            string strIndication = "";
            crlIndividu individuIndication = null;
            IntfDalIndividu serviceIndividu = new ImplDalIndividu();
            #endregion

            #region implementation
            divIndication.Style.Add("font-size", "14px");
            divIndication.Style.Add("color", "Red");
            if (Individu != null)
            {
                if (Individu.CinIndividu != "")
                {
                    numIndividu = serviceIndividu.isAllIndividu(Individu);
                    if (numIndividu.Equals(""))
                    {
                        isUpdate = serviceIndividu.updateIndividu(Individu);
                        if (!isUpdate)
                        {
                            strIndication = "Une erreur ce produit durant la modification!";
                            divIndication.InnerHtml = "<p>" + strIndication + "</p>";
                        }
                    }
                    else
                    {
                        strIndication = "CIN déjà enregistrer dans la base de données!<br/>";
                        individuIndication = serviceIndividu.selectIndividu(numIndividu);
                        if (individuIndication != null)
                        {
                            strIndication += "Nom:" + individuIndication.NomIndividu + "<br/>";
                            strIndication += "Prénom:" + individuIndication.PrenomIndividu + "<br/>";
                        }
                        divIndication.InnerHtml = "<p>" + strIndication + "</p>";
                    }

                }
                else
                {
                    isUpdate = serviceIndividu.updateIndividu(Individu);
                    if (!isUpdate)
                    {
                        strIndication = "Une erreur ce produit durant la modification!";
                        divIndication.InnerHtml = "<p>" + strIndication + "</p>";
                    }
                }
            }
            #endregion

            return isUpdate;
        }

        #endregion


        void IntfDalIndividu.insertToGridIndividu(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalIndividu serviceIndividu = new ImplDalIndividu();
            #endregion

            #region implementation
            this.strCommande = "SELECT individu.numIndividu, individu.civiliteIndividu, individu.nomIndividu,";
            this.strCommande += " individu.prenomIndividu, individu.cinIndividu, individu.adresse,";
            this.strCommande += " individu.profession, individu.telephoneFixeIndividu,";
            this.strCommande += " individu.telephoneMobileIndividu, individu.dateNaissanceIndividu,";
            this.strCommande += " individu.lieuNaissanceIndividu, individu.mailIndividu, individu.numQuartier,";
            this.strCommande += " individu.isCheque, individu.isBonCommande FROM individu";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param + " ASC";

            gridView.DataSource = serviceIndividu.getDataTableIndividu(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalIndividu.getDataTableIndividu(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implemntation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numIndividu", typeof(string));
            dataTable.Columns.Add("individu", typeof(string));
            dataTable.Columns.Add("adresse", typeof(string));
            dataTable.Columns.Add("contact", typeof(string));
            dataTable.Columns.Add("cin", typeof(string));
            DataRow dr;
            #endregion

            this.serviceConnection.openConnection();
            this.reader = this.serviceConnection.select(strRqst);

            if (this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    while (this.reader.Read())
                    {
                        dr = dataTable.NewRow();

                        dr["numIndividu"] = this.reader["numIndividu"].ToString();
                        dr["individu"] = this.reader["prenomIndividu"].ToString() + " " + this.reader["nomIndividu"].ToString();
                        dr["adresse"] = this.reader["adresse"].ToString();
                        dr["contact"] = this.reader["telephoneFixeIndividu"].ToString() + " / " + this.reader["telephoneMobileIndividu"].ToString();
                        dr["cin"] = this.reader["cinIndividu"].ToString();

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }

            this.serviceConnection.closeConnection();


            #endregion

            return dataTable;
        }







        
    }
}