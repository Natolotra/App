using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Objet oservation chauffeur
    /// </summary>
    public class crlObservationChauffeur
    {
        #region variable
        private string numObservation;
        private string idChauffeur;
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
        public string IdChauffeur
        {
            get
            {
                return idChauffeur;
            }
            set
            {
                idChauffeur = value;
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
        public crlObservationChauffeur()
        {
            this.NumObservation = "";
            this.IdChauffeur = "";
            this.TextObesvation = "";
            this.DateObservation = DateTime.Now;
            this.IsListeNoire = 0;
        }
        #endregion
    }
}