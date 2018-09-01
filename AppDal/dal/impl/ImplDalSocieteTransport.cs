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
    /// IntfDalSocieteTransport
    /// Implementation pour le service Societe Transport
    /// </summary>
    public class ImplDalSocieteTransport : IntfDalSocieteTransport
    {
        #region declaration
        IntfDalConnectBase serviceConnection = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalSocieteTransport()
        {
            this.serviceRessource = new ImplDalServiceRessource();
            this.serviceConnection = new ImplDalConnectBase(this.serviceRessource.getDefaultStrConnection());
        }
        public ImplDalSocieteTransport(string strConnection)
        {
            this.serviceConnection = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region IntfDalSocieteTransport Members

        string IntfDalSocieteTransport.insertSocieteTransport(crlSocieteTransport SocieteTransport)
        {
            #region declaration
            IntfDalSocieteTransport serviceSocieteTransport = new ImplDalSocieteTransport();
            int nombreInsertion = 0;
            string numerosSociete = "";
            #endregion

            #region implementation
            if (SocieteTransport != null)
            {

                SocieteTransport.NumerosSociete = serviceSocieteTransport.getNumerosSociete();
                this.strCommande = "INSERT INTO `societetransport` (`numerosSociete`,`nomSociete`) VALUES ";
                this.strCommande+= " ('" + SocieteTransport.NumerosSociete + "','" + SocieteTransport.NomSociete + "')";

                this.serviceConnection.openConnection();
                nombreInsertion = this.serviceConnection.requete(this.strCommande);
                if (nombreInsertion == 1)
                    numerosSociete = SocieteTransport.NumerosSociete;
                this.serviceConnection.closeConnection();
                
            }
            #endregion

            return numerosSociete;
        }

        bool IntfDalSocieteTransport.deleteSocieteTransport(crlSocieteTransport SocieteTransport)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (SocieteTransport != null)
            {
                if (SocieteTransport.NumerosSociete != "")
                {
                    this.strCommande = "DELETE FROM `societetransport` WHERE (`numerosSociete` = '" + SocieteTransport.NumerosSociete + "')";
                    this.serviceConnection.openConnection();
                    nombreDelete = this.serviceConnection.requete(this.strCommande);
                    if (nombreDelete == 1)
                        isDelete = true;
                    this.serviceConnection.closeConnection();
                }
            }
            #endregion

            return isDelete;
        }

        bool IntfDalSocieteTransport.deleteSocieteTransport(string numerosSociete)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (numerosSociete != "")
            {
                this.strCommande = "DELETE FROM `societetransport` WHERE (`numerosSociete` = '" + numerosSociete + "')";
                this.serviceConnection.openConnection();
                nombreDelete = this.serviceConnection.requete(this.strCommande);
                if (nombreDelete == 1)
                    isDelete = true;
                this.serviceConnection.closeConnection();
            }
            #endregion

            return isDelete;
        }

        bool IntfDalSocieteTransport.updateSocieteTransport(crlSocieteTransport SocieteTransport)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            #endregion

            #region implementation
            if (SocieteTransport != null)
            {
                if (SocieteTransport.NumerosSociete != "")
                {
                    this.strCommande = "UPDATE `societetransport` SET `nomSociete`='" + SocieteTransport.NomSociete + "'";
                    this.strCommande += " WHERE (`numerosSociete`='" + SocieteTransport.NumerosSociete + "')";

                    this.serviceConnection.openConnection();
                    nombreUpdate = this.serviceConnection.requete(this.strCommande);
                    if (nombreUpdate == 1)
                        isUpdate = true;
                    this.serviceConnection.closeConnection();
                }
            }
            #endregion

            return isUpdate;
        }

        int IntfDalSocieteTransport.isSocieteTransport(crlSocieteTransport SocieteTransport)
        {
            #region declaration
            int isSocieteTransport = 0;
            #endregion

            #region implementation
            if (SocieteTransport != null) 
            {
                this.strCommande = "SELECT * FROM `societetransport` WHERE (`numerosSociete`<> '" + SocieteTransport.NumerosSociete + "')";
                this.serviceConnection.openConnection();
                reader = this.serviceConnection.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows) 
                    {
                        while (reader.Read()) 
                        {
                            if (SocieteTransport.NomSociete.Trim().ToLower().Equals(reader["nomSociete"].ToString().Trim().ToLower())) 
                            {
                                isSocieteTransport = 1;
                                break;
                            }
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnection.closeConnection();
            }
            #endregion

            return isSocieteTransport;
        }

        crlSocieteTransport IntfDalSocieteTransport.selectSocieteTransport(string numerosSociete)
        {
            #region declaration
            crlSocieteTransport SocieteTransport = null; 
            #endregion

            #region implementation
            if (numerosSociete != "") 
            {
                this.strCommande = "SELECT * FROM `societetransport`";
                this.serviceConnection.openConnection();
                reader = this.serviceConnection.select(this.strCommande);
                if (reader != null) 
                {
                    if (reader.HasRows) 
                    {
                        if (reader.Read()) 
                        {
                            SocieteTransport = new crlSocieteTransport();
                            SocieteTransport.NomSociete = reader["nomSociete"].ToString();
                            SocieteTransport.NumerosSociete = reader["numerosSociete"].ToString();
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnection.closeConnection();
            }
            #endregion

            return SocieteTransport;
        }

        string IntfDalSocieteTransport.getNumerosSociete()
        {
            #region declaration
            int numTemp = 0;
            string numerosSociete = "0001";
            string[] tempNumerosSociete = null;
            #endregion

            #region implementation
            this.strCommande = "SELECT societetransport.numerosSociete AS maxNum FROM societetransport ORDER BY maxNum DESC";
            this.serviceConnection.openConnection();
            reader = this.serviceConnection.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumerosSociete = reader["maxNum"].ToString().ToString().Split('/');
                        numerosSociete = tempNumerosSociete[0];
                    }
                    numTemp = int.Parse(numerosSociete) + 1;
                    if (numTemp < 10)
                        numerosSociete = "000" + numTemp;
                    if (numTemp < 100 && numTemp >= 10)
                        numerosSociete = "00" + numTemp;
                    if (numTemp < 1000 && numTemp >= 100)
                        numerosSociete = "0" + numTemp;
                    if (numTemp >= 1000)
                        numerosSociete = "" + numTemp;
                  
                }
                reader.Dispose();
            }
            this.serviceConnection.closeConnection();
            numerosSociete = numerosSociete + "/ST/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString("0000");
            #endregion

            return numerosSociete;
        }

        void IntfDalSocieteTransport.loadDdlTri(DropDownList ddlTri)
        {
            ddlTri.Items.Clear();
            ddlTri.Items.Add(new ListItem("Numeros", "numerosSociete"));
            ddlTri.Items.Add(new ListItem("Nom Société", "nomSociete"));
        }

        #endregion
    }
}