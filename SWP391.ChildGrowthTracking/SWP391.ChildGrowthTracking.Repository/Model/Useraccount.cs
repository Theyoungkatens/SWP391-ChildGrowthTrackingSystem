using System;
using System.Collections.Generic;

namespace SWP391.ChildGrowthTracking.Repository.Model;

public partial class Useraccount
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public DateTime? LastLogin { get; set; }

    public string? ProfilePicture { get; set; }

    public string? Address { get; set; }

    public string? Status { get; set; }

    public int? Role { get; set; }

    public virtual ICollection<Child> Children { get; set; } = new List<Child>();

    public virtual ICollection<ConsultationRequest> ConsultationRequests { get; set; } = new List<ConsultationRequest>();

    public virtual Doctor? Doctor { get; set; }

    public virtual ICollection<RatingFeedback> RatingFeedbacks { get; set; } = new List<RatingFeedback>();

    public virtual ICollection<UserMembership> UserMemberships { get; set; } = new List<UserMembership>();
}
