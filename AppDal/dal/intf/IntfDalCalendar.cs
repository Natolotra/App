using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace arch.dal.intf
{
    /// <summary>
    /// Interface du service Calendar
    /// </summary>
    public interface IntfDalCalendar
    {
        DataTable getDataTableForCalendar(string idItineraire, string numAgence);
        DataTable getDataTable(string strSQL);

        DataTable getDTCalendarFBADIsNull(string idItineraire, string numAgence);
        DataTable getDTCalendarFBADIsNullSQL(string strSQL);

        DataTable getDTCalendarFBADIsNotNull(string idItineraire, string numAgence);
        DataTable getDTCalendarFBADIsNotNullSQL(string strSQL);
    }
}