using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.dal.intf;
using MySql.Data.MySqlClient;
using arch.crl;
using System.Web.UI.WebControls;

namespace arch.dal.impl
{
    /// <summary>
    /// Implementation du service Place FB
    /// </summary>
    public class ImplDalPlaceFB : IntfDalPlaceFB
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalPlaceFB()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalPlaceFB(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region IntfDalChauffeur Members

        bool IntfDalPlaceFB.insertPlaceFB(crlPlaceFB PlaceFB)
        {
            #region declaration
            IntfDalPlaceFB servicePlaceFB = new ImplDalPlaceFB();

            bool isInsert = false;
            int nombreInsert = 0;
            #endregion

            #region implementation
            if (PlaceFB != null)
            {
                if (PlaceFB.NumerosFB != "")
                {
                    PlaceFB.NumPlace = servicePlaceFB.getNumPlaceFB(PlaceFB.NumerosFB);
                    this.strCommande = "INSERT INTO `PlaceFB` (`numerosFB`,`numPlace`,`isOccuper`)";
                    this.strCommande += " VALUES ('" + PlaceFB.NumerosFB + "','" + PlaceFB.NumPlace + "','" + PlaceFB.IsOccuper + "')";

                    this.serviceConnectBase.openConnection();
                    nombreInsert = this.serviceConnectBase.requete(this.strCommande);
                    if (nombreInsert == 1)
                    {
                        isInsert = true;
                    }
                    this.serviceConnectBase.closeConnection();
                }
            }
            #endregion

            return isInsert;
        }

        string IntfDalPlaceFB.insertPlaceForFB(crlFicheBord FicheBord)
        {
            #region declaration
            IntfDalPlaceFB servicePlaceFB = new ImplDalPlaceFB();
            crlPlaceFB tempPlaceFB = null; 

            int nombreInsert = 0;
            int nombreDePlace = 0;
            string numerosFB = "";
            #endregion

            #region implementation
            if (FicheBord != null)
            {
                if (FicheBord.autorisationVoyage != null)
                {
                    if (FicheBord.autorisationVoyage.Verification != null)
                    {
                        if (FicheBord.autorisationVoyage.Verification.Licence != null)
                        {
                            nombreDePlace = FicheBord.autorisationVoyage.Verification.Licence.NombrePlacePayante;

                            if (nombreDePlace > 0)
                            {
                                for (int i = 0; i < nombreDePlace; i++)
                                {
                                    tempPlaceFB = new crlPlaceFB();
                                    tempPlaceFB.NumerosFB = FicheBord.NumerosFB;
                                    if (servicePlaceFB.insertPlaceFB(tempPlaceFB))
                                    {
                                        nombreInsert += 1;
                                    }
                                    
                                }
                                if (nombreInsert == nombreDePlace)
                                {
                                    numerosFB = FicheBord.NumerosFB;
                                }
                                else
                                {
                                    servicePlaceFB.deletePlaceFB(FicheBord.NumerosFB);
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            return numerosFB;
        }

        bool IntfDalPlaceFB.deletePlaceFB(crlPlaceFB PlaceFB)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (PlaceFB != null)
            {
                if (PlaceFB.NumerosFB != "")
                {
                    this.strCommande = "DELETE FROM `PlaceFB` WHERE (`numerosFB` = '" + PlaceFB.NumerosFB + "')";
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

        bool IntfDalPlaceFB.deletePlaceFB(string numerosFB)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
           
                if (numerosFB != "")
                {
                    this.strCommande = "DELETE FROM `PlaceFB` WHERE (`numerosFB` = '" + numerosFB + "')";
                    this.serviceConnectBase.openConnection();
                    nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                    if (nombreDelete == 1)
                        isDelete = true;
                    this.serviceConnectBase.closeConnection();
                }
            
            #endregion

            return isDelete;
        }

        List<crlPlaceFB> IntfDalPlaceFB.selectPlaceFB(string numerosFB)
        {
            #region declaration
            List<crlPlaceFB> placeFBs = null;
            crlPlaceFB tempPlaceFB = null;
            #endregion

            #region implementation
            if (numerosFB != "")
            {
                this.strCommande = "SELECT * FROM `PlaceFB` WHERE (`numerosFB`='" + numerosFB + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        placeFBs = new List<crlPlaceFB>();
                        while (this.reader.Read())
                        {
                            tempPlaceFB = new crlPlaceFB();
                            tempPlaceFB.NumerosFB = this.reader["numerosFB"].ToString();
                            tempPlaceFB.NumPlace = this.reader["numPlace"].ToString();
                            tempPlaceFB.IsOccuper = int.Parse(this.reader["isOccuper"].ToString());

                            placeFBs.Add(tempPlaceFB);
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return placeFBs;
        }

        crlPlaceFB IntfDalPlaceFB.selectPlaceFB(string numerosFB, string numPlace)
        {
            #region declaration
            crlPlaceFB PlaceFB = null;
            #endregion

            #region implementation
            if (numerosFB != "")
            {
                this.strCommande = "SELECT * FROM `PlaceFB` WHERE (`numerosFB`='" + numerosFB + "' AND `numPlace`='" + numPlace + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        
                        if(this.reader.Read())
                        {
                            PlaceFB = new crlPlaceFB();
                            PlaceFB.NumerosFB = this.reader["numerosFB"].ToString();
                            PlaceFB.NumPlace = this.reader["numPlace"].ToString();
                            try
                            {
                                PlaceFB.IsOccuper = int.Parse(this.reader["isOccuper"].ToString());
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

            return PlaceFB;
        }

        string IntfDalPlaceFB.getNombrePlaceLibre(string numerosFB)
        {
            #region declaration
            string nbPlaceLibre = "0";
            #endregion

            #region implementation
            if (numerosFB != "")
            {
                this.strCommande = "SELECT Count(placefb.numerosFB) As nbPlace FROM";
                this.strCommande += " placefb WHERE placefb.numerosFB = '" + numerosFB + "'";
                this.strCommande += " AND placefb.isOccuper <>  '1'";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            nbPlaceLibre = reader["nbPlace"].ToString();
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return nbPlaceLibre;
        }

        string IntfDalPlaceFB.getNumPlaceFB(string numerosFB)
        {
            #region declaration
            int numTemp = 0;
            string numPlaceFB = "002";
            string tempNumPlaceFb = "";
            #endregion

            #region implementation
            if (numerosFB != "")
            {
                this.strCommande = "SELECT numPlace FROM `PlaceFB` WHERE (`numerosFB`='" + numerosFB + "') ORDER BY numPlace DESC";
                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            tempNumPlaceFb = reader["numPlace"].ToString();
                            numTemp = int.Parse(tempNumPlaceFb) + 1;
                         }
                         if (numTemp < 10)
                               numPlaceFB = "00" + numTemp;
                         if (numTemp < 100 && numTemp >= 10)
                             numPlaceFB = "0" + numTemp;
                         if (numTemp >= 100)
                             numPlaceFB = "" + numTemp;
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                 
            }
            #endregion

            return numPlaceFB;
        }

        bool IntfDalPlaceFB.updatePlaceFB(crlPlaceFB PlaceFB)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpDate = 0;
            #endregion

            #region implementation
            if (PlaceFB != null)
            {
                this.strCommande = "UPDATE `placefb` SET `isOccuper`='" + PlaceFB.IsOccuper + "'";
                this.strCommande += " WHERE (`numerosFB`='" + PlaceFB.NumerosFB + "' AND `numPlace`='" + PlaceFB.NumPlace + "')";

                this.serviceConnectBase.openConnection();
                nombreUpDate = this.serviceConnectBase.requete(this.strCommande);
                if (nombreUpDate > 0)
                {
                    isUpdate = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isUpdate;
        }
        #endregion


        void IntfDalPlaceFB.loadDdlPlaceFBLibre(DropDownList ddl, string numerosFB)
        {
            #region declaration
            #endregion

            #region implementation
            if (numerosFB != "")
            {
                this.strCommande = "SELECT placefb.numPlace FROM placefb WHERE";
                this.strCommande += " placefb.numerosFB = '" + numerosFB + "' AND";
                this.strCommande += " placefb.isOccuper = '0' ";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    ddl.Items.Clear();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ddl.Items.Add(reader["numPlace"].ToString());
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }
       
    }
}