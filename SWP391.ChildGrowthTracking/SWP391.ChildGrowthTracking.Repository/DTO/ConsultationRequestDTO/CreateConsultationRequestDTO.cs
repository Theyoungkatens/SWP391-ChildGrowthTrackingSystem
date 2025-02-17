namespace SWP391.ChildGrowthTracking.Repository.DTO.ConsultationRequestDTO
{
    public class CreateConsultationRequestDTO
    {
        public int? UserId { get; set; }
        public int? ChildId { get; set; }
        
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? Urgency { get; set; }
        public string? Attachments { get; set; }
        public string? Category { get; set; }
    }
}
