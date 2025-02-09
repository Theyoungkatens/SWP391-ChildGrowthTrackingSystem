using System;
using System.Collections.Generic;

namespace SWP391.ChildGrowthTracking.Repository.Models;

public partial class Alert
{
    public int AlertId { get; set; }

    public int? ChildId { get; set; }

    public string? AlertType { get; set; }

    public DateTime? AlertDate { get; set; }

    public string? Message { get; set; }

    public bool? IsRead { get; set; }

    public virtual Child? Child { get; set; }
}
