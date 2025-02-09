using System;

namespace SWP391.ChildGrowthTracking.Repository.DTO.BlogDTO
{
    public class BlogDTO
    {
        public int BlogId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? Tags { get; set; }
        public string? Image { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Category { get; set; }
    }
}
