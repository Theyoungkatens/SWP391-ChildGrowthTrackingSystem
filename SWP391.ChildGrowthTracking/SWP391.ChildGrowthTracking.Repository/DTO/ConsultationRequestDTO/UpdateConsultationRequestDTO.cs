using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.ChildGrowthTracking.Repository.DTO.ConsultationRequestDTO
{
    public class UpdateConsultationRequestDTO
    {
        public int? UserId { get; set; }
        public int? ChildId { get; set; }
        public DateTime? RequestDate { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? Urgency { get; set; }
        public string? Attachments { get; set; }
        public string? Category { get; set; }
    }
}
