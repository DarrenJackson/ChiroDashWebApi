using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ChiroDash.Domain.Entities;
using Dapper;
using DapperExtensions;
using Microsoft.Extensions.Configuration;

namespace ChiroDash.Application.Scorecards.Queries
{
    public class GetScorecardsQuery
    {
        private readonly IConfiguration config;

        public GetScorecardsQuery(IConfiguration config)
        {
            this.config = config;
        }

        public IDbConnection Connection
            => new SqlConnection(config.GetConnectionString("ChiroDashConnectionString"));

        public async Task<IEnumerable<Scorecard>> Execute(string doctorId)
        {
            using (var conn = Connection)
            {
                conn.Open();

                var sql = $"SELECT * FROM Scorecard WHERE DoctorId = @ID";
                var result = await conn.QueryAsync<Scorecard>(sql, new { ID = doctorId });
                return result;
            }
        }
    }
}
