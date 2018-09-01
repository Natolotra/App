using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using arch.dal.intf;
using MySql.Data.MySqlClient;
using arch.crl;

namespace arch.dal.impl
{
    /// <summary>
    /// Summary description for ImplDalRecuDecaisser
    /// </summary>
    public class ImplDalRecuDecaisser : IntfDalRecuDecaisser
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalRecuDecaisser()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalRecuDecaisser(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion



        #region IntfDalRecuDecaisser Members

        crlRecuDecaisser IntfDalRecuDecaisser.selectRecuDecaisser(string numRecuDecaisser)
        {
            #region declaration
            crlRecuDecaisser recuDecaisser = null;
            IntfDalAgent serviceAgent = new ImplDalAgent();
            #endregion

            #region implementation
            if (numRecuDecaisser != "") 
            {
                this.strCommande = "SELECT * FROM `recudecaisser` WHERE `numRecuDecaisser`='" + numRecuDecaisser + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            recuDecaisser = new crlRecuDecaisser();
                            try
                            {
                                recuDecaisser.DateRecuDecaisser = Convert.ToDateTime(this.reader["dateRecuDecaisser"].ToString());
                            }
                            catch (Exception) { }
                            recuDecaisser.LibelleRecuDecaisser = this.reader["libelleRecuDecaisser"].ToString();
                            recuDecaisser.MatriculeAgent = this.reader["matriculeAgent"].ToString();
                            try
                            {
                                recuDecaisser.MotantRecuDecaisser = double.Parse(this.reader["motantRecuDecaisser"].ToString());
                            }
                            catch (Exception) { }
                            recuDecaisser.NumRecuDecaisser = this.reader["numRecuDecaisser"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (recuDecaisser.MatriculeAgent != "")
                {
                    recuDecaisser.agent = serviceAgent.selectAgent(recuDecaisser.MatriculeAgent);
                }
            }
            #endregion

            return recuDecaisser;
        }

        string IntfDalRecuDecaisser.insertRecuDecaisser(crlRecuDecaisser recuDecaisser, string sigleAgence)
        {
            #region declaration
            int nombreInsert = 0;
            string numRecuDecaisser = "";
            IntfDalRecuDecaisser serviceRecuDecaisser = new ImplDalRecuDecaisser();
            #endregion

            #region implementation
            if (recuDecaisser != null) 
            {
                recuDecaisser.NumRecuDecaisser = serviceRecuDecaisser.getNumRecuDecaisser(sigleAgence);

                this.strCommande = "INSERT INTO `recudecaisser` (`numRecuDecaisser`,`matriculeAgent`,`dateRecuDecaisser`,";
                this.strCommande += " `motantRecuDecaisser`,`libelleRecuDecaisser`) VALUES ('" + recuDecaisser.NumRecuDecaisser + "',";
                this.strCommande += " '" + recuDecaisser.MatriculeAgent + "','" + recuDecaisser.DateRecuDecaisser.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                this.strCommande += " '" + recuDecaisser.MotantRecuDecaisser.ToString("0") + "','" + recuDecaisser.LibelleRecuDecaisser + "')";

                this.serviceConnectBase.openConnection();
                nombreInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsert == 1) 
                {
                    numRecuDecaisser = recuDecaisser.NumRecuDecaisser;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numRecuDecaisser;
        }

        bool IntfDalRecuDecaisser.updateRecuDecaisser(crlRecuDecaisser recuDecaisser)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            #endregion

            #region implementation
            if (recuDecaisser != null) 
            {
                this.strCommande = "UPDATE `recudecaisser` SET `matriculeAgent`='" + recuDecaisser.MatriculeAgent + "',";
                this.strCommande += " `dateRecuDecaisser`='" + recuDecaisser.DateRecuDecaisser.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                this.strCommande += " `motantRecuDecaisser`='" + recuDecaisser.MotantRecuDecaisser.ToString("0") + "',";
                this.strCommande += " `libelleRecuDecaisser`='" + recuDecaisser.LibelleRecuDecaisser + "'";
                this.strCommande += " WHERE `numRecuDecaisser`='" + recuDecaisser.NumRecuDecaisser + "'";

                this.serviceConnectBase.openConnection();
                nombreUpdate = this.serviceConnectBase.requete(this.strCommande);
                if (nombreUpdate == 1) 
                {
                    isUpdate = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isUpdate;
        }

        string IntfDalRecuDecaisser.getNumRecuDecaisser(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numRecuEncaisser = "00001";
            string[] tempNumRecuEncaisser = null;
            string strDate = "RD" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT recudecaisser.numRecuDecaisser AS maxNum FROM recudecaisser";
            this.strCommande += " WHERE recudecaisser.numRecuDecaisser LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumRecuEncaisser = reader["maxNum"].ToString().ToString().Split('/');
                        numRecuEncaisser = tempNumRecuEncaisser[tempNumRecuEncaisser.Length - 1];
                    }
                    numTemp = double.Parse(numRecuEncaisser) + 1;
                    if (numTemp < 10)
                        numRecuEncaisser = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numRecuEncaisser = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numRecuEncaisser = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numRecuEncaisser = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numRecuEncaisser = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numRecuEncaisser = strDate + "/" + sigleAgence + "/" + numRecuEncaisser;
            #endregion

            return numRecuEncaisser;
        }

        bool IntfDalRecuDecaisser.insertAssocRecuDecaisserCarte(string numRecuDecaisser, string numCarte)
        {
            #region declaration
            bool isInsert = false;
            int nbInsert = 0;
            #endregion

            #region implementation
            if (numRecuDecaisser != "" && numCarte != "")
            {
                this.strCommande = "INSERT INTO `assocrecudecaisseruscarte` (`numRecuDecaisser`,";
                this.strCommande += " `numCarte`) VALUES ('" + numRecuDecaisser + "',";
                this.strCommande += " '" + numCarte + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    isInsert = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isInsert;
        }

        void IntfDalRecuDecaisser.insertToGridRecuDecaisser(GridView gridView, string param, string paramLike, string valueLike, string numAgence)
        {
            throw new NotImplementedException();
        }

        DataTable IntfDalRecuDecaisser.getDataTableRecuDecaisser(string strRqst)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
