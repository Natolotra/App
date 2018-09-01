using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlUSValidationReduction
    /// </summary>
    public class crlUSValidationReduction
    {
        #region variable
        string numUSValidationReduction;
        DateTime dateValidationReduction;
        string numUSReductionParticulier;
        string numCarte;
        DateTime valideDu;
        DateTime valideAu;
        int isLundi;
        int isMardi;
        int isMercredi;
        int isJeudi;
        int isVendredi;
        int isSamedi;
        int isDimanche;
        string matriculeAgent;
        string matriculeAgentControleur;
        int isValider;
        #endregion

        #region encapsulation
        public string NumUSValidationReduction
        {
            get
            {
                return numUSValidationReduction;
            }
            set
            {
                numUSValidationReduction = value;
            }
        }
        public DateTime DateValidationReduction
        {
            get
            {
                return dateValidationReduction;
            }
            set
            {
                dateValidationReduction = value;
            }
        }
        public string NumUSReductionParticulier
        {
            get
            {
                return numUSReductionParticulier;
            }
            set
            {
                numUSReductionParticulier = value;
            }
        }
        public string NumCarte
        {
            get
            {
                return numCarte;
            }
            set
            {
                numCarte = value;
            }
        }
        public DateTime ValideDu
        {
            get
            {
                return valideDu;
            }
            set
            {
                valideDu = value;
            }
        }
        public DateTime ValideAu
        {
            get
            {
                return valideAu;
            }
            set
            {
                valideAu = value;
            }
        }
        public int IsLundi
        {
            get
            {
                return isLundi;
            }
            set
            {
                isLundi = value;
            }
        }
        public int IsMardi
        {
            get
            {
                return isMardi;
            }
            set
            {
                isMardi = value;
            }
        }
        public int IsMercredi
        {
            get
            {
                return isMercredi;
            }
            set
            {
                isMercredi = value;
            }
        }
        public int IsJeudi
        {
            get
            {
                return isJeudi;
            }
            set
            {
                isJeudi = value;
            }
        }
        public int IsVendredi
        {
            get
            {
                return isVendredi;
            }
            set
            {
                isVendredi = value;
            }
        }
        public int IsSamedi
        {
            get
            {
                return isSamedi;
            }
            set
            {
                isSamedi = value;
            }
        }
        public int IsDimanche
        {
            get
            {
                return isDimanche;
            }
            set
            {
                isDimanche = value;
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
        public string MatriculeAgentControleur
        {
            get
            {
                return matriculeAgentControleur;
            }
            set
            {
                matriculeAgentControleur = value;
            }
        }
        public int IsValider
        {
            get
            {
                return isValider;
            }
            set
            {
                isValider = value;
            }
        }
        #endregion

        #region constructeur
        public crlUSValidationReduction()
        {
            this.NumUSValidationReduction = "";
            this.NumUSReductionParticulier = "";
            this.DateValidationReduction = DateTime.Now;
            this.NumCarte = "";
            this.ValideAu = DateTime.Now;
            this.ValideDu = DateTime.Now;
            this.IsLundi = 0;
            this.IsMardi = 0;
            this.IsMercredi = 0;
            this.IsJeudi = 0;
            this.IsVendredi = 0;
            this.IsSamedi = 0;
            this.IsDimanche = 0;
            this.MatriculeAgent = "";
            this.MatriculeAgentControleur = "";
            this.IsValider = -1;
        }
        #endregion
    }
}