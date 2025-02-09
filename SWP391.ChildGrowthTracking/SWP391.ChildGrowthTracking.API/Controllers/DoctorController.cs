using Microsoft.AspNetCore.Mvc;
using SWP391.ChildGrowthTracking.Repository.DTO.DoctorDTO;
using SWP391.ChildGrowthTracking.Repository.Interfaces;
using SWP391.ChildGrowthTracking.Repository.Services;

namespace SWP391.ChildGrowthTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctor _doctorService;

        public DoctorController(IDoctor doctorService)
        {
            _doctorService = doctorService;
        }

        // GET: api/doctor
        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _doctorService.GetAllDoctors();
            return Ok(doctors);
        }

        // GET api/doctor/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var doctor = await _doctorService.GetDoctorById(id);
            if (doctor == null)
            {
                return NotFound($"Doctor with ID {id} not found.");
            }
            return Ok(doctor);
        }

        

        // PUT api/doctor/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, [FromBody] DoctorCreateDTO doctorCreateDTO)
        {
            if (doctorCreateDTO == null)
            {
                return BadRequest("Doctor data is null.");
            }

            try
            {
                var updatedDoctor = await _doctorService.UpdateDoctor(id, doctorCreateDTO);
                return Ok(updatedDoctor);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error occurred: {ex.Message}");
            }
        }

        // DELETE api/doctor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            try
            {
                bool isDeleted = await _doctorService.DeleteDoctor(id);
                if (!isDeleted)
                {
                    return NotFound($"Doctor with ID {id} not found.");
                }
                return NoContent(); // Successfully deleted
            }
            catch (Exception ex)
            {
                return BadRequest($"Error occurred: {ex.Message}");
            }
        }
    }
}
