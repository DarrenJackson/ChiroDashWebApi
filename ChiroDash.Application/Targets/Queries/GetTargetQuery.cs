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

namespace ChiroDash.Application.Targets.Queries
{
    public class GetTargetQuery
    {
        private readonly IConfiguration config;

        public GetTargetQuery(IConfiguration config)
        {
            this.config = config;
        }

        public IDbConnection Connection
            => new SqlConnection(config.GetConnectionString("ChiroDashConnectionString"));

        public async Task<Target> Execute(string doctorId, string id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                try
                {
                    var sql = @"SELECT * FROM Target AS T 
                                WHERE T.Id = @ID
                                AND T.DoctorId = @DoctorID";

                    var target = await conn.QuerySingleOrDefaultAsync<Target>(sql, new { ID = id, DoctorID = doctorId });
                    return target;
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
