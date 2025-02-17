using Microsoft.AspNetCore.Mvc;
using SWP391.ChildGrowthTracking.Repository;
using SWP391.ChildGrowthTracking.Repository.DTO;
using SWP391.ChildGrowthTracking.Repository.DTO.ChildDTO;
using SWP391.ChildGrowthTracking.Repository.Services;
using System;
using System.Threading.Tasks;

namespace SWP391.ChildGrowthTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChildController : ControllerBase
    {
        private readonly IChild _childService;

        public ChildController(IChild childService)
        {
            _childService = childService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllChildren()
        {
            try
            {
                var children = await _childService.GetAllChild();
                return Ok(new { success = true, data = children });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChildById(int id)
        {
            try
            {
                var child = await _childService.GetChildById(id);
                if (child == null)
                    return NotFound(new { success = false, message = "Child not found." });

                return Ok(new { success = true, data = child });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error retrieving child", details = ex.Message });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateChild([FromBody] CreateChildDTO request)
        {
            try
            {
                var child = await _childService.CreateChild(request);
                return Ok(new { success = true, message = "Child created successfully.", data = child });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateChild(int id, [FromBody] UpdateChildDTO request)
        {
            try
            {
                var updatedChild = await _childService.UpdateChild(id, request);
                if (updatedChild == null)
                    return NotFound(new { success = false, message = "Child not found." });

                return Ok(new
                {
                    success = true,
                    message = "Child updated successfully.",
                    data = updatedChild
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = $"Error updating child: {ex.Message}" });
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteChild(int id)
        {
            try
            {
                var deleted = await _childService.DeleteChild(id);
                if (!deleted)
                    return NotFound(new { success = false, message = "Child not found." });

                return Ok(new { success = true, message = "Child deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error deleting child", details = ex.Message });
            }
        }
        [HttpGet("count")]
        public async Task<IActionResult> GetChildCount()
        {
            try
            {
                var childCount = await _childService.GetChildCount();
                return Ok(new { Count = childCount });
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an appropriate error response
                return StatusCode(500, new { Message = "An error occurred while fetching child count.", Details = ex.Message });
            }
        }
    }
}
