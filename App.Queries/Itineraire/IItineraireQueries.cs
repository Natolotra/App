using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Queries.Itineraire
{
    public interface IItineraireQueries
    {
        Task<IEnumerable<ItineraireDto>> GetAllItineraire();
    }
}
