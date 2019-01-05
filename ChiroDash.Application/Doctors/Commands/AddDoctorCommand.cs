using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ChiroDash.Application.Doctors.Models;
using ChiroDash.Domain.Entities;
using Dapper;
using DapperExtensions;
using DapperExtensions.Mapper;
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

        public async Task<Doctor> Execute(Doctor doctor)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        var id = await conn.InsertAsync(doctor, trans);
                        doctor.Id = id;
                        trans.Commit();
                        return doctor;
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
