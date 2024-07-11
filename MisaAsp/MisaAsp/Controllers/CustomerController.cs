using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MisaAsp.Models.Ulti;
using MisaAsp.Models.ViewModel;
using MisaAsp.Services;

namespace MisaAsp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        /// <summary>
        /// Api thêm mới 1 Customer
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("createCustomer")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomer request)
        {
            var res = new ResOutput();

            try
            {
                if (!ModelState.IsValid)
                {
                    res.HandleError("Thất bại");
                }
                else
                {
                    var customerId = await _customerService.CreateCustomerAsync(request);

                    if (customerId > 0)
                    {
                        res.HandleSuccess("Tạo mới thành công", new { CustomerId = customerId });
                    }
                    else
                    {
                        res.HandleError("Tạo mới thất bại", new { CustomerId = customerId });
                    }
                }

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.HandleError(ex.Message);
                return BadRequest(res);
            }
        }
        /// <summary>
        /// Api lấy tất cả thông tin của customer
        /// </summary>
        /// <returns></returns>
        [HttpGet("customer")]
        //[Authorize(Roles = "Admin")] // Chỉ admin mới có quyền truy cập
        [AllowAnonymous] // toàn quyền truy cập
        public async Task<IActionResult> GetCustomer()
        {
            var customers = await _customerService.GetAllCustomerAsync();
            var res = new ResOutput();

            if (customers != null && customers.Any())
            {
                res.HandleSuccess("Lấy thông tin Customer thành công", customers);
                return Ok(res);
            }
            else
            {
                res.HandleError("Lấy thông tin Customer thất bại");
                return BadRequest(res);
            }
        }

    }
}
