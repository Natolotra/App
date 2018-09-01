using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.dal.intf;
using arch.crl;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;
using System.Data;

namespace arch.dal.impl
{
    /// <summary>
    /// Description résumée de ImplDalVille
    /// </summary>
    public class ImplDalVille : IntfDalVille
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalVille()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalVille(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region IntfDalAgent Members
        string IntfDalVille.insertVille(crlVille Ville, string sigleAgence)
        {
            #region declaration
            IntfDalVille serviceVille = new ImplDalVille();
            int nombreInsertion = 0;
            string numVille = "";
            #endregion

            #region implementation
            if (Ville != null)
            {
                Ville.NumVille = serviceVille.getNumVille(sigleAgence);

                this.strCommande = "INSERT INTO `ville` (`numVille`,`nomVille`,`nomRegion`,`nomProvince`)";
                this.strCommande += " VALUES ('" + Ville.NumVille + "','" + Ville.NomVille + "',";
                this.strCommande += "'" + Ville.NomRegion + "', '" + Ville.NomProvince + "')";
               
                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    numVille = Ville.NumVille;
                this.serviceConnectBase.closeConnection();
                
            }
            #endregion

            return numVille;
        }

        bool IntfDalVille.deleteVille(crlVille Ville)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (Ville != null)
            {
                if (Ville.NumVille != "")
                {
                    this.strCommande = "DELETE FROM `ville` WHERE (`numVille` = '" + Ville.NumVille + "')";
                    this.serviceConnectBase.openConnection();
                    nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                    if (nombreDelete == 1)
                        isDelete = true;
                    this.serviceConnectBase.closeConnection();
                }
            }
            #endregion

            return isDelete;
        }

        bool IntfDalVille.deleteVille(string numVille)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
           
