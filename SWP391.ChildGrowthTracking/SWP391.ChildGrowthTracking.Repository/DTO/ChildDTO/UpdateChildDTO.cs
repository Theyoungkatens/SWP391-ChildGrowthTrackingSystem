using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.ChildGrowthTracking.Repository.DTO.ChildDTO
{
    public class UpdateChildDTO
    {
        public int? UserId { get; set; }
        public string? Name { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public double? BirthWeight { get; set; }
        public double? BirthHeight { get; set; }
        public string? BloodType { get; set; }
        public string? Allergies { get; set; }
        public string? Status { get; set; }
        public string? Relationship { get; set; }
    }
}
