using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SWP391.ChildGrowthTracking.Repository.DTO;
using SWP391.ChildGrowthTracking.Repository.DTO.MembershipPackageDTO;
using SWP391.ChildGrowthTracking.Repository.Model;

namespace SWP391.ChildGrowthTracking.Repository.Services
{
    public class MembershipPackageService : IMembershipPackage
    {
        private readonly Swp391ChildGrowthTrackingContext _context;

        public MembershipPackageService(Swp391ChildGrowthTrackingContext context)
        {
            _context = context;
        }

        public async Task<List<MembershipPackageDTO>> GetAllPackages()
        {
            return await _context.MembershipPackages
                .Select(mp => new MembershipPackageDTO
                {
                    PackageId = mp.PackageId,
                    PackageName = mp.PackageName,
                    Description = mp.Description,
                    Price = mp.Price,
                    DurationMonths = mp.DurationMonths,
                    Features = mp.Features,
                    MaxChildrenAllowed = mp.MaxChildrenAllowed,
                    Status = mp.Status
                }).ToListAsync();
        }

        public async Task<MembershipPackageDTO?> GetPackageById(int packageId)
        {
            var package = await _context.MembershipPackages
                .Where(mp => mp.PackageId == packageId)
                .Select(mp => new MembershipPackageDTO
                {
                    PackageId = mp.PackageId,
                    PackageName = mp.PackageName,
                    Description = mp.Description,
                    Price = mp.Price,
                    DurationMonths = mp.DurationMonths,
                    Features = mp.Features,
                    MaxChildrenAllowed = mp.MaxChildrenAllowed,
                    Status = mp.Status
                }).FirstOrDefaultAsync();

            return package;
        }

        public async Task<MembershipPackageDTO> CreatePackage(MembershipPackageCreateDTO dto)
        {
            var newPackage = new MembershipPackage
            {
                PackageName = dto.PackageName,
                Description = dto.Description,
                Price = dto.Price,
                DurationMonths = dto.DurationMonths,
                Features = dto.Features,
                MaxChildrenAllowed = dto.MaxChildrenAllowed,
                Status = "Inactive"
            };

            _context.MembershipPackages.Add(newPackage);
            await _context.SaveChangesAsync();

            return await GetPackageById(newPackage.PackageId) ?? throw new Exception("Error retrieving the newly created package.");
        }

        public async Task<MembershipPackageDTO?> UpdatePackage(int packageId, MembershipPackageUpdateDTO dto)
        {
            var package = await _context.MembershipPackages.FindAsync(packageId);
            if (package == null) return null;

            package.PackageName = dto.PackageName;
            package.Description = dto.Description;
            package.Price = dto.Price;
            package.DurationMonths = dto.DurationMonths;
            package.Features = dto.Features;
            package.MaxChildrenAllowed = dto.MaxChildrenAllowed;

            _context.MembershipPackages.Update(package);
            await _context.SaveChangesAsync();

            return await GetPackageById(packageId);
        }

        public async Task<bool> DeletePackage(int packageId)
        {
            var package = await _context.MembershipPackages.FindAsync(packageId);
            if (package == null) return false;

            _context.MembershipPackages.Remove(package);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ApprovePackage(int packageId)
        {
            var package = await _context.MembershipPackages.FindAsync(packageId);
            if (package == null) return false;

            package.Status = "Active";
            _context.MembershipPackages.Update(package);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeactivatePackage(int packageId)
        {
            var package = await _context.MembershipPackages.FindAsync(packageId);
            if (package == null) return false;

            package.Status = "Inactive";
            _context.MembershipPackages.Update(package);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
