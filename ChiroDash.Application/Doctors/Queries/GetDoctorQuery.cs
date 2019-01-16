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

        public async Task<Employee> Execute(string id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                var sql = @"SELECT * 
                            FROM Employee AS e 
                            JOIN Department_Employee as de on e.Id = de.EmployeeId
                            WHERE de.DepartmentId = 0
                            AND e.Id = @Id";

                var result = await conn.QuerySingleOrDefaultAsync<Employee>(sql, new { id });
                return result;
            }
        }
    }
}
