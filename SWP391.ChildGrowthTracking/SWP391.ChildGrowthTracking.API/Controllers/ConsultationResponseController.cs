using Microsoft.AspNetCore.Mvc;
using SWP391.ChildGrowthTracking.Repository;
using SWP391.ChildGrowthTracking.Repository.DTO.ConsultationResponseDTO;
using System;
using System.Threading.Tasks;

namespace SWP391.ChildGrowthTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultationResponseController : ControllerBase
    {
        private readonly IConsultationResponse _consultationResponseService;

        public ConsultationResponseController(IConsultationResponse consultationResponseService)
        {
            _consultationResponseService = consultationResponseService;
        }

        // Get all consultation responses
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllConsultationResponses()
        {
            try
            {
                var responses = await _consultationResponseService.GetAllConsultationResponses();
                return Ok(new { success = true, data = responses });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // Get a consultation response by ID
        [HttpGet("{responseId}")]
        public async Task<IActionResult> GetConsultationResponseById(int responseId)
        {
            try
            {
                var response = await _consultationResponseService.GetConsultationResponseById(responseId);
                if (response == null)
                {
                    return NotFound(new { success = false, message = "Consultation response not found." });
                }
                return Ok(new { success = true, data = response });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error retrieving response", details = ex.Message });
            }
        }

        // Create a new consultation response
        [HttpPost("create")]
        public async Task<IActionResult> CreateConsultationResponse([FromBody] CreateConsultationResponseDTO request)
        {
            try
            {
                var response = await _consultationResponseService.CreateConsultationResponse(request);
                return Ok(new { success = true, message = "Consultation response created successfully.", data = response });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // Update an existing consultation response
        [HttpPut("update/{responseId}")]
        public async Task<IActionResult> UpdateConsultationResponse(int responseId, [FromBody] UpdateConsultationResponseDTO request)
        {
            try
            {
                var updatedResponse = await _consultationResponseService.UpdateConsultationResponse(responseId, request);
                if (updatedResponse == null)
                {
                    return NotFound(new { success = false, message = "Consultation response not found." });
                }
                return Ok(new { success = true, message = "Consultation response updated successfully.", data = updatedResponse });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error updating response", details = ex.Message });
            }
        }

        // Delete a consultation response
        [HttpDelete("delete/{responseId}")]
        public async Task<IActionResult> DeleteConsultationResponse(int responseId)
        {
            try
            {
                var deleted = await _consultationResponseService.DeleteConsultationResponse(responseId);
                if (!deleted)
                {
                    return NotFound(new { success = false, message = "Consultation response not found." });
                }
                return Ok(new { success = true, message = "Consultation response deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error deleting response", details = ex.Message });
            }
        }

        // Count the total number of consultation responses
        [HttpGet("count")]
        public async Task<IActionResult> CountConsultationResponses()
        {
            try
            {
                var count = await _consultationResponseService.CountConsultationResponses();
                return Ok(new { success = true, data = count });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error counting responses", details = ex.Message });
            }
        }
    }
}
