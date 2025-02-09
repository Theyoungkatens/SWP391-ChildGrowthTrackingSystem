using System.Collections.Generic;
using System.Threading.Tasks;
using SWP391.ChildGrowthTracking.Repository.DTO;
using SWP391.ChildGrowthTracking.Repository.DTO.MembershipPackageDTO;

namespace SWP391.ChildGrowthTracking.Repository
{
    public interface IMembershipPackage
    {
        Task<List<MembershipPackageDTO>> GetAllPackages();
        Task<MembershipPackageDTO?> GetPackageById(int packageId);
        Task<MembershipPackageDTO> CreatePackage(MembershipPackageCreateDTO packageDto);
        Task<MembershipPackageDTO> UpdatePackage(int packageId, MembershipPackageUpdateDTO packageDto);
        Task<bool> DeletePackage(int packageId);
        Task<bool> ApprovePackage(int packageId);
        Task<bool> DeactivatePackage(int packageId);
    }
}
