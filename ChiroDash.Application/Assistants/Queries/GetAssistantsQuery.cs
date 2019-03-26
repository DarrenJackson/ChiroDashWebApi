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

        public async Task<IEnumerable<Assistant>> Execute()
        {
            using (var conn = Connection)
            {
                conn.Open();

                try 
                {
                    var sql = @"SELECT * FROM Assistant";

                    var result = await conn.QueryAsync<Assistant>(sql);
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
