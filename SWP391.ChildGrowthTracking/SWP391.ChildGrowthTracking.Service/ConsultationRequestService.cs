using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SWP391.ChildGrowthTracking.Repository.DTO.ConsultationRequestDTO;
using SWP391.ChildGrowthTracking.Repository.Model;

namespace SWP391.ChildGrowthTracking.Repository.Services
{
    public class ConsultationRequestService : IConsultationRequest
    {
        private readonly Swp391ChildGrowthTrackingContext _context;

        public ConsultationRequestService(Swp391ChildGrowthTrackingContext context)
        {
            _context = context;
        }

        // Get all consultation requests
        public async Task<List<ConsultationRequestGetDTO>> GetAllConsultationRequests()
        {
            return await _context.ConsultationRequests
                
                .Select(cr => new ConsultationRequestGetDTO
                {
                    RequestId = cr.RequestId,
                   
                    ChildId = cr.ChildId,
                    RequestDate = cr.RequestDate,
                    Description = cr.Description,
                    Status = cr.Status,
                    Urgency = cr.Urgency,
                    Attachments = cr.Attachments,
                    Category = cr.Category,
                    })
                .ToListAsync();
        }

        // Get a single consultation request by ID
        public async Task<ConsultationRequestGetDTO?> GetConsultationRequestById(int requestId)
        {
            var request = await _context.ConsultationRequests
                .Include(cr => cr.Child)
                .Include(cr => cr.User)
                .Where(cr => cr.RequestId == requestId)
                .Select(cr => new ConsultationRequestGetDTO
                {
                    RequestId = cr.RequestId,
                    UserId = cr.UserId,
                    ChildId = cr.ChildId,
                    RequestDate = cr.RequestDate,
                    Description = cr.Description,
                    Status = cr.Status,
                    Urgency = cr.Urgency,
                    Attachments = cr.Attachments,
                    Category = cr.Category,
                    
                })
                .FirstOrDefaultAsync();

            return request;
        }

        // Create a new consultation request
        public async Task<ConsultationRequestGetDTO> CreateConsultationRequest(CreateConsultationRequestDTO dto)
        {
            var newRequest = new ConsultationRequest
            {
                UserId = dto.UserId,
                ChildId = dto.ChildId,
                RequestDate = DateTime.UtcNow,  // Set current date
                Description = dto.Description,
                Status = "Active",  // Default status set to "Active"
                Urgency = dto.Urgency,
                Attachments = dto.Attachments,
                Category = dto.Category
            };

            _context.ConsultationRequests.Add(newRequest);
            await _context.SaveChangesAsync();

            return await GetConsultationRequestById(newRequest.RequestId)
                ?? throw new Exception("Error retrieving the newly created consultation request.");
        }

        // Update a consultation request
        public async Task<ConsultationRequestGetDTO?> UpdateConsultationRequest(int requestId, UpdateConsultationRequestDTO dto)
        {
            var request = await _context.ConsultationRequests.FindAsync(requestId);
            if (request == null) return null;

            request.UserId = dto.UserId ?? request.UserId;
            request.ChildId = dto.ChildId ?? request.ChildId;
            request.RequestDate = dto.RequestDate ?? request.RequestDate;
            request.Description = dto.Description ?? request.Description;
            request.Status = dto.Status ?? request.Status;
            request.Urgency = dto.Urgency ?? request.Urgency;
            request.Attachments = dto.Attachments ?? request.Attachments;
            request.Category = dto.Category ?? request.Category;

            _context.ConsultationRequests.Update(request);
            await _context.SaveChangesAsync();

            return await GetConsultationRequestById(requestId);
        }

        // Delete a consultation request
        public async Task<bool> DeleteConsultationRequest(int requestId)
        {
            var request = await _context.ConsultationRequests.FindAsync(requestId);
            if (request == null) return false;

            _context.ConsultationRequests.Remove(request);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<int> CountConsultationRequests()
        {
            return await _context.ConsultationRequests.CountAsync();
        }
    }
}
