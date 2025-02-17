using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SWP391.ChildGrowthTracking.Repository.DTO.RatingFeedbackDTO;
using SWP391.ChildGrowthTracking.Repository.Model;

namespace SWP391.ChildGrowthTracking.Repository
{
    public interface IRatingFeedback
    {
        // Get all rating feedback
        Task<List<RatingFeedbackGetDTO>> GetAllRatingFeedbacks();

        // Get a rating feedback by its ID
        Task<RatingFeedbackGetDTO?> GetRatingFeedbackById(int feedbackId);

        // Create a new rating feedback
        Task<RatingFeedbackGetDTO> CreateRatingFeedback(CreateRatingFeedbackDTO dto);

        // Update an existing rating feedback
        Task<RatingFeedbackGetDTO?> UpdateRatingFeedback(int feedbackId, UpdateRatingFeedbackDTO dto);

        // Delete a rating feedback by its ID
        Task<bool> DeleteRatingFeedback(int feedbackId);
    }
}
