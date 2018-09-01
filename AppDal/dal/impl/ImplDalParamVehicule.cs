using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using arch.dal.intf;
using arch.crl;

namespace arch.dal.impl
{
    /// <summary>
    /// Description résumée de ImplDalParamVehicule
    /// </summary>
    public class ImplDalParamVehicule : IntfDalParamVehicule
    {
        #region declaration
        ImplDalConnectBase serviceConnection = null;
        IntfDalServiceRessource serviceRessource = null;
        MySqlDataReader reader = null;

        string strCommande = "";
        string strConnection = "";
        #endregion

        #region constructeur
        public ImplDalParamVehicule(string strConnection)
        {
            this.strConnection = strConnection;
            serviceConnection = new ImplDalConnectBase(strConnection);
        }
        public ImplDalParamVehicule()
        {
            this.serviceRessource = new ImplDalServiceRessource();
            this.strConnection = this.serviceRessource.getDefaultStrConnection();
            this.serviceConnection = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region methode
        crlParamVehicule IntfDalParamVehicule.selectParamVehicule(string numParamVehicule)
        {
            #region declaration
            crlParamVehicule paramVehicule = null;
            #endregion

            #region implementation
            if (numParamVehicule != "")
            {
                this.strCommande = "SELECT * FROM `paramvehicule` WHERE (`numParamVehicule`='" + numParamVehicule + "')";
                this.serviceConnection.openConnection();
                this.reader = this.serviceConnection.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        paramVehicule = new crlParamVehicule();
                        if (this.reader.Read())
                        {
                            paramVehicule.NumParamVehicule = this.reader["numParamVehicule"].ToString();
                            try
                            {
                                paramVehicule.NbPassagerMin = int.Parse(this.reader["nbPassagerMin"].ToString());
                            }
                            catch (Exception)
                            {
                            }

                            try
                            {
                                paramVehicule.AvanceCarburantMax = double.Parse(this.reader["avanceCarburantMax"].ToString());
                            }
                            catch (Exception)
                            {
                            }

                            try
                            {
                                paramVehicule.AvanceChauffeurMax = double.Parse(this.reader["avanceChauffeurMax"].ToString());
                            }
                            catch (Exception)
                            {
                            }

                            try
                            {
                                paramVehicule.PoidBagageMax = double.Parse(this.reader["poidBagageMax"].ToString());
                            }
                            catch (Exception)
                            {
                            }

                            try
                            {
                                paramVehicule.Fond = double.Parse(this.reader["fond"].ToString());
                            }
                            catch (Exception)
                            {
                            }

                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnection.closeConnection();
            }
            #endregion

            return paramVehicule;
        }

        string IntfDalParamVehicule.insertParamVehicule(crlParamVehicule paramVehicule, string sigleAgence)
        {
            #region declaration
            string numParamVehicule = "";
            IntfDalParamVehicule serviceParamVehicule = new ImplDalParamVehicule();
            int nombreInsert = 0;
            #endregion

            #region implementation
            if (paramVehicule != null)
            {
                

                if (sigleAgence != "")
                {
                    paramVehicule.NumParamVehicule = serviceParamVehicule.getNumParamVehicule(sigleAgence);

                    this.strCommande = "INSERT INTO `paramvehicule` (`numParamVehicule`,`nbPassagerMin`,`avanceCarburantMax`,";
                    this.strCommande += " `avanceChauffeurMax`,`poidBagageMax`,`fond`) VALUES";
                    this.strCommande += " ('" + paramVehicule.NumParamVehicule + "', " + paramVehicule.NbPassagerMin + ",";
                    this.strCommande += " '" + paramVehicule.AvanceCarburantMax + "','" + paramVehicule.AvanceChauffeurMax+ "',";
                    this.strCommande += " '" + paramVehicule.PoidBagageMax + "','" + paramVehicule.Fond + "')";

                    this.serviceConnection.openConnection();
                    nombreInsert = this.serviceConnection.requete(this.strCommande);
                    if (nombreInsert == 1)
                    {
                        numParamVehicule = paramVehicule.NumParamVehicule;
                    }
                    this.serviceConnection.closeConnection();
                }
            }
            #endregion

            return numParamVehicule;
        }

        bool IntfDalParamVehicule.upDateParamVehicule(crlParamVehicule paramVehicule)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            #endregion

            #region implementation
            if (paramVehicule != null)
            {
                this.strCommande = "UPDATE `paramvehicule` SET `nbPassagerMin`='" + paramVehicule.NbPassagerMin + "',";
                this.strCommande += " `avanceCarburantMax`='" + paramVehicule.AvanceCarburantMax + "', `avanceChauffeurMax`='" + paramVehicule.AvanceChauffeurMax + "',";
                this.strCommande += " `poidBagageMax`='" + paramVehicule.PoidBagageMax + "',`fond`='" + paramVehicule.Fond + "'";
                this.strCommande += " WHERE `numParamVehicule`='" + paramVehicule.NumParamVehicule + "'";

                this.serviceConnection.openConnection();
                nombreUpdate = this.serviceConnection.requete(this.strCommande);
                if (nombreUpdate == 1)
                {
                    isUpdate = true;
                }
                this.serviceConnection.closeConnection();
            }
            #endregion

            return isUpdate;
        }

        string IntfDalParamVehicule.getNumParamVehicule(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numParamVehicule = "00001";
            string[] tempNumParamVehicule = null;
            string strDate = "PM" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT paramvehicule.numParamVehicule AS maxNum FROM paramvehicule";
            this.strCommande += " WHERE paramvehicule.numParamVehicule LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnection.openConnection();
            reader = this.serviceConnection.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumParamVehicule = reader["maxNum"].ToString().ToString().Split('/');
                        numParamVehicule = tempNumParamVehicule[tempNumParamVehicule.Length - 1];
                    }
                    numTemp = double.Parse(numParamVehicule) + 1;
                    if (numTemp < 10)
                        numParamVehicule = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numParamVehicule = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numParamVehicule = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numParamVehicule = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numParamVehicule = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnection.closeConnection();
            numParamVehicule = strDate + "/" + sigleAgence + "/" + numParamVehicule;
            #endregion

            return numParamVehicule;
        }
        #endregion

    }
}