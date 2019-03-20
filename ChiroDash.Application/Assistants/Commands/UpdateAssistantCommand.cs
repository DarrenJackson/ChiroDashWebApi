using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ChiroDash.Domain.Entities;
using DapperExtensions;
using Microsoft.Extensions.Configuration;

namespace ChiroDash.Application.Assistants.Commands
{
    public class UpdateAssistantCommand
    {
        private readonly IConfiguration config;

        public UpdateAssistantCommand(IConfiguration config)
        {
            this.config = config;
        }

        public IDbConnection Connection
            => new SqlConnection(config.GetConnectionString("ChiroDashConnectionString"));

        public async Task<bool> Execute(Assistant assistant)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        await conn.UpdateAsync(assistant, trans);
                        trans.Commit();
                        return true;
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
}
