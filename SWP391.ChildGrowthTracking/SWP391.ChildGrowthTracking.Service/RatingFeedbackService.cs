using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SWP391.ChildGrowthTracking.Repository.DTO.RatingFeedbackDTO;
using SWP391.ChildGrowthTracking.Repository.Model;

namespace SWP391.ChildGrowthTracking.Repository.Services
{
    public class RatingFeedbackService : IRatingFeedback
    {
        private readonly Swp391ChildGrowthTrackingContext _context;

        public RatingFeedbackService(Swp391ChildGrowthTrackingContext context)
        {
            _context = context;
        }

        // Get all rating feedbacks
        public async Task<List<RatingFeedbackGetDTO>> GetAllRatingFeedbacks()
        {
            return await _context.RatingFeedbacks
                .Include(rf => rf.User) // Including User relationship
                .Include(rf => rf.Doctor) // Including Doctor relationship
                .Select(rf => new RatingFeedbackGetDTO
                {
                    FeedbackId = rf.FeedbackId,
                    UserId = rf.UserId,
                    DoctorId = rf.DoctorId,
                    Rating = rf.Rating,
                    Comment = rf.Comment,
                    FeedbackDate = rf.FeedbackDate,
                    FeedbackType = rf.FeedbackType
                }).ToListAsync();
        }

        // Get a rating feedback by ID
        public async Task<RatingFeedbackGetDTO?> GetRatingFeedbackById(int feedbackId)
        {
            return await _context.RatingFeedbacks
                .Where(rf => rf.FeedbackId == feedbackId)
                .Include(rf => rf.User)
                .Include(rf => rf.Doctor)
                .Select(rf => new RatingFeedbackGetDTO
                {
                    FeedbackId = rf.FeedbackId,
                    UserId = rf.UserId,
                    DoctorId = rf.DoctorId,
                    Rating = rf.Rating,
                    Comment = rf.Comment,
                    FeedbackDate = rf.FeedbackDate,
                    FeedbackType = rf.FeedbackType
                }).FirstOrDefaultAsync();
        }

        // Create a new rating feedback
        public async Task<RatingFeedbackGetDTO> CreateRatingFeedback(CreateRatingFeedbackDTO dto)
        {
            var newFeedback = new RatingFeedback
            {
                UserId = dto.UserId,
                DoctorId = dto.DoctorId,
                Rating = dto.Rating,
                Comment = dto.Comment,
                FeedbackDate = DateTime.UtcNow,  // Set the current UTC date and time
                FeedbackType = dto.FeedbackType
            };

            _context.RatingFeedbacks.Add(newFeedback);
            await _context.SaveChangesAsync();

            return await GetRatingFeedbackById(newFeedback.FeedbackId)
                ?? throw new Exception("Error retrieving the newly created rating feedback.");
        }

        // Update an existing rating feedback
        public async Task<RatingFeedbackGetDTO?> UpdateRatingFeedback(int feedbackId, UpdateRatingFeedbackDTO dto)
        {
            var feedback = await _context.RatingFeedbacks.FindAsync(feedbackId);
            if (feedback == null) return null;

            feedback.UserId = dto.UserId ?? feedback.UserId;
            feedback.DoctorId = dto.DoctorId ?? feedback.DoctorId;
            feedback.Rating = dto.Rating ?? feedback.Rating;
            feedback.Comment = dto.Comment ?? feedback.Comment;

            // Set FeedbackDate to now if not provided in DTO
            feedback.FeedbackDate =  DateTime.UtcNow;

            feedback.FeedbackType = dto.FeedbackType ?? feedback.FeedbackType;

            _context.RatingFeedbacks.Update(feedback);
            await _context.SaveChangesAsync();

            return await GetRatingFeedbackById(feedbackId);
        }

        // Delete a rating feedback
        public async Task<bool> DeleteRatingFeedback(int feedbackId)
        {
            var feedback = await _context.RatingFeedbacks.FindAsync(feedbackId);
            if (feedback == null) return false;

            _context.RatingFeedbacks.Remove(feedback);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
