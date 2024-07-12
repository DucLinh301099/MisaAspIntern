using Microsoft.AspNetCore.Mvc;
using MisaAsp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using MisaAsp.Models.DTO;
using MisaAsp.Models.Ulti;

namespace MisaAsp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountantController : BaseController
    {
        private readonly IAccountantService _accountantService;

        public AccountantController(IAccountantService accountantService, ResOutput response): base(response)
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
            _response.Data = await _accountantService.GetServicesAsync();
            return Ok(_response);
        }
    }
}
