using System;
using System.Collections.Generic;

namespace SWP391.ChildGrowthTracking.Repository.Models;

public partial class Child
{
    public int ChildId { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public double? BirthWeight { get; set; }

    public double? BirthHeight { get; set; }

    public string? BloodType { get; set; }

    public string? Allergies { get; set; }

    public string? Status { get; set; }

    public string? Relationship { get; set; }

    public virtual ICollection<Alert> Alerts { get; set; } = new List<Alert>();

    public virtual ICollection<ConsultationRequest> ConsultationRequests { get; set; } = new List<ConsultationRequest>();

    public virtual Useraccount User { get; set; } = null!;

    public virtual ICollection<GrowthRecord> Records { get; set; } = new List<GrowthRecord>();
}
