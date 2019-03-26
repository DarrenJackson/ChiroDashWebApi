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
    public class AddAssistantCommand
    {
        private readonly IConfiguration config;

        public AddAssistantCommand(IConfiguration config)
        {
            this.config = config;
        }

        public IDbConnection Connection
            => new SqlConnection(config.GetConnectionString("ChiroDashConnectionString"));

        public async Task<Assistant> Execute(Assistant assistant)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        // Add ASSISTANT
                        var doctorId = await conn.InsertAsync(assistant, trans);
                        assistant.Id = doctorId;

                        trans.Commit();
                        return assistant;
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
