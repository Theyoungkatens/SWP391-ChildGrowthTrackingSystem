using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.ChildGrowthTracking.Repository.DTO.UseraccountDTO
{
    public class UpdateUserDTO
    {
        public string Username { get; set; }
        
        public string? PhoneNumber { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Address { get; set; }
    }
}
