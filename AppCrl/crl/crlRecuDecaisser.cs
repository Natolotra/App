using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlRecuDecaisser
    /// </summary>
    public class crlRecuDecaisser
    {
        #region variable
        string numRecuDecaisser;
        string matriculeAgent;
        DateTime dateRecuDecaisser;
        double motantRecuDecaisser;
        string libelleRecuDecaisser;
        #endregion

        #region encapsulation
        public string NumRecuDecaisser
        {
            get
            {
                return numRecuDecaisser;
            }
            set
            {
                numRecuDecaisser = value;
            }
        }
        public string MatriculeAgent
        {
            get
            {
                return matriculeAgent;
            }
            set
            {
                matriculeAgent = value;
            }
        }
        public DateTime DateRecuDecaisser
        {
            get
            {
                return dateRecuDecaisser;
            }
            set
            {
                dateRecuDecaisser = value;
            }
        }
        public double MotantRecuDecaisser
        {
            get
            {
                return motantRecuDecaisser;
            }
            set
            {
                motantRecuDecaisser = value;
            }
        }
        public string LibelleRecuDecaisser
        {
            get
            {
                return libelleRecuDecaisser;
            }
            set
            {
                libelleRecuDecaisser = value;
            }
        }
        #endregion

        #region variable d'objet
        public crlAgent agent;
        #endregion

        #region constructeur
        public crlRecuDecaisser()
        {
            this.NumRecuDecaisser = "";
            this.MatriculeAgent = "";
            this.DateRecuDecaisser = DateTime.Now;
            this.MotantRecuDecaisser = 0;
            this.LibelleRecuDecaisser = "";

            this.agent = null;
        }
        #endregion
    }
}