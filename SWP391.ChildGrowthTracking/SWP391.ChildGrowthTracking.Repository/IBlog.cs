using System.Collections.Generic;
using System.Threading.Tasks;
using SWP391.ChildGrowthTracking.Repository.DTO.BlogDTO;

namespace SWP391.ChildGrowthTracking.Repository.Services
{
    public interface IBlog
    {
        Task<List<BlogDTO>> GetAllBlogs();
        Task<BlogDTO?> GetBlogById(int blogId);
        Task<BlogDTO> CreateBlog(BlogCreateDTO blogDto);
        Task<BlogDTO> UpdateBlog(int blogId, BlogCreateDTO blogDto);
        Task<bool> DeleteBlog(int blogId);
        Task<bool> ApproveBlog(int blogId);
    }
}
