using Microsoft.AspNetCore.Mvc;
using SWP391.ChildGrowthTracking.Repository;
using SWP391.ChildGrowthTracking.Repository.DTO.ChildDTO;
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
                return BadRequest(new { success = false, message = ex.Message });
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

                return Ok(new { success = true, message = "Child updated successfully.", data = updatedChild });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
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
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetChildCount()
        {
            try
            {
                var childCount = await _childService.GetChildCount();
                return Ok(new { success = true, count = childCount });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("is-parent/{userId}/{childId}")]
        public async Task<IActionResult> IsParent(int userId, int childId)
        {
            try
            {
                var isParent = await _childService.IsParent(userId, childId);
                return Ok(new { success = true, isParent });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetChildrenByUserId(int userId)
        {
            try
            {
                var children = await _childService.GetChildrenByUserId(userId);
                return Ok(new { success = true, data = children });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("by-gender/{gender}")]
        public async Task<IActionResult> GetChildrenByGender(string gender)
        {
            try
            {
                var children = await _childService.GetChildrenByGender(gender);
                return Ok(new { success = true, data = children });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("update-status/{id}")]
        public async Task<IActionResult> UpdateChildStatus(int id, [FromBody] string status)
        {
            try
            {
                var updated = await _childService.UpdateChildStatus(id, status);
                if (!updated)
                    return NotFound(new { success = false, message = "Child not found." });

                return Ok(new { success = true, message = "Child status updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("by-age-range")]
        public async Task<IActionResult> GetChildrenByAgeRange([FromQuery] int minAge, [FromQuery] int maxAge)
        {
            try
            {
                var children = await _childService.GetChildrenByAgeRange(minAge, maxAge);
                return Ok(new { success = true, data = children });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("by-blood-type/{bloodType}")]
        public async Task<IActionResult> GetChildrenByBloodType(string bloodType)
        {
            try
            {
                var children = await _childService.GetChildrenByBloodType(bloodType);
                return Ok(new { success = true, data = children });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
