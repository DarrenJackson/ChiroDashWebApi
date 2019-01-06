using System;
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

        public async Task<IEnumerable<Employee>> Execute()
        {
            using (var conn = Connection)
            {
                conn.Open();

                try 
                {
                    var sql = @"SELECT * 
                                FROM Employee AS e 
                                JOIN Department_Employee as de on e.Id = de.EmployeeId
                                WHERE de.DepartmentId = 1";

                    var result = await conn.QueryAsync<Employee>(sql);
                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw; 
                }
            }
        }
    }
}
