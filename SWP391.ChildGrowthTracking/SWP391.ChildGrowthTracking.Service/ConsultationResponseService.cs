using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SWP391.ChildGrowthTracking.Repository.DTO.ConsultationResponseDTO;
using SWP391.ChildGrowthTracking.Repository.Model;

namespace SWP391.ChildGrowthTracking.Repository.Services
{
    public class ConsultationResponseService : IConsultationResponse
    {
        private readonly Swp391ChildGrowthTrackingContext _context;

        public ConsultationResponseService(Swp391ChildGrowthTrackingContext context)
        {
            _context = context;
        }

        // Get all consultation responses
        public async Task<List<ConsultationResponseGetDTO>> GetAllConsultationResponses()
        {
            return await _context.ConsultationResponses
                .Select(cr => new ConsultationResponseGetDTO
                {
                    ResponseId = cr.ResponseId,
                    RequestId = cr.RequestId,
                    DoctorId = cr.DoctorId,
                    ResponseDate = cr.ResponseDate,
                    Content = cr.Content,
                    Attachments = cr.Attachments,
                    Status = cr.Status,
                    Diagnosis = cr.Diagnosis
                })
                .ToListAsync();
        }

        // Get a single consultation response by ID
        public async Task<ConsultationResponseGetDTO?> GetConsultationResponseById(int responseId)
        {
            var response = await _context.ConsultationResponses
                .Include(cr => cr.Request)
                .Include(cr => cr.Doctor)
                .Where(cr => cr.ResponseId == responseId)
                .Select(cr => new ConsultationResponseGetDTO
                {
                    ResponseId = cr.ResponseId,
                    RequestId = cr.RequestId,
                    DoctorId = cr.DoctorId,
                    ResponseDate = cr.ResponseDate,
                    Content = cr.Content,
                    Attachments = cr.Attachments,
                    Status = cr.Status,
                    Diagnosis = cr.Diagnosis
                })
                .FirstOrDefaultAsync();

            return response;
        }


        // Create a new consultation response
        // Create a new consultation response
        public async Task<ConsultationResponseGetDTO> CreateConsultationResponse(CreateConsultationResponseDTO dto)
        {
            var newResponse = new ConsultationResponse
            {
                RequestId = dto.RequestId ?? throw new ArgumentNullException(nameof(dto.RequestId), "RequestId is required."),
                DoctorId = dto.DoctorId ?? throw new ArgumentNullException(nameof(dto.DoctorId), "DoctorId is required."),
                ResponseDate = DateTime.UtcNow,  // Set current date
                Content = dto.Content ?? throw new ArgumentNullException(nameof(dto.Content), "Content is required."),
                Attachments = dto.Attachments,
                Status = true,  // Default status set to "Active"
                Diagnosis = dto.Diagnosis
            };

            _context.ConsultationResponses.Add(newResponse);
            await _context.SaveChangesAsync();

            return await GetConsultationResponseById(newResponse.ResponseId)
                ?? throw new Exception("Error retrieving the newly created consultation response.");
        }



        // Update a consultation response

        public async Task<ConsultationResponseGetDTO?> UpdateConsultationResponse(int responseId, UpdateConsultationResponseDTO dto)
        {
            var response = await _context.ConsultationResponses.FindAsync(responseId);
            if (response == null) return null;

            // Updating fields only if they have been provided (nullable DTO properties)
            response.RequestId = dto.RequestId ?? response.RequestId;
            response.DoctorId = dto.DoctorId ?? response.DoctorId;
            response.ResponseDate = DateTime.UtcNow;  // Always set the current date and time
            response.Content = dto.Content ?? response.Content;
            response.Attachments = dto.Attachments ?? response.Attachments;
            response.Status = dto.Status ?? response.Status;
            response.Diagnosis = dto.Diagnosis ?? response.Diagnosis;

            // Mark the entity as modified
            _context.ConsultationResponses.Update(response);
            await _context.SaveChangesAsync();

            // Retrieve and return the updated response
            return await GetConsultationResponseById(responseId);
        }


        // Delete a consultation response
        public async Task<bool> DeleteConsultationResponse(int responseId)
        {
            var response = await _context.ConsultationResponses.FindAsync(responseId);
            if (response == null) return false;

            _context.ConsultationResponses.Remove(response);
            await _context.SaveChangesAsync();
            return true;
        }

        // Count total consultation responses
        public async Task<int> CountConsultationResponses()
        {
            return await _context.ConsultationResponses.CountAsync();
        }
    }
}