            if (numVille != "")
            {
                this.strCommande = "DELETE FROM `ville` WHERE (`numVille` = '" + numVille + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete == 1)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }
           
            #endregion

            return isDelete;
        }

        bool IntfDalVille.updateVille(crlVille Ville)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            #endregion

            #region implementation
            if (Ville != null)
            {
                if (Ville.NumVille != "")
                {
                    this.strCommande = "UPDATE `ville` SET `nomVille`='" + Ville.NomVille + "',";
                    this.strCommande += " `nomRegion`='" + Ville.NomRegion + "',`nomProvince`='" + Ville.NomProvince +"'";
                    this.strCommande += " WHERE (`numVille`='" + Ville.NumVille +"')";

                    this.serviceConnectBase.openConnection();
                    nombreUpdate = this.serviceConnectBase.requete(this.strCommande);
                    if (nombreUpdate == 1)
                        isUpdate = true;
                    this.serviceConnectBase.closeConnection();
                }
            }
            #endregion

            return isUpdate;
        }

        int IntfDalVille.isVilleInt(crlVille Ville)
        {
            #region declaration
            int isVille = 0;
            #endregion

            #region implementation
            if (Ville != null)
            {
                this.strCommande = "SELECT * FROM `ville` WHERE (`numVille`<>'" + Ville.NumVille + "' AND";
                this.strCommande += " `nomVille`='" + Ville.NomVille + "' AND `nomRegion`='" + Ville.NomRegion + "' AND";
                this.strCommande += " `nomProvince`='" + Ville.NomProvince + "')";
                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (Ville.NomVille.Trim().ToLower().Equals(reader["nomVille"].ToString().Trim().ToLower()) && 
                                Ville.NomRegion.Trim().ToLower().Equals(reader["nomRegion"].ToString().Trim().ToLower()) &&
                                Ville.NomProvince.Trim().ToLower().Equals(reader["nomProvince"].ToString().Trim().ToLower()))
                            {
                                isVille = 1;
                                break;
                            }
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isVille;
        }

        string IntfDalVille.isVille(crlVille Ville)
        {
            #region declaration
            string isVille = "";
            #endregion

            #region implementation
            if (Ville != null)
            {
                this.strCommande = "SELECT * FROM `ville` WHERE (`numVille`<>'" + Ville.NumVille + "' AND";
                this.strCommande += " `nomVille`='" + Ville.NomVille + "' AND `nomRegion`='" + Ville.NomRegion + "' AND";
                this.strCommande += " `nomProvince`='" + Ville.NomProvince + "')";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (Ville.NomVille.Trim().ToLower().Equals(reader["nomVille"].ToString().Trim().ToLower()) &&
                                Ville.NomRegion.Trim().ToLower().Equals(reader["nomRegion"].ToString().Trim().ToLower()) &&
                                Ville.NomProvince.Trim().ToLower().Equals(reader["nomProvince"].ToString().Trim().ToLower()))
                            {
                                isVille = reader["numVille"].ToString();
                                break;
                            }
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isVille;
        }

        crlVille IntfDalVille.selectVille(string numVille)
        {
            #region declaration
            crlVille Ville = null;
            #endregion

            #region implementation
            if (numVille != "")
            {
                this.strCommande = "SELECT * FROM `ville` WHERE (`numVille`='" + numVille + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            Ville = new crlVille();
                            Ville.NumVille = reader["numVille"].ToString();
                            Ville.NomVille = reader["nomVille"].ToString();
                            Ville.NomRegion = reader["nomRegion"].ToString();
                            Ville.NomProvince = reader["nomProvince"].ToString();

                            Ville.region = new crlRegion();
                            Ville.region.NomRegion = Ville.NomRegion;

                            Ville.province = new crlProvince();
                            Ville.province.nomProvince = Ville.NomProvince;
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return Ville;
        }

        crlVille IntfDalVille.selectVilleNom(string nomVille)
        {
            #region declaration
            crlVille Ville = null;
            #endregion

            #region implementation
            if (nomVille != "")
            {
                this.strCommande = "SELECT * FROM `ville` WHERE (`nomVille`='" + nomVille + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            Ville = new crlVille();
                            Ville.NumVille = reader["numVille"].ToString();
                            Ville.NomVille = reader["nomVille"].ToString();
                            Ville.NomRegion = reader["nomRegion"].ToString();
                            Ville.NomProvince = reader["nomProvince"].ToString();

                            Ville.region = new crlRegion();
                            Ville.region.NomRegion = Ville.NomRegion;

                            Ville.province = new crlProvince();
                            Ville.province.nomProvince = Ville.NomProvince;
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return Ville;
        }

        List<crlVille> IntfDalVille.selectVillesForItineraire(string idItineraire)
        {
            #region declaration
            List<crlVille> Villes = null;
            crlVille tempVille = null;
            #endregion

            #region implementation
            if (idItineraire != "")
            {
                this.strCommande = "SELECT ville.numVille, ville.nomVille, ville.nomRegion, ville.nomProvince";
                this.strCommande += " FROM ville Inner Join asociationvilleitineraire ON asociationvilleitineraire.numVille = ville.numVille";
                this.strCommande += " WHERE asociationvilleitineraire.idItineraire =  '" + idItineraire + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        Villes = new List<crlVille>();
                        while(this.reader.Read())
                        {
                            tempVille = new crlVille();
                            tempVille.NumVille = reader["numVille"].ToString();
                            tempVille.NomVille = reader["nomVille"].ToString();
                            tempVille.NomRegion = reader["nomRegion"].ToString();
                            tempVille.NomProvince = reader["nomProvince"].ToString();

                            tempVille.region = new crlRegion();
                            tempVille.region.NomRegion = tempVille.NomRegion;

                            tempVille.province = new crlProvince();
                            tempVille.province.nomProvince = tempVille.NomProvince;

                            Villes.Add(tempVille);
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return Villes;
        }

        List<crlVille> IntfDalVille.selectVillesForRN(string routeNationale)
        {
            #region declaration
            List<crlVille> Villes = null;
            crlVille tempVille = null;
            #endregion

            #region implementation
            if (routeNationale != "")
            {
                this.strCommande = "SELECT ville.numVille, ville.nomVille, ville.nomRegion,";
                this.strCommande += " ville.nomProvince FROM ville";
                this.strCommande += " Inner Join assovilleroutenationale ON assovilleroutenationale.numVille = ville.numVille";
                this.strCommande += " WHERE assovilleroutenationale.routeNationale = '" + routeNationale + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        Villes = new List<crlVille>();
                        while (this.reader.Read())
                        {
                            tempVille = new crlVille();
                            tempVille.NumVille = reader["numVille"].ToString();
                            tempVille.NomVille = reader["nomVille"].ToString();
                            tempVille.NomRegion = reader["nomRegion"].ToString();
                            tempVille.NomProvince = reader["nomProvince"].ToString();

                            tempVille.region = new crlRegion();
                            tempVille.region.NomRegion = tempVille.NomRegion;

                            tempVille.province = new crlProvince();
                            tempVille.province.nomProvince = tempVille.NomProvince;

                            Villes.Add(tempVille);
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return Villes;
        }

        string IntfDalVille.getNumVille(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numVille = "00001";
            string[] tempNumVille = null;
            string strDate = "VI" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT ville.numVille AS maxNum FROM ville";
            this.strCommande += " WHERE ville.numVille LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumVille = reader["maxNum"].ToString().ToString().Split('/');
                        numVille = tempNumVille[tempNumVille.Length - 1];
                    }
                    numTemp = double.Parse(numVille) + 1;
                    if (numTemp < 10)
                        numVille = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numVille = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numVille = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numVille = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numVille = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numVille = strDate + "/" + sigleAgence + "/" + numVille;
            #endregion

            return numVille;
        }
        #endregion

        #region insert data grid

        void IntfDalVille.insertToGridVille(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalVille serviceVille = new ImplDalVille();
            #endregion

            #region implementation
           


            this.strCommande = "SELECT ville.numVille, ville.nomVille, ville.nomRegion, ville.nomProvince";
            this.strCommande += " FROM ville WHERE " + paramLike + " LIKE  '%" + valueLike + "%' ORDER BY " + param;


            gridView.DataSource = serviceVille.getDataTableVille(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        void IntfDalVille.insertToGridVilleDestination(GridView gridView, string param, string paramLike, string valueLike, string numVille)
        {
            #region declaration
            IntfDalVille serviceVille = new ImplDalVille();
            #endregion

            #region implementation



            this.strCommande = "SELECT ville.numVille, ville.nomVille, ville.nomRegion, ville.nomProvince FROM ville";
            this.strCommande += " Inner Join trajet ON trajet.numVilleD = ville.numVille OR trajet.numVilleF = ville.numVille";
            this.strCommande += " WHERE ville.numVille <>  '" + numVille + "' AND " + paramLike + " LIKE  '%" + valueLike + "%'";
            this.strCommande += " AND ( trajet.numVilleD =  '" + numVille + "' OR trajet.numVilleF =  '" + numVille + "' )";
            this.strCommande += " GROUP BY ville.nomVille ORDER BY " + param;


            gridView.DataSource = serviceVille.getDataTableVille(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalVille.getDataTableVille(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numVille", typeof(string));
            dataTable.Columns.Add("nomVille", typeof(string));
            dataTable.Columns.Add("nomRegion", typeof(string));
            dataTable.Columns.Add("nomProvince", typeof(string));
          
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

                        dr["numVille"] = reader["numVille"].ToString();
                        dr["nomVille"] = reader["nomVille"].ToString();
                        dr["nomRegion"] = reader["nomRegion"].ToString();
                        dr["nomProvince"] = reader["nomProvince"].ToString();

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }

        void IntfDalVille.insertToGridVilleRN(GridView gridView, string param, string paramLike, string valueLike, string routeNationale)
        {
            #region declaration
            IntfDalVille serviceVille = new ImplDalVille();
            #endregion

            #region implementation



            this.strCommande = "SELECT ville.numVille, ville.nomVille, ville.nomRegion, ville.nomProvince";
            this.strCommande += " FROM ville Inner Join assovilleroutenationale ON assovilleroutenationale.numVille = ville.numVille";
            this.strCommande += " WHERE " + paramLike + " LIKE  '%" + valueLike + "%' AND";
            this.strCommande += " assovilleroutenationale.routeNationale = '" + routeNationale + "'";
            this.strCommande += " ORDER BY " + param;


            gridView.DataSource = serviceVille.getDataTableVilleRN(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalVille.getDataTableVilleRN(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numVille", typeof(string));
            dataTable.Columns.Add("nomVille", typeof(string));
            dataTable.Columns.Add("nomRegion", typeof(string));
            dataTable.Columns.Add("nomProvince", typeof(string));

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

                        dr["numVille"] = reader["numVille"].ToString();
                        dr["nomVille"] = reader["nomVille"].ToString();
                        dr["nomRegion"] = reader["nomRegion"].ToString();
                        dr["nomProvince"] = reader["nomProvince"].ToString();

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }

        void IntfDalVille.insertToGridVilleNotRN(GridView gridView, string param, string paramLike, string valueLike, List<crlVille> villes)
        {
            #region declaration
            IntfDalVille serviceVille = new ImplDalVille();

            string strWhere = "";
            #endregion

            #region implementation

            if (villes != null)
            {
                for (int i = 0; i < villes.Count; i++)
                {
                    strWhere += " ville.numVille <> '" + villes[i].NumVille + "' AND";
                }
            }

            this.strCommande = "SELECT ville.numVille, ville.nomVille, ville.nomRegion, ville.nomProvince FROM ville";
            this.strCommande += " WHERE " + strWhere;
            this.strCommande += " " + paramLike + " LIKE  '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;


            gridView.DataSource = serviceVille.getDataTableVilleNotRN(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalVille.getDataTableVilleNotRN(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numVille", typeof(string));
            dataTable.Columns.Add("nomVille", typeof(string));
            dataTable.Columns.Add("nomRegion", typeof(string));
            dataTable.Columns.Add("nomProvince", typeof(string));

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

                        dr["numVille"] = reader["numVille"].ToString();
                        dr["nomVille"] = reader["nomVille"].ToString();
                        dr["nomRegion"] = reader["nomRegion"].ToString();
                        dr["nomProvince"] = reader["nomProvince"].ToString();

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

        void IntfDalVille.loadDdlVilleA(DropDownList ddl)
        {
            #region implementation
            ddl.Items.Clear();
            this.strCommande = "SELECT * FROM `ville` ORDER BY ville.nomVille ASC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(this.strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    ddl.Items.Add("");
                    while (reader.Read())
                    {
                        ddl.Items.Add(new ListItem(reader["nomVille"].ToString(), reader["numVille"].ToString()));
                    }
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            #endregion
        }

        void IntfDalVille.loadDdlVille(DropDownList ddl)
        {
            #region implementation
            ddl.Items.Clear();
            this.strCommande = "SELECT * FROM `ville` ORDER BY ville.nomVille ASC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(this.strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ddl.Items.Add(new ListItem(reader["nomVille"].ToString(), reader["numVille"].ToString()));
                    }
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            #endregion
        }

        void IntfDalVille.loadDdlVille(DropDownList ddl, string numVille)
        {
            #region implementation
            ddl.Items.Clear();
            this.strCommande = "SELECT * FROM `ville` WHERE `numVille`<>'" + numVille + "' ORDER BY ville.nomVille ASC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(this.strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ddl.Items.Add(new ListItem(reader["nomVille"].ToString(), reader["numVille"].ToString()));
                    }
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            #endregion
        }

        void IntfDalVille.loadDdlVilleDestination(DropDownList ddl, string numVilleDepart)
        {
            #region implementation
            ddl.Items.Clear();
            this.strCommande = "SELECT ville.numVille, ville.nomVille FROM ville";
            this.strCommande += " Inner Join associationvilletrajet ON associationvilletrajet.numVille = ville.numVille";
            this.strCommande += " Inner Join trajet ON trajet.numTrajet = associationvilletrajet.numTrajet";
            this.strCommande += " WHERE (trajet.numVilleD =  '" + numVilleDepart + "' OR";
            this.strCommande += " trajet.numVilleF =  '" + numVilleDepart + "') AND";
            this.strCommande += " ville.numVille <>  '" + numVilleDepart + "' GROUP BY ville.numVille ORDER BY ville.nomVille ASC";

            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(this.strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    ddl.Items.Add("");
                    while (reader.Read())
                    {
                        ddl.Items.Add(new ListItem(reader["nomVille"].ToString(), reader["numVille"].ToString()));
                    }
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            #endregion
        }

        /*
        crlVille IntfDalVille.selectVilleForTrajet(string numTrajet, string NumVille)
        {
            #region declaration
            crlVille Ville = null;
            #endregion

            #region implementation
            if (numTrajet != "" && NumVille != "")
            {
                this.strCommande = "SELECT ville.NumVille, ville.nomVille, ville.NomRegion, ville.NomProvince FROM ville";
                this.strCommande += " Inner Join associationvilletrajet ON associationvilletrajet.NumVille = ville.NumVille";
                this.strCommande += " Inner Join trajet ON trajet.numTrajet = associationvilletrajet.numTrajet";
                this.strCommande += " WHERE ville.NumVille = '" + NumVille + "' AND trajet.numTrajet =  '" + numTrajet + "'";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            Ville = new crlVille();
                            Ville.NumVille = reader["NumVille"].ToString();
                            Ville.NomVille = reader["nomVille"].ToString();
                            Ville.NomRegion = reader["NomRegion"].ToString();
                            Ville.NomProvince = reader["NomProvince"].ToString();

                            Ville.region = new crlRegion();
                            Ville.region.NomRegion = Ville.NomRegion;

                            Ville.province = new crlProvince();
                            Ville.province.NomProvince = Ville.NomProvince;
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return Ville;
        }


        List<crlVille> IntfDalVille.selectVilleTrajet(string NumVille)
        {
            #region declaration variable
            List<crlVille> villes = null;
            List<crlTrajet> trajets = null;

            IntfDalVille serviceVille = new ImplDalVille();
            IntfDalTrajet serviceTrajet = new ImplDalTrajet();
            #endregion

            #region implementation
            trajets = serviceTrajet.selectTrajetsVille(NumVille);

            if (trajets != null)
            {
                villes = new List<crlVille>();
                for (int i = 0; i < trajets.Count; i++)
                {
                    villes.Add(serviceVille.selectVilleForTrajet(trajets[i].NumTrajet, NumVille));
                }
            }
            #endregion

            return villes;
        }
        */


        
    }
}