using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisaAsp.Models.Ulti;
using MisaAsp.Models.ViewModel;
using MisaAsp.Services;
using System.Linq;
using System.Threading.Tasks;

namespace MisaAsp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService, ResOutput _response) : base(_response)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Api thêm mới 1 khách hàng mới
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create-customer")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerVM request)
        {
            if (!ModelState.IsValid)
            {

                _response.HandleError();
                return Ok(_response);
            }

            var customerId = await _customerService.CreateCustomerAsync(request);
           

            if (customerId > 0)
            {
                _response.HandleSuccess("Tạo mới thành công", new { CustomerId = customerId });
                
            }
            else
            {
                _response.HandleError("Tạo mới thất bại", new { CustomerId = customerId });
                
            }
            return Ok(_response);
        }

        /// <summary>
        /// Api lấy tất cả thông tin của customer
        /// </summary>
        /// <returns></returns>
        [HttpGet("customer")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCustomer()
        {
            var customers = await _customerService.GetAllCustomerAsync();
            

            if (customers != null && customers.Any())
            {
                _response.HandleSuccess("Lấy thông tin khách hàng thành công", customers);
                
            }
            else
            {
                _response.HandleError("Lấy thông tin khách hàng thất bại");
                
            }
            return Ok(_response);
        }
    }
}
