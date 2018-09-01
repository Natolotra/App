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
    /// Implementation du service suivi commission
    /// </summary>
    public class ImplDalSuiviCommission : IntfDalSuiviCommission
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalSuiviCommission()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalSuiviCommission(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion


        #region methode
        crlSuiviCommission IntfDalSuiviCommission.selectSuiviCommission(string numSuiviCommission)
        {
            #region declaration
            crlSuiviCommission suiviCommission = null;
            #endregion

            #region implementation
            if (numSuiviCommission != "")
            {
                this.strCommande = "SELECT suivicommission.numSuiviCommission, suivicommission.matriculeAgent,";
                this.strCommande += " suivicommission.idCommission, suivicommission.dateHeure,";
                this.strCommande += " suivicommission.commentaire FROM suivicommission";
                this.strCommande += " WHERE suivicommission.numSuiviCommission = '" + numSuiviCommission + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            suiviCommission = new crlSuiviCommission();
                            suiviCommission.Commentaire = this.reader["commentaire"].ToString();
                            try
                            {
                                suiviCommission.DateHeure = Convert.ToDateTime(this.reader["dateHeure"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            suiviCommission.IdCommission = this.reader["idCommission"].ToString();
                            suiviCommission.MatriculeAgent = this.reader["matriculeAgent"].ToString();
                            suiviCommission.NumSuiviCommission = this.reader["numSuiviCommission"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return suiviCommission;
        }

        string IntfDalSuiviCommission.insertSuiviCommission(crlSuiviCommission suiviCommission, string sigleAgence)
        {
            #region declaration
            string numSuiviCommission = "";
            IntfDalSuiviCommission serviceSuiviCommission = new ImplDalSuiviCommission();
            int nbInsert = 0;
            #endregion

            #region Implementation
            if (suiviCommission != null)
            {
                if (sigleAgence != "")
                {
                    suiviCommission.NumSuiviCommission = serviceSuiviCommission.getNumSuiviCommission(sigleAgence);

                    this.strCommande = "INSERT INTO `suivicommission` (`numSuiviCommission`,`matriculeAgent`,";
                    this.strCommande += " `idCommission`,`dateHeure`,`commentaire`) VALUES";
                    this.strCommande += " ('" + suiviCommission.NumSuiviCommission + "',";
                    this.strCommande += " '" + suiviCommission.MatriculeAgent + "','" + suiviCommission.IdCommission + "',";
                    this.strCommande += " '" + suiviCommission.DateHeure.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    this.strCommande += " '" + suiviCommission.Commentaire + "')";

                    this.serviceConnectBase.openConnection();
                    nbInsert = this.serviceConnectBase.requete(this.strCommande);
                    if (nbInsert == 1)
                    {
                        numSuiviCommission = suiviCommission.NumSuiviCommission;
                    }
                    this.serviceConnectBase.closeConnection();
                }
            }
            #endregion

            return numSuiviCommission;
        }

        bool IntfDalSuiviCommission.updateSuiviCommission(crlSuiviCommission suiviCommission)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            #endregion

            #region implementation
            if (suiviCommission != null)
            {
                this.strCommande = "UPDATE `suivicommission` SET `commentaire`='" + suiviCommission.Commentaire + "',";
                this.strCommande += " `dateHeure`='" + suiviCommission.DateHeure.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                this.strCommande += " `IdCommission`='" + suiviCommission.IdCommission + "',";
                this.strCommande += " `matriculeAgent`='" + suiviCommission.MatriculeAgent + "'";
                this.strCommande += " WHERE `numSuiviCommission`='" + suiviCommission.NumSuiviCommission + "'";

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

        string IntfDalSuiviCommission.getNumSuiviCommission(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numSuiviCommission = "00001";
            string[] tempNumSuiviCommission = null;
            string strDate = "SM" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT  suivicommission.numSuiviCommission AS maxNum FROM  suivicommission";
            this.strCommande += " WHERE  suivicommission.numSuiviCommission LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumSuiviCommission = reader["maxNum"].ToString().ToString().Split('/');
                        numSuiviCommission = tempNumSuiviCommission[tempNumSuiviCommission.Length - 1];
                    }
                    numTemp = double.Parse(numSuiviCommission) + 1;
                    if (numTemp < 10)
                        numSuiviCommission = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numSuiviCommission = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numSuiviCommission = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numSuiviCommission = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numSuiviCommission = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numSuiviCommission = strDate + "/" + sigleAgence + "/" + numSuiviCommission;
            #endregion

            return numSuiviCommission;
        }
        #endregion
    }
}