using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlObservationMateriel
    /// </summary>
    public class crlObservationMateriel
    {
        #region variable
        private string numObservation;
        private string numAppareil;
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
        public string NumAppareil
        {
            get
            {
                return numAppareil;
            }
            set
            {
                numAppareil = value;
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
        public crlObservationMateriel()
        {
            this.NumObservation = "";
            this.NumAppareil = "";
            this.TextObesvation = "";
            this.DateObservation = DateTime.Now;
            this.IsListeNoire = 0;
        }
        #endregion

    }
}