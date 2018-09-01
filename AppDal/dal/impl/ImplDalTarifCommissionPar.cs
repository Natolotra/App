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
    /// Description résumée de ImplDalTarifCommissionPar
    /// </summary>
    public class ImplDalTarifCommissionPar : IntfDalTarifCommissionPar
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalTarifCommissionPar()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalTarifCommissionPar(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion


        #region methode
        crlTarifCommissionPar IntfDalTarifCommissionPar.selectTarifCommissionPar(string numTarifCommissionPar)
        {
            #region decalaration
            crlTarifCommissionPar tarifCommissionPar = null;
            #endregion

            #region implementation
            if (numTarifCommissionPar != "")
            {
                this.strCommande = "SELECT * FROM `tarifcommissionpar` WHERE (`numTarifCommissionPar`='" + numTarifCommissionPar + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            tarifCommissionPar = new crlTarifCommissionPar();
                            tarifCommissionPar.NumTarifCommissionPar = this.reader["numTarifCommissionPar"].ToString();
                            tarifCommissionPar.CommentaireTarifCommissionPar = this.reader["commentaireTarifCommissionPar"].ToString();
                            try
                            {
                                tarifCommissionPar.TypeCalcule = int.Parse(this.reader["typeCalcule"].ToString());
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

            return tarifCommissionPar;
        }
        crlTarifCommissionPar IntfDalTarifCommissionPar.selectTarifCommissionPar(int typeCalcule)
        {
            #region decalaration
            crlTarifCommissionPar tarifCommissionPar = null;
            #endregion

            #region implementation
            if (typeCalcule >= 0)
            {
                this.strCommande = "SELECT * FROM `tarifcommissionpar` WHERE (`typeCalcule`='" + typeCalcule + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            tarifCommissionPar = new crlTarifCommissionPar();
                            tarifCommissionPar.NumTarifCommissionPar = this.reader["numTarifCommissionPar"].ToString();
                            tarifCommissionPar.CommentaireTarifCommissionPar = this.reader["commentaireTarifCommissionPar"].ToString();
                            try
                            {
                                tarifCommissionPar.TypeCalcule = int.Parse(this.reader["typeCalcule"].ToString());
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

            return tarifCommissionPar;
        }
        #endregion


        
    }
}