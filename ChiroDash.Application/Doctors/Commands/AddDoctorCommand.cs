using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ChiroDash.Domain.Entities;
using Dapper;
using DapperExtensions;
using Microsoft.Extensions.Configuration;

namespace ChiroDash.Application.Doctors.Commands
{
    public class AddDoctorCommand
    {
        private readonly IConfiguration config;

        public AddDoctorCommand(IConfiguration config)
        {
            this.config = config;
        }

        public IDbConnection Connection
            => new SqlConnection(config.GetConnectionString("ChiroDashConnectionString"));

        public async Task<(Doctor, Target)> Execute(Doctor doctor)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        // Add DOCTOR
                        var doctorId = await conn.InsertAsync(doctor, trans);
                        doctor.Id = doctorId;

                        // Add TARGET 
                        var target = new Target
                        {
                            DoctorId = doctorId.ToString()
                        };
                        await conn.InsertAsync(target, trans);


                        trans.Commit();
                        return (doctor, target);
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
