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
    /// Implementation du service passager
    /// </summary>
    public class ImplDalPassager : IntfDalPassager
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalPassager()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalPassager(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region IntfDalPassager Members

        string IntfDalPassager.insertPassager(crlPassager Passager)
        {
            #region declaration
            IntfDalPassager servicePassager = new ImplDalPassager();
            int nombreInsertion = 0;
            string idPassager = "";
            #endregion

            #region implementation
            if (Passager != null)
            {
                Passager.IdPassager = servicePassager.getIdPassager();
                this.strCommande = "INSERT INTO `passager` (`idPassager`,`nomPassager`";
                this.strCommande += ",`prenomPassager`,`pieceIdentite`,`telephonePassager`)";
                this.strCommande += " VALUES ('" + Passager.IdPassager + "','" + Passager.NomPassager + "'";
                this.strCommande += ",'" + Passager.PrenomPassager + "','" + Passager.PieceIdentite + "'";
                this.strCommande += ",'" +Passager.TelephonePassager + "')";

                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    idPassager = Passager.IdPassager;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return idPassager;
        }

        bool IntfDalPassager.deletePassager(crlPassager Passager)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (Passager != null)
            {
                if (Passager.IdPassager != "")
                {
                    this.strCommande = "DELETE FROM `passager` WHERE (`idPassager` = '" + Passager.IdPassager + "')";
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

        bool IntfDalPassager.deletePassager(string idPassager)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
           
            if (idPassager != "")
            {
                this.strCommande = "DELETE FROM `passager` WHERE (`idPassager` = '" + idPassager + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete == 1)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }
           
            #endregion

            return isDelete;
        }

        bool IntfDalPassager.updatePassager(crlPassager Passager)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            #endregion

            #region implementation
            if (Passager != null)
            {
                if (Passager.IdPassager != "")
                {
                    this.strCommande = "UPDATE `passager` SET `nomPassager`='" + Passager.NomPassager + "', `prenomPassager`='" + Passager.PrenomPassager +"', ";
                    this.strCommande += "`pieceIdentite`='" + Passager.PieceIdentite + "', `telephonePassager`='" + Passager.TelephonePassager + "' ";
                    this.strCommande += "WHERE (`idPassager`='" + Passager.IdPassager + "')";

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

        string IntfDalPassager.isPassager(crlPassager Passager)
        {
            #region declaration
            string isPersonne = "";
            #endregion

            #region implementation
            if (Passager != null)
            {
                this.strCommande = "SELECT * FROM `passager` WHERE (`idPassager` <> '" + Passager.IdPassager + "')";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (Passager.NomPassager.Replace(" ", "").ToLower().Equals(reader["nomPassager"].ToString().Replace(" ","").ToLower()) &&
                                Passager.PrenomPassager.Replace(" ", "").ToLower().Equals(reader["prenomPassager"].ToString().Replace(" ","").ToLower()))
                            {
                                isPersonne = reader["idPassager"].ToString();
                                break;
                            }
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isPersonne;
        }

        crlPassager IntfDalPassager.selectPassager(string idPassager)
        {
            #region declaration
            crlPassager Passager = null;
            #endregion

            #region implementation
            if (idPassager != "")
            {
                this.strCommande = "SELECT * FROM `personne` WHERE (`idPersonne` = '" + idPassager + "')";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        Passager = new crlPassager();
                        reader.Read();
                        Passager.IdPassager = reader["idPassager"].ToString();
                        Passager.NomPassager = reader["nomPassager"].ToString();
                        Passager.PieceIdentite = reader["pieceIdentite"].ToString();
                        Passager.PrenomPassager = reader["prenomPassager"].ToString();
                        Passager.TelephonePassager = reader["telephonePassager"].ToString();
                        
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return Passager;
        }

        string IntfDalPassager.getIdPassager()
        {
            #region declaration
            int numTemp = 0;
            string idPassager = "00001";
            string[] tempIdPassager = null;
            #endregion

            #region implementation
            this.strCommande = "SELECT passager.idPassager AS maxNum FROM passager ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempIdPassager = reader["maxNum"].ToString().ToString().Split('/');
                        idPassager = tempIdPassager[0];
                    }
                    numTemp = int.Parse(idPassager) + 1;
                    if (numTemp < 10)
                        idPassager = "0000" + numTemp;
                    if (numTemp < 100 && numTemp >= 10)
                        idPassager = "000" + numTemp;
                    if (numTemp < 1000 && numTemp >= 100)
                        idPassager = "00" + numTemp;
                    if (numTemp < 10000 && numTemp >= 1000)
                        idPassager = "0" + numTemp;
                    if (numTemp >= 10000)
                        idPassager = "" + numTemp;
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            idPassager = idPassager + "/PA/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString("0000");
            #endregion

            return idPassager;
        }

        void IntfDalPassager.loadDdlTri(DropDownList ddlTri)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
