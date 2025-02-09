using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SWP391.ChildGrowthTracking.Repository;
using SWP391.ChildGrowthTracking.Repository.DTO;
using SWP391.ChildGrowthTracking.Repository.DTO.DoctorDTO;
using SWP391.ChildGrowthTracking.Repository.DTO.UseraccountDTO;
using SWP391.ChildGrowthTracking.Repository.Model;
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
                return Unauthorized(new { success = false, message = "Invalid username or password" });

            var token = GenerateJSONWebToken(user);

            // Xác định vai trò của người dùng
            string roleName = user.Role switch
            {
                1 => "Admin",
                2 => "Member",
                3 => "Doctor",
                _ => "Unknown"
            };

            return Ok(new
            {
                success = true,
                token,
                user = new
                {
                    user.UserId,
                    user.Username,
                    user.Email,
                    user.PhoneNumber,
                    Role = user.Role,
                    RoleName = roleName
                }
            });
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _userAccountService.GetUserById(id);
                return Ok(new { success = true, data = user });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error retrieving user", details = ex.Message });
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
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO request)
        {
            try
            {   
                var user = await _userAccountService.RegisterUser(request);
                return Ok(new { success = true, message = "User registered successfully.", data = user });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpPut("update/{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UpdateUserDTO request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest(new { success = false, message = "Invalid request data." });
                }

                var updatedUser = await _userAccountService.UpdateUserAsync(userId, request);
                if (updatedUser == null)
                {
                    return NotFound(new { success = false, message = "User not found." });
                }

                return Ok(new
                {
                    success = true,
                    message = "User updated successfully.",
                    user = new
                    {
                        updatedUser.UserId,
                        updatedUser.Username,
                        updatedUser.Email,
                        updatedUser.PhoneNumber,
                        updatedUser.ProfilePicture,
                        updatedUser.Address,
                        updatedUser.Status,
                        updatedUser.Role
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = $"Error updating user: {ex.Message}" });
            }
        }
        // PUT: api/user/ban/{id}
        [HttpPut("ban/{id}")]
        public async Task<IActionResult> BanUser(int id)
        {
            try
            {
                await _userAccountService.BanUser(id);
                return Ok(new { success = true, message = "User has been banned!" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error banning user", details = ex.Message });
            }
        }

        // PUT: api/user/remove/{id}
        [HttpPut("remove/{id}")]
        public async Task<IActionResult> RemoveUser(int id)
        {
            try
            {
                await _userAccountService.RemoveUser(id);
                return Ok(new { success = true, message = "User has been removed!" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error removing user", details = ex.Message });
            }
        }
        [HttpPost("create-doctor")]
        public async Task<IActionResult> CreateDoctor([FromBody] CreateUserDoctorDTO request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest(new { success = false, message = "Invalid request data." });
                }

                var doctorUser = await _userAccountService.CreateDoctorAsync(request);

                return Ok(new
                {
                    success = true,
                    message = "Doctor created successfully.",
                    data = doctorUser
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = $"Error creating doctor: {ex.Message}" });
            }
        }

        public sealed record LoginRequest(string UserName, string Password);
    }
}
