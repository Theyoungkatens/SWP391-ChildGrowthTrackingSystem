using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SWP391.ChildGrowthTracking.Repository;
using SWP391.ChildGrowthTracking.Repository.DTO;
using SWP391.ChildGrowthTracking.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                var query = this.context.Useraccounts.AsQueryable();

                // Filtering
                if (!string.IsNullOrEmpty(request.FilterOn) && !string.IsNullOrEmpty(request.FilterQuery))
                {
                    switch (request.FilterOn.ToLower())
                    {
                        case "username":
                            query = query.Where(u => u.Username.Contains(request.FilterQuery));
                            break;
                        case "email":
                            query = query.Where(u => u.Email.Contains(request.FilterQuery));
                            break;
                        case "phonenumber":
                            query = query.Where(u => u.PhoneNumber.Contains(request.FilterQuery));
                            break;
                        case "registrationdate":
                            if (DateTime.TryParse(request.FilterQuery, out var regDate))
                            {
                                query = query.Where(u => u.RegistrationDate.HasValue && u.RegistrationDate.Value.Date == regDate.Date);
                            }
                            break;
                        case "lastlogin":
                            if (DateTime.TryParse(request.FilterQuery, out var lastLogin))
                            {
                                query = query.Where(u => u.LastLogin.HasValue && u.LastLogin.Value.Date == lastLogin.Date);
                            }
                            break;
                        case "status":
                            query = query.Where(u => u.Status.Contains(request.FilterQuery));
                            break;
                        default:
                            break;
                    }
                }

                // Sorting
                if (!string.IsNullOrEmpty(request.SortBy))
                {
                    if (request.IsAscending == true)
                    {
                        query = request.SortBy.ToLower() switch
                        {
                            "username" => query.OrderBy(u => u.Username),
                            "email" => query.OrderBy(u => u.Email),
                            "phonenumber" => query.OrderBy(u => u.PhoneNumber),
                            "registrationdate" => query.OrderBy(u => u.RegistrationDate),
                            "lastlogin" => query.OrderBy(u => u.LastLogin),
                            "status" => query.OrderBy(u => u.Status),
                            _ => query.OrderBy(u => u.Username) // Default sort
                        };
                    }
                    else if (request.IsAscending == false)
                    {
                        query = request.SortBy.ToLower() switch
                        {
                            "username" => query.OrderByDescending(u => u.Username),
                            "email" => query.OrderByDescending(u => u.Email),
                            "phonenumber" => query.OrderByDescending(u => u.PhoneNumber),
                            "registrationdate" => query.OrderByDescending(u => u.RegistrationDate),
                            "lastlogin" => query.OrderByDescending(u => u.LastLogin),
                            "status" => query.OrderByDescending(u => u.Status),
                            _ => query.OrderByDescending(u => u.Username) // Default sort
                        };
                    }
                }

                // Paging
                int pageNumber = request.PageNumber ?? 1; // Default to 1 if null
                int pageSize = request.PageSize ?? 10; // Default to 10 if null
                var totalRecords = await query.CountAsync();
                var users = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

                return users;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
        public async Task<Useraccount> Authenticate(string username, string password)
        {
            return await context.Useraccounts.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }

    }
}
