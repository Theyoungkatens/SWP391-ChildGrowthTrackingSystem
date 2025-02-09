using Microsoft.AspNetCore.Mvc;
using SWP391.ChildGrowthTracking.Repository;
using SWP391.ChildGrowthTracking.Repository.DTO;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SWP391.ChildGrowthTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UseraccountController : ControllerBase
    {
        private readonly IUseraccount useraccount;

        public UseraccountController(IUseraccount useraccount)
        {
            this.useraccount = useraccount;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllDTO request)
        {
            try
            {
                var users = await this.useraccount.GetAllUsers(request);
                return Ok(new { success = true, data = users });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

            // POST api/<UseraccountController>
            [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UseraccountController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UseraccountController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
