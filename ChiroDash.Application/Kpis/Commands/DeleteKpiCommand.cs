using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ChiroDash.Domain.Entities;
using DapperExtensions;
using Microsoft.Extensions.Configuration;

namespace ChiroDash.Application.Kpis.Commands
{
    public class DeleteKpiCommand
    {
        private readonly IConfiguration config;

        public DeleteKpiCommand(IConfiguration config)
        {
            this.config = config;
        }

        public IDbConnection Connection
            => new SqlConnection(config.GetConnectionString("ChiroDashConnectionString"));

        public async Task<bool> Execute(Kpi kpi)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        var isDeleted = await conn.DeleteAsync(kpi, trans);
                        if (!isDeleted)
                        {
                            return false;
                        }

                        trans.Commit();
                        return true;
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
