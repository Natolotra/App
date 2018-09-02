using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Queries.Itineraire
{
    public class ItineraireQueries : IItineraireQueries
    {
        private readonly string _connectionString;

        public ItineraireQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<IEnumerable<ItineraireDto>> GetAllItineraire()
        {
            IEnumerable<ItineraireDto> itineraires = null;
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                itineraires = await connection.QueryAsync<ItineraireDto>(Constants.AppPs_Itineraire_GetAllItineraire, null, commandType: CommandType.StoredProcedure);
            }
            return itineraires;
        }
    }
}
