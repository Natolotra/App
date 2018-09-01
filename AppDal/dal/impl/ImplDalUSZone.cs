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
    /// Summary description for ImplDalUSZone
    /// </summary>
    public class ImplDalUSZone : IntfDalUSZone
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalUSZone(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalUSZone()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion

        #region IntfDalUSZone Members

        string IntfDalUSZone.insertUSZone(crlUSZone zone, string sigleAgence)
        {
            #region declaration
            string numZone = "";
            IntfDalUSZone serviceUSZone = new ImplDalUSZone();
            int nbInsert = 0;
            #endregion

            #region implemenation
            if (zone != null && sigleAgence != "")
            {
                zone.NumZone = serviceUSZone.getNumUSZone(sigleAgence);
                this.strCommande = "INSERT INTO `uszone` (`numZone`,`nomZone`,`niveau`,`numCommune`)";
                this.strCommande += " VALUES ('" + zone.NumZone + "','" + zone.NomZone + "',";
                this.strCommande += " '" + zone.Niveau.ToString("0") + "','" + zone.NumCommune + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    numZone = zone.NumZone;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numZone;
        }

        bool IntfDalUSZone.updateUSZone(crlUSZone zone)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            #endregion

            #region implementation
            if (zone != null)
            {
                this.strCommande = "UPDATE `uszone` SET `nomZone`='" + zone.NomZone + "',";
                this.strCommande += " `niveau`='" + zone.Niveau + "',`numCommune`='" + zone.NumCommune + "'";
                this.strCommande += " WHERE `numZone`='" + zone.NumZone + "'";

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

        crlUSZone IntfDalUSZone.selectUSZone(string numZone)
        {
            #region declaration
            crlUSZone zone = null;
            #endregion

            #region implementation
            if (numZone != "")
            {
                this.strCommande = "SELECT * FROM `uszone` WHERE (`numZone`='" + numZone + "')";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            zone = new crlUSZone();
                            zone.NumZone = this.reader["numZone"].ToString();
                            zone.NomZone = this.reader["nomZone"].ToString();
                            try
                            {
                                zone.Niveau = int.Parse(this.reader["niveau"].ToString());
                            }
                            catch (Exception) { }
                            zone.NumCommune = this.reader["numCommune"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return zone;
        }

        string IntfDalUSZone.getNumUSZone(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numZone = "00001";
            string[] tempNumZone = null;
            string strDate = "ZO" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT uszone.numZone AS maxNum FROM uszone";
            this.strCommande += " WHERE uszone.numZone LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumZone = reader["maxNum"].ToString().ToString().Split('/');
                        numZone = tempNumZone[tempNumZone.Length - 1];
                    }
                    numTemp = double.Parse(numZone) + 1;
                    if (numTemp < 10)
                        numZone = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numZone = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numZone = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numZone = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numZone = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numZone = strDate + "/" + sigleAgence + "/" + numZone;
            #endregion

            return numZone;
        }

        string IntfDalUSZone.isUSZone(crlUSZone zone)
        {
            #region declaration
            string numZone = "";
            #endregion

            #region implementation
            if (zone != null)
            {
                this.strCommande = "SELECT * FROM `uszone` WHERE (`numZone`<>'" + zone.NumZone + "'";
                this.strCommande += " AND `nomZone`='" + zone.NomZone + "' AND `umCommune`='" + zone.NumCommune + "')";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            numZone = this.reader["numZone"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numZone;
        }

       


        void IntfDalUSZone.loadDdlZoneUS(DropDownList ddl)
        {
            #region implementation
            if (ddl != null)
            {
                ddl.Items.Clear();
                ddl.Items.Add("");

                this.strCommande = "SELECT * FROM `uszone`";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            ddl.Items.Add(new ListItem(this.reader["nomZone"].ToString(), this.reader["numZone"].ToString()));
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }

        void IntfDalUSZone.loadDdlZoneUSCommune(DropDownList ddl, string numCommune)
        {
            #region implementation
            if (ddl != null)
            {
                ddl.Items.Clear();
                ddl.Items.Add("");

                this.strCommande = "SELECT * FROM `uszone` WHERE `numCommune`='" + numCommune + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            ddl.Items.Add(new ListItem(this.reader["nomZone"].ToString(), this.reader["numZone"].ToString()));
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }

        void IntfDalUSZone.loadDdlZoneUS(DropDownList ddl, string numZone)
        {
            #region implementation
            if (ddl != null)
            {
                ddl.Items.Clear();
                ddl.Items.Add("");

                if (numZone != "")
                {
                    this.strCommande = "SELECT * FROM `uszone` WHERE `numZone`<> '" + numZone + "'";

                    this.serviceConnectBase.openConnection();
                    this.reader = this.serviceConnectBase.select(this.strCommande);
                    if (this.reader != null)
                    {
                        if (this.reader.HasRows)
                        {
                            while (this.reader.Read())
                            {
                                ddl.Items.Add(new ListItem(this.reader["nomZone"].ToString(), this.reader["numZone"].ToString()));
                            }
                        }
                        this.reader.Dispose();
                    }
                    this.serviceConnectBase.closeConnection();
                }
            }
            #endregion
        }

        #endregion





        #region IntfDalUSZone Members


        string IntfDalUSZone.getNumCommune(string numZone)
        {
            #region declaration
            string numCommune = "";
            #endregion

            #region implementation
            if (numZone != "") 
            {
                this.strCommande = "SELECT uszone.numCommune FROM uszone";
                this.strCommande += " WHERE uszone.numZone = '" + numZone + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            numCommune = this.reader["numCommune"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numCommune;
        }

        #endregion
    }
}
