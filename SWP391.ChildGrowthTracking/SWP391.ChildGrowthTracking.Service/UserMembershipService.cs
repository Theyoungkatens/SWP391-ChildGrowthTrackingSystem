using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SWP391.ChildGrowthTracking.Repository.DTO;
using SWP391.ChildGrowthTracking.Repository.DTO.UserMembershipDTO;
using SWP391.ChildGrowthTracking.Repository.Model;

namespace SWP391.ChildGrowthTracking.Repository.Services
{
    public class UserMembershipService : IUserMembership
    {
        private readonly Swp391ChildGrowthTrackingContext _context;

        public UserMembershipService(Swp391ChildGrowthTrackingContext context)
        {
            _context = context;
        }

        public async Task<List<UserMembershipGetDTO>> GetAllUserMemberships()
        {
            return await _context.UserMemberships
                .Select(um => new UserMembershipGetDTO
                {
                    Membershipid = um.Membershipid,
                    UserId = um.UserId,
                    PackageId = um.PackageId,
                    StartDate = um.StartDate,
                    EndDate = um.EndDate,
                    SubscriptionStatus = um.SubscriptionStatus,
                    CouponCode = um.CouponCode
                }).ToListAsync();
        }

        public async Task<UserMembershipGetDTO?> GetUserMembershipById(int membershipId)
        {
            return await _context.UserMemberships
                .Where(um => um.Membershipid == membershipId)
                .Select(um => new UserMembershipGetDTO
                {
                    Membershipid = um.Membershipid,
                    UserId = um.UserId,
                    PackageId = um.PackageId,
                    StartDate = um.StartDate,
                    EndDate = um.EndDate,
                    SubscriptionStatus = um.SubscriptionStatus,
                    CouponCode = um.CouponCode
                }).FirstOrDefaultAsync();
        }

        public async Task<UserMembershipGetDTO> CreateUserMembership(CreateUserMembershipDTO dto)
        {
            // Lấy thông tin gói membership
            var package = await _context.MembershipPackages.FindAsync(dto.PackageId);
            if (package == null)
            {
                throw new Exception("Membership package not found.");
            }

            // Kiểm tra trạng thái của gói membership
            if (package.Status != "Active")
            {
                throw new Exception("The selected membership package is not active.");
            }

            // Kiểm tra user đã có gói membership này chưa
            var existingMembership = await _context.UserMemberships
                .FirstOrDefaultAsync(m => m.UserId == dto.UserId && m.PackageId == dto.PackageId);

            if (existingMembership != null)
            {
                throw new Exception("User already has an active membership for this package.");
            }

            // Ngày bắt đầu là hôm nay
            var startDate = DateTime.UtcNow;
            // Ngày kết thúc = StartDate + DurationMonths
            var endDate = startDate.AddMonths((int)package.DurationMonths);

            var newUserMembership = new UserMembership
            {
                UserId = dto.UserId,
                PackageId = dto.PackageId,
                StartDate = startDate,
                EndDate = endDate,
                SubscriptionStatus = "Pending", // Default status
            };

            _context.UserMemberships.Add(newUserMembership);
            await _context.SaveChangesAsync();

            return await GetUserMembershipById(newUserMembership.Membershipid)
                ?? throw new Exception("Error retrieving the newly created membership.");
        }



        public async Task<UserMembershipGetDTO?> UpdateUserMembership(int membershipId, UpdateUserMembershipDTO dto)
        {
            var membership = await _context.UserMemberships.FindAsync(membershipId);
            if (membership == null) return null;

            membership.PackageId = dto.PackageId ?? membership.PackageId;
            membership.StartDate = dto.StartDate ?? membership.StartDate;
            membership.EndDate = dto.EndDate ?? membership.EndDate;
            membership.SubscriptionStatus = dto.SubscriptionStatus ?? "Pending";

            _context.UserMemberships.Update(membership);
            await _context.SaveChangesAsync();

            return await GetUserMembershipById(membershipId);
        }

        public async Task<bool> DeleteUserMembership(int membershipId)
        {
            var membership = await _context.UserMemberships.FindAsync(membershipId);
            if (membership == null) return false;

            _context.UserMemberships.Remove(membership);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
