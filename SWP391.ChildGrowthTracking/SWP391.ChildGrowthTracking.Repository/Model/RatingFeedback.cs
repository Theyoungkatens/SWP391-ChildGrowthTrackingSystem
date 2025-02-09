using System;
using System.Collections.Generic;

namespace SWP391.ChildGrowthTracking.Repository.Model;

public partial class RatingFeedback
{
    public int FeedbackId { get; set; }

    public int? UserId { get; set; }

    public int? DoctorId { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? FeedbackDate { get; set; }

    public string? FeedbackType { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual Useraccount? User { get; set; }
}
