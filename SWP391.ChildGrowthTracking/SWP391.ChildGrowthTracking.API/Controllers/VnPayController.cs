using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWD.SAPelearning.Service;
using SWP391.ChildGrowthTracking.Repository.Model;
using System.Threading.Tasks;

namespace SWSWP391.ChildGrowthTracking.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class VNPayController : ControllerBase
    {
        protected readonly Swp391ChildGrowthTrackingContext context;
        private readonly IConfiguration _configuration;

        public VNPayController(IConfiguration configuration, Swp391ChildGrowthTrackingContext context)
        {
            _configuration = configuration;
            this.context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int PaymentId)
        {
            try
            {
                // Kiểm tra nếu tồn tại Payment
                var payment = await this.context.Payments
                                   .Where(x => x.PaymentId.Equals(PaymentId))
                                   .FirstOrDefaultAsync();
                if (payment != null)
                {
                    // Lấy thông tin ghi danh từ payment
                    var userMembership = await this.context.UserMemberships
                                        .Where(x => x.Membershipid.Equals(payment.Membershipid))
                                        .FirstOrDefaultAsync();

                    if (userMembership != null)
                    {
                        string ip = "256.256.256.1"; // Địa chỉ IP (có thể lấy từ request)
                        string url = _configuration["VnPay:Url"];
                        string returnUrl = _configuration["VnPay:ReturnAdminPath"];
                        string tmnCode = _configuration["VnPay:TmnCode"];
                        string hashSecret = _configuration["VnPay:HashSecret"];
                        SVnpay pay = new SVnpay();

                        pay.AddRequestData("vnp_Version", "2.1.0"); // Phiên bản API
                        pay.AddRequestData("vnp_Command", "pay"); // Lệnh thanh toán
                        pay.AddRequestData("vnp_TmnCode", tmnCode); // Mã website merchant
                        pay.AddRequestData("vnp_Amount", ((int)(payment.PaymentAmount * 100)).ToString()); // Số tiền thanh toán (VND)
                        pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); // Ngày tạo
                        pay.AddRequestData("vnp_CurrCode", "VND"); // Đơn vị tiền tệ
                        pay.AddRequestData("vnp_IpAddr", ip); // Địa chỉ IP khách hàng
                        pay.AddRequestData("vnp_Locale", "vn"); // Ngôn ngữ hiển thị
                        pay.AddRequestData("vnp_OrderInfo", "Thanh toán sản phẩm qua hệ thống SAPelearning"); // Thông tin thanh toán
                        pay.AddRequestData("vnp_OrderType", "other"); // Loại đơn hàng
                        pay.AddRequestData("vnp_ReturnUrl", returnUrl); // URL trả về sau khi thanh toán

                        // Tạo mã hóa đơn (TransactionId)
                        string transactionId = DateTime.Now.Ticks.ToString();
                        pay.AddRequestData("vnp_TxnRef", transactionId); // TransactionId của VNPay
                        pay.AddRequestData("vnp_ExpireDate", DateTime.Now.AddHours(1).ToString("yyyyMMddHHmmss")); // Thời gian hết hạn

                        // Lưu URL thanh toán được tạo bởi VNPay
                        string paymentUrl = pay.CreateRequestUrl(url, hashSecret);

                        // Cập nhật TransactionId cho payment
                        payment.TransactionId = transactionId;
                        this.context.Payments.Update(payment);

                        if (await this.context.SaveChangesAsync() > 0)
                        {
                            // Trả về URL thanh toán để khách hàng thanh toán qua VNPay
                            return Ok(paymentUrl);
                        }
                        else
                        {
                            throw new Exception("Lỗi trong quá trình lưu vào cơ sở dữ liệu");
                        }
                    }
                    else
                    {
                        return BadRequest("Không tìm thấy thông tin ghi danh tương ứng với Payment ID.");
                    }
                }
                else
                {
                    return BadRequest("Không tìm thấy Payment với ID đã cho.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Tạo URL thanh toán cho một `Payment`
        /// </summary>
        /// <param name="PaymentId">ID của Payment</param>
        /// <returns>URL thanh toán VNPay</returns>
        [HttpGet("CreatePaymentUrl")]
        [AllowAnonymous]
        public async Task<IActionResult> CreatePaymentUrl(string PaymentId)
        {
            try
            {
                // Chuyển đổi PaymentId sang int
                if (!int.TryParse(PaymentId, out int paymentIdInt))
                {
                    return BadRequest("ID thanh toán không hợp lệ.");
                }

                // Lấy thông tin thanh toán từ cơ sở dữ liệu
                var payment = await this.context.Payments.FirstOrDefaultAsync(x => x.PaymentId == paymentIdInt);
                if (payment != null)
                {
                    // Lấy địa chỉ IP của khách hàng
                    string ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "0.0.0.0";

                    // Lấy cấu hình VNPay từ appsettings
                    string baseUrl = _configuration["Vnpay:BaseUrl"];
                    string tmnCode = _configuration["Vnpay:TmnCode"];
                    string hashSecret = _configuration["Vnpay:HashSecret"];
                    string currCode = _configuration["Vnpay:CurrCode"];
                    string locale = _configuration["Vnpay:Locale"];
                    string returnUrl = _configuration["Vnpay:UrlReturnLocal"]; // Hoặc UrlReturnAzure tùy thuộc vào môi trường

                    SVnpay pay = new SVnpay();

                    // Thêm các thông tin yêu cầu thanh toán
                    pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]);
                    pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"]);
                    pay.AddRequestData("vnp_TmnCode", tmnCode);
                    pay.AddRequestData("vnp_Amount", ((int)payment.PaymentAmount * 100).ToString()); // Số tiền cần thanh toán
                    pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
                    pay.AddRequestData("vnp_CurrCode", currCode);
                    pay.AddRequestData("vnp_IpAddr", ip);
                    pay.AddRequestData("vnp_Locale", locale);
                    pay.AddRequestData("vnp_OrderInfo", "Thanh toán sản phẩm thông qua hệ thống BCS");
                    pay.AddRequestData("vnp_OrderType", "other");
                    pay.AddRequestData("vnp_ReturnUrl", returnUrl);

                    // Tạo mã giao dịch cho VNPay
                    string transactionCode = DateTime.Now.Ticks.ToString();
                    pay.AddRequestData("vnp_TxnRef", transactionCode); // Mã hóa đơn
                    pay.AddRequestData("vnp_ExpireDate", DateTime.Now.AddHours(1).ToString("yyyyMMddHHmmss"));

                    // Tạo URL thanh toán VNPay
                    string paymentUrl = pay.CreateRequestUrl(baseUrl, hashSecret);
                    Console.WriteLine("Payment URL: " + paymentUrl); // Log link thanh toán để kiểm tra

                    // Ghi lại các tham số đã gửi đến VNPay
                    Console.WriteLine("Request Parameters: ");
                    foreach (var requestData in pay.RequestData)
                    {
                        Console.WriteLine($"{requestData.Key}: {requestData.Value}");
                    }

                    // Cập nhật transactionCode cho payment
                    payment.TransactionId = transactionCode;
                    this.context.Payments.Update(payment);

                    // Lưu thông tin vào cơ sở dữ liệu
                    if (await this.context.SaveChangesAsync() > 0)
                    {
                        return Ok(paymentUrl);
                    }
                    else
                    {
                        throw new Exception("Lỗi trong quá trình lưu vào cơ sở dữ liệu");
                    }
                }
                else
                {
                    throw new Exception("Không tồn tại ID thanh toán.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Xác nhận thanh toán từ VNPay
        /// </summary>
        /// <returns>Kết quả xác nhận thanh toán</returns>
        [HttpGet("PaymentConfirm")]
        public async Task<IActionResult> ConfirmPayment()
        {
            string returnUrl = _configuration["VnPay:ReturnPath"];
            float amount = 0;
            string status = "failed";
            if (Request.Query.Count > 0)
            {
                string vnp_HashSecret = _configuration["VnPay:HashSecret"];
                var vnpayData = Request.Query;
                SVnpay vnpay = new SVnpay();

                foreach (string s in vnpayData.Keys)
                {
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(s, vnpayData[s]);
                    }
                }

                long orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
                float vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
                amount = vnp_Amount;
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                string vnp_SecureHash = Request.Query["vnp_SecureHash"];
                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);

                if (checkSignature && vnp_ResponseCode == "00")
                {
                    status = "success";

                    string transactionId = orderId.ToString();
                    var payment = await this.context.Payments.Where(x => x.TransactionId.Equals(transactionId)).FirstOrDefaultAsync();

                    if (payment != null)
                    {
                        // Cập nhật trạng thái thanh toán khi thành công
                        payment.Status = "Completed";
                        this.context.Payments.Update(payment);
                        await this.context.SaveChangesAsync();
                    }
                }
                else
                {
                    status = "failed";
                }
            }

            return Redirect(returnUrl + "?amount=" + amount + "&status=" + status);
        }
    }
}