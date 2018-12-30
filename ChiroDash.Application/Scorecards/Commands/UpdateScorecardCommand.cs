using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ChiroDash.Domain.Entities;
using Dapper;
using DapperExtensions;
using Microsoft.Extensions.Configuration;

namespace ChiroDash.Application.Scorecards.Commands
{
    public class UpdateScorecardCommand
    {
        private readonly IConfiguration config;

        public UpdateScorecardCommand(IConfiguration config)
        {
            this.config = config;
        }

        public IDbConnection Connection
            => new SqlConnection(config.GetConnectionString("ChiroDashConnectionString"));

        public async Task<bool> Execute(Scorecard scorecard)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        await conn.UpdateAsync(scorecard, trans);
                        trans.Commit();
                        return true;
                        // Can we use the Scorecard entity and somehow map Doctor

                        //var sql = @"UPDATE Scorecard SET 
                        //              PatientName = @PatientName,
                        //              AdjustmentsOnPlan = @AdjustmentsOnPlan, 
                        //              SpousePresent = @SpousePresent,
                        //              PrePayed = @PrePayed,
                        //              PreBooked = @PreBooked,
                        //              DidBeginCare = @DidBeginCare,
                        //              WorkshopBooked = @WorkshopBooked,
                        //              WorkshopAttended = @WorkshopAttended,
                        //              DocDidRof = @DocDidRof,
                        //              MindfitBooked = @MindfitBooked 
                        //          WHERE Id = @ID";


                        //var rows = await conn.ExecuteAsync(sql, new
                        //{
                        //    ID = id,
                        //    scorecard.PatientName,
                        //    scorecard.AdjustmentsOnPlan,
                        //    scorecard.DidBeginCare,
                        //    scorecard.DocDidRof,
                        //    scorecard.MindfitBooked,
                        //    scorecard.PreBooked,
                        //    scorecard.PrePayed,
                        //    scorecard.SpousePresent,
                        //    scorecard.WorkshopAttended,
                        //    scorecard.WorkshopBooked
                        //}, trans);

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
