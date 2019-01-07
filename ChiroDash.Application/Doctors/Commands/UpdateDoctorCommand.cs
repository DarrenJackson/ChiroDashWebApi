using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ChiroDash.Domain.Entities;
using DapperExtensions;
using Microsoft.Extensions.Configuration;

namespace ChiroDash.Application.Doctors.Commands
{
    public class UpdateDoctorCommand
    {
        private readonly IConfiguration config;

        public UpdateDoctorCommand(IConfiguration config)
        {
            this.config = config;
        }

        public IDbConnection Connection
            => new SqlConnection(config.GetConnectionString("ChiroDashConnectionString"));

        public async Task<bool> Execute(Employee doctor)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        await conn.UpdateAsync(doctor, trans);
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
