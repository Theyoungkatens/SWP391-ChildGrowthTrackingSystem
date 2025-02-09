using System;
using System.Collections.Generic;

namespace SWP391.ChildGrowthTracking.Repository.Models;

public partial class Blog
{
    public int BlogId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Tags { get; set; }

    public string? Image { get; set; }

    public string Status { get; set; } = null!;

    public string? Category { get; set; }
}
