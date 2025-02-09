using Microsoft.AspNetCore.Mvc;
using SWP391.ChildGrowthTracking.Service;
using SWP391.ChildGrowthTracking.Repository.DTO.BlogDTO;
using System;
using System.Threading.Tasks;
using SWP391.ChildGrowthTracking.Repository.Services;

namespace SWP391.ChildGrowthTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlog _blogService;

        public BlogController(IBlog blogService)
        {
            _blogService = blogService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllBlogs()
        {
            try
            {
                var blogs = await _blogService.GetAllBlogs();
                return Ok(new { success = true, data = blogs });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogById(int id)
        {
            try
            {
                var blog = await _blogService.GetBlogById(id);
                if (blog == null)
                    return NotFound(new { success = false, message = "Blog not found." });

                return Ok(new { success = true, data = blog });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error retrieving blog", details = ex.Message });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateBlog([FromBody] BlogCreateDTO request)
        {
            try
            {
                var blog = await _blogService.CreateBlog(request);
                return Ok(new { success = true, message = "Blog created successfully.", data = blog });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateBlog(int id, [FromBody] BlogCreateDTO request)
        {
            try
            {
                var updatedBlog = await _blogService.UpdateBlog(id, request);
                if (updatedBlog == null)
                    return NotFound(new { success = false, message = "Blog not found." });

                return Ok(new
                {
                    success = true,
                    message = "Blog updated successfully.",
                    data = updatedBlog
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = $"Error updating blog: {ex.Message}" });
            }
        }
        [HttpPut("approve/{blogId}")]
        public async Task<IActionResult> ApproveBlog(int blogId)
        {
            var result = await _blogService.ApproveBlog(blogId);
            if (!result)
                return NotFound(new { message = "Blog not found or already approved." });

            return Ok(new { message = "Blog approved successfully." });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            try
            {
                await _blogService.DeleteBlog(id);
                return Ok(new { success = true, message = "Blog deleted successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error deleting blog", details = ex.Message });
            }
        }

        
    }
}
