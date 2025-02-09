using System;
using System.Collections.Generic;

namespace SWP391.ChildGrowthTracking.Repository.Model;

public partial class ConsultationRequest
{
    public int RequestId { get; set; }

    public int? UserId { get; set; }

    public int? ChildId { get; set; }

    public DateTime? RequestDate { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }

    public string? Urgency { get; set; }

    public string? Attachments { get; set; }

    public string? Category { get; set; }

    public virtual Child? Child { get; set; }

    public virtual ICollection<ConsultationResponse> ConsultationResponses { get; set; } = new List<ConsultationResponse>();

    public virtual Useraccount? User { get; set; }
}
