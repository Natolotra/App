

namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlEtablissementScolaire
    /// </summary>
    public class crlEtablissementScolaire
    {
        #region variable
        string numEtablissementScolaire;
        string etablissementScolaire;
        string typeEtablissementScolaire;
        string secteurEtablissementScolaire;
        string adresseEtablissementScolaire;
        string numQuartier;
        string telephoneFixeEtablissementScolaire;
        string telephonePortableEtablissementScolaire;
        string mailEtablissementScolaire;
        string numIndividuResponsable;
        private int isCheque;
        private int isBonCommande;

        #endregion

        #region encapsulation
        public string NumEtablissementScolaire
        {
            get
            {
                return numEtablissementScolaire;
            }
            set
            {
                numEtablissementScolaire = value;
            }
        }
        public string EtablissementScolaire
        {
            get
            {
                return etablissementScolaire;
            }
            set
            {
                etablissementScolaire = value;
            }
        }
        public string TypeEtablissementScolaire
        {
            get
            {
                return typeEtablissementScolaire;
            }
            set
            {
                typeEtablissementScolaire = value;
            }
        }
        public string SecteurEtablissementScolaire
        {
            get
            {
                return secteurEtablissementScolaire;
            }
            set
            {
                secteurEtablissementScolaire = value;
            }
        }
        public string AdresseEtablissementScolaire
        {
            get
            {
                return adresseEtablissementScolaire;
            }
            set
            {
                adresseEtablissementScolaire = value;
            }
        }
        public string NumQuartier
        {
            get
            {
                return numQuartier;
            }
            set
            {
                numQuartier = value;
            }
        }
        public string TelephoneFixeEtablissementScolaire
        {
            get
            {
                return telephoneFixeEtablissementScolaire;
            }
            set
            {
                telephoneFixeEtablissementScolaire = value;
            }
        }
        public string TelephonePortableEtablissementScolaire
        {
            get
            {
                return telephonePortableEtablissementScolaire;
            }
            set
            {
                telephonePortableEtablissementScolaire = value;
            }
        }
        public string MailEtablissementScolaire
        {
            get
            {
                return mailEtablissementScolaire;
            }
            set
            {
                mailEtablissementScolaire = value;
            }
        }
        public string NumIndividuResponsable
        {
            get
            {
                return numIndividuResponsable;
            }
            set
            {
                numIndividuResponsable = value;
            }
        }
        public int IsCheque
        {
            get
            {
                return isCheque;
            }
            set
            {
                isCheque = value;
            }
        }
        public int IsBonCommande
        {
            get
            {
                return isBonCommande;
            }
            set
            {
                isBonCommande = value;
            }
        }
        #endregion

        #region variable d'objet
        public crlIndividu individuResponsable;
        #endregion

        #region constructeur
        public crlEtablissementScolaire()
        {
            this.AdresseEtablissementScolaire = "";
            this.EtablissementScolaire = "";
            this.MailEtablissementScolaire = "";
            this.NumEtablissementScolaire = "";
            this.TelephoneFixeEtablissementScolaire = "";
            this.TelephonePortableEtablissementScolaire = "";
            this.TypeEtablissementScolaire = "";
            this.NumQuartier = "";
            this.SecteurEtablissementScolaire = "";
            this.NumIndividuResponsable = "";
            this.IsCheque = -1;
            this.IsBonCommande = -1;

            this.individuResponsable = null;
        }
        #endregion
    }
}