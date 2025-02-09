using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SWP391.ChildGrowthTracking.Repository.DTO.BlogDTO;
using SWP391.ChildGrowthTracking.Repository.Model;

namespace SWP391.ChildGrowthTracking.Repository.Services
{
    public class BlogService : IBlog
    {
        private readonly Swp391ChildGrowthTrackingContext _context;

        public BlogService(Swp391ChildGrowthTrackingContext context)
        {
            _context = context;
        }

        public async Task<List<BlogDTO>> GetAllBlogs()
        {
            return await _context.Blogs
                .Select(b => new BlogDTO
                {
                    BlogId = b.BlogId,
                    Title = b.Title,
                    Content = b.Content,
                    CreatedDate = (DateTime)b.CreatedDate,
                    ModifiedDate = b.ModifiedDate,
                    Tags = b.Tags,
                    Image = b.Image,
                    Status = b.Status,
                    Category = b.Category
                }).ToListAsync();
        }

        public async Task<BlogDTO?> GetBlogById(int blogId)
        {
            var blog = await _context.Blogs.FindAsync(blogId);
            if (blog == null) return null;

            return new BlogDTO
            {
                BlogId = blog.BlogId,
                Title = blog.Title,
                Content = blog.Content,
                CreatedDate = (DateTime)blog.CreatedDate,
                ModifiedDate = blog.ModifiedDate,
                Tags = blog.Tags,
                Image = blog.Image,
                Status = blog.Status,
                Category = blog.Category
            };
        }

        public async Task<BlogDTO> CreateBlog(BlogCreateDTO blogDto)
        {
            try
            {
                if (blogDto == null)
                    throw new ArgumentNullException(nameof(blogDto), "Blog data cannot be null.");

                // Kiểm tra tiêu đề đã tồn tại chưa
                bool isTitleExist = await _context.Blogs
                    .AnyAsync(b => b.Title.ToLower() == blogDto.Title.ToLower());

                if (isTitleExist)
                    throw new Exception("A blog with the same title already exists.");

                // Map DTO to the Blog entity
                var newBlog = new Blog
                {
                    Title = blogDto.Title,
                    Content = blogDto.Content,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = null, // Blog mới tạo, chưa có chỉnh sửa
                    Tags = blogDto.Tags,
                    Image = blogDto.Image,
                    Status = "Draft", // Mặc định là "Draft"
                    Category = blogDto.Category
                };

                // Add the new blog to the database
                _context.Blogs.Add(newBlog);
                await _context.SaveChangesAsync(); // Lưu và lấy ID mới

                // Fetch and return the created blog
                return await GetBlogById(newBlog.BlogId);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while creating the blog: {ex.Message}");
            }
        }



        public async Task<BlogDTO> UpdateBlog(int blogId, BlogCreateDTO blogDto)
        {
            if (blogDto == null)
                throw new ArgumentNullException(nameof(blogDto), "Blog data cannot be null.");

            // 🔍 Tìm Blog theo ID
            var existingBlog = await _context.Blogs.FindAsync(blogId);
            if (existingBlog == null)
                throw new Exception("Blog not found.");

            // 🔍 Kiểm tra xem tiêu đề mới đã tồn tại chưa (trừ chính Blog đang cập nhật)
            bool isTitleExist = await _context.Blogs
                .AnyAsync(b => b.Title.ToLower() == blogDto.Title.ToLower() && b.BlogId != blogId);

            if (isTitleExist)
            {
                throw new Exception("A blog with the same title already exists.");
            }

            // ✅ Cập nhật thông tin Blog
            existingBlog.Title = blogDto.Title;
            existingBlog.Content = blogDto.Content;
            existingBlog.Tags = blogDto.Tags;
            existingBlog.Image = blogDto.Image;
            existingBlog.Category = blogDto.Category;
            existingBlog.ModifiedDate = DateTime.Now;

            _context.Blogs.Update(existingBlog);
            await _context.SaveChangesAsync();

            return new BlogDTO
            {
                BlogId = existingBlog.BlogId,
                Title = existingBlog.Title,
                Content = existingBlog.Content,
                CreatedDate = (DateTime)existingBlog.CreatedDate,
                ModifiedDate = existingBlog.ModifiedDate,
                Tags = existingBlog.Tags,
                Image = existingBlog.Image,
                Status = existingBlog.Status,
                Category = existingBlog.Category
            };
        }


        public async Task<bool> DeleteBlog(int blogId)
        {
            var blog = await _context.Blogs.FindAsync(blogId);
            if (blog == null) return false;

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ApproveBlog(int blogId)
        {
            var blog = await _context.Blogs.FindAsync(blogId);
            if (blog == null) return false;

            blog.Status = "Active";
            blog.ModifiedDate = DateTime.Now;

            _context.Blogs.Update(blog);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
