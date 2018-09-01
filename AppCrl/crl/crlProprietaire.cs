using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Description résumée de Proprietaire
    /// </summary>
    public class crlProprietaire
    {
        #region declaration
        private string numProprietaire;
        private string typeProprietaire;
        private string numOrganisme;
        private string numSociete;
        private string numIndividu;
        private string numAgence;
        #endregion

        #region encapsulation
        public string NumProprietaire
        {
            get
            {
                return numProprietaire;
            }
            set
            {
                numProprietaire = value;
            }
        }
        public string TypeProprietaire
        {
            get
            {
                return typeProprietaire;
            }
            set
            {
                typeProprietaire = value;
            }
        }
        public string NumOrganisme
        {
            get
            {
                return numOrganisme;
            }
            set
            {
                numOrganisme = value;
            }
        }
        public string NumSociete
        {
            get
            {
                return numSociete;
            }
            set
            {
                numSociete = value;
            }
        }
        public string NumIndividu
        {
            get
            {
                return numIndividu;
            }
            set
            {
                numIndividu = value;
            }
        }
        public string NumAgence
        {
            get
            {
                return numAgence;
            }
            set
            {
                numAgence = value;
            }
        }
        #endregion

        #region variable d'objet
        public crlIndividu Individu;
        public crlSociete societe;
        public crlOrganisme organisme;
        public crlTypeProprietaire typeProprietaireObj;
        public crlAgence agence;
        #endregion

        #region constructeur
        public crlProprietaire()
        {
            this.NumProprietaire = "";
            this.TypeProprietaire = "";
            this.NumOrganisme = "";
            this.NumSociete = "";
            this.NumIndividu = "";
            this.NumAgence = "";

            this.agence = null;
            this.Individu = null;
            this.societe = null;
            this.organisme = null;
            this.typeProprietaireObj = null;
        }
        #endregion
    }
}