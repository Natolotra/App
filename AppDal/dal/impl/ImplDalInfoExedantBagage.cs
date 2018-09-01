using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.dal.intf;
using MySql.Data.MySqlClient;
using arch.crl;

namespace arch.dal.impl
{
    /// <summary>
    /// Implementation du service InfoExedantBagage
    /// </summary>
    public class ImplDalInfoExedantBagage : IntfDalInfoExedantBagage
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalInfoExedantBagage()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalInfoExedantBagage(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region IntfDalPassager Members

        string IntfDalInfoExedantBagage.insertInfoExedantBagage(crlInfoExedantBagage InfoExedantBagage, string sigleAgence)
        {
            #region declaration
            IntfDalInfoExedantBagage serviceInfoExedantBagage = new ImplDalInfoExedantBagage();
            int nombreInsertion = 0;
            string numInfoBagage = "";
            #endregion

            #region implementation
            if (InfoExedantBagage  != null)
            {
                InfoExedantBagage.NumInfoBagage = serviceInfoExedantBagage.getNumInfoBagage(sigleAgence);
                this.strCommande = "INSERT INTO `infoexedantbagage` (`numInfoBagage`,`poidAutorise`";
                this.strCommande += ",`prixExedantBagage`) ";
                this.strCommande += " VALUES ('" + InfoExedantBagage.NumInfoBagage + "','" + InfoExedantBagage.PoidAutorise+ "'";
                this.strCommande += ",'" + InfoExedantBagage.PrixExedantBagage + "')";

                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    numInfoBagage = InfoExedantBagage.NumInfoBagage;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numInfoBagage;
        }

        bool IntfDalInfoExedantBagage.deleteInfoExedantBagage(crlInfoExedantBagage InfoExedantBagage)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (InfoExedantBagage != null)
            {
                if (InfoExedantBagage.NumInfoBagage != "")
                {
                    this.strCommande = "DELETE FROM `infoexedantbagage` WHERE (`numInfoBagage` = '" + InfoExedantBagage.NumInfoBagage + "')";
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

        bool IntfDalInfoExedantBagage.deleteInfoExedantBagage(string numInfoBagage)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
           
            if (numInfoBagage != "")
            {
                this.strCommande = "DELETE FROM `infoexedantbagage` WHERE (`numInfoBagage` = '" + numInfoBagage + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete == 1)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }
          
            #endregion

            return isDelete;
        }

        bool IntfDalInfoExedantBagage.updateInfoExedantBagage(crlInfoExedantBagage InfoExedantBagage)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            #endregion

            #region implementation
            if (InfoExedantBagage != null)
            {
                if (InfoExedantBagage.NumInfoBagage != "")
                {
                    this.strCommande = "UPDATE `infoexedantbagage` SET `poidAutorise`='" + InfoExedantBagage.PoidAutorise + "', ";
                    this.strCommande += "`prixExedantBagage`='" + InfoExedantBagage.PrixExedantBagage + "'";
                    this.strCommande += " WHERE (`numInfoBagage`='" + InfoExedantBagage.NumInfoBagage + "')";

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

        string IntfDalInfoExedantBagage.isInfoExedantBagage(crlInfoExedantBagage InfoExedantBagage)
        {
            #region declaration
            string numInfoBagage = "";
            #endregion

            #region implementation
            if (InfoExedantBagage != null)
            {
                this.strCommande = "SELECT * FROM `infoexedantbagage` WHERE (`numInfoBagage` <> '" + InfoExedantBagage.NumInfoBagage + "')";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (InfoExedantBagage.PoidAutorise == double.Parse(reader["poidAutorise"].ToString()) && InfoExedantBagage.PrixExedantBagage.Trim().ToLower().Equals(reader["prixExedantBagage"].ToString().Trim().ToLower()))
                            {
                                numInfoBagage = reader["numInfoBagage"].ToString();
                                    break;
                            }
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numInfoBagage;
        }

        int IntfDalInfoExedantBagage.isInfoExedantBagageInt(crlInfoExedantBagage InfoExedantBagage)
        {
            #region declaration
            int isInfoExedantBagage = 0;
            #endregion

            #region implementation
            if (InfoExedantBagage != null)
            {
                this.strCommande = "SELECT * FROM `infoexedantbagage` WHERE (`numInfoBagage` <> '" + InfoExedantBagage.NumInfoBagage + "')";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (InfoExedantBagage.PoidAutorise == double.Parse(reader["poidAutorise"].ToString()) && InfoExedantBagage.PrixExedantBagage.Trim().ToLower().Equals(reader["prixExedantBagage"].ToString().Trim().ToLower()))
                            {
                                isInfoExedantBagage = 1;
                                break;
                            }
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isInfoExedantBagage;
        }

        crlInfoExedantBagage IntfDalInfoExedantBagage.selectInfoExedantBagage(string numInfoBagage)
        {
            #region declaration
            crlInfoExedantBagage InfoExedantBagage = null;
            #endregion

            #region implementation
            if (numInfoBagage != "")
            {
                this.strCommande = "SELECT * FROM `infoexedantbagage` WHERE (`numInfoBagage` = '" + numInfoBagage + "')";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        InfoExedantBagage = new crlInfoExedantBagage();
                        reader.Read();
                        InfoExedantBagage.NumInfoBagage = reader["numInfoBagage"].ToString();
                        InfoExedantBagage.PoidAutorise = double.Parse(reader["poidAutorise"].ToString());
                        InfoExedantBagage.PrixExedantBagage = reader["prixExedantBagage"].ToString();
                        
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return InfoExedantBagage;
        }

        string IntfDalInfoExedantBagage.getNumInfoBagage(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numInfoBagage = "00001";
            string[] tempNumInfoBagage = null;
            string strDate = "IB" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT infoexedantbagage.numInfoBagage AS maxNum FROM infoexedantbagage";
            this.strCommande += " WHERE infoexedantbagage.numInfoBagage LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumInfoBagage = reader["maxNum"].ToString().ToString().Split('/');
                        numInfoBagage = tempNumInfoBagage[tempNumInfoBagage.Length - 1];
                    }
                    numTemp = double.Parse(numInfoBagage) + 1;
                    if (numTemp < 10)
                        numInfoBagage = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numInfoBagage = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numInfoBagage = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numInfoBagage = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numInfoBagage = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numInfoBagage = strDate + "/" + sigleAgence + "/" + numInfoBagage;
            #endregion

            return numInfoBagage;
        }

        #endregion
    }
}