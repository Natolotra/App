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
    /// Implementation du service ImplDalTypeProprietaire
    /// </summary>
    public class ImplDalTypeProprietaire : IntfDalTypeProprietaire
    {
        #region declaration
        ImplDalConnectBase serviceConnection = null;
        IntfDalServiceRessource serviceRessource = null;
        MySqlDataReader reader = null;

        string strCommande = "";
        string strConnection = "";
        #endregion

        #region constructeur
        public ImplDalTypeProprietaire(string strConnection)
        {
            this.strConnection = strConnection;
            serviceConnection = new ImplDalConnectBase(strConnection);
        }
        public ImplDalTypeProprietaire()
        {
            this.serviceRessource = new ImplDalServiceRessource();
            this.strConnection = this.serviceRessource.getDefaultStrConnection();
            this.serviceConnection = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region methode
        crlTypeProprietaire IntfDalTypeProprietaire.selectTypeProprietaire(string typeProprietaire)
        {
            #region declaration
            crlTypeProprietaire typeProprietaireObj = null;
            #endregion

            #region implementation
            if (typeProprietaire != "")
            {
                this.strCommande = "SELECT * FROM `typeproprietaire` WHERE (`typeProprietaire`='" + typeProprietaire + "')";

                this.serviceConnection.openConnection();
                if (this.serviceConnection.IsConnection)
                {
                    this.reader = this.serviceConnection.select(this.strCommande);
                    if (this.reader != null)
                    {
                        if (this.reader.HasRows)
                        {
                            if (this.reader.Read())
                            {
                                typeProprietaireObj = new crlTypeProprietaire();
                                typeProprietaireObj.TypeProprietairePro = this.reader["typeProprietaire"].ToString();
                            }
                        }
                        this.reader.Dispose();
                    }

                    while (this.serviceConnection.IsConnection)
                        this.serviceConnection.closeConnection();
                }

            }
            #endregion

            return typeProprietaireObj;
        }
        #endregion
    }
}