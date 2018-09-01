using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.dal.intf;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;
using System.Data;
using arch.crl;


namespace arch.dal.impl
{
    /// <summary>
    /// Description résumée de ImplDalStatistique
    /// </summary>
    public class ImplDalStatistique : IntfDalStatistique
    {
        #region declaration variable
        ImplDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalStatistique()
        {
            this.serviceRessource = new ImplDalServiceRessource();
            this.serviceConnectBase = new ImplDalConnectBase(this.serviceRessource.getDefaultStrConnection());
        }
        public ImplDalStatistique(string strConnection)
        {
            this.serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion


        #region methode
        double IntfDalStatistique.getNombrePassager(string idItineraire, string dateD, string dateF)
        {
            #region declaration
            double nbPassager = 0;
            #endregion

            #region implementation
            if (idItineraire != null)
            {
                this.strCommande = "SELECT Count(voyage.idVoyage) AS nbPassager FROM voyage";
                this.strCommande += " Inner Join fichebord ON fichebord.numerosFB = voyage.numerosFB";
                this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.numerosAV = fichebord.numerosAV";
                this.strCommande += " Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
                this.strCommande += " Inner Join autorisationdepart ON autorisationdepart.numerosFB = voyage.numerosFB";
                this.strCommande += " WHERE autorisationdepart.dateAD <= '" + dateF + "' AND";
                this.strCommande += " autorisationdepart.dateAD >= '" + dateD + "' AND";
                this.strCommande += " verification.idItineraire = '" + idItineraire + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            try
                            {
                                nbPassager = double.Parse(this.reader["nbPassager"].ToString());
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

            return nbPassager;
        }
        #endregion

        #region insert to grid jour
        void IntfDalStatistique.insertToGridCANombrePassagerJour(GridView gridView, DateTime dateDebut, DateTime dateFin, List<crlItineraire> itineraires)
        {
            #region declaration
            IntfDalStatistique serviceStatistique = new ImplDalStatistique();
            DateTime dateD;
            DateTime dateF;
            #endregion

            #region implementation

            if (dateDebut > dateFin)
            {
                dateD = dateFin;
                dateF = dateDebut;
            }
            else
            {
                dateD = dateDebut;
                dateF = dateFin;
            }

            gridView.DataSource = serviceStatistique.getDataTableNombrePassagerJour(dateD, dateF, itineraires);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalStatistique.getDataTableNombrePassagerJour(DateTime dateDebut, DateTime dateFin, List<crlItineraire> itineraires)
        {
            #region declaration
            DataTable dataTable = new DataTable();

            IntfDalStatistique serviceStatistique = new ImplDalStatistique();

            DateTime dateD = dateDebut;
            DateTime dateF = dateFin;

            string strAxe = "";
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();

            dataTable.Columns.Add("Itineraire", typeof(string));
            dataTable.Columns.Add("Axe", typeof(string));
            while (dateD <= dateF)
            {
                dataTable.Columns.Add(dateD.ToString("dd MMMM yyyy"), typeof(string));
                dateD = dateD.AddDays(1);
            }
            
            DataRow dr;
            #endregion

            for (int i = 0; i < itineraires.Count; i++)
            {
                dateD = dateDebut;
                dateF = dateFin;

                dr = dataTable.NewRow();
                strAxe = "";

                if (itineraires[i] != null)
                {
                    if (itineraires[i].routeNationale != null)
                    {
                        for (int j = 0; j < itineraires[i].routeNationale.Count; j++)
                        {
                            if (j == 0)
                            {
                                strAxe = strAxe + itineraires[i].routeNationale[j].RouteNationale;
                            }
                            else
                            {
                                strAxe = strAxe + "-" + itineraires[i].routeNationale[j].RouteNationale;
                            }
                        }
                    }
                }

                dr["Itineraire"] = itineraires[i].villeD.NomVille + "-" + itineraires[i].villeF.NomVille;
                dr["Axe"] = strAxe;

                while (dateD <= dateF)
                {
                    dr[dateD.ToString("dd MMMM yyyy")] = serviceStatistique.getNombrePassager(itineraires[i].IdItineraire, dateD.ToString("yyyy-MM-dd"), dateD.ToString("yyyy-MM-dd")).ToString("0");

                    dateD = dateD.AddDays(1);
                }

                dataTable.Rows.Add(dr);
            }


                

                       
                        
                    

            #endregion

            return dataTable;
        }
        #endregion

        #region insert to grid Mois
        void IntfDalStatistique.insertToGridCANombrePassagerMois(GridView gridView, DateTime dateDebut, DateTime dateFin, List<crlItineraire> itineraires)
        {
            #region declaration
            IntfDalStatistique serviceStatistique = new ImplDalStatistique();
            DateTime dateD;
            DateTime dateF;
            #endregion

            #region implementation

            if (dateDebut > dateFin)
            {
                dateD = dateFin;
                dateF = dateDebut;
            }
            else
            {
                dateD = dateDebut;
                dateF = dateFin;
            }

            gridView.DataSource = serviceStatistique.getDataTableNombrePassagerMois(dateD, dateF, itineraires);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalStatistique.getDataTableNombrePassagerMois(DateTime dateDebut, DateTime dateFin, List<crlItineraire> itineraires)
        {
            #region declaration
            DataTable dataTable = new DataTable();

            IntfDalStatistique serviceStatistique = new ImplDalStatistique();

            DateTime dateD = dateDebut;
            DateTime dateF = dateFin;

            string strAxe = "";
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();

            dataTable.Columns.Add("Itineraire", typeof(string));
            dataTable.Columns.Add("Axe", typeof(string));
            while (dateD <= dateF)
            {
                dataTable.Columns.Add(dateD.ToString("MMMM yyyy"), typeof(string));
                dateD = dateD.AddMonths(1);
            }

            DataRow dr;
            #endregion

            for (int i = 0; i < itineraires.Count; i++)
            {
                dateD = dateDebut;
                dateF = dateFin;

                dr = dataTable.NewRow();
                strAxe = "";

                if (itineraires[i] != null)
                {
                    if (itineraires[i].routeNationale != null)
                    {
                        for (int j = 0; j < itineraires[i].routeNationale.Count; j++)
                        {
                            if (j == 0)
                            {
                                strAxe = strAxe + itineraires[i].routeNationale[j].RouteNationale;
                            }
                            else
                            {
                                strAxe = strAxe + "-" + itineraires[i].routeNationale[j].RouteNationale;
                            }
                        }
                    }
                }

                dr["Itineraire"] = itineraires[i].villeD.NomVille + "-" + itineraires[i].villeF.NomVille;
                dr["Axe"] = strAxe;

                while (dateD <= dateF)
                {
                    dr[dateD.ToString("MMMM yyyy")] = serviceStatistique.getNombrePassager(itineraires[i].IdItineraire, dateD.ToString("yyyy-MM"), dateD.ToString("yyyy-MM") + "-32").ToString("0");

                    dateD = dateD.AddMonths(1);
                }

                dataTable.Rows.Add(dr);
            }








            #endregion

            return dataTable;
        }
        #endregion

        #region insert to grid annee
        void IntfDalStatistique.insertToGridCANombrePassagerAnnee(GridView gridView, DateTime dateDebut, DateTime dateFin, List<crlItineraire> itineraires)
        {
            #region declaration
            IntfDalStatistique serviceStatistique = new ImplDalStatistique();
            DateTime dateD;
            DateTime dateF;
            #endregion

            #region implementation

            if (dateDebut > dateFin)
            {
                dateD = dateFin;
                dateF = dateDebut;
            }
            else
            {
                dateD = dateDebut;
                dateF = dateFin;
            }

            gridView.DataSource = serviceStatistique.getDataTableNombrePassagerAnnee(dateD, dateF, itineraires);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalStatistique.getDataTableNombrePassagerAnnee(DateTime dateDebut, DateTime dateFin, List<crlItineraire> itineraires)
        {
            #region declaration
            DataTable dataTable = new DataTable();

            IntfDalStatistique serviceStatistique = new ImplDalStatistique();

            DateTime dateD = dateDebut;
            DateTime dateF = dateFin;

            string strAxe = "";
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();

            dataTable.Columns.Add("Itineraire", typeof(string));
            dataTable.Columns.Add("Axe", typeof(string));
            while (dateD <= dateF)
            {
                dataTable.Columns.Add(dateD.ToString("yyyy"), typeof(string));
                dateD = dateD.AddYears(1);
            }

            DataRow dr;
            #endregion

            for (int i = 0; i < itineraires.Count; i++)
            {
                dateD = dateDebut;
                dateF = dateFin;

                dr = dataTable.NewRow();
                strAxe = "";

                if (itineraires[i] != null)
                {
                    if (itineraires[i].routeNationale != null)
                    {
                        for (int j = 0; j < itineraires[i].routeNationale.Count; j++)
                        {
                            if (j == 0)
                            {
                                strAxe = strAxe + itineraires[i].routeNationale[j].RouteNationale;
                            }
                            else
                            {
                                strAxe = strAxe + "-" + itineraires[i].routeNationale[j].RouteNationale;
                            }
                        }
                    }
                }

                dr["Itineraire"] = itineraires[i].villeD.NomVille + "-" + itineraires[i].villeF.NomVille;
                dr["Axe"] = strAxe;

                while (dateD <= dateF)
                {
                    dr[dateD.ToString("yyyy")] = serviceStatistique.getNombrePassager(itineraires[i].IdItineraire, dateD.ToString("yyyy"), dateD.ToString("yyyy") + "-13").ToString("0");

                    dateD = dateD.AddYears(1);
                }

                dataTable.Rows.Add(dr);
            }








            #endregion

            return dataTable;
        }
        #endregion
    }
}