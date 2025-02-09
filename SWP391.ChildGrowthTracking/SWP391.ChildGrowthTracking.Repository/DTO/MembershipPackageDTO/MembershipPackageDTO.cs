using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.ChildGrowthTracking.Repository.DTO.MembershipPackageDTO
{
    
        public class MembershipPackageDTO
        {
            public int PackageId { get; set; }
            public string? PackageName { get; set; }
            public string? Description { get; set; }
            public decimal? Price { get; set; }
            public int? DurationMonths { get; set; }
            public string? Features { get; set; }
            public int? MaxChildrenAllowed { get; set; }
            public string? Status { get; set; }
        }
    }

