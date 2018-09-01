using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using arch.dal.intf;

namespace arch.dal.impl
{
    /// <summary>
    /// Implementation du Connect Excel
    /// </summary>
    public class implDalServiceConnectExcel : intfDalServiceConnectExcel
    {
        #region declaration variable
        OleDbConnection connection = null;


        string connectionString = "";
        string commandeString = "";

        bool isConnection = false;

        public bool IsConnection
        {
            get
            {
                return isConnection;
            }
            set
            {
                isConnection = value;
            }
        }
        #endregion

        #region construction
        public implDalServiceConnectExcel(string connectionString)
        {
            this.connectionString = connectionString;
            connection = new OleDbConnection(this.connectionString);
        }
        #endregion

        #region methode

        public OleDbDataReader select(string strCommande)
        {
            #region declaration
            OleDbDataReader read = null;
            OleDbCommand command = null;
            #endregion

            #region implementation
            try
            {
                command = new OleDbCommand(strCommande, connection);
                read = command.ExecuteReader();
            }
            catch (OleDbException)
            {
                read = null;
            }
            #endregion

            return read;
        }

        public void openConnection()
        {
            try
            {
                this.connection.Open();
                this.isConnection = true;
            }
            catch (OleDbException)
            {
            }
        }

        public void closeConnection()
        {
            try
            {
                this.connection.Close();
                this.isConnection = false;
            }
            catch (OleDbException)
            {
            }
        }

        public int requete(string strCommand)
        {
            OleDbCommand command = null;
            int nombre;

            try
            {
                command = new OleDbCommand(strCommand, this.connection);
                nombre = command.ExecuteNonQuery();
            }
            catch (OleDbException)
            {
                nombre = 0;
            }

            return nombre;
        }

        #endregion

        //INSERT INTO [Feuil1$] ([COL 1], [COL 2]) VALUES ('Valeur dans la cellule A1', 'Autre valeur dans la cellule A2')
        //SELECT * FROM [Feuil1$]
    }
}