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
    /// Implementation du service voyage
    /// </summary>
    public class ImplDalVoyage : IntfDalVoyage
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalVoyage()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalVoyage(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region IntfDalVoyage Members

        string IntfDalVoyage.insertVoyage(crlVoyage Voyage, string sigleAgence)
        {
            #region declaration
            IntfDalVoyage serviceVoyage = new ImplDalVoyage();
            int nombreInsertion = 0;
            string idVoyage = "";
            #endregion

            #region implementation
            if (Voyage != null)
            {
                Voyage.IdVoyage = serviceVoyage.getIdVoyage(sigleAgence);
                this.strCommande = "INSERT INTO `voyage` (`idVoyage`,`numIndividu`,`numerosFB`,`poidBagage`";
                this.strCommande += ",`destination`,`numBillet`,`numPlace`,`pieceIdentite`)";

                this.strCommande += " VALUES ('" + Voyage.IdVoyage + "', '" + Voyage.NumIndividu + "', '" + Voyage.NumerosFB + "'";
                this.strCommande += ",'" + Voyage.PoidBagage + "','" + Voyage.Destination + "','" + Voyage.NumBillet + "'";
                this.strCommande += ",'" + Voyage.NumPlace + "','" + Voyage.PieceIdentite + "')";

                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    idVoyage = Voyage.IdVoyage;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return idVoyage;
        }

        bool IntfDalVoyage.deleteVoyage(crlVoyage Voyage)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (Voyage != null)
            {
                if (Voyage.IdVoyage!= "")
                {
                    this.strCommande = "DELETE FROM `voyage` WHERE (`idVoyage` = '" + Voyage.IdVoyage + "')";
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

        bool IntfDalVoyage.deleteVoyage(string idVoyage)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (idVoyage != "")
            {
                this.strCommande = "DELETE FROM `voyage` WHERE (`idVoyage` = '" + idVoyage + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete == 1)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }
            
            #endregion

            return isDelete;
        }

        bool IntfDalVoyage.deleteAllVoyage(string idVoyage)
        {
            #region declaration
            crlVoyage voyage = null;
            bool isDelete = false;

            IntfDalVoyage serviceVoyage = new ImplDalVoyage();
            IntfDalRecu serviceRecu = new ImplDalRecu();
            IntfDalBagage serviceBagage = new ImplDalBagage();
            #endregion

            #region implementation
            if (idVoyage != "")
            {
                voyage = serviceVoyage.selectVoyage(idVoyage);

                if (voyage != null)
                {
                    serviceBagage.deleteAssociationBagageVoyage(voyage.IdVoyage);

                    if (voyage.bagage != null)
                    {
                        serviceRecu.deleteRecu(voyage.bagage.NumRecu);
                        serviceBagage.deleteBagage(voyage.bagage.IdBagage);
                        
                    }

                   

                    isDelete = serviceVoyage.deleteVoyage(idVoyage);
                }
            }
            #endregion

            return isDelete;
        }

        bool IntfDalVoyage.updateVoyage(crlVoyage Voyage)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            #endregion

            #region implementation
            if (Voyage != null)
            {
                if (Voyage.IdVoyage != "")
                {
                    this.strCommande = "UPDATE `voyage` SET `numIndividu`='" + Voyage.NumIndividu + "', `numerosFB`='" + Voyage.NumerosFB + "',";
                    this.strCommande += "`poidBagage`='" + Voyage.PoidBagage + "', `destination`='" + Voyage.Destination + "', ";
                    this.strCommande += "`numBillet`='" + Voyage.NumBillet + "', `numPlace`='" + Voyage.NumPlace + "', ";
                    this.strCommande += "`pieceIdentite`='" + Voyage.PieceIdentite + "' ";
                    this.strCommande += "WHERE (`idVoyage`='" + Voyage.IdVoyage + "')";

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

        int IntfDalVoyage.isVoyage(crlVoyage Voyage)
        {
            throw new NotImplementedException();
        }

        crlVoyage IntfDalVoyage.selectVoyage(string idVoyage)
        {
            #region declaration
            crlVoyage Voyage = null;
            IntfDalBagage serviceBagage = new ImplDalBagage();
            IntfDalIndividu serviceIndividu = new ImplDalIndividu();
            IntfDalBillet serviceBillet = new ImplDalBillet();
            IntfDalPlaceFB servicePlaceFB = new ImplDalPlaceFB();
            #endregion

            #region implementation
            if (idVoyage != "") 
            {
                this.strCommande = "SELECT * FROM `voyage` WHERE (`idVoyage`='" + idVoyage + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            Voyage = new crlVoyage();
                            Voyage.IdVoyage = reader["idVoyage"].ToString();
                            Voyage.NumIndividu = reader["numIndividu"].ToString();
                            Voyage.NumerosFB = reader["numerosFB"].ToString();
                            try
                            {
                                Voyage.PoidBagage = double.Parse(reader["poidBagage"].ToString());
                            }
                            catch (Exception)
                            {}
                            Voyage.Destination = reader["destination"].ToString();
                            Voyage.NumBillet = reader["numBillet"].ToString();
                            Voyage.NumPlace = reader["numPlace"].ToString();
                            Voyage.PieceIdentite = reader["pieceIdentite"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (Voyage != null) 
                {
                   
                    Voyage.billet = serviceBillet.selectBillet(Voyage.NumBillet);
                    Voyage.individu = serviceIndividu.selectIndividu(Voyage.NumIndividu);
                    Voyage.placeFB = servicePlaceFB.selectPlaceFB(Voyage.NumerosFB, Voyage.NumPlace);
                    Voyage.bagage = serviceBagage.selectBagageForVoyage(Voyage.IdVoyage);
                }
            }
            #endregion

            return Voyage;
        }

        string IntfDalVoyage.getIdVoyage(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string idVoyage = "00001";
            string[] tempIdVoyage = null;
            string strDate = "VO" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT voyage.idVoyage AS maxNum FROM voyage";
            this.strCommande += " WHERE voyage.idVoyage LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempIdVoyage = reader["maxNum"].ToString().ToString().Split('/');
                        idVoyage = tempIdVoyage[tempIdVoyage.Length - 1];
                    }
                    numTemp = double.Parse(idVoyage) + 1;
                    if (numTemp < 10)
                        idVoyage = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        idVoyage = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        idVoyage = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        idVoyage = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        idVoyage = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            idVoyage = strDate + "/" + sigleAgence + "/" + idVoyage;
            #endregion

            return idVoyage;
        }

        void IntfDalVoyage.loadDdlTri(DropDownList ddlTri)
        {
            throw new NotImplementedException();
        }

        #endregion


        void IntfDalVoyage.insertToGridVoyageFB(GridView gridView, string param, string paramLike, string valueLike, string numerosFB)
        {
            #region declaration
            IntfDalVoyage serviceVoyage = new ImplDalVoyage();
            #endregion

            #region implementation

            this.strCommande = "SELECT individu.nomIndividu, individu.prenomIndividu, voyage.pieceIdentite, voyage.destination,";
            this.strCommande += " voyage.numPlace, voyage.poidBagage, voyage.idVoyage, billet.prixBillet, billet.numBillet FROM voyage";
            this.strCommande += " Inner Join individu ON individu.numIndividu = voyage.numIndividu";
            this.strCommande += " Inner Join billet ON billet.numBillet = voyage.numBillet";
            this.strCommande += " Inner Join trajet ON trajet.numTrajet = billet.numTrajet";
            this.strCommande += " WHERE voyage.numerosFB =  '" + numerosFB + "'  AND " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceVoyage.getDataTableVoyageFB(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalVoyage.getDataTableVoyageFB(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            #endregion

            #region implementation
            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("idVoyage",typeof(string));
            dataTable.Columns.Add("client", typeof(string));
            dataTable.Columns.Add("pieceIdentite", typeof(string));
            dataTable.Columns.Add("destination", typeof(string));
            dataTable.Columns.Add("numPlace", typeof(string));
            dataTable.Columns.Add("poidBagage", typeof(string));
            dataTable.Columns.Add("prixTrajet", typeof(string));
            dataTable.Columns.Add("numBillet", typeof(string));
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

                        dr["idVoyage"] = reader["idVoyage"].ToString();
                        dr["client"] = reader["nomIndividu"].ToString() + " " + reader["prenomIndividu"].ToString();
                        dr["pieceIdentite"] = reader["pieceIdentite"].ToString();
                        dr["destination"] = reader["destination"].ToString();
                        dr["numPlace"] = reader["numPlace"].ToString();
                        dr["poidBagage"] = reader["poidBagage"].ToString() + "Kg";
                        dr["prixTrajet"] = serviceGeneral.separateurDesMilles(reader["prixBillet"].ToString()) + "Ar";
                        dr["numBillet"] = reader["numBillet"].ToString();

                        dataTable.Rows.Add(dr);
                    }
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            #endregion

            return dataTable;
        }





        void IntfDalVoyage.insertToGrigVoyageAutorisationDepart(GridView gridView, string param, string paramLike, string valueLike, string numerosFB)
        {
            #region declaration
            IntfDalVoyage serviceVoyage = new ImplDalVoyage();
            #endregion

            #region implementation

            this.strCommande = "SELECT individu.nomIndividu, individu.prenomIndividu, voyage.pieceIdentite,";
            this.strCommande += " voyage.destination, voyage.numPlace, voyage.poidBagage, billet.prixBillet,";
            this.strCommande += " billet.numBillet, bagage.excedentPoid, bagage.prixExcedent FROM voyage";
            this.strCommande += " Inner Join individu ON individu.numIndividu = voyage.numIndividu";
            this.strCommande += " Inner Join billet ON billet.numBillet = voyage.numBillet";
            this.strCommande += " Inner Join trajet ON trajet.numTrajet = billet.numTrajet";
            this.strCommande += " Left Join associationvoyagebagage ON associationvoyagebagage.idVoyage = voyage.idVoyage";
            this.strCommande += " Left Join bagage ON bagage.idBagage = associationvoyagebagage.idBagage";
            this.strCommande += " WHERE voyage.numerosFB =  '" + numerosFB + "'  AND " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceVoyage.getDataTableVoyageAutorisationDepart(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalVoyage.getDataTableVoyageAutorisationDepart(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            #endregion

            #region implementation
            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("passager", typeof(string));
            dataTable.Columns.Add("pieceIdentite", typeof(string));
            dataTable.Columns.Add("destination", typeof(string));
            dataTable.Columns.Add("numPlace", typeof(string));
            dataTable.Columns.Add("poidBagage", typeof(string));
            dataTable.Columns.Add("prixTrajet", typeof(string));
            dataTable.Columns.Add("numBillet", typeof(string));
            dataTable.Columns.Add("excedent", typeof(string));
            dataTable.Columns.Add("somme", typeof(string));
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

                        dr["passager"] = reader["nomIndividu"].ToString() + " " + reader["prenomIndividu"].ToString();
                        dr["pieceIdentite"] = reader["pieceIdentite"].ToString();
                        dr["destination"] = reader["destination"].ToString();
                        dr["numPlace"] = reader["numPlace"].ToString();
                        dr["poidBagage"] = reader["poidBagage"].ToString() + "Kg";
                        dr["prixTrajet"] =  serviceGeneral.separateurDesMilles(reader["prixBillet"].ToString()) + "Ar";
                        dr["numBillet"] = reader["numBillet"].ToString();
                        if (reader["excedentPoid"].ToString().Trim() != "" && reader["prixExcedent"].ToString().Trim() != "")
                        {
                            dr["excedent"] = serviceGeneral.separateurDesMilles(reader["prixExcedent"].ToString()) + "Ar (" + reader["excedentPoid"] + "Kg)";

                            try
                            {
                                dr["somme"] = serviceGeneral.separateurDesMilles((double.Parse(reader["prixBillet"].ToString()) + double.Parse(reader["prixExcedent"].ToString())).ToString()) + "Ar";
                            }
                            catch (Exception)
                            {
                            }
                        }
                        else
                        {
                            dr["excedent"] = "-";
                            dr["somme"] = serviceGeneral.separateurDesMilles(reader["prixBillet"].ToString()) + "Ar";
                        }
                        

                        dataTable.Rows.Add(dr);
                    }
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            #endregion

            return dataTable;
        }
    }
}