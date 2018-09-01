using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.dal.intf;
using MySql.Data.MySqlClient;
using System.Data;

namespace arch.dal.impl
{
    /// <summary>
    /// Implementation du service Calendar
    /// </summary>
    public class ImplDalCalendar : IntfDalCalendar
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalCalendar()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalCalendar(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        DataTable IntfDalCalendar.getDataTableForCalendar(string idItineraire, string numAgence)
        {
            #region declaration
            IntfDalCalendar intfDServiceCalendar = new ImplDalCalendar();
            DataTable dataTable = null;
            #endregion

            #region implementation
            this.strCommande = "SELECT fichebord.dateHeurPrevue, fichebord.numerosFB AS numFB,";
            this.strCommande += " vehicule.matriculeVehicule FROM fichebord";
            this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.numerosAV = fichebord.numerosAV";
            this.strCommande += " Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
            this.strCommande += " Inner Join licence ON licence.numLicence = verification.numLicence";
            this.strCommande += " Inner Join vehicule ON vehicule.numVehicule = licence.numVehicule";
            this.strCommande += " Inner Join agent ON agent.matriculeAgent = fichebord.matriculeAgent";
            this.strCommande += " WHERE verification.idItineraire LIKE  '%" + idItineraire + "%' AND";
            this.strCommande += " agent.numAgence = '" + numAgence + "'";
            this.strCommande += " GROUP BY fichebord.numerosFB";


            dataTable = intfDServiceCalendar.getDataTable(this.strCommande);

            dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["id"] };
            #endregion

            return dataTable;
        }

        DataTable IntfDalCalendar.getDataTable(string strSQL)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            DateTime dateDeDepart;
            #endregion

            #region implementation
            dataTable.Columns.Add("start", typeof(DateTime));
            dataTable.Columns.Add("end", typeof(DateTime));
            dataTable.Columns.Add("name", typeof(string));
            dataTable.Columns.Add("id", typeof(string));
            //dataTable.Columns.Add("color", typeof(string));
            DataRow dr;

            this.serviceConnectBase.openConnection();
            this.reader = this.serviceConnectBase.select(strSQL);
            if (this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dr = dataTable.NewRow();
                        dr["id"] = reader["numFB"].ToString();
                        dateDeDepart = Convert.ToDateTime(reader["dateHeurPrevue"].ToString());
                        dr["start"] = dateDeDepart;
                        dr["end"] = dateDeDepart.AddHours(1);
                        dr["name"] = reader["numImmatricule"].ToString();

                        dataTable.Rows.Add(dr);
                    }

                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            #endregion

