using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SWP391.ChildGrowthTracking.Repository;
using SWP391.ChildGrowthTracking.Repository.DTO;
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

        public async Task<List<Useraccount>> GetAllUsers(GetAllDTO request)
        {
            try
            {
                var query = context.Useraccounts.AsQueryable();

                // Filtering
                if (!string.IsNullOrWhiteSpace(request.FilterOn) && !string.IsNullOrWhiteSpace(request.FilterQuery))
                {
                    string filterQuery = request.FilterQuery.Trim().ToLower();

                    query = request.FilterOn.ToLower() switch
                    {
                        "username" => query.Where(u => u.Username != null && u.Username.ToLower().Contains(filterQuery)),
                        "email" => query.Where(u => u.Email != null && u.Email.ToLower().Contains(filterQuery)),
                        "phonenumber" => query.Where(u => u.PhoneNumber != null && u.PhoneNumber.Contains(filterQuery)),
                        "registrationdate" when DateTime.TryParse(filterQuery, out var regDate) =>
                            query.Where(u => u.RegistrationDate.HasValue && u.RegistrationDate.Value.Date == regDate.Date),
                        "lastlogin" when DateTime.TryParse(filterQuery, out var lastLogin) =>
                            query.Where(u => u.LastLogin.HasValue && u.LastLogin.Value.Date == lastLogin.Date),
                        "status" => query.Where(u => u.Status != null && u.Status.ToLower().Contains(filterQuery)),
                        _ => query // Không lọc nếu điều kiện không hợp lệ
                    };
                }

                // Sorting
                if (!string.IsNullOrWhiteSpace(request.SortBy))
                {
                    bool isAscending = request.IsAscending ?? true;

                    query = request.SortBy.ToLower() switch
                    {
                        "username" => isAscending
                            ? query.OrderBy(u => u.Username ?? "")
                            : query.OrderByDescending(u => u.Username ?? ""),
                        "email" => isAscending
                            ? query.OrderBy(u => u.Email ?? "")
                            : query.OrderByDescending(u => u.Email ?? ""),
                        "phonenumber" => isAscending
                            ? query.OrderBy(u => u.PhoneNumber ?? "")
                            : query.OrderByDescending(u => u.PhoneNumber ?? ""),
                        "registrationdate" => isAscending
                            ? query.OrderBy(u => u.RegistrationDate ?? DateTime.MinValue)
                            : query.OrderByDescending(u => u.RegistrationDate ?? DateTime.MinValue),
                        "lastlogin" => isAscending
                            ? query.OrderBy(u => u.LastLogin ?? DateTime.MinValue)
                            : query.OrderByDescending(u => u.LastLogin ?? DateTime.MinValue),
                        "status" => isAscending
                            ? query.OrderBy(u => u.Status ?? "")
                            : query.OrderByDescending(u => u.Status ?? ""),
                        _ => query.OrderBy(u => u.Username ?? "")
                    };
                }

                // Paging
                int pageNumber = request.PageNumber ?? 1;
                int pageSize = request.PageSize ?? 10;
                query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

                return await query.ToListAsync();
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

        public async Task<Useraccount> RegisterUser(RegisterDTO request)
        {
            try
            {
                if (request == null)
                    throw new ArgumentException("Invalid registration data.");

                request.Email = request.Email.Trim().ToLower(); // Chuẩn hóa email
                request.Username = request.Username?.Trim();
                request.PhoneNumber = request.PhoneNumber?.Trim();
                request.Address = request.Address?.Trim();

                // Kiểm tra email hợp lệ
                var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                if (!emailRegex.IsMatch(request.Email))
                    throw new ArgumentException("Invalid email format.");

                // Kiểm tra email đã tồn tại chưa
                bool emailExists = await context.Useraccounts.AnyAsync(u => u.Email.ToLower() == request.Email);
                if (emailExists)
                    throw new ArgumentException("Email has already been registered!");

                // Kiểm tra Username (Không để trống)
                if (string.IsNullOrWhiteSpace(request.Username))
                    throw new ArgumentException("Username is required.");

                // Mã hóa mật khẩu
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

                // Tạo user mới
                var user = new Useraccount
                {
                    Email = request.Email,
                    Username = request.Username,
                    PhoneNumber = request.PhoneNumber,
                    Address = request.Address,
                    Password = hashedPassword,
                    Role = 2, // Mặc định Role = 2
                    RegistrationDate = DateTime.UtcNow,
                    Status = "Active"
                };

                // Lưu vào database
                await context.Useraccounts.AddAsync(user);
                await context.SaveChangesAsync();

                return user;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"Validation Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during registration: {ex.Message}", ex);
            }
        }

        public async Task<Useraccount> UpdateUserAsync(int userId, UpdateUserDTO request)
        {
            try
            {
                var user = await context.Useraccounts.FindAsync(userId);
                if (user == null)
                {
                    throw new Exception("User not found.");
                }

                user.Username = request.Username ?? user.Username;
                user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
                user.ProfilePicture = request.ProfilePicture ?? user.ProfilePicture;
                user.Address = request.Address ?? user.Address;
                
                await context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating user: {ex.Message}");
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







    }
}
