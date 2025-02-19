

namespace SWP391.ChildGrowthTracking.Repository.DTO.PaymentDTO
{
    public class PaymentDTO
    {
        public int PaymentId { get; set; }

        public DateTime? PaymentDate { get; set; }

        public decimal? PaymentAmount { get; set; }

        public string? TransactionId { get; set; }

        public string? Status { get; set; }

        public int? Membershipid { get; set; }
    }
}
