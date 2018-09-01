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
    /// Implementation du service voiture
    /// </summary>
    public class ImplDalVoiture : IntfDalVoiture
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalVoiture()
        {
            this.serviceRessource = new ImplDalServiceRessource();
            this.serviceConnectBase = new ImplDalConnectBase(this.serviceRessource.getDefaultStrConnection());
        }
        public ImplDalVoiture(string strConnection)
        {
            this.serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region IntfDalVoiture Members

        string IntfDalVoiture.insertVoiture(crlVoiture Voiture)
        {
            #region declaration
            int nombreInsertion = 0;
            string numVehicule = "";
            IntfDalVoiture servicVoiture = new ImplDalVoiture();
            #endregion

            #region implementation
            if (Voiture != null)
            {
                Voiture.NumVehicule = servicVoiture.getNumVehicule();

                this.strCommande = "INSERT INTO `voiture` (`numVehicule`,`numLicence`,";
                this.strCommande += " `couleur`,`type`,`marque`,`nombrePlace`,`numImmatricule`,`colone`) ";
                this.strCommande += " VALUES ('" + Voiture.NumVehicule + "','" + Voiture.NumLicence + "'";
                this.strCommande += " ,'" + Voiture.Couleur + "','" + Voiture.Type + "'";
                this.strCommande += " ,'" + Voiture.Marque + "', '" + Voiture.NombrePlace + "'";
                this.strCommande += " ,'" + Voiture.NumImmatricule + "','" + Voiture.Colone + "')";

                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    numVehicule = Voiture.NumVehicule;
                this.serviceConnectBase.closeConnection();

            }
            #endregion

            return numVehicule;
        }

        bool IntfDalVoiture.deleteVoiture(crlVoiture Voiture)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (Voiture != null)
            {
                if (Voiture.NumVehicule != "")
                {
                    this.strCommande = "DELETE FROM `voiture` WHERE (`numVehicule` = '" + Voiture.NumVehicule + "')";
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

        bool IntfDalVoiture.deleteVoiture(string numVehicule)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation

            if (numVehicule != "")
            {
                this.strCommande = "DELETE FROM `voiture` WHERE (`numVehicule` = '" + numVehicule + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete == 1)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }
            
            #endregion

            return isDelete;
        }

        bool IntfDalVoiture.updateVoiture(crlVoiture Voiture)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            #endregion

            #region implementation
            if (Voiture != null)
            {
                if (Voiture.NumVehicule != "")
                {
                    this.strCommande = "UPDATE `voiture` SET ";
                    this.strCommande += ",`numLicence`='" + Voiture.NumLicence + "',`couleur`='" + Voiture.Couleur + "'";
                    this.strCommande += ",`type`='" + Voiture.Type + "',`marque`='" + Voiture.Marque + "',`colone`='" + Voiture.Colone + "'";
                    this.strCommande += ",`nombrePlace`='" + Voiture.NombrePlace + "',`numImmatricule`='" + Voiture.NumImmatricule + "'";
                    this.strCommande += " WHERE (`numVehicule`='" + Voiture.NumVehicule + "')";

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

        int IntfDalVoiture.isVoiture(crlVoiture Voiture)
        {
            #region declaratio
            int isVoiture = 0;
            #endregion

            #region implementation
            if (Voiture != null) 
            {
                this.strCommande = "SELECT * FROM `voiture` WHERE (`numVehicule` <> '" + Voiture.NumVehicule + "')";
                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null) 
                {
                    if (reader.HasRows) 
                    {
                        while(reader.Read()) 
                        {
                            if (Voiture.NumImmatricule.Trim().ToLower().Equals(reader["numImmatricule"].ToString().Trim().ToLower())) 
                            {
                                isVoiture = 1;
                                break;
                            }
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isVoiture;
        }

        crlVoiture IntfDalVoiture.selectVoiture(string numVehicule)
        {
            #region declaration
            IntfDalLicence serviceLicence = new ImplDalLicence();
            
            crlVoiture voiture = null;
            #endregion

            #region implementation
            if (numVehicule != "") 
            {
                this.strCommande = "SELECT * FROM `voiture` WHERE (`numVehicule`='" + numVehicule + "')";
                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null) 
                {
                    if (reader.HasRows) 
                    {
                        if (reader.Read()) 
                        {
                            voiture = new crlVoiture();
                            voiture.Couleur = reader["couleur"].ToString();
                            voiture.Marque = reader["marque"].ToString();
                            voiture.NumImmatricule = reader["numImmatricule"].ToString();
                            voiture.NumLicence = reader["numLicence"].ToString();
                            voiture.NumVehicule = reader["numVehicule"].ToString();
                            try
                            {
                                voiture.Colone = int.Parse(reader["colone"].ToString());
                                voiture.NombrePlace = int.Parse(reader["nombrePlace"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            voiture.Type = reader["type"].ToString();
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (voiture != null) 
                {
                    if (voiture.NumLicence != "") 
                    {
                        voiture.Licence = serviceLicence.selectLicence(voiture.NumLicence);
                    }
                }
            }
            #endregion

            return voiture;
        }

        void IntfDalVoiture.loadDdlTri(DropDownList ddlTri)
        {
            ddlTri.Items.Clear();
            ddlTri.Items.Add(new ListItem("Numeros", "numVehicule"));
            ddlTri.Items.Add(new ListItem("Matricule", "numImmatricule"));
            ddlTri.Items.Add(new ListItem("Prénom", "prenom"));
            ddlTri.Items.Add(new ListItem("Type", "type"));
            ddlTri.Items.Add(new ListItem("Marque", "marque"));
            ddlTri.Items.Add(new ListItem("Nombre de place", "nombrePlace"));
        }

        string IntfDalVoiture.getNumVehicule()
        {
            #region declaration
            int numTemp = 0;
            string numVehicule = "00001";
            string[] tempNumVehicule = null;
            #endregion

            #region implementation
            this.strCommande = "SELECT voiture.numVehicule AS maxNum FROM voiture ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumVehicule = reader["maxNum"].ToString().Split('/');
                        numVehicule = tempNumVehicule[0];
                    }
                    numTemp = int.Parse(numVehicule) + 1;
                    if (numTemp < 10)
                        numVehicule = "0000" + numTemp;
                    if (numTemp < 100 && numTemp >= 10)
                        numVehicule = "000" + numTemp;
                    if (numTemp < 1000 && numTemp >= 100)
                        numVehicule = "00" + numTemp;
                    if (numTemp < 10000 && numTemp >= 1000)
                        numVehicule = "0" + numTemp;
                    if (numTemp >= 10000)
                        numVehicule = "" + numTemp;
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numVehicule = numVehicule + "/VE/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString("0000");
            #endregion

            return numVehicule;
        }

       
        void IntfDalVoiture.loadDdlItineraire(DropDownList ddlItineraire, List<crlItineraire> Itineraires)
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

        #endregion
    }
}
