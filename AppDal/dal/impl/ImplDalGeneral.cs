using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using arch.dal.intf;

namespace arch.dal.impl
{
    public class ImplDalGeneral : IntfDalGeneral
    {
        #region declaration
        ImplDalConnectBase serviceConnection = null;
        IntfDalServiceRessource serviceRessource = null;
        MySqlDataReader reader = null;

        string strCommande = "";
        string strConnection = "";
        #endregion

        #region constructeur
        public ImplDalGeneral(string strConnection) 
        {
            this.strConnection = strConnection;
            serviceConnection = new ImplDalConnectBase(strConnection);
        }
        public ImplDalGeneral()
        {
            this.serviceRessource = new ImplDalServiceRessource();
            this.strConnection = this.serviceRessource.getDefaultStrConnection();
            this.serviceConnection = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region IntfDalGeneral Membres

        int IntfDalGeneral.delete(string paramTable, string paramPropriete, string paramValue)
        {
            #region variable
            int isDelete = 0;
            #endregion

            #region implementation
            if (paramTable.Trim() != "" && paramPropriete.Trim() != "") 
            {
                this.strCommande = "DELETE FROM `" + paramTable + "` WHERE (`" + paramPropriete + "`='" + paramValue + "')";

                this.serviceConnection.openConnection();
                if (this.serviceConnection.IsConnection) 
                {
                    isDelete= this.serviceConnection.requete(this.strCommande);
                    
                    while(this.serviceConnection.IsConnection)
                        this.serviceConnection.closeConnection();
                }
            }
            #endregion

            return isDelete;
        }

        int IntfDalGeneral.upDate(string strCommande)
        {
            #region variable
            int isUpdate = 0;
            #endregion

            #region implementation
            if (strCommande.Trim() != "") 
            {
                this.strCommande = strCommande;

                this.serviceConnection.openConnection();
                if (this.serviceConnection.IsConnection) 
                {
                    isUpdate = this.serviceConnection.requete(this.strCommande);
                   
                    while (this.serviceConnection.IsConnection)
                        this.serviceConnection.closeConnection();
                }
            }
            #endregion

            return isUpdate;
        }

        DataTable IntfDalGeneral.getDateTable(string strCommand)
        {
            #region Region declaration des variables
            MySqlConnection mySqlConnect = null;
            MySqlCommand mySqlCmd = null;
            MySqlDataAdapter mySqlDataAdapt = null;

            DataSet dsTable = null;
            DataView dvTable = null;

            mySqlConnect = new MySqlConnection(this.strConnection);
            #endregion

            #region implementation
            string sqlReq = null;
            //Contruction de la requete
            sqlReq = strCommand;

            //Ouverture de la connexion à la base de données
            mySqlConnect.Open();

            try
            {
                //Faire l'objet commande
                mySqlCmd = new MySqlCommand(sqlReq, mySqlConnect);

                //Ajouter les paramètres
                dvTable = new DataView();
                dsTable = new DataSet();
                mySqlDataAdapt = new MySqlDataAdapter(mySqlCmd);



                mySqlDataAdapt.Fill(dsTable, "table");
                dvTable = dsTable.Tables["table"].DefaultView;
            }
            catch (Exception erreur)
            {
                throw new Exception("une erreur est recuperée sur : " + erreur.Message);
            }
            finally
            {
                mySqlDataAdapt.Dispose();
                mySqlCmd.Dispose();
                mySqlConnect.Close();
                mySqlConnect = null;
            }
            #endregion

            return dvTable.ToTable();
        }

        TimeSpan IntfDalGeneral.getTimeSpan(string timeSpanStr)
        {
               /************************************/
              /*string time Span du de la forme 00:00:00:00*/
             /*******de la forme jj:HH:mm:ss********/
            /***********************************/
            #region declaration
            TimeSpan timeSpan;

            string[] timeSpanStrTab;
            int nbJour = 0;
            int nbHeure = 0;
            int nbMinute = 0;
            int nbSeconde = 0;
            #endregion

            #region implementation
            if (timeSpanStr != "")
            {
                timeSpanStrTab = timeSpanStr.Split(':');

                if (timeSpanStrTab.Length == 4)
                {
                    try
                    {
                        nbJour = int.Parse(timeSpanStrTab[0]);
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        nbHeure = int.Parse(timeSpanStrTab[1]);
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        nbMinute = int.Parse(timeSpanStrTab[2]);
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        nbSeconde = int.Parse(timeSpanStrTab[3]);
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            timeSpan = new TimeSpan(nbJour, nbHeure, nbMinute, nbSeconde);
            #endregion

            return timeSpan;
        }

        string IntfDalGeneral.getStringTimeSpan(TimeSpan timeSpan)
        {
            /************************************/
            /*string time Span du de la forme 00:00:00:00*/
            /*********de la forme jj:HH:mm:ss******/
            /************************************/
            #region declaration
            string timeSpanStr = "";
            #endregion

            #region implementation
            timeSpanStr += timeSpan.Days.ToString("00") + ":";
            timeSpanStr += timeSpan.Hours.ToString("00") + ":";
            timeSpanStr += timeSpan.Minutes.ToString("00") + ":";
            timeSpanStr += timeSpan.Seconds.ToString("00");
            #endregion

            return timeSpanStr;
        }

        string IntfDalGeneral.getTextTimeSpan(string timeSpanStr)
        {
            /************************************/
            /*string time Span du de la forme 00:00:00:00*/
            /*********de la forme jj:HH:mm:ss******/
            /************************************/
            #region declaration
            string textTimeSpan = "";
            string[] tabTimeSpan;
            int jour = 0;
            int heure = 0;
            int minute = 0;
            int seconde = 0;
            #endregion

            #region implementation
            if (timeSpanStr != "")
            {
                tabTimeSpan = timeSpanStr.Split(':');
                if (tabTimeSpan.Length == 4)
                {
                    try
                    {
                        jour = int.Parse(tabTimeSpan[0]);
                    }
                    catch (Exception)
                    {
                        jour = 0;
                    }

                    try
                    {
                        heure = int.Parse(tabTimeSpan[1]);
                    }
                    catch (Exception)
                    {
                        heure = 0;
                    }

                    try
                    {
                        minute = int.Parse(tabTimeSpan[2]);
                    }
                    catch (Exception)
                    {
                        minute = 0;
                    }

                    try
                    {
                        seconde = int.Parse(tabTimeSpan[3]);
                    }
                    catch (Exception)
                    {
                        seconde = 0;
                    }

                    if (jour > 0)
                    {
                        textTimeSpan = textTimeSpan + jour.ToString() + "Jour(s) ";
                    }
                    if (heure > 0)
                    {
                        textTimeSpan = textTimeSpan + heure.ToString() + "H ";
                    }
                    if (minute > 0)
                    {
                        textTimeSpan = textTimeSpan + minute.ToString() + "min ";
                    }
                    if (seconde > 0)
                    {
                        textTimeSpan = textTimeSpan + seconde.ToString() + "sec ";
                    }

                }
            }
            #endregion

            return textTimeSpan;
        }

        string IntfDalGeneral.separateurDesMilles(string strMontant)
        {
            #region declaration
            string strMontantSeparMille = "";
            char[] tabMontant;
            #endregion

            #region implementation
            tabMontant = strMontant.ToCharArray();

            for (int i = 0; i < tabMontant.Length; i++)
            {
                if ((i != 0) && ((i % 3) == 0))
                {
                    strMontantSeparMille = " " + strMontantSeparMille;
                }

                strMontantSeparMille = tabMontant[tabMontant.Length - (i + 1)] + strMontantSeparMille;
            }
            #endregion

                return strMontantSeparMille;
        }

        #endregion
    }
}
