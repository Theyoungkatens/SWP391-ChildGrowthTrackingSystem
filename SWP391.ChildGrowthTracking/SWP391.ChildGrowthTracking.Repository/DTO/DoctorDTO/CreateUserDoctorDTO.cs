using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.ChildGrowthTracking.Repository.DTO.DoctorDTO
{
    public class CreateUserDoctorDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Fullname { get; set; }
        public string Specialization { get; set; }
        public string Degree { get; set; }
        public string Hospital { get; set; }
        public string LicenseNumber { get; set; }
        public string Biography { get; set; }
    }

}
