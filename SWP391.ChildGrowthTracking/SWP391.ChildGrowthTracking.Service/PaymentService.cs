using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SWP391.ChildGrowthTracking.Repository;
using SWP391.ChildGrowthTracking.Repository.DTO.PaymentDTO;
using SWP391.ChildGrowthTracking.Repository.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SWP391.ChildGrowthTracking.Service
{
    public class PaymentService : IPayment
    {
        private readonly IConfiguration _configuration;
        private readonly Swp391ChildGrowthTrackingContext _context;

        public PaymentService(Swp391ChildGrowthTrackingContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<PaymentDTO> CreatePayment(int membershipId)
        {
            try
            {
                var membership = await _context.UserMemberships
                    .Include(m => m.Package) // Include Package to get Price
                    .FirstOrDefaultAsync(x => x.Membershipid == membershipId);

                if (membership != null && membership.Package != null)
                {
                    var payment = new Payment
                    {
                        Membershipid = membership.Membershipid,
                        PaymentAmount = membership.Package.Price ?? 0, // Get price from Package
                        PaymentDate = DateTime.Now,
                        Status = "Pending"
                    };

                    await _context.Payments.AddAsync(payment);
                    await _context.SaveChangesAsync();

                    return new PaymentDTO
                    {
                        PaymentId = payment.PaymentId,
                        PaymentDate = payment.PaymentDate,
                        PaymentAmount = payment.PaymentAmount,
                        Status = payment.Status,
                        Membershipid = payment.Membershipid
                    };
                }
                throw new Exception("Membership or Package not found.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating payment: " + ex.Message);
            }
        }

        public async Task<bool> DeletePayment(int paymentId)
        {
            try
            {
                var payment = await _context.Payments.FirstOrDefaultAsync(x => x.PaymentId == paymentId);
                if (payment != null)
                {
                    _context.Payments.Remove(payment);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting payment: " + ex.Message);
            }
        }

        public async Task<PaymentDTO> GetPayment(int membershipId)
        {
            try
            {
                var payment = await _context.Payments
                    .Where(x => x.Membershipid == membershipId)
                    .FirstOrDefaultAsync();

                if (payment != null)
                {
                    return new PaymentDTO
                    {
                        PaymentId = payment.PaymentId,
                        PaymentDate = payment.PaymentDate,
                        PaymentAmount = payment.PaymentAmount,
                        Status = payment.Status,
                        Membershipid = payment.Membershipid
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving payment: " + ex.Message);
            }
        }

        public async Task<PaymentDTO> UpdatePaymentStatus(int paymentId, string status)
        {
            try
            {
                var payment = await _context.Payments.FindAsync(paymentId);

                if (payment == null)
                {
                    throw new Exception($"Payment with ID {paymentId} not found.");
                }

                payment.Status = status;
                _context.Payments.Update(payment);
                await _context.SaveChangesAsync();

                return new PaymentDTO
                {
                    PaymentId = payment.PaymentId,
                    PaymentDate = payment.PaymentDate,
                    PaymentAmount = payment.PaymentAmount,
                    Status = payment.Status,
                    Membershipid = payment.Membershipid
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating payment status: " + ex.Message);
            }
        }
    }
}
