using System;

namespace SWP391.ChildGrowthTracking.Repository.DTO.RatingFeedbackDTO
{
    public class CreateRatingFeedbackDTO
    {
        public int? UserId { get; set; }
        public int? DoctorId { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }
        public string? FeedbackType { get; set; }
    }
}
