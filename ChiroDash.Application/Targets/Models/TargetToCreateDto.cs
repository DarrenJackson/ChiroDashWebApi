using System;
using System.Collections.Generic;
using System.Text;

namespace ChiroDash.Application.Targets.Models
{
    public class TargetToCreateDto
    {
        public string DoctorId { get; set; }
        public float AdjustmentsOnPlan { get; set; } = 0f;
        public float SpousePresent { get; set; } = 0f; 
        public float PrePayed { get; set; } = 0f;
        public float PreBooked { get; set; } = 0f;
        public float DidBeginCare { get; set; } = 0f;
        public float WorkshopBooked { get; set; } = 0f;
        public float WorkshopAttended { get; set; } = 0f;
        public float DocDidRof { get; set; } = 0f;
        public float MindfitBooked { get; set; } = 0f;
    }
}
