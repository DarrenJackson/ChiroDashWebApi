using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ChiroDash.Domain.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace ChiroDash.Application.Kpis.Queries
{
    public class GetKpiQuery
    {
        private readonly IConfiguration config;

        public GetKpiQuery(IConfiguration config)
        {
            this.config = config;
        }

        public IDbConnection Connection
            => new SqlConnection(config.GetConnectionString("ChiroDashConnectionString"));

        public async Task<Kpi> Execute(string doctorId, string id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                    var sql = @"SELECT * FROM Kpi AS S 
                                WHERE S.Id = @ID
                                AND S.DoctorId = @DoctorID";

                    var kpi = await conn.QuerySingleOrDefaultAsync<Kpi>(sql, new { ID = id, DoctorID = doctorId });
                    return kpi;
                //var sql = @"SELECT * FROM Kpi AS S 
                //            INNER JOIN Employee AS D ON S.DoctorId = D.Id WHERE S.Id = @ID";
                //var result = await conn.QueryAsync<Kpi, Employee, Kpi>(
                //    sql, 
                //    (card, doctor) =>
                //    {
                //        //card.Employee = doctor;
                //        return card;
                //    }, new { ID = id});

                //return result.FirstOrDefault();
            }
        }
    }
}
