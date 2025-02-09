using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using SWP391.ChildGrowthTracking.Repository.DTO.MembershipPackageDTO;
using SWP391.ChildGrowthTracking.Repository.Services;
using SWP391.ChildGrowthTracking.Repository;

namespace SWP391.ChildGrowthTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipPackageController : ControllerBase
    {
        private readonly IMembershipPackage _service;

        public MembershipPackageController(IMembershipPackage service)
        {
            _service = service;
        }


        /// <summary>
        /// Get all membership packages
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MembershipPackageDTO>>> GetAllPackages()
        {
            var packages = await _service.GetAllPackages();
            return Ok(packages);
        }

        /// <summary>
        /// Get a membership package by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<MembershipPackageDTO>> GetPackageById(int id)
        {
            var package = await _service.GetPackageById(id);
            if (package == null) return NotFound("Membership package not found.");
            return Ok(package);
        }

        /// <summary>
        /// Create a new membership package
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<MembershipPackageDTO>> CreatePackage([FromBody] MembershipPackageCreateDTO dto)
        {
            if (dto == null) return BadRequest("Invalid package data.");
            var package = await _service.CreatePackage(dto);
            return CreatedAtAction(nameof(GetPackageById), new { id = package.PackageId }, package);
        }

        /// <summary>
        /// Update an existing membership package
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<MembershipPackageDTO>> UpdatePackage(int id, [FromBody] MembershipPackageUpdateDTO dto)
        {
            if (dto == null) return BadRequest("Invalid update data.");
            var updatedPackage = await _service.UpdatePackage(id, dto);
            if (updatedPackage == null) return NotFound("Membership package not found.");
            return Ok(updatedPackage);
        }

        /// <summary>
        /// Delete a membership package
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePackage(int id)
        {
            var result = await _service.DeletePackage(id);
            if (!result) return NotFound("Membership package not found.");

            return Ok(new { message = "Membership package deleted successfully." });
        }


        /// <summary>
        /// Approve (Activate) a membership package
        /// </summary>
        [HttpPatch("{id}/approve")]
        public async Task<ActionResult> ApprovePackage(int id)
        {
            var result = await _service.ApprovePackage(id);
            if (!result) return NotFound("Membership package not found.");
            return Ok("Package approved successfully.");
        }

        /// <summary>
        /// Deactivate a membership package
        /// </summary>
        [HttpPatch("{id}/deactivate")]
        public async Task<ActionResult> DeactivatePackage(int id)
        {
            var result = await _service.DeactivatePackage(id);
            if (!result) return NotFound("Membership package not found.");
            return Ok("Package deactivated successfully.");
        }
    }
}
