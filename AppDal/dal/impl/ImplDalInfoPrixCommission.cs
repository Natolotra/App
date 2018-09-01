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
    /// Implementation du service Commission
    /// </summary>
    public class ImplDalInfoPrixCommission : IntfDalInfoPrixCommission
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalInfoPrixCommission(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalInfoPrixCommission() 
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion

        #region IntfDalFacture Members

        string IntfDalInfoPrixCommission.insertInfoPrixCommission(crlInfoPrixCommission InfoPrixCommission, string sigleAgence)
        {
            #region declaration
            IntfDalInfoPrixCommission serviceInfoPrixCommission = new ImplDalInfoPrixCommission();
            int nombreInsertion = 0;
            string numInfoPrixCommission = "";
            #endregion

            #region implementation
            if (InfoPrixCommission != null)
            {
                InfoPrixCommission.NumInfoPrixCommission = serviceInfoPrixCommission.getNumInfoPrixCommission(sigleAgence);
                this.strCommande = "INSERT INTO `infoprixcommission` (`numInfoPrixCommission`,`prix`,`paiement`)";
                this.strCommande += " VALUES ('" + InfoPrixCommission.NumInfoPrixCommission + "', '" + InfoPrixCommission.Prix + "', ";
                this.strCommande += " '" + InfoPrixCommission.Paiement + "')";

                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    numInfoPrixCommission = InfoPrixCommission.NumInfoPrixCommission;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numInfoPrixCommission;
        }

        bool IntfDalInfoPrixCommission.deleteInfoPrixCommission(crlInfoPrixCommission InfoPrixCommission)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (InfoPrixCommission != null)
            {
                if (InfoPrixCommission.NumInfoPrixCommission != "")
                {
                    this.strCommande = "DELETE FROM `infoprixcommission` WHERE (`numInfoPrixCommission` = '" + InfoPrixCommission.NumInfoPrixCommission + "')";
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

        bool IntfDalInfoPrixCommission.deleteInfoPrixCommission(string numInfoPrixCommission)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
           
            if (numInfoPrixCommission != "")
            {
                this.strCommande = "DELETE FROM `infoprixcommission` WHERE (`numInfoPrixCommission` = '" + numInfoPrixCommission + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete == 1)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }
          
            #endregion

            return isDelete;
        }

        bool IntfDalInfoPrixCommission.updateInfoPrixCommission(crlInfoPrixCommission InfoPrixCommission)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            #endregion

            #region implementation
            if (InfoPrixCommission != null)
            {
                if (InfoPrixCommission.NumInfoPrixCommission != "")
                {
                    this.strCommande = "UPDATE `infoprixcommission` SET `prix`='" + InfoPrixCommission.Prix + "', ";
                    this.strCommande += "`paiement`='" + InfoPrixCommission.Paiement + "' ";
                    this.strCommande += "WHERE (`numInfoPrixCommission`='" + InfoPrixCommission.NumInfoPrixCommission + "')";

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

        int IntfDalInfoPrixCommission.isInfoPrixCommissionInt(crlInfoPrixCommission InfoPrixCommission)
        {
            #region initialisation
            int isInfoPrixCommissionInt = 0;
            #endregion

            #region implementation
            if (InfoPrixCommission != null)
            {
                this.strCommande = "SELECT * FROM `infoprixcommission` WHERE (`numInfoPrixCommission`<>'" + InfoPrixCommission.NumInfoPrixCommission + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            if (InfoPrixCommission.Paiement == int.Parse(reader["paiement"].ToString()) && InfoPrixCommission.Prix.Trim().ToLower().Equals(reader["prix"].ToString().Trim().ToLower()))
                            {
                                isInfoPrixCommissionInt = 1;
                                break;
                            }
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isInfoPrixCommissionInt;
        }

        string IntfDalInfoPrixCommission.isInfoPrixCommission(crlInfoPrixCommission InfoPrixCommission)
        {
            #region initialisation
            string numInfoPrixCommission = "";
            #endregion

            #region implementation
            if (InfoPrixCommission != null)
            {
                this.strCommande = "SELECT * FROM `infoprixcommission` WHERE (`numInfoPrixCommission`<>'" + InfoPrixCommission.NumInfoPrixCommission + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (InfoPrixCommission.Paiement == int.Parse(reader["paiement"].ToString()) && InfoPrixCommission.Prix.Trim().ToLower().Equals(reader["prix"].ToString().Trim().ToLower()))
                            {
                                numInfoPrixCommission = reader["numInfoPrixCommission"].ToString();
                                break;
                            }
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numInfoPrixCommission;
        }

        crlInfoPrixCommission IntfDalInfoPrixCommission.selectInfoPrixCommission(string numInfoPrixCommission)
        {
            #region declaration
            crlInfoPrixCommission InfoPrixCommission = null;
            #endregion

            #region implementation
            if (numInfoPrixCommission != "")
            {
                this.strCommande = "SELECT * FROM `infoprixcommission` WHERE (`numInfoPrixCommission`='" + numInfoPrixCommission + "')";
                
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        InfoPrixCommission = new crlInfoPrixCommission();
                        reader.Read();
                        InfoPrixCommission.NumInfoPrixCommission = reader["numInfoPrixCommission"].ToString();
                        InfoPrixCommission.Paiement = int.Parse(reader["paiement"].ToString());
                        InfoPrixCommission.Prix = reader["prix"].ToString();
                        
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return InfoPrixCommission;
        }

        crlInfoPrixCommission IntfDalInfoPrixCommission.selectInfoPrixCommissionPaiement(string paiement)
        {
            #region declaration
            crlInfoPrixCommission InfoPrixCommission = null;
            #endregion

            #region implementation
            if (paiement != "")
            {
                this.strCommande = "SELECT * FROM `infoprixcommission` WHERE (`paiement`='" + paiement + "')";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        InfoPrixCommission = new crlInfoPrixCommission();
                        reader.Read();
                        InfoPrixCommission.NumInfoPrixCommission = reader["numInfoPrixCommission"].ToString();
                        InfoPrixCommission.Paiement = int.Parse(reader["paiement"].ToString());
                        InfoPrixCommission.Prix = reader["prix"].ToString();

                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return InfoPrixCommission;
        }

        List<crlInfoPrixCommission> IntfDalInfoPrixCommission.selectInfoPrixCommissions(string idItineraire)
        {
            #region declaration
            List<crlInfoPrixCommission> InfoPrixCommissions = null;
            crlInfoPrixCommission tempInfoPrixCommission = null;
            #endregion

            #region implementation
            if (idItineraire != "")
            {
                this.strCommande = "SELECT infoprixcommission.numInfoPrixCommission, infoprixcommission.prix,";
                this.strCommande += " infoprixcommission.paiement FROM infoprixcommission";
                this.strCommande += " Inner Join associationitineraireinfoprixcommission ON associationitineraireinfoprixcommission.numInfoPrixCommission = infoprixcommission.numInfoPrixCommission";
                this.strCommande += " WHERE associationitineraireinfoprixcommission.idItineraire = '" + idItineraire + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        InfoPrixCommissions = new List<crlInfoPrixCommission>();
                        while (reader.Read())
                        {
                            tempInfoPrixCommission = new crlInfoPrixCommission();
                            tempInfoPrixCommission.NumInfoPrixCommission = reader["numInfoPrixCommission"].ToString();
                            tempInfoPrixCommission.Paiement = int.Parse(reader["paiement"].ToString());
                            tempInfoPrixCommission.Prix = reader["prix"].ToString();

                            InfoPrixCommissions.Add(tempInfoPrixCommission);
                        }

                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return InfoPrixCommissions;
        }

        string IntfDalInfoPrixCommission.getNumInfoPrixCommission(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numInfoPrixCommission = "00001";
            string[] tempNumInfoPrixCommission = null;
            string strDate = "IP" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT infoprixcommission.numInfoPrixCommission AS maxNum FROM infoprixcommission";
            this.strCommande += " WHERE infoprixcommission.numInfoPrixCommission LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumInfoPrixCommission = reader["maxNum"].ToString().ToString().Split('/');
                        numInfoPrixCommission = tempNumInfoPrixCommission[tempNumInfoPrixCommission.Length - 1];
                    }
                    numTemp = double.Parse(numInfoPrixCommission) + 1;
                    if (numTemp < 10)
                        numInfoPrixCommission = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numInfoPrixCommission = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numInfoPrixCommission = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numInfoPrixCommission = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numInfoPrixCommission = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numInfoPrixCommission = strDate + "/" + sigleAgence + "/" + numInfoPrixCommission;
            #endregion

            return numInfoPrixCommission;
        }

        #endregion





        
    }
}