using Microsoft.AspNetCore.Mvc;
using SWP391.ChildGrowthTracking.Repository;
using SWP391.ChildGrowthTracking.Repository.DTO.AlertDTO;
using SWP391.ChildGrowthTracking.Repository.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWP391.ChildGrowthTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertController : ControllerBase
    {
        private readonly IAlert _alertService;

        public AlertController(IAlert alertService)
        {
            _alertService = alertService;
        }

        // GET: api/Alert
        [HttpGet]
        public async Task<ActionResult<List<AlertGetDTO>>> GetAllAlerts()
        {
            return Ok(await _alertService.GetAllAlerts());
        }

        // GET: api/Alert/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AlertGetDTO>> GetAlertById(int id)
        {
            var alert = await _alertService.GetAlertById(id);
            if (alert == null) return NotFound("Alert not found");
            return Ok(alert);
        }

        // POST: api/Alert
        // POST: api/Alert
        [HttpPost]
        public async Task<ActionResult<AlertGetDTO>> CreateAlert(CreateAlertDTO dto)
        {
            // Kiểm tra các điều kiện hợp lệ
            if (dto.ChildId == null || dto.ChildId <= 0)
                return BadRequest("ChildId không hợp lệ.");

            if (string.IsNullOrWhiteSpace(dto.AlertType))
                return BadRequest("AlertType không được để trống.");

            if (string.IsNullOrWhiteSpace(dto.Message))
                return BadRequest("Message không được để trống.");

            if (dto.AlertDate != null && dto.AlertDate > DateTime.UtcNow)
                return BadRequest("AlertDate không thể là ngày trong tương lai.");

            // Nếu dữ liệu hợp lệ, tiếp tục tạo Alert
            var newAlert = await _alertService.CreateAlert(dto);
            return CreatedAtAction(nameof(GetAlertById), new { id = newAlert.AlertId }, newAlert);
        }


        // PUT: api/Alert/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<AlertGetDTO>> UpdateAlert(int id, UpdateAlertDTO dto)
        {
            var updatedAlert = await _alertService.UpdateAlert(id, dto);
            if (updatedAlert == null) return NotFound("Alert not found");
            return Ok(updatedAlert);
        }

        // DELETE: api/Alert/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAlert(int id)
        {
            var result = await _alertService.DeleteAlert(id);
            if (!result) return NotFound("Alert not found");
            return Ok(true);
        }

        // GET: api/Alert/count
        [HttpGet("count")]
        public async Task<ActionResult<int>> CountAlerts()
        {
            return Ok(await _alertService.CountAlerts());
        }
    }
}
