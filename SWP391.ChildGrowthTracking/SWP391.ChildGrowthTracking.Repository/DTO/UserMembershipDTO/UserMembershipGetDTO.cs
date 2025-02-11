

namespace SWP391.ChildGrowthTracking.Repository.DTO
{
    public class UserMembershipGetDTO
    {
        public int Membershipid { get; set; }
        public int? UserId { get; set; }
        public int? PackageId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? SubscriptionStatus { get; set; }
        public string? CouponCode { get; set; }
    }
}