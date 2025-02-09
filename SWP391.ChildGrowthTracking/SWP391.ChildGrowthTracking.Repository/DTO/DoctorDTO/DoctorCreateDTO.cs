namespace SWP391.ChildGrowthTracking.Repository.DTO.DoctorDTO
{
    public class DoctorCreateDTO
    {
        public string Name { get; set; }
        public string Specialization { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Degree { get; set; }
        public string Hospital { get; set; }
        public string LicenseNumber { get; set; }
        public string Biography { get; set; }
        public int UserId { get; set; }
    }
}
