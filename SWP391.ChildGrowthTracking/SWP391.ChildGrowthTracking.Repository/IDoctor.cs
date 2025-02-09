using SWP391.ChildGrowthTracking.Repository.DTO.DoctorDTO;

namespace SWP391.ChildGrowthTracking.Repository.Interfaces
{
    public interface IDoctor
    {
        Task<List<DoctorDTO>> GetAllDoctors();
        Task<DoctorDTO?> GetDoctorById(int doctorId);
        
        Task<DoctorDTO> UpdateDoctor(int doctorId, DoctorCreateDTO doctorDto);
        Task<bool> DeleteDoctor(int doctorId);
    }
}
