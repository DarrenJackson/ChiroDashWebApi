using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ChiroDash.Domain.Entities;
using Dapper;
using DapperExtensions;
using Microsoft.Extensions.Configuration;

namespace ChiroDash.Application.Doctors.Queries
{
    public class GetDoctorsQuery
    {
        private readonly IConfiguration config;

        public GetDoctorsQuery(IConfiguration config)
        {
            this.config = config;
        }

        public IDbConnection Connection
            => new SqlConnection(config.GetConnectionString("ChiroDashConnectionString"));

        public async Task<IEnumerable<Doctor>> Execute()
        {
            using (var conn = Connection)
            {
                conn.Open();
                var result = await conn.GetListAsync<Doctor>();
                return result;
            }
        }
    }
}
