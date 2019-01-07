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

        public async Task<(Employee, Target)> Execute(Employee doctor)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        // Add EMPLOYEE
                        var doctorId = await conn.InsertAsync(doctor, trans);
                        doctor.Id = doctorId;

                        // Add TARGET 
                        var target = new Target
                        {
                            DoctorId = doctorId.ToString()
                        };
                        await conn.InsertAsync(target, trans);

                        // Add DEPARTMENT_EMPLOYEE 
                        var sql = @"INSERT INTO DEPARTMENT_EMPLOYEE 
                                    SELECT @doctorId, Id 
                                    FROM Department 
                                    WHERE Name = 'Doctor'";
                        await conn.ExecuteAsync(sql, new { doctorId }, trans);


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
