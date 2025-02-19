using System.Collections.Generic;
using System.Threading.Tasks;
using SWP391.ChildGrowthTracking.Repository.DTO;
using SWP391.ChildGrowthTracking.Repository.DTO.ChildDTO;

namespace SWP391.ChildGrowthTracking.Repository
{
    public interface IChild
    {
        Task<List<ChildGetDTO>> GetAllChild();
        Task<ChildGetDTO?> GetChildById(int childId);
        Task<ChildGetDTO> CreateChild(CreateChildDTO dto);
        Task<ChildGetDTO?> UpdateChild(int childId, UpdateChildDTO dto);
        Task<bool> DeleteChild(int childId);
        Task<int> GetChildCount();
        Task<bool> IsParent(int userId, int childId);
        Task<List<ChildGetDTO>> GetChildrenByUserId(int userId);
        Task<List<ChildGetDTO>> GetChildrenByGender(string gender);
        Task<bool> UpdateChildStatus(int childId, string status);
        Task<List<ChildGetDTO>> GetChildrenByAgeRange(int minAge, int maxAge);
        Task<List<ChildGetDTO>> GetChildrenByBloodType(string bloodType);
    }
}
