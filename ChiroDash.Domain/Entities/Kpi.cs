﻿using System;

namespace ChiroDash.Domain.Entities
{
    public class Kpi
    {
        public Kpi() => DateTime = DateTimeOffset.UtcNow;

        public int Id { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public string AssistantId { get; set; }
        public string DoctorId { get; set; }
        public string PatientName { get; set; }
        public bool SpousePresent { get; set; }
        public int AdjustmentsOnPlan { get; set; }
        public bool PrePaid { get; set; }
        public int PreBooked { get; set; }
        public bool DidBeginCare { get; set; }
        public bool WorkshopBooked { get; set; }
        public bool WorkshopAttended { get; set; }
        public bool DocDidRof { get; set; }
        public bool MindfitBooked { get; set; }
    }
}
