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
    /// Implementation du service point
    /// </summary>
    public class ImplDalUSPoint : IntfDalUSPoint
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalUSPoint(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalUSPoint()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion

        #region IntfDalUSPoint Members

        string IntfDalUSPoint.insertUSPoint(crlUSPoint point, string sigleAgence)
        {
            #region declaration
            string numPoint = "";
            int nbInsert = 0;
            IntfDalUSPoint serviceUSPoint = new ImplDalUSPoint();
            #endregion

            #region implementation
            if (point != null) 
            {
                point.NumPoint = serviceUSPoint.getNumUSPoint(sigleAgence);

                this.strCommande = "INSERT INTO `uspoint` (`numPoint`,`matriculeAgent`,`dateHeurePoint`,";
                this.strCommande += " `numVoyage`,`commentaire`,`numArret`) VALUES ('" + point.NumPoint + "',";
                this.strCommande += " '" + point.MatriculeAgent + "','" + point.DateHeurePoint.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                this.strCommande += " '" + point.NumVoyage + "','" + point.Commentaire + "','" + point.NumArret + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1) 
                {
                    numPoint = point.NumPoint;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numPoint;
        }

        bool IntfDalUSPoint.updateUSPoint(crlUSPoint point)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            #endregion

            #region implementation
            if (point != null) 
            {
                this.strCommande = "UPDATE `uspoint` SET `matriculeAgent`='" + point.MatriculeAgent + "',";
                this.strCommande += " `dateHeurePoint`='" + point.DateHeurePoint.ToString("yyyy-MM-dd") + "',";
                this.strCommande += " `numVoyage`='" + point.NumVoyage + "',`commentaire`='" + point.Commentaire + "',";
                this.strCommande += " `numArret`='" + point.NumArret + "'";
                this.strCommande += " WHERE `numPoint`='" + point.NumPoint + "'";

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

        crlUSPoint IntfDalUSPoint.selectUSPoint(string numPoint)
        {
            #region declaration
            crlUSPoint point = null;
            #endregion

            #region implementation
            if (numPoint != "") 
            {
                this.strCommande = "SELECT * FROM `uspoint` WHERE `numPoint`='" + numPoint + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            point = new crlUSPoint();
                            point.Commentaire = this.reader["commentaire"].ToString();
                            try
                            {
                                point.DateHeurePoint = Convert.ToDateTime(this.reader["dateHeurePoint"].ToString());
                            }
                            catch (Exception) { }
                            point.MatriculeAgent = this.reader["matriculeAgent"].ToString();
                            point.NumPoint = this.reader["numPoint"].ToString();
                            point.NumVoyage = this.reader["numVoyage"].ToString();
                            point.NumArret = this.reader["numArret"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return point;
        }

        string IntfDalUSPoint.getNumUSPoint(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numPoint = "000001";
            string[] tempNumPoint = null;
            string strDate = sigleAgence + DateTime.Now.ToString("yyMMdd");
            #endregion

            #region implementation
            this.strCommande = "SELECT uspoint.numPoint AS maxNum FROM uspoint";
            this.strCommande += " WHERE uspoint.numPoint LIKE '%" + strDate + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumPoint = reader["maxNum"].ToString().ToString().Split('/');
                        numPoint = tempNumPoint[tempNumPoint.Length - 1];
                    }
                    numTemp = double.Parse(numPoint) + 1;

                    numPoint = numTemp.ToString("000000");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numPoint = strDate + "/" + numPoint;
            #endregion

            return numPoint;
        }

        #endregion

        #region insert to grid
        void IntfDalUSPoint.insertToGridPointVoyage(GridView gridView, string param, string paramLike, string valueLike, string numVoyage)
        {
            #region declaration
            IntfDalUSPoint serviceUSPoint = new ImplDalUSPoint();
            #endregion

            #region implementation

            this.strCommande = "SELECT uspoint.numPoint, uspoint.matriculeAgent, uspoint.dateHeurePoint,";
            this.strCommande += " uspoint.numVoyage, uspoint.commentaire, uspoint.numArret,";
            this.strCommande += " usarret.nomArret, uslieu.nomLieu FROM uspoint";
            this.strCommande += " Inner Join usarret ON usarret.numArret = uspoint.numArret";
            this.strCommande += " Inner Join uslieu ON uslieu.numLieu = usarret.numLieu";
            this.strCommande += " WHERE uspoint.numVoyage = '" + numVoyage + "' AND";
            this.strCommande += " " + paramLike + " LIKE  '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;


            gridView.DataSource = serviceUSPoint.getDataTablePointVoyage(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalUSPoint.getDataTablePointVoyage(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numPoint", typeof(string));
            dataTable.Columns.Add("dateHeurePoint", typeof(DateTime));
            dataTable.Columns.Add("lieu", typeof(string));
            dataTable.Columns.Add("commentaire", typeof(string));

            DataRow dr;
            #endregion

            this.serviceConnectBase.openConnection();
            this.reader = this.serviceConnectBase.select(strRqst);
            if (this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    while (this.reader.Read())
                    {
                        dr = dataTable.NewRow();

                        dr["numPoint"] = reader["numPoint"].ToString();
                        try
                        {
                            dr["dateHeurePoint"] = Convert.ToDateTime(reader["dateHeurePoint"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        dr["lieu"] = reader["nomLieu"].ToString() + " " + reader["nomArret"].ToString();
                        dr["commentaire"] = reader["commentaire"].ToString();

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }
        #endregion
    }
}
