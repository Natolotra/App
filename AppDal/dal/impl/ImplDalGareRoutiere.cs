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
using arch.crl;
using MySql.Data.MySqlClient;

namespace arch.dal.impl
{
    /// <summary>
    /// implementation service gare routiere
    /// </summary>
    
    public class ImplDalGareRoutiere : IntfDalGareRoutiere
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalGareRoutiere(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalGareRoutiere()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(this.serviceRessource.getDefaultStrConnection());
        }
        #endregion

        #region intfDalGareRoutiere Members

        string IntfDalGareRoutiere.insertGareRoutiere(crlGareRoutiere gareRoutiere)
        {
            #region declaration
            IntfDalGareRoutiere serviceGareRoutiere = new ImplDalGareRoutiere();
            int nombreInsertion = 0;
            string numerosGareRoutiere = "";
            #endregion

            #region implementation
            if (gareRoutiere != null)
            {
                gareRoutiere.numerosGareRoutiere = serviceGareRoutiere.getNumerosGareRoutiere(gareRoutiere.nomProvince);
                this.strCommande = "INSERT INTO `gareroutiere` (`numerosGareRoutiere`,`nomProvince`,`localisation`,`sigleGare`,`numVille`) ";
                this.strCommande += "VALUES ('" + gareRoutiere.numerosGareRoutiere + "', '" + gareRoutiere.nomProvince + "','" + gareRoutiere.localisation + "', '" + gareRoutiere.sigleGare.ToUpper() + "','" + gareRoutiere.NumVille + "')";

                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    numerosGareRoutiere = gareRoutiere.numerosGareRoutiere;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numerosGareRoutiere;
        }

        bool IntfDalGareRoutiere.deleteGareRoutiere(crlGareRoutiere gareRoutiere)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (gareRoutiere != null)
            {
                if (gareRoutiere.numerosGareRoutiere != "")
                {
                    this.strCommande = "DELETE FROM `gareRoutiere` WHERE (`numerosGareRoutiere` = '" + gareRoutiere.numerosGareRoutiere + "')";
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

        bool IntfDalGareRoutiere.deleteGareRoutiere(string numerosGareRoutiere)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (numerosGareRoutiere != "")
            {
                this.strCommande = "DELETE FROM `gareRoutiere` WHERE (`numerosGareRoutiere` = '" + numerosGareRoutiere + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete == 1)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isDelete;
        }

        bool IntfDalGareRoutiere.updateGareRoutiere(crlGareRoutiere gareRoutiere)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            #endregion

            #region implementation
            if (gareRoutiere != null)
            {
                if (gareRoutiere.numerosGareRoutiere != "")
                {
                    this.strCommande = "UPDATE `gareRoutiere` SET `nomProvince` = '" + gareRoutiere.nomProvince + "', `numVille`='" + gareRoutiere.NumVille + "',";
                    this.strCommande += "`localisation`='" + gareRoutiere.localisation + "', `sigleGare`='" + gareRoutiere.sigleGare + "' ";
                    this.strCommande += "WHERE (`numerosGareRoutiere`='" + gareRoutiere.numerosGareRoutiere + "')";
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

        bool IntfDalGareRoutiere.isGareRoutiere(crlGareRoutiere gareRoutiere)
        {
            #region declaration
            bool isGareRoutiere = false;
            #endregion

            #region implementation
            if (gareRoutiere != null)
            {
                
                this.strCommande = "SELECT * FROM `gareRoutiere` WHERE (`numerosGareRoutiere`<>'" + gareRoutiere.numerosGareRoutiere+ "')";
                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            if (gareRoutiere.sigleGare.Trim().ToLower().Equals(reader["sigleGare"].ToString().Trim().ToLower()))
                            {
                                isGareRoutiere = true;
                                break;
                            }
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isGareRoutiere;
        }

        crlGareRoutiere IntfDalGareRoutiere.selectGareRoutiere(string numerosGareRoutiere)
        {
            #region declaration
            crlGareRoutiere gareRoutiere = null;
            crlProvince province = null;
            IntfDalVille serviceVille = new ImplDalVille(); 
            #endregion

            #region implementation
            if (numerosGareRoutiere != "")
            {
                this.strCommande = "SELECT * FROM `gareRoutiere` WHERE (`numerosGareRoutiere`='" + numerosGareRoutiere + "')";
                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            gareRoutiere = new crlGareRoutiere();
                            province = new crlProvince();
                            gareRoutiere.numerosGareRoutiere = reader["numerosGareRoutiere"].ToString();
                            gareRoutiere.nomProvince = reader["nomProvince"].ToString();
                            gareRoutiere.localisation = reader["localisation"].ToString();
                            gareRoutiere.sigleGare = reader["sigleGare"].ToString();
                            gareRoutiere.NumVille = reader["numVille"].ToString();
                            province.nomProvince = reader["nomProvince"].ToString();
                            gareRoutiere.province = province;
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                if (gareRoutiere != null)
                {
                    if (gareRoutiere.NumVille != "")
                    {
                        gareRoutiere.ville = serviceVille.selectVille(gareRoutiere.NumVille);
                    }
                }
            }
            #endregion

            return gareRoutiere;
        }

        string IntfDalGareRoutiere.getNumerosGareRoutiere(string nomProvince)
        {
            #region declaration
            int numTemp = 0;
            string numerosGareRoutiere = "0001";
            string[] tempNumerosGareRoutiere = null;
            #endregion

            #region implementation
            this.strCommande = "SELECT gareroutiere.numerosGareRoutiere AS maxNum FROM gareroutiere ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumerosGareRoutiere = reader["maxNum"].ToString().Split('/');
                        numerosGareRoutiere = tempNumerosGareRoutiere[0];
                    }
                    numTemp = int.Parse(numerosGareRoutiere) + 1;
                    if (numTemp < 10)
                        numerosGareRoutiere = "000" + numTemp;
                    if (numTemp < 100 && numTemp >= 10)
                        numerosGareRoutiere = "00" + numTemp;
                    if (numTemp < 1000 && numTemp >=100)
                        numerosGareRoutiere = "0" + numTemp;
                    if (numTemp >= 1000)
                        numerosGareRoutiere = "" + numTemp;
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numerosGareRoutiere = numerosGareRoutiere + "/GR-" + nomProvince.Substring(0, 3).ToUpper() + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString("0000");
            #endregion

            return numerosGareRoutiere;
        }

        void IntfDalGareRoutiere.loadDdlTri(DropDownList ddlTri)
        {
            ddlTri.Items.Clear();
            ddlTri.Items.Add(new ListItem("Numeros", "numerosGareRoutiere"));
            ddlTri.Items.Add(new ListItem("Province", "nomProvince"));
            ddlTri.Items.Add(new ListItem("Localisation", "localisation"));
            ddlTri.Items.Add(new ListItem("Sigle", "sigleGare"));
        }

        #endregion
    }
}
