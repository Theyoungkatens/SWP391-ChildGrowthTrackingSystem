
using System.ComponentModel.DataAnnotations;


namespace SWP391.ChildGrowthTracking.Repository.DTO.UseraccountDTO
{
    public class RegisterDTO
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string Username { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }

    }
}
