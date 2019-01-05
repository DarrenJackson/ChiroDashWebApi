using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace ChiroDashWebApi.Doctors
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly IConfiguration config;

        public DoctorRepository(IConfiguration config)
        {
            this.config = config;
        }

        public IDbConnection Connection 
            => new SqlConnection(config.GetConnectionString("ChiroDashConnectionString"));

        public async Task<Doctor> GetById(int id)
        {
            using (var conn = Connection)
            {
                string sQuery = "SELECT Id, Name FROM Doctor WHERE Id = @ID";
                conn.Open();
                var result = await conn.QueryAsync<Doctor>(sQuery, new { Id = id });
                return result.FirstOrDefault();
            }
        }
    }
}
