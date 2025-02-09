using System;
using System.Collections.Generic;

namespace SWP391.ChildGrowthTracking.Repository.Models;

public partial class MembershipPackage
{
    public int PackageId { get; set; }

    public string PackageName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int DurationMonths { get; set; }

    public string? Features { get; set; }

    public int? MaxChildrenAllowed { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<UserMembership> UserMemberships { get; set; } = new List<UserMembership>();
}
