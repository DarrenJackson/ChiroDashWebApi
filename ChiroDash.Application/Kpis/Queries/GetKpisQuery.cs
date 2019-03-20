using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading.Tasks;
using ChiroDash.Domain.Entities;
using ChiroDash.Domain.Helpers;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace ChiroDash.Application.Kpis.Queries
{
    public class GetKpisQuery
    {
        private readonly IConfiguration config;

        public GetKpisQuery(IConfiguration config)
        {
            this.config = config;
        }

        public IDbConnection Connection
            => new SqlConnection(config.GetConnectionString("ChiroDashConnectionString"));

        public async Task<IEnumerable<Kpi>> Execute(string doctorId, KpiResourceParameters resourceParams)
        {
            using (var conn = Connection)
            {
                conn.Open();

                if (!string.IsNullOrEmpty(resourceParams.Month) && !string.IsNullOrEmpty(resourceParams.Year))
                {
                    var month = DateTime.ParseExact(resourceParams.Month, "MMMM", CultureInfo.CurrentCulture).Month;
                    var year = resourceParams.Year;
                    var sql = @"SELECT * FROM Kpi WHERE DoctorId = @ID AND MONTH(DateTime) = @Month AND YEAR(DateTime) = @Year";
                    var result = await conn.QueryAsync<Kpi>(sql, new { ID = doctorId, Month = month, Year = year });
                    return result;
                }
                else
                {
                    var sql = @"SELECT * FROM Kpi WHERE DoctorId = @ID";
                    var result = await conn.QueryAsync<Kpi>(sql, new { ID = doctorId });
                    return result;
                }
            }
        }
    }
}