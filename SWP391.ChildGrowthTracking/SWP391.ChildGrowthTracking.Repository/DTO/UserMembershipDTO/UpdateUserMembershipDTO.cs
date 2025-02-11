using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.ChildGrowthTracking.Repository.DTO.UserMembershipDTO
{
    public class UpdateUserMembershipDTO
    {
        public int? PackageId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? SubscriptionStatus { get; set; }
        
    }
}
