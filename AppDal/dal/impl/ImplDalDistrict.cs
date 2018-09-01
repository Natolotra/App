using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.dal.intf;
using arch.crl;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;

namespace arch.dal.impl
{
    /// <summary>
    /// Description résumée de ImplDalDistrict
    /// </summary>
    public class ImplDalDistrict : IntfDalDistrict
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalDistrict()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalDistrict(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion


        #region methode
        string IntfDalDistrict.insertDistrict(crlDistrict district, string sigleAgence)
        {
            #region declaration
            IntfDalDistrict serviceDistrict = new ImplDalDistrict();
            string numDistrict = "";
            int nbInsert = 0;
            #endregion

            #region implementation
            if (district != null) 
            {
                district.NumDistrict = serviceDistrict.getNumDistrict(sigleAgence);
                this.strCommande = "INSERT INTO `district` (`numDistrict`,`district`,`nomRegion`)";
                this.strCommande += " VALUES ('" + district.NumDistrict + "','" + district.District + "',";
                this.strCommande += " '" + district.NomRegion + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1) 
                {
                    numDistrict = district.NumDistrict;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numDistrict;
        }

        bool IntfDalDistrict.updateDistrict(crlDistrict district)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            #endregion

            #region implementation
            if (district != null) 
            {
                this.strCommande = "UPDATE `district` SET `district`='" + district.District + "',";
                this.strCommande += " `nomRegion`='" + district.NomRegion + "' WHERE";
                this.strCommande += " `numDistrict`='" + district.NumDistrict + "'";

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

        string IntfDalDistrict.isDistrict(crlDistrict district)
        {
            #region declaration
            string numDisrict = "";
            #endregion

            #region implementation
            if (district != null) 
            {
                this.strCommande = "SELECT * FROM `district` WHERE";
                this.strCommande += " district.district = '" + district.District + "' AND";
                this.strCommande += " district.nomRegion = '" + district.NomRegion + "' AND";
                this.strCommande += " district.numDistrict <> '" + district.NumDistrict + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            numDisrict = this.reader["numDisrict"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numDisrict;
        }

        crlDistrict IntfDalDistrict.selectDistrict(string numDistrict)
        {
            #region declaration
            crlDistrict district = null;
            #endregion

            #region implementation
            if (numDistrict != "") 
            {
                this.strCommande = "SELECT * FROM `district` WHERE `numDistrict`='" + numDistrict + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            district = new crlDistrict();
                            district.District = this.reader["district"].ToString();
                            district.NomRegion = this.reader["nomRegion"].ToString();
                            district.NumDistrict = this.reader["numDistrict"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return district;
        }

        string IntfDalDistrict.getNumDistrict(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numDistrict = "00001";
            string[] tempNumDistrict = null;
            string strDate = "DI" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT district.numDistrict AS maxNum FROM district";
            this.strCommande += " WHERE district.numDistrict LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumDistrict = reader["maxNum"].ToString().ToString().Split('/');
                        numDistrict = tempNumDistrict[tempNumDistrict.Length - 1];
                    }
                    numTemp = double.Parse(numDistrict) + 1;
                    if (numTemp < 10)
                        numDistrict = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numDistrict = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numDistrict = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numDistrict = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numDistrict = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numDistrict = strDate + "/" + sigleAgence + "/" + numDistrict;
            #endregion

            return numDistrict;
        }
        #endregion

        #region IntfDalDistrict Members


        void IntfDalDistrict.loadDddlDistrict(DropDownList ddl)
        {
            #region implementation
            if (ddl != null)
            {
                ddl.Items.Clear();
                ddl.Items.Add("");
                this.strCommande = "SELECT * FROM `district`";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        while (this.reader.Read()) 
                        {
                            ddl.Items.Add(new ListItem(this.reader["district"].ToString(), this.reader["numDistrict"].ToString()));
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }

        void IntfDalDistrict.loadDddlDistrictRegion(DropDownList ddl, string nomRegion)
        {
            #region implementation
            if (ddl != null)
            {
                ddl.Items.Clear();
                ddl.Items.Add("");
                this.strCommande = "SELECT * FROM `district` WHERE `nomRegion`='" + nomRegion + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            ddl.Items.Add(new ListItem(this.reader["district"].ToString(), this.reader["numDistrict"].ToString()));
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