            return dataTable;
        }


        #region Fiche de bord Autorisation d depart is null
        DataTable IntfDalCalendar.getDTCalendarFBADIsNull(string idItineraire, string numAgence)
        {
            #region declaration
            IntfDalCalendar intfDServiceCalendar = new ImplDalCalendar();
            DataTable dataTable = null;
            #endregion

            #region implementation
            this.strCommande = "SELECT fichebord.dateHeurPrevue, fichebord.numerosFB AS numFB,";
            this.strCommande += " vehicule.matriculeVehicule, Sum(placefb.isOccuper) AS nbOccuper,";
            this.strCommande += " (paramvehicule.nbPassagerMin) AS nbPlaceMin, Count(placefb.numerosFB) AS nbPlace";
            this.strCommande += " FROM fichebord";
            this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.numerosAV = fichebord.numerosAV";
            this.strCommande += " Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
            this.strCommande += " Inner Join licence ON licence.numLicence = verification.numLicence";
            this.strCommande += " Inner Join vehicule ON vehicule.numVehicule = licence.numVehicule";
            this.strCommande += " Inner Join placefb ON placefb.numerosFB = fichebord.numerosFB";
            this.strCommande += " Left Join autorisationdepart ON autorisationdepart.numerosFB = fichebord.numerosFB";
            this.strCommande += " Inner Join agent ON agent.matriculeAgent = fichebord.matriculeAgent";
            this.strCommande += " Inner Join paramvehicule ON paramvehicule.numParamVehicule = vehicule.numParamVehicule";
            this.strCommande += " WHERE verification.idItineraire LIKE  '%" + idItineraire + "%' AND";
            this.strCommande += " agent.numAgence = '" + numAgence + "' AND";
            this.strCommande += " autorisationdepart.numerosFB IS NULL";
            this.strCommande += " GROUP BY fichebord.numerosFB";


            dataTable = intfDServiceCalendar.getDTCalendarFBADIsNullSQL(this.strCommande);

            dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["id"] };
            #endregion

            return dataTable;
        }

        DataTable IntfDalCalendar.getDTCalendarFBADIsNullSQL(string strSQL)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            DateTime dateDeDepart;

            int nbPlaceMin = 0;
            int nbPlaceOccuper = 0;
            int nbPlace = 0;
            #endregion

            #region implementation
            dataTable.Columns.Add("start", typeof(DateTime));
            dataTable.Columns.Add("end", typeof(DateTime));
            dataTable.Columns.Add("name", typeof(string));
            dataTable.Columns.Add("id", typeof(string));
            dataTable.Columns.Add("color", typeof(string));
            DataRow dr;

            this.serviceConnectBase.openConnection();
            this.reader = this.serviceConnectBase.select(strSQL);
            if (this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dr = dataTable.NewRow();
                        dr["id"] = reader["numFB"].ToString();
                        dateDeDepart = Convert.ToDateTime(reader["dateHeurPrevue"].ToString());
                        dr["start"] = dateDeDepart;
                        dr["end"] = dateDeDepart.AddHours(1);
                        dr["name"] = reader["matriculeVehicule"].ToString();


                        try
                        {
                            nbPlaceOccuper = int.Parse(reader["nbOccuper"].ToString());
                        }
                        catch (Exception)
                        {
                        }

                        try
                        {
                            nbPlaceMin = int.Parse(reader["nbPlaceMin"].ToString());
                        }
                        catch (Exception)
                        {
                        }

                        try
                        {
                            nbPlace = int.Parse(reader["nbPlace"].ToString());
                        }
                        catch(Exception)
                        {}

                        if (nbPlaceOccuper < nbPlaceMin)
                        {
                            dr["color"] = "0";
                        }
                        else 
                        {
                            if (nbPlaceOccuper == nbPlace)
                            {
                                dr["color"] = "2";
                            }
                            else
                            {
                                dr["color"] = "1";
                            }
                        }
                        

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


        #region Fiche de bord Autorisation d depart is not  null(mananaautorisation de depart)
        DataTable IntfDalCalendar.getDTCalendarFBADIsNotNull(string idItineraire, string numAgence)
        {
            #region declaration
            IntfDalCalendar intfDServiceCalendar = new ImplDalCalendar();
            DataTable dataTable = null;
            #endregion

            #region implementation
            this.strCommande = "SELECT fichebord.dateHeurPrevue, fichebord.numerosFB AS numFB,";
            this.strCommande += " vehicule.matriculeVehicule, Sum(placefb.isOccuper) AS nbOccuper, (paramvehicule.nbPassagerMin) AS nbPlaceMin,";
            this.strCommande += " Count(placefb.numerosFB) AS nbPlace, (autorisationdepart.numAutorisationDepart) AS numAD FROM fichebord";
            this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.numerosAV = fichebord.numerosAV";
            this.strCommande += " Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
            this.strCommande += " Inner Join licence ON licence.numLicence = verification.numLicence";
            this.strCommande += " Inner Join vehicule ON vehicule.numVehicule = licence.numVehicule";
            this.strCommande += " Inner Join placefb ON placefb.numerosFB = fichebord.numerosFB";
            this.strCommande += " Left Join autorisationdepart ON autorisationdepart.numerosFB = fichebord.numerosFB";
            this.strCommande += " Inner Join agent ON agent.matriculeAgent = fichebord.matriculeAgent";
            this.strCommande += " Inner Join paramvehicule ON paramvehicule.numParamVehicule = vehicule.numParamVehicule";
            this.strCommande += " WHERE verification.idItineraire LIKE  '%" + idItineraire + "%' AND";
            this.strCommande += " agent.numAgence = '" + numAgence + "' AND";
            this.strCommande += " autorisationdepart.numerosFB  IS NOT NULL";
            this.strCommande += " GROUP BY fichebord.numerosFB";


            dataTable = intfDServiceCalendar.getDTCalendarFBADIsNotNullSQL(this.strCommande);

            dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["id"] };
            #endregion

            return dataTable;
        }

        DataTable IntfDalCalendar.getDTCalendarFBADIsNotNullSQL(string strSQL)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            DateTime dateDeDepart;

            int nbPlaceMin = 0;
            int nbPlaceOccuper = 0;
            int nbPlace = 0;
            #endregion

            #region implementation
            dataTable.Columns.Add("start", typeof(DateTime));
            dataTable.Columns.Add("end", typeof(DateTime));
            dataTable.Columns.Add("name", typeof(string));
            dataTable.Columns.Add("id", typeof(string));
            dataTable.Columns.Add("color", typeof(string));
            DataRow dr;

            this.serviceConnectBase.openConnection();
            this.reader = this.serviceConnectBase.select(strSQL);
            if (this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dr = dataTable.NewRow();
                        dr["id"] = reader["numAD"].ToString();
                        dateDeDepart = Convert.ToDateTime(reader["dateHeurPrevue"].ToString());
                        dr["start"] = dateDeDepart;
                        dr["end"] = dateDeDepart.AddHours(1);
                        dr["name"] = reader["matriculeVehicule"].ToString();

                        try
                        {
                            nbPlaceOccuper = int.Parse(reader["nbOccuper"].ToString());
                        }
                        catch (Exception)
                        {
                        }

                        try
                        {
                            nbPlaceMin = int.Parse(reader["nbPlaceMin"].ToString());
                        }
                        catch (Exception)
                        {
                        }

                        try
                        {
                            nbPlace = int.Parse(reader["nbPlace"].ToString());
                        }
                        catch (Exception)
                        {
                        }

                        if (nbPlaceOccuper < nbPlaceMin)
                        {
                            dr["color"] = "0";
                        }
                        else
                        {
                            if (nbPlaceOccuper == nbPlace)
                            {
                                dr["color"] = "2";
                            }
                            else
                            {
                                dr["color"] = "1";
                            }
                        }

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