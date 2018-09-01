using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Objet pour suivi commission
    /// </summary>
    public class crlSuiviCommission
    {
        #region variable
        private string numSuiviCommission;
        private string matriculeAgent;
        private string idCommission;
        private DateTime dateHeure;
        private string commentaire;
        #endregion

        #region accesseur
        public string NumSuiviCommission
        {
            get
            {
                return numSuiviCommission;
            }
            set
            {
                numSuiviCommission = value;
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
        public string IdCommission
        {
            get
            {
                return idCommission;
            }
            set
            {
                idCommission = value;
            }
        }
        public DateTime DateHeure
        {
            get
            {
                return dateHeure;
            }
            set
            {
                dateHeure = value;
            }
        }
        public string Commentaire
        {
            get
            {
                return commentaire;
            }
            set
            {
                commentaire = value;
            }
        }
        #endregion

        #region constructeur
        public crlSuiviCommission()
        {
            this.NumSuiviCommission = "";
            this.MatriculeAgent = "";
            this.IdCommission = "";
            this.DateHeure = DateTime.Now;
            this.Commentaire = "";
        }
        #endregion
    }
}