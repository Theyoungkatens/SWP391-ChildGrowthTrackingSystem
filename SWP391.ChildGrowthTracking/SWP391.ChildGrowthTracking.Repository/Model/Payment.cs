using System;
using System.Collections.Generic;

namespace SWP391.ChildGrowthTracking.Repository.Model;

public partial class Payment
{
    public int PaymentId { get; set; }

    public DateTime? PaymentDate { get; set; }

    public decimal? PaymentAmount { get; set; }

    public string? TransactionId { get; set; }

    public string? Status { get; set; }

    public int? Membershipid { get; set; }

    public virtual UserMembership? Membership { get; set; }
}
