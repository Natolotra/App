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
    /// Implementation du service facture
    /// </summary>
    public class ImplDalFacture : IntfDalFacture
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalFacture(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalFacture() 
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion

        #region IntfDalFacture Members

        string IntfDalFacture.insertFacture(crlFacture Facture)
        {
            #region declaration
            IntfDalFacture serviceFacture = new ImplDalFacture();
            int nombreInsertion = 0;
            string numFacture = "";
            #endregion

            #region implementation
            if (Facture != null) 
            {
                Facture.NumFacture = serviceFacture.getNumFacture(Facture.agent.agence.SigleAgence);
                this.strCommande = "INSERT INTO `facture` (`numFacture`,`libele`,`montant`,`dateFacturation`,`matriculeAgent`)";
                this.strCommande += " VALUES ('" + Facture.NumFacture + "', '" + Facture.Libele + "', ";
                this.strCommande += " '" + Facture.Montant + "', '" + Facture.DateFacturation.ToString("yyyy-MM-dd") + "','" + Facture.MatriculeAgent + "')";

                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    numFacture = Facture.NumFacture;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numFacture;
        }

        string IntfDalFacture.insertFactureAssoc(crlFacture Facture)
        {
            #region declaration
            string numFacture = "";
            IntfDalFacture serviceFacture = new ImplDalFacture();
            IntfDalAutorisationDepart serviceAutorisationDepart = new ImplDalAutorisationDepart();
            #endregion

            #region implementation
            if (Facture != null)
            {
                if (Facture.autorisationDeparts != null)
                {
                    Facture.NumFacture = serviceFacture.insertFacture(Facture);
                    if (Facture.NumFacture != "")
                    {
                        for (int i = 0; i < Facture.autorisationDeparts.Count; i++)
                        {
                            serviceFacture.insertAssocFactureAD(Facture.NumFacture, Facture.autorisationDeparts[i].NumAutorisationDepart);
                            Facture.autorisationDeparts[i].ResteRegle = 0;
                            serviceAutorisationDepart.updateAutorisationDepart(Facture.autorisationDeparts[i]);
                        }
                        numFacture = Facture.NumFacture;
                    }
                }
            }
            #endregion

            return numFacture;
        }

        bool IntfDalFacture.insertAssocFactureAD(string numFacture, string numAutorisationDepart)
        {
            #region declaration
            bool isInsert = false;
            int nbInsert = 0;
            #endregion

            #region implementation
            if (numAutorisationDepart != "" && numFacture != "")
            {
                this.strCommande = "INSERT INTO `assocautorisationdepartfacture` (`numFacture`,`numAutorisationDepart`)";
                this.strCommande += " VALUES ('" + numFacture + "','" + numAutorisationDepart + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                    isInsert = true;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isInsert;
        }

        bool IntfDalFacture.deleteFacture(crlFacture Facture)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (Facture != null)
            {
                if (Facture.NumFacture != "")
                {
                    this.strCommande = "DELETE FROM `facture` WHERE (`numFacture` = '" + Facture.NumFacture + "')";
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

        bool IntfDalFacture.deleteFacture(string numFacture)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            
            if (numFacture != "")
            {
                this.strCommande = "DELETE FROM `facture` WHERE (`numFacture` = '" + numFacture + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete == 1)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }
           
            #endregion

            return isDelete;
        }

        bool IntfDalFacture.updateFacture(crlFacture Facture)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            #endregion

            #region implementation
            if (Facture != null)
            {
                if (Facture.NumFacture != "")
                {
                    this.strCommande = "UPDATE `facture` SET `libele`='" + Facture.Libele + "', ";
                    this.strCommande += "`montant`='" + Facture.Montant + "', `dateFacturation`='" + Facture.DateFacturation.ToString("yyyy-MM-dd") + "'";
                    this.strCommande += ",`matriculeAgent`='" + Facture.MatriculeAgent + "' WHERE (`numFacture`='" + Facture.NumFacture + "')";

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

        crlFacture IntfDalFacture.selectFacture(string numFacture)
        {
            #region declaration
            crlFacture Facture = null;

            IntfDalAgent serviceAgent = new ImplDalAgent();
            IntfDalFacture serviceFacture = new ImplDalFacture();
            #endregion

            #region implementation
            if (numFacture != "")
            {
                this.strCommande = "SELECT * FROM `facture` WHERE (`numFacture`='" + numFacture + "')";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null) 
                {
                    if (reader.HasRows) 
                    {
                        Facture = new crlFacture();
                        reader.Read();
                        Facture.NumFacture = reader["numFacture"].ToString();
                        Facture.Libele = reader["libele"].ToString();
                        Facture.Montant = reader["montant"].ToString();
                        Facture.MatriculeAgent = reader["matriculeAgent"].ToString();
                        try
                        {
                            Facture.DateFacturation = Convert.ToDateTime(reader["dateFacturation"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (Facture != null)
                {
                    if (Facture.MatriculeAgent != "")
                    {
                        Facture.agent = serviceAgent.selectAgent(Facture.MatriculeAgent);
                    }

                    Facture.autorisationDeparts = serviceFacture.selectADForFacture(Facture.NumFacture);
                }
            }
            #endregion

            return Facture;
        }

        string IntfDalFacture.getNumFacture(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numFacture = "00001";
            string[] tempNumFacture = null;
            string strDate = "FA" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT facture.numFacture AS maxNum FROM facture";
            this.strCommande += " WHERE facture.numFacture LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumFacture = reader["maxNum"].ToString().ToString().Split('/');
                        numFacture = tempNumFacture[tempNumFacture.Length - 1];
                    }
                    numTemp = double.Parse(numFacture) + 1;
                    if (numTemp < 10)
                        numFacture = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numFacture = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numFacture = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numFacture = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numFacture = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numFacture = strDate + "/" + sigleAgence + "/" + numFacture;
            #endregion

            return numFacture;
        }

        void IntfDalFacture.loadDdlTri(DropDownList ddlTri)
        {
            throw new NotImplementedException();
        }

        double IntfDalFacture.getTotalPrixBillet(string numFacture)
        {
            #region declaration
            double prixTotal = 0.00;
            #endregion

            #region implementation
            if (numFacture != "")
            {
                this.strCommande = "SELECT Sum(billet.prixBillet) AS prixTotal FROM billet";
                this.strCommande += " Inner Join voyage ON voyage.numBillet = billet.numBillet";
                this.strCommande += " Inner Join fichebord ON fichebord.numerosFB = voyage.numerosFB";
                this.strCommande += " Inner Join autorisationdepart ON autorisationdepart.numerosFB = fichebord.numerosFB";
                this.strCommande += " Inner Join assocautorisationdepartfacture ON assocautorisationdepartfacture.numAutorisationDepart = autorisationdepart.numAutorisationDepart";
                this.strCommande += " WHERE assocautorisationdepartfacture.numFacture = '" + numFacture + "'";

                this.serviceConnectBase.openConnection();
                this.reader = serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            try
                            {
                                prixTotal = double.Parse(this.reader["prixTotal"].ToString());
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

            return prixTotal;
        }

        double IntfDalFacture.getTotalPrixBagage(string numFacture)
        {
            #region declaration
            double prixTotal = 0.00;
            #endregion

            #region implementation
            if (numFacture != "")
            {
                this.strCommande = "SELECT Sum(recu.montant) AS prixTotal FROM recu";
                this.strCommande += " Inner Join bagage ON bagage.numRecu = recu.numRecu";
                this.strCommande += " Inner Join associationvoyagebagage ON associationvoyagebagage.idBagage = bagage.idBagage";
                this.strCommande += " Inner Join voyage ON voyage.idVoyage = associationvoyagebagage.idVoyage";
                this.strCommande += " Inner Join fichebord ON fichebord.numerosFB = voyage.numerosFB";
                this.strCommande += " Inner Join autorisationdepart ON autorisationdepart.numerosFB = fichebord.numerosFB";
                this.strCommande += " Inner Join assocautorisationdepartfacture ON assocautorisationdepartfacture.numAutorisationDepart = autorisationdepart.numAutorisationDepart";
                this.strCommande += " WHERE assocautorisationdepartfacture.numFacture = '" + numFacture + "'";

                this.serviceConnectBase.openConnection();
                this.reader = serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            try
                            {
                                prixTotal = double.Parse(this.reader["prixTotal"].ToString());
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

            return prixTotal;
        }

        double IntfDalFacture.getTotalPrixCommission(string numFacture)
        {
            #region declaration
            double prixTotal = 0.00;
            #endregion

            #region implementation
            if (numFacture != "")
            {
                this.strCommande = "SELECT Sum(recu.montant) AS prixTotal FROM recu";
                this.strCommande += " Inner Join commission ON commission.numRecu = recu.numRecu";
                this.strCommande += " Inner Join associationfichebordcommission ON associationfichebordcommission.idCommission = commission.idCommission";
                this.strCommande += " Inner Join fichebord ON fichebord.numerosFB = associationfichebordcommission.numerosFB";
                this.strCommande += " Inner Join autorisationdepart ON autorisationdepart.numerosFB = fichebord.numerosFB";
                this.strCommande += " Inner Join assocautorisationdepartfacture ON assocautorisationdepartfacture.numAutorisationDepart = autorisationdepart.numAutorisationDepart";
                this.strCommande += " WHERE assocautorisationdepartfacture.numFacture = '" + numFacture + "'";

                this.serviceConnectBase.openConnection();
                this.reader = serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            try
                            {
                                prixTotal = double.Parse(this.reader["prixTotal"].ToString());
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

            return prixTotal;
        }

        double IntfDalFacture.getTotalMontantRecu(string numFacture)
        {
            #region declaration
            double prixTotal = 0.00;
            #endregion

            #region implementation
            if (numFacture != "")
            {
                this.strCommande = "SELECT Sum(recuad.montant) AS prixTotal FROM recuad";
                this.strCommande += " Inner Join asociationautorisationdepartrecu ON asociationautorisationdepartrecu.numRecuAD = recuad.numRecuAD";
                this.strCommande += " Inner Join autorisationdepart ON autorisationdepart.numAutorisationDepart = asociationautorisationdepartrecu.numAutorisationDepart";
                this.strCommande += " Inner Join assocautorisationdepartfacture ON assocautorisationdepartfacture.numAutorisationDepart = autorisationdepart.numAutorisationDepart";
                this.strCommande += " WHERE assocautorisationdepartfacture.numFacture = '" + numFacture + "'";

                this.serviceConnectBase.openConnection();
                this.reader = serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            try
                            {
                                prixTotal = double.Parse(this.reader["prixTotal"].ToString());
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

            return prixTotal;
        }

        double IntfDalFacture.getMontantRecette(string numFacture)
        {
            #region declaration
            double prixTotal = 0.00;
            #endregion

            #region implementation
            if (numFacture != "")
            {
                this.strCommande = "SELECT Sum(autorisationdepart.recetteTotale) AS prixTotal FROM autorisationdepart";
                this.strCommande += " Inner Join assocautorisationdepartfacture ON assocautorisationdepartfacture.numAutorisationDepart = autorisationdepart.numAutorisationDepart";
                this.strCommande += " WHERE assocautorisationdepartfacture.numFacture = '" + numFacture + "'";

                this.serviceConnectBase.openConnection();
                this.reader = serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            try
                            {
                                prixTotal = double.Parse(this.reader["prixTotal"].ToString());
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

            return prixTotal;
        }

        double IntfDalFacture.getMontantRecu(string numFacture)
        {
            #region declaration
            double prixTotal = 0.00;
            #endregion

            #region implementation
            if (numFacture != "")
            {
                this.strCommande = "SELECT Sum(recuad.montant) AS prixTotal FROM recuad";
                this.strCommande += " Inner Join prelevement ON prelevement.numPrelevement = recuad.numPrelevement";
                this.strCommande += " Inner Join autorisationdepart ON autorisationdepart.numAutorisationDepart = prelevement.numAutorisationDepart";
                this.strCommande += " Inner Join assocautorisationdepartfacture ON assocautorisationdepartfacture.numAutorisationDepart = autorisationdepart.numAutorisationDepart";
                this.strCommande += " WHERE assocautorisationdepartfacture.numFacture = '" + numFacture + "'";

                this.serviceConnectBase.openConnection();
                this.reader = serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            try
                            {
                                prixTotal = double.Parse(this.reader["prixTotal"].ToString());
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

            return prixTotal;
        }

        List<crlAutorisationDepart> IntfDalFacture.selectADForFacture(string numFacture)
        {
            #region declaration
            List<crlAutorisationDepart> autorisationDeparts = null;

            IntfDalAutorisationDepart serviceAutorisationDepart = new ImplDalAutorisationDepart();
            #endregion

            #region implementation
            if (numFacture != "")
            {
                this.strCommande = "SELECT autorisationdepart.numAutorisationDepart FROM autorisationdepart";
                this.strCommande += " Inner Join assocautorisationdepartfacture ON assocautorisationdepartfacture.numAutorisationDepart = autorisationdepart.numAutorisationDepart";
                this.strCommande += " WHERE assocautorisationdepartfacture.numFacture = '" + numFacture + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        autorisationDeparts = new List<crlAutorisationDepart>();
                        while (this.reader.Read())
                        {
                            autorisationDeparts.Add(serviceAutorisationDepart.selectAutorisationDepart(this.reader["numAutorisationDepart"].ToString()));
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return autorisationDeparts;
        }

        #endregion

        #region insert to grid
        void IntfDalFacture.insertToGridFactureNotRecu(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalFacture serviceFacture = new ImplDalFacture();
            #endregion

            #region implementation
            this.strCommande = "SELECT facture.numFacture, facture.libele, facture.montant, facture.dateFacturation, facture.matriculeAgent,";
            this.strCommande += " Individu.nomIndividu, Individu.prenomIndividu, societe.nomSociete, organisme.nomOrganisme, Individu.civiliteIndividu,";
            this.strCommande += " Individu.cinIndividu, Individu.adresse, organisme.adresseOrganisme, societe.adresseSociete FROM facture";
            this.strCommande += " Left Join recuad ON recuad.numFacture = facture.numFacture";
            this.strCommande += " Inner Join assocautorisationdepartfacture ON assocautorisationdepartfacture.numFacture = facture.numFacture";
            this.strCommande += " Inner Join autorisationdepart ON autorisationdepart.numAutorisationDepart = assocautorisationdepartfacture.numAutorisationDepart";
            this.strCommande += " Inner Join fichebord ON fichebord.numerosFB = autorisationdepart.numerosFB";
            this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.numerosAV = fichebord.numerosAV";
            this.strCommande += " Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
            this.strCommande += " Inner Join licence ON licence.numLicence = verification.numLicence";
            this.strCommande += " Inner Join vehicule ON vehicule.numVehicule = licence.numVehicule";
            this.strCommande += " Inner Join proprietaire ON proprietaire.numProprietaire = vehicule.numProprietaire";
            this.strCommande += " Left Join Individu ON Individu.numIndividu = proprietaire.numIndividu";
            this.strCommande += " Left Join societe ON societe.numSociete = proprietaire.numSociete";
            this.strCommande += " Left Join organisme ON organisme.numOrganisme = proprietaire.numOrganisme";
            this.strCommande += " WHERE recuad.numRecuAD IS NULL AND";
            this.strCommande += " " + paramLike + " LIKE  '%" + valueLike + "%'";
            this.strCommande += " GROUP BY facture.numFacture ORDER BY " + param + " ASC";

            gridView.DataSource = serviceFacture.getDataTableFactureNotRecu(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalFacture.getDataTableFactureNotRecu(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            //IntfDalVille serviceVille = new ImplDalVille();
            //IntfDalFicheBord serviceFicheBord = new ImplDalFicheBord();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();

            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numFacture", typeof(string));
            dataTable.Columns.Add("montant", typeof(string));
            dataTable.Columns.Add("dateFacturation", typeof(DateTime));
            dataTable.Columns.Add("Individu", typeof(string));
            dataTable.Columns.Add("societe", typeof(string));
            dataTable.Columns.Add("organisme", typeof(string));
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

                        dr["numFacture"] = reader["numFacture"].ToString();
                        dr["montant"] = serviceGeneral.separateurDesMilles(reader["montant"].ToString()) + "Ar";
                        try
                        {
                            dr["dateFacturation"] = Convert.ToDateTime(reader["dateFacturation"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        dr["Individu"] = reader["prenomIndividu"].ToString() + " " + reader["nomIndividu"].ToString() + " / " + reader["cinIndividu"] + " / " + reader["adresse"].ToString();

                        dr["societe"] = reader["nomSociete"] + " / " + reader["adresseSociete"].ToString();

                        dr["organisme"] = reader["nomOrganisme"].ToString() + " / " + reader["adresseOrganisme"].ToString();
                        

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