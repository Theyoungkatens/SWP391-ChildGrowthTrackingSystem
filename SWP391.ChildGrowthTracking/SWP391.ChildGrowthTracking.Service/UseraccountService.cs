using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SWP391.ChildGrowthTracking.Repository;
using SWP391.ChildGrowthTracking.Repository.DTO;
using SWP391.ChildGrowthTracking.Repository.DTO.DoctorDTO;
using SWP391.ChildGrowthTracking.Repository.DTO.UseraccountDTO;
using SWP391.ChildGrowthTracking.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SWP391.ChildGrowthTracking.Service
{
    public class UseraccountService : IUseraccount
    {
        private readonly IConfiguration _configuration;

        private readonly Swp391ChildGrowthTrackingContext context;

        public UseraccountService(Swp391ChildGrowthTrackingContext Context, IConfiguration configuration)
        {
            context = Context;
            _configuration = configuration;
        }

        public async Task<List<GetAllUserDTO>> GetAllUsers()
        {
            try
            {
                return await context.Useraccounts
                    .Select(u => new GetAllUserDTO
                    {
                        UserId = u.UserId,
                        Username = u.Username,
                        Email = u.Email,
                        PhoneNumber = u.PhoneNumber,
                        RegistrationDate = u.RegistrationDate,
                        LastLogin = u.LastLogin,
                        ProfilePicture = u.ProfilePicture,
                        Address = u.Address,
                        Status = u.Status,
                        Role = (int)u.Role
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving users: {ex.Message}");
            }
        }

        public async Task<Useraccount> Authenticate(string username, string password)
        {
            var user = await context.Useraccounts.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                user.LastLogin = DateTime.UtcNow;
                await context.SaveChangesAsync(); // Lưu thời gian đăng nhập mới
            }

            return user;
        }

        public async Task<GetAllUserDTO> RegisterUser(RegisterDTO request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "Invalid registration data.");

            // Kiểm tra email hợp lệ trước khi chuẩn hóa
            if (!IsValidEmail(request.Email))
                throw new ArgumentException("Invalid email format.");

            // Chuẩn hóa đầu vào
            string normalizedEmail = request.Email.Trim().ToLower();
            string username = request.Username?.Trim() ?? throw new ArgumentException("Username is required.");
            string? phoneNumber = request.PhoneNumber?.Trim();
            string? address = request.Address?.Trim();

            // Kiểm tra email đã tồn tại
            bool emailExists = await context.Useraccounts.AnyAsync(u => u.Email == normalizedEmail);
            if (emailExists)
                throw new ArgumentException("Email has already been registered!");

            // Mã hóa mật khẩu
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // Tạo user mới
            var user = new Useraccount
            {
                Email = normalizedEmail,
                Username = username,
                PhoneNumber = phoneNumber,
                Address = address,
                Password = hashedPassword,
                Role = 2, // Mặc định Role = 2
                RegistrationDate = DateTime.UtcNow,
                Status = "Active"
            };

            // Lưu vào database
            await context.Useraccounts.AddAsync(user);
            await context.SaveChangesAsync();

            // Lấy thông tin người dùng vừa tạo bằng GetUserById()
            return await GetUserById(user.UserId);
        }


        // Kiểm tra định dạng email
        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
            return emailRegex.IsMatch(email);
        }

        public async Task<GetAllUserDTO> UpdateUserAsync(int userId, UpdateUserDTO request)
        {
            try
            {
                var user = await context.Useraccounts.FindAsync(userId);
                if (user == null)
                    throw new KeyNotFoundException("User not found.");

                // Cập nhật thông tin nếu request có giá trị mới
                user.Username = !string.IsNullOrWhiteSpace(request.Username) ? request.Username.Trim() : user.Username;
                user.PhoneNumber = !string.IsNullOrWhiteSpace(request.PhoneNumber) ? request.PhoneNumber.Trim() : user.PhoneNumber;
                user.ProfilePicture = !string.IsNullOrWhiteSpace(request.ProfilePicture) ? request.ProfilePicture.Trim() : user.ProfilePicture;
                user.Address = !string.IsNullOrWhiteSpace(request.Address) ? request.Address.Trim() : user.Address;

                await context.SaveChangesAsync();

                // Trả về thông tin sau khi cập nhật
                return await GetUserById(user.UserId);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating user: {ex.Message}", ex);
            }
        }

        public async Task<GetAllUserDTO> GetUserById(int userId)
        {
            try
            {
                var user = await context.Useraccounts
                    .Where(u => u.UserId == userId)
                    .Select(u => new GetAllUserDTO
                    {
                        UserId = u.UserId,
                        Username = u.Username,
                        Email = u.Email,
                        PhoneNumber = u.PhoneNumber,
                        RegistrationDate = u.RegistrationDate,
                        LastLogin = u.LastLogin,
                        ProfilePicture = u.ProfilePicture,
                        Address = u.Address,
                        Status = u.Status,
                        Role = (int)u.Role
                    })
                    .FirstOrDefaultAsync();

                if (user == null)
                    throw new KeyNotFoundException("User not found!");

                return user;
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving user: {ex.Message}", ex);
            }
        }

        public async Task<bool> BanUser(int userId)
        {
            var user = await context.Useraccounts.FindAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found!");

            user.Status = "Ban";
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveUser(int userId)
        {
            var user = await context.Useraccounts.FindAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found!");

            user.Status = "Remove";
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<CreateUserDoctorDTO> CreateDoctorAsync(CreateUserDoctorDTO request)
        {
            using var transaction = await this.context.Database.BeginTransactionAsync();
            try
            {
                if (request == null)
                {
                    throw new Exception("Request data is missing.");
                }

                // Kiểm tra email đã tồn tại chưa
                if (await this.context.Useraccounts.AnyAsync(u => u.Email == request.Email))
                {
                    throw new Exception("Email already exists!");
                }

                // Tạo tài khoản User cho bác sĩ
                var doctorUser = new Useraccount
                {
                    Username = request.Username,
                    Password = request.Password,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    Address = request.Address,
                    RegistrationDate = DateTime.UtcNow,
                    Role = 3, // Role 3 là Doctor
                    Status = "Active"
                };

                await this.context.Useraccounts.AddAsync(doctorUser);
                await this.context.SaveChangesAsync();

                // Tạo hồ sơ bác sĩ
                var doctor = new Doctor
                {
                    UserId = doctorUser.UserId,
                    Name = request.Fullname,
                    Specialization = request.Specialization,
                    Email = doctorUser.Email,
                    PhoneNumber = doctorUser.PhoneNumber,
                    Degree = request.Degree,
                    Hospital = request.Hospital,
                    LicenseNumber = request.LicenseNumber,
                    Biography = request.Biography
                };

                await this.context.Doctors.AddAsync(doctor);
                await this.context.SaveChangesAsync();

                await transaction.CommitAsync(); // Commit transaction nếu không có lỗi

                // Return the CreateUserDoctorDTO after successful creation
                return new CreateUserDoctorDTO
                {
                    Username = doctorUser.Username,
                    Email = doctorUser.Email,
                    PhoneNumber = doctorUser.PhoneNumber,
                    Address = doctorUser.Address,
                    Fullname = doctor.Name,
                    Specialization = doctor.Specialization,
                    Degree = doctor.Degree,
                    Hospital = doctor.Hospital,
                    LicenseNumber = doctor.LicenseNumber,
                    Biography = doctor.Biography
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(); // Rollback nếu có lỗi
                throw new Exception($"Error creating doctor: {ex.Message}");
            }
        }

        public async Task<List<GetAllUserDTO>> GetAllCustomers()
        {
            try
            {
                return await context.Useraccounts
                    .Where(u => u.Role == 2) // Chỉ lấy user có Role = 2 (Patient)
                    .Select(u => new GetAllUserDTO
                    {
                        UserId = u.UserId,
                        Username = u.Username,
                        Email = u.Email,
                        PhoneNumber = u.PhoneNumber,
                        RegistrationDate = u.RegistrationDate,
                        LastLogin = u.LastLogin,
                        ProfilePicture = u.ProfilePicture,
                        Address = u.Address,
                        Status = u.Status,
                        Role = (int)u.Role
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving patients: {ex.Message}");
            }
        }






    }
}
