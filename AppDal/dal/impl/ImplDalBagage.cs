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
    /// Implementation du service bagage
    /// </summary>
    public class ImplDalBagage : IntfDalBagage
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalBagage()
        {
            this.serviceRessource = new ImplDalServiceRessource();
            this.serviceConnectBase = new ImplDalConnectBase(this.serviceRessource.getDefaultStrConnection());
        }
        public ImplDalBagage(string strConnection)
        {
            this.serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region IntfDalBagage Members

        string IntfDalBagage.insertBagage(crlBagage Bagage, string sigleAgence)
        {
            #region declaration
            IntfDalBagage serviceBagage = new ImplDalBagage();
            int nombreInsertion = 0;
            string idBagage = "";
            #endregion

            #region implementation
            if (Bagage != null)
            {

                Bagage.IdBagage = serviceBagage.getIdBagage(sigleAgence);
                this.strCommande = "INSERT INTO `bagage` (`idBagage`,`numRecu`,";
                this.strCommande += "`excedentPoid`,`prixExcedent`) VALUES ";
                this.strCommande += "('" + Bagage.IdBagage + "','" + Bagage.NumRecu + "',";
                this.strCommande += "'" + Bagage.ExcedentPoid+ "',";
                this.strCommande += "'" + Bagage.PrixExcedent + "')";

                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    idBagage = Bagage.IdBagage;
                this.serviceConnectBase.closeConnection();

            }
            #endregion

            return idBagage;
        }

        bool IntfDalBagage.deleteBagage(crlBagage Bagage)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (Bagage != null)
            {
                if (Bagage.IdBagage != "")
                {
                    this.strCommande = "DELETE FROM `bagage` WHERE (`idBagage` = '" + Bagage.IdBagage + "')";
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

        bool IntfDalBagage.deleteBagage(string idBagage)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            
            if (idBagage != "")
            {
                this.strCommande = "DELETE FROM `bagage` WHERE (`idBagage` = '" + idBagage + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete == 1)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }
            
            #endregion

            return isDelete;
        }

        bool IntfDalBagage.deleteAssociationBagageVoyage(string idVoyage)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation

            if (idVoyage != "")
            {
                this.strCommande = "DELETE FROM `associationvoyagebagage` WHERE (`idVoyage` = '" + idVoyage + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete == 1)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }

            #endregion

            return isDelete;
        }

        bool IntfDalBagage.insertAssociationBagageVoyage(string idVoyage, string idBagage)
        {
            #region declaration
            bool isInsert = false;
            int nombreInsertion = 0;
            #endregion

            #region implementation
            if (idVoyage != "" && idBagage != "")
            {
                this.strCommande = "INSERT INTO `associationvoyagebagage` (`idVoyage`,`idBagage`)";
                this.strCommande += " VALUES ('" + idVoyage + "','" + idBagage + "')";

                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    isInsert = true; ;
                this.serviceConnectBase.closeConnection();

            }
            #endregion

            return isInsert;
        }

        bool IntfDalBagage.updateBagage(crlBagage Bagage)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            #endregion

            #region implementation
            if (Bagage != null)
            {
                if (Bagage.IdBagage != "")
                {
                    this.strCommande = "UPDATE `bagage` SET `numRecu`='" + Bagage.NumRecu + "', ";
                    this.strCommande += "`excedentPoid`='" + Bagage.ExcedentPoid + "', ";
                    this.strCommande += "`prixExcedent`='" + Bagage.PrixExcedent + "' ";
                    this.strCommande += "WHERE (`idBagage`='" + Bagage.IdBagage + "')";

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

        int IntfDalBagage.isBagage(crlBagage Bagage)
        {
            throw new NotImplementedException();
        }

        crlBagage IntfDalBagage.selectBagage(string idBagage)
        {
            #region initialisation
            IntfDalRecuEncaisser serviceRecu = new ImplDalRecuEncaisser();

            crlBagage Bagage = null;
            #endregion

            #region implementation
            if (idBagage != "")
            {
                this.strCommande = "SELECT * FROM `bagage` WHERE (`idBagage`='" + idBagage + "')";
                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        Bagage = new crlBagage();
                        Bagage.ExcedentPoid = double.Parse(reader["excedentPoid"].ToString());
                        Bagage.IdBagage = reader["idBagage"].ToString();
                        Bagage.NumRecu = reader["numRecu"].ToString();
                        Bagage.PrixExcedent = reader["prixExcedent"].ToString();
                       
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (Bagage != null)
                {
                    Bagage.recu = serviceRecu.selectRecuEncaisser(Bagage.NumRecu);
                }
            }
            #endregion

            return Bagage;
        }

        crlBagage IntfDalBagage.selectBagageForVoyage(string idVoyage)
        {
            #region initialisation
            IntfDalRecuEncaisser serviceRecu = new ImplDalRecuEncaisser();

            crlBagage Bagage = null;
            #endregion

            #region implementation
            if (idVoyage != "")
            {
                this.strCommande = "SELECT bagage.idBagage, bagage.numRecu, bagage.excedentPoid,";
                this.strCommande += " bagage.prixExcedent FROM bagage";
                this.strCommande += " Inner Join associationvoyagebagage ON associationvoyagebagage.idBagage = bagage.idBagage";
                this.strCommande += " WHERE associationvoyagebagage.idVoyage = '" + idVoyage + "'";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        Bagage = new crlBagage();
                        Bagage.ExcedentPoid = double.Parse(reader["excedentPoid"].ToString());
                        Bagage.IdBagage = reader["idBagage"].ToString();
                        Bagage.NumRecu = reader["numRecu"].ToString();
                        Bagage.PrixExcedent = reader["prixExcedent"].ToString();

                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (Bagage != null)
                {
                    Bagage.recu = serviceRecu.selectRecuEncaisser(Bagage.NumRecu);
                }
            }
            #endregion

            return Bagage;
        }

        string IntfDalBagage.getIdBagage(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string idBagage = "00001";
            string[] tempIdBagage = null;
            string strDate = "BA" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT bagage.idBagage AS maxNum FROM bagage";
            this.strCommande += " WHERE bagage.idBagage LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempIdBagage = reader["maxNum"].ToString().ToString().Split('/');
                        idBagage = tempIdBagage[tempIdBagage.Length - 1];
                    }
                    numTemp = double.Parse(idBagage) + 1;
                    if (numTemp < 10)
                        idBagage = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        idBagage = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        idBagage = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        idBagage = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        idBagage = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            idBagage = strDate + "/" + sigleAgence + "/" + idBagage;
            #endregion

            return idBagage;
        }

        void IntfDalBagage.loadDdlTri(DropDownList ddlTri)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
