using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.ChildGrowthTracking.Repository.DTO.AlertDTO
{
    public class UpdateAlertDTO
    {
        public int? ChildId { get; set; }
        public string? AlertType { get; set; }
        public DateTime? AlertDate { get; set; }
        public string? Message { get; set; }
        public bool? IsRead { get; set; }
    }
}
