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
    /// Description résumée de ImplDalTarifBaseCommission
    /// </summary>
    public class ImplDalTarifBaseCommission : IntfDalTarifBaseCommission
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalTarifBaseCommission()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalTarifBaseCommission(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region methode
        crlTarifBaseCommission IntfDalTarifBaseCommission.selectTarifBaseCommission(string numTarifBaseCommission)
        {
            #region decalaration
            crlTarifBaseCommission tarifBaseCommission = null;
            IntfDalTarifCommissionPar serviceTarifCommissionPar = new ImplDalTarifCommissionPar();
            #endregion

            #region implementation
            if (numTarifBaseCommission != "")
            {
                this.strCommande = "SELECT * FROM `tarifbasecommission` WHERE (`numTarifBaseCommission`='" + numTarifBaseCommission + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            tarifBaseCommission = new crlTarifBaseCommission();
                            tarifBaseCommission.NumTarifBaseCommission = this.reader["numTarifBaseCommission"].ToString();
                            tarifBaseCommission.NumTarifCommissionPar = this.reader["numTarifCommissionPar"].ToString();
                            try
                            {
                                tarifBaseCommission.MontantTarifBaseCommission = double.Parse(this.reader["montantTarifBaseCommission"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                if (tarifBaseCommission != null)
                {
                    if (tarifBaseCommission.NumTarifCommissionPar != "")
                    {
                        tarifBaseCommission.tarifCommissionPar = serviceTarifCommissionPar.selectTarifCommissionPar(tarifBaseCommission.NumTarifCommissionPar);
                    }
                }
            }
            #endregion

            return tarifBaseCommission;
        }

        string IntfDalTarifBaseCommission.insertTarifBaseCommission(crlTarifBaseCommission TarifBaseCommission, string sigleAgence)
        {
            #region declaration
            IntfDalTarifBaseCommission serviceTarifBaseCommission = new ImplDalTarifBaseCommission();
            int nombreInsertion = 0;
            string numTarifBaseBillet = "";
            #endregion

            #region implementation
            if (TarifBaseCommission != null)
            {
                TarifBaseCommission.NumTarifBaseCommission = serviceTarifBaseCommission.getNumerosTarifBaseCommission(sigleAgence);

                this.strCommande = "INSERT INTO `tarifbasecommission` (`numTarifBaseCommission`,`numTarifCommissionPar`,`montantTarifBaseCommission`)";
                this.strCommande += " VALUES ('" + TarifBaseCommission.NumTarifBaseCommission + "','" + TarifBaseCommission.NumTarifCommissionPar + "','" + TarifBaseCommission.MontantTarifBaseCommission + "')";

                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    numTarifBaseBillet = TarifBaseCommission.NumTarifBaseCommission;
                this.serviceConnectBase.closeConnection();

            }
            #endregion

            return numTarifBaseBillet;
        }

        List<crlTarifBaseCommission> IntfDalTarifBaseCommission.selectTarifBaseCommissions(string numTrajet)
        {
            #region decalaration
            List<crlTarifBaseCommission> tarifBaseCommissions = null;
            crlTarifBaseCommission tempTarifBaseCommission = null;
            IntfDalTarifCommissionPar serviceTarifCommissionPar = new ImplDalTarifCommissionPar();
            #endregion

            #region implementation
            if (numTrajet != "")
            {
                this.strCommande = "SELECT tarifbasecommission.numTarifBaseCommission, tarifbasecommission.numTarifCommissionPar,";
                this.strCommande += " tarifbasecommission.montantTarifBaseCommission FROM tarifbasecommission";
                this.strCommande += " Inner Join associationtrajettarifcommission ON associationtrajettarifcommission.numTarifBaseCommission = tarifbasecommission.numTarifBaseCommission";
                this.strCommande += " WHERE associationtrajettarifcommission.numTrajet = '" + numTrajet + "'";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        tarifBaseCommissions = new List<crlTarifBaseCommission>();
                        while(this.reader.Read())
                        {
                            tempTarifBaseCommission = new crlTarifBaseCommission();
                            tempTarifBaseCommission.NumTarifBaseCommission = this.reader["numTarifBaseCommission"].ToString();
                            tempTarifBaseCommission.NumTarifCommissionPar = this.reader["numTarifCommissionPar"].ToString();
                            try
                            {
                                tempTarifBaseCommission.MontantTarifBaseCommission = double.Parse(this.reader["montantTarifBaseCommission"].ToString());
                            }
                            catch (Exception)
                            {
                            }

                            tarifBaseCommissions.Add(tempTarifBaseCommission);
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                if (tarifBaseCommissions != null)
                {
                    for (int i = 0; i < tarifBaseCommissions.Count; i++)
                    {
                        if (tarifBaseCommissions[i].NumTarifCommissionPar != "")
                        {
                            tarifBaseCommissions[i].tarifCommissionPar = serviceTarifCommissionPar.selectTarifCommissionPar(tarifBaseCommissions[i].NumTarifCommissionPar);
                        }
                    }
                        
                }
            }
            #endregion

            return tarifBaseCommissions;
        }

        bool IntfDalTarifBaseCommission.updateTarifBaseCommission(crlTarifBaseCommission TarifBaseCommission)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            #endregion

            #region implementation
            if (TarifBaseCommission != null)
            {
                this.strCommande = "UPDATE `tarifbasecommission` SET `numTarifCommissionPar`='" + TarifBaseCommission.NumTarifCommissionPar + "',";
                this.strCommande += "`montantTarifBaseCommission`='" + TarifBaseCommission.MontantTarifBaseCommission.ToString("0") + "'";
                this.strCommande += " WHERE `numTarifBaseCommission`='" + TarifBaseCommission.NumTarifBaseCommission + "'";

                this.serviceConnectBase.openConnection();
                nbUpdate = this.serviceConnectBase.requete(this.strCommande);
                if (nbUpdate == 1)
                    isUpdate = true;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isUpdate;
        }

        bool IntfDalTarifBaseCommission.insertAssociationTrajetTarifCommission(string numTrajet, string numTarifBaseCommission)
        {
            #region declaration
            bool isInsert = false;
            int nombreInsertion = 0;
            #endregion

            #region implementation
            if (numTrajet != "" && numTarifBaseCommission != "")
            {
                this.strCommande = "INSERT INTO `associationtrajettarifcommission` (`numTrajet`,`numTarifBaseCommission`)";
                this.strCommande += " VALUES ('" + numTrajet + "','" + numTarifBaseCommission + "')";
                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    isInsert = true;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isInsert;
        }

        string IntfDalTarifBaseCommission.getNumerosTarifBaseCommission(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numTarifBaseBillet = "00001";
            string[] tempNumTarifBaseBillet = null;
            string strDate = "BC" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT tarifbasecommission.numTarifBaseCommission AS maxNum FROM tarifbasecommission";
            this.strCommande += " WHERE tarifbasecommission.numTarifBaseCommission LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumTarifBaseBillet = reader["maxNum"].ToString().ToString().Split('/');
                        numTarifBaseBillet = tempNumTarifBaseBillet[tempNumTarifBaseBillet.Length - 1];
                    }
                    numTemp = double.Parse(numTarifBaseBillet) + 1;
                    if (numTemp < 10)
                        numTarifBaseBillet = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numTarifBaseBillet = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numTarifBaseBillet = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numTarifBaseBillet = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numTarifBaseBillet = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numTarifBaseBillet = strDate + "/" + sigleAgence + "/" + numTarifBaseBillet;
            #endregion

            return numTarifBaseBillet;
        }
        #endregion

    }
}