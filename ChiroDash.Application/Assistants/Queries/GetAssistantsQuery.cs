using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ChiroDash.Domain.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace ChiroDash.Application.Assistants.Queries
{
    public class GetAssistantsQuery
    {
        private readonly IConfiguration config;

        public GetAssistantsQuery(IConfiguration config)
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
