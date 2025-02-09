using System;
using System.Collections.Generic;

namespace SWP391.ChildGrowthTracking.Repository.Model;

public partial class ConsultationResponse
{
    public int ResponseId { get; set; }

    public int? RequestId { get; set; }

    public int? DoctorId { get; set; }

    public DateTime? ResponseDate { get; set; }

    public string? Content { get; set; }

    public string? Attachments { get; set; }

    public bool? Status { get; set; }

    public string? Diagnosis { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual ConsultationRequest? Request { get; set; }
}
