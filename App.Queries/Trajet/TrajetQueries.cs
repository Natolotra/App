using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace App.Queries.Trajet
{
    public class TrajetQueries : ITrajetQueries
    {
        private readonly string _connectionString;

        public TrajetQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<IEnumerable<TrajetDto>> GetAllTrajet()
        {
            IEnumerable<TrajetDto> trajets = null;
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                trajets = await connection.QueryAsync<TrajetDto>(Constants.AppPs_Trajet_GetAllTrajet, null, commandType: CommandType.StoredProcedure);
            }
            return trajets;
        }
    }
}
