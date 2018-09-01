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
using MySql.Data.MySqlClient;
using arch.dal.intf;

namespace arch.dal.impl
{
    public class ImplDalConnectBase : IntfDalConnectBase
    {
        private MySqlConnection connection = null;
        public MySqlConnection Connection
        {
            get { return connection; }
            set { connection = value; }
        }

        private string strConnection = null;
        public string StrConnection
        {
            get { return strConnection; }
            set { strConnection = value; }
        }

        private bool isConnection;
        public bool IsConnection
        {
            get { return isConnection; }
            set { isConnection = value; }
        } 

        public ImplDalConnectBase(string strConnection) 
        {
            this.StrConnection = strConnection;
            this.Connection = new MySqlConnection(this.StrConnection);
            this.isConnection = false;
        }

        #region IntfDalConnectBase Membres

        public void openConnection()
        {
            try
            {
                this.Connection.Open();
                this.isConnection = true;
            }
            catch (MySqlException) 
            { }
        }

        public void closeConnection()
        {
            try
            {
                this.Connection.Close();
                this.isConnection = false;
            }
            catch (MySqlException)
            { }
        }

        public MySqlDataReader select(string strCommand)
        {
            MySqlDataReader reader;
            MySqlCommand command;

            try
            {
                command = new MySqlCommand(strCommand, this.Connection);
                reader = command.ExecuteReader();
            }
            catch (MySqlException)
            {
                reader = null;
            }

            return reader;
        }

        public int requete(string strCommand)
        {
            MySqlCommand command = null;
            int nombre;

            try
            {
                command = new MySqlCommand(strCommand, this.Connection);
                nombre = command.ExecuteNonQuery();
            }
            catch (MySqlException)
            {
                nombre = 0;
            }

            return nombre;
        }

        #endregion

        #region IntfDalConnectBase Membres

        void IntfDalConnectBase.openConnection()
        {
            try
            {
                this.Connection.Open();
                this.isConnection = true;
            }
            catch (MySqlException)
            { }
        }

        void IntfDalConnectBase.closeConnection()
        {
            try
            {
                this.Connection.Close();
                this.isConnection = false;
            }
            catch (MySqlException)
            { }
        }

        MySqlDataReader IntfDalConnectBase.select(string strCommand)
        {
            MySqlDataReader reader;
            MySqlCommand command;

            try
            {
                command = new MySqlCommand(strCommand, this.Connection);
                reader = command.ExecuteReader();
            }
            catch (MySqlException)
            {
                reader = null;
            }

            return reader;
        }

        int IntfDalConnectBase.requete(string strCommand)
        {
            MySqlCommand command = null;
            int nombre;

            try
            {
                command = new MySqlCommand(strCommand, this.Connection);
                nombre = command.ExecuteNonQuery();
            }
            catch (MySqlException)
            {
                nombre = 0;
            }

            return nombre;
        }

        #endregion
    }
}
