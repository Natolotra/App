using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Observation agent objet
    /// </summary>
    public class crlObservationAgent
    {
        #region variable
        private string numObservation;
        private string matriculeAgent;
        private string textObesvation;
        private DateTime dateObservation;
        private int isListeNoire;
        #endregion

        #region encapsulation
        public string NumObservation
        {
            get
            {
                return numObservation;
            }
            set
            {
                numObservation = value;
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
        public string TextObesvation
        {
            get
            {
                return textObesvation;
            }
            set
            {
                textObesvation = value;
            }
        }
        public DateTime DateObservation
        {
            get
            {
                return dateObservation;
            }
            set
            {
                dateObservation = value;
            }
        }
        public int IsListeNoire
        {
            get
            {
                return isListeNoire;
            }
            set
            {
                isListeNoire = value;
            }
        }
        #endregion

        #region constructeur
        public crlObservationAgent()
        {
            this.NumObservation = "";
            this.MatriculeAgent = "";
            this.TextObesvation = "";
            this.DateObservation = DateTime.Now;
            this.IsListeNoire = 0;
        }
        #endregion
    }
}