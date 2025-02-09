using System;
using System.Collections.Generic;

namespace SWP391.ChildGrowthTracking.Repository.Model;

public partial class Doctor
{
    public int DoctorId { get; set; }

    public string? Name { get; set; }

    public string? Specialization { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Degree { get; set; }

    public string? Hospital { get; set; }

    public string? LicenseNumber { get; set; }

    public string? Biography { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<ConsultationResponse> ConsultationResponses { get; set; } = new List<ConsultationResponse>();

    public virtual ICollection<RatingFeedback> RatingFeedbacks { get; set; } = new List<RatingFeedback>();

    public virtual Useraccount? User { get; set; }
}
