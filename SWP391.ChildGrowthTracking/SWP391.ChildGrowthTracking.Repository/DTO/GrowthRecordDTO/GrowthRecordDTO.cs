

namespace SWP391.ChildGrowthTracking.Repository.DTO.GrowthRecordDTO
{
    public class GrowthRecordDTO
    {
        public int RecordId { get; set; }
        public DateTime? Month { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public double? Bmi { get; set; }
        public double? HeadCircumference { get; set; }
        public double? UpperArmCircumference { get; set; }
        public bool? RecordedByUser { get; set; }
        public string? Notes { get; set; }
        public int? Old { get; set; }
        public int ChildId { get; set; } // Liên kết với ChildId
    }

}
