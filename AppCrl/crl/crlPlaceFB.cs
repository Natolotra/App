using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Objet place FB
    /// </summary>
    public class crlPlaceFB
    {
        #region variable
        private string numerosFB;
        private string numPlace;
        private int isOccuper;
        #endregion

        #region encapsulation 
        public string NumerosFB
        {
            get
            {
                return numerosFB;
            }
            set
            {
                numerosFB = value;
            }
        }
        public string NumPlace
        {
            get
            {
                return numPlace;
            }
            set
            {
                numPlace = value;
            }
        }
        public int IsOccuper
        {
            get
            {
                return isOccuper;
            }
            set
            {
                isOccuper = value;
            }
        }
        #endregion

        #region constructeur
        public crlPlaceFB()
        {
            this.NumerosFB = "";
            this.NumPlace = "";
            this.IsOccuper = 0;
        }
        #endregion
    }
}