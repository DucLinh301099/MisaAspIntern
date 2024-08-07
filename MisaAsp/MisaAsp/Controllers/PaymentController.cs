using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisaAsp.Controllers.Base;
using MisaAsp.Models.Ulti;
using MisaAsp.Models.ViewModel;

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MisaAsp.Services;

namespace MisaAsp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService, ResOutput response) : base(response)
        {
            _paymentService = paymentService;
        }

        /// <summary>
        /// Api tạo mới một payment
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create-payment")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreatePayment(PaymentMasterVM payment)
        {
        
            var paymentMasterId = await _paymentService.AddPaymentAsync(payment, payment.PaymentDetails);

            if (paymentMasterId > 0)
            {
                _response.HandleSuccess("Tạo mới thành công", new { PaymentMasterId = paymentMasterId });
            }
            else
            {
                _response.HandleError("Tạo mới thất bại", new { PaymentMasterId = paymentMasterId });
            }
            return Ok(_response);
        }

        /// <summary>
        /// Api lấy tất cả thông tin của payments
        /// </summary>
        /// <returns></returns>
        [HttpGet("withdraw-list")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();

            if (payments != null && payments.Any())
            {
                _response.HandleSuccess("Lấy thông tin payments thành công", payments);
            }
            else
            {
                _response.HandleError("Lấy thông tin payments thất bại");
            }
            return Ok(_response);
        }


    }

    
}
