using System;
using System.Collections.Generic;
using System.Text;

namespace ChiroDash.Application.Scorecards.Models
{
    public class ScorecardToCreateDto
    {
        public DateTimeOffset DateTime { get; set; }
        public string AssistantId { get; set; }
        public string PatientName { get; set; }
        public bool SpousePresent { get; set; }
        public int AdjustmentsOnPlan { get; set; }
        public bool PrePayed { get; set; }
        public int PreBooked { get; set; }
        public bool DidBeginCare { get; set; }
        public bool WorkshopBooked { get; set; }
        public bool WorkshopAttended { get; set; }
        public bool DocDidRof { get; set; }
        public bool MindfitBooked { get; set; }
    }
}
