using System.Collections.Generic;
using System.Threading.Tasks;
using SWP391.ChildGrowthTracking.Repository.DTO;
using SWP391.ChildGrowthTracking.Repository.DTO.UserMembershipDTO;

namespace SWP391.ChildGrowthTracking.Repository
{
    public interface IUserMembership
    {
        Task<List<UserMembershipGetDTO>> GetAllUserMemberships();
        Task<UserMembershipGetDTO?> GetUserMembershipById(int membershipId);
        Task<UserMembershipGetDTO> CreateUserMembership(CreateUserMembershipDTO dto);
        Task<UserMembershipGetDTO?> UpdateUserMembership(int membershipId, UpdateUserMembershipDTO dto);
        Task<bool> DeleteUserMembership(int membershipId);
    }
}