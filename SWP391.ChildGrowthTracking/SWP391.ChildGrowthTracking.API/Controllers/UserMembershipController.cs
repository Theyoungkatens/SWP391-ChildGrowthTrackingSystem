using Microsoft.AspNetCore.Mvc;
using SWP391.ChildGrowthTracking.Repository;
using SWP391.ChildGrowthTracking.Repository.DTO.UserMembershipDTO;
using SWP391.ChildGrowthTracking.Repository.Services;
using System;
using System.Threading.Tasks;

namespace SWP391.ChildGrowthTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMembershipController : ControllerBase
    {
        private readonly IUserMembership _userMembershipService;

        public UserMembershipController(IUserMembership userMembershipService)
        {
            _userMembershipService = userMembershipService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllUserMemberships()
        {
            try
            {
                var memberships = await _userMembershipService.GetAllUserMemberships();
                return Ok(new { success = true, data = memberships });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserMembershipById(int id)
        {
            try
            {
                var membership = await _userMembershipService.GetUserMembershipById(id);
                if (membership == null)
                    return NotFound(new { success = false, message = "Membership not found." });

                return Ok(new { success = true, data = membership });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error retrieving membership", details = ex.Message });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUserMembership([FromBody] CreateUserMembershipDTO request)
        {
            try
            {
                var membership = await _userMembershipService.CreateUserMembership(request);
                return Ok(new { success = true, message = "Membership created successfully.", data = membership });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUserMembership(int id, [FromBody] UpdateUserMembershipDTO request)
        {
            try
            {
                var updatedMembership = await _userMembershipService.UpdateUserMembership(id, request);
                if (updatedMembership == null)
                    return NotFound(new { success = false, message = "Membership not found." });

                return Ok(new
                {
                    success = true,
                    message = "Membership updated successfully.",
                    data = updatedMembership
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = $"Error updating membership: {ex.Message}" });
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUserMembership(int id)
        {
            try
            {
                var deleted = await _userMembershipService.DeleteUserMembership(id);
                if (!deleted)
                    return NotFound(new { success = false, message = "Membership not found." });

                return Ok(new { success = true, message = "Membership deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error deleting membership", details = ex.Message });
            }
        }
    }
}
