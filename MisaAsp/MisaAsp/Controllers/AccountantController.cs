using Microsoft.AspNetCore.Mvc;
using MisaAsp.Controllers.Base;
using MisaAsp.Models.Ulti;
using MisaAsp.Services;
using MisaAsp.Services.Base;

namespace MisaAsp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountantController : BaseController
    {
        private readonly IAccountantService _accountantService;

        public AccountantController(IAccountantService accountantService, ResOutput response, IBaseService baseService) : base(response, baseService)
        {
            _accountantService = accountantService;
        }

        /// <summary>
        /// Api hiển thị tất cả các Accountant service trong homepage
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetServices()
        {
            
            var services = await _accountantService.GetServicesAsync();

            if (services != null && services.Any())
            {
                _response.HandleSuccess("Lấy thông tin dịch vụ thành công", services);
            }
            else
            {
                _response.HandleError("Lấy thông tin dịch vụ thất bại");
            }
            return Ok(_response);
        }
    }
}
