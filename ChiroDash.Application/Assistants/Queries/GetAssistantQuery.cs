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
    public class GetAssistantQuery
    {
        private readonly IConfiguration config;

        public GetAssistantQuery(IConfiguration config)
        {
            this.config = config;
        }

        public IDbConnection Connection
            => new SqlConnection(config.GetConnectionString("ChiroDashConnectionString"));

        public async Task<Assistant> Execute(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();

                try 
                {
                    var sql = @"SELECT * FROM Assistant WHERE id = @Id";
                    var result = await conn.QuerySingleOrDefaultAsync<Assistant>(sql, new { id });
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
