using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SWP391.ChildGrowthTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrowthRecordController : ControllerBase
    {
        // GET: api/<GrowthRecordController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<GrowthRecordController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<GrowthRecordController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<GrowthRecordController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GrowthRecordController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
