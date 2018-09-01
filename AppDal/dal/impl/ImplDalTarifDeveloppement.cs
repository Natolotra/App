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
    /// Description résumée de ImplDalTarifDeveloppement
    /// </summary>
    public class ImplDalTarifDeveloppement : IntfDalTarifDeveloppement
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalTarifDeveloppement()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalTarifDeveloppement(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region methode
        crlTarifDeveloppement IntfDalTarifDeveloppement.selectTarifDeveloppement(string numTarifDeveloppement)
        {
            #region declaration
            crlTarifDeveloppement TarifDeveloppement = null;
            #endregion

            #region implementation
            if (numTarifDeveloppement != "")
            {
                this.strCommande = "SELECT * FROM `tarifdeveloppement` WHERE (`numTarifDeveloppement`='" + numTarifDeveloppement + "')";
            }
            else
            {
                this.strCommande = "SELECT * FROM `tarifdeveloppement` ORDER BY tarifdeveloppement.numTarifDeveloppement DESC";
            }

            this.serviceConnectBase.openConnection();
            this.reader = this.serviceConnectBase.select(this.strCommande); 
            if(this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    if (this.reader.Read())
                    {
                        TarifDeveloppement = new crlTarifDeveloppement();
                        TarifDeveloppement.NumTarifDeveloppement = this.reader["numTarifDeveloppement"].ToString();
                        TarifDeveloppement.Zone = this.reader["zone"].ToString();
                        TarifDeveloppement.MontantTarifDeveloppement = double.Parse(this.reader["montantTarifDeveloppement"].ToString());
                        TarifDeveloppement.CommentaireTarifDeveloppement = this.reader["commentaireTarifDeveloppement"].ToString();
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            #endregion

            return TarifDeveloppement;
        }

        crlTarifDeveloppement IntfDalTarifDeveloppement.selectTarifDeveloppementZone(string zone)
        {
            #region declaration
            crlTarifDeveloppement TarifDeveloppement = null;
            #endregion

            #region implementation
            if (zone != "")
            {
                this.strCommande = "SELECT * FROM `tarifdeveloppement` WHERE (`zone`='" + zone + "') ORDER BY tarifdeveloppement.numTarifDeveloppement DESC";
            }
            else
            {
                this.strCommande = "SELECT * FROM `tarifdeveloppement` ORDER BY tarifdeveloppement.numTarifDeveloppement DESC";
            }

            this.serviceConnectBase.openConnection();
            this.reader = this.serviceConnectBase.select(this.strCommande);
            if (this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    if (this.reader.Read())
                    {
                        TarifDeveloppement = new crlTarifDeveloppement();
                        TarifDeveloppement.NumTarifDeveloppement = this.reader["numTarifDeveloppement"].ToString();
                        TarifDeveloppement.Zone = this.reader["zone"].ToString();
                        TarifDeveloppement.MontantTarifDeveloppement = double.Parse(this.reader["montantTarifDeveloppement"].ToString());
                        TarifDeveloppement.CommentaireTarifDeveloppement = this.reader["commentaireTarifDeveloppement"].ToString();
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            #endregion

            return TarifDeveloppement;
        }

        string IntfDalTarifDeveloppement.insertTarifDeveloppement(crlTarifDeveloppement tarifDeveloppement, string sigleAgence)
        {
            #region declaration
            IntfDalTarifDeveloppement serviceTarifDeveloppement = new ImplDalTarifDeveloppement();
            int nombreInsertion = 0;
            string numTarifDeveloppement = "";
            #endregion

            #region implementation
            if (tarifDeveloppement != null && sigleAgence != "")
            {
                tarifDeveloppement.NumTarifDeveloppement = serviceTarifDeveloppement.getNumTarifDeveloppement(sigleAgence);

                this.strCommande = "INSERT INTO `tarifdeveloppement` (`numTarifDeveloppement`,`zone`,";
                this.strCommande += "`montantTarifDeveloppement`,`commentaireTarifDeveloppement`)";
                this.strCommande += " VALUES('" + tarifDeveloppement.NumTarifDeveloppement + "',";
                this.strCommande += "'" + tarifDeveloppement.Zone + "','" + tarifDeveloppement.MontantTarifDeveloppement.ToString("0") + "',";
                this.strCommande += "'" + tarifDeveloppement.CommentaireTarifDeveloppement + "')";

                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                {
                    numTarifDeveloppement = tarifDeveloppement.NumTarifDeveloppement;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numTarifDeveloppement;
        }

        bool IntfDalTarifDeveloppement.updateTarifDeveloppement(crlTarifDeveloppement tarifDeveloppement)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            #endregion

            #region implementation
            if (tarifDeveloppement != null)
            {
                if (tarifDeveloppement.NumTarifDeveloppement != "")
                {
                    this.strCommande = "UPDATE `tarifdeveloppement` SET `zone`='" + tarifDeveloppement.Zone + "',";
                    this.strCommande += " `montantTarifDeveloppement`='" + tarifDeveloppement.MontantTarifDeveloppement.ToString("0") + "',";
                    this.strCommande += " `commentaireTarifDeveloppement`='" + tarifDeveloppement.CommentaireTarifDeveloppement + "'";
                    this.strCommande += " WHERE (`numTarifDeveloppement`='" + tarifDeveloppement.NumTarifDeveloppement + "')";

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

        string IntfDalTarifDeveloppement.isTarifDeveloppement(crlTarifDeveloppement tarifDeveloppement)
        {
            #region declaration
            string isTarifDeveloppement = "";
            #endregion

            #region implementation
            if (tarifDeveloppement != null)
            {
                this.strCommande = "SELECT tarifdeveloppement.numTarifDeveloppement FROM tarifdeveloppement WHERE";
                this.strCommande += " tarifdeveloppement.zone = '" + tarifDeveloppement.Zone + "' AND";
                this.strCommande += " numTarifDeveloppement <> '" + tarifDeveloppement.NumTarifDeveloppement + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            isTarifDeveloppement = this.reader["numTarifDeveloppement"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }

            #endregion

            return isTarifDeveloppement;

        }

        string IntfDalTarifDeveloppement.getNumTarifDeveloppement(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numTarifDeveloppement = "00001";
            string[] tempNumTarifDeveloppement = null;
            string strDate = "TD" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT tarifdeveloppement.numTarifDeveloppement AS maxNum FROM tarifdeveloppement";
            this.strCommande += " WHERE tarifdeveloppement.numTarifDeveloppement LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumTarifDeveloppement = reader["maxNum"].ToString().ToString().Split('/');
                        numTarifDeveloppement = tempNumTarifDeveloppement[tempNumTarifDeveloppement.Length - 1];
                    }
                    numTemp = double.Parse(numTarifDeveloppement) + 1;
                    if (numTemp < 10)
                        numTarifDeveloppement = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numTarifDeveloppement = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numTarifDeveloppement = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numTarifDeveloppement = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numTarifDeveloppement = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numTarifDeveloppement = strDate + "/" + sigleAgence + "/" + numTarifDeveloppement;
            #endregion

            return numTarifDeveloppement;
        }
        #endregion

        #region insert to grid
        void IntfDalTarifDeveloppement.insertToGridTarifDeveloppement(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalTarifDeveloppement serviceTarifDeveloppement = new ImplDalTarifDeveloppement();
            #endregion

            #region implementation
            this.strCommande = "SELECT tarifdeveloppement.numTarifDeveloppement, tarifdeveloppement.zone, tarifdeveloppement.montantTarifDeveloppement,";
            this.strCommande += " tarifdeveloppement.commentaireTarifDeveloppement FROM tarifdeveloppement";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param + " ASC";

            gridView.DataSource = serviceTarifDeveloppement.getDataTableTarifDeveloppement(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalTarifDeveloppement.getDataTableTarifDeveloppement(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();

            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            #endregion

            #region initialise table
            dataTable.Columns.Add("numTarifDeveloppement", typeof(string));
            dataTable.Columns.Add("zone", typeof(string));
            dataTable.Columns.Add("montantTarifDeveloppement", typeof(string));
            dataTable.Columns.Add("commentaireTarifDeveloppement", typeof(string));

            DataRow dr = null;
            #endregion

            #region implementation
            this.serviceConnectBase.openConnection();
            this.reader = this.serviceConnectBase.select(strRqst);
            if (this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    while (this.reader.Read())
                    {
                        dr = dataTable.NewRow();

                        dr["numTarifDeveloppement"] = this.reader["numTarifDeveloppement"].ToString();
                        dr["zone"] = this.reader["zone"].ToString();
                        dr["montantTarifDeveloppement"] = serviceGeneral.separateurDesMilles(this.reader["montantTarifDeveloppement"].ToString()) + "Ar";
                        dr["commentaireTarifDeveloppement"] = this.reader["commentaireTarifDeveloppement"].ToString();
                        
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