using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ChiroDash.Application.Targets.Models;
using ChiroDash.Domain.Entities;
using DapperExtensions;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.Extensions.Configuration;

namespace ChiroDash.Application.Targets.Commands
{
    public class AddTargetCommand
    {
        private readonly IConfiguration config;

        public AddTargetCommand(IConfiguration config)
        {
            this.config = config;
        }

        public IDbConnection Connection
            => new SqlConnection(config.GetConnectionString("ChiroDashConnectionString"));

        public async Task<Target> Execute(Target target)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        target.Id = await conn.InsertAsync(target, trans);
                        trans.Commit();
                        return target;
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
