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
    /// Implementation du service abonnementNVDevis
    /// </summary>
    public class ImplDalUSAbonnementNVDevis : IntfDalUSAbonnementNVDevis
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalUSAbonnementNVDevis(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalUSAbonnementNVDevis()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion

        #region IntfDalUSAbonnementNVDevis Members

        string IntfDalUSAbonnementNVDevis.insertUSAbonnementNVDevis(crlUSAbonnementNVDevis abonnementNVDevis, string sigleAgence)
        {
            #region declaration
            IntfDalUSAbonnementNVDevis serviceUSAbonnementNVDevis = new ImplDalUSAbonnementNVDevis();
            string numAbonnementNVDevis = "";
            int nbInsert = 0;
            string numReductionBillet = "NULL";
            string numCategorieBillet = "NULL";
            string numAbonnementNV = "NULL";
            string numAbonnement = "NULL";
            #endregion

            #region implementation
            if (abonnementNVDevis != null)
            {
                
                if (abonnementNVDevis.NumReductionBillet != "")
                {
                    numReductionBillet = "'" + abonnementNVDevis.NumReductionBillet + "'";
                }
                if (abonnementNVDevis.NumCategorieBillet != "")
                {
                    numCategorieBillet = "'" + abonnementNVDevis.NumCategorieBillet + "'";
                }
                if (abonnementNVDevis.NumAbonnementNV != "")
                {
                    numAbonnementNV = "'" + abonnementNVDevis.NumAbonnementNV + "'";
                }
                if (abonnementNVDevis.NumAbonnement != "")
                {
                    numAbonnement = "'" + abonnementNVDevis.NumAbonnement + "'";
                }

                abonnementNVDevis.NumAbonnementNVDevis = serviceUSAbonnementNVDevis.getNumUSAbonnementNVDevis(sigleAgence);
                this.strCommande = "INSERT INTO `usabonnementnvdevis` (`numAbonnementNVDevis`,`numAbonnement`,";
                this.strCommande += " `montantNV`,`numZoneD`,`numZoneF`,`numReductionBillet`,`numCategorieBillet`,";
                this.strCommande += " `prixUnitaireNV`,`numProforma`,`numInfoPasse`,`numCarte`,`montantCarte`,";
                this.strCommande += " `dateAbonnementNVDevis`,`numAbonnementNV`,`nombreVoyage`) VALUES";
                this.strCommande += " ('" + abonnementNVDevis.NumAbonnementNVDevis + "',";
                this.strCommande += " " + numAbonnement + ",";
                this.strCommande += " '" + abonnementNVDevis.MontantNV.ToString("0") + "','" + abonnementNVDevis.NumZoneD + "',";
                this.strCommande += " '" + abonnementNVDevis.NumZoneF + "',";
                this.strCommande += " " + numReductionBillet + "," + numCategorieBillet + ",'" + abonnementNVDevis.PrixUnitaireNV.ToString("0") + "',";
                this.strCommande += " '" + abonnementNVDevis.NumProforma + "','" + abonnementNVDevis.NumInfoPasse + "',";
                this.strCommande += " '" + abonnementNVDevis.NumCarte + "','" + abonnementNVDevis.MontantCarte.ToString("0") + "',";
                this.strCommande += " '" + abonnementNVDevis.DateAbonnementNVDevis.ToString("yyyy-MM-dd") + "',";
                this.strCommande += " " + numAbonnementNV + ",'" + abonnementNVDevis.NombreVoyage + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    numAbonnementNVDevis = abonnementNVDevis.NumAbonnementNVDevis;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numAbonnementNVDevis;
        }

        bool IntfDalUSAbonnementNVDevis.updateUSAbonnementNVDevis(crlUSAbonnementNVDevis abonnementNVDevis)
        {
            #region declaration
            bool isUpdate = false;
            string numReductionBillet = "NULL";
            string numCategorieBillet = "NULL";
            string numAbonnementNV = "NULL";
            string numAbonnement = "NULL";
            int nbUpdate = 0;
            #endregion

            #region implementation
            if (abonnementNVDevis != null)
            {
                
                if (abonnementNVDevis.NumReductionBillet != "")
                {
                    numReductionBillet = "'" + abonnementNVDevis.NumReductionBillet + "'";
                }
                if (abonnementNVDevis.NumCategorieBillet != "")
                {
                    numCategorieBillet = "'" + abonnementNVDevis.NumCategorieBillet + "'";
                }
                if (abonnementNVDevis.NumAbonnementNV != "")
                {
                    numAbonnementNV = "'" + abonnementNVDevis.NumAbonnementNV + "'";
                }
                if (abonnementNVDevis.NumAbonnement != "")
                {
                    numAbonnement = "'" + abonnementNVDevis.NumAbonnement + "'";
                }
                this.strCommande = "UPDATE `usabonnementnvdevis` SET ";
                this.strCommande += " `numAbonnement`=" + numAbonnement + ",";
                this.strCommande += " `montantNV`='" + abonnementNVDevis.MontantNV.ToString("0") + "',`numZoneD`='" + abonnementNVDevis.NumZoneD + "',";
                this.strCommande += " `numZoneF`='" + abonnementNVDevis.NumZoneF + "',`numProforma`='" + abonnementNVDevis.NumProforma + "',";
                this.strCommande += " `numReductionBillet`=" + numReductionBillet + ",`numCategorieBillet`=" + numCategorieBillet + ",";
                this.strCommande += " `prixUnitaireNV`='" + abonnementNVDevis.PrixUnitaireNV.ToString("0") + "',";
                this.strCommande += " `numInfoPasse`='" + abonnementNVDevis.NumInfoPasse + "',`numCarte`='" + abonnementNVDevis.NumCarte + "',";
                this.strCommande += " `montantCarte`='" + abonnementNVDevis.MontantCarte.ToString("0") + "',";
                this.strCommande += " `dateAbonnementNVDevis`='" + abonnementNVDevis.DateAbonnementNVDevis.ToString("yyyy-MM-dd") + "',";
                this.strCommande += " `numAbonnementNV`=" + numAbonnementNV + ",`nombreVoyage`='" + abonnementNVDevis.NombreVoyage + "'";
                this.strCommande += " WHERE `numAbonnementNVDevis`='" + abonnementNVDevis.NumAbonnementNVDevis + "'";
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

        crlUSAbonnementNVDevis IntfDalUSAbonnementNVDevis.selectUSAbonnementNVDevis(string numAbonnementNVDevis)
        {
            #region declaration
            crlUSAbonnementNVDevis abonnementNVDevis = null;
            IntfDalUSInfoPasse serviceUSInfoPasse = new ImplDalUSInfoPasse();
            IntfDalUSZone serviceUSZone = new ImplDalUSZone();
            #endregion

            #region implementation
            if (numAbonnementNVDevis != "")
            {
                this.strCommande = "SELECT * FROM `usabonnementnvdevis` WHERE `numAbonnementNVDevis`='" + numAbonnementNVDevis + "'";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            abonnementNVDevis = new crlUSAbonnementNVDevis();

                            abonnementNVDevis.NumProforma = this.reader["numProforma"].ToString();
                            try
                            {
                                abonnementNVDevis.MontantNV = double.Parse(this.reader["montantNV"].ToString());
                            }
                            catch (Exception) { }
                            
                            try
                            {
                                abonnementNVDevis.PrixUnitaireNV = double.Parse(this.reader["prixUnitaireNV"].ToString());
                            }
                            catch (Exception) { }
                            abonnementNVDevis.NumAbonnement = this.reader["numAbonnement"].ToString();
                            abonnementNVDevis.NumAbonnementNVDevis = this.reader["numAbonnementNVDevis"].ToString();
                            abonnementNVDevis.NumCategorieBillet = this.reader["numCategorieBillet"].ToString();
                            abonnementNVDevis.NumReductionBillet = this.reader["numReductionBillet"].ToString();
                            abonnementNVDevis.NumZoneD = this.reader["numZoneD"].ToString();
                            abonnementNVDevis.NumZoneF = this.reader["numZoneF"].ToString();
                            abonnementNVDevis.NumInfoPasse = this.reader["numInfoPasse"].ToString();
                            abonnementNVDevis.NumCarte = this.reader["numCarte"].ToString();
                            try
                            {
                                abonnementNVDevis.MontantCarte = double.Parse(this.reader["montantCarte"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                abonnementNVDevis.DateAbonnementNVDevis = Convert.ToDateTime(this.reader["dateAbonnementNVDevis"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            abonnementNVDevis.NumAbonnementNV = this.reader["numAbonnementNV"].ToString();
                            try
                            {
                                abonnementNVDevis.NombreVoyage = int.Parse(this.reader["nombreVoyage"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (abonnementNVDevis != null)
                {
                    if (abonnementNVDevis.NumInfoPasse != "")
                    {
                        abonnementNVDevis.infoPasse = serviceUSInfoPasse.selectUSInfoPasse(abonnementNVDevis.NumInfoPasse);
                    }
                    if (abonnementNVDevis.NumZoneD != "")
                    {
                        abonnementNVDevis.zoneD = serviceUSZone.selectUSZone(abonnementNVDevis.NumZoneD);
                    }
                    if (abonnementNVDevis.NumZoneF != "")
                    {
                        abonnementNVDevis.zoneF = serviceUSZone.selectUSZone(abonnementNVDevis.NumZoneF);
                    }
                }
            }
            #endregion

            return abonnementNVDevis;
        }

        string IntfDalUSAbonnementNVDevis.getNumUSAbonnementNVDevis(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numAbonnementNVDevis = "00001";
            string[] tempNumAbonnementNVDevis = null;
            string strDate = "AU" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT usabonnementnvdevis.numAbonnementNVDevis AS maxNum FROM usabonnementnvdevis";
            this.strCommande += " WHERE usabonnementnvdevis.numAbonnementNVDevis LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumAbonnementNVDevis = reader["maxNum"].ToString().ToString().Split('/');
                        numAbonnementNVDevis = tempNumAbonnementNVDevis[tempNumAbonnementNVDevis.Length - 1];
                    }
                    numTemp = double.Parse(numAbonnementNVDevis) + 1;
                    if (numTemp < 10)
                        numAbonnementNVDevis = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numAbonnementNVDevis = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numAbonnementNVDevis = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numAbonnementNVDevis = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numAbonnementNVDevis = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numAbonnementNVDevis = strDate + "/" + sigleAgence + "/" + numAbonnementNVDevis;
            #endregion

            return numAbonnementNVDevis;
        }




        crlUSAbonnementNV IntfDalUSAbonnementNVDevis.getUSAbonnementNV(crlUSAbonnementNVDevis abonnementNVDevis)
        {
            #region declaration
            crlUSAbonnementNV abonnementNV = null;
            crlUSPlageNombreBillet plageNombreBillet = null;
            IntfDalUSPlageNombreBillet serviceUSPlageNombreBillet = new ImplDalUSPlageNombreBillet();
            #endregion

            #region implementation
            if (abonnementNVDevis != null)
            {
                abonnementNV = new crlUSAbonnementNV();
                abonnementNV.NumAbonnement = abonnementNVDevis.NumAbonnement;
                abonnementNV.NumZoneD = abonnementNVDevis.NumZoneD;
                abonnementNV.NumZoneF = abonnementNVDevis.NumZoneF;
                abonnementNV.NumCarte = abonnementNVDevis.NumCarte;
                plageNombreBillet = serviceUSPlageNombreBillet.getPlageNombreBillet(abonnementNVDevis.infoPasse.NombrePasse);
                if (plageNombreBillet != null)
                {
                    abonnementNV.DateValideAu = DateTime.Now.Add(plageNombreBillet.DureeDeValidite);
                }
            }
            #endregion

            return abonnementNV;
        }

        crlUSAbonnementNV IntfDalUSAbonnementNVDevis.getUSAbonnementNV(crlUSAbonnementNVDevis abonnementNVDevis, crlUSCarte carte)
        {
            #region declaration
            crlUSAbonnementNV abonnementNV = null;
            crlUSPlageNombreBillet plageNombreBillet = null;
            IntfDalUSPlageNombreBillet serviceUSPlageNombreBillet = new ImplDalUSPlageNombreBillet();
            IntfDalUSAbonnementNV serviceUSAbonnementNV = new ImplDalUSAbonnementNV();
            #endregion

            #region implementation
            if (abonnementNVDevis != null && carte != null)
            {
                abonnementNV = serviceUSAbonnementNV.selectUSAbonnementNV(carte.NumAbonnementNV);
                if (abonnementNV != null)
                {
                    abonnementNV.NumAbonnement = abonnementNVDevis.NumAbonnement;
                    abonnementNV.NumZoneD = abonnementNVDevis.NumZoneD;
                    abonnementNV.NumZoneF = abonnementNVDevis.NumZoneF;
                    abonnementNV.NumCarte = abonnementNVDevis.NumCarte;
                    plageNombreBillet = serviceUSPlageNombreBillet.getPlageNombreBillet(abonnementNVDevis.infoPasse.NombrePasse);
                    if (plageNombreBillet != null)
                    {
                        if (abonnementNV.DateValideAu > DateTime.Now)
                        {
                            abonnementNV.DateValideAu = abonnementNV.DateValideAu.Add(plageNombreBillet.DureeDeValidite);
                        }
                        else
                        {
                            abonnementNV.DateValideAu = DateTime.Now.Add(plageNombreBillet.DureeDeValidite);
                        }
                    }
                }
            }
            #endregion

            return abonnementNV;
        }




        bool IntfDalUSAbonnementNVDevis.deleteUSAbonnementNVDevis(string numAbonnementNVDevis)
        {
            #region declaration
            bool isUpdate = false;
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            #endregion

            #region implementation
            if (numAbonnementNVDevis != "")
            {
                this.strCommande = "UPDATE `uscarte` SET `numAbonnementNVDevis`=NULL";
                this.strCommande += " WHERE `numAbonnementNVDevis`='" + numAbonnementNVDevis + "'";

                this.serviceConnectBase.openConnection();
                this.serviceConnectBase.requete(this.strCommande);
                this.serviceConnectBase.closeConnection();

                if (serviceGeneral.delete("usabonnementnvdevis", "numAbonnementNVDevis", numAbonnementNVDevis) == 1)
                {
                    isUpdate = true;
                }
            }
            #endregion

            return isUpdate;
        }

        bool IntfDalUSAbonnementNVDevis.deleteUSAbonnementNVDevisProforma(string numProforma)
        {
            #region declaration
            bool isUpdate = true;
            IntfDalUSAbonnementNVDevis serviceUSAbonnementNVDevis = new ImplDalUSAbonnementNVDevis();
            #endregion

            #region implementation
            if (numProforma != "")
            {
                this.strCommande = "SELECT usabonnementnvdevis.numAbonnementNVDevis";
                this.strCommande += " FROM usabonnementnvdevis";
                this.strCommande += " WHERE usabonnementnvdevis.numProforma = '" + numProforma + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while(this.reader.Read())
                        {
                            serviceUSAbonnementNVDevis.deleteUSAbonnementNVDevis(this.reader["numAbonnementNVDevis"].ToString());
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isUpdate;
        }
        #endregion

        #region insert to grid


        void IntfDalUSAbonnementNVDevis.insertToGridAbonnementNVDevisProforma(GridView gridView, string param, string paramLike, string valueLike, string numProforma)
        {
            #region declaration
            IntfDalUSAbonnementNVDevis serviceUSAbonnementNVDevis = new ImplDalUSAbonnementNVDevis();
            #endregion

            #region implementation

            this.strCommande = "SELECT usabonnementnvdevis.numAbonnementNVDevis, usabonnementnvdevis.numAbonnement,";
            this.strCommande += " usabonnementnvdevis.prixUnitaireNV, usabonnementnvdevis.montantNV,";
            this.strCommande += " usabonnementnvdevis.numZoneD, usabonnementnvdevis.numZoneF,";
            this.strCommande += " usabonnementnvdevis.numReductionBillet, usabonnementnvdevis.numCategorieBillet,";
            this.strCommande += " usabonnementnvdevis.numProforma, usabonnementnvdevis.numInfoPasse,";
            this.strCommande += " usabonnementnvdevis.numCarte, usabonnementnvdevis.montantCarte,";
            this.strCommande += " usinfopasse.nombrePasse, usinfopasse.niveau, usinfopasse.numInfoPasse,";
            this.strCommande += " usinfopasse.numReductionBillet, usabonnementnvdevis.dateAbonnementNVDevis,";
            this.strCommande += " usabonnementnvdevis.numAbonnementNV, usabonnementnvdevis.nombreVoyage";
            this.strCommande += " FROM usabonnementnvdevis";
            this.strCommande += " Inner Join usinfopasse ON usinfopasse.numInfoPasse = usabonnementnvdevis.numInfoPasse";
            this.strCommande += " WHERE usabonnementnvdevis.numProforma = '" + numProforma + "' AND";
            this.strCommande += " " + paramLike + " LIKE  '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;


            gridView.DataSource = serviceUSAbonnementNVDevis.getDataTableAbonnementNVDevisProforma(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalUSAbonnementNVDevis.getDataTableAbonnementNVDevisProforma(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalUSZone serviceUSZone = new ImplDalUSZone();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            crlUSZone zoneD = null;
            crlUSZone zoneF = null;
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numAbonnementNVDevis", typeof(string));
            dataTable.Columns.Add("Zone", typeof(string));
            dataTable.Columns.Add("prixUnitaireNV", typeof(string));
            dataTable.Columns.Add("nombreVoyage", typeof(string));
            dataTable.Columns.Add("montantNV", typeof(string));
            dataTable.Columns.Add("montantCarte", typeof(string));
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

                        dr["numAbonnementNVDevis"] = reader["numAbonnementNVDevis"].ToString();

                        zoneD = serviceUSZone.selectUSZone(this.reader["numZoneD"].ToString());
                        zoneF = serviceUSZone.selectUSZone(this.reader["numZoneF"].ToString());

                        if (zoneD != null && zoneF != null)
                        {
                            dr["Zone"] = zoneD.NomZone + "-" + zoneF.NomZone;
                        }
                        else 
                        {
                            dr["Zone"] = this.reader["numZoneD"].ToString() + "-" + this.reader["numZoneF"].ToString();
                        }

                        dr["prixUnitaireNV"] = serviceGeneral.separateurDesMilles(reader["prixUnitaireNV"].ToString()) + " Ar";
                        dr["nombreVoyage"] = reader["nombrePasse"].ToString();
                        dr["montantNV"] = serviceGeneral.separateurDesMilles(reader["montantNV"].ToString()) + " Ar";
                        dr["montantCarte"] = serviceGeneral.separateurDesMilles(reader["montantCarte"].ToString()) + " Ar";

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
