

namespace arch.crl
{
    /// <summary>
    /// Objet Type agent
    /// </summary>
    public class crlTypeAgent
    {

       #region declaration variable
       private string TypeAgent;
       private string commentaireTypeAgent;
       #endregion

       #region encapsulation
        public string typeAgent
        {
          get { return TypeAgent; }
          set { TypeAgent = value; }
        }
        public string CommentaireTypeAgent
        {
            get
            {
                return commentaireTypeAgent;
            }
            set
            {
                commentaireTypeAgent = value;
            }
        }
        #endregion

       #region constructeur
       public crlTypeAgent()
       {
           this.typeAgent = "";
           this.CommentaireTypeAgent = "";
       }
       #endregion

    }
}