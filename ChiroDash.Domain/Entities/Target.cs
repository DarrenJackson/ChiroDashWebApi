
namespace ChiroDash.Domain.Entities
{
    public class Target
    {
        public int Id { get; set; } 
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

        //public Doctor Doctor { get; set; }
    }

}
