using Microsoft.AspNetCore.Mvc;
using SWP391.ChildGrowthTracking.Repository;
using System;
using System.Threading.Tasks;

namespace SWP391.ChildGrowthTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPayment _paymentService;

        public PaymentController(IPayment paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePayment(int membershipId)
        {
            try
            {
                var payment = await _paymentService.CreatePayment(membershipId);
                if (payment == null)
                {
                    return BadRequest("Failed to create payment. Membership not found.");
                }
                return Ok(payment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("delete/{paymentId}")]
        public async Task<IActionResult> DeletePayment(int paymentId)
        {
            try
            {
                var result = await _paymentService.DeletePayment(paymentId);
                if (!result)
                {
                    return NotFound("Payment not found.");
                }
                return Ok("Payment successfully deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{membershipId}")]
        public async Task<IActionResult> GetPayment(int membershipId)
        {
            try
            {
                var payment = await _paymentService.GetPayment(membershipId);
                if (payment == null)
                {
                    return NotFound("Payment not found.");
                }
                return Ok(payment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("update/{paymentId}")]
        public async Task<IActionResult> UpdatePayment(int paymentId, string status)
        {
            try
            {
                var payment = await _paymentService.UpdatePaymentStatus(paymentId, status);
                if (payment == null)
                {
                    return NotFound("Payment not found or could not be updated.");
                }
                return Ok(payment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
