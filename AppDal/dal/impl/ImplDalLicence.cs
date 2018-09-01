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
using System.Collections.Generic;

namespace arch.dal.impl
{
    /// <summary>
    /// Implementation du service licence
    /// </summary>
    public class ImplDalLicence : IntfDalLicence
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalLicence()
        {
            this.serviceRessource = new ImplDalServiceRessource();
            this.serviceConnectBase = new ImplDalConnectBase(this.serviceRessource.getDefaultStrConnection());
        }
        public ImplDalLicence(string strConnection)
        {
            this.serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region IntfDalLicence Members

        string IntfDalLicence.insertLicence(crlLicence Licence, string sigleAgence)
        {
            #region declaration
            int nombreInsertion = 0;
            string numLicence = "";
            IntfDalLicence serviceLicence = new ImplDalLicence();
            #endregion

            #region implementation
            if (Licence != null)
            {
                Licence.NumLicence = serviceLicence.getNumLicence(sigleAgence);

                this.strCommande = "INSERT INTO `licence` (`numLicence`,`numerosLicence`,`zone`,`numCooperative`,";
                this.strCommande += " `numVehicule`,`datePremiereMiseCiculation`,`datePremiereExploitation`,";
                this.strCommande += " `valideAu`,`valideDu`,`nombrePlacePayante`,`isProvisoire`)";
                this.strCommande += " VALUES ('" + Licence.NumLicence + "','" + Licence.NumerosLicence + "'";
                this.strCommande += " ,'" + Licence.Zone+ "','" + Licence.NumCooperative + "'";
                this.strCommande += " ,'" + Licence.NumVehicule + "', '" + Licence.DatePremiereMiseCiculation.ToString("yyyy-MM-dd") + "'";
                this.strCommande += " ,'" + Licence.DatePremiereExploitation.ToString("yyyy-MM-dd") + "','" + Licence.ValideAu.ToString("yyyy-MM-dd") + "'";
                this.strCommande += " ,'" + Licence.ValideDu.ToString("yyyy-MM-dd") + "','" + Licence.NombrePlacePayante + "'";
                this.strCommande += " ,'" + Licence.IsProvisoire + "')";

                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    numLicence = Licence.NumLicence;
                this.serviceConnectBase.closeConnection();

            }
            #endregion

            return numLicence;
        }

        bool IntfDalLicence.deleteLicence(crlLicence Licence)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (Licence != null)
            {
                if (Licence.NumLicence != "")
                {
                    this.strCommande = "DELETE FROM `licence` WHERE (`numLicence` = '" + Licence.NumLicence + "')";
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

        bool IntfDalLicence.deleteLicence(string numLicence)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            
            if (numLicence != "")
            {
                this.strCommande = "DELETE FROM `licence` WHERE (`numLicence` = '" + numLicence + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete == 1)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }
            
            #endregion

            return isDelete;
        }

        bool IntfDalLicence.updateLicence(crlLicence Licence)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            #endregion

            #region implementation
            if (Licence != null)
            {
                if (Licence.NumLicence != "")
                {
                    this.strCommande = "UPDATE `licence` SET `numerosLicence`='" + Licence.NumerosLicence + "'";
                    this.strCommande += ",`zone`='" + Licence.Zone + "',`numCooperative`='" + Licence.NumCooperative + "'";
                    this.strCommande += ",`numVehicule`='" + Licence.NumVehicule + "',`datePremiereMiseCiculation`='" + Licence.DatePremiereMiseCiculation.ToString("yyyy-MM-dd") + "'";
                    this.strCommande += ",`datePremiereExploitation`='" + Licence.DatePremiereExploitation.ToString("yyyy-MM-dd") + "',`valideAu`='" + Licence.ValideAu.ToString("yyyy-MM-dd") + "'";
                    this.strCommande += ",`valideDu`='" + Licence.ValideDu.ToString("yyyy-MM-dd") + "',`nombrePlacePayante`='" + Licence .NombrePlacePayante + "'";
                    this.strCommande += ",`isProvisoire`='" + Licence.IsProvisoire + "' WHERE (`numLicence`='" + Licence.NumLicence + "')";

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

        string IntfDalLicence.insertLicenceUS(crlLicence Licence, string sigleAgence)
        {
            #region declaration
            int nombreInsertion = 0;
            string numLicence = "";
            string numCooperative = "NULL";
            IntfDalLicence serviceLicence = new ImplDalLicence();
            #endregion

            #region implementation
            if (Licence != null)
            {
                if (Licence.NumCooperative != "")
                {
                    numCooperative = "'" + Licence.NumCooperative + "'";
                }
                Licence.NumLicence = serviceLicence.getNumLicence(sigleAgence);

                this.strCommande = "INSERT INTO `licence` (`numLicence`,`numerosLicence`,`zone`,`numCooperative`,";
                this.strCommande += " `numVehicule`,`nombrePlacePayante`)";
                this.strCommande += " VALUES ('" + Licence.NumLicence + "','" + Licence.NumerosLicence + "'";
                this.strCommande += " ,'" + Licence.Zone + "'," + numCooperative;
                this.strCommande += " ,'" + Licence.NumVehicule + "','" + Licence.NombrePlacePayante + "')";

                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    numLicence = Licence.NumLicence;
                this.serviceConnectBase.closeConnection();

            }
            #endregion

            return numLicence;
        }

        bool IntfDalLicence.updateLicenceUS(crlLicence Licence)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            string numCooperative = "NULL";
            #endregion

            #region implementation
            if (Licence != null)
            {
                if (Licence.NumLicence != "")
                {
                    if (Licence.NumCooperative != "")
                    {
                        numCooperative = "'" + Licence.NumCooperative + "'";
                    }

                    this.strCommande = "UPDATE `licence` SET `numerosLicence`='" + Licence.NumerosLicence + "'";
                    this.strCommande += ",`zone`='" + Licence.Zone + "',`numCooperative`=" + numCooperative;
                    this.strCommande += ",`numVehicule`='" + Licence.NumVehicule + "',`nombrePlacePayante`='" + Licence.NombrePlacePayante + "'";
                    this.strCommande += " WHERE (`numLicence`='" + Licence.NumLicence + "')";

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

        bool IntfDalLicence.insertAssoItineraireLicence(string numLicence, string idItineraire)
        {
            #region declaration
            bool isInsert = false;
            int nbInsert = 0;
            #endregion

            #region implementation
            if (numLicence != "" && idItineraire != "")
            {
                this.strCommande = "INSERT INTO `assoitinerairelicence` (`numLicence`,`idItineraire`)";
                this.strCommande += " VALUES ('" + numLicence + "','" + idItineraire + "')";

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

        bool IntfDalLicence.deleteAssoItineraireLicence(string numLicence, string idItineraire)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (idItineraire != "" && numLicence != "")
            {
                this.strCommande = "DELETE FROM `assoitinerairelicence` WHERE (`idItineraire` = '" + idItineraire + "' AND";
                this.strCommande += " `numLicence`='" + numLicence + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete == 1)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isDelete;
        }

        int IntfDalLicence.isLicence(crlLicence Licence)
        {
            #region declaration
            int isLicence = 0;
            #endregion

            #region implementation
            if (Licence != null)
            {
                this.strCommande = "SELECT * FROM `licence` WHERE (`numLicence`<>'" + Licence.NumLicence + "')";
                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (Licence.NumerosLicence.Trim().ToLower().Equals(reader["numerosLicence"].ToString().Trim().ToLower()))
                            {
                                isLicence = 1;
                                break;
                            }
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isLicence;
        }

        crlLicence IntfDalLicence.selectLicence(string numLicence)
        {
            #region declaration
            IntfDalLicence serviceLicence = new ImplDalLicence();

            List<crlItineraire> Itineraires = null;
            crlLicence Licence = null;

            IntfDalCooperative serviceCooperative = new ImplDalCooperative();
            IntfDalZone serviceZone = new ImplDalZone();
            IntfDalVehicule serviceVehicule = new ImplDalVehicule();
            #endregion

            #region implementation
            if (numLicence != "") 
            {
                Itineraires = serviceLicence.selectItineraire(numLicence);
                this.strCommande = "SELECT * FROM licence WHERE (licence.numLicence = '" + numLicence + "')";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null) 
                {
                    if (reader.HasRows) 
                    {
                        if (reader.Read()) 
                        {
                            Licence = new crlLicence();

                            Licence.NumLicence = reader["numLicence"].ToString();
                            Licence.NumerosLicence = reader["numerosLicence"].ToString();
                            Licence.Zone = reader["zone"].ToString();
                            Licence.NumCooperative = reader["numCooperative"].ToString();
                            Licence.NumVehicule = reader["numVehicule"].ToString();
                            try
                            {
                                Licence.DatePremiereMiseCiculation = Convert.ToDateTime(reader["datePremiereMiseCiculation"].ToString());
                            }
                            catch (Exception) { }
                            try
                            {
                                Licence.DatePremiereExploitation = Convert.ToDateTime(reader["datePremiereExploitation"].ToString());
                            }
                            catch (Exception) { }

                            try
                            {
                                Licence.ValideAu = Convert.ToDateTime(reader["valideAu"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                Licence.ValideDu = Convert.ToDateTime(reader["valideDu"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                Licence.NombrePlacePayante = int.Parse(reader["nombrePlacePayante"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                Licence.IsProvisoire = int.Parse(reader["isProvisoire"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (Licence != null)
                {
                    if (Licence.Zone != "")
                    {
                        Licence.zoneObj = serviceZone.selectZone(Licence.Zone);
                    }
                    if (Licence.NumCooperative != "")
                    {
                        Licence.cooperative = serviceCooperative.selectCooperative(Licence.NumCooperative);
                    }
                    if (Licence.NumVehicule != "")
                    {
                        Licence.vehicule = serviceVehicule.selectVehicule(Licence.NumVehicule);
                    }
                    Licence.itineraires = serviceLicence.selectItineraire(Licence.NumLicence);
                }
            }
            #endregion

            return Licence;
        }

        void IntfDalLicence.loadDdlTri(DropDownList ddlTri)
        {
            ddlTri.Items.Clear();
            ddlTri.Items.Add(new ListItem("Numeros", "numLicence"));
            ddlTri.Items.Add(new ListItem("Nom", "nom"));
            ddlTri.Items.Add(new ListItem("Prénom", "prenom"));
            ddlTri.Items.Add(new ListItem("Société", "nomSociete"));
            ddlTri.Items.Add(new ListItem("Date expiration", "dateValideAu"));
        }

        List<crlItineraire> IntfDalLicence.selectItineraire(string numLicence)
        {
            #region declaration
            List<crlItineraire> Itineraires = null;
            crlItineraire ItineraireTemp = null;
            IntfDalVille serviceVille = new ImplDalVille(); 
            #endregion

            #region implementation
            if(numLicence != "")
            {
                this.strCommande = "SELECT itineraire.idItineraire, itineraire.distanceParcour, itineraire.nombreRepos,";
                this.strCommande += " itineraire.dureeTrajet, itineraire.numVilleItineraireDebut, itineraire.numVilleItineraireFin,";
                this.strCommande += " itineraire.numInfoBagage FROM itineraire";
                this.strCommande += " Inner Join assoitinerairelicence ON assoitinerairelicence.idItineraire = itineraire.idItineraire";
                this.strCommande += " WHERE assoitinerairelicence.numLicence = '" + numLicence + "'";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null) 
                {
                    if (reader.HasRows) 
                    {
                        Itineraires = new List<crlItineraire>();
                        while (reader.Read()) 
                        {
                            ItineraireTemp = new crlItineraire();
                            ItineraireTemp.DistanceParcour = int.Parse(reader["distanceParcour"].ToString());
                            ItineraireTemp.DureeTrajet = reader["dureeTrajet"].ToString();
                            ItineraireTemp.IdItineraire = reader["idItineraire"].ToString();
                            ItineraireTemp.NumVilleItineraireDebut = reader["numVilleItineraireDebut"].ToString();
                            ItineraireTemp.NumVilleItineraireFin = reader["numVilleItineraireFin"].ToString();

                            Itineraires.Add(ItineraireTemp);
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                if (Itineraires != null)
                {
                    for (int i = 0; i < Itineraires.Count; i++)
                    {
                        if (Itineraires[i].NumVilleItineraireDebut != "")
                        {
                            Itineraires[i].villeD = serviceVille.selectVille(Itineraires[i].NumVilleItineraireDebut);
                        }
                        if (Itineraires[i].NumVilleItineraireFin != "")
                        {
                            Itineraires[i].villeF = serviceVille.selectVille(Itineraires[i].NumVilleItineraireFin);
                        }
                        if (Itineraires[i].IdItineraire != "")
                        {
                            Itineraires[i].villes = serviceVille.selectVillesForItineraire(Itineraires[i].IdItineraire);
                        }
                    }
                }
            }
            #endregion

            return Itineraires;
        }

        void IntfDalLicence.loadDdlItineraire(DropDownList ddlItineraire, List<crlItineraire> Itineraires)
        {
            #region implementation
            ddlItineraire.Items.Clear();

            if (Itineraires != null)
            {
                if (Itineraires.Count > 0)
                {
                    for (int i = 0; i < Itineraires.Count; i++)
                    {
                        ddlItineraire.Items.Add(new ListItem(Itineraires[i].villeD.NomVille + "-" + Itineraires[i].villeF.NomVille, Itineraires[i].IdItineraire));
                    }
                }
            }
            #endregion
        }

        string IntfDalLicence.getNumLicence(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numLicence = "00001";
            string[] tempNumLicence = null;
            string strDate = "LI" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT licence.numLicence AS maxNum FROM licence";
            this.strCommande += " WHERE licence.numLicence LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumLicence = reader["maxNum"].ToString().ToString().Split('/');
                        numLicence = tempNumLicence[tempNumLicence.Length - 1];
                    }
                    numTemp = double.Parse(numLicence) + 1;
                    if (numTemp < 10)
                        numLicence = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numLicence = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numLicence = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numLicence = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numLicence = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numLicence = strDate + "/" + sigleAgence + "/" + numLicence;
            #endregion

            return numLicence;
        }
        #endregion

        #region insert to grid
        void IntfDalLicence.insertToGridLicence(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalLicence serviceLicence = new ImplDalLicence();
            #endregion

            #region implementation

            this.strCommande = "SELECT licence.zone, licence.numerosLicence, licence.valideAu, vehicule.matriculeVehicule,";
            this.strCommande += " licence.numLicence, licence.nombrePlacePayante FROM licence";
            this.strCommande += " Inner Join vehicule ON vehicule.numVehicule = licence.numVehicule";
            this.strCommande += " WHERE (" + paramLike + " LIKE  '%" + valueLike + "%')";
            this.strCommande += " ORDER BY " + param + " ASC";


            gridView.DataSource = serviceLicence.getDataTableLicence(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalLicence.getDataTableLicence(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalVille serviceVille = new ImplDalVille();
            IntfDalLicence serviceLicence = new ImplDalLicence();
            crlLicence licence = null;
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numLicence", typeof(string));
            dataTable.Columns.Add("matriculeVehicule", typeof(string));
            dataTable.Columns.Add("zone", typeof(string));
            dataTable.Columns.Add("valideAu", typeof(DateTime));
            dataTable.Columns.Add("nombrePlacePayante", typeof(string));
            dataTable.Columns.Add("itineraire1", typeof(string));
            dataTable.Columns.Add("itineraire2", typeof(string));
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
                        licence = serviceLicence.selectLicence(reader["numLicence"].ToString());
                        dr["numLicence"] = reader["numLicence"].ToString();
                        dr["matriculeVehicule"] = reader["matriculeVehicule"].ToString();
                        dr["zone"] = reader["zone"].ToString();
                        try
                        {
                            dr["valideAu"] = Convert.ToDateTime(reader["valideAu"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        dr["nombrePlacePayante"] = reader["nombrePlacePayante"].ToString();

                        if (licence != null)
                        {
                            if (licence.itineraires != null)
                            {
                                if (licence.itineraires.Count > 0)
                                {
                                    if (licence.itineraires != null)
                                    {
                                        dr["itineraire1"] = licence.itineraires[0].villeD.NomVille + "-" + licence.itineraires[0].villeF.NomVille;
                                    }
                                    else
                                    {
                                        dr["itineraire1"] = "";
                                    }
                                }
                                else
                                {
                                    dr["itineraire1"] = "";
                                }

                                if (licence.itineraires.Count > 1)
                                {
                                    if (licence.itineraires != null)
                                    {
                                        dr["itineraire2"] = licence.itineraires[1].villeD.NomVille + "-" + licence.itineraires[1].villeF.NomVille;
                                    }
                                    else
                                    {
                                        dr["itineraire2"] = "";
                                    }
                                }
                                else
                                {
                                    dr["itineraire2"] = "";
                                }
                            }
                            else
                            {
                                dr["itineraire1"] = "";
                                dr["itineraire2"] = "";
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

        void IntfDalLicence.insertToGridLicenceUS(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalLicence serviceLicence = new ImplDalLicence();
            #endregion

            #region implementation

            this.strCommande = "SELECT vehicule.numVehicule, vehicule.numParamVehicule,";
            this.strCommande += " vehicule.sourceEnergie, vehicule.numProprietaire, vehicule.matriculeVehicule,";
            this.strCommande += " vehicule.marqueVehicule, vehicule.typeVehicule, vehicule.numSerieVehicule,";
            this.strCommande += " vehicule.numMoteurVehicule, vehicule.puissanceVehicule,";
            this.strCommande += " vehicule.couleurVehicule, vehicule.placesAssiseVehicule,";
            this.strCommande += " vehicule.nombreColoneVehicule, vehicule.poidsTotalVehicule,";
            this.strCommande += " vehicule.poidsVideVehicule, vehicule.imageVehicule, licence.numLicence,";
            this.strCommande += " licence.numerosLicence, licence.zone FROM licence";
            this.strCommande += " Inner Join vehicule ON vehicule.numVehicule = licence.numVehicule";
            this.strCommande += " WHERE (" + paramLike + " LIKE  '%" + valueLike + "%') AND";
            this.strCommande += " (licence.zone = 'Urbaine' OR licence.zone = 'Suburbaine')";
            this.strCommande += " ORDER BY " + param + " ASC";


            gridView.DataSource = serviceLicence.getDataTableLicenceUS(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalLicence.getDataTableLicenceUS(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalVille serviceVille = new ImplDalVille();
            IntfDalLicence serviceLicence = new ImplDalLicence();
            crlLicence licence = null;
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numLicence", typeof(string));
            dataTable.Columns.Add("vehicule", typeof(string));
            dataTable.Columns.Add("zone", typeof(string));
            dataTable.Columns.Add("nbPlace", typeof(string));
            dataTable.Columns.Add("numerosLicence", typeof(string));
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
                        
                        dr["numLicence"] = reader["numLicence"].ToString();
                        dr["vehicule"] = reader["matriculeVehicule"].ToString() + " " + this.reader["couleurVehicule"].ToString() + " " + this.reader["marqueVehicule"].ToString();
                        dr["zone"] = this.reader["zone"].ToString();
                        dr["nbPlace"] = reader["placesAssiseVehicule"].ToString();
                        dr["numerosLicence"] = reader["numerosLicence"].ToString();


                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }

        void IntfDalLicence.insertToGridLicenceProprietaire(GridView gridView, string param, string paramLike, string valueLike, string numProprietaire)
        {
            #region declaration
            IntfDalLicence serviceLicence = new ImplDalLicence();
            #endregion

            #region implementation

            this.strCommande = "SELECT licence.numLicence, licence.numerosLicence, licence.zone, licence.numCooperative,";
            this.strCommande += " licence.numVehicule, licence.datePremiereMiseCiculation, licence.datePremiereExploitation,";
            this.strCommande += " licence.valideAu, licence.valideDu, licence.nombrePlacePayante, licence.isProvisoire,";
            this.strCommande += " vehicule.numVehicule, vehicule.numParamVehicule, vehicule.sourceEnergie,";
            this.strCommande += " vehicule.numProprietaire, vehicule.matriculeVehicule, vehicule.marqueVehicule,";
            this.strCommande += " vehicule.typeVehicule, vehicule.numSerieVehicule, vehicule.numMoteurVehicule,";
            this.strCommande += " vehicule.puissanceVehicule, vehicule.couleurVehicule, vehicule.placesAssiseVehicule,";
            this.strCommande += " vehicule.nombreColoneVehicule, vehicule.poidsTotalVehicule, vehicule.poidsVideVehicule,";
            this.strCommande += " vehicule.imageVehicule FROM licence";
            this.strCommande += " Inner Join vehicule ON vehicule.numVehicule = licence.numVehicule";
            this.strCommande += " WHERE vehicule.numProprietaire = '" + numProprietaire + "' AND";
            this.strCommande += " (" + paramLike + " LIKE  '%" + valueLike + "%')";
            this.strCommande += " ORDER BY " + param + " ASC";


            gridView.DataSource = serviceLicence.getDataTableLicenceProprietaire(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalLicence.getDataTableLicenceProprietaire(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalVille serviceVille = new ImplDalVille();


            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numLicence", typeof(string));
            dataTable.Columns.Add("matriculeVehicule", typeof(string));
            dataTable.Columns.Add("marqueVehicule", typeof(string));
            dataTable.Columns.Add("couleurVehicule", typeof(string));
            dataTable.Columns.Add("zone", typeof(string));
            dataTable.Columns.Add("valideAu", typeof(DateTime));
            dataTable.Columns.Add("nombrePlacePayante", typeof(string));
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

                        dr["numLicence"] = reader["numLicence"].ToString();
                        dr["matriculeVehicule"] = reader["matriculeVehicule"].ToString();
                        dr["marqueVehicule"] = reader["marqueVehicule"].ToString();
                        dr["couleurVehicule"] = reader["couleurVehicule"].ToString();
                        dr["zone"] = reader["zone"].ToString();
                        dr["valideAu"] = Convert.ToDateTime(reader["valideAu"].ToString());
                        dr["nombrePlacePayante"] = reader["nombrePlacePayante"].ToString();

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }

        void IntfDalLicence.insertToGridLicenceUSAvecPropriete(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalLicence serviceLicence = new ImplDalLicence();
            #endregion

            #region implementation

            this.strCommande = "SELECT licence.numerosLicence, licence.numLicence, cooperative.nomCooperative,";
            this.strCommande += " vehicule.matriculeVehicule, vehicule.marqueVehicule, vehicule.couleurVehicule, Individu.adresse As adresseInd,";
            this.strCommande += " Individu.nomIndividu, Individu.prenomIndividu, organisme.nomResponsable AS nomRespOrg,";
            this.strCommande += " Individu.telephoneFixeIndividu, Individu.telephoneMobileIndividu, organisme.adresseOrganisme,";
            this.strCommande += " organisme.telephoneFixeOrganisme, organisme.telephoneMobileOrganisme,";
            this.strCommande += " organisme.telephoneFixeResponsable, organisme.telephoneMobileResponsable,";
            this.strCommande += " societe.adresseSociete, societe.telephoneFixeSociete, societe.telephoneMobileSociete,";
            this.strCommande += " societe.telephoneFixeResponsable, societe.telephoneMobileResponsable,";
            this.strCommande += " organisme.prenomResponsable AS prenomRespOrg, organisme.nomOrganisme, societe.nomSociete,";
            this.strCommande += " societe.nomResponsable As nomRespSoc, societe.prenomResponsable As prenomRespSoc FROM licence";
            this.strCommande += " Left Join cooperative ON cooperative.numCooperative = licence.numCooperative";
            this.strCommande += " Inner Join vehicule ON vehicule.numVehicule = licence.numVehicule";
            this.strCommande += " Inner Join proprietaire ON proprietaire.numProprietaire = vehicule.numProprietaire";
            this.strCommande += " Left Join Individu ON Individu.numIndividu = proprietaire.numIndividu";
            this.strCommande += " Left Join organisme ON organisme.numOrganisme = proprietaire.numOrganisme";
            this.strCommande += " Left Join societe ON societe.numSociete = proprietaire.numSociete";
            this.strCommande += " WHERE (" + paramLike + " LIKE  '%" + valueLike + "%') AND";
            this.strCommande += " (licence.zone = 'Urbaine' OR licence.zone = 'Suburbaine')";
            this.strCommande += " ORDER BY " + param + " ASC";


            gridView.DataSource = serviceLicence.getDataTableLicenceUSAvecPropriete(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalLicence.getDataTableLicenceUSAvecPropriete(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalVille serviceVille = new ImplDalVille();
            IntfDalLicence serviceLicence = new ImplDalLicence();
            crlLicence licence = null;
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numLicence", typeof(string));
            dataTable.Columns.Add("vehicule", typeof(string));
            dataTable.Columns.Add("cooperative", typeof(string));
            dataTable.Columns.Add("individu", typeof(string));
            dataTable.Columns.Add("adresseIndividu", typeof(string));
            dataTable.Columns.Add("contactIndividu", typeof(string));
            dataTable.Columns.Add("responsable", typeof(string));
            dataTable.Columns.Add("contactResponsable", typeof(string));
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

                        dr["numLicence"] = reader["numLicence"].ToString();
                        dr["vehicule"] = reader["matriculeVehicule"].ToString() + " " + this.reader["couleurVehicule"].ToString() + " " + this.reader["marqueVehicule"].ToString();
                        dr["cooperative"] = this.reader["nomCooperative"].ToString();
                        dr["individu"] = reader["nomIndividu"].ToString() + " " + reader["prenomIndividu"].ToString() + reader["nomSociete"].ToString() + reader["nomOrganisme"].ToString();
                        dr["adresseIndividu"] = reader["adresseInd"].ToString();
                        dr["contactIndividu"] = reader["telephoneFixeIndividu"].ToString() + " " + reader["telephoneMobileIndividu"].ToString() + reader["telephoneFixeSociete"].ToString() + " " + reader["telephoneMobileSociete"].ToString() + reader["telephoneFixeOrganisme"].ToString() + " " + reader["telephoneMobileOrganisme"].ToString();
                        dr["responsable"] = reader["nomRespSoc"].ToString() + " " + reader["prenomRespSoc"].ToString() + reader["nomRespOrg"].ToString() + " " + reader["prenomRespOrg"].ToString();
                        dr["contactResponsable"] = reader["telephoneFixeResponsable"].ToString() + " " + reader["telephoneMobileResponsable"].ToString() + reader["telephoneFixeResponsable"].ToString() + " " + reader["telephoneMobileResponsable"].ToString();

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
