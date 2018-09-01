using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.dal.intf;
using MySql.Data.MySqlClient;
using arch.crl;
using System.Web.UI.WebControls;
using System.Data;

namespace arch.dal.impl
{
    /// <summary>
    /// implementation du service prix de base
    /// </summary>
    public class ImplDalUSPrixBase : IntfDalUSPrixBase
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalUSPrixBase(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalUSPrixBase()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion


        string IntfDalUSPrixBase.insertUSPrixBase(crlUSPrixBase prixBase, string sigleAgence)
        {
            #region declaration
            string numPrixBase = "";
            IntfDalUSPrixBase serviceUSPrixBase = new ImplDalUSPrixBase();
            int nbInsert = 0;
            #endregion

            #region implemenation
            if (prixBase != null && sigleAgence != "")
            {
                prixBase.NumPrixBase = serviceUSPrixBase.getNumUSPrixBase(sigleAgence);
                this.strCommande = "INSERT INTO `usprixbase` (`numPrixBase`,`montantPrixBase`,";
                this.strCommande += " `descriptionPrixBase`,`niveauPrixBase`)";
                this.strCommande += " VALUES ('" + prixBase.NumPrixBase + "','" + prixBase.MontantPrixBase.ToString("0") + "',";
                this.strCommande += " '" + prixBase.DescriptionPrixBase + "','" + prixBase.NiveauPrixBase.ToString("0") + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    numPrixBase = prixBase.NumPrixBase;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numPrixBase;
        }

        bool IntfDalUSPrixBase.updateUSPrixBase(crlUSPrixBase prixBase)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            #endregion

            #region implementation
            if (prixBase != null)
            {
                this.strCommande = "UPDATE `usprixbase` SET `montantPrixBase`='" + prixBase.MontantPrixBase.ToString("0") + "',";
                this.strCommande += " `descriptionPrixBase`='" + prixBase.DescriptionPrixBase + "',";
                this.strCommande += " `niveauPrixBase`='" + prixBase.NiveauPrixBase.ToString("0") + "' WHERE";
                this.strCommande += " `numPrixBase`='" + prixBase.NumPrixBase + "'";

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

        string IntfDalUSPrixBase.isUSPrixBase(crlUSPrixBase prixBase)
        {
            #region declaration
            string numPrixBase = "";
            #endregion

            #region implementation
            if (prixBase != null)
            {
                this.strCommande = "SELECT * FROM `usprixbase` WHERE (`numPrixBase`<>'" + prixBase.NumPrixBase + "'";
                this.strCommande += " AND `niveauPrixBase`='" + prixBase.NiveauPrixBase + "')";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            numPrixBase = this.reader["numPrixBase"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numPrixBase;
        }

        crlUSPrixBase IntfDalUSPrixBase.selectUSPrixBase(string numPrixBase)
        {
            #region declaration
            crlUSPrixBase prixBase = null;
            #endregion

            #region implementation
            if (numPrixBase != "")
            {
                this.strCommande = "SELECT * FROM `usprixbase` WHERE (`numPrixBase`='" + numPrixBase + "')";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            prixBase = new crlUSPrixBase();
                            prixBase.NumPrixBase = this.reader["numPrixBase"].ToString();
                            prixBase.DescriptionPrixBase = this.reader["descriptionPrixBase"].ToString();
                            try
                            {
                                prixBase.NiveauPrixBase = int.Parse(this.reader["niveauPrixBase"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                prixBase.MontantPrixBase = double.Parse(this.reader["montantPrixBase"].ToString());
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

            return prixBase;
        }

        crlUSPrixBase IntfDalUSPrixBase.selectUSPrixBase(int niveau)
        {
            #region declaration
            crlUSPrixBase prixBase = null;
            #endregion

            #region implementation
            if (niveau >= 0)
            {
                this.strCommande = "SELECT * FROM `usprixbase` WHERE (`niveauPrixBase`='" + niveau + "')";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            prixBase = new crlUSPrixBase();
                            prixBase.NumPrixBase = this.reader["numPrixBase"].ToString();
                            prixBase.DescriptionPrixBase = this.reader["descriptionPrixBase"].ToString();
                            try
                            {
                                prixBase.NiveauPrixBase = int.Parse(this.reader["niveauPrixBase"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                prixBase.MontantPrixBase = double.Parse(this.reader["montantPrixBase"].ToString());
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

            return prixBase;
        }

        string IntfDalUSPrixBase.getNumUSPrixBase(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numPrixBase = "00001";
            string[] tempNumPrixBase = null;
            string strDate = "PB" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT usprixbase.numPrixBase AS maxNum FROM usprixbase";
            this.strCommande += " WHERE usprixbase.numPrixBase LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumPrixBase = reader["maxNum"].ToString().ToString().Split('/');
                        numPrixBase = tempNumPrixBase[tempNumPrixBase.Length - 1];
                    }
                    numTemp = double.Parse(numPrixBase) + 1;
                    if (numTemp < 10)
                        numPrixBase = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numPrixBase = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numPrixBase = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numPrixBase = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numPrixBase = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numPrixBase = strDate + "/" + sigleAgence + "/" + numPrixBase;
            #endregion

            return numPrixBase;
        }


        int IntfDalUSPrixBase.getNiveauPrixBase(string numLieuD, string numLieuF)
        {
            #region declaration
            int niveau = -1;
            crlUSLieu lieuD = null;
            crlUSLieu lieuF = null;
            crlUSZone zoneD = null;
            crlUSZone zoneF = null;

            IntfDalUSLieu serviceUSLieu = new ImplDalUSLieu();
            IntfDalUSZone serviceUSZone = new ImplDalUSZone(); 
            IntfDalUSPrixBase serviceUSPrixBase = new ImplDalUSPrixBase();
            #endregion

            #region implementation
            if (numLieuD != "" && numLieuF != "")
            {
                lieuD = serviceUSLieu.selectUSLieu(numLieuD);
                lieuF = serviceUSLieu.selectUSLieu(numLieuF);

                if (lieuD != null && lieuF != null)
                {
                    zoneD = serviceUSZone.selectUSZone(lieuD.NumZone);
                    zoneF = serviceUSZone.selectUSZone(lieuF.NumZone);

                    if (zoneD != null && zoneF != null)
                    {
                        if (zoneD.Niveau == 1 && zoneD.Niveau == zoneF.Niveau)
                        {
                            niveau = 1;
                        }
                        else
                        {
                            if (zoneD.Niveau == 1)
                            {
                                niveau = zoneF.Niveau;
                            }
                            else if (zoneF.Niveau == 1)
                            {
                                niveau = zoneD.Niveau;
                            }
                            else
                            {
                                

                                if (serviceUSPrixBase.isMemeAxe(numLieuD, numLieuF))
                                {
                                    niveau = Math.Abs(zoneD.Niveau - zoneF.Niveau) + 1;
                                }
                                else
                                {
                                    niveau = zoneD.Niveau + zoneF.Niveau;
                                }
                            }

                        }
                    }
                }
            }
            #endregion

            return niveau;
        }

        int IntfDalUSPrixBase.getNiveauPrixBase(crlUSZone zoneD, crlUSZone zoneF, bool isMemeAxe)
        {
            #region declaration
            int niveau = -1;
            #endregion

            #region implementation
            if (zoneD != null && zoneF != null)
            {
                if (zoneD.Niveau == 1 && zoneF.Niveau == 1)
                {
                    niveau = 1;
                }
                else
                {
                    if (zoneD.Niveau == 1 || zoneF.Niveau == 1)
                    {
                        if (zoneD.Niveau == 1)
                        {
                            niveau = zoneF.Niveau;
                        }
                        else
                        {
                            niveau = zoneD.Niveau;
                        }
                    }
                    else
                    {
                        if (isMemeAxe)
                        {
                            niveau = Math.Abs(zoneD.Niveau - zoneF.Niveau) + 1;
                        }
                        else
                        {
                            niveau = zoneF.Niveau + zoneD.Niveau;
                        }
                    }
                }
            }
            #endregion

            return niveau;
        }

        int IntfDalUSPrixBase.getNiveauPrixBase(string numZoneD, string numZoneF, bool isMemeAxe)
        {
            #region declaration
            int niveau = -1;
            crlUSZone zoneD = null;
            crlUSZone zoneF = null;
            IntfDalUSZone serviceUSZone = new ImplDalUSZone();
            #endregion

            #region implementation
            zoneD = serviceUSZone.selectUSZone(numZoneD);
            zoneF = serviceUSZone.selectUSZone(numZoneF);

            if (zoneD != null && zoneF != null)
            {
                if (zoneD.Niveau == 1 && zoneF.Niveau == 1)
                {
                    niveau = 1;
                }
                else
                {
                    if (zoneD.Niveau == 1 || zoneF.Niveau == 1)
                    {
                        if (zoneD.Niveau == 1)
                        {
                            niveau = zoneF.Niveau;
                        }
                        else
                        {
                            niveau = zoneD.Niveau;
                        }
                    }
                    else
                    {
                        if (isMemeAxe)
                        {
                            niveau = Math.Abs(zoneD.Niveau - zoneF.Niveau) + 1;
                        }
                        else
                        {
                            niveau = zoneF.Niveau + zoneD.Niveau;
                        }
                    }
                }
            }
            #endregion

            return niveau;
        }
        
        bool IntfDalUSPrixBase.isMemeAxe(string numLieuD, string numLieuF)
        {
            #region declaration
            bool isMemeAxe = false;
            List<string> numAxesLieuD = null;
            List<string> numAxesLieuF = null; 
            #endregion

            #region implementation
            if (numLieuD != "" && numLieuF != "")
            {
                this.strCommande = "SELECT usligne.numAxe FROM uslieu";
                this.strCommande += " Inner Join usarret ON usarret.numLieu = uslieu.numLieu";
                this.strCommande += " Inner Join usassoclignearret ON usassoclignearret.numArret = usarret.numArret";
                this.strCommande += " Inner Join usligne ON usligne.numLigne = usassoclignearret.numLigne";
                this.strCommande += " WHERE uslieu.numLieu = '" + numLieuD + "'";
                this.strCommande += " GROUP BY usligne.numAxe";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        numAxesLieuD = new List<string>();
                        while (this.reader.Read())
                        {
                            numAxesLieuD.Add(this.reader["numAxe"].ToString());
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                this.strCommande = "SELECT usligne.numAxe FROM uslieu";
                this.strCommande += " Inner Join usarret ON usarret.numLieu = uslieu.numLieu";
                this.strCommande += " Inner Join usassoclignearret ON usassoclignearret.numArret = usarret.numArret";
                this.strCommande += " Inner Join usligne ON usligne.numLigne = usassoclignearret.numLigne";
                this.strCommande += " WHERE uslieu.numLieu = '" + numLieuF + "'";
                this.strCommande += " GROUP BY usligne.numAxe";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        numAxesLieuF = new List<string>();
                        while (this.reader.Read())
                        {
                            numAxesLieuF.Add(this.reader["numAxe"].ToString());
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                if (numAxesLieuD != null && numAxesLieuF != null)
                {
                    if (numAxesLieuD.Count > 0 && numAxesLieuF.Count > 0)
                    {
                        for (int i = 0; i < numAxesLieuD.Count; i++)
                        {
                            for (int j = 0; j < numAxesLieuF.Count; j++)
                            {
                                if (numAxesLieuD[i].Equals(numAxesLieuF[j]))
                                {
                                    isMemeAxe = true;
                                    break;
                                }
                            }
                            if (isMemeAxe)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            #endregion

            return isMemeAxe;
        }


        void IntfDalUSPrixBase.insertToGridPrixBase(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalUSPrixBase serviceUSPrixBase = new ImplDalUSPrixBase();
            #endregion

            #region implementation

            this.strCommande = "SELECT usprixbase.numPrixBase, usprixbase.montantPrixBase,";
            this.strCommande += " usprixbase.descriptionPrixBase, usprixbase.niveauPrixBase";
            this.strCommande += " FROM usprixbase";
            this.strCommande += " WHERE " + paramLike + " LIKE  '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;


            gridView.DataSource = serviceUSPrixBase.getDataTablePrixBase(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalUSPrixBase.getDataTablePrixBase(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numPrixBase", typeof(string));
            dataTable.Columns.Add("montantPrixBase", typeof(string));
            dataTable.Columns.Add("descriptionPrixBase", typeof(string));
            dataTable.Columns.Add("niveauPrixBase", typeof(string));

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

                        dr["numPrixBase"] = reader["numPrixBase"].ToString();
                        dr["montantPrixBase"] = serviceGeneral.separateurDesMilles(reader["montantPrixBase"].ToString()) + "Ar";
                        dr["descriptionPrixBase"] = reader["descriptionPrixBase"].ToString();
                        dr["niveauPrixBase"] = reader["niveauPrixBase"].ToString();

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }

        #region IntfDalUSPrixBase Members


        void IntfDalUSPrixBase.loadDDLNiveauPrixBase(DropDownList ddlZone)
        {
            #region declaration
            if (ddlZone != null) 
            {
                ddlZone.Items.Clear();
                ddlZone.Items.Add("");

                this.strCommande = "SELECT * FROM `usprixbase`";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        while (this.reader.Read()) 
                        {
                            ddlZone.Items.Add(this.reader["niveauPrixBase"].ToString());
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }

        #endregion
    }
}