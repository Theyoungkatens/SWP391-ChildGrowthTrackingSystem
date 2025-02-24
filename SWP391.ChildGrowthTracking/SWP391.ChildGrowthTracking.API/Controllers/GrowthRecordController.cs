using Microsoft.AspNetCore.Mvc;
using SWP391.ChildGrowthTracking.Repository;
using SWP391.ChildGrowthTracking.Repository.DTO.GrowthRecordDTO;
using SWP391.ChildGrowthTracking.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWP391.ChildGrowthTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrowthRecordController : ControllerBase
    {
        private readonly IGrowthRecord _growthRecordService;

        public GrowthRecordController(IGrowthRecord growthRecordService)
        {
            _growthRecordService = growthRecordService;
        }

        // GET: api/GrowthRecord
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GrowthRecordDTO>>> GetAll()
        {
            var records = await _growthRecordService.GetAll();
            return Ok(records);
        }

        // GET api/GrowthRecord/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GrowthRecordDTO>> Get(int id)
        {
            var record = await _growthRecordService.GetById(id);
            if (record == null)
            {
                return NotFound("Growth record not found.");
            }
            return Ok(record);
        }

        // POST api/GrowthRecord
        [HttpPost("{childId}")]
        public async Task<ActionResult<GrowthRecordDTO>> Create(int childId, [FromBody] GrowthRecordDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            var createdRecord = await _growthRecordService.Create(childId, dto);
            return CreatedAtAction(nameof(Get), new { id = createdRecord.RecordId }, createdRecord);
        }

        // PUT api/GrowthRecord/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GrowthRecordDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            var updated = await _growthRecordService.Update(id, dto);
            if (!updated)
            {
                return NotFound("Growth record not found.");
            }
            return NoContent();
        }

        // DELETE api/GrowthRecord/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _growthRecordService.Delete(id);
            if (!deleted)
            {
                return NotFound("Growth record not found.");
            }
            return NoContent();
        }
    }
}
