using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.crl;

namespace arch.dal.intf
{
    /// <summary>
    /// Interface du service IntfTypeProprietaire
    /// </summary>
    public interface IntfDalTypeProprietaire
    {
        crlTypeProprietaire selectTypeProprietaire(string typeProprietaire);
    }
}