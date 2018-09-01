using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.crl;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace arch.dal.intf
{
    /// <summary>
    /// Interface du service lien
    /// </summary>
    public interface IntfDalLien
    {
        crlLien selectLien(string numLien);
        string insertLien(crlLien lien);
        bool updateLien(crlLien lien);
        string isLien(crlLien lien);
        string getNumLien();

        bool insertAssocAgentLien(string matriculeAgent, string numLien);
        bool deleteAssocAgentLien(string matriculeAgent, string numLien);

        bool insertAssocTypeAgentLien(string typeAgent, string numLien);
        bool deleteAssocTypeAgentLien(string typeAgent, string numLien);
        bool deleteAssocTypeAgentLien(string typeAgent);

        bool isPageAgent(string matriculeAgent, string numLien);
        bool isPageTypeAgent(string typeAgent, string numLien);

        List<crlLien> selectAllLien(string matriculeAgent);
        List<crlLien> selectLien(int niveau, string matriculeAgent);
        List<crlLien> selectLien(int niveau, string numLien, string matriculeAgent);

        HtmlGenericControl getLiLien(crlLien lien, string preUrl, string preUrlImage);
        void getMenu(HtmlGenericControl ulLien, string matriculeAgent, string preUrl, string preUrlImage);

        List<crlLien> selectAllLien(string matriculeAgent, string indicateurZone);
        List<crlLien> selectLienM(int niveau, string matriculeAgent, string indicateurZone);
        List<crlLien> selectLienE(int niveau, string numLien, string matriculeAgent, string indicateurZone);
        void getMenu(Panel panLien, string matriculeAgent, string preUrl, string preUrlImage, string indicateurZone);

    }
}