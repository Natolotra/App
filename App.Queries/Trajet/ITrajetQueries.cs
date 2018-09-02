using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Queries.Trajet
{
    public interface ITrajetQueries
    {
        Task<IEnumerable<TrajetDto>> GetAllTrajet();

        Task<IEnumerable<TrajetDto>> GetListTrajetByIdItineraire(string idIt);
    }
}
