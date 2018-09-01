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
    /// Description résumée de ImplDalTarifBaseBillet
    /// </summary>
    public class ImplDalTarifBaseBillet : IntfDalTarifBaseBillet
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalTarifBaseBillet()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalTarifBaseBillet(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region methode
        crlTarifBaseBillet IntfDalTarifBaseBillet.selectTarifBaseBillet(string numTarifBaseBillet)
        {
            #region decalaration
            crlTarifBaseBillet tarifBaseBillet = null;
            #endregion

            #region implementation
            if (numTarifBaseBillet != "")
            {
                this.strCommande = "SELECT * FROM `tarifbasebillet` WHERE (`numTarifBaseBillet`='" + numTarifBaseBillet + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            tarifBaseBillet = new crlTarifBaseBillet();
                            tarifBaseBillet.NumTarifBaseBillet = this.reader["numTarifBaseBillet"].ToString();
                            try
                            {
                                tarifBaseBillet.MontantTarifBaseBillet = double.Parse(this.reader["montantTarifBaseBillet"].ToString());
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

            return tarifBaseBillet;
        }

        string IntfDalTarifBaseBillet.insertTarifBaseBillet(crlTarifBaseBillet tarifBaseBillet, string sigleAgence)
        {
            #region declaration
            IntfDalTarifBaseBillet serviceTarifBaseBillet = new ImplDalTarifBaseBillet();
            int nombreInsertion = 0;
            string numTarifBaseBillet = "";
            #endregion

            #region implementation
            if (tarifBaseBillet != null)
            {
                tarifBaseBillet.NumTarifBaseBillet = serviceTarifBaseBillet.getNumerosTarifBaseBillet(sigleAgence);

                this.strCommande = "INSERT INTO `tarifbasebillet` (`numTarifBaseBillet`,`montantTarifBaseBillet`)";
                this.strCommande += " VALUES ('" + tarifBaseBillet.NumTarifBaseBillet + "','" + tarifBaseBillet.MontantTarifBaseBillet + "')";

                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    numTarifBaseBillet = tarifBaseBillet.NumTarifBaseBillet;
                this.serviceConnectBase.closeConnection();

            }
            #endregion

            return numTarifBaseBillet;
        }

        bool IntfDalTarifBaseBillet.updateTarifBaseBillet(crlTarifBaseBillet tarifBaseBillet)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            #endregion

            #region implementation
            if (tarifBaseBillet != null)
            {
                this.strCommande = "UPDATE `tarifbasebillet` SET `montantTarifBaseBillet`='" + tarifBaseBillet.MontantTarifBaseBillet.ToString("0") + "'";
                this.strCommande += " WHERE (`numTarifBaseBillet`='" + tarifBaseBillet.NumTarifBaseBillet +"')";

                this.serviceConnectBase.openConnection();
                nbUpdate = this.serviceConnectBase.requete(this.strCommande);
                if (nbUpdate == 1)
                    isUpdate = true;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isUpdate;
        }

        string IntfDalTarifBaseBillet.getNumerosTarifBaseBillet(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numTarifBaseBillet = "00001";
            string[] tempNumTarifBaseBillet = null;
            string strDate = "TB" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT tarifbasebillet.numTarifBaseBillet AS maxNum FROM tarifbasebillet";
            this.strCommande += " WHERE tarifbasebillet.numTarifBaseBillet LIKE '%" + sigleAgence + "%'";
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