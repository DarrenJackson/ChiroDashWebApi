using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ChiroDash.Application.Targets.Models;
using ChiroDash.Domain.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace ChiroDash.Application.Targets.Queries
{
    public class GetDoctorTargetsQuery
    {
        private readonly IConfiguration config;

        public GetDoctorTargetsQuery(IConfiguration config)
        {
            this.config = config;
        }

        public IDbConnection Connection
            => new SqlConnection(config.GetConnectionString("ChiroDashConnectionString"));

        public async Task<IEnumerable<Target>> Execute(int doctorId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        var sql = $"SELECT * FROM Target WHERE DoctorId = @ID";
                        var result = await conn.QueryAsync<Target>(sql, new { ID = doctorId }, trans);

                        trans.Commit();

                        return result;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
