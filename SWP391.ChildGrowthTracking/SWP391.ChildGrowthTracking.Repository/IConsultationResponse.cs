using SWP391.ChildGrowthTracking.Repository.DTO.ConsultationResponseDTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWP391.ChildGrowthTracking.Repository
{
    public interface IConsultationResponse
    {
        // Get all consultation responses
        Task<List<ConsultationResponseGetDTO>> GetAllConsultationResponses();

        // Get a single consultation response by ID
        Task<ConsultationResponseGetDTO?> GetConsultationResponseById(int responseId);

        // Create a new consultation response
        Task<ConsultationResponseGetDTO> CreateConsultationResponse(CreateConsultationResponseDTO dto);

        // Update a consultation response
        Task<ConsultationResponseGetDTO?> UpdateConsultationResponse(int responseId, UpdateConsultationResponseDTO dto);

        // Delete a consultation response
        Task<bool> DeleteConsultationResponse(int responseId);

        // Count total consultation responses
        Task<int> CountConsultationResponses();
    }
}
