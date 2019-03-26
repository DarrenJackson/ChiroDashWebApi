using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ChiroDash.Domain.Entities;
using DapperExtensions;
using Microsoft.Extensions.Configuration;

namespace ChiroDash.Application.Kpis.Commands
{
    public class AddKpiCommand
    {
        private readonly IConfiguration config;

        public AddKpiCommand(IConfiguration config)
        {
            this.config = config;
        }

        public IDbConnection Connection
            => new SqlConnection(config.GetConnectionString("ChiroDashConnectionString"));

        public async Task<Kpi> Execute(Kpi kpi)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        kpi.Id = await conn.InsertAsync(kpi, trans);
                        trans.Commit();
                        return kpi;
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
