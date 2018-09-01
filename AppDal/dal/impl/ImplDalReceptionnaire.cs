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
    /// implementation du service personne
    /// </summary>
    public class ImplDalReceptionnaire : IntfDalReceptionnaire
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalReceptionnaire(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalReceptionnaire() 
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion

        #region IntfDalPersonne Members

        string IntfDalReceptionnaire.insertPersonne(crlReceptionnaire Personne, string sigleAgence)
        {
            #region declaration
            IntfDalReceptionnaire servicePersonne = new ImplDalReceptionnaire();
            int nombreInsertion = 0;
            string idPersonne = "";
            #endregion

            #region implementation
            if (Personne != null) 
            {
                Personne.IdPersonne = servicePersonne.getIdPersonne(sigleAgence);
                this.strCommande = "INSERT INTO `receptionnaire` (`idPersonne`,`nomPersonne`,`prenomPersonne`";
                this.strCommande += ",`adressePersonne`,`telephone`)";
                this.strCommande += " VALUES ('" + Personne.IdPersonne + "','" + Personne.NomPersonne + "'";
                this.strCommande += ",'" + Personne.PrenomPersonne + "','" + Personne.AdressePersonne + "'";
                this.strCommande += ",'" + Personne.Telephone + "')";

                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    idPersonne = Personne.IdPersonne;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return idPersonne;
        }

        bool IntfDalReceptionnaire.deletePersonne(crlReceptionnaire Personne)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (Personne != null)
            {
                if (Personne.IdPersonne != "")
                {
                    this.strCommande = "DELETE FROM `receptionnaire` WHERE (`idPersonne` = '" + Personne.IdPersonne + "')";
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

        bool IntfDalReceptionnaire.deletePersonne(string idPersonne)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            
            if (idPersonne != "")
            {
                this.strCommande = "DELETE FROM `receptionnaire` WHERE (`idPersonne` = '" + idPersonne + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete == 1)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }
           
            #endregion

            return isDelete;
        }

        bool IntfDalReceptionnaire.updatePersonne(crlReceptionnaire Personne)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            #endregion

            #region implementation
            if (Personne != null)
            {
                if (Personne.IdPersonne != "")
                {
                    this.strCommande = "UPDATE `receptionnaire` SET `nomPersonne`='" + Personne.NomPersonne + "', `prenomPersonne`='" + Personne.PrenomPersonne + "', ";
                    this.strCommande += "`adressePersonne`='" + Personne.AdressePersonne + "', `telephone`='" + Personne.Telephone + "' ";
                    this.strCommande += "WHERE (`idPersonne`='" + Personne.IdPersonne + "')";

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

        string IntfDalReceptionnaire.isPersonne(crlReceptionnaire Personne)
        {
            #region declaration
            string isPersonne = "";
            #endregion

            #region implementation
            if (Personne != null) 
            {
                this.strCommande = "SELECT * FROM `receptionnaire` WHERE (`idPersonne` <> '" + Personne.IdPersonne + "' AND";
                this.strCommande += " `nomPersonne`='" + Personne.NomPersonne + "' AND `prenomPersonne`='" + Personne.PrenomPersonne + "')";
                
                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null) 
                {
                    if (reader.HasRows) 
                    {
                        if (reader.Read()) 
                        {
                            
                            isPersonne = reader["idPersonne"].ToString();
                                
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isPersonne;
        }

        int IntfDalReceptionnaire.isPersonneInt(crlReceptionnaire Personne)
        {
            #region declaration
            int isPersonne = 0;
            #endregion

            #region implementation
            if (Personne != null)
            {
                this.strCommande = "SELECT * FROM `receptionnaire` WHERE (`idPersonne` <> '" + Personne.IdPersonne + "' AND";
                this.strCommande += " `nomPersonne`='" + Personne.NomPersonne + "' AND `prenomPersonne`='" + Personne.PrenomPersonne + "')";
                
                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            
                             isPersonne = 1;
                                
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isPersonne;
        }

        crlReceptionnaire IntfDalReceptionnaire.selectPersonne(string idPersonne)
        {
            #region declaration
            crlReceptionnaire Personne = null;
            #endregion

            #region implementation
            if (idPersonne != "") 
            {
                this.strCommande = "SELECT * FROM `receptionnaire` WHERE (`idPersonne` = '" + idPersonne + "')";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null) 
                {
                    if (reader.HasRows) 
                    {
                        Personne = new crlReceptionnaire();
                        reader.Read();
                        Personne.AdressePersonne = reader["adressePersonne"].ToString();
                        Personne.IdPersonne = reader["idPersonne"].ToString();
                        Personne.NomPersonne = reader["nomPersonne"].ToString();
                        Personne.PrenomPersonne = reader["prenomPersonne"].ToString();
                        Personne.Telephone = reader["telephone"].ToString();
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return Personne;
        }

        string IntfDalReceptionnaire.getIdPersonne(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string idPersonne = "00001";
            string[] tempIdPersonne = null;
            string strDate = "RC" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT receptionnaire.idPersonne AS maxNum FROM receptionnaire";
            this.strCommande += " WHERE receptionnaire.idPersonne LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempIdPersonne = reader["maxNum"].ToString().ToString().Split('/');
                        idPersonne = tempIdPersonne[tempIdPersonne.Length - 1];
                    }
                    numTemp = double.Parse(idPersonne) + 1;
                    if (numTemp < 10)
                        idPersonne = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        idPersonne = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        idPersonne = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        idPersonne = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        idPersonne = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            idPersonne = strDate + "/" + sigleAgence + "/" + idPersonne;
            #endregion

            return idPersonne;
        }

        void IntfDalReceptionnaire.loadDdlTri(DropDownList ddlTri)
        {
            throw new NotImplementedException();
        }

        #endregion




        #region insert to grid
        void IntfDalReceptionnaire.insertToGridReceptionnaire(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalReceptionnaire serviceReceptionnaire = new ImplDalReceptionnaire();
            #endregion

            #region implementation
            this.strCommande = "SELECT receptionnaire.idPersonne, receptionnaire.nomPersonne, receptionnaire.prenomPersonne,";
            this.strCommande += " receptionnaire.adressePersonne, receptionnaire.telephone FROM receptionnaire";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param + " ASC";

            gridView.DataSource = serviceReceptionnaire.getDataTableReceptionnaire(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalReceptionnaire.getDataTableReceptionnaire(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implemntation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("idPersonne", typeof(string));
            dataTable.Columns.Add("receptionnaire", typeof(string));
            dataTable.Columns.Add("adresse", typeof(string));
            dataTable.Columns.Add("contact", typeof(string));
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

                        dr["idPersonne"] = this.reader["idPersonne"].ToString();
                        dr["receptionnaire"] = this.reader["prenomPersonne"].ToString() + " " + this.reader["nomPersonne"].ToString();
                        dr["adresse"] = this.reader["adressePersonne"].ToString();
                        dr["contact"] = this.reader["telephone"].ToString();

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
