using Microsoft.AspNetCore.Mvc;
using SWP391.ChildGrowthTracking.Repository.DTO.ConsultationRequestDTO;
using SWP391.ChildGrowthTracking.Repository.Services;
using System;
using System.Threading.Tasks;

namespace SWP391.ChildGrowthTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultationRequestController : ControllerBase
    {
        private readonly IConsultationRequest _consultationRequestService;

        public ConsultationRequestController(IConsultationRequest consultationRequestService)
        {
            _consultationRequestService = consultationRequestService;
        }

        // GET: api/ConsultationRequest/get-all
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllConsultationRequests()
        {
            try
            {
                var consultationRequests = await _consultationRequestService.GetAllConsultationRequests();
                return Ok(new { success = true, data = consultationRequests });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // GET: api/ConsultationRequest/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConsultationRequestById(int id)
        {
            try
            {
                var consultationRequest = await _consultationRequestService.GetConsultationRequestById(id);
                if (consultationRequest == null)
                    return NotFound(new { success = false, message = "Consultation request not found." });

                return Ok(new { success = true, data = consultationRequest });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error retrieving consultation request", details = ex.Message });
            }
        }

        // POST: api/ConsultationRequest/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateConsultationRequest([FromBody] CreateConsultationRequestDTO request)
        {
            try
            {
                var createdRequest = await _consultationRequestService.CreateConsultationRequest(request);
                return Ok(new { success = true, message = "Consultation request created successfully.", data = createdRequest });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // PUT: api/ConsultationRequest/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateConsultationRequest(int id, [FromBody] UpdateConsultationRequestDTO request)
        {
            try
            {
                var updatedRequest = await _consultationRequestService.UpdateConsultationRequest(id, request);
                if (updatedRequest == null)
                    return NotFound(new { success = false, message = "Consultation request not found." });

                return Ok(new
                {
                    success = true,
                    message = "Consultation request updated successfully.",
                    data = updatedRequest
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = $"Error updating consultation request: {ex.Message}" });
            }
        }

        // DELETE: api/ConsultationRequest/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteConsultationRequest(int id)
        {
            try
            {
                var deleted = await _consultationRequestService.DeleteConsultationRequest(id);
                if (!deleted)
                    return NotFound(new { success = false, message = "Consultation request not found." });

                return Ok(new { success = true, message = "Consultation request deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error deleting consultation request", details = ex.Message });
            }
        }

        // GET: api/ConsultationRequest/count
        [HttpGet("count")]
        public async Task<IActionResult> CountConsultationRequests()
        {
            try
            {
                var count = await _consultationRequestService.CountConsultationRequests();
                return Ok(new { success = true, count });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error counting consultation requests", details = ex.Message });
            }
        }
    }
}
