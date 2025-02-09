using System;
using System.Collections.Generic;

namespace SWP391.ChildGrowthTracking.Repository.Model;

public partial class Blog
{
    public int BlogId { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Tags { get; set; }

    public string? Image { get; set; }

    public string? Status { get; set; }

    public string? Category { get; set; }
}
