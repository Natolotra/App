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
    /// Implementation du service DesignationCommission
    /// </summary>
    public class ImplDalDesignationCommission : IntfDalDesignationCommission
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalDesignationCommission(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalDesignationCommission() 
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion

        #region IntfDalFacture Members

        string IntfDalDesignationCommission.insertDesignationCommission(crlDesignationCommission DesignationCommission, string sigleAgence)
        {
            #region declaration
            IntfDalDesignationCommission serviceDesignationCommission = new ImplDalDesignationCommission();
            int nombreInsertion = 0;
            string numRecu = "";
            #endregion

            #region implementation
            if (DesignationCommission != null)
            {
                DesignationCommission.NumDesignation = serviceDesignationCommission.getNumDesignation(sigleAgence);
                this.strCommande = "INSERT INTO `designationcommission` (`numDesignation`,`designation`,`typeCommission`,`paiement`)";
                this.strCommande += " VALUES ('" + DesignationCommission.NumDesignation + "', '" + DesignationCommission.Designation + "', ";
                this.strCommande += " '" + DesignationCommission.TypeCommission + "', '" + DesignationCommission.Paiement + "')";

                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    numRecu = DesignationCommission.NumDesignation;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numRecu;
        }

        bool IntfDalDesignationCommission.deleteDesignationCommission(crlDesignationCommission DesignationCommission)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (DesignationCommission != null)
            {
                if (DesignationCommission.NumDesignation != "")
                {
                    this.strCommande = "DELETE FROM `designationcommission` WHERE (`numDesignation` = '" + DesignationCommission.NumDesignation + "')";
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

        bool IntfDalDesignationCommission.deleteDesignationCommission(string numDesignation)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
           
            if (numDesignation != "")
            {
                this.strCommande = "DELETE FROM `designationcommission` WHERE (`numDesignation` = '" + numDesignation + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete == 1)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }
           
            #endregion

            return isDelete;
        }

        bool IntfDalDesignationCommission.updateDesignationCommission(crlDesignationCommission DesignationCommission)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            #endregion

            #region implementation
            if (DesignationCommission != null)
            {
                if (DesignationCommission.NumDesignation != "")
                {
                    this.strCommande = "UPDATE `designationcommission` SET `designation`='" + DesignationCommission.Designation + "', ";
                    this.strCommande += "`typeCommission`='" + DesignationCommission.TypeCommission + "', `paiement`='" + DesignationCommission.Paiement + "'";
                    this.strCommande += "WHERE (`numDesignation`='" + DesignationCommission.NumDesignation + "')";

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

        string IntfDalDesignationCommission.isDesignationCommission(crlDesignationCommission DesignationCommission)
        {
            #region declaration
            string numDesignationCommission = "";
            #endregion

            #region implementation
            if (DesignationCommission != null)
            {
                this.strCommande = "SELECT * FROM `designationcommission`";
                this.strCommande += " WHERE `numDesignation`<>'" + DesignationCommission.NumDesignation + "' AND";
                this.strCommande += " `designation`='" + DesignationCommission.Designation + "' AND";
                this.strCommande += " `paiement`='" + DesignationCommission.Paiement + "' AND";
                this.strCommande += " `typeCommission`='" + DesignationCommission.TypeCommission + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            numDesignationCommission = this.reader["numDesignation"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numDesignationCommission;
        }

        crlDesignationCommission IntfDalDesignationCommission.selectDesignationCommission(string numDesignation)
        {
            #region declaration
            crlDesignationCommission DesignationCommission = null;
            #endregion

            #region implementation
            if (numDesignation != "")
            {
                this.strCommande = "SELECT * FROM `designationcommission` WHERE (`numDesignation`='" + numDesignation + "')";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        DesignationCommission = new crlDesignationCommission();
                        reader.Read();
                        DesignationCommission.NumDesignation = reader["numDesignation"].ToString();
                        DesignationCommission.Designation = reader["designation"].ToString();
                        DesignationCommission.Paiement = int.Parse(reader["paiement"].ToString());
                        DesignationCommission.TypeCommission = reader["typeCommission"].ToString();
                       
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return DesignationCommission;
        }

        List<crlDesignationCommission> IntfDalDesignationCommission.selectDesignationCommissions(string typeCommission)
        {
           #region declaration
            List<crlDesignationCommission> DesignationCommissions = null;
            crlDesignationCommission tempDesignationCommission = null;
            #endregion

            #region implementation
            if (typeCommission != "")
            {
                this.strCommande = "SELECT * FROM `designationcommission` WHERE (`typeCommission`='" + typeCommission + "')";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        DesignationCommissions = new List<crlDesignationCommission>();

                        while (reader.Read())
                        {
                            tempDesignationCommission = new crlDesignationCommission();

                            tempDesignationCommission.NumDesignation = reader["numDesignation"].ToString();
                            tempDesignationCommission.Designation = reader["designation"].ToString();
                            tempDesignationCommission.Paiement = int.Parse(reader["paiement"].ToString());
                            tempDesignationCommission.TypeCommission = reader["typeCommission"].ToString();

                            DesignationCommissions.Add(tempDesignationCommission);
                        }
                       
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return DesignationCommissions;
        }

        string IntfDalDesignationCommission.getNumDesignation(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numDesignation = "00001";
            string[] tempNumDesignation = null;
            string strDate = "DE" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT designationcommission.numDesignation AS maxNum FROM designationcommission";
            this.strCommande += " WHERE designationcommission.numDesignation LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumDesignation = reader["maxNum"].ToString().ToString().Split('/');
                        numDesignation = tempNumDesignation[tempNumDesignation.Length - 1];
                    }
                    numTemp = double.Parse(numDesignation) + 1;
                    if (numTemp < 10)
                        numDesignation = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numDesignation = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numDesignation = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numDesignation = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numDesignation = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numDesignation = strDate + "/" + sigleAgence + "/" + numDesignation;
            #endregion

            return numDesignation;
        }

        void IntfDalDesignationCommission.loadDdlDesignationCommission(DropDownList ddl, string typeCommission)
        {
            if (typeCommission != "" && ddl != null)
            {
                ddl.Items.Clear();

                this.strCommande = "SELECT * FROM designationcommission WHERE (`typeCommission`='" + typeCommission + "')";
                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ddl.Items.Add(new ListItem(reader["designation"].ToString(), reader["numDesignation"].ToString()));
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
        }

        #endregion




        #region insert to grid
        void IntfDalDesignationCommission.insertToGridDesignationCommission(GridView gridView, string param, string paramLike, string valueLike)
        {

            #region declaration
            IntfDalDesignationCommission serviceDesignationCommission = new ImplDalDesignationCommission();
            #endregion

            #region implementation

            this.strCommande = "SELECT designationcommission.numDesignation, designationcommission.designation,";
            this.strCommande += " designationcommission.typeCommission, designationcommission.paiement FROM designationcommission";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceDesignationCommission.getDataTableDesignationCommission(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalDesignationCommission.getDataTableDesignationCommission(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numDesignation", typeof(string));
            dataTable.Columns.Add("designation", typeof(string));
            dataTable.Columns.Add("typeCommission", typeof(string));
            dataTable.Columns.Add("paiement", typeof(string));
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

                        dr["numDesignation"] = reader["numDesignation"].ToString();
                        dr["designation"] = reader["designation"].ToString();
                        dr["typeCommission"] = reader["typeCommission"].ToString();

                        if (reader["paiement"].ToString().Equals("0"))
                        {
                            dr["paiement"] = "Interne";
                        }
                        else if (reader["paiement"].ToString().Equals("1"))
                        {
                            dr["paiement"] = "Par Kg";
                        }
                        else if (reader["paiement"].ToString().Equals("2"))
                        {
                            dr["paiement"] = "Par pièce";
                        }
                        else
                        {
                            dr["paiement"] = "";
                        }

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