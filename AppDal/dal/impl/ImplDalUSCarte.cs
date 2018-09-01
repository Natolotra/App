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
    /// Description résumée de ImplDalUSCarte
    /// </summary>
    public class ImplDalUSCarte : IntfDalUSCarte
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalUSCarte()
        {
            this.serviceRessource = new ImplDalServiceRessource();
            this.serviceConnectBase = new ImplDalConnectBase(this.serviceRessource.getDefaultStrConnection());
        }
        public ImplDalUSCarte(string strConnection)
        {
            this.serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion


        #region methode
        string IntfDalUSCarte.insertUSCarte(crlUSCarte carte, string sigleAgence)
        {
            #region declaration
            string numCarte = "";
            IntfDalUSCarte serviceUSCarte = new ImplDalUSCarte();
            string numAbonnement = "NULL";
            string numAbonnementNV = "NULL";
            string numAbonnementNVDevis = "NULL";
            string numUSReductionParticulier = "NULL";
            string numUSValidationReduction = "NULL";
            int nbInsert = 0;
            #endregion

            #region implementation
            if (carte != null && sigleAgence != "")
            {
                if (carte.NumAbonnement != "")
                {
                    numAbonnement = "'" + carte.NumAbonnement + "'";
                }
                if (carte.NumUSReductionParticulier != "")
                {
                    numUSReductionParticulier = "'" + carte.NumUSReductionParticulier + "'";
                }
                if (carte.NumUSValidationReduction != "")
                {
                    numUSValidationReduction = "'" + carte.NumUSValidationReduction + "'";
                }
                if (carte.NumAbonnementNV != "")
                {
                    numAbonnementNV = "'" + carte.NumAbonnementNV + "'";
                }
                if (carte.NumAbonnementNVDevis != "")
                {
                    numAbonnementNVDevis = "'" + carte.NumAbonnementNVDevis + "'";
                }
                carte.NumCarte = serviceUSCarte.getNumUSCarte(sigleAgence);

                this.strCommande = "INSERT INTO `uscarte` (`numCarte`,`prixCarte`,`numUSReductionParticulier`,";
                this.strCommande += " `numAbonnement`,`numAgence`, `numAbonnementNV`, `numAbonnementNVDevis`,";
                this.strCommande += " `numUSValidationReduction`)";
                this.strCommande += " VALUES ('" + carte.NumCarte + "','" + carte.PrixCarte.ToString("0") + "',";
                this.strCommande += " " + numUSReductionParticulier + "," + numAbonnement + ",'" + carte.NumAgence + "',";
                this.strCommande += " " + numAbonnementNV + ", " + numAbonnementNVDevis + "," + numUSValidationReduction + ")";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    numCarte = carte.NumCarte;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numCarte;
        }

        bool IntfDalUSCarte.updateUSCarte(crlUSCarte carte)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            string numAbonnement = "NULL";
            string numAbonnementNV = "NULL";
            string numAbonnementNVDevis = "NULL";
            string numUSReductionParticulier = "NULL";
            string numUSValidationReduction = "NULL";
            #endregion

            #region implementation
            if (carte != null)
            {
                if (carte.NumAbonnement != "")
                {
                    numAbonnement = "'" + carte.NumAbonnement + "'";
                }
                if (carte.NumUSReductionParticulier != "")
                {
                    numUSReductionParticulier = "'" + carte.NumUSReductionParticulier + "'";
                }
                if (carte.NumUSValidationReduction != "")
                {
                    numUSValidationReduction = "'" + carte.NumUSValidationReduction + "'";
                }
                if (carte.NumAbonnementNV != "")
                {
                    numAbonnementNV = "'" + carte.NumAbonnementNV + "'";
                }
                if (carte.NumAbonnementNVDevis != "")
                {
                    numAbonnementNVDevis = "'" + carte.NumAbonnementNVDevis + "'";
                }

                this.strCommande = "UPDATE `uscarte` SET `prixCarte`='" + carte.PrixCarte.ToString("0") + "',";
                this.strCommande += " `numUSReductionParticulier`=" + numUSReductionParticulier + ",";
                this.strCommande += " `numAbonnement`=" + numAbonnement + ",";
                this.strCommande += " `numAgence`='" + carte.NumAgence + "',";
                this.strCommande += " `numAbonnementNV`=" + numAbonnementNV + ",";
                this.strCommande += " `numAbonnementNVDevis`=" + numAbonnementNVDevis + ",";
                this.strCommande += " `numUSValidationReduction`=" + numUSValidationReduction;
                this.strCommande += " WHERE `numCarte`='" + carte.NumCarte + "'";

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

        string IntfDalUSCarte.isUSCarte(crlUSCarte carte)
        {
            #region declaration
            string numCarte = "";
            #endregion

            #region implementation
            #endregion

            return numCarte;
        }

        crlUSCarte IntfDalUSCarte.selectUSCarte(string numCarte)
        {
            #region declaration
            crlUSCarte carte = null;
            IntfDalAbonnement serviceAbonnement = new ImplDalAbonnement();
            IntfDalUSReductionParticulier serviceUSReductionParticulier = new ImplDalUSReductionParticulier();
            #endregion

            #region implementation
            if (numCarte != "")
            {
                this.strCommande = "SELECT * FROM `uscarte` WHERE `numCarte`='" + numCarte + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            carte = new crlUSCarte();
                            carte.NumCarte = this.reader["numCarte"].ToString();
                            carte.NumAbonnement = this.reader["numAbonnement"].ToString();
                            carte.NumUSReductionParticulier = this.reader["numUSReductionParticulier"].ToString();
                            carte.NumUSValidationReduction = this.reader["numUSValidationReduction"].ToString();
                            try
                            {
                                carte.PrixCarte = double.Parse(this.reader["prixCarte"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            carte.NumAgence = this.reader["numAgence"].ToString();
                            carte.NumAbonnementNV = this.reader["numAbonnementNV"].ToString();
                            carte.NumAbonnementNVDevis = this.reader["numAbonnementNVDevis"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                if (carte != null)
                {
                    if (carte.NumAbonnement != "")
                    {
                        carte.abonnement = serviceAbonnement.selectAbonnement(carte.NumAbonnement);
                    }
                    if (carte.NumUSReductionParticulier != "")
                    {
                        carte.reductionParticulier = serviceUSReductionParticulier.selectUSReductionParticulier(carte.NumUSReductionParticulier);
                    }
                }
            }
            #endregion

            return carte;
        }

        string IntfDalUSCarte.getNumUSCarte(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numCarte = "00001";
            string[] tempNumCarte = null;
            string strDate = "CA" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT uscarte.numCarte AS maxNum FROM uscarte";
            this.strCommande += " WHERE uscarte.numCarte LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumCarte = reader["maxNum"].ToString().ToString().Split('/');
                        numCarte = tempNumCarte[tempNumCarte.Length - 1];
                    }
                    numTemp = double.Parse(numCarte) + 1;
                    if (numTemp < 10)
                        numCarte = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numCarte = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numCarte = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numCarte = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numCarte = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numCarte = strDate + "/" + sigleAgence + "/" + numCarte;
            #endregion

            return numCarte;
        }

        bool IntfDalUSCarte.insertAssocAgenceCarte(string numAgence, string numCarte, string matriculeAgent, string numAbonnement, string numUSReductionParticulier, string commentaire)
        {
            #region declaration
            bool isInsert = false;
            int nbInsert = 0;
            string strMatriculeAgent = "NULL";
            string strNumAgence = "NULL";
            string strNumAbonnement = "NULL";
            string strNumUSReductionParticulier = "NULL";
            #endregion

            #region implementation
            if (numAgence != "" && numCarte != "")
            {
                if(matriculeAgent != "")
                {
                    strMatriculeAgent = "'" + matriculeAgent + "'";
                }
                if (numAgence != "")
                {
                    strNumAgence = "'" + numAgence + "'";
                }
                if (numAbonnement != "")
                {
                    strNumAbonnement = "'" + numAbonnement + "'";
                }
                if (numUSReductionParticulier != "")
                {
                    strNumUSReductionParticulier = "'" + numUSReductionParticulier + "'";
                }
                this.strCommande = "INSERT INTO `usassocagencecarte` (`numCarte`,`numAgence`,`matriculeAgent`,";
                this.strCommande += " `date`,`numAbonnement`,`numUSReductionParticulier`,`commentaire`) VALUES";
                this.strCommande += " ('" + numCarte + "'," + strNumAgence + "," + strMatriculeAgent + ",";
                this.strCommande += " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                this.strCommande += " " + strNumAbonnement + ", " + strNumUSReductionParticulier + ",";
                this.strCommande += " '" + commentaire + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    isInsert = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isInsert;
        }

        bool IntfDalUSCarte.deleteAssocAgenceCarte(string numAgence, string numCarte, string dateTimeStr)
        {
            #region declaration
            bool isDelete = false;
            int nbDelete = 0;
            #endregion

            #region implementation
            if (numAgence != "" && numCarte != "" && dateTimeStr != "")
            {
                this.strCommande = "DELETE FROM `usassocagencecarte` WHERE `numCarte`='" + numCarte + "' AND";
                this.strCommande += " `numAgence`='" + numAgence + "' AND `date`='" + dateTimeStr + "'";

                this.serviceConnectBase.openConnection();
                nbDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nbDelete > 0)
                {
                    isDelete = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isDelete;
        }

        void IntfDalUSCarte.loadDdlUSCarteDisponible(DropDownList ddl, string numAgence)
        {
            #region implementation
            if (ddl != null)
            {
                ddl.Items.Clear();
                ddl.Items.Add("");

                this.strCommande = "SELECT uscarte.numCarte FROM uscarte WHERE uscarte.numAgence = '" + numAgence + "' AND";
                this.strCommande += " uscarte.numUSReductionParticulier IS NULL  AND uscarte.numAbonnement IS NULL  AND";
                this.strCommande += " uscarte.numAbonnementNV IS NULL  AND uscarte.numAbonnementNVDevis IS NULL AND";
                this.strCommande += " uscarte.numUSValidationReduction IS NULL";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            ddl.Items.Add(this.reader["numCarte"].ToString());
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }

        void IntfDalUSCarte.loadDdlUSCarteAbonnement(DropDownList ddl, string numAbonnement)
        {
            #region implementation
            if (ddl != null)
            {
                ddl.Items.Clear();
                ddl.Items.Add("");

                this.strCommande = "SELECT uscarte.numCarte FROM uscarte WHERE uscarte.numAbonnement = '" + numAbonnement + "' AND";
                this.strCommande += " uscarte.numAbonnementNVDevis IS NULL ";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            ddl.Items.Add(this.reader["numCarte"].ToString());
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }

        void IntfDalUSCarte.loadDdlUSCarteAbonnementNonBloquer(DropDownList ddl, string numAbonnement)
        {
            #region implementation
            if (ddl != null)
            {
                ddl.Items.Clear();
                ddl.Items.Add("");

                this.strCommande = "SELECT uscarte.numCarte FROM uscarte WHERE uscarte.numAbonnement = '" + numAbonnement + "' AND";
                this.strCommande += " uscarte.numAbonnementNVDevis IS NULL AND uscarte.numAbonnementNV IS NOT NULL";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            ddl.Items.Add(this.reader["numCarte"].ToString());
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }

        void IntfDalUSCarte.loadDdlUSCarteAbonnementBloquer(DropDownList ddl, string numAbonnement)
        {
            #region implementation
            if (ddl != null)
            {
                ddl.Items.Clear();
                ddl.Items.Add("");

                this.strCommande = "SELECT uscarte.numCarte FROM uscarte WHERE uscarte.numAbonnement = '" + numAbonnement + "' AND";
                this.strCommande += " uscarte.numAbonnementNVDevis IS NULL AND uscarte.numAbonnementNV IS NULL";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            ddl.Items.Add(this.reader["numCarte"].ToString());
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }

        void IntfDalUSCarte.loadDdlUSCarteReduction(DropDownList ddl, string numReductionParticulier)
        {
            #region implementation
            if (ddl != null)
            {
                ddl.Items.Clear();
                ddl.Items.Add("");

                this.strCommande = "SELECT uscarte.numCarte FROM uscarte WHERE uscarte.numUSReductionParticulier = '" + numReductionParticulier + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            ddl.Items.Add(this.reader["numCarte"].ToString());
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }

        void IntfDalUSCarte.loadDdlUSCarteReductionBloquer(DropDownList ddl, string numReductionParticulier)
        {
            #region implementation
            if (ddl != null)
            {
                ddl.Items.Clear();
                ddl.Items.Add("");

                this.strCommande = "SELECT uscarte.numCarte FROM uscarte WHERE uscarte.numUSReductionParticulier = '" + numReductionParticulier + "' AND";
                this.strCommande += " uscarte.numUSValidationReduction IS NULL";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            ddl.Items.Add(this.reader["numCarte"].ToString());
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }

        void IntfDalUSCarte.loadDdlUSCarteReductionNonBloquer(DropDownList ddl, string numReductionParticulier)
        {
            #region implementation
            if (ddl != null)
            {
                ddl.Items.Clear();
                ddl.Items.Add("");

                this.strCommande = "SELECT uscarte.numCarte FROM uscarte WHERE uscarte.numUSReductionParticulier = '" + numReductionParticulier + "' AND";
                this.strCommande += " uscarte.numUSValidationReduction IS NOT NULL";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            ddl.Items.Add(this.reader["numCarte"].ToString());
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }
        #endregion


        void IntfDalUSCarte.insertToGridCarte(GridView gridView, string param, string paramLike, string valueLike, string numAgence)
        {
            #region declaration
            IntfDalUSCarte serviceUSCarte = new ImplDalUSCarte();
            #endregion

            #region implementation

            this.strCommande = "SELECT uscarte.numCarte, uscarte.prixCarte, uscarte.numUSReductionParticulier,";
            this.strCommande += " uscarte.numAbonnement, uscarte.numAgence, uscarte.numUSValidationReduction";
            this.strCommande += " FROM uscarte WHERE " + paramLike + " LIKE  '%" + valueLike + "%' AND";
            this.strCommande += " uscarte.numAgence LIKE '%" + numAgence + "%'";
            this.strCommande += " ORDER BY " + param;


            gridView.DataSource = serviceUSCarte.getDataTableCarte(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalUSCarte.getDataTableCarte(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numCarte", typeof(string));
            dataTable.Columns.Add("prixCarte", typeof(string));
            dataTable.Columns.Add("numCarteReduction", typeof(string));
            dataTable.Columns.Add("numAbonnement", typeof(string));

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

                        dr["numCarte"] = reader["numCarte"].ToString();
                        dr["prixCarte"] = serviceGeneral.separateurDesMilles(reader["prixCarte"].ToString()) + " Ar";
                        dr["numCarteReduction"] = reader["numCarteReduction"].ToString();
                        dr["numAbonnement"] = reader["numAbonnement"].ToString();

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