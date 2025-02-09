using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.ChildGrowthTracking.Repository.DTO.UseraccountDTO
{
    public class GetAllUserDTO
    {
        public int UserId { get; set; }

        public string Username { get; set; } 

        public string Email { get; set; } 

        

        public string? PhoneNumber { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public DateTime? LastLogin { get; set; }

        public string? ProfilePicture { get; set; }

        public string? Address { get; set; }

        public string? Status { get; set; }

        public int Role { get; set; }

    }
}
