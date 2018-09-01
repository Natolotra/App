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
    /// Description résumée de ImplDalRecuFac
    /// </summary>
    public class ImplDalRecuFac : IntfDalRecuFac
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalRecuFac(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalRecuFac() 
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion


        #region methode
        string IntfDalRecuFac.insertRecuFac(crlRecuFac recuFac)
        {
            #region declaration
            string numRecuFac = "";
           
            #endregion

            #region implementation
            if (numRecuFac != null)
            {
                this.strCommande = "INSERT INTO `recufac` (`numRecuFac`,`matriculeAgent`,`libele`,`montant`,";
                this.strCommande += " `dateRecu`,`numFacture`) VALUES ( '" + recuFac.NumRecuFac + "',";
                this.strCommande += " '" + recuFac.MatriculeAgent + "', '" + recuFac.Libele + "',";
                this.strCommande += " '" + recuFac.Montant + "', '" + recuFac.Date.ToString("yyyy-MM-dd") + "',";
                this.strCommande += " '" + recuFac.NumFacture + "')";

                this.serviceConnectBase.openConnection();
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numRecuFac;
        }
        #endregion
    }
}