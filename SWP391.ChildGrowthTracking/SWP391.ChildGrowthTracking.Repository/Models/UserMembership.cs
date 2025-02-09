using System;
using System.Collections.Generic;

namespace SWP391.ChildGrowthTracking.Repository.Models;

public partial class UserMembership
{
    public int Membershipid { get; set; }

    public int UserId { get; set; }

    public int PackageId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string? SubscriptionStatus { get; set; }

    public string? CouponCode { get; set; }

    public virtual MembershipPackage Package { get; set; } = null!;

    public virtual Payment? Payment { get; set; }

    public virtual Useraccount User { get; set; } = null!;
}
