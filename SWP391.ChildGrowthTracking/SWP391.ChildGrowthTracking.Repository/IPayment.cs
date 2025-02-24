using SWP391.ChildGrowthTracking.Repository.DTO.PaymentDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.ChildGrowthTracking.Repository
{
    public interface IPayment
    {
        Task<PaymentDTO> CreatePayment(int membershipId);
        Task<bool> DeletePayment(int paymentId);
        Task<PaymentDTO> GetPayment(int membershipId);
        Task<PaymentDTO> UpdatePaymentStatus(int paymentId);
    }
}
