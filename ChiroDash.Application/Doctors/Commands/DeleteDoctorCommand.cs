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
    public class DeleteDoctorCommand
    {
        private readonly IConfiguration config;

        public DeleteDoctorCommand(IConfiguration config)
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
                        var isDeleted = conn.Delete(doctor, trans);
                        //var sql = "DELETE FROM Doctor WHERE ID = @Id";
                        //var rowCount = await conn.ExecuteAsync(sql, new { id }, trans);
                        if (!isDeleted)
                        {
                            return false;
                        }

                        var sql = "DELETE FROM Target WHERE DoctorId = @Id";
                        await conn.ExecuteAsync(sql, new { doctor.Id }, trans);

                        sql = "DELETE FROM Department_Employee WHERE EmployeeId = @Id";
                        await conn.ExecuteAsync(sql, new { doctor.Id }, trans);
                        
                        sql = "DELETE FROM Scorecard WHERE DoctorId = @Id";
                        await conn.ExecuteAsync(sql, new { doctor.Id }, trans);

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
