using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ChiroDash.Domain.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace ChiroDash.Application.Scorecards.Queries
{
    public class GetScorecardQuery
    {
        private readonly IConfiguration config;

        public GetScorecardQuery(IConfiguration config)
        {
            this.config = config;
        }

        public IDbConnection Connection
            => new SqlConnection(config.GetConnectionString("ChiroDashConnectionString"));

        public async Task<Scorecard> Execute(string doctorId, string id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                    var sql = @"SELECT * FROM Scorecard AS S 
                                WHERE S.Id = @ID
                                AND S.DoctorId = @DoctorID";

                    var scorecard = await conn.QuerySingleOrDefaultAsync<Scorecard>(sql, new { ID = id, DoctorID = doctorId });
                    return scorecard;
                //var sql = @"SELECT * FROM Scorecard AS S 
                //            INNER JOIN Doctor AS D ON S.DoctorId = D.Id WHERE S.Id = @ID";
                //var result = await conn.QueryAsync<Scorecard, Doctor, Scorecard>(
                //    sql, 
                //    (card, doctor) =>
                //    {
                //        //card.Doctor = doctor;
                //        return card;
                //    }, new { ID = id});

                //return result.FirstOrDefault();
            }
        }
    }
}
