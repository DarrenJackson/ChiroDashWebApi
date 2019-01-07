using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ChiroDash.Domain.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace ChiroDash.Application.Targets.Queries
{
    public class GetTargetsQuery
    {
        private readonly IConfiguration config;

        public GetTargetsQuery(IConfiguration config)
        {
            this.config = config;
        }

        public IDbConnection Connection
            => new SqlConnection(config.GetConnectionString("ChiroDashConnectionString"));

        public async Task<List<Target>> Execute()
        {
            using (var conn = Connection)
            {
                conn.Open();
                var sql = @"SELECT * FROM Target AS T 
                            INNER JOIN Employee AS D ON T.DoctorId = D.Id";
                var targets = await conn.QueryAsync<Target, Employee, Target>(
                    sql,
                    (target, doctor) => target);

                return targets.ToList();
            }
        }
    }
}
