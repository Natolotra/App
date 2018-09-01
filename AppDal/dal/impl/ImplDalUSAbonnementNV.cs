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
using System.Collections.Generic;

namespace arch.dal.impl
{
    /// <summary>
    /// Implementation du service USAbonnementNV
    /// </summary>
    public class ImplDalUSAbonnementNV : IntfDalUSAbonnementNV
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalUSAbonnementNV(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalUSAbonnementNV()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion

        #region IntfDalUSAbonnementNV Members

        string IntfDalUSAbonnementNV.insertUSAbonnementNV(crlUSAbonnementNV abonnementNV, string sigleAgence)
        {
            #region declaration
            IntfDalUSAbonnementNV serviceUSAbonnementNV = new ImplDalUSAbonnementNV();
            string numAbonnementNV = "";
            int nbInsert = 0;
            string numCarte = "NULL";
            string numAbonnement = "NULL";
            #endregion

            #region implementation
            if (abonnementNV != null) 
            {

                if (abonnementNV.NumCarte != "")
                {
                    numCarte = "'" + abonnementNV.NumCarte + "'";
                }
                if (abonnementNV.NumAbonnement != "")
                {
                    numAbonnement = "'" + abonnementNV.NumAbonnement + "'";
                }
                abonnementNV.NumAbonnementNV = serviceUSAbonnementNV.getNumUSAbonnementNV(sigleAgence);
                this.strCommande = "INSERT INTO `usabonnementnv` (`numAbonnementNV`,`dateValideAu`,`numAbonnement`,";
                this.strCommande += " `numZoneD`,`numZoneF`,`numCarte`) VALUES";
                this.strCommande += " ('" + abonnementNV.NumAbonnementNV + "','" + abonnementNV.DateValideAu.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                this.strCommande += " " + numAbonnement + ",";
                this.strCommande += " '" + abonnementNV.NumZoneD + "','" + abonnementNV.NumZoneF + "',";
                this.strCommande += " " + numCarte + ")";
                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1) 
                {
                    numAbonnementNV = abonnementNV.NumAbonnementNV;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numAbonnementNV;
        }

        bool IntfDalUSAbonnementNV.updateUSAbonnementNV(crlUSAbonnementNV abonnementNV)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            string numCarte = "NULL";
            string numAbonnement = "NULL";
            #endregion

            #region implementation
            if (abonnementNV != null) 
            {

                if (abonnementNV.NumCarte != "")
                {
                    numCarte = "'" + abonnementNV.NumCarte + "'";
                }
                if (abonnementNV.NumAbonnement != "")
                {
                    numAbonnement = "'" + abonnementNV.NumAbonnement + "'";
                }
                this.strCommande = "UPDATE `usabonnementnv` SET `dateValideAu`='" + abonnementNV.DateValideAu.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                this.strCommande += " `numAbonnement`=" + numAbonnement + ",";
                this.strCommande += " `numZoneD`='" + abonnementNV.NumZoneD + "',`numZoneF`='" + abonnementNV.NumZoneF + "',";
                this.strCommande += " `numCarte`=" + numCarte;
                this.strCommande += " WHERE `numAbonnementNV`='" + abonnementNV.NumAbonnementNV + "'";
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

        crlUSAbonnementNV IntfDalUSAbonnementNV.selectUSAbonnementNV(string numAbonnementNV)
        {
            #region declaration
            crlUSAbonnementNV abonnementNV = null;
            IntfDalUSAbonnementNV serviceUSAbonnementNV = new ImplDalUSAbonnementNV();
            #endregion

            #region implementation
            if (numAbonnementNV != "") 
            {
                this.strCommande = "SELECT * FROM `usabonnementnv` WHERE `numAbonnementNV`='" + numAbonnementNV + "'";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            abonnementNV = new crlUSAbonnementNV();
                            try
                            {
                                abonnementNV.DateValideAu = Convert.ToDateTime(this.reader["dateValideAu"].ToString());
                            }
                            catch (Exception) { }
                            
                            abonnementNV.NumAbonnement = this.reader["numAbonnement"].ToString();
                            abonnementNV.NumAbonnementNV = this.reader["numAbonnementNV"].ToString();
                            abonnementNV.NumZoneD = this.reader["numZoneD"].ToString();
                            abonnementNV.NumZoneF = this.reader["numZoneF"].ToString();
                            abonnementNV.NumCarte = this.reader["numCarte"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (abonnementNV != null)
                {
                    if (abonnementNV.NumAbonnementNV != "")
                    {
                        abonnementNV.abonnementNVDevis = serviceUSAbonnementNV.getAbonnementNVDevisValide(abonnementNV.NumAbonnementNV); 
                    }
                }
            }
            #endregion

            return abonnementNV;
        }

        crlUSAbonnementNV IntfDalUSAbonnementNV.selectUSAbonnementNVCarte(string numCarte)
        {
            #region declaration
            crlUSAbonnementNV abonnementNV = null;
            IntfDalUSAbonnementNV serviceUSAbonnementNV = new ImplDalUSAbonnementNV();
            #endregion

            #region implementation
            if (numCarte != "")
            {
                this.strCommande = "SELECT * FROM `usabonnementnv` WHERE `numCarte`='" + numCarte + "'";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            abonnementNV = new crlUSAbonnementNV();
                            try
                            {
                                abonnementNV.DateValideAu = Convert.ToDateTime(this.reader["dateValideAu"].ToString());
                            }
                            catch (Exception)
                            {
                            }

                            abonnementNV.NumAbonnement = this.reader["numAbonnement"].ToString();
                            abonnementNV.NumAbonnementNV = this.reader["numAbonnementNV"].ToString();
                            abonnementNV.NumZoneD = this.reader["numZoneD"].ToString();
                            abonnementNV.NumZoneF = this.reader["numZoneF"].ToString();
                            abonnementNV.NumCarte = this.reader["numCarte"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (abonnementNV != null)
                {
                    if (abonnementNV.NumAbonnementNV != "")
                    {
                        abonnementNV.abonnementNVDevis = serviceUSAbonnementNV.getAbonnementNVDevisValide(abonnementNV.NumAbonnementNV);
                    }
                }
            }
            #endregion

            return abonnementNV;
        }

        string IntfDalUSAbonnementNV.getNumUSAbonnementNV(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numAbonnementNV = "00001";
            string[] tempNumAbonnementNV = null;
            string strDate = "AN" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT usabonnementnv.numAbonnementNV AS maxNum FROM usabonnementnv";
            this.strCommande += " WHERE usabonnementnv.numAbonnementNV LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumAbonnementNV = reader["maxNum"].ToString().ToString().Split('/');
                        numAbonnementNV = tempNumAbonnementNV[tempNumAbonnementNV.Length - 1];
                    }
                    numTemp = double.Parse(numAbonnementNV) + 1;
                    if (numTemp < 10)
                        numAbonnementNV = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numAbonnementNV = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numAbonnementNV = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numAbonnementNV = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numAbonnementNV = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numAbonnementNV = strDate + "/" + sigleAgence + "/" + numAbonnementNV;
            #endregion

            return numAbonnementNV;
        }

        List<crlUSAbonnementNVDevis> IntfDalUSAbonnementNV.getAbonnementNVDevisValide(string numAbonnementNV)
        {
            #region declaration
            List<crlUSAbonnementNVDevis> abonnementNVDevisValide = null;
            crlUSAbonnementNVDevis abonnementNVDevisTemp = null;
            IntfDalUSZone serviceUSZone = new ImplDalUSZone();
            IntfDalUSInfoPasse serviceUSInfoPasse = new ImplDalUSInfoPasse();
            #endregion

            #region implementation
            if (numAbonnementNV != "")
            {
                this.strCommande = "SELECT usabonnementnvdevis.numAbonnementNVDevis, usabonnementnvdevis.numAbonnement,";
                this.strCommande += " usabonnementnvdevis.prixUnitaireNV, usabonnementnvdevis.montantNV,";
                this.strCommande += " usabonnementnvdevis.numZoneD, usabonnementnvdevis.numZoneF,";
                this.strCommande += " usabonnementnvdevis.numReductionBillet, usabonnementnvdevis.numCategorieBillet,";
                this.strCommande += " usabonnementnvdevis.numProforma, usabonnementnvdevis.numInfoPasse,";
                this.strCommande += " usabonnementnvdevis.numCarte, usabonnementnvdevis.montantCarte,";
                this.strCommande += " usabonnementnvdevis.dateAbonnementNVDevis, usabonnementnvdevis.numAbonnementNV,";
                this.strCommande += " usabonnementnvdevis.nombreVoyage FROM usabonnementnvdevis";
                this.strCommande += " WHERE usabonnementnvdevis.numAbonnementNV = '" + numAbonnementNV + "' AND";
                this.strCommande += " usabonnementnvdevis.nombreVoyage > '0'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        abonnementNVDevisValide = new List<crlUSAbonnementNVDevis>();
                        while (this.reader.Read())
                        {
                            abonnementNVDevisTemp = new crlUSAbonnementNVDevis();
                            try
                            {
                                abonnementNVDevisTemp.DateAbonnementNVDevis = Convert.ToDateTime(this.reader["dateAbonnementNVDevis"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                abonnementNVDevisTemp.MontantCarte = double.Parse(this.reader["montantCarte"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                abonnementNVDevisTemp.MontantNV = double.Parse(this.reader["montantNV"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                abonnementNVDevisTemp.NombreVoyage = int.Parse(this.reader["nombreVoyage"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            abonnementNVDevisTemp.NumAbonnement = this.reader["numAbonnement"].ToString();
                            abonnementNVDevisTemp.NumAbonnementNV = this.reader["numAbonnementNV"].ToString();
                            abonnementNVDevisTemp.NumAbonnementNVDevis = this.reader["numAbonnementNVDevis"].ToString();
                            abonnementNVDevisTemp.NumCarte = this.reader["numCarte"].ToString();
                            abonnementNVDevisTemp.NumCategorieBillet = this.reader["numCategorieBillet"].ToString();
                            abonnementNVDevisTemp.NumInfoPasse = this.reader["numInfoPasse"].ToString();
                            abonnementNVDevisTemp.NumProforma = this.reader["numProforma"].ToString();
                            abonnementNVDevisTemp.NumReductionBillet = this.reader["numReductionBillet"].ToString();
                            abonnementNVDevisTemp.NumZoneD = this.reader["numZoneD"].ToString();
                            abonnementNVDevisTemp.NumZoneF = this.reader["numZoneF"].ToString();
                            try
                            {
                                abonnementNVDevisTemp.PrixUnitaireNV = double.Parse(this.reader["prixUnitaireNV"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            if (abonnementNVDevisTemp.NumInfoPasse != "")
                            {
                                abonnementNVDevisTemp.infoPasse = serviceUSInfoPasse.selectUSInfoPasse(abonnementNVDevisTemp.NumInfoPasse);
                            }
                            if (abonnementNVDevisTemp.NumZoneD != "")
                            {
                                abonnementNVDevisTemp.zoneD = serviceUSZone.selectUSZone(abonnementNVDevisTemp.NumZoneD);
                            }
                            if (abonnementNVDevisTemp.NumZoneF != "")
                            {
                                abonnementNVDevisTemp.zoneF = serviceUSZone.selectUSZone(abonnementNVDevisTemp.NumZoneF);
                            }


                            abonnementNVDevisValide.Add(abonnementNVDevisTemp);
                            abonnementNVDevisTemp = null;

                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return abonnementNVDevisValide;
        }

        int IntfDalUSAbonnementNV.getNombreVoyageAbonnementNV(string numAbonnementNV)
        {
            #region declaration
            int nombreVoyage = 0;
            #endregion

            #region implementation
            if (numAbonnementNV != "")
            {
                this.strCommande = "SELECT Sum(usabonnementnvdevis.nombreVoyage) AS nombreVoyage";
                this.strCommande += " FROM usabonnementnvdevis";
                this.strCommande += " WHERE usabonnementnvdevis.numAbonnementNV = '" + numAbonnementNV + "'";

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
                                nombreVoyage = int.Parse(this.reader["nombreVoyage"].ToString());
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

            return nombreVoyage;
        }

        void IntfDalUSAbonnementNV.loadDdlAbonnementNVNonCarte(DropDownList ddl, string numAbonnement)
        {
            #region declaration
            crlUSZone zoneD = null;
            crlUSZone zoneF = null;
            IntfDalUSZone serviceUSZone = new ImplDalUSZone();
            #endregion

            #region implementation
            if (ddl != null)
            {
                ddl.Items.Clear();
                ddl.Items.Add("");

                this.strCommande = "SELECT usabonnementnv.numZoneD, usabonnementnv.numZoneF,";
                this.strCommande += " usabonnementnv.numAbonnementNV FROM usabonnementnv";
                this.strCommande += " Left Join uscarte ON uscarte.numAbonnementNV = usabonnementnv.numAbonnementNV";
                this.strCommande += " WHERE usabonnementnv.numAbonnement = '" + numAbonnement + "' AND";
                this.strCommande += " uscarte.numAbonnementNV IS NULL";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            zoneD = serviceUSZone.selectUSZone(this.reader["numZoneD"].ToString());
                            zoneF = serviceUSZone.selectUSZone(this.reader["numZoneF"].ToString());

                            if (zoneD != null && zoneF != null)
                            {
                                ddl.Items.Add(new ListItem(zoneD.NomZone + "-" + zoneF.NomZone, this.reader["numAbonnementNV"].ToString()));
                            }
                            else
                            {
                                ddl.Items.Add(new ListItem(this.reader["numZoneD"].ToString() + "-" + this.reader["numZoneF"].ToString(), this.reader["numAbonnementNV"].ToString()));
                            }
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }

        #endregion


        void IntfDalUSAbonnementNV.insertToGridAbonnementNV(GridView gridView, string param, string paramLike, string valueLike, string numAbonnement)
        {
            #region declaration
            IntfDalUSAbonnementNV serviceUSAbonnementNV = new ImplDalUSAbonnementNV();
            #endregion

            #region implementation
            this.strCommande = "SELECT usabonnementnv.numAbonnementNV,";
            this.strCommande += " usabonnementnv.dateValideAu,";
            this.strCommande += " usabonnementnv.numAbonnement,";
            this.strCommande += " usabonnementnv.numZoneD, usabonnementnv.numZoneF,";
            this.strCommande += " usabonnementnv.numCarte, Sum(usabonnementnvdevis.nombreVoyage) AS nbVoyage";
            this.strCommande += " FROM usabonnementnv";
            this.strCommande += " Inner Join usabonnementnvdevis ON usabonnementnvdevis.numAbonnementNV = usabonnementnv.numAbonnementNV";
            this.strCommande += " WHERE usabonnementnvdevis.nombreVoyage > '0' AND";
            this.strCommande += " " + paramLike + " LIKE '%" + valueLike + "%' AND";
            this.strCommande += " usabonnementnv.numAbonnement = '" + numAbonnement + "'";
            this.strCommande += " GROUP BY usabonnementnv.numAbonnementNV";
            this.strCommande += " ORDER BY " + param + " ASC";

            gridView.DataSource = serviceUSAbonnementNV.getDataTableAbonnementNV(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalUSAbonnementNV.getDataTableAbonnementNV(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();

            IntfDalUSZone serviceUSZone = new ImplDalUSZone();
            crlUSZone zoneD = null;
            crlUSZone zoneF = null;
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numAbonnementNV", typeof(string));
            dataTable.Columns.Add("dateValideAu", typeof(DateTime));
            dataTable.Columns.Add("nombreVoyage", typeof(string));
            dataTable.Columns.Add("zone", typeof(string));
            dataTable.Columns.Add("numCarte", typeof(string));

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

                        dr["numAbonnementNV"] = reader["numAbonnementNV"].ToString();
                        dr["dateValideAu"] = Convert.ToDateTime(reader["dateValideAu"].ToString());
                        dr["nombreVoyage"] = reader["nbVoyage"].ToString();

                        zoneD = serviceUSZone.selectUSZone(reader["numZoneD"].ToString());
                        zoneF = serviceUSZone.selectUSZone(reader["numZoneF"].ToString());

                        if (zoneD != null && zoneF != null)
                        {
                            dr["zone"] = zoneD.NomZone + "-" + zoneF.NomZone;
                        }
                        else
                        {
                            dr["zone"] = reader["numZoneD"].ToString() + "-" + reader["numZoneF"].ToString();
                        }

                        dr["numCarte"] = reader["numCarte"].ToString();


                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }



        
    }
}
