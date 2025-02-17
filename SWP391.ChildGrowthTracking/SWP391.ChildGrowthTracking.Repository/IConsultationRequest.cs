using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SWP391.ChildGrowthTracking.Repository.DTO.ConsultationRequestDTO;

namespace SWP391.ChildGrowthTracking.Repository.Services
{
    public interface IConsultationRequest
    {
        // Get all consultation requests
        Task<List<ConsultationRequestGetDTO>> GetAllConsultationRequests();

        // Get a single consultation request by ID
        Task<ConsultationRequestGetDTO?> GetConsultationRequestById(int requestId);

        // Create a new consultation request
        Task<ConsultationRequestGetDTO> CreateConsultationRequest(CreateConsultationRequestDTO dto);

        // Update an existing consultation request
        Task<ConsultationRequestGetDTO?> UpdateConsultationRequest(int requestId, UpdateConsultationRequestDTO dto);

        // Delete a consultation request
        Task<bool> DeleteConsultationRequest(int requestId);
        Task<int> CountConsultationRequests();
    }
}
