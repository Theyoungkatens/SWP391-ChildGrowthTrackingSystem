using System;
using System.Collections.Generic;

namespace SWP391.ChildGrowthTracking.Repository.Model;

public partial class GrowthRecord
{
    public int RecordId { get; set; }

    public DateTime? Month { get; set; }

    public double? Weight { get; set; }

    public double? Height { get; set; }

    public double? Bmi { get; set; }

    public double? HeadCircumference { get; set; }

    public double? UpperArmCircumference { get; set; }

    public bool? RecordedByUser { get; set; }

    public string? Notes { get; set; }

    public int? Old { get; set; }

    public virtual ICollection<Child> Children { get; set; } = new List<Child>();
    
}
