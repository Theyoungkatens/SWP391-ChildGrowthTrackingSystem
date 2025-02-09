using Microsoft.EntityFrameworkCore;
using SWP391.ChildGrowthTracking.Repository.Interfaces;
using SWP391.ChildGrowthTracking.Repository.DTO.DoctorDTO;
using SWP391.ChildGrowthTracking.Repository.Model;

namespace SWP391.ChildGrowthTracking.Repository.Services
{
    public class DoctorService : IDoctor
    {
        private readonly Swp391ChildGrowthTrackingContext _context;

        public DoctorService(Swp391ChildGrowthTrackingContext context)
        {
            _context = context;
        }

        // Get all doctors
        public async Task<List<DoctorDTO>> GetAllDoctors()
        {
            return await _context.Doctors
                .Select(d => new DoctorDTO
                {
                    DoctorId = d.DoctorId,
                    Name = d.Name,
                    Specialization = d.Specialization,
                    Email = d.Email,
                    PhoneNumber = d.PhoneNumber,
                    Degree = d.Degree,
                    Hospital = d.Hospital,
                    LicenseNumber = d.LicenseNumber,
                    Biography = d.Biography,
                    UserId = d.UserId
                }).ToListAsync();
        }

        // Get a doctor by ID
        public async Task<DoctorDTO?> GetDoctorById(int doctorId)
        {
            var doctor = await _context.Doctors.FindAsync(doctorId);
            if (doctor == null) return null;

            return new DoctorDTO
            {
                DoctorId = doctor.DoctorId,
                Name = doctor.Name,
                Specialization = doctor.Specialization,
                Email = doctor.Email,
                PhoneNumber = doctor.PhoneNumber,
                Degree = doctor.Degree,
                Hospital = doctor.Hospital,
                LicenseNumber = doctor.LicenseNumber,
                Biography = doctor.Biography,
                UserId = doctor.UserId
            };
        }

        

        // Update an existing doctor
        public async Task<DoctorDTO> UpdateDoctor(int doctorId, DoctorCreateDTO doctorDto)
        {
            if (doctorDto == null)
                throw new ArgumentNullException(nameof(doctorDto), "Doctor data cannot be null.");

            // Find the doctor by ID
            var existingDoctor = await _context.Doctors.FindAsync(doctorId);
            if (existingDoctor == null)
                throw new Exception("Doctor not found.");

            // Check if the email already exists (excluding the current doctor)
            bool isEmailExist = await _context.Doctors
                .AnyAsync(d => d.Email.ToLower() == doctorDto.Email.ToLower() && d.DoctorId != doctorId);

            if (isEmailExist)
            {
                throw new Exception("A doctor with the same email already exists.");
            }

            // Update doctor information
            existingDoctor.Name = doctorDto.Name;
            existingDoctor.Specialization = doctorDto.Specialization;
            existingDoctor.Email = doctorDto.Email;
            existingDoctor.PhoneNumber = doctorDto.PhoneNumber;
            existingDoctor.Degree = doctorDto.Degree;
            existingDoctor.Hospital = doctorDto.Hospital;
            existingDoctor.LicenseNumber = doctorDto.LicenseNumber;
            existingDoctor.Biography = doctorDto.Biography;
            existingDoctor.UserId = doctorDto.UserId;

            _context.Doctors.Update(existingDoctor);
            await _context.SaveChangesAsync();

            return new DoctorDTO
            {
                DoctorId = existingDoctor.DoctorId,
                Name = existingDoctor.Name,
                Specialization = existingDoctor.Specialization,
                Email = existingDoctor.Email,
                PhoneNumber = existingDoctor.PhoneNumber,
                Degree = existingDoctor.Degree,
                Hospital = existingDoctor.Hospital,
                LicenseNumber = existingDoctor.LicenseNumber,
                Biography = existingDoctor.Biography,
                UserId = existingDoctor.UserId
            };
        }

        // Delete a doctor
        public async Task<bool> DeleteDoctor(int doctorId)
        {
            var doctor = await _context.Doctors.FindAsync(doctorId);
            if (doctor == null) return false;

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
