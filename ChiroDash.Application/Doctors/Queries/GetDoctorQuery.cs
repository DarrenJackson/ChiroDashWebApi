using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ChiroDash.Domain.Entities;
using Dapper;
using DapperExtensions;
using Microsoft.Extensions.Configuration;

namespace ChiroDash.Application.Doctors.Queries
{
    public class GetDoctorQuery
    {
        private readonly IConfiguration config;

        public GetDoctorQuery(IConfiguration config)
        {
            this.config = config;
        }

        public IDbConnection Connection
            => new SqlConnection(config.GetConnectionString("ChiroDashConnectionString"));

        public async Task<Doctor> Execute(string id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                var result = await conn.GetAsync<Doctor>(id);
                return result;
            }
        }
    }
}
