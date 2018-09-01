using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.dal.intf;
using MySql.Data.MySqlClient;
using arch.crl;
using System.Web.UI.WebControls;
using System.Data;

namespace arch.dal.impl
{
    /// <summary>
    /// Description résumée de ImplDalRouteNationale
    /// </summary>
    public class ImplDalRouteNationale : IntfDalRouteNationale
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region construction
        public ImplDalRouteNationale()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalRouteNationale(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region methode
        string IntfDalRouteNationale.insertRouteNationale(crlRouteNationale routeNationale)
        {
            #region declaration
            string strRouteNationale = "";
            int nbInsert = 0;
            #endregion

            #region implementation
            if (routeNationale != null)
            {
                this.strCommande = "INSERT INTO `routenationale` (`routeNationale`,`distanceRN`)";
                this.strCommande += " VALUES ('" + routeNationale.RouteNationale + "','" + routeNationale.DistanceRN + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    strRouteNationale = routeNationale.RouteNationale;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return strRouteNationale;
        }

        bool IntfDalRouteNationale.insertAssocVilleRouteNationale(string numVille, string routeNationale)
        {
            #region declaration
            bool isInsert = false;
            int nbInsert = 0;
            #endregion

            #region Implementation
            if (numVille != "" && routeNationale != "")
            {
                this.strCommande = "INSERT INTO `assovilleroutenationale` (`routeNationale`,`numVille`)";
                this.strCommande += " VALUES ('" + routeNationale + "','" + numVille + "')";
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

        bool IntfDalRouteNationale.deleteAssocVilleRouteNationale(string numVille, string routeNationale)
        {
            #region declaration
            bool isDelete = false;
            int nbDelete = 0;
            #endregion

            #region Implementation
            if (numVille != "" && routeNationale != "")
            {
                this.strCommande = "DELETE FROM `assovilleroutenationale` WHERE";
                this.strCommande += " `routeNationale`='" + routeNationale + "' AND";
                this.strCommande += " `numVille`='" + numVille + "'";

                this.serviceConnectBase.openConnection();
                nbDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nbDelete == 1)
                {
                    isDelete = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isDelete;
        }

        bool IntfDalRouteNationale.updateRouteNationale(crlRouteNationale routeNationale, string strRouteNationale)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            #endregion

            #region implementation
            if (routeNationale != null)
            {
                this.strCommande = "UPDATE `routenationale` SET `routeNationale`='" + routeNationale.RouteNationale + "',";
                this.strCommande += " `distanceRN`='" + routeNationale.DistanceRN.ToString("0") + "'";
                this.strCommande += " WHERE `routeNationale`='" + strRouteNationale + "'";

                this.serviceConnectBase.openConnection();
                nbUpdate = this.serviceConnectBase.requete(this.strCommande);
                if (nbUpdate == 1)
                    isUpdate = true;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isUpdate;
        }

        bool IntfDalRouteNationale.deleteRouteNationale(string routeNationale)
        {
            #region declaration
            bool isDelete = false;
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            #endregion

            #region implementation
            if (routeNationale != null)
            {
                serviceGeneral.delete("assovilleroutenationale", "routeNationale", routeNationale);
                if (serviceGeneral.delete("routenationale", "routeNationale", routeNationale) == 1)
                {
                    isDelete = true;
                }
            }
            #endregion

            return isDelete;
        }

        crlRouteNationale IntfDalRouteNationale.selectRouteNationale(string routeNationale)
        {
            #region declaration
            crlRouteNationale routeNationaleObj = null;
            IntfDalVille serviceVille = new ImplDalVille();
            #endregion

            #region implementation
            if (routeNationale != "")
            {
                this.strCommande = "SELECT * FROM `routenationale` WHERE (`routeNationale`='" + routeNationale + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            routeNationaleObj = new crlRouteNationale();
                            routeNationaleObj.RouteNationale = this.reader["routeNationale"].ToString();
                            try
                            {
                                routeNationaleObj.DistanceRN = double.Parse(this.reader["distanceRN"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (routeNationaleObj != null)
                {
                    routeNationaleObj.villes = serviceVille.selectVillesForRN(routeNationaleObj.RouteNationale);
                }
            }
            #endregion

            return routeNationaleObj;
        }

        List<crlRouteNationale> IntfDalRouteNationale.selectRNForItineraire(string idItineraire)
        {
            #region declaration
            List<crlRouteNationale> routeNationales = null;
            crlRouteNationale tempRouteNationaleObj = null;
            IntfDalVille serviceVille = new ImplDalVille();
            #endregion

            #region implementation
            if (idItineraire != "")
            {
                this.strCommande = "SELECT routenationale.routeNationale, routenationale.distanceRN FROM routenationale";
                this.strCommande += " Inner Join assoitineraireroutenationale ON assoitineraireroutenationale.routeNationale = routenationale.routeNationale";
                this.strCommande += " WHERE assoitineraireroutenationale.idItineraire = '" + idItineraire + "'";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        routeNationales = new List<crlRouteNationale>();

                        while (this.reader.Read())
                        {
                            tempRouteNationaleObj = new crlRouteNationale();

                            tempRouteNationaleObj.RouteNationale = this.reader["routeNationale"].ToString();
                            try
                            {
                                tempRouteNationaleObj.DistanceRN = double.Parse(this.reader["distanceRN"].ToString());
                            }
                            catch (Exception)
                            {
                            }

                            routeNationales.Add(tempRouteNationaleObj);
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (routeNationales != null)
                {
                    for (int i = 0; i < routeNationales.Count; i++)
                    {
                        routeNationales[i].villes = serviceVille.selectVillesForRN(routeNationales[i].RouteNationale);
                    }
                }
            }
            #endregion

            return routeNationales;
        }

        bool IntfDalRouteNationale.isRouteNationale(crlRouteNationale routeNationale)
        {
            #region declaration
            bool isRN = false;
            #endregion

            #region implementation
            if (routeNationale != null)
            {
                this.strCommande = "SELECT * FROM `routenationale` WHERE (`routeNationale`='" + routeNationale.RouteNationale + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            if (routeNationale.RouteNationale.Trim().ToLower().Equals(this.reader["routeNationale"].ToString().Trim().ToLower()))
                            {
                                isRN = true;
                                break;
                            }
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isRN;
        }

        string IntfDalRouteNationale.isRouteNationale(crlRouteNationale routeNationale, string strRouteNationale)
        {
            #region declaration
            string strRN = "";
            #endregion

            #region implementation
            if (routeNationale != null)
            {
                this.strCommande = "SELECT * FROM `routenationale` WHERE (`routeNationale`='" + routeNationale.RouteNationale + "' AND";
                this.strCommande += " `routeNationale`<>'" + strRouteNationale + "')";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if(this.reader.Read())
                        {
                            strRN = this.reader["routeNationale"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return strRN;
        }

        #endregion

        #region insert to grid
        void IntfDalRouteNationale.insertToGridRouteNationale(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalRouteNationale serviceRouteNationale = new ImplDalRouteNationale();
            #endregion

            #region implementation
            this.strCommande = "SELECT * FROM `routenationale`";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param + " ASC";

            gridView.DataSource = serviceRouteNationale.getDataTableRouteNationale(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalRouteNationale.getDataTableRouteNationale(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region initialise table
            dataTable.Columns.Add("Route nationale", typeof(string));
            dataTable.Columns.Add("Distance", typeof(string));

            DataRow dr = null;
            #endregion

            #region implementation
            this.serviceConnectBase.openConnection();
            this.reader = this.serviceConnectBase.select(strRqst);
            if (this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    while (this.reader.Read())
                    {
                        dr = dataTable.NewRow();

                        dr["Route nationale"] = this.reader["routeNationale"].ToString();
                        dr["Distance"] = this.reader["distanceRN"].ToString() + "Km";

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            #endregion

            return dataTable;
        }

        void IntfDalRouteNationale.insertToGridRouteNationaleItineraire(GridView gridView, string param, string paramLike, string valueLike, string idItineraire)
        {
            #region declaration
            IntfDalRouteNationale serviceRouteNationale = new ImplDalRouteNationale();
            #endregion

            #region implementation
            this.strCommande = "SELECT routenationale.routeNationale, routenationale.distanceRN FROM `routenationale`";
            this.strCommande += " Inner Join assoitineraireroutenationale ON assoitineraireroutenationale.routeNationale = routenationale.routeNationale";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%' AND";
            this.strCommande += " assoitineraireroutenationale.idItineraire =  '" + idItineraire + "'";
            this.strCommande += " ORDER BY " + param + " ASC";

            gridView.DataSource = serviceRouteNationale.getDataTableRouteNationaleItineraire(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalRouteNationale.getDataTableRouteNationaleItineraire(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region initialise table
            dataTable.Columns.Add("Route nationale", typeof(string));
            dataTable.Columns.Add("Distance", typeof(string));

            DataRow dr = null;
            #endregion

            #region implementation
            this.serviceConnectBase.openConnection();
            this.reader = this.serviceConnectBase.select(strRqst);
            if (this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    while (this.reader.Read())
                    {
                        dr = dataTable.NewRow();

                        dr["Route nationale"] = this.reader["routeNationale"].ToString();
                        dr["Distance"] = this.reader["distanceRN"].ToString() + "Km";

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            #endregion

            return dataTable;
        }

        void IntfDalRouteNationale.insertToGridRouteNationaleNotItineraire(GridView gridView, string param, string paramLike, string valueLike, List<crlRouteNationale> routeNationales)
        {
            #region declaration
            IntfDalRouteNationale serviceRouteNationale = new ImplDalRouteNationale();
            string strWhere = "";
            #endregion

            #region implementation

            if (routeNationales != null)
            {
                for (int i = 0; i < routeNationales.Count; i++)
                {
                    strWhere += " routenationale.routeNationale <> '" + routeNationales[i].RouteNationale + "' AND";
                }
            }
            this.strCommande = "SELECT routenationale.routeNationale, routenationale.distanceRN FROM `routenationale`";
            this.strCommande += " WHERE " + strWhere;
            this.strCommande += " " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param + " ASC";

            gridView.DataSource = serviceRouteNationale.getDataTableRouteNationaleNotItineraire(this.strCommande);
            gridView.DataBind();
            #endregion
        }
        DataTable IntfDalRouteNationale.getDataTableRouteNationaleNotItineraire(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region initialise table
            dataTable.Columns.Add("Route nationale", typeof(string));
            dataTable.Columns.Add("Distance", typeof(string));

            DataRow dr = null;
            #endregion

            #region implementation
            this.serviceConnectBase.openConnection();
            this.reader = this.serviceConnectBase.select(strRqst);
            if (this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    while (this.reader.Read())
                    {
                        dr = dataTable.NewRow();

                        dr["Route nationale"] = this.reader["routeNationale"].ToString();
                        dr["Distance"] = this.reader["distanceRN"].ToString() + "Km";

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