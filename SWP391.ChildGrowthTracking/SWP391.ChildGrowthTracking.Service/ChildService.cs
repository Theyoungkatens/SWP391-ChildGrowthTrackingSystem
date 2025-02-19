using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SWP391.ChildGrowthTracking.Repository.DTO;
using SWP391.ChildGrowthTracking.Repository.DTO.ChildDTO;
using SWP391.ChildGrowthTracking.Repository.Model;

namespace SWP391.ChildGrowthTracking.Repository.Services
{
    public class ChildService : IChild
    {
        private readonly Swp391ChildGrowthTrackingContext _context;

        public ChildService(Swp391ChildGrowthTrackingContext context)
        {
            _context = context;
        }

        // Get all children
        public async Task<List<ChildGetDTO>> GetAllChild()
        {
            return await _context.Childs
                .Select(c => new ChildGetDTO
                {
                    ChildId = c.ChildId,
                    UserId = c.UserId,
                    Name = c.Name,
                    DateOfBirth = c.DateOfBirth,
                    Gender = c.Gender,
                    BirthWeight = c.BirthWeight,
                    BirthHeight = c.BirthHeight,
                    BloodType = c.BloodType,
                    Allergies = c.Allergies,
                    Status = c.Status,
                    Relationship = c.Relationship
                }).ToListAsync();
        }

        // Get a child by its ID
        public async Task<ChildGetDTO?> GetChildById(int childId)
        {
            return await _context.Childs
                .Where(c => c.ChildId == childId)
                .Select(c => new ChildGetDTO
                {
                    ChildId = c.ChildId,
                    UserId = c.UserId,
                    Name = c.Name,
                    DateOfBirth = c.DateOfBirth,
                    Gender = c.Gender,
                    BirthWeight = c.BirthWeight,
                    BirthHeight = c.BirthHeight,
                    BloodType = c.BloodType,
                    Allergies = c.Allergies,
                    Status = c.Status,
                    Relationship = c.Relationship
                }).FirstOrDefaultAsync();
        }

        // Create a new child record
        public async Task<ChildGetDTO> CreateChild(CreateChildDTO dto)
        {
            // Check if the user has a membership
            var userMembership = await _context.UserMemberships
                .FirstOrDefaultAsync(um => um.UserId == dto.UserId);

            if (userMembership == null)
            {
                throw new Exception("User does not have a membership.");
            }

            // Ensure the membership status is 'Complete'
            if (userMembership.SubscriptionStatus != "Complete")
            {
                throw new Exception("User membership is not complete.");
            }

            // Check if a child with the same name already exists for the user
            bool childExists = await _context.Childs
                .AnyAsync(c => c.UserId == dto.UserId && c.Name == dto.Name);

            if (childExists)
            {
                throw new Exception($"A child with the name '{dto.Name}' already exists.");
            }

            // Proceed to create the new child record
            var newChild = new Child
            {
                UserId = dto.UserId,
                Name = dto.Name,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                BirthWeight = dto.BirthWeight,
                BirthHeight = dto.BirthHeight,
                BloodType = dto.BloodType,
                Allergies = dto.Allergies,
                Status = "Active", // Set Status to "Active"
                Relationship = dto.Relationship
            };

            try
            {
                _context.Childs.Add(newChild);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the full exception details for debugging
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                throw;  // Rethrow the exception after logging
            }

            // Return the created child DTO
            return await GetChildById(newChild.ChildId)
                ?? throw new Exception("Error retrieving the newly created child record.");
        }

        // Update an existing child record
        public async Task<ChildGetDTO?> UpdateChild(int childId, UpdateChildDTO dto)
        {
            var child = await _context.Childs.FindAsync(childId);
            if (child == null) return null;

            // Update fields only if they are provided in the DTO
            child.UserId = dto.UserId ?? child.UserId;
            child.Name = dto.Name ?? child.Name;
            child.DateOfBirth = dto.DateOfBirth ?? child.DateOfBirth;
            child.Gender = dto.Gender ?? child.Gender;
            child.BirthWeight = dto.BirthWeight ?? child.BirthWeight;
            child.BirthHeight = dto.BirthHeight ?? child.BirthHeight;
            child.BloodType = dto.BloodType ?? child.BloodType;
            child.Allergies = dto.Allergies ?? child.Allergies;
            child.Status = dto.Status ?? child.Status;
            child.Relationship = dto.Relationship ?? child.Relationship;

            _context.Childs.Update(child);
            await _context.SaveChangesAsync();

            return await GetChildById(childId);
        }

