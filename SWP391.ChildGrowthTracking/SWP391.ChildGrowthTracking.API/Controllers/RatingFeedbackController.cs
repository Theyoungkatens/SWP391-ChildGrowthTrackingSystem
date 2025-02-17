using Microsoft.AspNetCore.Mvc;
using SWP391.ChildGrowthTracking.Repository;
using SWP391.ChildGrowthTracking.Repository.DTO.RatingFeedbackDTO;
using SWP391.ChildGrowthTracking.Repository.Services;
using System.Threading.Tasks;

namespace SWP391.ChildGrowthTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingFeedbackController : ControllerBase
    {
        private readonly IRatingFeedback _ratingFeedbackService;

        public RatingFeedbackController(IRatingFeedback ratingFeedbackService)
        {
            _ratingFeedbackService = ratingFeedbackService;
        }

        // GET: api/ratingfeedback
        [HttpGet]
        public async Task<IActionResult> GetAllRatingFeedbacks()
        {
            var feedbacks = await _ratingFeedbackService.GetAllRatingFeedbacks();
            return Ok(feedbacks);
        }

        // GET: api/ratingfeedback/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRatingFeedbackById(int id)
        {
            var feedback = await _ratingFeedbackService.GetRatingFeedbackById(id);
            if (feedback == null)
            {
                return NotFound();
            }

            return Ok(feedback);
        }

        // POST: api/ratingfeedback
        [HttpPost]
        public async Task<IActionResult> CreateRatingFeedback([FromBody] CreateRatingFeedbackDTO dto)
        {
            try
            {
                var createdFeedback = await _ratingFeedbackService.CreateRatingFeedback(dto);
                return CreatedAtAction(nameof(GetRatingFeedbackById), new { id = createdFeedback.FeedbackId }, createdFeedback);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/ratingfeedback/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRatingFeedback(int id, [FromBody] UpdateRatingFeedbackDTO dto)
        {
            var updatedFeedback = await _ratingFeedbackService.UpdateRatingFeedback(id, dto);
            if (updatedFeedback == null)
            {
                return NotFound();
            }

            return Ok(updatedFeedback);
        }

        // DELETE: api/ratingfeedback/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRatingFeedback(int id)
        {
            var result = await _ratingFeedbackService.DeleteRatingFeedback(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
