using System;

namespace SWP391.ChildGrowthTracking.Repository.DTO.BlogDTO
{
    public class BlogCreateDTO
    {
        
        public string Title { get; set; } 
        public string Content { get; set; } 
        
        public string? Tags { get; set; }
        public string? Image { get; set; }
        
        public string? Category { get; set; }
    }
}