        // Delete an existing child record
        public async Task<bool> DeleteChild(int childId)
        {
            var child = await _context.Childs.FindAsync(childId);
            if (child == null) return false;

            // Delete related GrowthRecord and Alert
              var alerts = await _context.Alerts.Where(a => a.ChildId == childId).ToListAsync();

            _context.Alerts.RemoveRange(alerts);

            // Now delete the child
            _context.Childs.Remove(child);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetChildCount()
        {
            return await _context.Childs.CountAsync();
        }
        // Kiểm tra xem user có phải là cha mẹ của đứa trẻ không
        public async Task<bool> IsParent(int userId, int childId)
        {
            return await _context.Childs.AnyAsync(c => c.ChildId == childId && c.UserId == userId);
        }

        // Lấy danh sách trẻ em theo UserId
        public async Task<List<ChildGetDTO>> GetChildrenByUserId(int userId)
        {
            return await _context.Childs
                .Where(c => c.UserId == userId)
                .Select(c => new ChildGetDTO
                {
                    ChildId = c.ChildId,
                    UserId = c.UserId,
                    Name = c.Name,
                    DateOfBirth = c.DateOfBirth,
                    Gender = c.Gender,
                    BirthWeight = c.BirthWeight,
                    BirthHeight = c.BirthHeight,
                    BloodType = c.BloodType,
                    Allergies = c.Allergies,
                    Status = c.Status,
                    Relationship = c.Relationship
                }).ToListAsync();
        }

        // Lấy danh sách trẻ em theo giới tính
        public async Task<List<ChildGetDTO>> GetChildrenByGender(string gender)
        {
            return await _context.Childs
                .Where(c => c.Gender == gender)
                .Select(c => new ChildGetDTO
                {
                    ChildId = c.ChildId,
                    UserId = c.UserId,
                    Name = c.Name,
                    DateOfBirth = c.DateOfBirth,
                    Gender = c.Gender,
                    BirthWeight = c.BirthWeight,
                    BirthHeight = c.BirthHeight,
                    BloodType = c.BloodType,
                    Allergies = c.Allergies,
                    Status = c.Status,
                    Relationship = c.Relationship
                }).ToListAsync();
        }
        public async Task<bool> UpdateChildStatus(int childId, string status)
        {
            var child = await _context.Childs.FindAsync(childId);
            if (child == null) return false;

            child.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<ChildGetDTO>> GetChildrenByAgeRange(int minAge, int maxAge)
        {
            var today = DateTime.Today;
            return await _context.Childs
                .Where(c => c.DateOfBirth.HasValue &&
           (today.Year - c.DateOfBirth.Value.Year) >= minAge &&
           (today.Year - c.DateOfBirth.Value.Year) <= maxAge)

                .Select(c => new ChildGetDTO
                {
                    ChildId = c.ChildId,
                    UserId = c.UserId,
                    Name = c.Name,
                    DateOfBirth = c.DateOfBirth,
                    Gender = c.Gender,
                    BirthWeight = c.BirthWeight,
                    BirthHeight = c.BirthHeight,
                    BloodType = c.BloodType,
                    Allergies = c.Allergies,
                    Status = c.Status,
                    Relationship = c.Relationship
                }).ToListAsync();
        }
        public async Task<List<ChildGetDTO>> GetChildrenByBloodType(string bloodType)
        {
            return await _context.Childs
                .Where(c => c.BloodType == bloodType)
                .Select(c => new ChildGetDTO
                {
                    ChildId = c.ChildId,
                    UserId = c.UserId,
                    Name = c.Name,
                    DateOfBirth = c.DateOfBirth,
                    Gender = c.Gender,
                    BirthWeight = c.BirthWeight,
                    BirthHeight = c.BirthHeight,
                    BloodType = c.BloodType,
                    Allergies = c.Allergies,
                    Status = c.Status,
                    Relationship = c.Relationship
                }).ToListAsync();
        }


    }
}
