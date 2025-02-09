using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SWP391.ChildGrowthTracking.Repository;
using SWP391.ChildGrowthTracking.Repository.DTO;
using SWP391.ChildGrowthTracking.Repository.Models;
using SWP391.ChildGrowthTracking.Service;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.ChildGrowthTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUseraccount _userAccountService;

        public UserAccountController(IConfiguration config, IUseraccount userAccountService)
        {
            _config = config;
            _userAccountService = userAccountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userAccountService.Authenticate(request.UserName, request.Password);
            if (user == null)
                return Unauthorized();

            var token = GenerateJSONWebToken(user);
            return Ok(new { success = true, token });
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllDTO request)
        {
            try
            {
                var users = await _userAccountService.GetAllUsers(request);
                return Ok(new { success = true, data = users });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        private string GenerateJSONWebToken(Useraccount userAccount)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                new Claim[]
                {
                    new(ClaimTypes.Name, userAccount.Username),
                    new(ClaimTypes.Role, userAccount.Status),
                },
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public sealed record LoginRequest(string UserName, string Password);
    }
}
