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
    /// Implementation du service escorte
    /// </summary>
    public class ImplDalEscorte : IntfDalEscorte
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalEscorte(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalEscorte()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion

        #region IntfDalEscorte Members

        string IntfDalEscorte.insertEscorte(crlEscorte Escorte)
        {
            #region declaration
            IntfDalEscorte serviceEscorte = new ImplDalEscorte();
            int nombreInsertion = 0;
            string matriculeEscorte = "";
            #endregion

            #region implementation
            if (Escorte != null)
            {
                Escorte.MatriculeEscorte = serviceEscorte.getMatriculeEscorte();
                this.strCommande = "INSERT INTO `escorte` (`matriculeEscorte`,`nomEscorte`,`prenomEscorte`";
                this.strCommande += ",`cinEscorte`,`adresseEscorte`,`telephoneEscorte`,`telephoneMobileEscorte`)";
                this.strCommande += " VALUES ('" + Escorte.MatriculeEscorte + "','" + Escorte.NomEscorte + "'";
                this.strCommande += ",'" + Escorte.PrenomEscorte + "','" + Escorte.CinEscorte + "'";
                this.strCommande += ",'" + Escorte.AdresseEscorte + "','" + Escorte.TelephoneEscorte + "', '" + Escorte.TelephoneMobileEscorte + "')";

                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    matriculeEscorte = Escorte.MatriculeEscorte;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return matriculeEscorte;
        }

        bool IntfDalEscorte.deleteEscorte(crlEscorte Escorte)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (Escorte != null)
            {
                if (Escorte.MatriculeEscorte != "")
                {
                    this.strCommande = "DELETE FROM `escorte` WHERE (`matriculeEscorte` = '" + Escorte.MatriculeEscorte + "')";
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

        bool IntfDalEscorte.deleteEscorte(string matriculeEscorte)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            
            if (matriculeEscorte != "")
            {
                this.strCommande = "DELETE FROM `escorte` WHERE (`matriculeEscorte` = '" + matriculeEscorte + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete == 1)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }
            
            #endregion

            return isDelete;
        }

        bool IntfDalEscorte.updateEscorte(crlEscorte Escorte)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            #endregion

            #region implementation
            if (Escorte != null)
            {
                if (Escorte.MatriculeEscorte != "")
                {
                    this.strCommande = "UPDATE `escorte` SET `nomEscorte`='" + Escorte.NomEscorte + "', `prenomEscorte`='" + Escorte.PrenomEscorte + "', ";
                    this.strCommande += "`cinEscorte`='" + Escorte.CinEscorte + "', `adresseEscorte`='" + Escorte.AdresseEscorte + "', ";
                    this.strCommande += "`telephoneEscorte`='" + Escorte.TelephoneEscorte + "', `telephoneMobileEscorte`='" + Escorte.TelephoneMobileEscorte + "' ";
                    this.strCommande += "WHERE (`matriculeEscorte`='" + Escorte.MatriculeEscorte + "')";

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

        string IntfDalEscorte.isEscorte(crlEscorte Escorte)
        {
            #region declaration
            string isEscorte = "";
            #endregion

            #region implementation
            if (Escorte != null)
            {
                this.strCommande = "SELECT * FROM `escorte` WHERE (`matriculeEscorte` <> '" + Escorte.MatriculeEscorte+ "')";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (Escorte.CinEscorte.Trim().ToLower().Equals(reader["cinEscorte"].ToString().Trim().ToLower()))
                            {
                                isEscorte = reader["matriculeEscorte"].ToString();
                                break;
                            }
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isEscorte;
        }

        int IntfDalEscorte.isEscorteInt(crlEscorte Escorte)
        {
            #region declaration
            int isEscorte = 0;
            #endregion

            #region implementation
            if (Escorte != null)
            {
                this.strCommande = "SELECT * FROM `escorte` WHERE (`matriculeEscorte` <> '" + Escorte.MatriculeEscorte + "')";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (Escorte.CinEscorte.Trim().ToLower().Equals(reader["cinEscorte"].ToString().Trim().ToLower()))
                            {
                                isEscorte = 1;
                                break;
                            }
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isEscorte;
        }

        crlEscorte IntfDalEscorte.selectEscorte(string matriculeEscorte)
        {
            #region declaration
            crlEscorte Escorte= null;
            #endregion

            #region implementation
            if (matriculeEscorte != "")
            {
                this.strCommande = "SELECT * FROM `escorte` WHERE (`matriculeEscorte` = '" + matriculeEscorte + "')";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        Escorte = new crlEscorte();
                        reader.Read();
                        Escorte.AdresseEscorte = reader["adresseEscorte"].ToString();
                        Escorte.CinEscorte = reader["cinEscorte"].ToString();
                        Escorte.MatriculeEscorte = reader["matriculeEscorte"].ToString();
                        Escorte.NomEscorte = reader["nomEscorte"].ToString();
                        Escorte.PrenomEscorte = reader["prenomEscorte"].ToString();
                        Escorte.TelephoneEscorte = reader["telephoneEscorte"].ToString();
                        Escorte.TelephoneMobileEscorte = reader["telephoneMobileEscorte"].ToString();
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return Escorte;
        }

        string IntfDalEscorte.getMatriculeEscorte()
        {
            #region declaration
            int numTemp = 0;
            string matriculeEscorte = "00001";
            string[] tempMatriculeEscorte = null;
            #endregion

            #region implementation
            this.strCommande = "SELECT escorte.matriculeEscorte AS maxNum FROM escorte ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempMatriculeEscorte = reader["maxNum"].ToString().ToString().Split('/');
                        matriculeEscorte = tempMatriculeEscorte[0];
                    }
                    numTemp = int.Parse(matriculeEscorte) + 1;
                    if (numTemp < 10)
                        matriculeEscorte = "0000" + numTemp;
                    if (numTemp < 100 && numTemp >= 10)
                        matriculeEscorte = "000" + numTemp;
                    if (numTemp < 1000 && numTemp >= 100)
                        matriculeEscorte = "00" + numTemp;
                    if (numTemp < 10000 && numTemp >= 1000)
                        matriculeEscorte = "0" + numTemp;
                    if (numTemp >= 10000)
                        matriculeEscorte = "" + numTemp;
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            matriculeEscorte = matriculeEscorte + "/ES/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString("0000");
            #endregion

            return matriculeEscorte;
        }

        void IntfDalEscorte.loadDdlTri(DropDownList ddlTri)
        {
            throw new NotImplementedException();
        }

        #endregion


        
    }
}
